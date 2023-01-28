using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace restaurant.webui.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Username field is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Password field is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string? ReturnUrl { get; set; }
    }
}