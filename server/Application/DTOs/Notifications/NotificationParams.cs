using Application.Enums;
using Action = Application.Enums.Action;

namespace Application.DTOs
{
    public record NotificationParams(Operation Operation, Action Action, string Recipient);
}