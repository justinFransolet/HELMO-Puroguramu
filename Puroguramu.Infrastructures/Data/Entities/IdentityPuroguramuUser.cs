using Microsoft.AspNetCore.Identity;
using Puroguramu.Domains.User.Data;

namespace Puroguramu.Infrastructures.Data.Entities;

public class IdentityPuroguramuUser : IdentityUser, IUser
{

    public string Matricule { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Group { get; set; } = string.Empty;

    public byte[] ProfilePicture { get; set; } = Array.Empty<byte>();
}
