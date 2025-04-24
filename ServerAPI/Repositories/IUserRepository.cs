using Core;

namespace ServerAPI.Repositories;

public interface IUserRepository
{
    void AddUser(User user);
    User? GetById(int id);
}