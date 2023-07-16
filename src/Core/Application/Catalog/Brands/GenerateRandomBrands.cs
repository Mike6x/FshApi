namespace FSH.WebApi.Application.Catalog.Brands;
public class GenerateRandomBrands : IRequest
{
    public int NSeed { get; set; }
}

public class GenerateRandomBrandsHandler(
    ISender mediator,
    INotificationSender notifications,
    ICurrentUser currentUser) : IRequestHandler<GenerateRandomBrands>
{
    private readonly ISender _mediator = mediator;
    private readonly INotificationSender _notifications = notifications;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task Handle(GenerateRandomBrands request, CancellationToken cancellationToken)
    {
        await NotifyAsync("Your job processing has started", 0, cancellationToken);

        foreach (int index in Enumerable.Range(1, request.NSeed))
        {
            await _mediator.Send(
                new CreateBrandRequest
                {
                    Code = $"Brand Random - {Guid.NewGuid()}",
                    Name = $"Brand Random - {Guid.NewGuid()}",
                    Description = "Funny description"
                },
                cancellationToken);

            await NotifyAsync("Progress: ", request.NSeed > 0 ? (index * 100 / request.NSeed) : 0, cancellationToken);
        }

        await NotifyAsync("Job successfully completed", 0, cancellationToken);

    }

    private async Task NotifyAsync(string message, int progress, CancellationToken cancellationToken)
    {
        //_progress.SetValue(progress);
        await _notifications.SendToUserAsync(
            new JobNotification()
            {
                //JobId = _performingContext.BackgroundJob.Id,
                Message = message,
                Progress = progress
            },
            _currentUser.GetUserId().ToString(),
            cancellationToken);
    }
}
