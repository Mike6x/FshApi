//using FSH.WebApi.Application.Common.Chat;
//using FSH.WebApi.Application.Common.Exceptions;
//using FSH.WebApi.Application.Communication.ChatMessages;
//using FSH.WebApi.Application.Identity.Users;
//using FSH.WebApi.Domain.Communication;
//using FSH.WebApi.Infrastructure.Persistence.Context;
//using FSH.WebApi.Shared.Authorization;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Localization;

//namespace FSH.WebApi.Infrastructure.Chat;
//public class ChatService : IChatService
//{
//    private readonly ApplicationDbContext _context;
//    private readonly IUserService _userService;
//    private readonly IStringLocalizer<ChatService> _localizer;

//    public ChatService(
//        ApplicationDbContext context,
//        IUserService userService,
//        IStringLocalizer<ChatService> localizer)
//    {
//        _context = context;
//        _userService = userService;
//        _localizer = localizer;
//    }

//    public async Task<IEnumerable<UserDetailsDto>> GetChatUsersAsync(string userId, CancellationToken cancellationToken)
//    {
//        var userRoles = await _userService.GetRolesAsync(userId, cancellationToken);
//        bool userIsAdmin = userRoles?.Any(x => x.RoleName == FSHRoles.Admin) == true;
//        var allUsers = await _userService.GetListAsync(cancellationToken);

//        allUsers = allUsers?.Where(user => user.Id.ToString() != userId && (userIsAdmin || (user.IsActive && user.EmailConfirmed))).ToList();

//        return allUsers ?? new List<UserDetailsDto>();
//    }

//    //public async Task<IEnumerable<ChatHistoryResponse>> GetChatHistoryAsync(string userId, string contactId, CancellationToken cancellationToken)
//    //{
//    //    var response = await _userService.GetAsync(userId, cancellationToken);
//    //    if (response != null)
//    //    {
//    //        var query = await _context.ChatMessages
//    //            .Where(h => (h.FromUserId == userId && h.ToUserId == contactId) || (h.FromUserId == contactId && h.ToUserId == userId))
//    //            .OrderBy(a => a.CreatedOn)
//    //            .Select(x => new ChatHistoryResponse
//    //            {
//    //                Id = x.Id,
//    //                Message = x.Message,
//    //                CreatedDate = x.CreatedOn,

//    //                FromUserId = x.FromUserId,
//    //                FromUserFullName = $"{x.FromUserFirstName} {x.FromUserLastName}",
//    //                FromUserImageUrl = x.FromUserImageUrl ?? string.Empty,
//    //                ToUserId = x.ToUserId,
//    //                ToUserFullName = $"{x.ToUserFirstName} {x.ToUserLastName}",
//    //                ToUserImageUrl = x.ToUserImageUrl ?? string.Empty,
//    //            }).ToListAsync();

//    //        return query ?? new List<ChatHistoryResponse>();
//    //    }
//    //    else
//    //    {
//    //        throw new NotFoundException(_localizer["User Not Found."]);
//    //    }
//    //}

//    //public async Task<int> SaveMessageAsync(ChatHistory<IChatUser> message, CancellationToken cancellationToken)
//    //{
//    //    var toUser = await _context.Users
//    //        .Where(user => user.Id == message.ToUserId).FirstOrDefaultAsync(cancellationToken);
//    //    var fromUser = await _context.Users
//    //        .Where(user => user.Id == message.ToUserId).FirstOrDefaultAsync(cancellationToken);

//    //    var chatMessage = new ChatMessage(
//    //            fromUser.Id,
//    //            fromUser.UserName ?? fromUser.Id,
//    //            fromUser.FirstName ?? fromUser.Id,
//    //            fromUser.LastName,
//    //            fromUser.ImageUrl,
//    //            toUser.Id,
//    //            toUser.UserName ?? toUser.Id,
//    //            toUser.FirstName ?? toUser.Id,
//    //            toUser.LastName,
//    //            toUser.ImageUrl,
//    //            message.Message,
//    //            false);

//    //    await _context.ChatMessages.AddAsync(chatMessage, cancellationToken);

//    //    return await _context.SaveChangesAsync(cancellationToken);
//    //}

//    //public async Task<int> SaveChatMessageAsync(ChatInfo message, CancellationToken cancellationToken)
//    //{
//    //    var toUser = await _context.Users
//    //        .Where(user => user.Id == message.ToUserId).FirstOrDefaultAsync(cancellationToken);
//    //    if (toUser != null) await _context.ChatMessages.AddAsync(message, cancellationToken);

//    //    return await _context.SaveChangesAsync(cancellationToken);
//    //}

//    //public async Task<IEnumerable<ChatMessageDto>> GetChatConversationAsync(string userId, string contactId, CancellationToken cancellationToken)
//    //{
//    //    var response = await _userService.GetAsync(userId, cancellationToken);
//    //    if (response != null)
//    //    {
//    //        var query = await _context.ChatMessages
//    //            .Where(h => (h.FromUserId == userId && h.ToUserId == contactId) || (h.FromUserId == contactId && h.ToUserId == userId))
//    //            .OrderBy(a => a.CreatedOn)
//    //            .Select(x => new ChatMessageDto
//    //            {
//    //                Id = x.Id,
//    //                Message = x.Message,
//    //                CreatedOn = x.CreatedOn,

//    //                FromUserId = x.FromUserId,
//    //                FromUserName = x.FromUserName,
//    //                FromUserFirstName = x.FromUserFirstName,
//    //                FromUserLastName = x.FromUserLastName,
//    //                FromUserImageUrl = x.FromUserImageUrl,

//    //                ToUserId = x.ToUserId,
//    //                ToUserName = x.ToUserName,
//    //                ToUserFirstName = x.ToUserFirstName,
//    //                ToUserLastName = x.ToUserLastName,
//    //                ToUserImageUrl = x.ToUserImageUrl,
//    //            }).ToListAsync();

//    //        return query ?? new List<ChatMessageDto>();
//    //    }
//    //    else
//    //    {
//    //        throw new NotFoundException(_localizer["User Not Found."]);
//    //    }
//    //}

//}