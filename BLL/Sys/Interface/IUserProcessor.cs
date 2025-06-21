using DAL.Entity.Sys;
using DAL.Enum.Sys;
using System;

namespace BLL.Sys.Interface
{
    public interface IUserProcessor : IDisposable
    {
        #region Accessors(Getters): +7
        int getID();
        string getName();
        string getEmail();
        string getHashedPassword();
        UserRole getRole();
        string getCreatedAt();
        string getUpdatedAt();
        User getUser();
        #endregion

        #region Mutators(Setters): +7
        void setID(int id);
        void setName(string name);
        void setEmail(string email);
        void setHashedPassword(string hashedPassword);
        void setRole(UserRole role);
        void setCreatedAt();
        void setUpdatedAt();
        #endregion

        #region Lifecycle Methods: +3
        void Initialize();
        void Terminate();
        void Dispose();
        #endregion
    }
}