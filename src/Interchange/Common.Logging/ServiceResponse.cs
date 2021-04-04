using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Logging
{
    public class ServiceResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public enum Status
    {
        Created,
        InProgress,
        NotStared,
        InProduction,
        Completed,
        Cancelled

    }

}
