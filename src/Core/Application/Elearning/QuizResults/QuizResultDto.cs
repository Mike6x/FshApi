using FSH.WebApi.Application.Elearning.Quizs;

namespace FSH.WebApi.Application.Elearning.QuizResults;
public class QuizResultDto : IDto
{
    public Guid Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    // Student Point, Used time, Time spent on taking the quiz
    public decimal Sp { get; set; }
    public decimal Ut { get; set; }
    public string Fut { get; set; } = default!;

    // Quiz Title, Gained Score, Passing score, Passing score in percent, Total score, Time limit
    public string Qt { get; set; } = default!;
    public decimal Tp { get; set; }
    public decimal Ps { get; set; }
    public decimal Psp { get; set; }
    public decimal Tl { get; set; }

    // Quiz version, type : Graded
    public string V { get; set; } = default!;
    public string T { get; set; } = default!;

    public Guid QuizId { get; set; }
    public string QuizName { get; set; } = default!;
}

public class QuizResultDetailsDto : IDto
{
    public Guid Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    // Student Point, time, Time spent on taking the quiz
    public decimal Sp { get; set; }
    public decimal Ut { get; set; }
    public string Fut { get; set; } = default!;

    // Quiz Title, Gained Score, Passing score, Passing score in percent, Total score, Time limit
    public string Qt { get; set; } = default!;
    public decimal Tp { get; set; }
    public decimal Ps { get; set; }
    public decimal Psp { get; set; }
    public decimal Tl { get; set; }

    // Quiz version, type : Graded
    public string V { get; set; } = default!;
    public string T { get; set; } = default!;

    // Quiz Info
    public QuizDto Quiz { get; set; } = default!;
}

public class QuizResultExportDto : IDto
{
    public Guid Id { get; set; }

    // Student Point, Used time, Time spent on taking the quiz
    public decimal Sp { get; set; }
    public decimal Ut { get; set; }
    public string Fut { get; set; } = default!;

    // Quiz Title, Gained Score, Passing score, Passing score in percent, Total score, Time limit
    public string Qt { get; set; } = default!;
    public decimal Tp { get; set; }
    public decimal Ps { get; set; }
    public decimal Psp { get; set; }
    public decimal Tl { get; set; }

    // Quiz version, type : Graded
    public string V { get; set; } = default!;
    public string T { get; set; } = default!;

    public string QuizName { get; set; } = default!;

    public Guid CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}