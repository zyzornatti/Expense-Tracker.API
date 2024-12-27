using AutoMapper;
using Expense_Tracker.API.CustomActionFilters;
using Expense_Tracker.API.CustomExceptions;
using Expense_Tracker.API.Models.Domain;
using Expense_Tracker.API.Models.DTO;
using Expense_Tracker.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Expense_Tracker.API.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/auth")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenRepository;

        public UserController(IUserService userRepository, IMapper mapper, ITokenService tokenRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenRepository = tokenRepository;
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
               throw new ResourceNotFoundException("Invalid username or password.");
            }

            // Generate JWT
            var token = _tokenRepository.GenerateJwtToken(details);
            var response = new LoginResponseDto
            { 
                JwtToken = token 
            };
            return Ok(response);

        }
    }
}
