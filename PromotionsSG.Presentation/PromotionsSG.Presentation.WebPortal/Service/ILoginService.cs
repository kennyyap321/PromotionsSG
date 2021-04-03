using Common.DBTableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionsSG.Presentation.WebPortal.Service
{
    public interface ILoginService
    {
        #region User
        Task<User> LoginAsync(string userName, string password, int userType);
        Task<int> RegisterUserAsync(User user);
        Task<int> UpdateUserAsync(User user);
        #endregion
    }
}
