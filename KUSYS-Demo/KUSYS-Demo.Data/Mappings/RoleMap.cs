using KUSYS_Demo.Entity.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.Data.Mappings
{
    public class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);

            builder.HasMany<UserRole>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();

            builder.HasData(
               new Role
               {
                   Id = 1,
                   Name = "Admin",
                   NormalizedName = "ADMIN",
                   ConcurrencyStamp = Guid.NewGuid().ToString()
               },
               new Role
               {
                   Id = 2,
                   Name = "User",
                   NormalizedName = "USER",
                   ConcurrencyStamp = Guid.NewGuid().ToString()
               });


            builder.ToTable("AspNetRoles");
        }
    }
}
