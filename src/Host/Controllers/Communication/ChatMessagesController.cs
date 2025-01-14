﻿using FSH.WebApi.Application.Communication.Chat;
using FSH.WebApi.Domain.Communication;

namespace FSH.WebApi.Host.Controllers.Communication;

public class ChatMessagesController : VersionedApiController
{
    [HttpPost("remove")]
    [MustHavePermission(FSHAction.Delete, FSHResource.ChatMessages)]
    [OpenApiOperation("Remove messages of one user.", "")]
    public Task<List<ChatMessage>> RemoveAsync(RemoveChatMessagesRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("conversation/{userId}/{contactId}")]
    [MustHavePermission(FSHAction.View, FSHResource.ChatMessages)]
    [OpenApiOperation("Get ChatMessage Conversation between two users.", "")]
    public async Task<List<ChatMessageDto>> GetConversationAsync(string userId, string contactId)
    {
        return await Mediator.Send(new GetChatConversationRequest(userId, contactId));
    }

    [HttpPost("GetList")]
    [MustHavePermission(FSHAction.View, FSHResource.ChatMessages)]
    [OpenApiOperation("Get ChatMessage between two users.", "")]
    public Task<List<ChatMessageDto>> GetListAsync(GetChatMessagesRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.ChatMessages)]
    [OpenApiOperation("Search ChatMessages using available filters.", "")]
    public Task<PaginationResponse<ChatMessageDto>> SearchAsync(SearchChatMessagesRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.ChatMessages)]
    [OpenApiOperation("Get ChatMessage details.", "")]
    public Task<ChatMessageDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetChatMessageRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.ChatMessages)]
    [OpenApiOperation("Create a new ChatMessage.", "")]
    public Task<Guid> CreateAsync(CreateChatMessageRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.ChatMessages)]
    [OpenApiOperation("Update a ChatMessage.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateChatMessageRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.ChatMessages)]
    [OpenApiOperation("Delete a ChatMessage.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteChatMessageRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.ChatMessages)]
    [OpenApiOperation("Export a ChatMessages.", "")]
    public async Task<FileResult> ExportAsync(ExportChatMessagesRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "ChatMessageExports");
    }

    [HttpPost("import")]
    [MustHavePermission(FSHAction.Import, FSHResource.ChatMessages)]
    [OpenApiOperation("Import a ChatMessages.", "")]
    public async Task<ActionResult<int>> ImportAsync(ImportChatMessagesRequest request)
    {
        return Ok(await Mediator.Send(request));
    }
}