using Application.Configurations;
using Application.DTOs.Authentication;
using Application.Enums;
using Application.Exceptions;
using Application.Interfaces.Authentication;
using Application.Services.ServerResponces;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("verify-account")]
        public async Task<IActionResult> VerifyAccountAsync(string identifier)
        {
            var verificationResult = await _authService.VerifyAccountAsync(identifier);
            return _responseService.SuccessResponse(verificationResult, HttpStatusCodes.OK);
        }

// TODO Add custom exceptions 
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerDto)
        {
            _logger.LogInformation($"Registering user with email: {registerDto.Email} and password: {registerDto.Password}");

            try
            {
                var user = await _authService.RegisterUserAsync(registerDto.Email, registerDto.Password);
                return _responseService.SuccessResponse(user, HttpStatusCodes.OK);
            }
            catch (UserAlreadyExistsException)
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
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            var token = await _authService.AuthenticateUserAsync(loginUserDto.Email, loginUserDto.Password);
            return Ok(new { Token = token });
        }

        [HttpPost("confirm-verification-code")]
        public async Task<IActionResult> ConfirmVerificationCodeAsync(string identifier, string verificationCode)
        {
            var doesMatch = await _authService.ConfirmVerificationCodeAsync(identifier, verificationCode);
            return _responseService.SuccessResponse(doesMatch.ToString(), HttpStatusCodes.OK);
        }
    }
}
