using MessagePack;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPI_NapolitanoSalinasVazquez_P3.Models
{
    public class SaleOrderLine
    {
        
        public int Id { get; set; }

        [ForeignKey("SaleOrderId")]
        public int SaleOrderLineId { get; set; }

        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }

        public decimal ProductPrice {get; set; }    

        public Product Product { get; set; }

    }
}
