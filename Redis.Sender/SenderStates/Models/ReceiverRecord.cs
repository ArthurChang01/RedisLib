namespace Redis.Sender.SenderStates.Models
{
    public class ReceiverRecord
    {
        public int ReceiverNodeId { get; set; }

        public string ReceiverId { get; set; }

        public int UnReplyCounter { get; set; }
    }
}
