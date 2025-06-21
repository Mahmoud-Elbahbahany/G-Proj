using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity.ResultsManagement
{
    internal class Grade
    {
    }
}
/* Attributes:
    grade_id (INTEGER PRIMARY KEY) - Unique grade identifier.
    response_id (INTEGER) - Foreign key to Test_Responses(response_id).
    score (REAL) - Numeric score (e.g., 85.5).
    is_finalized (INTEGER) - Boolean flag (0 = draft, 1 = finalized).
 */
