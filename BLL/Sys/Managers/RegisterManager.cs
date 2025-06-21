using BLL.Sys.Concrete;
using BLL.Sys.Interface;
using BLL.Sys.Processors;
using DAL.Entity.Sys;
using DAL.Enum.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Sys.Managers
{
    public static class RegisterManager
    {
        private static readonly IUserProcessor _processor = new UserProcessor();
        private static readonly IUserBuilder _builder = new UserBuilder(_processor);

        public static User Register(string name, string email, string password)
        {
            // Check if email already exists
            if (LoginManager.Authenticate(email, password) != null)
            {
                return null;
            }

            // Create new user
            var newUser = _builder
                .WithID(LoginManager.GetNextId())
                .WithName(name)
                .WithEmail(email)
                .WithPassword(LoginManager.HashPassword(password))
                .WithRole(UserRole.Guest) // Default role for new users
                .Build();

            // Add to "database"
            LoginManager.AddUser(newUser);

            return newUser.Clone();
        }
    }
}
