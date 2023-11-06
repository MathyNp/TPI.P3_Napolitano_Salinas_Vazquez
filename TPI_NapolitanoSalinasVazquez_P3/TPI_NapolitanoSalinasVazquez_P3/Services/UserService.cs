using SQLitePCL;
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

        // validacion de credenciales - login
        public BaseResponse Login(string mail, string userPassword)
        {
            BaseResponse response = new BaseResponse();
            User? userForLogin = _context.Users.SingleOrDefault(u => u.UserMail == mail);
            if (userForLogin != null)
            {
                if (userForLogin.UserPassword == userPassword)
                    {
                    response.IsSuccess = true;
                    response.Message = "loging Succesfull";
                    }
                    else
                    {
                    response.IsSuccess = false;
                    response.Message = "wrong password";
                }
                    }
            else 
                {
                response.IsSuccess = false;
                response.Message = "wrong email";
            }
            return response;
        }

        //buscar usuario por mail
        public User? GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(u => u.UserMail == email);
        }

        // crear usuario
        public int CreateUser(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
            return user.UserID;
                }

        // actualizar datos del usuario
        public void UpdateUser(User user)
        {
            _context.Update(user);
            _context.SaveChanges();
        }
        

        // borrar usuario
        public void DeleteUser(int userId)
        {
            User userToBeDeleted = _context.Users.FirstOrDefault(u => u.UserID == userId);
            userToBeDeleted.UserState = false;
            _context.Update(userToBeDeleted);
            _context.SaveChanges();
        }


    }

    
}
