using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Puroguramu.Infrastructures.Data.Entities;

namespace Puroguramu.Infrastructures.Data.Configuration;

public class ExerciseUserConfiguration : IEntityTypeConfiguration<ExerciseUser>
{
    public void Configure(EntityTypeBuilder<ExerciseUser> builder)
    {
        builder.HasKey(exerciseUser => new { exerciseUser.IdExercise, exerciseUser.IdUser});
        builder.Property(exerciceUser => exerciceUser.IsFinished).IsRequired();
        builder.Property(exerciceUser => exerciceUser.Solution).IsRequired();
    }
}
