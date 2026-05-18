using Puroguramu.Domains.Exercice;
using Puroguramu.Domains.Lesson;
using Xunit;

namespace Puroguramu.Domains.Tests;

public class PresentationModelTests
{
    [Fact]
    public void TestResultViewModel_ShouldExposeUnderlyingValues()
    {
        var result = new TestResult("Check sum", TestStatus.Failed, "expected 2, got 3");
        var viewModel = new TestResultViewModel(result);

        Assert.Equal("Failed", viewModel.Status);
        Assert.Equal("Check sum", viewModel.Label);
        Assert.True(viewModel.HasError);
        Assert.Equal("expected 2, got 3", viewModel.ErrorMessage);
    }

    [Fact]
    public void LessonDisplay_RecordsUseValueEquality()
    {
        var id = Guid.NewGuid();
        var first = new LessonDisplay("Lesson", id, 1, 3);
        var second = new LessonDisplay("Lesson", id, 1, 3);

        Assert.Equal(first, second);
    }

    [Fact]
    public void ExerciseDisplay_PreservesTheProvidedExerciseInstance()
    {
        var exercise = new FakeExercise
        {
            IdExercise = Guid.NewGuid(),
            Name = "Exercise",
            States = "public class Exercise {}",
            Publish = true,
            Difficulty = "Easy",
            Model = "model",
            Solution = "solution",
        };

        var display = new ExerciseDisplay(exercise, true, 2);

        Assert.Same(exercise, display.Exercise);
        Assert.True(display.IsFinish);
        Assert.Equal(2, display.Position);
    }

    private sealed class FakeExercise : IExercise
    {
        public Guid IdExercise { get; set; }

        public string Name { get; set; } = string.Empty;

        public string States { get; set; } = string.Empty;

        public bool Publish { get; set; }

        public string Difficulty { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public string Solution { get; set; } = string.Empty;

        public string InjectIntoTemplate(string code) => code;
    }
}

