using FSH.WebApi.Domain.Common.Contracts;
using FSH.WebApi.Infrastructure.Identity;

namespace FSH.WebApi.Infrastructure.Chat;
public class Message : AuditableEntity, IAggregateRoot
{
    public string FromUserId { get; set; } = default!;
    public string ToUserId { get; set; } = default!;
    public string MessageText { get; set; } = default!;
    public DateTime CreatedDate { get; set; }
    public bool IsRead { get; private set; }

    public virtual ApplicationUser FromUser { get; set; } = default!;
    public virtual ApplicationUser ToUser { get; set; } = default!;

    public Message(string toUserId, string messageText, bool isRead)
    {
        ToUserId = toUserId;
        MessageText = messageText;
        IsRead = isRead;
    }

    public Message()
        : this(string.Empty, string.Empty, false)
    {
    }

    public Message Update(string? toUserId, string? message, bool? isRead)
    {
        if (toUserId is not null && ToUserId?.Equals(toUserId) is not true) ToUserId = toUserId;
        if (message is not null && MessageText?.Equals(message) is not true) MessageText = message;
        if (isRead is not null && !IsRead.Equals(isRead)) IsRead = (bool)isRead;

        return this;
    }
}