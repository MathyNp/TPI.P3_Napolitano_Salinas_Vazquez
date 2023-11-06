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
        [Authorize]

        public class ClientController : ControllerBase
        {
            private readonly IClientService _clientService;
            private readonly IUserService _userService;

            public ClientController(IClientService clientService, IUserService userService)
            {
                _clientService = clientService;
                _userService = userService;
            }

            [HttpPost("Create Client")]
            public IActionResult CreateAdmin([FromBody] UserCreateDto dto)
            {
                // validacion para que solo pueda crear usuarios un Admin, se guarda el tipo de user en la variable rol
                string rol = User.Claims.FirstOrDefault(C => C.Type == ClaimTypes.Role).Value.ToString();

                if (rol == UserRoleEnum.Admin.ToString())
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
                return Forbid();
            }
        }
    
}
