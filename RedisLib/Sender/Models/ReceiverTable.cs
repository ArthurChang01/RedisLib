﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace RedisLib.Sender.Models
{
    [ExcludeFromCodeCoverage]
    public class ReceiverTable
    {
        private int _nodeId = -1;

        public IList<ReceiverRecord> Receivers { get; set; }

        public int CandidateNodeId { get { return _nodeId; } set { this._nodeId = value; } }

        public int Amount { get { return this.Receivers.Count(); } }
    }
}