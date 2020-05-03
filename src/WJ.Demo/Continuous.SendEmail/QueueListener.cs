using Domain;
using Microsoft.Azure.WebJobs;
using Serilog;
using System.Threading.Tasks;

namespace Continuous.SendEmail
{
    /// <summary>
    /// Clase que implementa la interfaz <see cref="IQueueListener"/>
    /// </summary>
    public class QueueListener : IQueueListener
    {
        IEmailSender EmailSender;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="emailSender"></param>
        public QueueListener(IEmailSender emailSender)
        {
            EmailSender = emailSender;
        }

        ///<inheritdoc/>
        public async Task ReadMessage([QueueTrigger("email-queue")] User user)
        {
            Log.Information("Recibido el usuario {0} con email {1}", user.Name, user.Email);

            await EmailSender.Send(user.Email, user.Name, "Bienvenido a Dotnetters WebJobs", $"Buenas {user.Name},<br/> Bienvenido a la demo de Dotnetters WebJobs.<br/><br/> Un cordial saludo.");
        }
    }
}
