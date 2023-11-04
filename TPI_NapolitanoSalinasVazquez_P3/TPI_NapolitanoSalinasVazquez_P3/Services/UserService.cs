using SQLitePCL;
using TPI_NapolitanoSalinasVazquez_P3.Interfaces;


namespace TPI_NapolitanoSalinasVazquez_P3.Services
{
    public interface UserService : IUserService
    {
        public BaseResponse ValidarUsuario(string userMail, string UserPassword)
        {
            BaseResponse response = new BaseResponse();
            User? userLogin = _context.User.SingleOrDefault(u => u.userMail == email);


            if (userLogin != null)
            {
                    if (userLogin.userPassword == password)
                    {
                        response.Result = true;
                        response.Message = "Login ok";
                    }
                    else
                    {
                        response.Result = false;
                        response.Message = "Password error";
                    }
            else 
                {
                    response.Result = false;
                    response.Message = "Mail error";
                }
            }
            return response;

        }

    }
}
