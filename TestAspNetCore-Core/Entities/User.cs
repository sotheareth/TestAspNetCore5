using System.Collections.Generic;

namespace TestAspNetCore_Core.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public IList<Role> Roles { get; set; }
        public IList<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
