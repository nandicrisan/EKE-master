using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EKE.Data.Entities.Identity.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email cím megadása kötelező")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Jelszó cím megadása kötelező")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Megjegyez?")]
        public bool RememberMe { get; set; }
    }
}
