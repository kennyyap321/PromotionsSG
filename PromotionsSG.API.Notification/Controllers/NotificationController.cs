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
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using PromotionsSG.API.NotificationAPI.Repository;

namespace PromotionsSG.API.Notification.Controllers
{
    public class NotificationController : ControllerBase
    {
        private readonly ILogger<NotificationController> _logger;
        private readonly INotificationRepository _repository;
        private IConfiguration _configuration;

        public NotificationController(ILogger<NotificationController> logger, INotificationRepository notificationRepository, IConfiguration configuration)
        {
            _logger = logger;
            _repository = notificationRepository;
            _configuration = configuration;
        }

        #region Health
        public IActionResult Health()
        {
            return Ok($"{nameof(NotificationController)} healthy at : {DateTime.Now}");
        }
        #endregion


        #region Notification
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

        [HttpGet]
        [Route("notification/PublishEmail")]
        public async Task<bool> PublishEmailAsync()
        {
            _logger.LogInformation("email Repo before retrieve promo");
            var promotionList = await _repository.RetrieveTopFiveNewestPromotions();
            _logger.LogInformation("email Repo after retrieve promo");
            /*aws sns */
            var sns = new AmazonSimpleNotificationServiceClient();
            var listTopicsRequest = new ListTopicsRequest();
            ListTopicsResponse listTopicsResponse;
            _logger.LogInformation("sns before retrieve topic");
            listTopicsResponse = await sns.ListTopicsAsync(listTopicsRequest);
            _logger.LogInformation("sns after retrieve topic");
            var selectedTopic = listTopicsResponse.Topics.FirstOrDefault();
            _logger.LogInformation("sns before retrieve subscriber");
            var subscriptionList = (await sns.ListSubscriptionsByTopicAsync(selectedTopic.TopicArn)).Subscriptions.Select(x => x.Endpoint).ToList();
            _logger.LogInformation("sns after retrieve subscriber");
            #region config
            var emailConfig = new EmailService.EmailConfiguration
            {
                SmtpServer = _configuration.GetValue<string>("EmailConfiguration:SmtpServer"),
                SmtpPort = Convert.ToInt32(_configuration.GetValue<string>("EmailConfiguration:SmtpPort")),
                SmtpUsername = _configuration.GetValue<string>("EmailConfiguration:SmtpUsername"),
                SmtpPassword = _configuration.GetValue<string>("EmailConfiguration:SmtpPassword")
            };



            var messageContent = new EmailService.EmailMessage().Content;
            int count = 1;
            messageContent = "Newest promotions happening right now in SG<div>";
            foreach (var promotion in promotionList)
            {
                messageContent += "<h2>#" + count + " </h2>" +
                    "<br /><b>Promotion Title:</b>" + promotion.Header +
                    "<br /><b>Promotion Description:</b>" + promotion.Description +
                    "<br /><b>Promotion Start Date:</b>" + promotion.StartDate.ToString("yyyy - MM - dd") +
                    "<br /><b>Promotion End Date:</b>" + promotion.EndDate.ToString("yyyy - MM - dd");
                count++;
            }
            messageContent += "</div>";

            var emailMessage = new EmailService.EmailMessage
            {
                FromAddresses = new List<EmailService.EmailAddress>(),
                ToAddresses = new List<EmailService.EmailAddress>(),
                Content = messageContent,
                Subject = "PromotionsSG You Should Not Miss"
            };


            var emailAddrFrom = new EmailService.EmailAddress
            {
                Address = "PromotionsSG@gmail.com",
                Name = "PromotionsSG"
            };

            var emailAddrToList = new List<EmailService.EmailAddress>();
            foreach (var subscriptionEmail in subscriptionList)
            {
                emailAddrToList.Add(new EmailService.EmailAddress { Address = subscriptionEmail });
            }

            emailMessage.FromAddresses = new List<EmailService.EmailAddress> { emailAddrFrom };
            emailMessage.ToAddresses = emailAddrToList;
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
            _logger.LogInformation("before email stmp send");
            using (var emailClient = new SmtpClient())
            {
                await emailClient.ConnectAsync(emailConfig.SmtpServer, emailConfig.SmtpPort);
                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
                await emailClient.AuthenticateAsync(emailConfig.SmtpUsername, emailConfig.SmtpPassword);
                await emailClient.SendAsync(message);
                await emailClient.DisconnectAsync(true);
            }
            _logger.LogInformation("after email stmp send");
            #endregion
            return true;
        }
        #endregion
    }
}
