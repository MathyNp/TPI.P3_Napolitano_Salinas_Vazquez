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

            // Crear cliente - Registro ----------------------------------------------------------------------------
            
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
                };

         
                return Ok($"El usuario {dto.UserName} fue creado correctamente. ");
            }
        // Modificar el estado del cliente -----------------------------------------------

            [Authorize(Policy = "Admin")]
            [HttpPut("ChangeStateClient/{id}")]
            public IActionResult ChangeState(int id, bool? newState)
            {
                try
                {
                    _userService.ChangeStateUser(id, newState);
                    return Ok($"Cliente {id} suspendido con exito.");
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
                    };
                    _userService.UpdateUser(clienttoupdate);
                    return Ok($"Usuario ID:{id} actualizado correctamente.");
                }
                catch { return BadRequest("Credenciales Invalidas"); }
            }

            // Borrar cliente de la db ----------------------------------------------------------------------------

            [Authorize(Policy = "Admin")]
            [HttpDelete("DeleteClient")]
            public IActionResult DeleteClient()
            {
                try
                {
                    int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
                    _userService.DeleteUser(id);
                    return Ok($"Usuario ID:{id} eliminado con exito");
                }
                catch { return BadRequest("Usuario no encontrado"); }

            }
            
            
            // Agregar producto al carrito por ID ---------------------------------------------------------------

            
            [HttpPost("AddToCart/{productId}")]
        public IActionResult AddToCart(int productId)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(UserId))
            {
                return Unauthorized("Debe iniciar sesion para agregar productos al carrito.");
            }

            var product = _productService.GetById(productId);
            if (product == null)
            {
                return NotFound($"El producto con ID {productId} no existe.");
            }

            _userService.PurchaseProduct(productId, UserId);
            return Ok();
        }

        // Comprar carrito -----------------------------------------------------------------------------------

        [HttpDelete("FinishCart")]
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

                    totalPrice += product.productPrice;
                }

                _userService.FinishUserCart(int.Parse(UserId));

                return Ok($"Compra realizada con exito, total: ${totalPrice}");
            }
            catch
            {

                return BadRequest("Error al procesar la compra");
            }
        }


        // Mostrar carrito ---------------------------------------------------------------------------------

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
                catch (ArgumentException ex)
                {
                    return BadRequest(ex.Message);
                }
            }

        // Limpiar carrito

        [HttpDelete("ClearCart")]
        public IActionResult ClearCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized("No logeado");
                }

                _userService.ClearCart(int.Parse(userId));

                return Ok("Carrito vaciado con éxito.");
            }
            catch 
            {

                return BadRequest("Error al intentar vaciar el carrito");
            }
        }

        // Mostrar los clientes dados de baja

        [Authorize(Policy = "Admin")]
        [HttpGet("GetClientStateFalse")]

        public IActionResult getClienteStateFalse()
        {
            try
            {
                List<User> clientStateFalse = _userService.GetUserStateFalse();
                return Ok(clientStateFalse);
            }
            catch
            {
                return BadRequest("cledenciales invalidas");
            }
            
        }

        

    }
          
        

}
