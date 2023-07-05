using FSH.WebApi.Infrastructure.Chat;
using FSH.WebApi.Infrastructure.Identity;
using FSH.WebApi.Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FSH.WebApi.Host.Controllers.Communication;

public class MessagesController : VersionedApiController
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;
    public MessagesController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    //[HttpGet("{contactId}")]
    //public async Task<List<ChatMessage>> GetConversationAsync(string contactId)
    //{
    //    string? userId = User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).Select(a => a.Value).FirstOrDefault();
    //    var messages = await _context.ChatMessages
    //            .Where(h => (h.FromUserId == contactId && h.ToUserId == userId) || (h.FromUserId == userId && h.ToUserId == contactId))
    //            .OrderBy(a => a.CreatedDate)
    //            .Include(a => a.FromUser)
    //            .Include(a => a.ToUser)
    //            .Select(x => new ChatMessage
    //            {
    //                FromUserId = x.FromUserId,
    //                Message = x.Message,
    //                CreatedDate = x.CreatedDate,

    //                // Id = x.Id,
    //                ToUserId = x.ToUserId,
    //                ToUser = x.ToUser,
    //                FromUser = x.FromUser
    //            }).ToListAsync();
    //    return messages;
    //}

    [HttpGet("users")]
    public async Task<List<ApplicationUser>> GetUsersAsync()
    {
        string? userId = User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).Select(a => a.Value).FirstOrDefault();
        var allUsers = await _context.Users.Where(user => user.Id != userId).ToListAsync();
        return allUsers;
    }

    [HttpGet("users/{userId}")]
    public async Task<ApplicationUser?> GetUserDetailsAsync(string userId)
    {
        var user = await _context.Users.Where(user => user.Id == userId).FirstOrDefaultAsync();
        return user;
    }

    //[HttpPost("save")]
    //public async Task<IActionResult> SaveMessageAsync(ChatMessage message)
    //{
    //    var userId = User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).Select(a => a.Value).FirstOrDefault();
    //    message.FromUserId = userId;
    //    message.CreatedDate = DateTime.Now;
    //    message.ToUser = await _context.Users.Where(user => user.Id == message.ToUserId).FirstOrDefaultAsync();
    //    await _context.ChatMessages.AddAsync(message);
    //    return Ok(await _context.SaveChangesAsync());
    //}

    //[HttpPost]
    //[MustHavePermission(FSHAction.Create, FSHResource.ChatMessages)]
    //[OpenApiOperation("Create a new ChatMessage.", "")]
    //public Task<Guid> CreateAsync(CreateChatMessageRequest request)
    //{
    //    return Mediator.Send(request);
    //}
}
