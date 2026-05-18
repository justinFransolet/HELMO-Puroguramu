namespace Puroguramu.Domains.Exercice;

public record TeacherExerciseDisplay(
    string Name,
    string Difficulty,
    Guid IdExercise,
    int Position
    );
