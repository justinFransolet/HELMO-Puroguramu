using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Puroguramu.Domains;
using Puroguramu.Infrastructures.Data.Configuration;
using Puroguramu.Infrastructures.Data.Entities;

namespace Puroguramu.Infrastructures.Data;

public class PuroguramuDbContext : IdentityDbContext<IdentityPuroguramuUser>
{
    public PuroguramuDbContext(DbContextOptions options) : base(options)
    {
    }

    public override DbSet<IdentityPuroguramuUser> Users { get; set; }

    public DbSet<Exercise> Exercises { get; set; }

    public DbSet<Lesson> Lessons { get; set; }

    public DbSet<ExerciseUser> ExercisesUsers { get; set; }

    public DbSet<LessonExercise> LessonsExercises { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ExerciceConfiguration());
        modelBuilder.ApplyConfiguration(new LessonConfiguration());
        modelBuilder.ApplyConfiguration(new ExerciseUserConfiguration());
        modelBuilder.ApplyConfiguration(new LessonExerciseConfiguration());

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = UserRoleConfiguration.AdminId,
                UserId = UserConfiguration.JustinId,
            },
            new IdentityUserRole<string>
            {
                RoleId = UserRoleConfiguration.TeacherId,
                UserId = UserConfiguration.ClementId,
            },
            new IdentityUserRole<string>
            {
                RoleId = UserRoleConfiguration.StudentId,
                UserId = UserConfiguration.UserUnId,
            },
            new IdentityUserRole<string>
            {
                RoleId = UserRoleConfiguration.StudentId,
                UserId = UserConfiguration.UserDeuxId,
            },
            new IdentityUserRole<string>
            {
                RoleId = UserRoleConfiguration.TeacherId,
                UserId = UserConfiguration.TeacherId,
            });
    }
}
