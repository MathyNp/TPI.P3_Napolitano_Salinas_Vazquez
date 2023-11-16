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
            private readonly IClientService _clientService;
            private readonly IUserService _userService;

            public ClientController(IClientService clientService, IUserService userService)
            {
                _clientService = clientService;
                _userService = userService;
            }

            [Authorize(Policy = "Admin")]
            [HttpGet("all client")]

            public IActionResult GetClients() 
            {
                try
                {
                    List<User> clients = _clientService.GetClients();
                    return Ok(clients);
                }
                catch
                {
                    return BadRequest("Credenciales Invalidas");
                }
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

        [HttpPost("AddToCart/{UserId}/{productId}")]
            public IActionResult AddToCart(int UserId, int productId) 
            { 
                _userService.PurchaseProduct(productId, UserId);
                return Ok();
            }

            [HttpDelete("FinishCart/{UserId}")]
            public IActionResult FinishCart(int UserId) 
            {
                _userService.FinishUserCart(UserId);
                return Ok();
            }
            
    }

}
