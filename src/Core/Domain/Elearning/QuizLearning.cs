namespace FSH.WebApi.Domain.Elearning;
public class QuizLearning : AuditableEntity, IAggregateRoot
{
    public virtual Quiz Quiz { get; private set; } = default!;

    // public virtual ApplicationUser user { get; private set; }
    public DateTime StartTime { get; private set; }

    public DateTime EndTime { get; private set; }
}
