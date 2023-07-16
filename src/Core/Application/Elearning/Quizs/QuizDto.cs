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

    public DefaultIdType QuizTypeId { get; set; }
    public string QuizTypeName { get; set; } = default!;
    public DefaultIdType QuizTopicId { get; set; }
    public string QuizTopicName { get; set; } = default!;
    public DefaultIdType QuizModeId { get; set; }
    public string QuizModeName { get; set; } = default!;

    public decimal? Price { get; set; }
    public int? Sale { get; set; }
    public decimal? Rating { get; set; }
    public int? RatingCount { get; set; }
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

    public DefaultIdType QuizTypeId { get; set; }
    public string QuizTypeName { get; set; } = default!;
    public DefaultIdType QuizTopicId { get; set; }
    public string QuizTopicName { get; set; } = default!;
    public DefaultIdType QuizModeId { get; set; }
    public string QuizModeName { get; set; } = default!;

    public decimal? Price { get; set; }
    public int? Sale { get; set; }
    public decimal? Rating { get; set; }
    public int? RatingCount { get; set; }
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

    public DefaultIdType QuizTypeId { get; set; }
    public string QuizTypeName { get; set; } = default!;
    public DefaultIdType QuizTopicId { get; set; }
    public string QuizTopicName { get; set; } = default!;
    public DefaultIdType QuizModeId { get; set; }
    public string QuizModeName { get; set; } = default!;

    public decimal? Price { get; set; }
    public int? Sale { get; set; }
    public decimal? Rating { get; set; }
    public int? RatingCount { get; set; }
}
