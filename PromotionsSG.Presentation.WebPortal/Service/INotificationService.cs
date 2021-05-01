using System.Threading.Tasks;
using static Common.DBTableModelsService.EmailService;

namespace PromotionsSG.Presentation.WebPortal.Service
{
    public interface INotificationService
    {
        #region Custom
        Task<bool> SendEmailAsync(EmailMessage emailMessage);
        #endregion
    }
}
