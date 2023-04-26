namespace FSH.WebApi.Domain.Elearning;
public class QuizResult : AuditableEntity, IAggregateRoot
{
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }

    // Student Point, Used time, Time spent on taking the quiz
    public decimal Sp { get; private set; }
    public decimal Ut { get; private set; }
    public string Fut { get; private set; }

    // Quiz Title, Gained Score, Passing score, Passing score in percent, Total score, Time limit
    public string Qt { get; private set; } = default!;
    public decimal Tp { get; private set; }
    public decimal Ps { get; private set; }
    public decimal Psp { get; private set; }
    public decimal Tl { get; private set; }

    // Quiz version, type : Graded
    public string V { get; private set; } = "9.0";
    public string T { get; private set; } = nameof(QuizType.graded);

    // public virtual ApplicationUser user { get; private set; }
    public Guid QuizId { get; private set; }
    public virtual Quiz Quiz { get; private set; } = default!;

    public QuizResult(
        Guid quizId,
        DateTime startTime,
        DateTime endTime,
        decimal earnedPoints,
        decimal userTimeSpend,
        string timeTakingQuiz,
        string quizTitle,
        decimal gainedScore,
        decimal passingScore,
        decimal passingScoreInPercent,
        decimal timeLimit,
        string quizVersion,
        string quizType)
    {
        QuizId = quizId;

        StartTime = startTime;
        EndTime = endTime;

        Sp = earnedPoints;
        Ut = userTimeSpend;
        Fut = timeTakingQuiz;

        Qt = quizTitle;
        Tp = gainedScore;
        Ps = passingScore;
        Psp = passingScoreInPercent;
        Tl = timeLimit;

        V = quizVersion;
        T = quizType;
    }

    public QuizResult()
        : this(Guid.Empty, DateTime.UtcNow, DateTime.UtcNow, 0, 0, string.Empty,  string.Empty, 0, 0, 0, 0, string.Empty, string.Empty)
    {
    }

    public QuizResult Update(
        Guid? quizId,
        DateTime? startTime,
        DateTime? endTime,
        decimal? earnedPoints,
        decimal? userTimeSpend,
        string? timeTakingQuiz,
        string? quizTitle,
        decimal? gainedScore,
        decimal? passingScore,
        decimal? passingScoreInPercent,
        decimal? timeLimit,
        string? quizVersion,
        string? quizType)
    {
        if (quizId.HasValue && quizId.Value != Guid.Empty && !QuizId.Equals(quizId.Value)) QuizId = quizId.Value;
        if (startTime.HasValue && startTime.Value != DateTime.MinValue && !StartTime.Equals(startTime.Value)) StartTime = startTime.Value;
        if (endTime.HasValue && endTime.Value != DateTime.MinValue && !EndTime.Equals(endTime.Value)) EndTime = endTime.Value;

        if (earnedPoints.HasValue && Sp != earnedPoints) Sp = earnedPoints.Value;
        if (userTimeSpend.HasValue && Ut != userTimeSpend) Ut = userTimeSpend.Value;
        if (timeTakingQuiz is not null && Fut?.Equals(timeTakingQuiz) is not true) Fut = timeTakingQuiz;

        if (quizTitle is not null && Qt?.Equals(quizTitle) is not true) Qt = quizTitle;
        if (gainedScore.HasValue && Tp != gainedScore) Tp = gainedScore.Value;
        if (passingScore.HasValue && Ps != passingScore) Ps = passingScore.Value;
        if (passingScoreInPercent.HasValue && Psp != passingScore) Psp = passingScoreInPercent.Value;
        if (timeLimit.HasValue && Tl != timeLimit) Tl = timeLimit.Value;

        if (quizVersion is not null && V?.Equals(quizVersion) is not true) V = quizVersion;
        if (quizType is not null && T?.Equals(quizType) is not true) T = quizType;

        return this;
    }
}
