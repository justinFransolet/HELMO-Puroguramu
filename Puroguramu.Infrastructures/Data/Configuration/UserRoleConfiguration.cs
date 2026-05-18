using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Puroguramu.Infrastructures.Data.Configuration;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public static readonly string AdminId = DateTime.Parse("2024-04-01T10:30:00.0")+"AdminId";
    public static readonly string TeacherId =  DateTime.Parse("2024-04-01T10:30:00.0")+"TeacherId";
    public static readonly string StudentId = DateTime.Parse("2024-04-01T10:30:00.0")+"StudentId";

    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        var adminRole = new IdentityRole()
        {
            Id = AdminId, ConcurrencyStamp = AdminId, Name = "Admin", NormalizedName = "ADMIN",
        };

        var teacherRole = new IdentityRole()
        {
            Id = TeacherId, ConcurrencyStamp = TeacherId, Name = "Teacher", NormalizedName = "TEACHER",
        };

        var studentRole = new IdentityRole()
        {
            Id = StudentId, ConcurrencyStamp = StudentId, Name = "Student", NormalizedName = "STUDENT",
        };

        builder.HasData(adminRole, teacherRole, studentRole);
    }
}
