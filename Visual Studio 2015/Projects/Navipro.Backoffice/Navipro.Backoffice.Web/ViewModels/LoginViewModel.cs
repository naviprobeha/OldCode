using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Navipro.Backoffice.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Användar-id")]
        public string email { get; set; }

       
        [DataType(DataType.Password)]
        [Display(Name = "Lösenord")]
        public string password { get; set; }

        [Display(Name = "Kom ihåg mig?")]
        public bool rememberMe { get; set; }

        public string errorMessage { get; set; }
    }

}