#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy everything
COPY ["src/Console/Console.csproj", "src/Console/"]
COPY ["src/SharedKernel/SharedKernel.csproj", "src/SharedKernel/"]
RUN dotnet restore "./src/Console/Console.csproj"
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
#ENTRYPOINT ["dotnet", "Console.dll"]


# Install CRON
USER root
RUN apt-get update -qq && apt-get -y install cron -qq --force-yes

# Add export environment variable script and schedule
COPY ./src/Console/*.sh ./
COPY ./src/Console/schedule /etc/cron.d/schedule
RUN sed -i 's/\r//' export_env.sh \
    && sed -i 's/\r//' run_app.sh \
    && sed -i 's/\r//' /etc/cron.d/schedule \
    && chmod +x export_env.sh run_app.sh \
    && chmod 0644 /etc/cron.d/schedule

# Create log file
RUN touch /var/log/cron.log
RUN chmod 0666 /var/log/cron.log

# Volume required for tail command
VOLUME /var/log

# Run Cron
CMD /app/export_env.sh && /usr/sbin/cron && tail -f /var/log/cron.log