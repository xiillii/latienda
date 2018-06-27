using System;
using System.Collections.Generic;

namespace ServiceRestConsumer.Entities
{
    public class Meta
    {
        public Guid ResponseIdentifier { get; set; }
        public ResponseTypes Status { get; set; }
        public DateTimeOffset Date { get; set; }
        public List<string> Messages { get; set; }
    }
}