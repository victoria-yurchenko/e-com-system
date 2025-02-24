using System.Text.Json;
using Domain.Entities;

namespace Application.Extensions
{
    public static class UserExtensions
    {
        public static string ToJsonString(this User user)
        {
            return JsonSerializer.Serialize(user);
        }
    }
}
