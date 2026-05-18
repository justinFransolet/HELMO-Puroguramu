using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Puroguramu.Domains;
using Puroguramu.Domains.Exercice.Data;
using Puroguramu.Domains.Lesson;
using Puroguramu.Infrastructures.Data.Entities;

namespace Puroguramu.App.Pages;

public class DashBoard : PageModel
{
    public List<LessonDisplay> LessonList { get; set; } = new List<LessonDisplay>();

    private readonly IRepositoryLesson _repositoryLesson;

    private readonly IRepositoryExercise _repositoryExercise;

    private readonly UserManager<IdentityPuroguramuUser> _userManager;

    public DashBoard(IRepositoryExercise repositoryExercise,IRepositoryLesson repositoryLesson, UserManager<IdentityPuroguramuUser> userManager)
    {
        _repositoryLesson = repositoryLesson;
        _repositoryExercise = repositoryExercise;
        _userManager = userManager;
    }

    public async Task OnGetAsync()
    {
        var currentUser = await _userManager.GetUserAsync(User);
        if (currentUser == null)
        {
            return;
        }

        var lessonIdList = await _repositoryLesson.AllLessonPublish();
        foreach (var lesson in lessonIdList)
        {
            var finishExercises = 0;
            var exerciseIdList = await _repositoryLesson.GetExercisePublishOfLesson(lesson.IdLesson);
            foreach (var exercise in exerciseIdList)
            {
                var exerciseUser = await _repositoryExercise.GetExerciseUser(exercise.IdExercise, currentUser.Matricule);
                if (exerciseUser != null)
                {
                    finishExercises += exerciseUser.IsFinished ? 1 : 0;
                }
            }

            LessonList.Add(new LessonDisplay(lesson.Name,lesson.IdLesson,finishExercises,exerciseIdList.Count));
        }
    }
}
