using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPI_NapolitanoSalinasVazquez_P3.Models
{
    public class ShoppingCart
    {

        [Key] // Hace que la id sea la clave principal dentro de la BD
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // hace que la id se genere con cada user en la BD

        public int Id { get; set; }

        [Required]
        [ForeignKey("CartID")]
        public int CartId { get; set; }

        [Required]
        public string CartUser { get; set; }

        [Required]
        public List<SaleOrderLine> saleOrderLines { get; set; } = new List<SaleOrderLine>();

        [Required]
        public decimal TotalPrice 
        {
            get
            {
                decimal totalPriceCart = 0;

                foreach (var saleOrderLine in saleOrderLines)
                {
                    totalPriceCart += saleOrderLine.ProductPrice;
                }
                
                return totalPriceCart;
            }
        }



        
    }
}
