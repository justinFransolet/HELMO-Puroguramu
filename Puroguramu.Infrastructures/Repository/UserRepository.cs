using Microsoft.EntityFrameworkCore;
using Puroguramu.Domains.User.Data;
using Puroguramu.Infrastructures.Data;
using Puroguramu.Infrastructures.Data.Configuration;
using Puroguramu.Infrastructures.Data.Entities;

namespace Puroguramu.Infrastructures.Repository;

public class UserRepository : IUserRepository
{
    private readonly PuroguramuDbContext _dbAccess;

    public UserRepository(PuroguramuDbContext dbAccess)
    {
        _dbAccess = dbAccess;
    }

    public async Task<IUser?> GetByMatricule(string matricule)
    {
        try
        {
            return await _dbAccess.Users.Include(user => user.Id).FirstOrDefaultAsync(user => user.Id == matricule);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<bool> NewUser(string matricule,string firstname,string lastname,string mail,string password,string group)
    {
        try
        {
            var user = new IdentityPuroguramuUser()
            {
                Id = matricule,
                FirstName = firstname,
                LastName = lastname,
                UserName = lastname + " " + firstname,
                Email = mail,
                PasswordHash = password,
                Group = group,
            };
            _dbAccess.Users.Add(user);
            await _dbAccess.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public Task<List<IUser>> GetAllUsers() => _dbAccess.Users.Select(u => (IUser) u).ToListAsync();

    public async Task<List<string>> GetNumberOfStudent()
    {
        var studentList = await _dbAccess.UserRoles
            .Where(user => user.RoleId == UserRoleConfiguration.StudentId)
            .Select(user => user.UserId)
            .ToListAsync();

        return studentList;
    }
}
