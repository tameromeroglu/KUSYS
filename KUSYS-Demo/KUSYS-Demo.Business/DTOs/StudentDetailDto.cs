using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.Business.DTOs
{
    public class StudentDetailDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }


        [DisplayName("Phone Number")]
        [Required(ErrorMessage = "*{0} cannot be empty.")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(10, ErrorMessage = "*{0} must not exceed {1} characters.")]
        [MinLength(10, ErrorMessage = "*{0} cannot be less than {1} characters.")]
        public string PhoneNumber { get; set; }


        [DisplayName("Adress")]
        [Required(ErrorMessage = "*{0} cannot be empty.")]
        [MaxLength(255, ErrorMessage = "*{0} must not exceed {1} characters.")]
        public string Adress { get; set; }
    }
}
