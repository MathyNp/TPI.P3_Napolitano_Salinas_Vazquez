using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TPI_NapolitanoSalinasVazquez_P3.Interfaces;
using TPI_NapolitanoSalinasVazquez_P3.Models.Dto;
using TPI_NapolitanoSalinasVazquez_P3.Models;
using TPI_NapolitanoSalinasVazquez_P3.Data;

namespace TPI_NapolitanoSalinasVazquez_P3.Controllers
{
   
        [Route("api/[controller]")]
        [ApiController]
        

        public class ClientController : ControllerBase
        {
            
            private readonly IUserService _userService;
            private readonly IProductService _productService;
            private readonly TPI_NapolitanoSalinasVazquez_P3Context _context;

            public ClientController( IUserService userService, IProductService productService, TPI_NapolitanoSalinasVazquez_P3Context context)
            {

                    _userService = userService;
                    _productService = productService;
                    _context = context;
            }

            // Crear cliente - Registro ---------------------------------------------------------------------------- >>
            
            [HttpPost("CreateClient")]
            public IActionResult CreateClient([FromBody] UserCreateClientDto dto)
            {
                Client client = new Client()
                {
                    UserMail = dto.UserMail,
                    UserName = dto.UserName,
                    UserPassword = dto.UserPassword,
                    UserRol = UserRoleEnum.Client,
                    address = dto.address,
                    paymentMethod = dto.paymentMethod,
                };

            int userId = _userService.CreateUser(client);

                return Ok($"El usuario {dto.UserName} | ID: {userId} Fue creado correctamente. ");
            }
        // Modificar el estado del cliente -----------------------------------------------
        [Authorize(Policy = "Admin")]
        [HttpPut("ChangeStateClient")]
            public IActionResult ChangeState(int id, bool? newState)
            {
                var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(UserId))
                {
                    return Unauthorized("Debe iniciar sesion para cambiar su estado.");
                }
                try
                {
                    _userService.ChangeStateUser(id, newState);
                    return Ok($"Actualizacion del estado del usuario {id} con exito.");
                }
                catch (ArgumentException ex)
                {
                    return NotFound(ex.Message);
                }
            }

        // Modificar credenciales cliente ----------------------------------------------------------------------

            [Authorize(Policy = "Client")]
            [HttpPut("UpdateClient/{id}")]

            public IActionResult updateClient(int id, [FromBody] UserUpdateClientDto dto)
            {

                try
                {
                    Client clienttoupdate = new Client()
                    {
                        UserID = id,
                        UserMail = dto.UserMail,
                        UserName = dto.UserName,
                        UserPassword = dto.UserPassword,
                        address = dto.address,
                        paymentMethod = dto.paymentMethod,
                    };
                    _userService.UpdateUser(clienttoupdate);
                    return Ok($"Usuario ID:{id} actualizado correctamente.");
                }
                catch { return BadRequest("Credenciales Invalidas"); }
            }




        // Agregar producto al carrito por ID ---------------------------------------------------------------

        [Authorize(Policy = "Client")]
        [HttpPost("AddToCart/{productId}/{amount}")]
            public IActionResult AddToCart(int productId, int amount)
            {
                var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                
                try
                {
                    var product = _productService.GetById(productId);                   

                    _userService.PurchaseProduct(productId, UserId, amount);
                    return Ok($"{amount} producto/s del id:{productId} agregado/s correctamente al carrito del usuario {UserId}");
            }
                catch (InvalidOperationException ex)
                {
                    return BadRequest(ex.Message);
                }
        }

        // Mostrar carrito ---------------------------------------------------------------------------------
        [Authorize(Policy = "Client")]
        [HttpGet("GetClientCart")]
        public IActionResult GetClientCart()
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            try
            {
                if (string.IsNullOrEmpty(UserId))
                {
                    return Unauthorized("No logeado");
                }

                var cart = _userService.GetClientCart(int.Parse(UserId));
                return Ok(cart);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Comprar carrito -----------------------------------------------------------------------------------
        [Authorize(Policy = "Client")]
        [HttpPut("FinishCart")]
        public IActionResult FinishCart()
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            
            try
            {
                if (string.IsNullOrEmpty(UserId))
                {
                    return Unauthorized("No logeado");
                }

                var cartItems = _userService.GetClientCart(int.Parse(UserId));

                decimal totalPrice = 0;

                foreach (var cartItem in cartItems)
                {
                    var product = _context.Product.Find(cartItem.productId);

                    if (product == null)
                    {
                        return BadRequest($"El producto con ID {cartItem.productId} no existe.");
                    }

                    var validationResult = _productService.ValidateSell(product);

                    if (!validationResult.IsSuccess)
                    {
                        return BadRequest(validationResult);
                    }

                    decimal discountedPrice = _userService.CalculateDiscountedPrice(int.Parse(UserId), product.productPrice);

                    totalPrice += discountedPrice;
                }

                var user = _context.Users.Find(int.Parse(UserId)) as Client;
                var paymentMessage = user?.paymentMethod == 1 ? "transferencia 10% de descuento" :
                                     user?.paymentMethod == 2 ? "tarjeta" :
                                     "otro";

                return Ok($"Compra realizada con éxito, con {paymentMessage}  total: ${totalPrice}");

                //_userService.FinishUserCart(int.Parse(UserId));

                //return Ok($"Compra realizada con exito, total: ${totalPrice}");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }




        // Limpiar carrito
        [Authorize(Policy = "Client")]
        [HttpDelete("ClearCart")]
        public IActionResult ClearCart()
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                if (string.IsNullOrEmpty(UserId))
                {
                    return Unauthorized("No logeado");
                }

                _userService.ClearCart(int.Parse(UserId));

                return Ok("Carrito vaciado con éxito.");
            }
            catch 
            {

                return BadRequest("Error al intentar vaciar el carrito");
            }
        }

        

        

        

    }
          
        

}
