using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.Business.DTOs
{
    public class StudentsCoursesDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required(ErrorMessage = "*{0} cannot be empty.")]
        public string CourseId { get; set; }

        public string CourseName { get; set; }

    }
}
