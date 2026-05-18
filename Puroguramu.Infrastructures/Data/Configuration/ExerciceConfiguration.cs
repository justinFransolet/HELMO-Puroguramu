using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Puroguramu.Infrastructures.Data.Entities;

namespace Puroguramu.Infrastructures.Data.Configuration;

public class ExerciceConfiguration : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.HasKey(exercise => exercise.IdExercise);
        builder.Property(exercise => exercise.Name).IsRequired();
        builder.Property(exercise => exercise.Model).IsRequired();
        builder.Property(exercise => exercise.Solution).IsRequired();
        builder.Property(exercice => exercice.Difficulty).IsRequired();
        builder.Property(exercice => exercice.States).IsRequired();
        SeedData(builder);
    }

    private void SeedData(EntityTypeBuilder<Exercise> builder)
    {
        var exercise1 = new Exercise()
        {
         IdExercise = Guid.Parse("89a9b82b-6961-4d00-95a1-7ac8412598c2"),
         Name = "Exercice 1",
         Difficulty = "Medium",
         States = "Explication",
         Publish = true,
         Model = @"// code-insertion-point

            public class Test
            {
                public static TestResult Ensure(float b, int exponent, float expected)
                {
                  TestStatus status = TestStatus.Passed;
                  float actual = float.NaN;
                  try
                  {
                     actual = Exercice.Power(b, exponent);
                     if(Math.Abs(actual - expected) > 0.00001f)
                     {
                         status = TestStatus.Failed;
                     }
                  }
                  catch(Exception ex)
                  {
                     status = TestStatus.Inconclusive;
                  }

                  return new TestResult(
                    string.Format(""Power of {0} by {1} should be {2}"", b, exponent, expected),
                    status,
                    status == TestStatus.Passed ? string.Empty : string.Format(""Expected {0}. Got {1}."", expected, actual)
                  );
                }
            }

            return new TestResult[] {
              Test.Ensure(2, 4, 16.0f),
              Test.Ensure(2, -4, 1.0f/16.0f)
            };
            ",
         Solution = "Solution",
        };
        var exercise2 = new Exercise()
        {
            IdExercise = Guid.Parse("9d14ac8c-6776-4da3-aeed-2332588b9d31"),
            Name = "Exercice 2",
            Difficulty = "Easy",
            States = "Explication",
            Publish = true,
            Model = "Model n'existe pas",
            Solution = "Solution",
        };
        var exercise3 = new Exercise()
        {
            IdExercise = Guid.Parse("9aac602c-f7e2-475e-8eeb-9be12c3c2536"),
            Name = "Exercice 3",
            Difficulty = "Difficult",
            States = "Explication",
            Publish = true,
            Model = "Model n'existe pas",
            Solution = "Solution",
        };
        builder.HasData(exercise1,exercise2,exercise3);
    }
}
