using Ardalis.Specification;

namespace FSH.WebApi.Infrastructure.Chat;

public class ChatMessageByIdSpec : Specification<ChatMessage, ChatMessageDetailsDto>, ISingleResultSpecification<ChatMessage>
{
    public ChatMessageByIdSpec(DefaultIdType id) =>
        Query
            .Where(e => e.Id == id);
}

public class ChatMessageByFromUserSpec : Specification<ChatMessage>, ISingleResultSpecification<ChatMessage>
{
    public ChatMessageByFromUserSpec(string fromUserId) =>
        Query
            .Where(e => e.FromUser.ToString() == fromUserId);
}

public class ChatMessageByToUserSpec : Specification<ChatMessage>, ISingleResultSpecification<ChatMessage>
{
    public ChatMessageByToUserSpec(string toUserId) =>
        Query
            .Where(e => e.CreatedBy.ToString() == toUserId);
}