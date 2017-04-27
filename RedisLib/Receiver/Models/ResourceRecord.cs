using System;

namespace RedisLib.Receiver.Models
{
    public class ResourceRecord
    {
        public string Id { get; set; }

        public string Resource { get; set; }

        public long AmountOfLog { get; set; }

        public DateTime UpdateTime { get; set; }

        public void Update(ResourceRecord source)
        {
            this.Resource = source.Resource;
            this.UpdateTime = source.UpdateTime;
        }
    }
}
