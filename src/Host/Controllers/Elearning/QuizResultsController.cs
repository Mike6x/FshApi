using FSH.WebApi.Application.Elearning.QuizResults;
using Microsoft.AspNetCore.Cors;

namespace FSH.WebApi.Host.Controllers.Elearning;

public class QuizResultsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.QuizResults)]
    [OpenApiOperation("Search QuizResults using available filters.", "")]
    public Task<PaginationResponse<QuizResultDto>> SearchAsync(SearchQuizResultsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.QuizResults)]
    [OpenApiOperation("Get QuizResult details.", "")]
    public Task<QuizResultDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetQuizResultRequest(id));
    }

    [HttpPost("mobile-create")]
    [TenantIdHeader]
    [AllowAnonymous]
    [Consumes("application/x-www-form-urlencoded")]
    [OpenApiOperation("Anonymous user creates a new QuizResult.", "")]
    public Task<Guid> MobileCreateAsync([FromForm] MobileCreateQuizResultRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.QuizResults)]
    [OpenApiOperation("Create a new QuizResult.", "")]
    public Task<Guid> CreateAsync(CreateQuizResultRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.QuizResults)]
    [OpenApiOperation("Update a QuizResult.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateQuizResultRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.QuizResults)]
    [OpenApiOperation("Delete a QuizResult.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteQuizResultRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.QuizResults)]
    [OpenApiOperation("Export a QuizResults.", "")]
    public async Task<FileResult> ExportAsync(ExportQuizResultsRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "QuizResultExports");
    }
}