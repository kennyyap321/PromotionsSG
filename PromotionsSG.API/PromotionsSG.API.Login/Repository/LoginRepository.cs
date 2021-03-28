using Common.DBTableModels;
using PromotionsSG.API.Login.RepositoryInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionsSG.API.Login.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly MyDBContext _context;

        public LoginRepository(MyDBContext context)
        {
            _context = context;
        }
        public async Task<int> CreateUser(UserLogin userLogin)
        {
            _context.Add(userLogin);
            await _context.SaveChangesAsync();
            return 1;
        }

        public async Task<int> UpdateUser(UserLogin userLogin)
        {
            _context.Update(userLogin);
            await _context.SaveChangesAsync();
            return 1;
        }

        public async Task<UserLogin> Login(string userLoginId, string password)
        {
            //use .where for multiple records
            var userData = await _context.UserLogin.FirstOrDefaultAsync(x => x.UserLoginId == userLoginId && x.Password == password);
            return userData;
        }
    }
}
