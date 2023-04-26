using FSH.WebApi.Application.Common.Models.Chat;
using FSH.WebApi.Application.Communication.Chat;

namespace FSH.WebApi.Application.Common.Chat;
public interface IChatService
{
    Task<IEnumerable<ChatUserResponse>> GetChatUsersAsync(string userId, CancellationToken cancellationToken);

    Task<bool> SaveMessageAsync(ChatHistory<IChatUser> message, CancellationToken cancellationToken);

    Task<IEnumerable<ChatHistoryResponse>> GetChatHistoryAsync(string userId, string contactId, CancellationToken cancellationToken);
}
