using Common.DBTableModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PromotionsSG.Presentation.WebPortal.Models;
using PromotionsSG.Presentation.WebPortal.Service;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Microsoft.Extensions.Configuration;

namespace PromotionsSG.Presentation.WebPortal.Controllers
{
    public class ClaimController : Controller
    {
        #region Fields
        private readonly ILogger<ClaimController> _logger;
        private readonly IClaimService _claimService;
        private readonly ICustomerProfileService _customerProfileService;
        private IConfiguration _configuration;
        #endregion


        #region Dependency injection
        public ClaimController(ILogger<ClaimController> logger, IClaimService claimService, ICustomerProfileService customerProfileService, IConfiguration configuration)
        {
            _logger = logger;
            _claimService = claimService;
            _customerProfileService = customerProfileService;
            _configuration = configuration;
        }
        #endregion


        #region Get
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string userName = HttpContext.Session.GetString("username");
            int customerProfileId = (await _customerProfileService.CustomerProfile(userName)).CustomerProfileId;

            IEnumerable<ClaimWithPromotionAndShopInfo> cwpasis = await _claimService.RetrieveClaimsWithPromotionAndShopInfoByCustomerProfileIdAsync(customerProfileId);

            ClaimViewModel claimViewModel = new ClaimViewModel { ClaimExtraInfoListDto = cwpasis };

            return View(claimViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int claimId)
        {
            //Claim detaails
            ClaimWithPromotionAndShopInfo cwpasi = await _claimService.RetrieveClaimWithPromotionAndShopInfoByClaimIdAsync(claimId);

            //QRCode
            string txtQrCode = $"ClaimId_{claimId}_ShopProfileId_{cwpasi.PromotionDto.ShopProfileId}_PromotionId_{cwpasi.PromotionDto.PromotionId}";
            QRCodeGenerator qrCodeGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrCodeGenerator.CreateQrCode(txtQrCode, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            byte[] qrBytes = await BitmapToBytes(qrCodeImage);

            //Viewmodel
            ClaimViewModel claimViewModel = new ClaimViewModel { ClaimExtraInfo = cwpasi, QrBtyes = qrBytes };

            return View(claimViewModel);
        }
        #endregion


        #region Post
        [HttpPost]
        public async Task<IActionResult> Claim(int promotionId)
        {
            string userName = HttpContext.Session.GetString("username");
            var customerProfile = (await _customerProfileService.CustomerProfile(userName));
            var customerProfileId = customerProfile.CustomerProfileId;
            var customerEmailAddr = customerProfile.CustomerEmail;

            Claim claim = new Claim
            {
                PromotionId = promotionId,
                CustomerProfileId = customerProfileId
            };

            var result = await _claimService.ClaimAsync(claim);
            if (result != null)
            {
                //send email

                /*aws sns */
                //var sns = new AmazonSimpleNotificationServiceClient();
                //var listTopicsRequest = new ListTopicsRequest();
                //ListTopicsResponse listTopicsResponse;

                //listTopicsResponse = await sns.ListTopicsAsync(listTopicsRequest);
                //var selectedTopic = listTopicsResponse.Topics.FirstOrDefault();
                //await sns.PublishAsync(new PublishRequest
                //{
                //    Subject = "PromotionsSG Claim Voucher",
                //    Message = "Your Code is : v" + "p" + promotionId + "c" + customerProfileId,
                //    TopicArn = selectedTopic.TopicArn
                //});

                /*mailkit*/
                var emailConfig = new EmailService.EmailConfiguration
                {
                    SmtpServer = _configuration.GetValue<string>("EmailConfiguration:SmtpServer"),
                    SmtpPort = Convert.ToInt32(_configuration.GetValue<string>("EmailConfiguration:SmtpPort")),
                    SmtpUsername = _configuration.GetValue<string>("EmailConfiguration:SmtpUsername"),
                    SmtpPassword = _configuration.GetValue<string>("EmailConfiguration:SmtpPassword")
                };
                EmailService emailService = new EmailService(emailConfig);
                var message = $"Your Code is : vp{promotionId}c{customerProfileId}";
                var emailAddr = new EmailService.EmailAddress
                {
                    Address = customerEmailAddr,
                    Name = userName
                };
                emailService.Send(new EmailService.EmailMessage
                {
                    FromAddresses = new List<EmailService.EmailAddress> { emailAddr },
                    ToAddresses = new List<EmailService.EmailAddress> { emailAddr },
                    Content = "Your Code is : v" + "p" + promotionId + "c" + customerProfileId,
                    Subject = "PromotionsSG Claim Voucher"
                });

                return new JsonResult(result);
            }

            return new JsonResult(null);
        }

        [HttpPost]
        public async Task<IActionResult> Use(int claimId)
        {
            Claim claim = await _claimService.RetrieveAsync(claimId);

            claim.Consumed = true;
            claim.ConsumeDate = DateTime.Now;

            await _claimService.UpdateAsync(claim);

            return RedirectToAction("Details", "Claim", new { claimId });
        }
        #endregion


        #region Non action
        [NonAction]
        private async static Task<byte[]> BitmapToBytes(Bitmap image)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, ImageFormat.Png);
                return stream.ToArray();
            }
        }
        #endregion
    }
}
