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
    }
}
