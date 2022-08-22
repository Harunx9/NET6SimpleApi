using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimpleApi.Model.Base;

public class TaskConfiguration : IEntityTypeConfiguration<TaskEntity>
{
    public void Configure(EntityTypeBuilder<TaskEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasQueryFilter(x => x.IsDeleted == false);
        builder.Property(x => x.Version).IsRequired();
        builder.Property(x => x.Title).HasMaxLength(250);
        builder.Property(x => x.Desctiption).HasMaxLength(512);
    }
}