using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace STNDownV2.Models
{
    public class ServerCheckerEvent
    {
        public long ID { get; set; }
        public DateTime Timestamp { get; set; }
        public bool Success { get; set; }
        public long Latency { get; set; }

        [ForeignKey("CheckedServerForeignKey")]
        public int CheckedServerId { get; set; }
        public CheckedServer CheckedServer { get; set; }
    }
}