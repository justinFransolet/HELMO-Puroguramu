namespace Puroguramu.Domains.User.Data;

public interface IUser
{
    string Matricule { get; set; }

    string LastName { get; set; }

    string FirstName { get; set; }

    string UserName { get; set; }

    string Group { get; set; }

    string Email { get; set; }
}
