using Serilog;

namespace Triggered.HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
            Log.Information("Hello World!!!");
            Log.CloseAndFlush();
        }
    }
}
