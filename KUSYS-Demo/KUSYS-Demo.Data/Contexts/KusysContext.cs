using KUSYS_Demo.Data.Mappings;
using KUSYS_Demo.Entity;
using KUSYS_Demo.Entity.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.Data.Contexts
{
    public class KusysContext: IdentityDbContext<User, Role,int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public KusysContext(DbContextOptions<KusysContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserMap());
            builder.ApplyConfiguration(new RoleMap());
            builder.ApplyConfiguration(new UserRoleMap());
            builder.ApplyConfiguration(new CourseMap());
            builder.ApplyConfiguration(new StudentMap());
        }

    }
}
