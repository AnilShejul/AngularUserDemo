
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AngularDemo.API.Core;
using AngularDemo.API.DTOs;
using AngularDemo.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AngularDemo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _config = config;
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDTO user)
        {

            user.EmailAddress = user.EmailAddress.ToLower();
            if (await _repo.UserExist(user.EmailAddress))
                return BadRequest("Email Address is already registered.Please try with another email.");

            var userToCreate = new WebUser
            {
                EmailAddress = user.EmailAddress
            };

            var createdUser = await _repo.Register(userToCreate, user.Password);
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDTO user)
        {
            var userObj = await _repo.Login(user.EmailAddress.ToLower(), user.Password);

            if (userObj == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,userObj.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier,userObj.EmailAddress.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds=new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

            var tokenDescritor=new SecurityTokenDescriptor
            {
                Subject=new ClaimsIdentity(claims),
                Expires=DateTime.Now.AddDays(1),
                SigningCredentials=creds,
                
                
            };

            var tokenHandler=new JwtSecurityTokenHandler();

            var token= tokenHandler.CreateToken(tokenDescritor);
            return Ok(
                new{
                    token=tokenHandler.WriteToken(token)
                }
            );



        }


    }
}