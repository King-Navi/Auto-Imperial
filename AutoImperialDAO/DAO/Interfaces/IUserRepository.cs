using AutoImperialDAO.Models;

namespace AutoImperialDAO.DAO.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User Authenticate(string username, string password);
    }
}
