using Puroguramu.Domains;

namespace Puroguramu.Infrastructures.Data.Entities;

public class Exercise : IExercise
{
    public Guid IdExercise { get; set; }

    public string Name { get; set; } = string.Empty;

    public string States { get; set; } = string.Empty;

    public string Difficulty { get; set; } = string.Empty;

    public bool Publish { get; set; } = false;

    public string Model { get; set; } = string.Empty;

    public string Solution { get; set; } = string.Empty;

    public string InjectIntoTemplate(string code)
        => Model.Replace("// code-insertion-point", code);
}
