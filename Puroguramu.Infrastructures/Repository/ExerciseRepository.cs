using Microsoft.EntityFrameworkCore;
using Puroguramu.Domains;
using Puroguramu.Domains.Exercice.Data;
using Puroguramu.Infrastructures.Data;
using Puroguramu.Infrastructures.Data.Entities;

namespace Puroguramu.Infrastructures.Repository;

public class ExerciseRepository : IRepositoryExercise
{
    private readonly PuroguramuDbContext _dbAccess;

    public ExerciseRepository(PuroguramuDbContext dbAccess)
    {
        _dbAccess = dbAccess;
    }

    public async Task<bool> AddExercise(Guid idLesson, string name, string states, string difficulty, string model, string solution,bool publish)
    {
        try
        {
            var exercise = new Exercise()
            {
                IdExercise = Guid.NewGuid(),Name = name, States = states, Difficulty = difficulty, Model = model, Solution = solution,Publish = publish,
            };

            Console.WriteLine("next position " + getNextPosition(idLesson));

            var exerciseLesson = new LessonExercise()
            {
                IdExercise = exercise.IdExercise,
                IdLesson = idLesson,
                Position = getNextPosition(idLesson),
            };
            _dbAccess.Exercises.Add(exercise);
            _dbAccess.LessonsExercises.Add(exerciseLesson);
            await _dbAccess.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Add exo fail : " + e);
            return false;
        }
    }

    private int getNextPosition(Guid idLesson)
    {
        var highestPosition = _dbAccess.LessonsExercises
            .Where(le => le.IdLesson == idLesson)
            .Max(le => (int?)le.Position) ?? 0;

        return highestPosition + 1;
    }

    public async Task<IExercise?> GetExercise(Guid idExercise)
    {
        try
        {
            return await _dbAccess.Exercises.FirstAsync(ex => ex.IdExercise == idExercise);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<Guid?> GetLessonOfExercise(Guid idExercise)
    {
        try
        {
            var exerciseLesson = await _dbAccess.LessonsExercises.FirstAsync(ex => ex.IdExercise == idExercise);
            return exerciseLesson.IdLesson;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<bool> IsUniqueName(Guid idLesson, string name)
    {
        var lessonRepository = new LessonRepository(_dbAccess);

        var listIdExercise = await lessonRepository.GetExerciseOfLesson(idLesson);

        foreach (var exercise in listIdExercise)
        {
            if (exercise.Name == name)
            {
                return false;
            }
        }

        return true;
    }

    public async Task<bool> SetExerciseToLesson(Guid idLesson, Guid idExercise)
    {
        try
        {
            await _dbAccess.LessonsExercises.FirstAsync(le => le.IdExercise == idExercise && le.IdLesson == idLesson);

            var lessonExercise = new LessonExercise()
                {
                    IdLesson = idLesson,
                    IdExercise = idExercise,
                };
            _dbAccess.LessonsExercises.Add(lessonExercise);
            await _dbAccess.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<IExerciseUser?> GetExerciseUser(Guid idExercise, string idUser)
    {
        try
        {
            return await _dbAccess.ExercisesUsers.FirstAsync(eu => eu.IdExercise == idExercise && eu.IdUser == idUser);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<bool> AddExerciseUser(Guid idExercise, string idUser)
    {
        try
        {
            var exerciseUser = new ExerciseUser()
            {
                IdExercise = idExercise,
                IdUser = idUser,
                IsFinished = false,
                Solution =  @"public class Exercice
{
    // Tapez votre code ici
}
",
            };
            _dbAccess.ExercisesUsers.Add(exerciseUser);
            await _dbAccess.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> ChangeFinishStatusExercise(IExerciseUser exerciseUser, bool status)
    {
        try
        {
            exerciseUser.IsFinished = status;

            await _dbAccess.SaveChangesAsync();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> UpdateExerciseSolution(IExerciseUser exerciseUser, string solution)
    {
        try
        {
            exerciseUser.Solution = solution;

            await _dbAccess.SaveChangesAsync();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> DeleteExercise(Guid idExercise)
    {
        try
        {
            if (!await DeleteExerciseNoSave(idExercise))
            {
                return false;
            }

            await _dbAccess.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> UpdateName(Guid idExercise, string name)
    {
        var exercise = await GetExercise(idExercise);

        if (exercise == null)
        {
            return false;
        }

        exercise.Name = name;
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

    public async Task<bool> UpdateState(Guid idExercise, string state)
    {
        var exercise = await GetExercise(idExercise);

        if (exercise == null)
        {
            return false;
        }

        exercise.States = state;
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

    public async Task<bool> UpdatePublish(Guid idExercise, bool publish)
    {
        var exercise = await GetExercise(idExercise);

        if (exercise == null)
        {
            return false;
        }

        exercise.Publish = publish;
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

    public async Task<bool> UpdateModel(Guid idExercise, string model)
    {
        var exercise = await GetExercise(idExercise);

        if (exercise == null)
        {
            return false;
        }

        exercise.Model = model;
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

    public async Task<bool> UpdateSolution(Guid idExercise, string solution)
    {
        var exercise = await GetExercise(idExercise);

        if (exercise == null)
        {
            return false;
        }

        exercise.Solution = solution;
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

    public async Task<bool> UpdateDifficulty(Guid idExercise, string difficulty)
    {
        var exercise = await GetExercise(idExercise);

        if (exercise == null)
        {
            return false;
        }

        exercise.Difficulty = difficulty;
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

    public async Task<bool> MoveExercise(Guid idLesson, Guid exercise, string direction)
    {


        // Set l'id de l'exercice actuel à -1
        var currentExercice = _dbAccess.LessonsExercises.FirstOrDefault(le => le.IdLesson == idLesson && le.IdExercise == exercise);
        if(currentExercice == null)
        {
            return false;
        }

        if (direction == "up")
        {
            if(currentExercice.Position <= 0)
            {
                return false;
            }

            var previousExo = _dbAccess.LessonsExercises.First(le => le.IdLesson == idLesson && le.Position == currentExercice.Position - 1);

            currentExercice.Position--;
            previousExo.Position++;

        }

        if (direction == "down")
        {
            if(currentExercice.Position >= _dbAccess.LessonsExercises.Count(le => le.IdLesson == idLesson) - 1)
            {
                return false;
            }

            var nextExo = _dbAccess.LessonsExercises.First(le => le.IdLesson == idLesson && le.Position == currentExercice.Position + 1);

            currentExercice.Position++;
            nextExo.Position--;

        }



        await _dbAccess.SaveChangesAsync();

        return true;


    }

    public Task<int> GetExercisePosition() => throw new NotImplementedException();

    public async Task<int> GetExercisePosition(Guid IdExercice) => await _dbAccess.LessonsExercises.FirstAsync(le => le.IdExercise == IdExercice).ContinueWith(task => task.Result.Position);

    private async Task<List<ExerciseUser>> GetAllExerciseUser(Guid idExercise) => await _dbAccess.ExercisesUsers.Where(exerciseUser => exerciseUser.IdExercise == idExercise).ToListAsync();

    public async Task<bool> DeleteExerciseNoSave(Guid idExercise)
    {
        try
        {
            var exercise = await GetExercise(idExercise);
            if (exercise == null)
            {
                return false;
            }

            var listExerciseUser = await GetAllExerciseUser(idExercise);
            foreach (var exerciseUser in listExerciseUser)
            {
                _dbAccess.ExercisesUsers.Remove(exerciseUser);
            }

            _dbAccess.Exercises.Remove((Exercise)exercise);
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }
}
