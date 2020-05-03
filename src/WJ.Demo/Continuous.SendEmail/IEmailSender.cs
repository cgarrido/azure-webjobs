using System.Threading.Tasks;

namespace Continuous.SendEmail
{
    /// <summary>
    /// Interfaz que representa a un emisor de emails
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Método que envía un email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="name"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task Send(string email, string name, string subject, string message);
    }
}
