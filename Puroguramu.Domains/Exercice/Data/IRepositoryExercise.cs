namespace Puroguramu.Domains.Exercice.Data;

public interface IRepositoryExercise
{
    Task<bool> AddExercise(Guid idLesson,string name,string states,string difficulty,string model, string solution,bool publish);

    Task<IExercise?> GetExercise(Guid idExercise);

    Task<Guid?> GetLessonOfExercise(Guid idExercise);

    Task<bool> IsUniqueName(Guid idLesson, string name);

    Task<bool> SetExerciseToLesson(Guid idLesson,Guid idExercise);

    Task<IExerciseUser?> GetExerciseUser(Guid idExercise, string idUser);

    Task<bool> AddExerciseUser(Guid idExercise, string idUser);

    Task<bool> ChangeFinishStatusExercise(IExerciseUser exerciseUser,bool status);

    Task<bool> UpdateExerciseSolution(IExerciseUser exerciseUser, string solution);

    Task<bool> DeleteExercise(Guid idExercise);

    Task<bool> UpdateName(Guid idExercise,string name);

    Task<bool> UpdateState(Guid idExercise,string state);

    Task<bool> UpdatePublish(Guid idExercise,bool publish);

    Task<bool> UpdateModel(Guid idExercise,string model);

    Task<bool> UpdateSolution(Guid idExercise,string solution);

    Task<bool> UpdateDifficulty(Guid idExercise,string difficulty);

    Task<bool> MoveExercise(Guid idLesson, Guid exercise, string direction);

    Task<int> GetExercisePosition(Guid idExercise);
}
