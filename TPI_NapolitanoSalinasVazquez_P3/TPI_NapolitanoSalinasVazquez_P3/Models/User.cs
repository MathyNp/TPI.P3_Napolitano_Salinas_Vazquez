//using TPI_NapolitanoSalinasVazquez_P3.Interfaces;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPI_NapolitanoSalinasVazquez_P3.Models
{
    public abstract class User 
    {

        [Key] // Hace que la id sea la clave principal dentro de la BD
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // hace que la id se genere con cada user en la BD
        public int UserID { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string UserPassword { get; set; }


        [Required]
        [EmailAddress]
        public string UserMail { get; set; }

        [Required]
        [EnumDataType(typeof(UserRoleEnum))]
        public UserRoleEnum UserRol { get; set; }

        public bool UserState { get; set; } = true;

        public List<History> History { get; set; }



    }
   
}
