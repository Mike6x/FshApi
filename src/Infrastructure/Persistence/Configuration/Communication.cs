using Finbuckle.MultiTenant.EntityFrameworkCore;
using FSH.WebApi.Infrastructure.Chat;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSH.WebApi.Infrastructure.Persistence.Configuration;

public class ChatMessageConfig : IEntityTypeConfiguration<ChatMessage>
{
    public void Configure(EntityTypeBuilder<ChatMessage> builder)
    {
        builder
            .ToTable("ChatMessages", SchemaNames.Communication)
            .IsMultiTenant();
    }
}