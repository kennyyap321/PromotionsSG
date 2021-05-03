using Common.DBTableModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PromotionsSG.API.NotificationAPI.Repository
{
    public interface INotificationRepository
    {
        #region Notification
        Task<List<Promotion>> RetrieveTopFiveNewestPromotions();
        #endregion
    }
}
