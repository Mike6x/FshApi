using FSH.WebApi.Application.Elearning.Quizs;

namespace FSH.WebApi.Host.Controllers.Elearning;

public class QuizsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Quizs)]
    [OpenApiOperation("Search Quizs using available filters.", "")]
    public Task<PaginationResponse<QuizDto>> SearchAsync(SearchQuizsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Quizs)]
    [OpenApiOperation("Get Quiz details.", "")]
    public Task<QuizDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetQuizRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Quizs)]
    [OpenApiOperation("Create a new Quiz.", "")]
    public Task<Guid> CreateAsync(CreateQuizRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Quizs)]
    [OpenApiOperation("Update a Quiz.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateQuizRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Quizs)]
    [OpenApiOperation("Delete a Quiz.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteQuizRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.Quizs)]
    [OpenApiOperation("Export a Quizs.", "")]
    public async Task<FileResult> ExportAsync(ExportQuizsRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "QuizExports");
    }

    [HttpPost("import")]
    [MustHavePermission(FSHAction.Import, FSHResource.Quizs)]
    [OpenApiOperation("Import a Quizs Info.", "")]
    public async Task<ActionResult<int>> ImportAsync(ImportQuizsRequest request)
    {
        return Ok(await Mediator.Send(request));
    }
}