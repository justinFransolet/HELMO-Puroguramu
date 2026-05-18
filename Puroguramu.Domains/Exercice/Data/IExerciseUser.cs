namespace Puroguramu.Domains;

public interface IExerciseUser
{
    Guid IdExercise { get; set; }

    string IdUser { get; set; }

    bool IsFinished { get; set; }

    string Solution { get; set; }
}
