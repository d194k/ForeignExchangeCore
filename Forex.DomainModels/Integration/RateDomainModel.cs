using System;
using System.Collections.Generic;
using System.Text;

namespace Forex.DomainModels.Integration
{
    public class RateDomainModel
    {
        public DateTime RecordDate { get; set; }
        public decimal ExchangeRate { get; set; }
    }
}
