using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Puroguramu.Domains;
using Puroguramu.Domains.Lesson;
using Puroguramu.Domains.User.Data;

namespace Puroguramu.App.Pages.Teacher;

[Authorize(Roles = "Teacher")]
public class TeacherDashBoard : PageModel
{
    private readonly IRepositoryLesson _repositoryLesson;

    private readonly IUserRepository _repositoryUser;

    [BindProperty]
    public List<LessonTeacherDisplay> LessonList { get; set; } = new List<LessonTeacherDisplay>();

    public TeacherDashBoard(IUserRepository repositoryUser,IRepositoryLesson repositoryLesson)
    {
        _repositoryUser = repositoryUser;
        _repositoryLesson = repositoryLesson;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var listLesson = await _repositoryLesson.AllLesson();
        foreach (var lesson in listLesson)
        {
            LessonList.Add(new LessonTeacherDisplay(lesson.Name, lesson.IdLesson, await _repositoryLesson.GetNumberOfStudentFinish(lesson.IdLesson),
                 (await _repositoryUser.GetNumberOfStudent()).Count));
        }

        return Page();
    }
}
