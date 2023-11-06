
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;
using TPI_NapolitanoSalinasVazquez_P3.Models;
using TPI_NapolitanoSalinasVazquez_P3.Models.Dto;
using TPI_NapolitanoSalinasVazquez_P3.Models.Responses;
using TPI_NapolitanoSalinasVazquez_P3.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TPI_NapolitanoSalinasVazquez_P3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        public IUserService _userService;
        public IConfiguration _config;

        public AuthenticateController(IUserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] CredentialsDto credentialsDto)
        {
            BaseResponse loginResult = _userService.Login(credentialsDto.MailUser, credentialsDto.Password);
            if ( loginResult.Message == "wrong email") 
            {
                return BadRequest(loginResult.Message);
            }
            else if (loginResult.Message == "wrong password")
            {
                return Unauthorized();
            }
            if (loginResult.IsSuccess)
            {
                User user = _userService.GetUserByEmail(credentialsDto.MailUser);

                var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"]));
                var signature = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);
                string roleClaimValue = user.UserRol.ToString();

                var claimsForToken = new List<Claim>();
                claimsForToken.Add(new Claim("sub", user.UserID.ToString()));
                claimsForToken.Add(new Claim("email", user.UserMail));
                claimsForToken.Add(new Claim("username", user.UserName));
                claimsForToken.Add(new Claim("rol", roleClaimValue));

                var jwtSecurityToken = new JwtSecurityToken(
                    _config["Authentication:Issuer"],
                    _config["Authentication:Audience"],
                    claimsForToken,
                    DateTime.UtcNow,
                    DateTime.UtcNow.AddHours(1),
                    signature);

                string tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                return Ok(tokenToReturn);
            }
            return BadRequest();
        }
    }
}
