using Application.Common.Interfaces;
using Application.Common.Interfaces.EmailService;
using Infrastructure.BackgroundJobs;
using Infrastructure.EmailService;
using Infrastructure.Idempotence;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Quartz;
using SharedKernel.Interfaces;
using SharedKernel.Services;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Decorate(typeof(INotificationHandler<>), typeof(IdempotentDomainEventHandler<>));

        services.AddPersistance(configuration);

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<ILoggerProvider, LoggerProvider>();

        services.AddEmailSerivce(configuration);

        return services;
    }

    private static IServiceCollection AddPersistance(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<AuthenticationDbContext>(options =>           
                options.UseSqlServer(
                    connectionString,
                    o => o.MigrationsHistoryTable(
                        HistoryRepository.DefaultTableName,
                        GenerateSchemaName(nameof(AuthenticationDbContext)))));

        services.AddDbContext<DotNETDbContext>(options =>
                options.UseSqlServer(
                    connectionString,
                    o => o.MigrationsHistoryTable(
                        HistoryRepository.DefaultTableName,
                        GenerateSchemaName(nameof(DotNETDbContext)))));

        services.AddQuartz(configure =>
        {
            var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));

            configure
                .AddJob<ProcessOutboxMessagesJob>(jobKey)
                .AddTrigger(
                    trigger =>
                        trigger.ForJob(jobKey)
                            .WithSimpleSchedule(
                                schedule =>
                                    schedule.WithIntervalInSeconds(10)
                                        .RepeatForever()));
        }).AddQuartzHostedService();



        services.AddRepositories();

        return services;
    }

    private static void AddEmailSerivce(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var emailSettings = new EmailSettings();
        configuration.Bind(EmailSettings.SectionName, emailSettings);
        services.AddSingleton(Options.Create(emailSettings));

        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        if (environment is "Development")
        {
            services.AddTransient<IEmailService, EmailServiceSMTP4Dev>();
        }
        else
        {
            services.AddTransient<IEmailService, EmailServiceBrevo>();
        }
    }

    private static string GenerateSchemaName(string fullName)
    {
        var onlyName = fullName.Substring(0, fullName.Length - 9);

        return onlyName.ToLower();
    }
}