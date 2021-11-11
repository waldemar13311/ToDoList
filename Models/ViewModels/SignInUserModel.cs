using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Models.ViewModels
{
    public class SignInUserModel
    {
        [Required(ErrorMessage = "Login is not specified")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password is not specified")]
        [UIHint("Password")]
        public string Password { get; set; }
    }
}
