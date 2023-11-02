using TPI_NapolitanoSalinasVazquez_P3.Interfaces;
using TPI_NapolitanoSalinasVazquez_P3.Models;
using TPI_NapolitanoSalinasVazquez_P3.Models.Responses;
using System.Linq;
using TPI_NapolitanoSalinasVazquez_P3.Data;

namespace TPI_NapolitanoSalinasVazquez_P3.Services
{
    public class UserService : IUserService
    {
        private readonly TPI_NapolitanoSalinasVazquez_P3Context _context;

        public UserService(TPI_NapolitanoSalinasVazquez_P3Context context)
        {
            _context = context;
        }

        public BaseResponse Login(string mail, string userPassword)
        {
            BaseResponse response = new BaseResponse();
            User user = _context.Users.SingleOrDefault(u => u.UserMail == mail);

            if (user != null)
            {
                if (user.UserPassword != userPassword)
                {
                    response.IsSuccess = false;
                    response.Message = "Error al iniciar sesion";
                }
                else
                {
                    response.IsSuccess = true;
                    response.Message = "Inicio de sesion exitoso";
                }
            }
            else 
            {
                response.IsSuccess = false;
                response.Message = "No existe el usuario";
            }

            return response;
        }

        public BaseResponse Register(string mail, string password) 
        {
            BaseResponse response = new BaseResponse();

            if (_context.Set<User>().Any(u => u.UserMail == mail))
            {
                response.IsSuccess = false;
                response.Message = "El usuario ya existe";
            }
            else
            {
                
                User newuser = new User
                {
                    UserMail = mail,
                    UserPassword = password
                };

                _context.Users.Add(newuser);
                _context.SaveChanges();

                response.IsSuccess = true;
                response.Message = "Usuario creado con exito";
            }

            return response;

        }





    }

    
}
