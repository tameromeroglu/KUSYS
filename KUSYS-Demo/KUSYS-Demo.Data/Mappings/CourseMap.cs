using KUSYS_Demo.Entity;
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
    public class CourseMap : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CourseId).IsRequired();
            builder.Property(c => c.CourseId).HasMaxLength(20);

            builder.Property(c => c.CourseName).IsRequired();
            builder.Property(c => c.CourseName).HasMaxLength(70);

            builder.ToTable("Courses");

            builder.HasData(

                new Course
                {
                    Id = 1,
                    CourseId = "CSI101",
                    CourseName = "Introduction to Computer Science"
                },
                new Course
                {
                    Id = 2,
                    CourseId = "CSI102",
                    CourseName = "Algorithms"
                },
                new Course
                {
                    Id = 3,
                    CourseId = "MAT101",
                    CourseName = "Calculus"
                },
                new Course
                {
                    Id = 4,
                    CourseId = "PHY101",
                    CourseName = "Physics"
                }
                );

        }
    }
}
