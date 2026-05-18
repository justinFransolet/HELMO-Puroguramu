namespace Puroguramu.Domains;

public interface IAssessExercise
{
    Task<ExerciseResult> Assess(IExercise exercise, string proposal);

    Task<ExerciseResult> StubForExercise(IExerciseUser exerciseUser);
}
