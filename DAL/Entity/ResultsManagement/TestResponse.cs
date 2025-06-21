using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity.ResultsManagement
{
    public class TestResponse
    {
        #region Properties:

        #endregion
    }
}
/* Attributes:
    response_id (INTEGER PRIMARY KEY) - Unique response identifier.
    test_id (INTEGER) - Foreign key to Tests(test_id).
    student_id (INTEGER) - Foreign key to Users(id) (student who submitted).
    answers_json (TEXT) - JSON of student answers (e.g., {"q1": "B", "q2": "True"}).
    submission_time (TEXT) - Submission timestamp.
 */
