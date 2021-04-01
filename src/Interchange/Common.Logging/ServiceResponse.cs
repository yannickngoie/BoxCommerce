﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Logging
{
    public class ServiceResponse
    {
        public bool success { get; set; }
        public string Message { get; set; }
    }

    public enum Status
    {
        InProgress,
        NotStared,
        InProduction,
        Completed
    }

}
