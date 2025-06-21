using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity.Sys
{
    public class AuditLog
    {
        #region Properties:

        #region ID_Constraints:
        private int ID;
        #endregion

        #region UserID_Constraints:
        private int UserID;
        #endregion

        #region Action_Constraints:
        private string Action;
        #endregion

        #region CreatedAt_Constraints:
        public string CreatedAt = string.Empty;
        #endregion

        #region UpdatedAt_Contraints:
        public string UpdatedAt = string.Empty;
        #endregion

        #endregion
    }
}
