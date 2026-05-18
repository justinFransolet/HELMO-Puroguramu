using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Puroguramu.Domains;
using Puroguramu.Domains.Exercice;
using Puroguramu.Domains.Exercice.Data;
using Puroguramu.Domains.Lesson;
using Puroguramu.Infrastructures.Data.Entities;

namespace Puroguramu.App.Pages;

public class LessonView : PageModel
{
    public LessonDetailDisplay DataLesson { get; set; } = new LessonDetailDisplay(string.Empty, string.Empty);

    public List<ExerciseDisplay> ExercisesList { get; set; } = new List<ExerciseDisplay>();

    private readonly IRepositoryLesson _repositoryLesson;

    private readonly IRepositoryExercise _repositoryExercise;

    private readonly UserManager<IdentityPuroguramuUser> _userManager;

    public LessonView(IRepositoryExercise repositoryExercise,IRepositoryLesson repositoryLesson, UserManager<IdentityPuroguramuUser> userManager)
    {
        _repositoryLesson = repositoryLesson;
        _repositoryExercise = repositoryExercise;
        _userManager = userManager;
    }

    public async Task<IActionResult> OnGetAsync(Guid idLesson)
    {
        var currentUser = await _userManager.GetUserAsync(User);
        if (currentUser == null)
        {
            return StatusCode(418);
        }

        var lesson = await _repositoryLesson.GetLesson(idLesson);
        if (lesson == null)
        {
            return StatusCode(418);
        }

        DataLesson = new LessonDetailDisplay(lesson.Name,lesson.Description);
        var listExercise = _repositoryLesson.GetExercisePublishOfLesson(idLesson);
        foreach (var exercise in listExercise.Result)
        {
            var exerciseStatut = _repositoryExercise.GetExerciseUser(exercise.IdExercise, currentUser.Matricule);
            if (exerciseStatut.Result == null)
            {
                await _repositoryExercise.AddExerciseUser(exercise.IdExercise, currentUser.Matricule);
                exerciseStatut = _repositoryExercise.GetExerciseUser(exercise.IdExercise, currentUser.Matricule);
            }

            var position = await _repositoryExercise.GetExercisePosition(exercise.IdExercise);
            ExercisesList.Add(new ExerciseDisplay(exercise,exerciseStatut.Result!.IsFinished, position));
        }

        return Page();
    }
}
