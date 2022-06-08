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
    public class UserRoleMap : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(r => new { r.UserId, r.RoleId });

            builder.HasData(
             new UserRole
             {
                 RoleId = 1,
                 UserId = 1
             },
             new UserRole
             {
                 RoleId = 2,
                 UserId = 2
             },
             new UserRole
             {
                 RoleId = 2,
                 UserId = 3
             },
             new UserRole
             {
                 RoleId = 2,
                 UserId = 4
             }
             );

            builder.ToTable("AspNetUserRoles");

        }
    }
}
