using Microsoft.EntityFrameworkCore;
using Puroguramu.Domains;
using Puroguramu.Domains.Lesson.Data;
using Puroguramu.Infrastructures.Data;
using Puroguramu.Infrastructures.Data.Entities;

namespace Puroguramu.Infrastructures.Repository;

public class LessonRepository : IRepositoryLesson
{
    private readonly PuroguramuDbContext _dbAccess;

    public LessonRepository(PuroguramuDbContext dbAccess)
    {
        _dbAccess = dbAccess;
    }

    public async Task<List<ILesson>> AllLesson() => await _dbAccess.Lessons.Select(l => (ILesson)l).ToListAsync();

    public async Task<List<ILesson>> AllLessonPublish() => await _dbAccess.Lessons.Where(lesson => lesson.Publish == true).Select(l => (ILesson)l).ToListAsync();

    public async Task<ILesson?> GetLesson(Guid idLesson)
    {
        try
        {
            return await _dbAccess.Lessons.FirstAsync(l => l.IdLesson == idLesson);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<bool> AddLesson(string name, string description)
    {
        try
        {
            var lesson = new Lesson() { Name = name, Description = description };
            _dbAccess.Lessons.Add(lesson);
            await _dbAccess.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<List<IExercise>> GetExerciseOfLesson(Guid idLesson)
    {
        var listIdExercise = await _dbAccess.LessonsExercises
            .Where(le => le.IdLesson==idLesson)
            .Select(le => le.IdExercise)
            .ToListAsync();

        var listExercise = new List<IExercise>();

        var repository = new ExerciseRepository(_dbAccess);

        foreach (var exerciseId in listIdExercise)
        {
            var exercise = await repository.GetExercise(exerciseId);
            if (exercise != null)
            {
                listExercise.Add(exercise);
            }
        }

        return listExercise;
    }

    public async Task<List<IExercise>> GetExercisePublishOfLesson(Guid idLesson)
    {
        var listIdExercise = await _dbAccess.LessonsExercises
            .Where(le => le.IdLesson==idLesson)
            .Select(le => le.IdExercise)
            .ToListAsync();

        var listExercise = new List<IExercise>();

        var repository = new ExerciseRepository(_dbAccess);

        foreach (var exerciseId in listIdExercise)
        {
            var exercise = await repository.GetExercise(exerciseId);
            if (exercise != null && exercise.Publish)
            {
                listExercise.Add(exercise);
            }
        }

        return listExercise;
    }

    public async Task<int> GetNumberOfExercisesPublishOfLesson(Guid idLesson)
    {
        var listIdExercise = await _dbAccess.LessonsExercises
            .Where(le => le.IdLesson==idLesson)
            .Select(le => le.IdExercise)
            .ToListAsync();

        var nbExercise = 0;

        var repository = new ExerciseRepository(_dbAccess);

        foreach (var exerciseId in listIdExercise)
        {
            var exercise = await repository.GetExercise(exerciseId);
            if (exercise != null && exercise.Publish)
            {
                nbExercise++;
            }
        }

        return nbExercise;
    }

    public async Task<bool> IsUniqueName(string name) => (await _dbAccess.Lessons.Where(lesson => lesson.Name==name).ToListAsync()).Count == 0;

    public async Task<int> GetNumberOfStudentFinish(Guid idLesson)
    {
        var userRepository = new UserRepository(_dbAccess);

        var listStudent = await userRepository.GetNumberOfStudent();

        var nbrStudent = listStudent.Count;

        var listIdExercise = await _dbAccess.LessonsExercises
            .Where(le => le.IdLesson==idLesson)
            .Select(le => le.IdExercise)
            .ToListAsync();

        var repository = new ExerciseRepository(_dbAccess);
        foreach (var user in listStudent)
        {
            foreach (var exerciseId in listIdExercise)
            {
                var exercise = await repository.GetExerciseUser(exerciseId,user);
                if (exercise is not { IsFinished: true })
                {
                    nbrStudent--;
                    break;
                }
            }
        }

        return nbrStudent;
    }

    public async Task<bool> UpdateLessonName(Guid idLesson, string name)
    {
        var lesson = await GetLesson(idLesson);

        if (lesson == null)
        {
            return false;
        }

        lesson.Name = name;
        try
        {
            await _dbAccess.SaveChangesAsync();
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateLessonDescription(Guid idLesson, string description)
    {
        var lesson = await GetLesson(idLesson);

        if (lesson == null)
        {
            return false;
        }

        lesson.Description = description;
        try
        {
            await _dbAccess.SaveChangesAsync();
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateLessonPublish(Guid idLesson, bool publish)
    {
        var lesson = await GetLesson(idLesson);

        if (lesson == null)
        {
            return false;
        }

        lesson.Publish = publish;
        try
        {
            await _dbAccess.SaveChangesAsync();
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> DeleteLesson(Guid idLesson)
    {
        var exerciseRepository = new ExerciseRepository(_dbAccess);

        try
        {
            var lesson = await GetLesson(idLesson);

            if (lesson == null)
            {
                return false;
            }

            var listIdExercise = await GetExerciseOfLesson(idLesson);

            foreach (var exercise in listIdExercise)
            {
                await exerciseRepository.DeleteExerciseNoSave(exercise.IdExercise);
            }

            _dbAccess.Lessons.Remove((Lesson)lesson);
            await _dbAccess.SaveChangesAsync();
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }

    public async Task<int> GetNumberOfLessons() => await _dbAccess.Lessons.Where(lesson => lesson.Publish == true).Select(l => (ILesson)l).CountAsync();

    public async Task<int> GetNumberOfExercicesAllLessons()
    {
        var lessons = await AllLessonPublish();
        Console.WriteLine("lessons.count" + lessons.Count);

        var nbExercices = 0;

        foreach (var lesson in lessons)
        {
            var exs = await GetExercisePublishOfLesson(lesson.IdLesson);
            Console.WriteLine("exs" + exs.Count);
            nbExercices += exs.Count;
        }

        return nbExercices;
    }

    public void MoveExercise(Guid idLesson, Guid exercise, string direction)
    {


        throw new NotImplementedException();
    }
}
