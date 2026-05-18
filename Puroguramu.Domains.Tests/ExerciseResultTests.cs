using Puroguramu.Domains.Exercice;
using Xunit;

namespace Puroguramu.Domains.Tests;

public class ExerciseResultTests
{
    [Fact]
    public void EmptyTestResults_DefaultToStartedAndEmptyCollection()
    {
        var result = new ExerciseResult("proposal");

        Assert.Equal(ExerciseStatus.Started, result.Status);
        Assert.Empty(result.TestResults);
    }

    [Fact]
    public void AllPassedTests_ReportsPassed()
    {
        var result = new ExerciseResult("proposal", new[]
        {
            new TestResult("test-1", TestStatus.Passed),
            new TestResult("test-2", TestStatus.Passed),
        });

        Assert.Equal(ExerciseStatus.Passed, result.Status);
    }

    [Theory]
    [InlineData(TestStatus.Failed)]
    [InlineData(TestStatus.Inconclusive)]
    public void AnyNonPassedTest_ReportsFailed(TestStatus status)
    {
        var result = new ExerciseResult("proposal", new[]
        {
            new TestResult("test-1", status, "boom"),
        });

        Assert.Equal(ExerciseStatus.Started, result.Status);
    }
}

