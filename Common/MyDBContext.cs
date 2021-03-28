using System;
using System.Collections.Generic;
using System.Text;
using Common.DBTableModels;
using Microsoft.EntityFrameworkCore;

namespace Common.DBTableModels
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {
        }

        public DbSet<UserLogin> UserLogin { get; set; }
        public DbSet<CustomerProfile> CustomerProfile { get; set; }
    }
}
