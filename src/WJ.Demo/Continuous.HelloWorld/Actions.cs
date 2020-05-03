using Microsoft.Azure.WebJobs;
using Serilog;

namespace Continuous.HelloWorld
{
    /// <summary>
    /// Implementa la las acciones de la interfaz <see cref="IActions"/>
    /// </summary>
    public class Actions : IActions
    {
        ///<inheritdoc/>
        public void SayHello([TimerTrigger("00:00:02", RunOnStartup = true)]TimerInfo timerInfo)
        {
            Log.Information("Hello");
        }

        ///<inheritdoc/>
        public void SayBye([TimerTrigger("00:00:10", RunOnStartup = true)]TimerInfo timerInfo)
        {
            Log.Information("Bye");
        }
    }
}
