using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Puroguramu.Infrastructures.Data.Entities;

namespace Puroguramu.Infrastructures.Data.Configuration;

public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.HasKey(lesson => lesson.IdLesson);
        builder.Property(lesson => lesson.Name).IsRequired();
        builder.Property(lesson => lesson.Description).IsRequired();
        SeedData(builder);
    }

    private void SeedData(EntityTypeBuilder<Lesson> builder)
    {
        var lesson1 = new Lesson()
        {
            IdLesson = Guid.Parse("dfd5b0a5-40b3-41ec-8c0c-a44b8f0298f8"),
            Description = "Description",
            Name = "Leçon 1",
            Publish = true,
        };
        builder.HasData(lesson1);
    }
}
