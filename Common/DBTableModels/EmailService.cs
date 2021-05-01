using System.Collections.Generic;

namespace Common.DBTableModelsService
{
    public class EmailService 
    {
        #region SubClasses
        public class EmailMessage
        {
            public EmailMessage()
            {
                ToAddresses = new List<EmailAddress>();
                FromAddresses = new List<EmailAddress>();
            }

            public List<EmailAddress> ToAddresses { get; set; }
            public List<EmailAddress> FromAddresses { get; set; }
            public string Subject { get; set; }
            public string Content { get; set; }
        }

        public class EmailAddress
        {
            public string Name { get; set; }
            public string Address { get; set; }
        }

        public interface IEmailConfiguration
        {
            string SmtpServer { get; }
            int SmtpPort { get; }
            string SmtpUsername { get; set; }
            string SmtpPassword { get; set; }

            string PopServer { get; }
            int PopPort { get; }
            string PopUsername { get; }
            string PopPassword { get; }
        }

        public class EmailConfiguration : IEmailConfiguration
        {
            public string SmtpServer { get; set; }
            public int SmtpPort { get; set; }
            public string SmtpUsername { get; set; }
            public string SmtpPassword { get; set; }

            public string PopServer { get; set; }
            public int PopPort { get; set; }
            public string PopUsername { get; set; }
            public string PopPassword { get; set; }
        }
        #endregion

       

    }

}
