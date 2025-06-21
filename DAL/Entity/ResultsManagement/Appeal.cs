using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity.ResultsManagement
{
    public class Appeal
    {
    }
}
/* Attributes:
    appeal_id (INTEGER PRIMARY KEY) - Unique appeal identifier.
    grade_id (INTEGER) - Foreign key to Grades(grade_id).
    student_id (INTEGER) - Foreign key to Users(id).
    reason (TEXT) - Appeal reason.
    status (TEXT) - Appeal status (e.g., "Pending", "Approved", "Rejected").
 */