using KUSYS_Demo.Entity.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserName).HasMaxLength(50);
            builder.Property(x => x.Email).HasMaxLength(100);
            
            builder.HasMany<UserRole>().WithOne().HasForeignKey(x => x.UserId).IsRequired();

            var adminUser = new User
            {
                Id = 1,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                SecurityStamp = Guid.NewGuid().ToString()

            };
            adminUser.PasswordHash = CreatePasswordHash(adminUser, "123");

            var testUser = new User
            {
                Id = 2,
                UserName = "tameromeroglu",
                NormalizedUserName = "TAMEROMEROGLU",
                
                SecurityStamp = Guid.NewGuid().ToString()

            };
            testUser.PasswordHash = CreatePasswordHash(testUser, "tameromeroglu");

            var testUser2 = new User
            {
                Id = 3,
                UserName = "didierdrogba",
                NormalizedUserName = "DIDIERDROGBA",

                SecurityStamp = Guid.NewGuid().ToString()

            };
            testUser2.PasswordHash = CreatePasswordHash(testUser2, "didierdrogba");

            var testUser3 = new User
            {
                Id = 4,
                UserName = "heuminson",
                NormalizedUserName = "HEUMINSON",

                SecurityStamp = Guid.NewGuid().ToString()

            };
            testUser3.PasswordHash = CreatePasswordHash(testUser3, "heuminson");

            builder.HasData(adminUser, testUser, testUser2, testUser3);

            builder.ToTable("AspNetUsers");
        }

        private string CreatePasswordHash(User user, string password)
        {
            var passwordHasher = new PasswordHasher<User>();
            return passwordHasher.HashPassword(user, password);
        }
    }
}
