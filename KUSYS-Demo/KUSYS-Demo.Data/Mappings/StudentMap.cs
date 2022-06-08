using KUSYS_Demo.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.Data.Mappings
{
    public class StudentMap : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.FirstName).IsRequired();
            builder.Property(c => c.FirstName).HasMaxLength(20);

            builder.Property(c => c.LastName).IsRequired();
            builder.Property(c => c.LastName).HasMaxLength(20);

            builder.Property(c => c.BirthDate).IsRequired();

            builder.Property(c => c.PhoneNumber).IsRequired();
            builder.Property(c => c.PhoneNumber).HasMaxLength(10);

            builder.Property(c => c.Adress).IsRequired();
            builder.Property(c => c.Adress).HasMaxLength(255);




            builder.ToTable("Students");

            builder.HasData(
                new Student
                {
                    Id = 1,
                    FirstName = "Tamer",
                    LastName = "ÖMEROĞLU",
                    BirthDate = new DateTime(1994, 10, 22),
                    PhoneNumber = "5308212363",
                    Adress = "Bursa"
                },
                new Student
                {
                    Id = 2,
                    FirstName = "Heu Min",
                    LastName = "SON",
                    BirthDate = new DateTime(1994, 10, 22),
                    PhoneNumber = "2222222222",
                    Adress = "Japonya"
                },
                new Student
                {
                    Id = 3,
                    FirstName = "Didier",
                    LastName = "DROGBA",
                    BirthDate = new DateTime(1994, 10, 22),
                    PhoneNumber = "3333333333",
                    Adress = "İstanbul"
                }
                );
        }
    }
}
