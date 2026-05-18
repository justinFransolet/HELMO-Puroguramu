using Puroguramu.Domains;

namespace Puroguramu.Infrastructures.Data.Entities;

public class ExerciseUser : IExerciseUser
{
    public Guid IdExercise { get; set; }

    public string IdUser { get; set; }

    public bool IsFinished { get; set; } = false;

    public string Solution { get; set; } = string.Empty;
}
