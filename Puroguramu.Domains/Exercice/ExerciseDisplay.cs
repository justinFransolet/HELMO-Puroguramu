namespace Puroguramu.Domains.Exercice;

public record ExerciseDisplay(
    IExercise Exercise,
    bool IsFinish,
    int Position
    );
