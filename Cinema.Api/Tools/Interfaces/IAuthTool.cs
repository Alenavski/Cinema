namespace Cinema.API.Tools.Interfaces
{
    public interface IAuthTool
    {
        string CreateToken(string email, int id, string role);
    }
}