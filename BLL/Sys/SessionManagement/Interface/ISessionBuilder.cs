using BLL.Sys.SessionManagement.Concrete;
using DAL.Entity.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Sys.SessionManagement.Interface
{
    public interface ISessionBuilder : IDisposable
    {
        #region Methods:
        public void generateSessionID();
        public void setSessionID();
        public void setUserID(int _UserID);
        public void setDeviceInfo(string _DeviceInfo);
        public Session generateSession(int _UserID, string _DeviceInfo);
        #endregion
    }
}
