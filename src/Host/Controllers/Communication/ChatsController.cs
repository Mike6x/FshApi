using FSH.WebApi.Application.Common.Chat;
using FSH.WebApi.Application.Common.Interfaces;
using FSH.WebApi.Application.Common.Models.Chat;
using FSH.WebApi.Application.Communication.Chat;

namespace FSH.WebApi.Host.Controllers.Communication;

public class ChatsController : VersionedApiController
{
    private readonly ICurrentUser _currentUserService;
    private readonly IChatService _chatService;

    public ChatsController(ICurrentUser currentUserService, IChatService chatService)
    {
        _currentUserService = currentUserService;
        _chatService = chatService;
    }

    /// <summary>
    /// Get user wise chat history.
    /// </summary>
    /// <param name="contactId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Status 200 OK.</returns>
    /// Get user wise chat history
    [HttpGet("{contactId}")]
    [MustHavePermission(FSHAction.View, FSHResource.Chats)]
    [OpenApiOperation("get user wise chat history.", "")]
    public async Task<IEnumerable<ChatHistoryResponse>> GetChatHistoryAsync(string contactId, CancellationToken cancellationToken)
    {
        return await _chatService.GetChatHistoryAsync(_currentUserService.GetUserId().ToString(), contactId, cancellationToken);
    }

    /// <summary>
    /// get available users.
    /// </summary>
    /// <returns>Status 200 OK.</returns>
    /// get available users - sorted by date of last message if exists
    [HttpGet("users")]
    [MustHavePermission(FSHAction.View, FSHResource.Chats)]
    [OpenApiOperation("get available users.", "")]
    public async Task<IEnumerable<ChatUserResponse>> GetChatUsersAsync(CancellationToken cancellationToken)
    {
        return await _chatService.GetChatUsersAsync(_currentUserService.GetUserId().ToString(), cancellationToken);
    }

    /// <summary>
    /// Save Chat Message.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Status 200 OK.</returns>
    /// save chat message
    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Chats)]
    [OpenApiOperation("save chat message.", "")]
    public async Task<bool> SaveMessageAsync(ChatHistory<IChatUser> message, CancellationToken cancellationToken)
    {
        message.FromUserId = _currentUserService.GetUserId().ToString();
        message.ToUserId = message.ToUserId;
        message.CreatedDate = DateTime.Now;

        await _chatService.SaveMessageAsync(message, cancellationToken);
        return true;
    }
}
