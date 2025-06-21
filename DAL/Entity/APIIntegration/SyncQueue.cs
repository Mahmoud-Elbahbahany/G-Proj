using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity.APIIntegration
{
    public class SyncQueue
    {
    }
}
/*  Entity: Sync_Queue
    Description: Queues data for online synchronization.
    Attributes:
        queue_id (INTEGER PRIMARY KEY) - Unique queue identifier.
        data_type (TEXT) - Type of data (e.g., "Grade", "Test").
        data_json (TEXT) - JSON of data to sync (e.g., grade details).
        status (TEXT) - Sync status (e.g., "Pending", "Synced").
        timestamp (TEXT) - Queue entry timestamp.
 */
