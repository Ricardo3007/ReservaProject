using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ReservaProject.Applications;
using ReservaProject.Applications.Contrats;
using ReservaProject.DTo;
using ReservaProject.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReservaProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IConfiguration _configuration;

        public UsuarioController(IUsuarioService usuarioService, IConfiguration configuration)
        {
            _usuarioService = usuarioService;
            _configuration = configuration;
        }

        /// <summary>
        /// consultar Mesa
        /// </summary>
        [HttpGet("[action]")]
        public Request<List<ClienteDTO>> GetCliente()
        {
            return _usuarioService.GetCliente();
        }


        //[AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            Request<bool> existUsu = _usuarioService.GetUsuario(loginModel.Nombre, loginModel.Password);
            if (existUsu.Result == true)
            {
                var token = GenerateJwtToken(loginModel.Nombre);
                //return Request<IActionResult>.Succes(Request < IActionResult >(token));
                return Ok(new { Token = token });
            }
            return Unauthorized();
        }

        private string GenerateJwtToken(string username)
        {
            var jwtSettings = _configuration.GetSection("Jwt").Get<JwtSettings>();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, username)
            };

            var token = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenf = tokenHandler.CreateToken(token);

            return tokenHandler.WriteToken(tokenf);
        }
    }
}
