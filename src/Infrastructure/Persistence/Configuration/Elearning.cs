using Finbuckle.MultiTenant.EntityFrameworkCore;
using FSH.WebApi.Domain.Elearning;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSH.WebApi.Infrastructure.Persistence.Configuration;

public class QuizConfig : IEntityTypeConfiguration<Quiz>
{
    public void Configure(EntityTypeBuilder<Quiz> builder)
    {
        builder
            .ToTable("Quizs", SchemaNames.Elearning)
            .IsMultiTenant();
    }
}

public class QuizResultConfig : IEntityTypeConfiguration<QuizResult>
{
    public void Configure(EntityTypeBuilder<QuizResult> builder)
    {
        builder
            .ToTable("QuizResults", SchemaNames.Elearning)
            .IsMultiTenant();
    }
}