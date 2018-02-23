using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Linq;

namespace EKE.Data.Entities.Identity
{
    public class UserSeed
    {
        private BaseDbContext _context;

        public UserSeed(BaseDbContext context)
        {
            _context = context;
        }

        public async void SeedAdminUser()
        {
            var user = new ApplicationUser
            {
                UserName = "superadmin@eke.ma",
                NormalizedUserName = "superadmin@eke.ma",
                Email = "superadmin@eke.ma",
                NormalizedEmail = "superadmin@eke.ma",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var roleStore = new RoleStore<ApplicationRole>(_context);

            if (!_context.Roles.Any(r => r.Name == "superadmin"))
            {
                await roleStore.CreateAsync(new ApplicationRole { Name = "superadmin", NormalizedName = "superadmin" });
            }

            if (!_context.Roles.Any(r => r.Name == "gyopar"))
            {
                await roleStore.CreateAsync(new ApplicationRole { Name = "gyopar", NormalizedName = "gyopar" });
            }

            if (!_context.Roles.Any(r => r.Name == "muzeum"))
            {
                await roleStore.CreateAsync(new ApplicationRole { Name = "muzeum", NormalizedName = "muzeum" });
            }

            if (!_context.Roles.Any(r => r.Name == "elorendel"))
            {
                await roleStore.CreateAsync(new ApplicationRole { Name = "elorendel", NormalizedName = "elorendel" });
            }

            if (!_context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user, "cff41bNG");
                user.PasswordHash = hashed;
                var userStore = new UserStore<ApplicationUser>(_context);
                await userStore.CreateAsync(user);
                await userStore.AddToRoleAsync(user, "superadmin");
            }

            await _context.SaveChangesAsync();
        }
    }
}

