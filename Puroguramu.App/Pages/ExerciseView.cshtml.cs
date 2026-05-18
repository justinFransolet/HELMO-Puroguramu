using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Puroguramu.Domains;
using Puroguramu.Domains.Exercice;
using Puroguramu.Domains.Exercice.Data;
using Puroguramu.Infrastructures.Data.Entities;

namespace Puroguramu.App.Pages;

public class ExerciseViewModel : PageModel
{
    private readonly IAssessExercise _assessor;

    private readonly ILogger<ExerciseViewModel> _logger;

    private IRepositoryExercise _repositoryExercise;

    private ExerciseResult? _result = new ExerciseResult(string.Empty);

    [BindProperty]
    public string Proposal { get; set; } = string.Empty;

    [BindProperty]
    public Guid IdExercise { get; set; } = Guid.Empty;

    [BindProperty]
    public string Name { get; set; } = string.Empty;

    [BindProperty]
    public string States { get; set; } = string.Empty;

    [BindProperty]
    public Exercise Exercice { get; set; } = new Exercise();

    public string ExerciseResultStatus
        => _result?.Status switch
        {
            ExerciseStatus.NotStarted => "Not Started",
            ExerciseStatus.Started => "Started",
            ExerciseStatus.Passed => "Succeeded",
            ExerciseStatus.Failed => "Failed",
            _ => "Unknown"
        };

    public IEnumerable<TestResultViewModel> TestResult
        => _result
            ?.TestResults
            .Select(result => new TestResultViewModel(result)) ?? Array.Empty<TestResultViewModel>();

    private readonly UserManager<IdentityPuroguramuUser> _userManager;

    public ExerciseViewModel(ILogger<ExerciseViewModel> logger,IAssessExercise assessor, IRepositoryExercise repositoryExercise, UserManager<IdentityPuroguramuUser> userManager)
    {

        _assessor = assessor;
        _repositoryExercise = repositoryExercise;
        _logger = logger;
        _userManager = userManager;
    }

    public async Task<IActionResult> OnGetAsync(Guid idExercise)
    {

        var currentUser = await _userManager.GetUserAsync(User);
        if (currentUser == null)
        {
            return StatusCode(418);
        }

        Exercice = (Exercise?) await _repositoryExercise.GetExercise(idExercise);



        var exerciseUser = await _repositoryExercise.GetExerciseUser(idExercise, currentUser.Matricule);
        if (Exercice == null || exerciseUser == null)
        {
            _logger.LogCritical("Exercise or ExerciseUser are null");
            return StatusCode(418);
        }

        _result = await _assessor.StubForExercise(exerciseUser);

        _logger.LogInformation("Exercise found");

        //Public data
        IdExercise = Exercice.IdExercise;
        Name = Exercice.Name;
        States = Exercice.States;
        Proposal = exerciseUser.Solution;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var currentUser = await _userManager.GetUserAsync(User);
        if (currentUser == null)
        {
            return StatusCode(418);
        }

        Exercice = (Exercise?)await _repositoryExercise.GetExercise(IdExercise);

        var exerciseUser = await _repositoryExercise.GetExerciseUser(IdExercise, currentUser.Matricule);

        if (Exercice == null || exerciseUser == null)
        {
            _logger.LogCritical("Exercise or ExerciseUser are null");
            return StatusCode(418);
        }

        _result = await _assessor.Assess(Exercice, Proposal);

        if (!await _repositoryExercise.ChangeFinishStatusExercise(exerciseUser, _result.Status == ExerciseStatus.Passed))
        {
            _logger.LogCritical("Impossible de changer le statut");
            return StatusCode(503);
        }

        if (!await _repositoryExercise.UpdateExerciseSolution(exerciseUser,Proposal))
        {
            _logger.LogCritical("Impossible de changer la solution");
            return StatusCode(503);
        }

        if (!await _repositoryExercise.UpdateExerciseSolution(exerciseUser,Proposal))
        {
            _logger.LogCritical("Impossible de changer la solution");
            return StatusCode(503);
        }

        return Page();
    }
}
