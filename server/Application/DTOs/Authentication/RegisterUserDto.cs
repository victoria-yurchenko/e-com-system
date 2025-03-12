using System.Text.Json.Serialization;

namespace Application.DTOs.Authentication
{
    public record RegisterUserDto(
        [property: JsonPropertyName("email")] string Email,
        [property: JsonPropertyName("password")] string Password
    );
}
