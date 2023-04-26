using FSH.WebApi.Domain.Place;

namespace FSH.WebApi.Application.Place.Channels;

public class CreateChannelRequestValidator : CustomValidator<CreateChannelRequest>
{
    public CreateChannelRequestValidator(IReadRepository<Channel> entityRepo, IStringLocalizer<CreateChannelRequestValidator> T)
    {
        RuleFor(e => e.Code)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (code, ct) => await entityRepo.FirstOrDefaultAsync(new ChannelByCodeSpec(code), ct) is null)
                .WithMessage((_, code) => T["Channel with Code{0} already exists.", code]);

        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await entityRepo.FirstOrDefaultAsync(new ChannelByNameSpec(name), ct) is null)
                .WithMessage((_, name) => T["Channel with Name {0} already exists.", name]);
    }
}

public class UpdateChannelRequestValidator : CustomValidator<UpdateChannelRequest>
{
    public UpdateChannelRequestValidator(IReadRepository<Channel> entityRepo, IStringLocalizer<UpdateChannelRequestValidator> T)
    {
        RuleFor(e => e.Code)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (entity, code, ct) =>
                    await entityRepo.FirstOrDefaultAsync(new ChannelByCodeSpec(code), ct)
                        is not Channel existingEntity || existingEntity.Id == entity.Id)
                .WithMessage((_, code) => T["Channel {0} already Exists.", code]);

        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (entity, name, ct) =>
                    await entityRepo.FirstOrDefaultAsync(new ChannelByNameSpec(name), ct)
                        is not Channel existingEntity || existingEntity.Id == entity.Id)
                .WithMessage((_, name) => T["Channel {0} already Exists.", name]);
    }
}