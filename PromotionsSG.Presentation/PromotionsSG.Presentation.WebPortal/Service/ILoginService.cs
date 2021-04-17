using Common.DBTableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionsSG.Presentation.WebPortal.Service
{
    public interface ILoginService
    {
        #region Custom
        Task<User> LoginAsync(int userType, string userName, string password);
        Task<User> RegisterAsync(User user);
        #endregion


        #region CRUD
        Task<User> RetrieveAsync(int userId);
        Task<User> UpdateAsync(User user);
        #endregion
    }
}
