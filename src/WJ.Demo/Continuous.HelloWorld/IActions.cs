using Microsoft.Azure.WebJobs;

namespace Continuous.HelloWorld
{
    /// <summary>
    /// Interfaz que define las acciones necesarias del webjob
    /// </summary>
    public interface IActions
    {
        /// <summary>
        /// Método que saluda
        /// </summary>
        /// <param name="timerInfo"></param>
        public void SayHello([TimerTrigger("00:00:02", RunOnStartup = true)]TimerInfo timerInfo);

        /// <summary>
        /// Método que se despide
        /// </summary>
        /// <param name="timerInfo"></param>
        public void SayBye([TimerTrigger("00:00:10", RunOnStartup = true)]TimerInfo timerInfo);
    }
}
