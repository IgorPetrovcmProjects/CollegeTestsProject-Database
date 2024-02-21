namespace Repository_App.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repository_App.Entities;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users", "main_schema");

        builder.HasKey(x => x.Id);

        builder
            .HasMany(x => x.Tests)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);

        builder
            .Property(x => x.Login)
            .HasColumnName("login")
            .HasMaxLength(18);

        builder
            .Property(x => x.Id)
            .IsRequired()
            .HasColumnName("id");

        builder
            .Property(x => x.Password)
            .IsRequired()
            .HasColumnName("password")
            .HasMaxLength(18);

        builder 
            .Property(x => x.Email)
            .HasColumnName("email");
    }
}