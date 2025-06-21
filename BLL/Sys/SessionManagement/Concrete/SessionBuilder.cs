using BLL.Sys.SessionManagement.Interface;
using DAL.Entity.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Sys.SessionManagement.Concrete
{
    public class SessionBuilder : ISessionBuilder 
    {
        #region Properties
        private Guid? NewID;
        private string? SessionID;
        private int UserID;
        private string? DeviceInfo;
        #endregion

        #region Ctor
        public SessionBuilder() {
            this.NewID = Guid.Empty;
            this.SessionID = string.Empty;
            this.UserID = default(int);
            this.DeviceInfo = string.Empty;
        }
        #endregion

        #region Methods
        public void generateSessionID() { this.NewID = Guid.NewGuid(); }

        public void setSessionID() { this.SessionID = this.NewID.ToString(); }

        public void setUserID(int _UserID) { this.UserID = _UserID; }

        public void setDeviceInfo(string _DeviceInfo) { this.DeviceInfo = _DeviceInfo; }

        public Session generateSession(int _UserID, string _DeviceInfo)
        {
            this.generateSessionID();
            this.setSessionID();
            this.setUserID(_UserID);
            this.setDeviceInfo(_DeviceInfo);
            return new Session(this.SessionID, this.UserID, this.DeviceInfo);
        }
        #endregion

        #region Dispose_Methods
        public void Dispose()
        {
            this.NewID = null;
            this.SessionID = null;
            this.UserID = default(int);
            this.DeviceInfo = null;
        }
        #endregion

        #region Default_Methods
        public override string ToString()
        {
            return @$"
                    NewID: {this.NewID.ToString()}
                    DeviceInfo: {this.UserID}
                    UserID: {this.DeviceInfo}";
        }
        #endregion
    }
}


