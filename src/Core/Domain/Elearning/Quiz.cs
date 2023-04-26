using FSH.WebApi.Domain.Catalog;

namespace FSH.WebApi.Domain.Elearning;
public class Quiz : AuditableEntity, IAggregateRoot
{
    public string Code { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = string.Empty;
    public string QuizPath { get; private set; } = string.Empty;
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public bool IsActive { get; private set; }
    public QuizType QuizType { get; private set; } = QuizType.graded;
    public QuizTopic QuizTopic { get; private set; } = QuizTopic.General;

    public Quiz(
        string code,
        string name,
        string description,
        string quizPath,
        DateTime? startTime,
        DateTime? endTime,
        bool isActive,
        QuizType quizType,
        QuizTopic quizTopic)
    {
        Code = code;
        Name = name;
        Description = description;
        QuizPath = quizPath;
        StartTime = startTime ?? DateTime.MinValue;
        EndTime = endTime ?? DateTime.UtcNow;
        IsActive = endTime >= DateTime.Today && isActive;
        QuizType = quizType;
        QuizTopic = quizTopic;
    }

    public Quiz()
    : this(string.Empty, string.Empty, string.Empty, string.Empty, DateTime.MinValue, DateTime.UtcNow, false, QuizType.graded, QuizTopic.General)
    {
    }

    public Quiz Update(
    string? code,
    string? name,
    string? description,
    string? quizPath,
    DateTime? startTime,
    DateTime? endTime,
    bool isActive,
    QuizType? quizType,
    QuizTopic? quizTopic)
    {
        if (code is not null && Code?.Equals(code) is not true) Code = code;
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (quizPath is not null && QuizPath?.Equals(quizPath) is not true) QuizPath = quizPath;
        if (startTime.HasValue && startTime.Value != DateTime.MinValue && !StartTime.Equals(startTime.Value)) StartTime = startTime.Value;
        if (endTime.HasValue && endTime.Value != DateTime.MinValue && !EndTime.Equals(endTime.Value)) EndTime = endTime.Value;

        IsActive = EndTime >= DateTime.Today && isActive;

        if (quizType is not null && !QuizType.Equals(quizType)) QuizType = (QuizType)quizType;
        if (quizTopic is not null && QuizTopic.Equals(quizTopic) is not true) QuizTopic = (QuizTopic)quizTopic;

        return this;
    }

    public Quiz ClearQuizPath()
    {
        QuizPath = string.Empty;
        return this;
    }
}
