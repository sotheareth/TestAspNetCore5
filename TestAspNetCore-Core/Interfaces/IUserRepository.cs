using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestAspNetCore_Core.Entities;

namespace TestAspNetCore_Core.Interfaces
{
    public interface IUserRepository
    {
        Task RegisterUser(User user);
        Task UpdateUser(User user);
        Task<User> GetUser(int userId);
        Task<User> GetUserByName(string userName);
        Task<User> GetUserByToken(string token);
        Task<User> GetUserByRefreshToken(string refreshToken);
        Task DeleteUser(int userId);
        Task<IEnumerable<User>> GetUsers();
        Task<bool> RefreshToken(string token, string ipAddress, string refreshToken);
        Task<bool> RevokeToken(string token, string ipAddress, string refreshToken);


    }
}
