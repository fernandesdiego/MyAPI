using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyAPI.Api.ViewModels
{
    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "The field {0} is required.")]
        [EmailAddress(ErrorMessage = "The field {0} must have a valid format")]
        public string Email { get; set; }
        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
