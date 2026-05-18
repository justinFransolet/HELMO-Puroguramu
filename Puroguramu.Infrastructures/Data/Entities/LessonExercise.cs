using Puroguramu.Domains.Lesson.Data;

namespace Puroguramu.Infrastructures.Data.Entities;

public class LessonExercise : ILessonExercise
{
    public Guid IdLesson { get; set; }

    public Guid IdExercise { get; set; }
    
    public int Position { get; set; }
}
