using BLL.Sys.Concrete;
using BLL.Sys.Interface;
using BLL.Sys.Processors;
using DAL.Entity.Sys;
using DAL.Enum.Sys;

namespace BLL.Sys.Managers
{
    public static class LoginManager
    {
        // In a real application, this would be replaced with database access
        private static readonly Dictionary<string, User> _users = new Dictionary<string, User>();
        private static readonly IUserProcessor _processor = new UserProcessor();
        private static readonly IUserBuilder _builder = new UserBuilder(_processor);

        static LoginManager()
        {
            // Add a test user
            var testUser = _builder
                .WithID(1)
                .WithName("Admin User")
                .WithEmail("admin@example.com")
                .WithPassword("hashedpassword") // In real app, this should be properly hashed
                .WithRole(UserRole.Admin)
                .Build();

            _users.Add(testUser.Email, testUser);
        }

        public static User Authenticate(string email, string password)
        {
            if (_users.TryGetValue(email, out var user))
            {
                // In a real app, verify the hashed password
                if (user.HashedPassword == HashPassword(password))
                {
                    return user.Clone();
                }
            }
            return null;
        }

        public static string HashPassword(string password)
        {
            // In a real application, use proper password hashing like PBKDF2, bcrypt, etc.
            return password; // This is just a placeholder
        }

        public static int GetNextId()
        {
            return _users.Count + 1;
        }

        public static void AddUser(User user)
        {
            if (!_users.ContainsKey(user.Email))
            {
                _users.Add(user.Email, user);
            }
        }
    }
}
