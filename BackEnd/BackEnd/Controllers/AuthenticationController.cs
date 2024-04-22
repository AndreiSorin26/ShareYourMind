using Database.Entities;
using Services.Interfaces;
using Services.Exceptions;
using Services.DTOs;

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IUserService m_userService, IConfiguration m_configuration) : Controller
    {
        private String? GenerateJwtToken(User user)
        {
            var jwtKey = m_configuration["JwtConfiguration:Key"];
            var lifeSpanInSeconds = Convert.ToInt32(m_configuration["JwtConfiguration:LifeSapanInSeconds"]);
            if (jwtKey == null)
                return null;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.DisplayName),
                new(ClaimTypes.Role, user.Role.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddSeconds(lifeSpanInSeconds),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [HttpPost]
        public ActionResult<LoginResponseDTO> LogIn([FromBody] LoginCredentialsDTO loginRequest)
        {
            try
            {
                User user = m_userService.GetRaw(loginRequest.DisplayName);
                if (user.Approved == false)
                    return Unauthorized("User not approved yet");

                const int iterationCount = 350000;
                const int saltSize = 64;

                var passwordBytes = Encoding.UTF8.GetBytes(loginRequest.Password);
                var hashedPassword = Rfc2898DeriveBytes.Pbkdf2(passwordBytes, user.Salt, iterationCount, HashAlgorithmName.SHA512, saltSize);
                var password = Convert.ToHexString(hashedPassword);

                if (password != user.Password)
                    return Unauthorized("Invalid username or password");

                var token = GenerateJwtToken(user);
                if (token == null)
                    return StatusCode(500, "Server could not generate token");

                var response = new LoginResponseDTO()
                {
                    Token = token
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return ex switch
                {
                    BadQuery bq => BadRequest(bq.Message),
                    EntityNotFound => NotFound(),
                    _ => StatusCode(500, ex.Message),
                };
            }
        }
    }
}
