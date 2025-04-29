using System.Text.Json.Serialization;

namespace Application.DTOs.Authentication
{
    public record IdentifierDto([property: JsonPropertyName("identifier")] string Identifier);
}
