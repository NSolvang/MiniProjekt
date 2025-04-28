using Core;

namespace ServerAPI.Repositories;

public interface IUserRepository
{
    void AddUser(User user);
    User? GetUser(string username, string password);
    User? GetUserById(int id);
}