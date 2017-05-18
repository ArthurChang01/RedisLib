using System.Diagnostics.CodeAnalysis;

namespace Transceiver.Model.Receiver
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

        public override int GetHashCode()
        {
            return this.ReceiverNodeId.GetHashCode() +
                       this.ReceiverId.GetHashCode() +
                       this.UnReplyCounter.GetHashCode();
        }
    }
}
