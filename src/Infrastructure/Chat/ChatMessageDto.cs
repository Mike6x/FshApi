using FSH.WebApi.Application.Common.Interfaces;

namespace FSH.WebApi.Infrastructure.Chat;

public class ChatMessageDto : IDto
{
    public DefaultIdType Id { get; set; }

    public string FromUserId { get; set; } = default!;
    public string FromUserEmail { get; set; } = default!;
    public string FromUserUserName { get; set; } = default!;
    public string FromUserFirstName { get; set; } = default!;
    public string? FromUserLastName { get; set; }

    // public string? FromUserImageUrl { get; set; }
    // public string FromUserFullName => FromUserFirstName + " " + FromUserLastName;

    public string ToUserId { get; set; } = default!;
    public string ToUserEmail { get; set; } = default!;
    public string ToUserUserName { get; set; } = default!;
    public string ToUserFirstName { get; set; } = default!;
    public string? ToUserLastName { get; set; }

    // public string? ToUserImageUrl { get; set; }
    // public string ToUserFullName => ToUserFirstName + " " + ToUserLastName;

    public string Message { get; set; } = default!;
    public bool IsRead { get; set; }
    public DateTime CreatedDate { get; set; }
}

public class ChatMessageDetailsDto : IDto
{
    public DefaultIdType Id { get; set; }

    public string FromUserId { get; set; } = default!;
    public string FromUserEmail { get; set; } = default!;
    public string FromUserUserName { get; set; } = default!;
    public string FromUserFirstName { get; set; } = default!;
    public string? FromUserLastName { get; set; }

    // public string? FromUserImageUrl { get; set; }

    public string ToUserId { get; set; } = default!;
    public string ToUserEmail { get; set; } = default!;
    public string ToUserUserName { get; set; } = default!;
    public string ToUserFirstName { get; set; } = default!;
    public string? ToUserLastName { get; set; }

    // public string? ToUserImageUrl { get; set; }

    public string Message { get; set; } = default!;
    public bool IsRead { get; set; }
    public DateTime CreatedDate { get; set; }
}

public class ChatMessageExportDto : IDto
{
    public DefaultIdType Id { get; set; }
    public string FromUserId { get; set; } = default!;
    public string ToUserId { get; set; } = default!;
    public string Message { get; set; } = default!;
    public bool IsRead { get; set; }
    public DateTime CreatedDate { get; set; }

    public string FromUserEmail { get; set; } = default!;
    public string FromUserUserName { get; set; } = default!;
    public string FromUserFirstName { get; set; } = default!;
    public string? FromUserLastName { get; set; }
    public string FromUserFullName => FromUserFirstName + " " + FromUserLastName;

    public string ToUserEmail { get; set; } = default!;
    public string ToUserUserName { get; set; } = default!;
    public string ToUserFirstName { get; set; } = default!;
    public string? ToUserLastName { get; set; }
    public string ToUserFullName => ToUserFirstName + " " + ToUserLastName;

    public DefaultIdType CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public DefaultIdType LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}