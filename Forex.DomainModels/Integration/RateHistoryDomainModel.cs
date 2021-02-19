using System;
using System.Collections.Generic;
using System.Text;

namespace Forex.DomainModels.Integration
{
    public class RateHistoryDomainModel
    {
            public string CurrencyCode { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public List<RateDomainModel> Records { get; set; }
    }
}
