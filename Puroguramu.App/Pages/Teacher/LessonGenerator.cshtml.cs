using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Puroguramu.Domains;

namespace Puroguramu.App.Pages.Teacher;

[Authorize(Roles = "Teacher")]
public class LessonGenerator : PageModel
{
    [BindProperty]
    [Required]
    [RegularExpression("^([a-zA-Z0-9]|é|è|ç| ){5,60}$",ErrorMessage = "Taille maximum = 60 caractères")]
    public string Name { get; set; } = string.Empty;

    private IRepositoryLesson _repositoryLesson;

    private ILogger<LessonGenerator> _logger;

    private static readonly Regex NameRegex = new Regex("^([a-zA-Z0-9]|é|è|ç| ){5,60}$");

    public LessonGenerator(IRepositoryLesson repositoryLesson, ILogger<LessonGenerator> logger)
    {
        _logger = logger;
        _repositoryLesson = repositoryLesson;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!string.IsNullOrEmpty(Name) && NameRegex.Match(Name).Success && await _repositoryLesson.IsUniqueName(Name))
        {
            if (! await _repositoryLesson.AddLesson(Name,string.Empty))
            {
                _logger.LogError("Impossible de créer la leçon");
                return Page();
            }

            _logger.LogInformation("Leçon créer");
            return RedirectToPage("/Teacher/TeacherDashBoard");
        }
        else
        {
            _logger.LogWarning("Vous ne respectez pas les règles pour créer la leçon.");
        }

        return Page();
    }
}
