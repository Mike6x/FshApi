namespace FSH.WebApi.Application.Communication.Chat;
public partial class ChatHistoryResponse
{
    public long Id { get; set; } = default!;
    public string FromUserId { get; set; } = default!;
    public string FromUserImageURL { get; set; } = default!;
    public string FromUserFullName { get; set; } = default!;
    public string ToUserId { get; set; } = default!;
    public string ToUserImageURL { get; set; } = default!;
    public string ToUserFullName { get; set; } = default!;
    public string Message { get; set; } = default!;
    public DateTime CreatedDate { get; set; }
}
