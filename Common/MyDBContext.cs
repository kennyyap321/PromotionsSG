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

        #region Claim module
        public DbSet<Claim> Claims { get; set; }
        #endregion

        #region Feedback module
        public DbSet<Feedbacks> Feedback { get; set; }
        #endregion

        #region Recommendation module
        public DbSet<Recommendations> Recommendation { get; set; }
        public DbSet<CodeTable> CodeTable { get; set; }
        #endregion
    }
}
