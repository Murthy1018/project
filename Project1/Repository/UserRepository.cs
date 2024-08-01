using Microsoft.EntityFrameworkCore;
using Project1.Interfaces;
using Project1.Models;

namespace Project1.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly RegisterAPIDbContext regiterDbContext;

        public UserRepository(RegisterAPIDbContext regiterDbContext)
        {
            this.regiterDbContext = regiterDbContext;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await regiterDbContext.User.ToListAsync();
        }

        public async Task<User> GetUser(int userId)
        {
            return await regiterDbContext.User
                .FirstOrDefaultAsync(e => e.Id == userId);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await regiterDbContext.User.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<User> GetUser(string email, string pwd)
        {
            return await regiterDbContext.User.FirstOrDefaultAsync(x => x.Email == email && x.Password == pwd);

        }
        public async Task<int> GetUsersCountAsync()
        {
            return await regiterDbContext.User.CountAsync();
        }
        public async Task<User> AddUser(User user)
        {
            var result = await regiterDbContext.User.AddAsync(user);
            await regiterDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<User> UpdateUser(User user)
        {
            var result = await regiterDbContext.User
                .FirstOrDefaultAsync(e => e.Id == user.Id);

            if (result != null)
            {
                result.Email = user.Email;
                result.Password = user.Password;
                result.FirstName = user.FirstName;
                result.LastName = user.LastName;
                result.Phone = user.Phone;
                result.Address = user.Address;
                result.Resume = user.Resume;
                result.Country = user.Country;


                await regiterDbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<User> DeleteUser(int userId)
        {
            var result = await regiterDbContext.User
                .FirstOrDefaultAsync(e => e.Id == userId);
            if (result != null)
            {
                regiterDbContext.User.Remove(result);
                await regiterDbContext.SaveChangesAsync();
            }
            return result;
        }

        
    }
}

