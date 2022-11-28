using Microsoft.EntityFrameworkCore;
using Travel.Models.Account;
using Travel.Models.Permission;

namespace Travel.TravelDbContext
{
    public class TrDbContext :DbContext
    {
        public TrDbContext(DbContextOptions<TrDbContext> options) :base(options)
        {

        }


        #region User Tables 
        public DbSet<Permission> tblPermission { get; set; }
        public DbSet<Role> tblRole { get; set; }
        public DbSet<UserRole> tblUserRole { get; set; }
        public DbSet<User> tblUser { get; set; }
        public DbSet<RolePermission> tblRolePermission { get; set; }
       
        #endregion
    }
}
