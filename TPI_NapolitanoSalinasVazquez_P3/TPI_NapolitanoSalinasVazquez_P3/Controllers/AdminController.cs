using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using TPI_NapolitanoSalinasVazquez_P3.Interfaces;
using TPI_NapolitanoSalinasVazquez_P3.Models;
using TPI_NapolitanoSalinasVazquez_P3.Models.Dto;
using TPI_NapolitanoSalinasVazquez_P3.Models.Responses;
using TPI_NapolitanoSalinasVazquez_P3.Services;

namespace TPI_NapolitanoSalinasVazquez_P3.Controllers
{

    [Route("api/[controller]")]
    [ApiController]


    public class AdminController : Controller
    {
        
        private readonly IUserService _userService;
        private readonly IHistoryService _historyService;

        public AdminController(IUserService userService, IHistoryService historyService)
        {
            
            _userService = userService;
            _historyService = historyService;
        }

        // Crear Admin --------------------------------------------------------

        [Authorize(Policy = "Admin")]
        [HttpPost("CreateAdmin")]

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
            catch (UnauthorizedAccessException)
            {
                return Forbid("No tienes los permisos necesarios");
            }

        }

        // Editar credenciales Admin --------------------------------------------------------

        [Authorize(Policy = "Admin")]
        [HttpPut("UpdateAdmin/{id}")]

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
            catch (UnauthorizedAccessException)
            {
                return Forbid("No tienes los permisos necesarios");
            }
        }

        // Eliminar Admins -----------------------------------------------------------------

        [Authorize(Policy = "Admin")]
        [HttpDelete("DeleteAdmin/{id}")]
        public IActionResult DeleteAdmin(int id)
        {
            try
            {
                var deleteClient = _userService.GetUserById(id);
                if (deleteClient == null) return BadRequest("usuario no encontrado");

                _userService.DeleteUser(id);
                return Ok($"Usuario ID:{id} eliminado con exito");
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid("No tienes los permisos necesarios");
            }

        }

        // Lista de Admins --------------------------------------------------------

        [Authorize(Policy = "Admin")]
        [HttpGet("GetAllAdmins")]
        
        public IActionResult GetAdmins()
        {
            try
            {
                List<User> admins = _userService.GetAdmins();
                return Ok(admins);
            }
            catch
            {
                return BadRequest("Credenciales Invalidas");
            }
        }

        // Lista de clientes ====================================================

        [Authorize(Policy = "Admin")]
        [HttpGet("GetAllClients")]


        public IActionResult GetClients()
        {
            try
            {
                List<User> clients = _userService.GetClients();
                return Ok(clients);
            }
            catch(UnauthorizedAccessException)
            {
                return Forbid("No tienes los permisos necesarios");
            }
        }

        //Lista de todos los usuarios ------------------------------------------------

        [Authorize(Policy = "Admin")]
        [HttpGet("GetAllUsers")]

        public IActionResult GetAllUsers() 
        {
            try
            {
                List<User> users = _userService.GetAllUsers();
                return Ok(users);
            }
            catch(UnauthorizedAccessException)
            {
                return Forbid("No tienes los permisos necesarios");
            }
        }

        // Lista de carritos --------------------------------------------------------------

        [Authorize(Policy = "Admin")]
        [HttpGet("GetAllCart")]
        public IActionResult GetAllCart()
        {
            try
            {
                var cartItems = _userService.GetCart();
                return Ok(cartItems);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid("No tienes los permisos necesarios");
            }
        }
        // ADMIN OPTIONS ---------------------------------------------------------

        [Authorize(Policy = "Admin")]
        [HttpGet("GetOutStock")]
        public IActionResult GetStock()
        {
            var outStock = _userService.GetOutStock();
            try
            {
                return Ok(outStock);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // Historial total de las ordenes ----------------------------------------------------------------------------------------------------------------
        [Authorize(Policy = "Admin")]
        [HttpGet("GetOrderHistory")]
        public IActionResult GetAllHistory()
        {
            try
            {
                var orders = _historyService.GetAllHistory();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        // Historial de compra por ID --------------------------------------------------------------------------------------------------------------------
        [Authorize(Policy = "Admin")]
        [HttpGet("GetOrderHistoryByUserId")]
        public IActionResult GetHistoryById(int userId)
        {
            var history = _historyService.GetHistoryByClient(userId);

            if (history == null)
             {
                return BadRequest($"No se encontro ningun usuario con el id {userId}");
             }

            return Ok(history);
        }

        // filtra por fecha------------------------------------------------------------------------------

        [Authorize(Policy = "Admin")]
        [HttpGet("filtreDate")]
        public ActionResult<List<History>> GetHistoriesByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                var histories = _historyService.GetHistoriesByDateRange(startDate, endDate);
                return Ok(histories);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        // Lista de clientes dados de baja -------------------------------------------------------------------------------------------------------------
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
                return BadRequest("Credenciales invalidas");
            }

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


    }
}
