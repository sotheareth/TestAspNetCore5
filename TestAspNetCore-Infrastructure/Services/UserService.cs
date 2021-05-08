using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestAspNetCore_Core.Entities;
using TestAspNetCore_Core.Interfaces;

namespace TestAspNetCore_Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository repository, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task DeleteUser(int userId)
        {
            await _repository.DeleteUser(userId);
            await _unitOfWork.Complete();
        }

        public async Task<User> GetUser(int userId)
        {
            return await _repository.GetUser(userId);
        }

        public async Task<User> GetUserByName(string userName)
        {
            return await _repository.GetUserByName(userName);
        }

        public async Task<User> GetUserByRefreshToken(string refreshToken)
        {
            return await _repository.GetUserByRefreshToken(refreshToken);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _repository.GetUsers();
        }

        public async Task<bool> RefreshToken(string token, string ipAddress, string refreshToken)
        {
            return await _repository.RefreshToken(token, ipAddress, refreshToken);
        }

        public async Task<bool> RevokeToken(string token, string ipAddress, string refreshToken)
        {
            return await _repository.RevokeToken(token, ipAddress, refreshToken);
        }

        public async Task Register(User user)
        {
            await _repository.RegisterUser(user);
            await _unitOfWork.Complete();
        }

        public async Task Update(User user)
        {
            await _repository.UpdateUser(user);
            await _unitOfWork.Complete();
        }

        public async Task<User> GetUserByToken(string token)
        {
            return await _repository.GetUserByToken(token);
        }
    }
}
