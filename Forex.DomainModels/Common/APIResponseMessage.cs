using System;
using System.Collections.Generic;
using System.Text;

namespace Forex.DomainModels.Common
{
    public class APIResponseMessage
    {
        public int StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public string Message { get; set; }
        public dynamic Result { get; set; }
    }
}
