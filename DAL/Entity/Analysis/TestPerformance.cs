using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity.Analysis
{
    public class TestPerformance
    {
    }
}
/* Entity: Test_Performance (Optional)

    Description: Stores precomputed test performance reports.
    Attributes:
        report_id (INTEGER PRIMARY KEY) - Unique report identifier.
        test_id (INTEGER) - Foreign key to Tests(test_id).
        average_score (REAL) - Average student score.
        pass_rate (REAL) - Percentage of passing students.
        timestamp (TEXT) - Report generation timestamp.
 */
