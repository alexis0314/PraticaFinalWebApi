using System.ComponentModel.DataAnnotations;

namespace PracticaFinalWebApi.DTOs
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Correo { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}