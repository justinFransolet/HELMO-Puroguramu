using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Puroguramu.Infrastructures.Data.Entities;

namespace Puroguramu.Infrastructures.Data.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<IdentityPuroguramuUser>
{
    public static readonly string JustinId =  "A000001";
    public static readonly string ClementId = "A000002";
    public static readonly string UserUnId = "A000003";
    public static readonly string UserDeuxId = "A000004";
    public static readonly string TeacherId = "T000001";

    public void Configure(EntityTypeBuilder<IdentityPuroguramuUser> builder)
    {
        builder.Property(user => user.Email).IsRequired().IsUnicode();
        builder.Property(user => user.UserName).HasMaxLength(100).IsUnicode().IsRequired();
        builder.Property(user => user.PasswordHash).IsRequired();
        builder.Property(user => user.Group).IsRequired();
        SeedData(builder);
    }

    private void SeedData(EntityTypeBuilder<IdentityPuroguramuUser> builder)
    {
        var ph = new PasswordHasher<IdentityPuroguramuUser>();

        var justin = new IdentityPuroguramuUser()
        {
            Id = JustinId,
            Matricule = "A000001",
            LastName = "Fransolet",
            FirstName = "Justin",
            UserName = "justin.fransolet@gmail.com",
            NormalizedUserName = "justin.fransolet@gmail.com".ToUpper(),
            Group = "2i1",
            Email = "justin.fransolet@gmail.com",
        };
        justin.PasswordHash = ph.HashPassword(justin, "admin");

        var clement = new IdentityPuroguramuUser()
        {
            Id = ClementId,
            Matricule = "A000002",
            LastName = "Lemlijn",
            FirstName = "Clement",
            UserName = "clement.lemlijn@gmail.com",
            NormalizedUserName = "clement.lemlijn@gmail.com".ToUpper(),
            Group = "2i1",
            Email = "clement.lemlijn@gmail.com",
        };
        clement.PasswordHash = ph.HashPassword(clement, "admin");

        var userUn = new IdentityPuroguramuUser()
        {
            Id = UserUnId,
            Matricule = "A000003",
            LastName = "Un",
            FirstName = "User1",
            UserName = "user.un@gmail.com",
            NormalizedUserName = "user.un@gmail.com".ToUpper(),
            Group = "2i1",
            Email = "user.un@gmail.com",
        };
        userUn.PasswordHash = ph.HashPassword(userUn, "admin");

        var userDeux = new IdentityPuroguramuUser()
        {
            Id = UserDeuxId,
            Matricule = "A000004",
            LastName = "Deux",
            FirstName = "User2",
            UserName = "user.deux@gmail.com",
            NormalizedUserName = "user.deux@gmail.com".ToUpper(),
            Group = "2i1",
            Email = "user.deux@gmail.com",
        };
        userDeux.PasswordHash = ph.HashPassword(userDeux, "admin");

        var teacher = new IdentityPuroguramuUser()
        {
            Id = TeacherId,
            Matricule = "T000001",
            LastName = "Mr",
            FirstName = "Teacher",
            UserName = "mr.teacher@gmail.com",
            NormalizedUserName = "mr.teacher@gmail.com".ToUpper(),
            Group = "2i1",
            Email = "mr.teacher@gmail.com",
        };
        teacher.PasswordHash = ph.HashPassword(teacher, "Test12$");

        builder.HasData(justin, clement, userUn, userDeux, teacher);
    }
}
