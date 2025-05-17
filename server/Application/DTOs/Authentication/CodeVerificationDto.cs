using System.Text.Json.Serialization;

namespace Application.DTOs.Authentication
{
    public record CodeVerificationDto(
        [property: JsonPropertyName("identifier")] string Identifier,
        [property: JsonPropertyName("verificationCode")] string VerificationCode);
}
