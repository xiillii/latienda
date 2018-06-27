using System;

namespace ServiceRestConsumer.Business.Model
{
    public class BaseBusinessResponse
    {
        public bool Success { get; set; }
        public Exception Exception { get; set; }
    }
}