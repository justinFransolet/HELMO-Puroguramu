namespace Puroguramu.Domains.Lesson;

public record LessonTeacherDisplay(
    string Name,
    Guid IdLesson,
    int NbrStudentFinish,
    int NbrStudent
);
