using KUSYS_Demo.Business.DTOs;
using KUSYS_Demo.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.Business.Query
{
    public class StudentQuery
    {
        public IQueryable<StudentDto> SelectStudentList(IQueryable<Student> query)
        {
            var select = query.Select(x => new StudentDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                BirthDate = x.BirthDate.ToShortDateString(),

            });

            return select;
        }


        public IQueryable<StudentDetailDto> SelectStudentDetail(IQueryable<Student> query)
        {
            var select = query.Select(x => new StudentDetailDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                BirthDate = x.BirthDate,
                PhoneNumber = x.PhoneNumber,
                Adress = x.Adress

            });

            return select;
        }

        public IQueryable<StudentsCoursesDto> SelectStudentCourse(IQueryable<Student> query)
        {
            var select = query
                .Include(y => y.Course)
                .Select(x => new StudentsCoursesDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                CourseId = x.Course.CourseId,
                CourseName = x.Course.CourseName

            });

            return select;
        }


        public IQueryable<CourseDto> SelectCourse(IQueryable<Course> query)
        {
            var select = query.Select(x => new CourseDto
            {
                CourseId = x.CourseId,
                CourseName = x.CourseName
            });

            return select;
        }

    }
}
