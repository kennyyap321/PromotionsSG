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
        #region Fields
        private readonly MyDBContext _context;
        #endregion


        #region Dependency injection
        public LoginRepository(MyDBContext context)
        {
            _context = context;
        }
        #endregion


        #region CRUD
        public async Task<User> RetrieveAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<User> InsertAsync(User user)
        {
            if (_context.Users.FirstOrDefault(u => u.UserName == user.UserName) == null)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return user;
            }

            return null;
        }

        public async Task<User> UpdateAsync(User userChanged)
        {
            var user = _context.Users.Attach(userChanged);
            user.State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return userChanged;
        }
        #endregion


        #region Custom
        public async Task<User> LoginAsync(User user)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserType == user.UserType && u.UserName == user.UserName && u.Password == user.Password);
        }
        #endregion
    }
}
