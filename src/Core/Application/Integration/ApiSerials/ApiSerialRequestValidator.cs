using FSH.WebApi.Domain.Integration;

namespace FSH.WebApi.Application.Integration.ApiSerials;

public class CreateApiSerialRequestValidator : CustomValidator<CreateApiSerialRequest>
{
    public CreateApiSerialRequestValidator()
    {
        RuleFor(e => e.ItemSerial)
            .NotEmpty()
            .MaximumLength(75);
        RuleFor(e => e.ItemCode)
            .NotEmpty()
            .MaximumLength(75);

        RuleFor(e => e.ItemName)
            .NotEmpty()
            .MaximumLength(75);
    }
}

public class UpdateApiSerialRequestValidator : CustomValidator<UpdateApiSerialRequest>
{
    public UpdateApiSerialRequestValidator()
    {
        RuleFor(e => e.ItemSerial)
            .NotEmpty()
            .MaximumLength(75);
        RuleFor(e => e.ItemCode)
            .NotEmpty()
            .MaximumLength(75);

        RuleFor(e => e.ItemName)
            .NotEmpty()
            .MaximumLength(75);
    }
}