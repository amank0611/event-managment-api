using EventManagement.Application.DTOs;
using EventManagement.Application.Interfaces.Services;
using EventManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EventManagementAPI.Controllers
{
    [Route("api/Authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("UserLogin")]
        public async Task<IActionResult> UserLogin(string email, string password)
        {
            return Ok(await _authenticationService.UserLogin(email, password));
        }
        [HttpPut("RegisterUser")]
        public async Task<IActionResult> RegisterUser(UserDto userDto)
        {
            return Ok(await _authenticationService.RegisterUser(userDto));
        }
    }
}
