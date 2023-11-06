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
    [Authorize]

    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IUserService _userService;

        public AdminController(IAdminService adminService, IUserService userService)
        {
            _adminService = adminService;
            _userService = userService;
        }

        [HttpPost("Create Admin")]
        public IActionResult CreateAdmin([FromBody] UserCreateDto dto)
        {
            // validacion para que solo pueda crear usuarios un Admin, se guarda el tipo de user en la variable rol
            string rol = User.Claims.FirstOrDefault(C => C.Type == ClaimTypes.Role).Value.ToString();

            if (rol == UserRoleEnum.Admin.ToString())
            {
                Admin admin = new Admin()
                {
                    UserMail = dto.UserMail,
                    UserName = dto.UserName,
                    UserPassword = dto.UserPassword,
                    UserRol = UserRoleEnum.Admin,
                };
                int id = _userService.CreateUser(admin);
                return Ok(id);
            }
            return Forbid();
        }
    }
}
