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

        public void Add(Product product)
        {
            _context.Product.Add(product);
            _context.SaveChanges();
        }
    }
}
