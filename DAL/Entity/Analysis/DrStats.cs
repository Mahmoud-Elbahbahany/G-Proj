using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity.Analysis
{
    public class DrStats
    {
    }
}
/* Entity: Dr_Stats (Optional)

    Description: Tracks Dr grading efficiency.
    Attributes:
        stat_id (INTEGER PRIMARY KEY) - Unique stat identifier.
        dr_id (INTEGER) - Foreign key to Users(id).
        tests_graded (INTEGER) - Number of tests graded.
        avg_grading_time (REAL) - Average time to grade (in minutes).
        timestamp (TEXT) - Stat calculation timestamp.
 */
