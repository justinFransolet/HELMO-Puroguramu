namespace Puroguramu.Domains.User.Data;

public interface IAssessUser
{
    public bool IsConnectable(IUser user,string password);
}
