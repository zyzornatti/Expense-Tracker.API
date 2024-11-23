using AutoMapper;
using Expense_Tracker.API.CustomActionFilters;
using Expense_Tracker.API.Models.Domain;
using Expense_Tracker.API.Models.DTO;
using Expense_Tracker.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Expense_Tracker.API.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/auth")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserController(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost("register")]
        [ValidateModel]
        //[Route("register")]
        public async Task<IActionResult> Register([FromBody] UserDto loginInput)
        {
            var details = await _userRepository.RegisterAsync(_mapper.Map<User>(loginInput));
            if(details == null)
            {
                return BadRequest("Username or email already exists.");
            }

            return Ok("User registered successfully.");
            //return Ok(_mapper.Map<UserDto>(details));
        }

        [HttpPost("login")]
        [ValidateModel]
        public async Task<IActionResult> Login([FromBody] LoginDto loginInput)
        {
            var details = await _userRepository.LoginAsync(_mapper.Map<User>(loginInput));
            if (details == null)
            {
                Unauthorized("Invalid username or password.");
            }

            // Generate JWT
            var token = GenerateJwtToken(details);
            return Ok(new { Token = token });

        }

        // Generate JWT Token
        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
