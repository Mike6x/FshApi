using FSH.WebApi.Domain.Settings;

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

    public DefaultIdType QuizTypeId { get; private set; }
    public virtual Dimension QuizType { get; private set; } = default!;
    public DefaultIdType QuizTopicId { get; private set; }
    public virtual Dimension QuizTopic { get; private set; } = default!;
    public DefaultIdType QuizModeId { get; private set; }
    public virtual Dimension QuizMode { get; private set; } = default!;

    public decimal? Price { get; private set; }
    public int? Sale { get; private set; }
    public int? RatingCount { get; private set; }
    public decimal? Rating { get; private set; }

    public Quiz(
        string code,
        string name,
        string description,
        string quizPath,
        DateTime? startTime,
        DateTime? endTime,
        bool isActive,
        DefaultIdType quizTypeId,
        DefaultIdType quizTopicId,
        DefaultIdType quizModeId,
        decimal? price,
        int? sale,
        int? ratingCount,
        decimal? rating)
    {
        Code = code;
        Name = name;
        Description = description;
        QuizPath = quizPath;
        StartTime = startTime ?? DateTime.MinValue;
        EndTime = endTime ?? DateTime.UtcNow;
        IsActive = endTime >= DateTime.Today && isActive;
        QuizTypeId = quizTypeId;
        QuizTopicId = quizTopicId;
        QuizModeId = quizModeId;
        Sale = sale;
        Price = price;
        RatingCount = ratingCount;
        Rating = rating;
    }

    public Quiz()
    : this(string.Empty, string.Empty, string.Empty, string.Empty, DateTime.MinValue, DateTime.UtcNow, false, DefaultIdType.Empty, DefaultIdType.Empty, DefaultIdType.Empty, null, null, null, null)
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
    DefaultIdType? quizTypeId,
    DefaultIdType? quizTopicId,
    DefaultIdType? quizModeId,
    decimal? price,
    int? sale,
    int? ratingCount,
    decimal? rating)
    {
        if (code is not null && Code?.Equals(code) is not true) Code = code;
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (quizPath is not null && QuizPath?.Equals(quizPath) is not true) QuizPath = quizPath;
        if (startTime.HasValue && startTime.Value != DateTime.MinValue && !StartTime.Equals(startTime.Value)) StartTime = startTime.Value;
        if (endTime.HasValue && endTime.Value != DateTime.MinValue && !EndTime.Equals(endTime.Value)) EndTime = endTime.Value;

        IsActive = EndTime >= DateTime.Today && isActive;

        if (quizTypeId.HasValue && quizTypeId.Value != DefaultIdType.Empty && !QuizTypeId.Equals(quizTypeId.Value)) QuizTypeId = quizTypeId.Value;
        if (quizTopicId.HasValue && quizTopicId.Value != DefaultIdType.Empty && !QuizTopicId.Equals(quizTopicId.Value)) QuizTopicId = quizTopicId.Value;
        if (quizModeId.HasValue && quizModeId.Value != DefaultIdType.Empty && !QuizModeId.Equals(quizModeId.Value)) QuizModeId = quizModeId.Value;

        if (sale.HasValue && sale != Sale) Sale = sale;
        if (price.HasValue && price != Price) Price = price;
        if (ratingCount.HasValue && ratingCount != RatingCount) RatingCount = ratingCount;
        if (rating.HasValue && rating != Rating) Rating = rating;

        return this;
    }

    public Quiz ClearQuizPath()
    {
        QuizPath = string.Empty;
        return this;
    }
}
