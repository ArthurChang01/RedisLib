using System.Collections.Generic;
using System.Linq;

namespace RedisLib.Sender.Models
{
    public class ReceiverTable
    {
        public IEnumerable<ReceiverRecord> Receivers { get; set; }

        public int CandidateNodeId { get; set; }

        public int Amount { get { return this.Receivers.Count(); } }
    }
}
