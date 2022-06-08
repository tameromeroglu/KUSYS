using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.Business.DTOs
{
    public class LoginDto
    {
        [DisplayName("Username")]
        [Required(ErrorMessage = "*{0} cannot be empty.")]
        public string Username { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "*{0} cannot be empty.")]
        public string Password { get; set; }
    }
}
