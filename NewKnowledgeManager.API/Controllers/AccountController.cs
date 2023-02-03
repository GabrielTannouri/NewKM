using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NewKnowledgeManager.API.DTOs;
using NewKnowledgeManager.Domain.Enums;
using NewKnowledgeManager.Domain.Interfaces;
using NewKnowledgeManager.Domain.Models;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NewKnowledgeManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IUserApplication _userApplication;

        public AccountController(IConfiguration config,
                                 IUserApplication userApplication)
        {
            _config = config;
            _userApplication = userApplication;
        }

        [HttpGet]
        [Route("Admins")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AdminEndPoint()
        {
            var user = await _userApplication.GetUsers();
            return Ok(user);
        }

        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginUser loginRequest)
        {

            // Recupera o usuário
            var user = await _userApplication.GetUserByLogin(loginRequest.Login);

            // Verifica se o usuário existe
            if (user == null)
            {
                return NotFound(new { message = "Usuário ou senha inválidos" });
            }
            else
            {
                var claims = new List<Claim>();

                foreach (Role role in Enum.GetValues(typeof(Role)))
                {
                    if (user.Role <= role && role != Role.Auditor)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
                    }
                }

                // Gera o Token
                var token = this.GenerateToken(user, claims.ToArray());

                // Retorna os dados
                return Ok(token);
            }
        }

        private string GenerateToken(User user, Claim[] additionalClaims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Login),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            if (additionalClaims is object)
            {
                var claimList = new List<Claim>(claims);
                claimList.AddRange(additionalClaims);
                claims = claimList.ToArray();
            }

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
