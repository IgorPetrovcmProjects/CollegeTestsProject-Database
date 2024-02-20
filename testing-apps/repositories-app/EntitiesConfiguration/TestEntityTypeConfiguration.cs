namespace Repository_App.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repository_App.Entities;

public class TestEntityTypeConfiguration : IEntityTypeConfiguration<Test>
{
    public void Configure(EntityTypeBuilder<Test> builder)
    {
        builder.ToTable("tests");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .IsRequired()
            .HasField("id")
            .HasDefaultValue(Guid.NewGuid());

        builder
            .Property(x => x.Title)
            .IsRequired()
            .HasField("title");


        builder
            .Property(x => x.Description)
            .IsRequired()
            .HasField("description");
    }
}