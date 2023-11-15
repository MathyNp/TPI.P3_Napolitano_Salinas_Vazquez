using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPI_NapolitanoSalinasVazquez_P3.Models
{
    public class Client : User
    {


        public string address { get; set; }

        public string paymentMethod { get; set; }

        
    }
}
