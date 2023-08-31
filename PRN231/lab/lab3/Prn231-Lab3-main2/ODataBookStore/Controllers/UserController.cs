using BussinessObject.Models;
using DataAccess.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

using ODataBookStore.Request;
using ODataBookStore.Utils;
using System.Security.Claims;

namespace ODataBookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly IUserService _userService;

        public UserController(JwtService jwtService, IUserService userService)
        {
            this._jwtService = jwtService;
            this._userService = userService;
        }
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(LoginRequest loginRequest)
        {
            
            User user =  _userService.GetByUsernameAsync(loginRequest.UserName).Result;
            if (user == null)
            {
                return BadRequest();
            }
            bool isVerify = PasswordService.Verify(loginRequest.Password, user.Password);
            if (isVerify)
            {
                string token = _jwtService.GenerateJwtToken(user.Username, user.Role == Role.ADMIN ? "admin" : "user", Convert.ToString(user.Id));
                return Ok(token);
            }
            return Unauthorized();
        }

        [HttpGet("Infor")]
        [Authorize]
        public async Task<IActionResult> GetInfor()
        {
            ClaimsPrincipal currentUser = HttpContext.User;
            int userId = Convert.ToInt32(currentUser.FindFirstValue("Id"));
            return Ok(_userService.GetByIdAsync(userId).Result);
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(User user)
        {
            try
            {
                user.Password = PasswordService.Hash(user.Password);
                User tmp = await _userService.SaveUser(user);
                if (tmp == null)
                {
                    return BadRequest();
                }

                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
