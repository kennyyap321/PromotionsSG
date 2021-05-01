using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using MimeKit;
using System.Linq;
using MimeKit.Text;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Common.DBTableModelsService;

namespace PromotionsSG.API.Notification.Controllers
{
    public class NotificationController : ControllerBase
    {
        private readonly ILogger<NotificationController> _logger;
        private IConfiguration _configuration;

        public NotificationController(ILogger<NotificationController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        #region Health
        public IActionResult Health()
        {
            return Ok($"{nameof(NotificationController)} healthy at : {DateTime.Now}");
        }
        #endregion


        #region Promotion
        [HttpPost]
        [Route("notification/sendEmail")]
        public async Task<bool> SendEmailAsync([FromBody] EmailService.EmailMessage emailMessage)
        {
            try
            {
                #region config
                var emailConfig = new EmailService.EmailConfiguration
                {
                    SmtpServer = _configuration.GetValue<string>("EmailConfiguration:SmtpServer"),
                    SmtpPort = Convert.ToInt32(_configuration.GetValue<string>("EmailConfiguration:SmtpPort")),
                    SmtpUsername = _configuration.GetValue<string>("EmailConfiguration:SmtpUsername"),
                    SmtpPassword = _configuration.GetValue<string>("EmailConfiguration:SmtpPassword")
                };

                var emailAddr = new EmailService.EmailAddress
                {
                    Address = "PromotionsSG@gmail.com",
                    Name = "PromotionsSG"
                };
                emailMessage.FromAddresses = new List<EmailService.EmailAddress> { emailAddr };
                //if (isPublish)
                //{
                //    emailMessage.ToAddresses =
                //    new List<EmailService.EmailAddress>()
                //    ;//sns subscriber list
                //}

                #endregion

                #region send
                var message = new MimeMessage();
                message.To.AddRange(emailMessage.ToAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
                message.From.AddRange(emailMessage.FromAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));

                message.Subject = emailMessage.Subject;
                message.Body = new TextPart(TextFormat.Html)
                {
                    Text = emailMessage.Content
                };

                using (var emailClient = new SmtpClient())
                {
                    await emailClient.ConnectAsync(emailConfig.SmtpServer, emailConfig.SmtpPort);
                    emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
                    await emailClient.AuthenticateAsync(emailConfig.SmtpUsername, emailConfig.SmtpPassword);
                    await emailClient.SendAsync(message);
                    await emailClient.DisconnectAsync(true);
                }
                #endregion
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
