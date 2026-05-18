using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Puroguramu.Infrastructures.Migrations
{
    public partial class production : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Matricule = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Group = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilePicture = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    IdExercise = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    States = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Difficulty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Publish = table.Column<bool>(type: "bit", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Solution = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.IdExercise);
                });

            migrationBuilder.CreateTable(
                name: "ExercisesUsers",
                columns: table => new
                {
                    IdExercise = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUser = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsFinished = table.Column<bool>(type: "bit", nullable: false),
                    Solution = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExercisesUsers", x => new { x.IdExercise, x.IdUser });
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    IdLesson = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Publish = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.IdLesson);
                });

            migrationBuilder.CreateTable(
                name: "LessonsExercises",
                columns: table => new
                {
                    IdLesson = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdExercise = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonsExercises", x => new { x.IdLesson, x.IdExercise });
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "01-04-24 10:30:00AdminId", "01-04-24 10:30:00AdminId", "Admin", "ADMIN" },
                    { "01-04-24 10:30:00StudentId", "01-04-24 10:30:00StudentId", "Student", "STUDENT" },
                    { "01-04-24 10:30:00TeacherId", "01-04-24 10:30:00TeacherId", "Teacher", "TEACHER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "Group", "LastName", "LockoutEnabled", "LockoutEnd", "Matricule", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "A000001", 0, "dc01b8e9-0d15-43aa-94fa-6ae2345c5528", "justin.fransolet@gmail.com", false, "Justin", "2i1", "Fransolet", false, null, "A000001", null, "JUSTIN.FRANSOLET@GMAIL.COM", "AQAAAAEAACcQAAAAEGQV36zm6YB594B8ooNp3ak9yuP5cx9H3z5+VIA4T5szkoH4Wi2Ejd1oUtL9ce2B+A==", null, false, new byte[0], "6165bea8-a6d3-4440-99b6-35272ce0ddca", false, "justin.fransolet@gmail.com" },
                    { "A000002", 0, "f7fdf403-f4ee-424f-8817-bdeed360785b", "clement.lemlijn@gmail.com", false, "Clement", "2i1", "Lemlijn", false, null, "A000002", null, "CLEMENT.LEMLIJN@GMAIL.COM", "AQAAAAEAACcQAAAAEHJezVaCNVVWKXR+Vo9TcpfsKA0CzS8B+R57VlF8hUIaijX+1PA5T9LhlmTNgLIWwA==", null, false, new byte[0], "4865e2e6-6054-4041-b8e7-2425fca031ef", false, "clement.lemlijn@gmail.com" },
                    { "A000003", 0, "31fda27d-a24a-4ffc-be17-8ecfd748c283", "user.un@gmail.com", false, "User1", "2i1", "Un", false, null, "A000003", null, "USER.UN@GMAIL.COM", "AQAAAAEAACcQAAAAEMYG2ASkd1mscplrP6YrgNgrGrabmhNpGeQ7qR0lNjqRJMgPM/D1NLD5X2KDQOVsiw==", null, false, new byte[0], "1dbb64ba-578d-40dd-94b4-1709e27789e6", false, "user.un@gmail.com" },
                    { "A000004", 0, "d1ecb401-3a59-41df-ba7d-0673c65d06cc", "user.deux@gmail.com", false, "User2", "2i1", "Deux", false, null, "A000004", null, "USER.DEUX@GMAIL.COM", "AQAAAAEAACcQAAAAEPR5DP9kvz+i96wzipaZcP6yubAdhdshZvN0Hcdjn3iczqhqnQND/AYIhWbUirgIUw==", null, false, new byte[0], "9052189a-385d-41a9-9ea8-5e5856db0c12", false, "user.deux@gmail.com" },
                    { "T000001", 0, "074f29df-dff7-41ce-9dd9-e163c8b3209d", "mr.teacher@gmail.com", false, "Teacher", "2i1", "Mr", false, null, "T000001", null, "MR.TEACHER@GMAIL.COM", "AQAAAAEAACcQAAAAEOwMAS0+/PkumGzujxq830xXnqzxw4TByBhEJqEZmYzjA/qJR2ffVExAdLis84Mg/Q==", null, false, new byte[0], "e678c994-3020-4de1-b14b-8ca215d0d4a1", false, "mr.teacher@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "IdExercise", "Difficulty", "Model", "Name", "Publish", "Solution", "States" },
                values: new object[,]
                {
                    { new Guid("89a9b82b-6961-4d00-95a1-7ac8412598c2"), "Medium", "// code-insertion-point\r\n\r\n            public class Test\r\n            {\r\n                public static TestResult Ensure(float b, int exponent, float expected)\r\n                {\r\n                  TestStatus status = TestStatus.Passed;\r\n                  float actual = float.NaN;\r\n                  try\r\n                  {\r\n                     actual = Exercice.Power(b, exponent);\r\n                     if(Math.Abs(actual - expected) > 0.00001f)\r\n                     {\r\n                         status = TestStatus.Failed;\r\n                     }\r\n                  }\r\n                  catch(Exception ex)\r\n                  {\r\n                     status = TestStatus.Inconclusive;\r\n                  }\r\n\r\n                  return new TestResult(\r\n                    string.Format(\"Power of {0} by {1} should be {2}\", b, exponent, expected),\r\n                    status,\r\n                    status == TestStatus.Passed ? string.Empty : string.Format(\"Expected {0}. Got {1}.\", expected, actual)\r\n                  );\r\n                }\r\n            }\r\n\r\n            return new TestResult[] {\r\n              Test.Ensure(2, 4, 16.0f),\r\n              Test.Ensure(2, -4, 1.0f/16.0f)\r\n            };\r\n            ", "Exercice 1", true, "Solution", "Explication" },
                    { new Guid("9aac602c-f7e2-475e-8eeb-9be12c3c2536"), "Difficult", "Model n'existe pas", "Exercice 3", true, "Solution", "Explication" },
                    { new Guid("9d14ac8c-6776-4da3-aeed-2332588b9d31"), "Easy", "Model n'existe pas", "Exercice 2", true, "Solution", "Explication" }
                });

            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "IdLesson", "Description", "Name", "Publish" },
                values: new object[] { new Guid("dfd5b0a5-40b3-41ec-8c0c-a44b8f0298f8"), "Description", "Leçon 1", true });

            migrationBuilder.InsertData(
                table: "LessonsExercises",
                columns: new[] { "IdExercise", "IdLesson", "Position" },
                values: new object[,]
                {
                    { new Guid("89a9b82b-6961-4d00-95a1-7ac8412598c2"), new Guid("dfd5b0a5-40b3-41ec-8c0c-a44b8f0298f8"), 0 },
                    { new Guid("9aac602c-f7e2-475e-8eeb-9be12c3c2536"), new Guid("dfd5b0a5-40b3-41ec-8c0c-a44b8f0298f8"), 2 },
                    { new Guid("9d14ac8c-6776-4da3-aeed-2332588b9d31"), new Guid("dfd5b0a5-40b3-41ec-8c0c-a44b8f0298f8"), 1 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "01-04-24 10:30:00AdminId", "A000001" },
                    { "01-04-24 10:30:00TeacherId", "A000002" },
                    { "01-04-24 10:30:00StudentId", "A000003" },
                    { "01-04-24 10:30:00StudentId", "A000004" },
                    { "01-04-24 10:30:00TeacherId", "T000001" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "ExercisesUsers");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "LessonsExercises");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
