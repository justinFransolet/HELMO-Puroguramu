using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Puroguramu.Domains;
using Puroguramu.Domains.Exercice;
using Puroguramu.Domains.Exercice.Data;

namespace Puroguramu.App.Pages.Teacher;

[Authorize(Roles = "Teacher")]
public class ExerciseEditor : PageModel
{
    [BindProperty]
    public TeacherExerciseEditor Input { get; set; } = new TeacherExerciseEditor(string.Empty, string.Empty, string.Empty,false, string.Empty, string.Empty);

    [BindProperty]
    public Guid IdExercise { get; set; } = Guid.Empty;

    public Guid IdLesson { get; set; } = Guid.Empty;

    private ExerciseResult _result = new ExerciseResult(string.Empty);

    public IEnumerable<TestResultViewModel> TestResult
        => _result
            ?.TestResults
            .Select(result => new TestResultViewModel(result)) ?? Array.Empty<TestResultViewModel>();

    public bool ResultError { get; set; } = false;

    public TeacherExerciseEditor ValidData { get; set; } = new TeacherExerciseEditor(string.Empty, string.Empty,string.Empty, false, string.Empty, string.Empty);

    private IRepositoryExercise _repositoryExercise;

    private IAssessExercise _assessor;

    private ILogger<ExerciseEditor> _logger;

    private static readonly Regex NameRegex = new Regex("^([a-zA-Z0-9]|é|è|ç| ){5,60}$");

    private static readonly Regex StateRegex = new Regex("^([a-zA-Z0-9]|é|è|ç| ){1,500}$");

    public ExerciseEditor(ILogger<ExerciseEditor> logger, IRepositoryExercise repositoryExercise,IAssessExercise assessExercise)
    {
        _assessor = assessExercise;
        _logger = logger;
        _repositoryExercise = repositoryExercise;
    }

    public async Task<IActionResult> OnGetAsync(Guid idExercise)
    {
        var exercise = await _repositoryExercise.GetExercise(idExercise);

        var lesson = await _repositoryExercise.GetLessonOfExercise(idExercise);

        if (exercise == null || lesson == null)
        {
            _logger.LogError("Impossible de trouver l'exercice");
            return StatusCode(418);
        }

        Input = new TeacherExerciseEditor(exercise.Name, exercise.States, exercise.Difficulty,exercise.Publish, exercise.Model, exercise.Solution);

        ValidData = new TeacherExerciseEditor(exercise.Name, exercise.States, exercise.Difficulty, exercise.Publish, exercise.Model, exercise.Solution);

        IdExercise = idExercise;

        IdLesson = (Guid)lesson;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var exercise = await _repositoryExercise.GetExercise(IdExercise);

        var lesson = await _repositoryExercise.GetLessonOfExercise(IdExercise);

        if (exercise == null || lesson == null)
        {
            _logger.LogError("Impossible de trouver l'exercice");
            return StatusCode(418);
        }

        if (Input.Name != exercise.Name)
        {
            if (!string.IsNullOrEmpty(Input.Name) && NameRegex.Match(Input.Name).Success && await _repositoryExercise.IsUniqueName(IdLesson,Input.Name))
            {
                if (!await _repositoryExercise.UpdateName(IdExercise, Input.Name))
                {
                    _logger.LogError("Impossible de changer le nom de l'exercice");
                    return StatusCode(503);
                }

                _logger.LogInformation("Changement de nom de l'exercice");
            }
            else
            {
                _logger.LogWarning("Le nom ne respecte pas les règles pour la modification");
            }
        }

        if (Input.State != exercise.States)
        {
            if (!string.IsNullOrEmpty(Input.State) && StateRegex.Match(Input.State).Success)
            {
                if (!await _repositoryExercise.UpdateState(IdExercise, Input.State))
                {
                    _logger.LogError("Impossible de changer l'énoncé de l'exercice");
                    return StatusCode(503);
                }

                _logger.LogInformation("Changement de l'énoncé de l'exercice");
            }
            else
            {
                _logger.LogWarning("L'énoncé ne respecte pas les règles pour la modification");
            }
        }

        if (Input.Difficulty != exercise.Difficulty)
        {
            if (!await _repositoryExercise.UpdateDifficulty(IdExercise, Input.Difficulty))
            {
                _logger.LogError("Impossible de changer la difficulté de l'exercice");
                return StatusCode(503);
            }

            _logger.LogInformation("Changement de la difficulté de l'exercice");
        }

        if (Input.Publish != exercise.Publish)
        {
            if (!await _repositoryExercise.UpdatePublish(IdExercise, Input.Publish))
            {
                _logger.LogError("Impossible de changer le statut de l'exercice");
                return StatusCode(503);
            }

            _logger.LogInformation("Changement de statut de l'exercice");
        }

        if (Input.Model != exercise.Model)
        {
            if (!await _repositoryExercise.UpdateModel(IdExercise, Input.Model))
            {
                _logger.LogError("Impossible de changer le modèle de l'exercice");
                return StatusCode(503);
            }

            _logger.LogInformation("Changement du modèle de l'exercice");
        }

        if (Input.Solution != exercise.Solution)
        {
            if (await IsSolutionValid(IdExercise, Input.Solution))
            {
                if (!await _repositoryExercise.UpdateSolution(IdExercise, Input.Solution))
                {
                    _logger.LogError("Impossible de changer la solution de l'exercice");
                    return StatusCode(503);
                }

                _logger.LogInformation("Changement de la solution de l'exercice");
            }
            else
            {
                _logger.LogWarning("La solution n'est pas correcte");
            }
        }

        Input = new TeacherExerciseEditor(exercise.Name, exercise.States, exercise.Difficulty,exercise.Publish, exercise.Model, exercise.Solution);

        ValidData = new TeacherExerciseEditor(exercise.Name, exercise.States,exercise.Difficulty, exercise.Publish, exercise.Model, exercise.Solution);

        IdLesson = (Guid)lesson;

        return Page();
    }

    private async Task<bool> IsSolutionValid(Guid idExercise, string inputSolution)
    {
        var exercise = await _repositoryExercise.GetExercise(IdExercise);

        if (exercise == null)
        {
            _logger.LogError("Impossible de trouver l'exercice");
            return false;
        }

        _result = await _assessor.Assess(exercise,inputSolution);

        foreach (var result in _result.TestResults)
        {
            if (result.Status != TestStatus.Passed)
            {
                _logger.LogWarning("Le code de la solution n'est pas juste");
                ResultError = true;
                return false;
            }
        }

        return true;
    }
}

public record TeacherExerciseEditor(
    [Required, RegularExpression("^([a-zA-Z0-9]|é|è|ç| ){5,60}$", ErrorMessage = "Taille maximum = 60 caractères")]
    string Name,
    [Required, RegularExpression("^([a-zA-Z0-9]|é|è|ç| ){1,500}$", ErrorMessage = "Taille maximum = 500 caractères")]
    string State,
    [Required] string Difficulty,
    [Required]bool Publish,
    [Required]string Model,
    [Required]string Solution);
