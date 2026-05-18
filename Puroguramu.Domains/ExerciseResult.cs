using Puroguramu.Domains.Exercice;

namespace Puroguramu.Domains;

public record ExerciseResult(string Proposal, IEnumerable<TestResult>? TestResults = null)
{
    public IEnumerable<TestResult> TestResults { get; } = TestResults ?? Array.Empty<TestResult>();

    public ExerciseStatus Status
    {
        get =>
            !TestResults.Any()
                ? ExerciseStatus.Started
                : TestResults.Any(test => test.Status != TestStatus.Passed)
                    ? ExerciseStatus.Started
                    : ExerciseStatus.Passed;
        set => Status = value;
    }
}

public record TestResult(string Label, TestStatus Status, string ErrorMessage = "");


public enum TestStatus
{
    Inconclusive  = 0,
    Failed = 1,
    Passed = 2,
}

