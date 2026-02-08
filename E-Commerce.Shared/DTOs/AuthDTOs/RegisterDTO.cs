using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.DTOs.AuthDTOs
{
    public class RegisterDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string DisplayName { get; set; } = string.Empty;
        //[Required]
        public string? UserName { get; set; } = string.Empty;
        //[Phone]
        public string? PhoneNumber { get; set; } = string.Empty;

    }
}
