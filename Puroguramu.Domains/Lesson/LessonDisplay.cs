namespace Puroguramu.Domains.Lesson;

public record LessonDisplay(
    string Name,
    Guid IdLesson,
    int NbrExerciseFinish,
    int NbrExercise
    );
