using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Models
{
    public class CreateUserModel
    {

        [Required(ErrorMessage = "The field Login is required")]
        [MinLength(4, ErrorMessage = "Minimum length is 4 characters")]
        [MaxLength(24, ErrorMessage = "Maximum length is 24 characters")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9-_\.]{3,25}$", ErrorMessage = "Unsuitable login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "The field Email is required")]
        [RegularExpression(@"^[-\w.]+@([A-z0-9][-A-z0-9]+\.)+[A-z]{2,4}$", ErrorMessage = "Please enter a valid email address")]
        //[UIHint("EmailAddress")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Fill the field password")]
        [UIHint("Password")]
        [MinLength(6, ErrorMessage = "Minimum length is 6 characters")]
        [MaxLength(32, ErrorMessage = "Maximum length is 32 characters")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Confirm your password")]
        [UIHint("Password")]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        public string ConfirmedPassword { get; set; }
    }
}
