using Microsoft.EntityFrameworkCore;

namespace Common.DBTableModels
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {
        }

        #region Login module
        public DbSet<User> Users { get; set; }
        #endregion

        #region CustomerProfile module
        public DbSet<CustomerProfiles> CustomerProfile { get; set; }
        #endregion

        #region ShopProfile module
        public DbSet<ShopProfile> ShopProfiles { get; set; }
        #endregion

        #region Promotion module
        public DbSet<Promotion> Promotions { get; set; }
        #endregion
    }
}
