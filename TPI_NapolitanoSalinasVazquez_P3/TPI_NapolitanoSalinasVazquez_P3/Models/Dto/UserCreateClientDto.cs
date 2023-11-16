using System.ComponentModel.DataAnnotations;

namespace TPI_NapolitanoSalinasVazquez_P3.Models.Dto
{
    public class UserCreateClientDto
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? UserPassword { get; set; }

        [Required]
        [EmailAddress]
        public string? UserMail { get; set; }

        [Required]
        public string? UserRol { get; set; }

        [Required]
        public string address { get; set; } = string.Empty;

    }
}
