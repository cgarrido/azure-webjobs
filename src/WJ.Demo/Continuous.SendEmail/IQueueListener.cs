using Domain;
using Microsoft.Azure.WebJobs;
using System.Threading.Tasks;

namespace Continuous.SendEmail
{
    /// <summary>
    /// Interfaz que representa a un receptor de eventos de tipo cola
    /// </summary>
    public interface IQueueListener
    {
        /// <summary>
        /// Método que lee un mensaje de la cola email-queue
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task ReadMessage([QueueTrigger("email-queue")] User user);
    }
}
