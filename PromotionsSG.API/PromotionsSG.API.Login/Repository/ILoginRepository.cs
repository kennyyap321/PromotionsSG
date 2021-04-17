using Common.DBTableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionsSG.API.Login.Repository
{
    public interface ILoginRepository
    {
        #region CRUD
        Task<User> RetrieveAsync(int userId);
        Task<User> InsertAsync(User user);
        Task<User> UpdateAsync(User user);
        #endregion

        #region Custom
        Task<User> LoginAsync(User user);
        #endregion
    }
}
