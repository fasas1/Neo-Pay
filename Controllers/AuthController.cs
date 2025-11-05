using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeoPay.Entities;
using NeoPay.Entities.Dtos;
using NeoPay.IServices;
using System.Net;

namespace NeoPay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly APIResponse _response;

        public AuthController(IUserService userService)
        {
            _userService = userService;
            _response = new APIResponse();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.CreateUserAsync(model);
            if (!result.Succeeded)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.AddRange(result.Errors.Select(e => e.Description));
                return BadRequest(_response);
            }

            _response.StatusCode = HttpStatusCode.Created;
            _response.IsSuccess = true;
            _response.Result = new
            {
                message = "User registered successfully",
                userName = model.UserName,
                fullName = model.Name
            };

            return StatusCode((int)HttpStatusCode.Created, _response);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
           var result = await _userService.LoginAsync(model);
            if (!result.IsSuccessful)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.Unauthorized;
                _response.ErrorMessages.Add(result.Message);
                return Unauthorized(_response);

            }
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = new
            {
                token = result.Token,
                fullName = result.FullName,
                role = result.Role
            };

            return Ok(_response);
        }
    }
}
