namespace Neighbours.Web.Models.Account
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Neighbours.Common.GlobalConstants;
    using Neighbours.Common.ValidationAttributes;
    using Neighbours.Data.Models;

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Birth Date")]
        [BirthDate(GlobalValidationConstants.MinBirthDate, GlobalValidationConstants.MinAgeToRegister, ErrorMessage = "{0} You are either too old or too young.")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "Profile Image")]
        public ProfileImage ProfileImage { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public Gender Gender { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}