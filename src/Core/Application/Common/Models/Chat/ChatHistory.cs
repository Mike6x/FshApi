using FSH.WebApi.Application.Common.Chat;

namespace FSH.WebApi.Application.Common.Models.Chat;

public class ChatHistory<TUser> : IChatHistory<TUser>
    where TUser : IChatUser
{
    public long Id { get; set; }
    public string FromUserId { get; set; } = default!;
    public string ToUserId { get; set; } = default!;
    public string Message { get; set; } = default!;
    public DateTime CreatedDate { get; set; }
    public virtual TUser FromUser { get; set; } = default!;
    public virtual TUser ToUser { get; set; } = default!;
}