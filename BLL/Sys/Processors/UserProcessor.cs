using BLL.Sys.Interface;
using DAL.Entity.Sys;
using DAL.Enum.Sys;
using System;

namespace BLL.Sys.Processors
{
    public class UserProcessor : IUserProcessor
    {
        #region Fields: +1
        private User _user;
        #endregion

        #region Constructor: +1
        public UserProcessor()
        {
            Initialize();
        }
        #endregion

        #region Accessors(Getters): +7
        public int getID() => _user.ID;
        public string getName() => _user.Name;
        public string getEmail() => _user.Email;
        public string getHashedPassword() => _user.HashedPassword;
        public UserRole getRole() => _user.Role;
        public string getCreatedAt() => _user.CreatedAt;
        public string getUpdatedAt() => _user.UpdatedAt;
        public User getUser() => _user.Clone();
        #endregion

        #region Mutators(Setters): +7
        public void setID(int id)
        {
            _user.ID = id;
            setUpdatedAt();
        }
        public void setName(string name) => _user.Name = name;
        public void setEmail(string email) => _user.Email = email;
        public void setHashedPassword(string hashedPassword) => _user.HashedPassword = hashedPassword;
        public void setRole(UserRole role) => _user.Role = role;
        public void setCreatedAt() => _user.CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public void setUpdatedAt() => _user.UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        #endregion

        #region Lifecycle Methods: +3
        public void Initialize()
        {
            _user = new User(0, "", "", "", UserRole.Guest);
        }

        public void Terminate()
        {
            _user?.Dispose();
            Initialize();
        }

        public void Dispose()
        {
            Terminate();
            GC.SuppressFinalize(this);
        }
        #endregion

        #region DeCtor: +1
        ~UserProcessor() => Dispose();
        #endregion
    }
}