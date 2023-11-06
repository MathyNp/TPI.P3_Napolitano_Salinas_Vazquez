using System.ComponentModel.DataAnnotations;

namespace TPI_NapolitanoSalinasVazquez_P3.Models.Dto
{
    public class UserCreateDto
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





    }
}
