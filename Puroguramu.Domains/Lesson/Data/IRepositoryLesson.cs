using Puroguramu.Domains.Lesson.Data;

namespace Puroguramu.Domains;

public interface IRepositoryLesson
{
    Task<List<ILesson>> AllLesson();

    Task<List<ILesson>> AllLessonPublish();

    Task<ILesson?> GetLesson(Guid idLesson);

    Task<bool> AddLesson(string name, string description);

    Task<List<IExercise>> GetExerciseOfLesson(Guid idLesson);

    Task<List<IExercise>> GetExercisePublishOfLesson(Guid idLesson);

    Task<bool> IsUniqueName(string name);

    Task<int> GetNumberOfStudentFinish(Guid idLesson);

    Task<bool> UpdateLessonName(Guid idLesson,string name);

    Task<bool> UpdateLessonDescription(Guid idLesson,string description);

    Task<bool> UpdateLessonPublish(Guid idLesson,bool publish);

    Task<bool> DeleteLesson(Guid idLesson);

    Task<int> GetNumberOfLessons();

    Task<int> GetNumberOfExercicesAllLessons();

    void MoveExercise(Guid idLesson, Guid exercise, string direction);
}
