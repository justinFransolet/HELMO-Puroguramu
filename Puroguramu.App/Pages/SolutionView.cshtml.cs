using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Puroguramu.Domains.Exercice.Data;

namespace Puroguramu.App.Pages;

public class SolutionView : PageModel
{
    public Guid IdExercise { get; set; } = Guid.Empty;

    public string Solution { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    private IRepositoryExercise _repositoryExercise;

    private ILogger<SolutionView> _logger;

    public SolutionView(ILogger<SolutionView> logger, IRepositoryExercise repositoryExercise)
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

        Solution = exercise.Solution;
        IdExercise = idExercise;
        Name = exercise.Name;

        return Page();
    }
}
