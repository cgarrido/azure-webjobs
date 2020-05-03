using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Continuous.HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureWebJobs(b =>
                {
                    b.AddAzureStorageCoreServices()
                    .AddTimers();
                })
                .ConfigureLogging((context, b) =>
                {
                    b.SetMinimumLevel(LogLevel.Debug);

                    //Log with application insights
                    string appInsightsKey = context.Configuration["APPINSIGHTS_INSTRUMENTATIONKEY"];
                    if (!string.IsNullOrEmpty(appInsightsKey))
                    {
                        b.AddApplicationInsightsWebJobs(o => o.InstrumentationKey = appInsightsKey);
                    }
                    b.AddSerilog();
                })
                .UseConsoleLifetime()
                .UseSerilog((hostingContext, loggerConfiguration) =>
                {
                    var appInsightsKey = hostingContext.Configuration["APPINSIGHTS_INSTRUMENTATIONKEY"];

                    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration)
                    .WriteTo.Console()
                    .WriteTo.ApplicationInsights(appInsightsKey, TelemetryConverter.Traces);
                });

            var host = builder.Build();
            host.RunAsync().Wait();
        }
    }
}
