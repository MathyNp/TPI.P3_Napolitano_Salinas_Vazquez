using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TPI_NapolitanoSalinasVazquez_P3.Interfaces;
using TPI_NapolitanoSalinasVazquez_P3.Models;
using TPI_NapolitanoSalinasVazquez_P3.Models.Dto;
using TPI_NapolitanoSalinasVazquez_P3.Services;

namespace TPI_NapolitanoSalinasVazquez_P3.Controllers
{

    [Route("api/[controller]")]
    [ApiController]


    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IUserService _userService;

        public AdminController(IAdminService adminService, IUserService userService)
        {
            _adminService = adminService;
            _userService = userService;
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("All Admins")]
        
        public IActionResult GetAdmins()
        {
            try
            {
                List<User> admins = _adminService.GetAdmins();
                return Ok(admins);
            }
            catch
            {
                return BadRequest("Credenciales Invalidas");
            }
        }

        [Authorize(Policy = "Admin")]
        [HttpPost("Create Admin")]
     
        public IActionResult CreateAdmin([FromBody] UserCreateDto dto)
        {
            try
            {
                Admin admin = new Admin()
                {
                    UserMail = dto.UserMail,
                    UserName = dto.UserName,
                    UserPassword = dto.UserPassword,
                    UserRol = UserRoleEnum.Admin,
                };
                int id = _userService.CreateUser(admin);
                return Ok($"El administrador fue creado correctamente | ID: {admin.UserID} | UserName: {admin.UserName}");
            }
            catch { return BadRequest("Credenciales Invalidas"); }

        }

        [Authorize(Policy = "Admin")]
        [HttpPut("Update Admin / {id}")]
     
        public IActionResult updateAdmin(int id, [FromBody] UserUpdateDto dto)
        {

            try
            {
                Admin admintoupdate = new Admin()
                {
                    UserID = id,
                    UserMail = dto.UserMail,
                    UserName = dto.UserName,
                    UserPassword = dto.UserPassword,
                };
                _userService.UpdateUser(admintoupdate);
                return Ok($"Usuario ID:{id} actualizado correctamente.");
            }
            catch {return BadRequest("Credenciales Invalidas"); }
        }

        [HttpGet("GetAllCart")]
        public IActionResult GetAllCart()
        {
            var cartItems = _userService.GetCart();
            return Ok(cartItems);
        }



    }
}
