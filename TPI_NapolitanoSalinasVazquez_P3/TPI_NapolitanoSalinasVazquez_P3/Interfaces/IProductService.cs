using TPI_NapolitanoSalinasVazquez_P3.Models;
using TPI_NapolitanoSalinasVazquez_P3.Models.Responses;

namespace TPI_NapolitanoSalinasVazquez_P3.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();

        void Add(Product product);

        void DeleteAll();

        Product GetById(int id);

        void ChangeState(int id, bool? newState);

        void Update(int id, Product product);

        void ProductSell(int id, int amount);
    }
}
