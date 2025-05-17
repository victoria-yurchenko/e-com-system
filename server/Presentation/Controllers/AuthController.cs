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
    public class AuthController(IAuthService authService, ILogger<AuthController> logger, ResponseService<string> responseService, ErrorMessagesConfig errorMessages) : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        private readonly ILogger<AuthController> _logger = logger;
        private readonly ResponseService<string> _responseService = responseService;
        private readonly ErrorMessagesConfig _errorMessages = errorMessages;

        [HttpPost("send-verify-account-code")]
        public async Task<IActionResult> SendAccountVerificationCodeAsync([FromBody] IdentifierDto identifierDto)
        {
            _logger.LogInformation($"Verifying account with identifier, test: {identifierDto.Identifier}");
            var verificationResult = await _authService.SendAccountVerificationCodeAsync(identifierDto.Identifier);
            return _responseService.SuccessResponse(verificationResult, HttpStatusCodes.OK);
        }

        [HttpPost("verify-account")]
        public async Task<IActionResult> ConfirmVerificationCodeAsync([FromBody] CodeVerificationDto codeVerificationDto)
        {
            var verificationResult = await _authService.ConfirmVerificationCodeAsync(codeVerificationDto.Identifier, codeVerificationDto.VerificationCode);
            try
            {
                return !verificationResult
                    ? _responseService.ClientErrorResponse("Invalid verification code", HttpStatusCodes.BadRequest)
                    : _responseService.SuccessResponse("Correct verification code", HttpStatusCodes.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error during ConfirmVerificationCodeAsync: {ex.Message}");
                return _responseService.ServerErrorResponse(_errorMessages.Unknown, HttpStatusCodes.InternalServerError);
            }
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

        // [HttpPost("confirm-verification-code")]
        // public async Task<IActionResult> ConfirmVerificationCodeAsync([FromBody] string identifier, [FromBody] string verificationCode)
        // {
        //     var doesMatch = await _authService.ConfirmVerificationCodeAsync(identifier, verificationCode);
        //     return _responseService.SuccessResponse(doesMatch.ToString(), HttpStatusCodes.OK);
        // }
    }
}
