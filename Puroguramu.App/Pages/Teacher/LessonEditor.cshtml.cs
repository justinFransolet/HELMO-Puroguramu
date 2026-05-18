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
public class LessonEditor : PageModel
{
    [BindProperty]
    public LessonEditorModel Input { get; set; } = new LessonEditorModel(string.Empty,string.Empty, false);

    public LessonEditorModel RealData { get; set; } = new LessonEditorModel(string.Empty,string.Empty, false);

    public List<TeacherExerciseDisplay> ExercisesList { get; set; } = new List<TeacherExerciseDisplay>();

    [BindProperty]
    public Guid IdLesson { get; set; } = Guid.Empty;

    private ILogger<LessonEditor> _logger;

    private IRepositoryLesson _repositoryLesson;
    private IRepositoryExercise _repositoryExercise;

    private static readonly Regex NameRegex = new Regex("^([a-zA-Z0-9]|é|è|ç| ){5,60}$");
    private static readonly Regex DescriptionRegex = new Regex("^([a-zA-Z0-9]|é|è|ç| ){1,500}$");

    public LessonEditor(ILogger<LessonEditor> logger,IRepositoryLesson repositoryLesson, IRepositoryExercise repositoryExercise)
    {
        _logger = logger;
        _repositoryLesson = repositoryLesson;
        _repositoryExercise = repositoryExercise;
    }

    public async Task<IActionResult> OnGetAsync(Guid idLesson)
    {
        var lesson = await _repositoryLesson.GetLesson(idLesson);
        if (lesson == null)
        {
            _logger.LogCritical("Impossible de retrouver la leçon");
            return StatusCode(503);
        }

        Input = new LessonEditorModel(lesson.Name, lesson.Description, lesson.Publish);
        RealData = new LessonEditorModel(lesson.Name, lesson.Description, lesson.Publish);
        AddExercises(idLesson);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!string.IsNullOrEmpty(Input.Name) && NameRegex.Match(Input.Name).Success && await _repositoryLesson.IsUniqueName(Input.Name))
        {
            if (! await _repositoryLesson.UpdateLessonName(IdLesson,Input.Name))
            {
                _logger.LogError("Impossible de modifier le nom");
                return StatusCode(503);
            }
        }
        else
        {
            _logger.LogWarning("Vous ne respectez pas les règles pour changer de nom.");
        }

        if (!string.IsNullOrEmpty(Input.Description) && DescriptionRegex.Match(Input.Description).Success)
        {
            if (! await _repositoryLesson.UpdateLessonDescription(IdLesson,Input.Description))
            {
                _logger.LogError("Impossible de modifier la description");
                return StatusCode(503);
            }
        }
        else
        {
            _logger.LogWarning("Vous ne respectez pas les règles pour changer de description.");
        }

        if (! await _repositoryLesson.UpdateLessonPublish(IdLesson,Input.Publish))
        {
            _logger.LogError("Impossible de modifier la visibilité");
            return StatusCode(503);
        }

        var lesson = await _repositoryLesson.GetLesson(IdLesson);
        if (lesson == null)
        {
            _logger.LogError("Impossible de retrouver la leçon");
            return StatusCode(503);
        }

        RealData = new LessonEditorModel(lesson.Name, lesson.Description, lesson.Publish);
        AddExercises(IdLesson);

        return Page();
    }

    private async void AddExercises(Guid idLesson)
    {
        var listExercise = await _repositoryLesson.GetExerciseOfLesson(idLesson);


        foreach (var exercise in listExercise)
        {
            var position = await _repositoryExercise.GetExercisePosition(exercise.IdExercise);
            ExercisesList.Add(new TeacherExerciseDisplay(exercise.Name,exercise.Difficulty,exercise.IdExercise, position));
        }
    }


    public async Task<IActionResult> OnPostMoveExerciseAsync(string direction, Guid exercise)
    {
        Console.WriteLine("move " + IdLesson + " " + exercise + " " + direction);

        await _repositoryExercise.MoveExercise(IdLesson, exercise, direction);


        // _repositoryLesson.MoveExercise(IdLesson, exercise, direction);
        return RedirectToPage("/Teacher/LessonEditor", new { idLesson = IdLesson });
    }



}

public record LessonEditorModel(
    [Required,RegularExpression("^([a-zA-Z0-9]|é|è|ç| ){5,60}$",ErrorMessage = "Taille maximum = 60 caractères")]string Name,
    [Required,RegularExpression("^([a-zA-Z0-9]|é|è|ç| ){1,500}$",ErrorMessage = "Taille maximum = 500 caractères")]string Description,
    [Required]bool Publish);
