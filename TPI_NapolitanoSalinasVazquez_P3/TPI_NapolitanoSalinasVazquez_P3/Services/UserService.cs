using SQLitePCL;
using TPI_NapolitanoSalinasVazquez_P3.Interfaces;
using TPI_NapolitanoSalinasVazquez_P3.Models;
using TPI_NapolitanoSalinasVazquez_P3.Models.Responses;
using System.Linq;
using TPI_NapolitanoSalinasVazquez_P3.Data;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace TPI_NapolitanoSalinasVazquez_P3.Services
{
    public class UserService : IUserService
    {
        private readonly TPI_NapolitanoSalinasVazquez_P3Context _context;

        public UserService(TPI_NapolitanoSalinasVazquez_P3Context context)
        {
            _context = context;
        }



        // validacion de credenciales - login -------------------------------------------------------------------------
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

        // Lista all users --------------------------------------------------------------------------------------------
        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        // Lista clientes --------------------------------------------------------------------------------------------
        public List<User> GetClients()
        {
            return _context.Users.Where(a => a.UserRol == UserRoleEnum.Client).ToList();
        }

        // Lista admins --------------------------------------------------------------------------------------------
        public List<User> GetAdmins()
        {
            return _context.Users.Where(a => a.UserRol == UserRoleEnum.Admin).ToList();
        }

        //Buscar usuario por mail -----------------------------------------------------------------------------------
        public User? GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(u => u.UserMail == email);
        }

        //Buscar usuario por id -------------------------------------------------------------------------------------
        public User? GetUserById(int userID)
        {
            return _context.Users.SingleOrDefault(u => u.UserID == userID);
        }

        // Crear usuario --------------------------------------------------------------------------------------------
        public int CreateUser(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
            return user.UserID;
        }

        // Actualizar datos del usuario -----------------------------------------------------------------------------
        public void UpdateUser(User user)
        {
            _context.Update(user);
            _context.SaveChanges();
        }


        // Borrar usuario ------------------------------------------------------------------------------------------
        public void DeleteUser(int userID)
        {
            var userToBeDeleted = _context.Users.FirstOrDefault(u => u.UserID == userID);
            _context.Users.Remove(userToBeDeleted);
            _context.SaveChangesAsync();
            /*userToBeDeleted.UserState = false;
            _context.Update(userToBeDeleted);
            _context.SaveChanges();*/
        }

        // Agregar producto al carrito del cliente -------------------------------------------------------------------
        public void PurchaseProduct(int productId, string UserId)
        {
            int userIdInt = int.Parse(UserId);
            var user = _context.Users.FirstOrDefault(u => u.UserID == userIdInt);
            var cartItem = new ShoppingCart { UserId = userIdInt, productId = productId };

            _context.ShoppingCart.Add(cartItem);
            _context.SaveChanges();
        }

        // Comprar Carrito del cliente ---------------------------------------------------------------------------------
        public void FinishUserCart(int userId)
        {
            var cartItems = _context.ShoppingCart
                .Where(cartItem => cartItem.UserId == userId)
                .ToList();

            decimal totalAmount = 0;
            List<int> purchasedProductIds = new List<int>();


            foreach (var cartItem in cartItems)
            {
                var product = _context.Product.Find(cartItem.productId);

                if (product == null)
                {
                    throw new InvalidOperationException($"El producto id:{product} no existe.");
                }
                else
                {
                    if (!product.productState)
                    {
                        throw new InvalidOperationException($"El producto id:{product} no esta disponible para la venta");
                    }

                    if (product.productStock > 0)
                    {
                        product.productStock--;
                        totalAmount += product.productPrice;
                        purchasedProductIds.Add(product.productID);
                    }

                    else
                    {
                        throw new InvalidOperationException($"No se posee suficiente stock del producto id:{product}");
                    }

                    _context.ShoppingCart.Remove(cartItem);
                }

                var history = new History
                {
                    UserId = userId,
                    Date = DateTime.UtcNow,
                    ProductIds = JsonSerializer.Serialize(purchasedProductIds),
                    Amount = totalAmount

                };

                _context.Histories.Add(history);

                _context.SaveChanges();
            }


        }

        // Mostrar todos los carritos ------------------------------------------------------------------------------
        public List<ShoppingCart> GetCart()
        {
            return _context.ShoppingCart.ToList();

        }

        // Mostrar carrito del cliente ---------------------------------------------------------------------------
        public List<ShoppingCart> GetClientCart(int userId)
        {
            var cartItems = _context.ShoppingCart.Where(u => u.UserId == userId).ToList();
            if (cartItems.Count == 0)
            {
                throw new ArgumentException("El cliente no tiene productos");

            }

            return cartItems;
        }

        // Vaciar carrito 

        public void ClearCart(int userId)
        {
            var cartItems = _context.ShoppingCart
                .Where(u => u.UserId == userId)
                .ToList();

            _context.ShoppingCart.RemoveRange(cartItems);
            _context.SaveChanges();
        }

        public void ChangeStateUser(int id, bool? newState)
        {

            var user = _context.Users.Find(id);

            if (user == null)
            {
                throw new ArgumentException($"No se encontró un producto con el ID {id}.");
            }

            user.UserState = newState.Value;
            _context.SaveChanges();
        }

        public List<User> GetUserStateFalse() 
        {
            return _context.Users.Where(c=>c.UserState == false).ToList();
        }


        // BUSQUEDA ADMIN - Productos sin stock
        public List<Product> GetOutStock()
        {
            var outStock = _context.Product.Where(c => c.productState == false).ToList();

            if (!outStock.Any())
            {
                throw new Exception("No hay productos sin stock.");
            }

            return outStock;
        }

        // Historial de compra por id

        public List<History> GetOrder(int userId) 
        {
            return _context.Histories.Where(h => h.UserId == userId).ToList();
        }

        public List<History> GetAllOrders()
        {
            var history = _context.Histories.ToList();

            if (!history.Any())
            {
                throw new Exception("Vacio / Error");
            }
            return history;
        }


    }
}
