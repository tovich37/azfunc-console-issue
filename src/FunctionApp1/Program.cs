using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.Configure<LoggerFilterOptions>(options =>
        {
            var applicationInsightsLoggerProvider = options.Rules.FirstOrDefault(rule =>
                rule.ProviderName == typeof(ApplicationInsightsLoggerProvider).FullName);

            if (applicationInsightsLoggerProvider is not null)
                options.Rules.Remove(applicationInsightsLoggerProvider);
        });
    })
    .ConfigureLogging(builder =>
    {
        // Remove the default multi lines console logger
        var loggerProviderServices = builder
            .Services
            .Where(descriptor => descriptor.ServiceType == typeof(ILoggerProvider))
            .ToList();
        var workerLoggerProviderService = loggerProviderServices
            .Single(descriptor => descriptor.ImplementationType?.Name == "WorkerLoggerProvider");
        builder.Services.Remove(workerLoggerProviderService);

        builder.AddSimpleConsole(options =>
        {
            options.IncludeScopes = false;
            options.TimestampFormat = "HH:mm:ss ";
            options.SingleLine = true;
        });
    })
    .Build();

host.Run();