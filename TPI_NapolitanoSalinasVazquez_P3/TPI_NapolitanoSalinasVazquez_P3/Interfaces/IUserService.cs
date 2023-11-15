using TPI_NapolitanoSalinasVazquez_P3.Models;
using TPI_NapolitanoSalinasVazquez_P3.Models.Responses;


namespace TPI_NapolitanoSalinasVazquez_P3.Interfaces
{
    public interface IUserService
    {
        public BaseResponse Login(string mail, string userPassword);

        List<User> GetAllUsers();

        public int CreateUser(User user);

        public void UpdateUser(User user);

        public void DeleteUser(int userID);

        public User? GetUserByEmail(string userName);

        public void PurchaseProduct(int productId, int UserId);

        public void FinishUserCart(int userId);

        List<ShoppingCart> GetCart();
    }
}
