using ManagementSolution.Application.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSolution.Application.Models.Identity
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
