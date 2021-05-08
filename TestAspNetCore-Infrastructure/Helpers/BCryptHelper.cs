using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestAspNetCore_Infrastructure.Helpers
{
    public class BCryptHelper
    {
        public static async Task<string> PasswordHash(string password)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            return await Task.FromResult(passwordHash);
        }

        public static async Task<bool> VerifyHash(string password, string passwordHash)
        {
            bool verified = BCrypt.Net.BCrypt.Verify(password, passwordHash);
            return await Task.FromResult(verified);
        }

    }
}
