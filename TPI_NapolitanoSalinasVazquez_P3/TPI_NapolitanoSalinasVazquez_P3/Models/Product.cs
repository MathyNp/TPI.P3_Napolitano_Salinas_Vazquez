using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPI_NapolitanoSalinasVazquez_P3.Models
{
    public class Product
    {
        [Key] // Hace que la id sea la clave principal dentro de la BD
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // hace que la id se genere con cada product en la BD
        public int productID { get; set; }

        public string? productName { get; set; }

        public int productPrice { get; set; }

        public int productStock { get; set; }

        public bool productState { get; set; } = false;

    }
}
