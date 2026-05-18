using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Puroguramu.Infrastructures.Data.Entities;

namespace Puroguramu.Infrastructures.Data.Configuration;

public class LessonExerciseConfiguration : IEntityTypeConfiguration<LessonExercise>
{
    public void Configure(EntityTypeBuilder<LessonExercise> builder)
    {
        builder.HasKey(lessonExercice => new { lessonExercice.IdLesson, lessonExercice.IdExercise });
        SeedData(builder);
    }

    private void SeedData(EntityTypeBuilder<LessonExercise> builder)
    {
        var lessonExercise1 = new LessonExercise()
        {
            IdLesson = Guid.Parse("dfd5b0a5-40b3-41ec-8c0c-a44b8f0298f8"),
            IdExercise = Guid.Parse("89a9b82b-6961-4d00-95a1-7ac8412598c2"),
            Position = 0,
        };
        var lessonExercise2 = new LessonExercise()
        {
            IdLesson = Guid.Parse("dfd5b0a5-40b3-41ec-8c0c-a44b8f0298f8"),
            IdExercise = Guid.Parse("9d14ac8c-6776-4da3-aeed-2332588b9d31"),
            Position = 1,
        };
        var lessonExercise3 = new LessonExercise()
        {
            IdLesson = Guid.Parse("dfd5b0a5-40b3-41ec-8c0c-a44b8f0298f8"),
            IdExercise = Guid.Parse("9aac602c-f7e2-475e-8eeb-9be12c3c2536"),
            Position = 2,
        };
        builder.HasData(lessonExercise1,lessonExercise2,lessonExercise3);
    }
}
