using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace Continuous.SendEmail
{
    /// <summary>
    /// Implementa la interfaz <see cref="IEmailSender"/> haciendo uso de SendGrid
    /// </summary>
    public class EmailSenderSendGrid : IEmailSender
    {
        private CredentialsSendGrid Credentials;
        SendGridClient Client;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="credentialsConfig"></param>
        public EmailSenderSendGrid(CredentialsSendGrid credentialsConfig)
        {
            Credentials = credentialsConfig;
            Client = new SendGridClient(credentialsConfig.ApiKey);
        }

        ///<inheritdoc/>
        public async Task Send(string email, string name, string subject, string message)
        {
            var from = new EmailAddress(Credentials.EmailFrom, Credentials.NameFrom);
            var to = new EmailAddress(email, name);
            var emailSendGrid = MailHelper.CreateSingleEmail(from, to, subject, message, message);
            await Client.SendEmailAsync(emailSendGrid);
        }
    }
}
