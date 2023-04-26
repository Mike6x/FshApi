using FSH.WebApi.Application.Common.Chat;
using FSH.WebApi.Application.Common.Models.Chat;

namespace FSH.WebApi.Application.Communication.Chat;
public class ChatUserResponse
{
    public string Id { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string ProfilePictureDataUrl { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string EmailAddress { get; set; } = default!;
    public bool IsOnline { get; set; }
    public virtual ICollection<ChatHistory<IChatUser>> ChatHistoryFromUsers { get; set; } = default!;
    public virtual ICollection<ChatHistory<IChatUser>> ChatHistoryToUsers { get; set; } = default!;
}