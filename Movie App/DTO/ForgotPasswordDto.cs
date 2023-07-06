using System;
using System.ComponentModel.DataAnnotations;

namespace Movie_App.DTO
{
    public class ForgotPasswordDto
    {
        //[Required]
        //public string email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string confirmPassword { get; set; }
    }
}

