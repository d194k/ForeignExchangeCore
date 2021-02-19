using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forex.DomainModels.Integration
{
    public class ExchangeRatesDomainModel
    {
        public bool Success { get; set; }
        public int Timestamp { get; set; }
        public bool Historical { get; set; } = false;
        public string Base { get; set; }
        public DateTime Date { get; set; }
        public JObject Rates { get; set; }
    }
}
