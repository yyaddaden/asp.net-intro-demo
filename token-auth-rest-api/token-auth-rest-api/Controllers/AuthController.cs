using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using token_auth_rest_api.Models;

namespace token_auth_rest_api.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Produces("application/json")]
    [Route("api/Users")]
    public class AuthController : ControllerBase
    {
        private TokenAuthDbContext _context;
        private ILogger<AuthController> _logger;
        private UserManager<ApiUser> _userManager;
        private IPasswordHasher<ApiUser> _passwordHasher;
        private IConfiguration _config;

        public AuthController(TokenAuthDbContext context, UserManager<ApiUser> userManager, IPasswordHasher<ApiUser> passwordHasher, ILogger<AuthController> logger, IConfiguration config)
        {
            _context = context;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _logger = logger;
            _config = config;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Login), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [Route("login")]
        public async Task<IActionResult> LoginUser([FromBody] Login model)
        {
            try
            {
                ApiUser user = await _userManager.FindByNameAsync(model.Username);

                if (user != null)
                {
                    if (PasswordVerificationResult.Success == _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password))
                    {
                        var test = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);

                        Claim[] claims = new Claim[]
                        {
                            new Claim(ClaimTypes.Name, user.UserName),
                            new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                            new Claim(ClaimTypes.Role, user.Role)
                        };

                        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));
                        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        JwtSecurityToken token = new JwtSecurityToken(
                            issuer: _config["Token:Issuer"],
                            audience: _config["Token:Audience"],
                            claims: claims,
                            expires: DateTime.UtcNow.AddMinutes(15),
                            signingCredentials: creds
                        );

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception thrown: {ex}");
            }

            return StatusCode((int)HttpStatusCode.Forbidden);
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Register), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [Route("register")]
        public async Task<IActionResult> RegisterUser([FromForm] Register model)
        {
            try
            {
                ApiUser user = new ApiUser()
                {
                    UserName = model.Username,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Role = model.Role
                };

                user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);
                await _userManager.CreateAsync(user);
                return StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception thrown: {ex}");
            }

            return BadRequest();
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [Route("admin")]
        public async Task<IActionResult> InfoAdmin()
        {
            string username = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            ApiUser user = await _userManager.FindByNameAsync(username);
            return Ok(new { UserId = user.Id, Message = $"[ {user.Role.ToUpper()} ] Welcome {user.FirstName} {user.LastName}" });
        }

        [HttpGet]
        [Authorize(Roles = "user")]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [Route("user")]
        public async Task<IActionResult> InfoUser()
        {
            string username = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            ApiUser user = await _userManager.FindByNameAsync(username);
            return Ok(new { UserId = user.Id, Message = $"[ {user.Role.ToUpper()} ] Welcome {user.FirstName} {user.LastName}" });
        }
    }
}
