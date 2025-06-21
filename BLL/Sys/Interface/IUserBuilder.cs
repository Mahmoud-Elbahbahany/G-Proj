using DAL.Entity.Sys;
using DAL.Enum.Sys;
using System;

namespace BLL.Sys.Interface
{
    public interface IUserBuilder : IDisposable
    {
        #region Builder Methods: +6
        IUserBuilder WithID(int id);
        IUserBuilder WithName(string name);
        IUserBuilder WithEmail(string email);
        IUserBuilder WithPassword(string hashedPassword);
        IUserBuilder WithRole(UserRole role);
        User Build();
        void Reset();
        #endregion

        #region Lifecycle Methods: +3
        void Initialize();
        void Terminate();
        void Dispose();
        #endregion
    }
}