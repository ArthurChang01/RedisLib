using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace RedisLib.Sender.Models
{
    [ExcludeFromCodeCoverage]
    public class ReceiverTable
    {
        public ReceiverTable()
        {
            this.Receivers = new List<ReceiverRecord>();
        }

        public IList<ReceiverRecord> Receivers { get; set; }

        public IDictionary<enLogType, int> CandidateInfo { get; set; }

        public int Amount { get { return this.Receivers.Count(); } }
    }
}
