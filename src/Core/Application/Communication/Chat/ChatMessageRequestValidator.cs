namespace FSH.WebApi.Application.Communication.Chat;

public class CreateChatMessageRequestValidator : CustomValidator<CreateChatMessageRequest>
{
    public CreateChatMessageRequestValidator()
    {
        RuleFor(e => e.ToUserId)
            .NotEmpty()
            .MaximumLength(256);

        RuleFor(e => e.Message)
            .MaximumLength(256);
    }
}

public class UpdateChatMessageRequestValidator : CustomValidator<UpdateChatMessageRequest>
{
    public UpdateChatMessageRequestValidator()
    {
        RuleFor(e => e.ToUserId)
            .NotEmpty()
            .MaximumLength(256);

        RuleFor(e => e.Message)
            .NotEmpty()
            .MaximumLength(256);
    }
}