using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

using Puroguramu.Domains;
using Puroguramu.Domains.Exercice;


namespace Puroguramu.Infrastructures.Roslyn;

public class RoslynAssessor : IAssessExercise
{
    private static readonly ScriptOptions Options = ScriptOptions.Default
        .WithImports("System", "System.Linq", "Puroguramu.Domains")
        .WithReferences("System.Core","Puroguramu.Domains");

    public async Task<ExerciseResult> Assess(IExercise exercise, string proposal)
    {
        var codeToRun = exercise.InjectIntoTemplate(proposal);
        try
        {
            ScriptState<TestResult[]> run = await CSharpScript.RunAsync<TestResult[]>(
                codeToRun,
                Options);

            return new ExerciseResult(proposal, run.ReturnValue);
        }
        catch (CompilationErrorException ex)
        {
            Console.WriteLine("excompilation " + ex.Message);
            return new ExerciseResult(proposal,
                ex.Diagnostics.Select(d => new TestResult("Compilation Error", TestStatus.Inconclusive, d.ToString())));
        }
    }

    public async Task<ExerciseResult> StubForExercise(IExerciseUser exerciseUser) => await Task.FromResult(new ExerciseResult(exerciseUser.Solution));
}
