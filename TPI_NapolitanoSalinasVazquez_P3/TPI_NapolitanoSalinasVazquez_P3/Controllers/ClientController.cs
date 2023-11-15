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

            [HttpPost("CreateClient")]
            public IActionResult CreateAdmin([FromBody] UserCreateDto dto)
            {
                Client client = new Client()
                {
                    UserMail = dto.UserMail,
                    UserName = dto.UserName,
                    UserPassword = dto.UserPassword,
                    UserRol = UserRoleEnum.Client
                };

                int id = _userService.CreateUser(client);
                return Ok(id);
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
