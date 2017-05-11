using System.Diagnostics.CodeAnalysis;

namespace RedisLib.Sender.Models
{
    [ExcludeFromCodeCoverage]
    public class ReceiverRecord
    {
        public int ReceiverNodeId { get; set; }

        public string ReceiverId { get; set; }

        public int UnReplyCounter { get; set; }

        public override bool Equals(object obj)
        {
            ReceiverRecord target = obj as ReceiverRecord;
            if (target == null) return false;

            return this.ReceiverId.Equals(target.ReceiverId) &&
                this.ReceiverNodeId == target.ReceiverNodeId &&
                this.UnReplyCounter == target.UnReplyCounter;
        }
    }
}
