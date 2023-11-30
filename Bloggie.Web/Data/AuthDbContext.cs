using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var superAdminRoleId = "06142612-f04b-4d2e-929e-b383307c6ef9";
            var adminRoleId = "0269f4ee-0507-4e66-b71e-318f32fce67a";
            var userRoleId = "b6f55a04-f64f-43d3-9525-811889e54814";
            //seed Roles (User,Admin, Super Admin
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                  Name = "SuperAdmin",
                  NormalizedName = "SuperAdmin",
                  Id = superAdminRoleId,
                  ConcurrencyStamp = superAdminRoleId
                },
                new IdentityRole()
                {
                  Name = "Admin",
                  NormalizedName = "Admin",
                  Id = adminRoleId,
                  ConcurrencyStamp = adminRoleId
                },
                new IdentityRole()
                {
                  Name = "User",
                  NormalizedName = "User",
                  Id = userRoleId,
                  ConcurrencyStamp = userRoleId
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);

            //Seed Super Admin User
            var superAdminId = "bb8659f3-9b60-4fe1-81e8-bec77c28eb2f";
            var superAdminUser = new IdentityUser()
            {
                Id = superAdminId,
                UserName = "superadmin@softlive.com",
                Email = "superadmin@softlive.com",
                NormalizedEmail = "superadmin@softlive.com".ToUpper(),
                NormalizedUserName= "superadmin@softlive.com".ToUpper()


            };
            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(superAdminUser, "superadmin123");

            builder.Entity<IdentityUser>().HasData(superAdminUser);
            // add All Roles to Super Admin User
            var superAdminRoles = new List<IdentityUserRole<string>>()
            {
                new IdentityUserRole<string>
                {
                    RoleId=superAdminRoleId,
                    UserId= superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId=adminRoleId,
                    UserId= superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId=userRoleId,
                    UserId= superAdminId
                }

            };
            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);

        }
    }
}
