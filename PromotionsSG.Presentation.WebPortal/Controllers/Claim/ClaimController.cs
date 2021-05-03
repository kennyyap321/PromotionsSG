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
        private readonly INotificationService _notificationService;
        private IConfiguration _configuration;
        #endregion


        #region Dependency injection
        public ClaimController(ILogger<ClaimController> logger, IClaimService claimService, ICustomerProfileService customerProfileService, INotificationService notificationService, IConfiguration configuration)
        {
            _logger = logger;
            _claimService = claimService;
            _customerProfileService = customerProfileService;
            _notificationService = notificationService;
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
            string previousPage = Request.Headers["Referer"].ToString();
            if (previousPage.Contains("Claim"))
            {
                previousPage = "Claim";
            }
            else
            {
                previousPage = "Recommendation";
            }

            //Claim details
            ClaimWithPromotionAndShopInfo cwpasi = await _claimService.RetrieveClaimWithPromotionAndShopInfoByClaimIdAsync(claimId);

            //QRCode
            string txtQrCode = $"ClaimId_{claimId}_ShopProfileId_{cwpasi.PromotionDto.ShopProfileId}_PromotionId_{cwpasi.PromotionDto.PromotionId}";
            QRCodeGenerator qrCodeGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrCodeGenerator.CreateQrCode(txtQrCode, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            byte[] qrBytes = await BitmapToBytes(qrCodeImage);

            //Viewmodel
            ClaimViewModel claimViewModel = new ClaimViewModel { ClaimExtraInfo = cwpasi, QrBtyes = qrBytes, PreviousPage = previousPage };

            return View(claimViewModel);
        }
        #endregion


        #region Post
        [HttpGet]
        public async Task TestFixDLL()
        {
            //QRCode
            string txtQrCode = Guid.NewGuid().ToString();
            QRCodeGenerator qrCodeGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrCodeGenerator.CreateQrCode(txtQrCode, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
        }

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

                //Claim details
                int claimId = result.ClaimId;
                ClaimWithPromotionAndShopInfo cwpasi = await _claimService.RetrieveClaimWithPromotionAndShopInfoByClaimIdAsync(claimId);

                //QRCode
                string txtQrCode = $"ClaimId_{claimId}_ShopProfileId_{cwpasi.PromotionDto.ShopProfileId}_PromotionId_{cwpasi.PromotionDto.PromotionId}";
                QRCodeGenerator qrCodeGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrCodeGenerator.CreateQrCode(txtQrCode, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(20);
                byte[] qrBytes = await BitmapToBytes(qrCodeImage);
                string qrCodeString = Convert.ToBase64String(qrBytes);

                /*mailkit*/
                var emailAddr = new Common.DBTableModelsService.EmailService.EmailAddress
                {
                    Address = customerEmailAddr,
                    Name = userName
                };
                var emailMessage = new Common.DBTableModelsService.EmailService.EmailMessage
                {
                    FromAddresses = new List<Common.DBTableModelsService.EmailService.EmailAddress> { emailAddr },
                    ToAddresses = new List<Common.DBTableModelsService.EmailService.EmailAddress> { emailAddr },
                    Content = $@"

                        <div style=""margin - left:0; "">
                           <img src=""data:image/png;base64,{qrCodeString}"" height=""300"" width=""300"" />
                        </div>
                        <div>
                            <br /><b>Shop:</b>{cwpasi.ShopProfileDto.ShopName}
                            <br /><b>Promotion Title:</b>{cwpasi.PromotionDto.Header}
                            <br /><b>Promotion Description:</b>{cwpasi.PromotionDto.Description}
                            <br /><b>Promotion Start Date:</b>{cwpasi.PromotionDto.StartDate.ToString("yyyy-MM-dd")}
                            <br /><b>Promotion End Date:</b>{cwpasi.PromotionDto.EndDate.ToString("yyyy-MM-dd")}
                        </div>

                    ",
                    Subject = "PromotionsSG Claim Voucher"
                };
                var emailResult = await _notificationService.SendEmailAsync(emailMessage);
                return emailResult ? new JsonResult(result) : new JsonResult(null);
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
