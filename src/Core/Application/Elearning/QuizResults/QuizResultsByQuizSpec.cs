using FSH.WebApi.Domain.Elearning;

namespace FSH.WebApi.Application.Elearning.QuizResults;

public class QuizResultsByQuizSpec : Specification<QuizResult>
{
    public QuizResultsByQuizSpec(Guid quizId) =>
        Query.Where(e => e.QuizId == quizId);
}
