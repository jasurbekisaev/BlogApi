using BlogApi.DtoModels.UserDtoModel;
using BlogApi.Managers;
using BlogApi.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager _usermanager;
        private ILogger<UserController> _logger;
        private readonly UserProvider _userProvider;

        public UserController(UserManager usermanager, ILogger<UserController> logger, UserProvider userProvider)
        {
            _usermanager = usermanager;
            _logger = logger;
            _userProvider = userProvider;
        }

        [HttpPost("signUp")]
        public async Task<IActionResult> SignUp([FromBody] CreateUserDto model)
        {
            if (model == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _usermanager.Register(model);

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var token = await _usermanager.Login(model);

            return Ok(new { Token = token });
        }


        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var userId = _userProvider.UserId;

            var user = await _usermanager.GetUser(userId);
            if (user == null)
            {
                return Unauthorized();
            }
            return Ok(new UserDto(user));

        }
        [HttpPost("GetUser")]
        public async Task<IActionResult> GetUser(string userName)
        {
            var user = await _usermanager.GetUser(userName);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(new UserDto(user));
        }

    }
}
