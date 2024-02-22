namespace Repository_App.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repository_App.Entities;

public class TestEntityTypeConfiguration : IEntityTypeConfiguration<Test>
{
    public void Configure(EntityTypeBuilder<Test> builder)
    {
        builder.ToTable("tests", "main_schema");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .IsRequired()
            .HasColumnName("id");

        builder
            .Property(x => x.Title)
            .IsRequired()
            .HasColumnName("title");


        builder
            .Property(x => x.Description)
            .HasColumnName("description");

        builder.
            Property(x => x.UserId)
            .IsRequired()
            .HasColumnName("user_id");
    }
}