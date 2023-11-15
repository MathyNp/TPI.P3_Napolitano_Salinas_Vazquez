using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
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

        [HttpPut]
        public IActionResult updateAdmin([FromBody] UserUpdateDto dto)
        {
            string rol = User.Claims.FirstOrDefault(C => C.Type == ClaimTypes.Role).Value.ToString();

            if (rol == UserRoleEnum.Admin.ToString())
            {
                Admin admintoupdate = new Admin()
                {
                    UserID = int.Parse(User.Claims.FirstOrDefault(C => C.Type == ClaimTypes.NameIdentifier).Value),
                    UserMail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value,
                    UserName = User.Claims.FirstOrDefault(c => c.Type.Contains("username")).Value,
                    UserPassword = dto.UserPassword,

                };
                _userService.UpdateUser(admintoupdate);
                return Ok();
            }
            return Forbid();
        }

        [HttpGet("GetAllCart")]
        public IActionResult GetAllCart()
        {
            var cartItems = _userService.GetCart();
            return Ok(cartItems);
        }

    }
}
