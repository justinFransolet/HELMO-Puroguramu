using Microsoft.AspNetCore.Mvc.RazorPages;
using Puroguramu.Domains;
using Puroguramu.Domains.User.Data;

namespace Puroguramu.App.Pages;

public class IndexModel : PageModel
{
    private readonly IUserRepository _repository;
    private readonly IRepositoryLesson _repositoryLesson;

    public int Users { get; set; } = 0;

    public int NumberOfLessons { get; set; } = 0;

    public int NumberOfExercices { get; set; } = 0;

    public IndexModel(IUserRepository repository, IRepositoryLesson repositoryLesson)
    {
        _repository = repository;
        _repositoryLesson = repositoryLesson;
    }

    public async Task OnGetAsync()
    {
        Users = (await _repository.GetNumberOfStudent()).Count();
        NumberOfLessons = await _repositoryLesson.GetNumberOfLessons();
        NumberOfExercices = await _repositoryLesson.GetNumberOfExercicesAllLessons();
    }
}
