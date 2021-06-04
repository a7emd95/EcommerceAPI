using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Name is Required"), MinLength(3, ErrorMessage = "Name Must be 3 Charachter At Least")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email Is Required"), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Is Required"), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
