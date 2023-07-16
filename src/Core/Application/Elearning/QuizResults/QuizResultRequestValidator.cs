using FSH.WebApi.Domain.Elearning;

namespace FSH.WebApi.Application.Elearning.QuizResults;

public class CreateQuizResultRequestValidator : CustomValidator<CreateQuizResultRequest>
{
    public CreateQuizResultRequestValidator(
        IReadRepository<Quiz> quizRepo,
        IStringLocalizer<CreateQuizResultRequestValidator> T)
    {
        RuleFor(e => e.Sp)
            .GreaterThanOrEqualTo(1);
        RuleFor(e => e.Ps)
            .GreaterThanOrEqualTo(1);
        RuleFor(e => e.Psp)
            .GreaterThanOrEqualTo(1);
        RuleFor(e => e.Tp)
            .GreaterThanOrEqualTo(0);

        RuleFor(e => e.QuizId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await quizRepo.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => T["Quiz {0} Not Found.", id]);
    }
}

public class UpdateQuizResultRequestValidator : CustomValidator<UpdateQuizResultRequest>
{
    public UpdateQuizResultRequestValidator(
        IReadRepository<QuizResult> quizResultRepo,
        IReadRepository<Quiz> quizRepo,
        IStringLocalizer<UpdateQuizResultRequestValidator> T)
    {
        RuleFor(e => e.Id)
            .NotEmpty()
            .MustAsync(async (quizResult, id, ct) =>
                    await quizResultRepo.FirstOrDefaultAsync(new QuizResultByIdWithQuizSpec(id), ct)
                        is not null)
                .WithMessage((_, id) => T["QuizResult {0} already Exists.", id]);

            // .MustAsync(async (quizResult, id, ct) =>
            // await quizResultRepo.FirstOrDefaultAsync(new QuizResultByIdWithQuizSpec(id), ct)
            //    is not QuizResult existingQuizResult || existingQuizResult.Id == quizResult.Id)
            // .WithMessage((_, id) => T["QuizResult {0} already Exists.", id]);

        RuleFor(e => e.Sp)
            .GreaterThanOrEqualTo(1);
        RuleFor(e => e.Ps)
            .GreaterThanOrEqualTo(1);
        RuleFor(e => e.Psp)
            .GreaterThanOrEqualTo(1);
        RuleFor(e => e.Tp)
            .GreaterThanOrEqualTo(0);

        RuleFor(e => e.QuizId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await quizRepo.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => T["Quiz {0} Not Found.", id]);
    }
}