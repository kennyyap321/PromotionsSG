using Common.DBTableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionsSG.API.Login.RepositoryInterface
{
    public interface ILoginRepository
    {
        Task<int> CreateUser(UserLogin userLogin);
        Task<int> UpdateUser(UserLogin userLogin);
        Task<UserLogin> Login(string userLoginId, string password);
    }
}
