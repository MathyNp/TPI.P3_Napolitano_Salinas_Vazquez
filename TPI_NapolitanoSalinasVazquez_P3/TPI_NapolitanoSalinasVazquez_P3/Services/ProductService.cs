using Microsoft.EntityFrameworkCore;
using TPI_NapolitanoSalinasVazquez_P3.Data;
using TPI_NapolitanoSalinasVazquez_P3.Interfaces;
using TPI_NapolitanoSalinasVazquez_P3.Models;

namespace TPI_NapolitanoSalinasVazquez_P3.Services
{
    public class ProductService : IProductService
    {
        private readonly TPI_NapolitanoSalinasVazquez_P3Context _context;

        public ProductService(TPI_NapolitanoSalinasVazquez_P3Context context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Product;
        }

        public  Product GetById(int id)
        {
            return _context.Product.Find(id);
        }

        public void Add(Product product)
        {
            _context.Product.Add(product);
            _context.SaveChanges();
        }

        public void DeleteAll()
        {
            var allProducts = _context.Product.ToList();
            _context.Product.RemoveRange(allProducts);
            _context.SaveChanges();
        }

        public void ChangeState(int id, bool? newState)
        {
            
            var product = _context.Product.Find(id);

            if (product == null)
            {
                throw new ArgumentException($"No se encontró un producto con el ID {id}.");
            }

            product.productState = newState.Value;
            _context.SaveChanges();
        }
        // ...

        public void Update(int id, Product updatedProduct)
        {
            var product = _context.Product.Find(id);

            if (product == null)
            {
                throw new ArgumentException($"No se encontró un producto con el ID {id}.");
            }            

            if (updatedProduct.productName == "string")
            {
                product.productName = product.productName;
            } 
            else
            {
                product.productName = updatedProduct.productName;
            }

            if (updatedProduct.productPrice == 0)
            {
                product.productPrice = product.productPrice;
            }
            else
            {
                product.productPrice = updatedProduct.productPrice;
            }

            if (updatedProduct.productStock == 0)
            {
                product.productStock = product.productStock;
            }
            else
            {
                product.productStock = updatedProduct.productStock;
            }
            _context.SaveChanges();
        }

        public void ProductSell(int id) 
        {
            var product = _context.Product.Find(id);

            product.productStock = product.productStock - 1;
            
            
            if (product.productStock == 0)
            {
                product.productState = false;
            }

            _context.SaveChanges();
        }

    }
}
