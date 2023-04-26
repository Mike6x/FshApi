using FSH.WebApi.Application.Common.Chat;
using FSH.WebApi.Application.Common.Models.Chat;
using FSH.WebApi.Application.Communication.Chat;
using FSH.WebApi.Application.Identity.Users;
using FSH.WebApi.Infrastructure.Persistence.Context;
using FSH.WebApi.Shared.Authorization;
using MapsterMapper;
using Microsoft.Extensions.Localization;

namespace FSH.WebApi.Infrastructure.Chat;
public class ChatService : IChatService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly IStringLocalizer<ChatService> _localizer;

    public ChatService(
        ApplicationDbContext context,
        IMapper mapper,
        IUserService userService,
        IStringLocalizer<ChatService> localizer)
    {
        _context = context;
        _mapper = mapper;
        _userService = userService;
        _localizer = localizer;
    }

    public async Task<IEnumerable<ChatUserResponse>> GetChatUsersAsync(string userId, CancellationToken cancellationToken)
    {
        var userRoles = await _userService.GetRolesAsync(userId, cancellationToken);
        bool userIsAdmin = userRoles?.Any(x => x.RoleName == FSHRoles.Admin) == true;
        var allUsers = await _userService.GetListAsync(cancellationToken);

        allUsers = allUsers?.Where(user => user.Id.ToString() != userId && (userIsAdmin || user.IsActive && user.EmailConfirmed)).ToList();

        return _mapper.Map<IEnumerable<ChatUserResponse>>(allUsers ?? new List<UserDetailsDto>());

        // var chatUsers = _mapper.Map<IEnumerable<ChatUserResponse>>(allUsers ?? new List<UserDetailsDto>());
        // return chatUsers;
    }

    public Task<IEnumerable<ChatHistoryResponse>> GetChatHistoryAsync(string userId, string contactId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SaveMessageAsync(ChatHistory<IChatUser> message, CancellationToken cancellationToken)
    {
        // message.ToUser = await _context.Users.Where(user => user.Id == message.ToUserId).FirstOrDefaultAsync();
        // await _context.ChatHistories.AddAsync(_mapper.Map<ChatHistory<ApplicationUser>>(message));
        await _context.SaveChangesAsync();
        return true;
    }
}
