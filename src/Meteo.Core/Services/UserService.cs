using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Meteo.Core.Domain;
using Meteo.Core.Security;
using Microsoft.AspNetCore.Identity;

namespace Meteo.Core.Services
{
    public class UserService : IUserService
    {
        private static readonly ISet<User> _users = new HashSet<User>();
        private readonly IJwtHandler _jwtHandler;
        private readonly IPasswordHasher<User> _hasher;

        public UserService(IJwtHandler jwtHandler,
            IPasswordHasher<User> hasher)
        {
            _jwtHandler = jwtHandler;
            _hasher = hasher;
        }

        public async Task SignUpAsync(string email, string password, string role)
        {
            var user = _users.SingleOrDefault(x => x.Email == email.ToLowerInvariant());
            if (user != null)
            {
                throw new Exception($"User {email} already exists.");
            }
            user = new User(email, role);
            user.SetPassword(password, _hasher);
            _users.Add(user);
            await Task.CompletedTask;
        }

        public async Task<JsonWebToken> SignInAsync(string email, string password)
        {
            var user = _users.SingleOrDefault(x => x.Email == email.ToLowerInvariant());
            if (user == null)
            {
                throw new Exception("Invalid credentials.");
            }
            if (!user.ValidatePassword(password, _hasher))
            {
                throw new Exception("Invalid credentials.");
            }

            return await Task.FromResult(_jwtHandler.Create(user.Id, user.Role, null));          
        }
    }
}