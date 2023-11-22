using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyTestTask.Models;
using MyTestTask.Services.Interfaces;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MyTestTask.Controllers
{

    //[Authorize]
    [Route("api/v1/contacts")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ContactControllers : ControllerBase
    {

        private readonly ITaskService taskService;
        private readonly IMapper _mapper;
        //private readonly SignInManager<IdentityUser> signInManager;
        //private readonly UserManager<IdentityUser> userManager;
        //private readonly IConfiguration _configuration;

        public ContactControllers(
            ITaskService taskService,
            IMapper mapper,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IConfiguration configuration)
        {
            this.taskService = taskService;
            _mapper = mapper;
            //this.signInManager = signInManager;
            //this.userManager = userManager;
            //_configuration = configuration;
        }

        [HttpGet]
        [Route("Task-GetAll")]
        //[Authorize]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var result = await taskService.GetAll(userId);
                var mappedResult = _mapper.Map<List<ContactDTO>>(result);
                return Ok(mappedResult);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to get all contacts: {ex.Message}");
            }
        }


        //[Authorize]
        [HttpGet]
        [Route("Task-Get/{taskId}")]
        public async Task<IActionResult> Get(int taskId)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var result = await taskService.Get(userId, taskId);
                var mappedResult = _mapper.Map<ContactDTO>(result);
                return Ok(mappedResult);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to get contact: {ex.Message}");
            }
        }

        //[Authorize]
        [HttpPost]
        [Route("Task-Add")]
        public async Task<IActionResult> Add([FromBody] Contact task)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var result = await taskService.Add(userId, task);
                var mappedResult = _mapper.Map<ContactDTO>(result);
                return Ok(mappedResult);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to add contact: {ex.Message}");
            }
        }

        //[Authorize]
        [HttpDelete]
        [Route("Task-Delete/{taskId}")]
        public async Task<IActionResult> Delete(int taskId)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                await taskService.Delete(userId, taskId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete contact: {ex.Message}");
            }
        }

        //[Authorize]
        [HttpPut]
        [Route("Task-Update/{taskId}")]
        public async Task<IActionResult> Update(int taskId, [FromBody] Contact task)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                await taskService.Update(userId, taskId, task);
                var updatedTask = await taskService.Get(userId, taskId);
                var mappedResult = _mapper.Map<ContactDTO>(updatedTask);
                return Ok(mappedResult);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to update contact: {ex.Message}");
            }
        }




    }


    //[Route("api/v1/contacts")]
    //[ApiController]
    //public class LoginControllers : ControllerBase
    //{

    //    private readonly ITaskService taskService;
    //    private readonly IMapper _mapper;
    //    private readonly SignInManager<IdentityUser> signInManager;
    //    private readonly UserManager<IdentityUser> userManager;
    //    private readonly IConfiguration _configuration;

    //    public LoginControllers(
    //        ITaskService taskService,
    //        IMapper mapper,
    //        SignInManager<IdentityUser> signInManager,
    //        UserManager<IdentityUser> userManager,
    //        IConfiguration configuration)
    //    {
    //        this.taskService = taskService;
    //        _mapper = mapper;
    //        this.signInManager = signInManager;
    //        this.userManager = userManager;
    //        _configuration = configuration;
    //    }

    //    [HttpPost]
    //    [Route("login")]
    //    public async Task<IActionResult> Login([FromBody] LoginViewModel model)
    //    {
    //        var user = await userManager.FindByNameAsync(model.Email);

    //        if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
    //        {
    //            var claims = new List<Claim>
    //            {
    //                new Claim(ClaimTypes.NameIdentifier, user.Id),
    //                new Claim(ClaimTypes.Name, user.UserName),
    //            };

    //            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JwtSettings:SecurityKey").Value));
    //            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    //            var token = new JwtSecurityToken(
    //                issuer: _configuration["JwtSettings:Issuer"],
    //                audience: _configuration["JwtSettings:Audience"],
    //                claims: claims,
    //                expires: DateTime.Now.AddHours(Convert.ToDouble(_configuration["JwtSettings:ValidForMinutes"])),
    //                signingCredentials: credentials
    //            );

    //            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

    //            return Ok(new { Token = tokenString });
    //        }

    //        return BadRequest("Invalid login attempt");
    //    }

    //    [HttpPost]
    //    [Route("register")]
    //    public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
    //    {
    //        var user = new IdentityUser { UserName = model.Email, Email = model.Email };

    //        var result = await userManager.CreateAsync(user, model.Password);

    //        if (result.Succeeded)
    //        {
    //            var claims = new List<Claim>
    //    {
    //        new Claim(ClaimTypes.NameIdentifier, user.Id),
    //        new Claim(ClaimTypes.Name, user.UserName),
    //        // Добавьте другие необходимые утверждения (claims)
    //    };

    //            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecurityKey"]));
    //            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    //            var token = new JwtSecurityToken(
    //                issuer: _configuration["JwtSettings:Issuer"],
    //                audience: _configuration["JwtSettings:Audience"],
    //                claims: claims,
    //                expires: DateTime.Now.AddHours(1), // Установите срок действия токена
    //                signingCredentials: credentials
    //            );

    //            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

    //            return Ok(new { Token = tokenString });
    //        }

    //        return BadRequest(result.Errors);
    //    }

    //    [HttpPost]
    //    [Route("logout")]
    //    [Authorize]
    //    public async Task<IActionResult> Logout()
    //    {
    //        await signInManager.SignOutAsync();
    //        return Ok("Logout successful");
    //    }
    //}
}