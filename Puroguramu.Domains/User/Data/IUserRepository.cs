namespace Puroguramu.Domains.User.Data;

public interface IUserRepository
{
    Task<IUser?> GetByMatricule(string matricule);

    Task<bool> NewUser(string matricule,string firstname,string lastname,string mail,string password,string group);

    Task<List<IUser>> GetAllUsers();

    Task<List<string>> GetNumberOfStudent();
}
