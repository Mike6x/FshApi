using FSH.WebApi.Domain.Common.Contracts;
using FSH.WebApi.Infrastructure.Identity;

namespace FSH.WebApi.Infrastructure.Chat;
public class ChatMessage : AuditableEntity, IAggregateRoot
{
    public string FromUserId { get; private set; }
    public virtual ApplicationUser FromUser { get; set; } = default!;
    public string ToUserId { get; private set; }
    public virtual ApplicationUser ToUser { get; set; } = default!;
    public string Message { get; private set; }
    public bool IsRead { get; private set; }

    public ChatMessage(string fromUserId, string toUserId, string message, bool isRead)
    {
        FromUserId = fromUserId;
        ToUserId = toUserId;
        Message = message;
        IsRead = isRead;
    }

    public ChatMessage()
        : this(string.Empty, string.Empty, string.Empty, false)
    {
    }

    public ChatMessage Update(string? fromUserId, string? toUserId, string? message, bool? isRead)
    {
        if (fromUserId is not null && FromUserId?.Equals(fromUserId) is not true) FromUserId = fromUserId;
        if (toUserId is not null && ToUserId?.Equals(toUserId) is not true) ToUserId = toUserId;
        if (message is not null && Message?.Equals(message) is not true) Message = message;
        if (isRead is not null && !IsRead.Equals(isRead)) IsRead = (bool)isRead;

        return this;
    }
}
