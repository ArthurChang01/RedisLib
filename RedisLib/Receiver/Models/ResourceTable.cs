using System;
using System.Collections.Concurrent;
using System.Linq;

namespace RedisLib.Receiver.Models
{
    public class ResourceTable
    {
        #region Constructor
        public ResourceTable()
        {
            this.Records = new ConcurrentBag<ResourceRecord>();
        }
        #endregion

        #region Property
        public ConcurrentBag<ResourceRecord> Records { get; set; }
        #endregion

        #region Public Methods
        public void Add(ResourceRecord rcd)
        {
            this.Records.Add(rcd);
        }

        public void Update(ResourceRecord rcd)
        {
            ResourceRecord target = this.Records.FirstOrDefault(o => o.Id.Equals(rcd.Id));

            if (target == null) throw new Exception();
        }

        public ResourceRecord GetToughMember()
        {
            ResourceRecord result = null;

            long max = this.Records.Max(o => o.AmountOfLog);
            result = this.Records.FirstOrDefault(o => o.AmountOfLog == max);

            return result;
        }

        public bool IsExist(string Id)
        {
            return this.Records.Any(o => o.Id.Equals(Id));
        }
        #endregion
    }
}
