using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace restaurant.webui.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage ="Fill in the firstname field")]
        public string FirstName { get; set; }
        [Required(ErrorMessage ="Fill in the userName field")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string RePassword { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}