using BLL.Sys.Interface;
using DAL.Entity.Sys;
using DAL.Enum.Sys;
using System;

namespace BLL.Sys.Concrete
{
    public class UserBuilder : IUserBuilder
    {
        #region Fields: +1
        private readonly IUserProcessor _processor;
        #endregion

        #region Constructor: +1
        public UserBuilder(IUserProcessor processor)
        {
            _processor = processor;
            Initialize();
        }
        #endregion

        #region Builder Methods: +6
        public IUserBuilder WithID(int id)
        {
            _processor.setID(id);
            return this;
        }

        public IUserBuilder WithName(string name)
        {
            _processor.setName(name);
            return this;
        }

        public IUserBuilder WithEmail(string email)
        {
            _processor.setEmail(email);
            return this;
        }

        public IUserBuilder WithPassword(string hashedPassword)
        {
            _processor.setHashedPassword(hashedPassword);
            return this;
        }

        public IUserBuilder WithRole(UserRole role)
        {
            _processor.setRole(role);
            return this;
        }

        public User Build()
        {
            var user = _processor.getUser();
            Reset();
            return user;
        }
        #endregion

        #region Lifecycle Methods: +3
        public void Initialize() => _processor.Initialize();

        public void Terminate() => _processor.Terminate();

        public void Dispose()
        {
            _processor.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Utility Methods: +1
        public void Reset() => Initialize();
        #endregion

        #region DeCtor: +1
        ~UserBuilder() => Dispose();
        #endregion
    }
}