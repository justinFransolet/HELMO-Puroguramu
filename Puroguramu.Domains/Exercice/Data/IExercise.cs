namespace Puroguramu.Domains;

public interface IExercise
{
    Guid IdExercise { get; set; }

    string Name { get; set; }

    string States { get; set; }

    bool Publish { get; set; }

    string Difficulty { get; set; }

    string Model { get; set; }

    string Solution { get; set; }

    string InjectIntoTemplate(string code);

}
