using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity.Sys
{
    public class Bacup
    {
        #region Properties:

        #region ID_Constraints:
        private int ID = default;
        #endregion

        #region FilePath_Constraints:
        private string FilePath;
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
