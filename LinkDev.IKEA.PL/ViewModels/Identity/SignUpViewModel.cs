using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Identity
{
    public class SignUpViewModel
    {
        [Display(Name ="First Name")]
        [Required(ErrorMessage = "First Name is required.")]
        public required string  FirstName { get; set; }

        [Display(Name = "Last Name")]

        [Required(ErrorMessage = "Last Name is required.")]
        public  required string LastName { get; set; }

        [ Required(ErrorMessage = "User Name is required.")]
        public required string UserName { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]

        public required string ConfirmPassword { get; set; } 
        public bool IsAgree { get; set; } = false;
    }
}
