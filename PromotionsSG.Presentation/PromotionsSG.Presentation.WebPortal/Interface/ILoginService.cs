using Common.DBTableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionsSG.Presentation.WebPortal
{
    public interface ILoginService
    {
        Task<UserLogin> Login(string userLoginId, string password);
    }
}
