

namespace TPI_NapolitanoSalinasVazquez_P3.Interfaces
{
    public interface IUserService
    {
        public BaseResponse ValidarUsuario(string username, string userPassword);
        public User? GetUserEmail(string username);
    }
}
