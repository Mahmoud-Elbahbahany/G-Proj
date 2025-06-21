using BLL.Sys.Concrete;
using BLL.Sys.Interface;
using BLL.Sys.Processors;
using DAL.Entity.Sys;
using DAL.Enum.Sys;
using DAL.Repository.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BLL.Sys.Registration
{
    //public static class RegistrationManager
    //{
    //    #region Private Fields
    //    private static readonly IUserProcessor _processor = new UserProcessor();
    //    private static readonly IUserBuilder _builder = new UserBuilder(_processor);
    //    private static readonly UserRepository _userRepository = new UserRepository();
    //    #endregion

    //    #region Public Methods

    //    /// <summary>
    //    /// Registers a new user with the system
    //    /// </summary>
    //    public static RegistrationResult RegisterUser(string name, string email, string password, UserRole role)
    //    {
    //        try
    //        {
    //            // Validate inputs
    //            if (string.IsNullOrWhiteSpace(name))
    //                return new RegistrationResult(false, "Name cannot be empty");

    //            if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
    //                return new RegistrationResult(false, "Invalid email address");

    //            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
    //                return new RegistrationResult(false, "Password must be at least 8 characters");

    //            // Check for existing email
    //            if (_userRepository.GetByEmail(email) != null)
    //                return new RegistrationResult(false, "Email already registered");

    //            // Create and save user
    //            var user = _builder
    //                .WithName(name)
    //                .WithEmail(email)
    //                .WithPassword(HashPassword(password))
    //                .WithRole(role)
    //                .Build();

    //            _userRepository.Add(user);

    //            return new RegistrationResult(true, "Registration successful", user);
    //        }
    //        catch (Exception ex)
    //        {
    //            return new RegistrationResult(false, $"Registration failed: {ex.Message}");
    //        }
    //    }

    //    /// <summary>
    //    /// Gets all registered users
    //    /// </summary>
    //    public static List<User> GetAllUsers()
    //    {
    //        try
    //        {
    //            return _userRepository.GetAll().ToList();
    //        }
    //        catch
    //        {
    //            return new List<User>();
    //        }
    //    }

    //    /// <summary>
    //    /// Gets a user by their ID
    //    /// </summary>
    //    public static User GetUserById(int id)
    //    {
    //        try
    //        {
    //            return _userRepository.GetById(id);
    //        }
    //        catch
    //        {
    //            return null;
    //        }
    //    }

    //    /// <summary>
    //    /// Gets a user by their email
    //    /// </summary>
    //    public static User GetUserByEmail(string email)
    //    {
    //        try
    //        {
    //            return _userRepository.GetByEmail(email);
    //        }
    //        catch
    //        {
    //            return null;
    //        }
    //    }

    //    /// <summary>
    //    /// Updates an existing user
    //    /// </summary>
    //    public static OperationResult UpdateUser(User user)
    //    {
    //        try
    //        {
    //            if (user == null)
    //                return new OperationResult(false, "User cannot be null");

    //            // Verify user exists
    //            var existingUser = _userRepository.GetById(user.ID);
    //            if (existingUser == null)
    //                return new OperationResult(false, "User not found");

    //            // Update timestamps
    //            user.UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    //            if (string.IsNullOrEmpty(user.CreatedAt))
    //                user.CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

    //            _userRepository.Update(user);
    //            return new OperationResult(true, "User updated successfully");
    //        }
    //        catch (Exception ex)
    //        {
    //            return new OperationResult(false, $"Update failed: {ex.Message}");
    //        }
    //    }

    //    /// <summary>
    //    /// Deletes a user by ID
    //    /// </summary>
    //    public static OperationResult DeleteUser(int id)
    //    {
    //        try
    //        {
    //            var user = _userRepository.GetById(id);
    //            if (user == null)
    //                return new OperationResult(false, "User not found");

    //            _userRepository.Delete(id);
    //            return new OperationResult(true, "User deleted successfully");
    //        }
    //        catch (Exception ex)
    //        {
    //            return new OperationResult(false, $"Delete failed: {ex.Message}");
    //        }
    //    }

    //    /// <summary>
    //    /// Exports all users to a CSV file
    //    /// </summary>
    //    public static OperationResult ExportUsersToCsv(string filePath)
    //    {
    //        try
    //        {
    //            var users = GetAllUsers();
    //            if (!users.Any())
    //                return new OperationResult(false, "No users to export");

    //            using (var writer = new StreamWriter(filePath))
    //            {
    //                // Write header
    //                writer.WriteLine("ID,Name,Email,Role,CreatedAt,UpdatedAt");

    //                // Write data
    //                foreach (var user in users)
    //                {
    //                    writer.WriteLine($"\"{user.ID}\",\"{user.Name}\",\"{user.Email}\",\"{user.Role}\",\"{user.CreatedAt}\",\"{user.UpdatedAt}\"");
    //                }
    //            }

    //            return new OperationResult(true, $"Successfully exported {users.Count} users");
    //        }
    //        catch (Exception ex)
    //        {
    //            return new OperationResult(false, $"Export failed: {ex.Message}");
    //        }
    //    }

    //    /// <summary>
    //    /// Verifies user credentials
    //    /// </summary>
    //    public static AuthenticationResult AuthenticateUser(string email, string password)
    //    {
    //        try
    //        {
    //            var user = GetUserByEmail(email);
    //            if (user == null)
    //                return new AuthenticationResult(false, "Invalid email or password");

    //            if (VerifyPassword(password, user.HashedPassword))
    //                return new AuthenticationResult(true, "Authentication successful", user);

    //            return new AuthenticationResult(false, "Invalid email or password");
    //        }
    //        catch (Exception ex)
    //        {
    //            return new AuthenticationResult(false, $"Authentication failed: {ex.Message}");
    //        }
    //    }
    //    #endregion

    //    #region Private Methods
    //    private static string HashPassword(string password)
    //    {
    //        // In production, use proper hashing like BCrypt or PBKDF2
    //        return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
    //    }

    //    private static bool VerifyPassword(string inputPassword, string storedHash)
    //    {
    //        // Match the hashing algorithm used in HashPassword
    //        return HashPassword(inputPassword) == storedHash;
    //    }

    //    private static bool IsValidEmail(string email)
    //    {
    //        try
    //        {
    //            var addr = new System.Net.Mail.MailAddress(email);
    //            return addr.Address == email;
    //        }
    //        catch
    //        {
    //            return false;
    //        }
    //    }
    //    #endregion

    //    #region Result Classes
    //    public class RegistrationResult : OperationResult
    //    {
    //        public User User { get; }

    //        public RegistrationResult(bool success, string message, User user = null)
    //            : base(success, message)
    //        {
    //            User = user;
    //        }
    //    }

    //    public class AuthenticationResult : OperationResult
    //    {
    //        public User User { get; }

    //        public AuthenticationResult(bool success, string message, User user = null)
    //            : base(success, message)
    //        {
    //            User = user;
    //        }
    //    }

    //    public class OperationResult
    //    {
    //        public bool Success { get; }
    //        public string Message { get; }

    //        public OperationResult(bool success, string message)
    //        {
    //            Success = success;
    //            Message = message;
    //        }
    //    }
    //    #endregion
    //}
}
