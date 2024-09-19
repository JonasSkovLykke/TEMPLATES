using Console;
using Microsoft.Extensions.Configuration;
using Serilog;

IConfiguration configuration = Configurations.Initialize();

Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();

Log.CloseAndFlush();