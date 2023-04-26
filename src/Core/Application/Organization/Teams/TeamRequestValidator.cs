using FSH.WebApi.Domain.Organization;
using FSH.WebApi.Domain.People;

namespace FSH.WebApi.Application.Organization.Teams;

public class CreateTeamRequestValidator : CustomValidator<CreateTeamRequest>
{
    public CreateTeamRequestValidator(IReadRepository<Team> entityRepo, IReadRepository<SubDepartment> fatherRepo, IStringLocalizer<CreateTeamRequestValidator> t)
    {
        RuleFor(e => e.Code)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (code, ct) => await entityRepo.FirstOrDefaultAsync(new TeamByCodeSpec(code), ct) is null)
                .WithMessage((_, code) => t["Team with Code{0} already exists.", code]);

        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await entityRepo.FirstOrDefaultAsync(new TeamByNameSpec(name), ct) is null)
                .WithMessage((_, name) => t["Team with Name {0} already exists.", name]);

        RuleFor(e => e.SubDepartmentId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await fatherRepo.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => t["Department {0} Not Found.", id]);

    }
}

public class UpdateTeamRequestValidator : CustomValidator<UpdateTeamRequest>
{
    public UpdateTeamRequestValidator(IReadRepository<Team> entityRepo, IReadRepository<SubDepartment> fatherRepo, IStringLocalizer<UpdateTeamRequestValidator> t)
    {
        RuleFor(e => e.Code)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (entity, code, ct) =>
                    await entityRepo.FirstOrDefaultAsync(new TeamByCodeSpec(code), ct)
                        is not Team existingEntity || existingEntity.Id == entity.Id)
                .WithMessage((_, code) => t["Team {0} already Exists.", code]);

        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (entity, name, ct) =>
                    await entityRepo.FirstOrDefaultAsync(new TeamByNameSpec(name), ct)
                        is not Team existingEntity || existingEntity.Id == entity.Id)
                .WithMessage((_, name) => t["Team {0} already Exists.", name]);

        RuleFor(e => e.SubDepartmentId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await fatherRepo.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => t["Department {0} Not Found.", id]);
    }
}