using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestAspNetCore_Core.Entities;
using TestAspNetCore_Core.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TestAspNetCore_Infrastructure.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly IGenericRepository<User> _repository;
        private readonly CustomerContext _context;

        public UserRepository(IGenericRepository<User> repository, CustomerContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task DeleteUser(int userId)
        {
            var user = await _repository.GetByIdAsync(userId);
            await _repository.Delete(user);
        }

        public async Task<User> GetUser(int userId)
        {
            return await _context.User.SingleOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<User> GetUserByName(string userName)
        {
            return await _context.User.SingleOrDefaultAsync(x => x.Username == userName);
        }

        public async Task<User> GetUserByRefreshToken(string refreshToken)
        {
            return await _context.User.SingleOrDefaultAsync(x => x.RefreshTokens.Any(y => y.ReplacedByToken == refreshToken));
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _repository.ListAllAsync();
        }

        public async Task<bool> RefreshToken(string token, string ipAddress, string refreshToken)
        {
            var user = await _context.User.Include(token => token.RefreshTokens).SingleOrDefaultAsync(x => x.RefreshTokens.Any(y => y.Token == token));
            if (user == null) return false;

            var currentRefreshToken = user.RefreshTokens.Single(x => x.Token == token);
            if (!currentRefreshToken.IsActive) return false;

            // replace old refresh token with a new one and save
            currentRefreshToken.ReplacedByToken = refreshToken;
            currentRefreshToken.Revoked = DateTime.UtcNow;
            currentRefreshToken.RevokedByIp = ipAddress;
            user.RefreshTokens.Add(currentRefreshToken);

            _context.Update(user);
            _context.SaveChanges();

            return true;
        }

        public async Task<bool> RevokeToken(string token, string ipAddress, string refreshToken)
        {
            var user = await _context.User.Include(x => x.RefreshTokens).SingleOrDefaultAsync(x => x.RefreshTokens.Any(y => y.Token == token));
            if (user == null) return false;

            var currentRefreshToken = user.RefreshTokens.Single(x => x.Token == token);
            if (!currentRefreshToken.IsActive) return false;

            // replace old refresh token with a new one and save
            currentRefreshToken.Revoked = DateTime.UtcNow;
            currentRefreshToken.RevokedByIp = ipAddress;

            _context.Update(user);
            _context.SaveChanges();

            return true;
        }

        public async Task RegisterUser(User user)
        {
            await _repository.Add(user);
        }

        public async Task UpdateUser(User user)
        {
            await _repository.Update(user);
        }

        public async Task<User> GetUserByToken(string token)
        {
            return await _context.User.SingleOrDefaultAsync(x => x.RefreshTokens.Any(t => t.Token == token));
        }
    }
}
