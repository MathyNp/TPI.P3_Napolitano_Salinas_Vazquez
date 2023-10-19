namespace TPI_NapolitanoSalinasVazquez_P3.Models
{
    public class ShoppingCart
    {
        private int cartId { get; set; }
        public int productAmount { get; set; }
        public int totalPrice { get; set; }
        public DateOnly DateOnly { get; set; }
        public string shippingAddress { get; set; }

        // public (bool, string) Buy() { }

        // public string? ListInfo() { }

        // public (bool, string) Shipping() { }


    }


}
