using CommonDB = Common.DBTableModels;

namespace PromotionsSG.API.NotificationAPI.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly CommonDB.MyDBContext _context;

        public NotificationRepository(CommonDB.MyDBContext context)
        {
            _context = context;
        }

        #region Notification

        #endregion
    }
}
