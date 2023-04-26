using FSH.WebApi.Domain.Elearning;

namespace FSH.WebApi.Application.Elearning.Quizs;

public class QuizByIdSpec : Specification<Quiz, QuizDetailsDto>, ISingleResultSpecification<Quiz>
{
    public QuizByIdSpec(Guid id) =>
        Query
            .Where(e => e.Id == id);
}

public class QuizByCodeSpec : Specification<Quiz>, ISingleResultSpecification<Quiz>
{
    public QuizByCodeSpec(string code) =>
        Query
            .Where(e => e.Code == code);
}

public class QuizByNameSpec : Specification<Quiz>, ISingleResultSpecification<Quiz>
{
    public QuizByNameSpec(string name) =>
        Query
            .Where(e => e.Name == name);
}