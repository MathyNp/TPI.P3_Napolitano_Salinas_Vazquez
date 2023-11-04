//using TPI_NapolitanoSalinasVazquez_P3.Interfaces;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TPI_NapolitanoSalinasVazquez_P3.Models
{
    public abstract class User 
    {
        [Key] // Hace que la id sea la clave principal dentro de la BD
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // hace que la id se genere con cada user en la BD
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserMail { get; set; }
        public string UserRol { get; set; }

    }
}
