using System;
using System.Collections.Generic;
using System.Text;

namespace Forex.DomainModels.Integration
{
    public class ExchangeRateSyncBaseDomainModel
    {
        public int Id { get; set; }
        public string BaseCurrency { get; set; }
        public DateTime SyncDate { get; set; }
    }
}
