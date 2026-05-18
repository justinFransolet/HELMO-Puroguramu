using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Puroguramu.Domains;

namespace Puroguramu.App.Pages.Teacher;

[Authorize(Roles = "Teacher")]
public class LessonSuppressor : PageModel
{
    private IRepositoryLesson _repositoryLesson;

    private ILogger<LessonSuppressor> _logger;

    [BindProperty]
    public string Name { get; set; } = string.Empty;

    [BindProperty]
    public Guid IdLesson { get; set; } = Guid.Empty;

    [BindProperty]
    public bool Confirm { get; set; }

    public LessonSuppressor(IRepositoryLesson repositoryLesson,ILogger<LessonSuppressor> logger)
    {
        _repositoryLesson = repositoryLesson;
        _logger = logger;
    }

    public async Task<IActionResult> OnGetAsync(Guid idLesson)
    {
        var lesson = await _repositoryLesson.GetLesson(idLesson);

        if (lesson == null)
        {
            _logger.LogError("Impossible de trouver la leçon");
            return StatusCode(418);
        }

        IdLesson = idLesson;
        Name = lesson.Name;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var lesson = await _repositoryLesson.GetLesson(IdLesson);

        if (lesson == null)
        {
            _logger.LogError("Impossible de trouver la leçon");
            return StatusCode(418);
        }

        if (Confirm)
        {
            if (await _repositoryLesson.DeleteLesson(IdLesson))
            {
                _logger.LogInformation("Suppression de la leçon");
                return RedirectToPage("/Teacher/TeacherDashBoard");
            }
            else
            {
                _logger.LogError("Impossible de supprimer la leçon");
            }
        }
        else
        {
            _logger.LogWarning("Confirmation non validé");
        }

        return Page();
    }
}
