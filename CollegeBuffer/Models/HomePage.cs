using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI.WebControls;

namespace CollegeBuffer.Models
{
    public class HomePage
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(20, MinimumLength = 4)]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(20, MinimumLength = 4)]
        [Display(Name = "User name")]
        public string NewUserName { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Retype password")]
        public string PasswordAgain { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email address")]
        public string EmailAddress { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("First name")]
        public string FirstName { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Last name")]
        public string LastName { get; set; }
    }
}