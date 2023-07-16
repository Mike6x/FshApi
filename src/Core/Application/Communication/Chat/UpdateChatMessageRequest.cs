using FSH.WebApi.Domain.Communication;
using FSH.WebApi.Domain.Production;

namespace FSH.WebApi.Application.Communication.Chat;

public class UpdateChatMessageRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }

    public string FromUserId { get; set; } = default!;
    public string ToUserId { get; set; } = default!;
    public string? Message { get; set; }
    public DateTime? CreatedDate { get; set; }
    public bool? IsRead { get; set; }
    public bool? IsImageMesage { get; set; }
    public bool DeleteCurrentImage { get; set; }
    public FileUploadRequest? Image { get; set; }
}

public class UpdateChatMessageRequestHandler : IRequestHandler<UpdateChatMessageRequest, DefaultIdType>
{
    private readonly IRepository<ChatMessage> _repository;
    private readonly IStringLocalizer _t;
    private readonly IFileStorageService _file;

    public UpdateChatMessageRequestHandler(IRepository<ChatMessage> repository, IStringLocalizer<UpdateChatMessageRequestHandler> localizer, IFileStorageService file) =>
        (_repository, _t, _file) = (repository, localizer, file);

    public async Task<DefaultIdType> Handle(UpdateChatMessageRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = entity
        ?? throw new NotFoundException(_t["Entity {0} Not Found.", request.Id]);

        // Remove old image if flag is set
        if (request.DeleteCurrentImage)
        {
            string? currentMessageImagePath = entity.ImagePath;
            if (!string.IsNullOrEmpty(currentMessageImagePath))
            {
                string root = Directory.GetCurrentDirectory();
                _file.Remove(Path.Combine(root, currentMessageImagePath));
            }

            entity = entity.ClearImagePath();
        }

        string? imagePath = request.Image is not null
            ? await _file.UploadAsync<Product>(request.Image, FileType.Image, cancellationToken)
            : null;

        _ = entity.Update(
                request.FromUserId,
                request.ToUserId,
                request.Message,
                request.CreatedDate,
                request.IsRead,
                request.IsImageMesage,
                imagePath);

        await _repository.UpdateAsync(entity, cancellationToken);

        return request.Id;
    }
}