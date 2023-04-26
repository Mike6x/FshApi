using FSH.WebApi.Domain.Elearning;

namespace FSH.WebApi.Application.Elearning.QuizResults;

public class QuizResultByIdWithQuizSpec : Specification<QuizResult, QuizResultDetailsDto>, ISingleResultSpecification<QuizResult>
{
    public QuizResultByIdWithQuizSpec(Guid id) =>
        Query
            .Where(e => e.Id == id)
            .Include(e => e.Quiz);
}

public class QuizResultByNameSpec : Specification<QuizResult>, ISingleResultSpecification<QuizResult>
{
    public QuizResultByNameSpec(string name) =>
        Query.Where(e => e.Quiz.Name == name);
}