using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Globalization;
using System.IO;

namespace Continuous.SendEmail
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureWebJobs(b =>
                {
                    b.AddAzureStorageCoreServices()
                    .AddAzureStorage();
                })
                .ConfigureLogging((context, b) =>
                {
                    b.SetMinimumLevel(LogLevel.Debug);

                    //Log with application insights
                    string appInsightsKey = context.Configuration["APPINSIGHTS_INSTRUMENTATIONKEY"];
                    if (!string.IsNullOrEmpty(appInsightsKey))
                    {
                        b.AddApplicationInsights(appInsightsKey);
                    }
                    b.AddSerilog();
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton(new CredentialsSendGrid() {
                        ApiKey = context.Configuration["SendGrid:ApiKey"],
                        EmailFrom = context.Configuration["SendGrid:EmailFrom"],
                        NameFrom = context.Configuration["SendGrid:NameFrom"]
                    });
                    services.AddSingleton<IEmailSender, EmailSenderSendGrid>();
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
