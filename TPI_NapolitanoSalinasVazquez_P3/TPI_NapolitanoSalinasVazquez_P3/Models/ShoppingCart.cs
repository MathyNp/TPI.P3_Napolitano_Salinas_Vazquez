namespace TPI_NapolitanoSalinasVazquez_P3.Models
{
    public class ShoppingCart
    {
        private int CartId { get; set; }
        public int ProductAmount { get; set; }
        public int TotalPrice { get; set; }
        public DateOnly Date { get; set; }
        public string ShippingAddress { get; set; }


    }


}
