using FSH.WebApi.Domain.Elearning;

namespace FSH.WebApi.Application.Elearning.Quizs;
public class QuizDto : IDto
{
    public Guid Id { get; set; }

    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? QuizPath { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsActive { get; set; }
    public QuizType QuizType { get; set; }
    public QuizTopic QuizTopic { get; set; }
}

public class QuizDetailsDto : IDto
{
    public Guid Id { get; set; }

    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? QuizPath { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsActive { get; set; }
    public QuizType QuizType { get; set; }
    public QuizTopic QuizTopic { get; set; }
}

public class QuizExportDto : IDto
{
    public Guid Id { get; set; }

    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string? QuizPath { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsActive { get; set; }
    public QuizType QuizType { get; set; }
    public QuizTopic QuizTopic { get; set; }
}
