using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TPI_NapolitanoSalinasVazquez_P3.Interfaces;
using TPI_NapolitanoSalinasVazquez_P3.Models.Dto;
using TPI_NapolitanoSalinasVazquez_P3.Models;



namespace TPI_NapolitanoSalinasVazquez_P3.Controllers
{
   
        [Route("api/[controller]")]
        [ApiController]
        

        public class ClientController : ControllerBase
        {
            
            private readonly IUserService _userService;

            public ClientController( IUserService userService)
            {
                
                _userService = userService;
            }

            

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

                int id = _userService.CreateUser(client);
                return Ok(id);
            }

            [HttpPut("Update Client / {id}")]

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
            [Authorize(Policy = "Admin")]
            [HttpDelete("delete client")]
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

            [HttpPost("AddToCart/{productId}")]            
            public IActionResult AddToCart(int productId) 
            { 
                var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(UserId))
                {

                    return Unauthorized("El usuario no está autenticado.");
                }
                
                _userService.PurchaseProduct(productId, UserId);
                return Ok();
            }

            [HttpDelete("FinishCart")]
            public IActionResult FinishCart() 
            {
                var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(UserId))
                {
                    return Unauthorized("No logeado");
                }
                _userService.FinishUserCart(int.Parse(UserId));
                return Ok("Carrito terminado.");

                
            }

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
            
        }

}
