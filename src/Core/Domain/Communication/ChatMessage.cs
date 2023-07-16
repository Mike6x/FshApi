namespace FSH.WebApi.Domain.Communication;
public class ChatMessage : AuditableEntity, IAggregateRoot
{
    public string? FromUserId { get; set; }
    public string? ToUserId { get; set; }
    public string Message { get; set; } = default!;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public bool IsRead { get; set; }
    public bool IsImageMessage { get; set; }
    public string? ImagePath { get; set; }

     // public virtual ApplicationUser? FromUser { get; set; }
     // public virtual ApplicationUser? ToUser { get; set; }

    public ChatMessage(string? fromUserId, string? toUserId, string message, DateTime createdDate, bool isRead, bool isImgMessage, string? imagePath)
    {
        FromUserId = fromUserId;
        ToUserId = toUserId;
        Message = message;
        CreatedDate = createdDate;
        IsRead = isRead;
        IsImageMessage = isImgMessage;
        ImagePath = imagePath;
    }

    public ChatMessage()
        : this(string.Empty, string.Empty, string.Empty, DateTime.UtcNow, false, false, string.Empty)
    {
    }

    public ChatMessage Update(string? fromUserId, string? toUserId, string? message, DateTime? createdDate, bool? isRead, bool? isImageMessage, string? imagePath)
    {
        if (fromUserId is not null && FromUserId?.Equals(fromUserId) is not true) FromUserId = fromUserId;
        if (toUserId is not null && ToUserId?.Equals(toUserId) is not true) ToUserId = toUserId;
        if (message is not null && Message?.Equals(message) is not true) Message = message;
        if (createdDate is not null && !CreatedDate.Equals(createdDate)) CreatedDate = (DateTime)createdDate;
        if (isRead is not null && !IsRead.Equals(isRead)) IsRead = (bool)isRead;
        if (isImageMessage is not null && !IsImageMessage.Equals(isImageMessage)) IsImageMessage = (bool)isImageMessage;
        if (imagePath is not null && ImagePath?.Equals(imagePath) is not true) ImagePath = imagePath;

        return this;
    }

    public ChatMessage ClearImagePath()
    {
        ImagePath = string.Empty;
        return this;
    }
}