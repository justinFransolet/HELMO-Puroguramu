using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Puroguramu.Domains;
using Puroguramu.Domains.Exercice.Data;

namespace Puroguramu.App.Pages.Teacher;

[Authorize(Roles = "Teacher")]
public class ExerciseSuppressor : PageModel
{
    private IRepositoryExercise _repositoryExercise;

    private ILogger<LessonSuppressor> _logger;

    [BindProperty]
    public string Name { get; set; } = string.Empty;

    [BindProperty]
    public Guid IdExercise { get; set; } = Guid.Empty;

    [BindProperty]
    public bool Confirm { get; set; }

    public ExerciseSuppressor(IRepositoryExercise repositoryExercise,ILogger<LessonSuppressor> logger)
    {
        _repositoryExercise = repositoryExercise;
        _logger = logger;
    }

    public async Task<IActionResult> OnGetAsync(Guid idExercise)
    {
        var exercise = await _repositoryExercise.GetExercise(idExercise);

        if (exercise == null)
        {
            _logger.LogError("Impossible de trouver l'exercice");
            return StatusCode(418);
        }

        IdExercise = idExercise;
        Name = exercise.Name;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var exercise = await _repositoryExercise.GetExercise(IdExercise);

        if (exercise == null)
        {
            _logger.LogError("Impossible de trouver l'exercice");
            return StatusCode(418);
        }

        if (Confirm)
        {
            if (await _repositoryExercise.DeleteExercise(IdExercise))
            {
                _logger.LogInformation("Suppression de l'exercice");
                return RedirectToPage("/Teacher/TeacherDashBoard");
            }
            else
            {
                _logger.LogError("Impossible de supprimer l'exercice");
            }
        }
        else
        {
            _logger.LogWarning("Confirmation non validé");
        }

        return Page();
    }
}
