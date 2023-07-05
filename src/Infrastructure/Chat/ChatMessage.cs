using FSH.WebApi.Domain.Common.Contracts;
using FSH.WebApi.Infrastructure.Identity;

namespace FSH.WebApi.Infrastructure.Chat;
public class ChatMessage : AuditableEntity, IAggregateRoot
{
    public string FromUserId { get; set; } = default!;
    public string ToUserId { get; set; } = default!;
    public string Message { get; set; } = default!;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public bool IsRead { get; private set; }

    public virtual ApplicationUser FromUser { get; set; } = default!;
    public virtual ApplicationUser ToUser { get; set; } = default!;

    public ChatMessage(string fromUserId, string toUserId, string message, DateTime createdDate, bool isRead)
    {
        FromUserId = fromUserId;
        ToUserId = toUserId;
        Message = message;
        CreatedDate = createdDate;
        IsRead = isRead;
    }

    public ChatMessage()
        : this(string.Empty, string.Empty, string.Empty, DateTime.UtcNow, false)
    {
    }

    public ChatMessage Update(string? fromUserId, string? toUserId, string? message, DateTime? createdDate, bool? isRead)
    {
        if (fromUserId is not null && FromUserId?.Equals(fromUserId) is not true) FromUserId = fromUserId;
        if (toUserId is not null && ToUserId?.Equals(toUserId) is not true) ToUserId = toUserId;
        if (message is not null && Message?.Equals(message) is not true) Message = message;
        if (createdDate is not null && !CreatedDate.Equals(createdDate)) CreatedDate = (DateTime)createdDate;
        if (isRead is not null && !IsRead.Equals(isRead)) IsRead = (bool)isRead;

        return this;
    }
}