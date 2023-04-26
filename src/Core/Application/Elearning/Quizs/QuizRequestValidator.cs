using FSH.WebApi.Domain.Elearning;

namespace FSH.WebApi.Application.Elearning.Quizs;

public class CreateQuizRequestValidator : CustomValidator<CreateQuizRequest>
{
    public CreateQuizRequestValidator(IReadRepository<Quiz> quizRepo, IStringLocalizer<CreateQuizRequestValidator> T)
    {
        RuleFor(e => e.Code)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (code, ct) => await quizRepo.FirstOrDefaultAsync(new QuizByCodeSpec(code), ct) is null)
                .WithMessage((_, code) => T["Quiz with Code{0} already exists.", code]);

        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await quizRepo.FirstOrDefaultAsync(new QuizByNameSpec(name), ct) is null)
                .WithMessage((_, name) => T["Quiz with Name {0} already exists.", name]);

        RuleFor(e => e.QuizMedia);
    }
}

public class UpdateQuizRequestValidator : CustomValidator<UpdateQuizRequest>
{
    public UpdateQuizRequestValidator(IReadRepository<Quiz> quizRepo, IStringLocalizer<UpdateQuizRequestValidator> T)
    {
        RuleFor(e => e.Code)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (quiz, Code, ct) =>
                    await quizRepo.FirstOrDefaultAsync(new QuizByCodeSpec(Code), ct)
                        is not Quiz existingquiz || existingquiz.Id == quiz.Id)
                .WithMessage((_, Code) => T["Quiz {0} already Exists.", Code]);

        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (quiz, name, ct) =>
                    await quizRepo.FirstOrDefaultAsync(new QuizByNameSpec(name), ct)
                        is not Quiz existingquiz || existingquiz.Id == quiz.Id)
                .WithMessage((_, name) => T["Quiz {0} already Exists.", name]);

        RuleFor(e => e.QuizMedia);
    }
}