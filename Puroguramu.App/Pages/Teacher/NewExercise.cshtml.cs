using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Puroguramu.Domains;
using Puroguramu.Domains.Exercice.Data;

namespace Puroguramu.App.Pages.Teacher;

public class NewExercise : PageModel
{
    [BindProperty]
    [Required]
    [RegularExpression("^([a-zA-Z0-9]|é|è|ç| ){5,60}$",ErrorMessage = "Taille maximum = 60 caractères")]
    public string Name { get; set; } = string.Empty;

    [BindProperty]
    public Guid IdLesson { get; set; } = Guid.Empty;

    private IRepositoryExercise _repositoryExercise;

    private IRepositoryLesson _repositoryLesson;

    private ILogger<LessonGenerator> _logger;

    private static readonly Regex NameRegex = new Regex("^([a-zA-Z0-9]|é|è|ç| ){5,60}$");

    public NewExercise(IRepositoryLesson repositoryLesson,IRepositoryExercise repositoryExercise, ILogger<LessonGenerator> logger)
    {
        _logger = logger;
        _repositoryExercise = repositoryExercise;
        _repositoryLesson = repositoryLesson;
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
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!string.IsNullOrEmpty(Name) && NameRegex.Match(Name).Success && await _repositoryExercise.IsUniqueName(IdLesson,Name))
        {
            if (! await _repositoryExercise.AddExercise(IdLesson,Name,"Pas de description","Undefined","Pas de code","Pas de solution",false))
            {
                _logger.LogError("Impossible de créer l'exercice");
                return Page();
            }

            _logger.LogInformation("Exercice créer");
            return RedirectToPage("/Teacher/TeacherDashBoard");
        }
        else
        {
            _logger.LogWarning("Vous ne respectez pas les règles pour créer l'exercice.");
        }

        return Page();
    }
}
