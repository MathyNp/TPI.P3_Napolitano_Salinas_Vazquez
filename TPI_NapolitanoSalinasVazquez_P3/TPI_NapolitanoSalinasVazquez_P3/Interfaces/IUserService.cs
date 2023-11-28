using TPI_NapolitanoSalinasVazquez_P3.Models;
using TPI_NapolitanoSalinasVazquez_P3.Models.Responses;


namespace TPI_NapolitanoSalinasVazquez_P3.Interfaces
{
    public interface IUserService
    {
        public BaseResponse Login(string mail, string userPassword);

        public List<User> GetAllUsers();

        public List<User> GetAdmins();

        public List<User> GetClients();

        public int CreateUser(User user);

        public void UpdateUser(User user);

        //public void DeleteUser(int userID);
        public void DeleteUser(int userID);

        public User? GetUserByEmail(string userName);

        public User? GetUserById(int UserID);

        public void PurchaseProduct(int productId, string UserId, int amount);

        List<ShoppingCart> GetCart();

        List<ShoppingCart> GetClientCart(int userId);

        public void ClearCart(int userId);

        public void FinishUserCart(int userId);

        void ChangeStateUser(int id, bool? newState);

        public List<User> GetUserStateFalse();

        public List<Product> GetOutStock();

        public decimal CalculateDiscountedPrice(int userId, int productPrice);
    }
}
