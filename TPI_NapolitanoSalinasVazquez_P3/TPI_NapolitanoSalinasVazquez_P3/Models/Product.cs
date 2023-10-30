using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPI_NapolitanoSalinasVazquez_P3.Models
{
    public class Product
    {
        
        public int productID { get; set; }

        
        public string productName { get; set; }

        
        public int productPrice { get; set; }

        
        public int productStock { get; set; }
    }
}
