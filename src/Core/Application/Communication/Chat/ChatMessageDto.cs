namespace FSH.WebApi.Application.Communication.Chat;

public class ChatMessageDto : IDto
{
    public DefaultIdType Id { get; set; }

    public string FromUserId { get; set; } = default!;
    public string ToUserId { get; set; } = default!;
    public string Message { get; set; } = default!;
    public bool IsRead { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsImageMessage { get; set; }
    public string? ImagePath { get; set; }
}

public class ChatMessageDetailsDto : IDto
{
    public DefaultIdType Id { get; set; }
    public string FromUserId { get; set; } = default!;
    public string ToUserId { get; set; } = default!;
    public string Message { get; set; } = default!;
    public bool IsRead { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsImageMessage { get; set; }
    public string? ImagePath { get; set; }
}

public class ChatMessageExportDto : IDto
{
    public DefaultIdType Id { get; set; }
    public string FromUserId { get; set; } = default!;
    public string ToUserId { get; set; } = default!;
    public string Message { get; set; } = default!;
    public bool IsRead { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsImageMessage { get; set; }
    public string? ImagePath { get; set; }

    public DefaultIdType CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public DefaultIdType LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}