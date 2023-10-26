using TPI_NapolitanoSalinasVazquez_P3.Interfaces;
using TPI_NapolitanoSalinasVazquez_P3.Models;

namespace TPI_NapolitanoSalinasVazquez_P3.Services
{
    public class ShoppingCartService
    {
        private List<ShoppingCart> cart = new List<ShoppingCart>();
        private List<Product> inventario = new List<Product>();

        public ShoppingCartService()
        {
            
            inventario.Add(new Product { productId = 1, productName = "Producto A", productPrice = 10, productStock = 100 });
            inventario.Add(new Product { productId = 2, productName = "Producto B", productPrice = 20, productStock = 50 });
            inventario.Add(new Product { productId = 3, productName = "Producto C", productPrice = 15, productStock = 75 });
        }
        public Buy(ShoppingCart item)
        {
            var productoEnInventario = inventario.FirstOrDefault(p => p.productId == item.Product.productId);
            if (productoEnInventario != null)
            {
                // Verifica si la cantidad solicitada está disponible
                if (productoEnInventario.productStock >= item.productAmount)
                {
                    // Agrega el producto al carrito
                    cart.Add(item);
                    // Reduce la cantidad disponible en el inventario
                    productoEnInventario.productStock -= item.productAmount;
                }
                else
                {
                    throw new InvalidOperationException("No hay suficiente stock disponible para este producto.");
                }
            }
            else
            {
                throw new InvalidOperationException("El producto no se encuentra en el inventario.");
            }
            
        }
        // public (bool, string) Buy() { }

        // public string? ListInfo() { }

        // public (bool, string) Shipping() { }
    }
}
