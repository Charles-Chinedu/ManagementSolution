using ManagementSolution.Application.DTOs;
using System.ComponentModel.DataAnnotations;

namespace ManagementSolution.DTOs.AccountModels
{
    public class Register : AccountBase
    {
        [Required(ErrorMessage = "Full Name is required")]
        [MinLength(5)]
        [MaxLength(100)]
        public string? FullName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }
    }
}
