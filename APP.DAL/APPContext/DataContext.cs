using APP.MODEL;
using Microsoft.EntityFrameworkCore;

namespace APP.DAL.APPContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<UserInfo> UserInfos { get; set; }

        public DbSet<UserLogin> UserLogins { get; set; }

        public DbSet<TargetUser> TargetUsers { get; set; }

    }
}