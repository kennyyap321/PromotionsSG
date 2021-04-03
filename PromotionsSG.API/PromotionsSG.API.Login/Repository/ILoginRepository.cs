using Common.DBTableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionsSG.API.Login.Repository
{
    public interface ILoginRepository
    {
        #region User
        Task<User> LoginAsync(string userName, string password, int userType);
        Task<int> CreateUserAsync(User user);
        Task<int> UpdateUserAsync(User user);
        #endregion
    }
}
