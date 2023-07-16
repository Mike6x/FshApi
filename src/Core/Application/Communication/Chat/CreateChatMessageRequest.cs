using FSH.WebApi.Domain.Communication;
using FSH.WebApi.Domain.Production;

namespace FSH.WebApi.Application.Communication.Chat;

public class CreateChatMessageRequest : IRequest<DefaultIdType>
{
    public string FromUserId { get; set; } = default!;
    public string ToUserId { get; set; } = default!;
    public string Message { get; set; } = default!;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public bool? IsRead { get; set; }
    public bool? IsImageMesage { get; set; }
    public FileUploadRequest? Image { get; set; }
}

public class CreateChatMessageRequestHandler : IRequestHandler<CreateChatMessageRequest, DefaultIdType>
{
    private readonly IRepository<ChatMessage> _repository;
    private readonly IFileStorageService _file;

    private readonly INotificationSender _notifications;
    public CreateChatMessageRequestHandler(IRepository<ChatMessage> repository, IFileStorageService file, INotificationSender notifications)
    {
        _repository = repository;
        _file = file;
        _notifications = notifications;
    }

    public async Task<DefaultIdType> Handle(CreateChatMessageRequest request, CancellationToken cancellationToken)
    {
        string imagePath = await _file.UploadAsync<Product>(request.Image, FileType.Image, cancellationToken);

        var entity = new ChatMessage(
                request.FromUserId,
                request.ToUserId,
                request.Message,
                request.CreatedDate,
                request.IsRead ?? false,
                request.IsImageMesage ?? false,
                imagePath);

        await _repository.AddAsync(entity, cancellationToken);

        await NotifyAsync("You have a messange !", request.ToUserId, request.FromUserId, cancellationToken);

        return entity.Id;
    }

    private async Task NotifyAsync(string message, string receiverUserId, string senderUserId, CancellationToken cancellationToken)
    {
        await _notifications.SendToUserAsync(
            new ReceiveChatNotification()
            {
                Message = message,
                ReceiverUserId = receiverUserId,
                SenderUserId = senderUserId,
            },
            receiverUserId,
            cancellationToken);
    }
}