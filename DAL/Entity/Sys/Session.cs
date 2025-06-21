using DAL.Enum.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity.Sys
{
    public class Session: IDisposable
    {
        #region Properties
        private string? SessionID;
        private string? DeviceInfo;
        private int UserID;
        private UserRole UserSessionRole;
        private bool IsActive;
        private bool IsLocked;
        private DateTime LoginTime;
        private DateTime LogoutTime;
        private DateTime LastActivityTime;
        private DateTime SessionExpiration;
        #endregion

        #region Ctor
        //public Session() { }

        public Session(string _SessionID , int _UserID , string _DeviceInfo) {
            this.setSessionID(_SessionID);
            this.setUserID(_UserID);
            this.setDeviceInfo(_DeviceInfo);
            this.InitializeSession();
        }
        #endregion

        #region Accessors(Getters)
        public string getSessionID() { return this.SessionID != null ? this.SessionID : string.Empty; }

        public string getDeviceInfo() { return this.DeviceInfo != null ? this.DeviceInfo : string.Empty; }
       
        public int getUserID() { return (int)this.UserID; }
        
        public UserRole getUserSessionRole() { return this.UserSessionRole; }
       
        public bool getIsActive() {  return this.IsActive; }
        
        public bool getIsLocked() { return this.IsLocked; }
        
        public DateTime getLoginTime() { return this.LoginTime; }

        public DateTime getLogoutTime() {  return this.LogoutTime; }
        
        public DateTime getLastActivityTime() { return this.LastActivityTime; }
        
        public DateTime getSessionExpiration() { return this.SessionExpiration; }
        #endregion

        #region Mutators(Setters)
        public void setSessionID(string _SessionID) {  this.SessionID = _SessionID; }
        
        public void setDeviceInfo(string _DeviceInfo) {  this.DeviceInfo = _DeviceInfo; }

        public void setUserID(int _UserID) { this.UserID = _UserID; }

        public void setUserSessionRole(UserRole _UserSessionRole) { this.UserSessionRole = _UserSessionRole; }
        #endregion

        #region Initialization_Methods
        public void setIniIsActive() { this.IsActive = true; }

        public void setIniIsLocked() { this.IsLocked = false; }

        public void setIniLoginTime() { this.LoginTime = DateTime.Now; }

        public void setIniLastActivityTime() { this.LastActivityTime = DateTime.Now; }

        public void setIniSessionExpiration() { this.SessionExpiration = this.getLoginTime().AddMinutes(120); }

        public void InitializeSession()
        {
            this.setIniLoginTime();
            this.setIniLastActivityTime();
            this.setIniSessionExpiration();
            this.setIniIsActive();
            this.setIniIsLocked();
        }
        #endregion

        #region Termination_Methods
        public void setTerIsActive() { this.IsActive = false; }

        public void setTerIsLocked() { this.IsLocked = true; }

        public void setTerLogoutTime() { this.LogoutTime = DateTime.Now; }

        public void setTerLastActivityTime() { this.LastActivityTime = DateTime.Now; }

        public void setTerSessionExpiration() { this.SessionExpiration = DateTime.Now; }

        public void TerminateSession()
        { 
            this.setTerIsActive();
            this.setTerLogoutTime();
            this.setTerLastActivityTime();
            this.setTerSessionExpiration();
        }
        #endregion

        #region Dipose_Methods
        public void Dispose()
        {
            this.SessionID = null;
            this.DeviceInfo = null;
            this.UserID = default(int);
            this.UserSessionRole = default(UserRole);
            this.IsActive = default(bool);
            this.IsLocked = default(bool);
            this.LoginTime = default(DateTime);
            this.LogoutTime = default(DateTime);
            this.LastActivityTime = default(DateTime);
            this.SessionExpiration = default(DateTime);
        }
        #endregion

        #region Default_Methods
        public override string ToString()
        {
            return @$"
                    SessionID: {this.getSessionID()}
                    UserID: {this.getUserID()}
                    DeviceInfo: {this.getDeviceInfo()}
                    UserSessionRole: {this.getUserSessionRole().ToString()}
                    IsActive: {this.getIsActive()}
                    IsLocked: {this.getIsLocked()}
                    LoginTime: {this.getLoginTime()}
                    LogoutTime: {this.getLogoutTime()}
                    LastActivityTime: {this.getLastActivityTime()}
                    SessionExpiration: {this.getSessionExpiration()}";
        }
        #endregion
    }
}