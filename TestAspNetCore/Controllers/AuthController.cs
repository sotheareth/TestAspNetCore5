using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polly;
using System;
using System.Text;
using System.Threading.Tasks;
using TestAspNetCore.Helper;
using TestAspNetCore.Helpers;
using TestAspNetCore.Options;
using TestAspNetCore_Core.Dtos.Requests;
using TestAspNetCore_Core.Dtos.Responses;
using TestAspNetCore_Core.Entities;
using TestAspNetCore_Core.Interfaces;
using TestAspNetCore_Infrastructure.Helpers;

namespace TestAspNetCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly JwtSetting _setting;
        private readonly IUserService _userService;

        public AuthController(JwtSetting setting, IUserService userService)
        {
            _setting = setting;
            _userService = userService;
        }

        //[AllowAnonymous]
        //[HttpPost("CreateToken")]
        private async Task<IActionResult> CreateToken()
        {
            string tokenString = await JwtHelper.GenerateToken(new User(), _setting);            
            return Ok(new { Token = tokenString });
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequest request)
        {
            if (request == null) return Unauthorized();
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            var passwordHash = await BCryptHelper.PasswordHash(request.Password);
            var user = await _userService.GetUserByName(request.Username);
            if(user == null)
            {
                return BadRequest(new { message = "User not found" });
            }

            bool isVerified = await BCryptHelper.VerifyHash(request.Password, user.Password);
            if (!isVerified)
            {
                return BadRequest(new { message = "Invalid username or password" });
            }

            string tokenString = await JwtHelper.GenerateToken(user, _setting);
            user.RefreshTokens.Add(new RefreshToken { 
                Expires = DateTime.UtcNow.AddHours(1),
                Token = tokenString,
                Created = DateTime.UtcNow,
                CreatedByIp = await HttpHelper.GetIP4Address(HttpContext)
            });
            await _userService.Update(user);
            
            return Ok(new UserResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Token = tokenString
            });

        }

        [AllowAnonymous]
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromQuery] string token)
        {
            var user = await _userService.GetUserByToken(token);
            if (user == null) return Ok(new { message = "User not found." });

            var ipAddress = await HttpHelper.GetIP4Address(HttpContext);
            var response = await JwtHelper.GenerateRefreshToken(ipAddress);

            if (response == null)
                return Unauthorized(new { message = "Invalid token" });

            var newToken = await JwtHelper.GenerateToken(user, _setting);
            await _userService.RefreshToken(token, ipAddress, newToken);

            return Ok(new UserResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Token = newToken
            });
        }

        [AllowAnonymous]
        [HttpPost("RevokeToken")]
        public async Task<IActionResult> RevokeToken([FromQuery] string token)
        {
            var user = await _userService.GetUserByToken(token);
            if (user == null) return Ok(new { message = "User not found." });

            var ipAddress = await HttpHelper.GetIP4Address(HttpContext);
            var response = await JwtHelper.GenerateRefreshToken(ipAddress);

            if (response == null)
                return Unauthorized(new { message = "Invalid token" });

            var newToken = await JwtHelper.GenerateToken(user, _setting);
            await _userService.RevokeToken(token, ipAddress, newToken);

            return Ok(new UserResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Token = newToken
            });
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            var user = new User { 
                Username = request.Username,
                Password = await BCryptHelper.PasswordHash(request.Password),
                FirstName = request.FirstName,
                LastName = request.LastName
            };
            await _userService.Register(user);
            
            return Ok(new UserResponse { 
                Id = user.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username
            });
        }

        [AllowAnonymous]
        [HttpPost("TestRetry")]
        public async Task<IActionResult> TestRetry()
        {
            var message = Encoding.UTF8.GetBytes("hello, retry pattern");

            var retry = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            try
            {
                await retry.ExecuteAsync(async () =>
                {
                    Console.WriteLine($"begin at {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}.");
                    var result = await HttpRequestHelper.GetAsync("https://jsonplaceholder.typicode.com/todos");
                    Console.WriteLine($"result: {result}");
                    Console.WriteLine($"end at {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}.");
                    
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"exception here. {ex.Message}");
            }

            return Ok();
        }

    }
}
