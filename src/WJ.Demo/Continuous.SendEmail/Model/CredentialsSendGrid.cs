using System;
using System.Collections.Generic;
using System.Text;

namespace Continuous.SendEmail
{
    /// <summary>
    /// Representa las credenciales del servicio SENDGRID
    /// </summary>
    public class CredentialsSendGrid
    {
        /// <summary>
        /// API Key de la cuenta de SendGrid
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Email emisor de correos
        /// </summary>
        public string EmailFrom { get; set; }
        
        /// <summary>
        /// Nombre emisor de correos
        /// </summary>
        public string NameFrom { get; set; }
    }
}
