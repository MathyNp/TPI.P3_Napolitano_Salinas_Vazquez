using System.ComponentModel.DataAnnotations;

namespace TPI_NapolitanoSalinasVazquez_P3.Models.Dto
{
    public class UserUpdateClientDto
    {
        [Required]
        public string? UserName { get; set; } = string.Empty;

        [Required]
        public string? UserPassword { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string? UserMail { get; set; } = string.Empty;
        [Required]
        public string address {  get; set; } = string.Empty;

        [Required]

        public int paymentMethod { get; set; }
    }
}
