using TPI_NapolitanoSalinasVazquez_P3.Models;
using TPI_NapolitanoSalinasVazquez_P3.Models.Responses;


namespace TPI_NapolitanoSalinasVazquez_P3.Interfaces
{
    public interface IUserService
    {
        public BaseResponse Login(string mail, string userPassword);

        public int CreateUser(User user);

        public void UpdateUser(User user);

        public void DeleteUser(int userID);

        public User? GetUserByEmail(string userName);
    }
}
