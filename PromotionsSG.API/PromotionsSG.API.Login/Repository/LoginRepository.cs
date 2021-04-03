using Common.DBTableModels;
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

        #region UserLogin
        public async Task<User> LoginAsync(string userName, string password, int userType)
        {
            var result = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password && u.UserType == userType);

            return result;
        }

        public async Task<int> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            var result = await _context.SaveChangesAsync();

            var createdUserId = (await _context.Users.FirstAsync(u => u.UserName == user.UserName && u.UserType == user.UserType)).UserId;

            return createdUserId;
        }

        public async Task<int> UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            var result = await _context.SaveChangesAsync();

            var updatedUserId = user.UserId;

            return updatedUserId;
        }
        #endregion
    }
}
