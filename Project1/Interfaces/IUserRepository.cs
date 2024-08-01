using Project1.Models;

namespace Project1.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUser(int UserID);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUser(string email, string pwd);
        Task<int> GetUsersCountAsync();
        Task<User> AddUser(User user);
        Task<User> UpdateUser(User user);
        Task<User> DeleteUser(int UserID);

    }
}
