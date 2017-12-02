using System;
using Microsoft.AspNetCore.Identity;

namespace Meteo.Core.Domain
{
    public class User
    {
        public Guid Id { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string Role { get; protected set; }

        public User(string email, string role)
        {
            Id = Guid.NewGuid();
            Email = email.ToLowerInvariant();
            Role = role;
        }

        public void SetPassword(string password, IPasswordHasher<User> hasher)
        {
            Password = hasher.HashPassword(this, password);
        }

        public bool ValidatePassword(string password, IPasswordHasher<User> hasher)
        {
            var result = hasher.VerifyHashedPassword(this, Password, password);

            return result != PasswordVerificationResult.Failed;
        }
    }
}