using Application.Configurations;
using Application.DTOs;
using Application.Enums;
using Application.Exceptions;
using Application.Interfaces;
using Application.Services;
using Application.Services.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Presentation.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        private readonly ResponseService<string> _responseService;
        private readonly ErrorMessagesConfig _errorMessages;


        public AuthController(IAuthService authService, ILogger<AuthController> logger, ResponseService<string> responseService, ErrorMessagesConfig errorMessages)
        {
            _authService = authService;
            _logger = logger;
            _responseService = responseService;
            _errorMessages = errorMessages;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerDto)
        {
            _logger.LogInformation($"Registering user with email: {registerDto.Email}");

            try
            {
                var user = await _authService.RegisterUserAsync(registerDto.Email, registerDto.Password);
                return _responseService.SuccessResponse(user, HttpStatusCodes.OK);
            }
            catch (UserAlreadyExistsException ex)
            {
                return _responseService.ClientErrorResponse(_errorMessages.EmailAlreadyInUse, HttpStatusCodes.Conflict);
            }
            catch (ArgumentException ex)
            {
                return _responseService.ClientErrorResponse(ex.Message, HttpStatusCodes.BadRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error during registration: {ex.Message}");
                return _responseService.ServerErrorResponse(_errorMessages.Unknown, HttpStatusCodes.InternalServerError);
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginUserDto loginUserDto)
        {
            var token = _authService.AuthenticateUserAsync(loginUserDto.Email, loginUserDto.Password);
            return Ok(new { Token = token });
        }
    }
}
