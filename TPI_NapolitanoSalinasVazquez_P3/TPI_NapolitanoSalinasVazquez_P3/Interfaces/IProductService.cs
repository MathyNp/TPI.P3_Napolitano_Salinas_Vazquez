using TPI_NapolitanoSalinasVazquez_P3.Models;
using TPI_NapolitanoSalinasVazquez_P3.Models.Responses;

namespace TPI_NapolitanoSalinasVazquez_P3.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();

        void Add(Product product);


    }
}
