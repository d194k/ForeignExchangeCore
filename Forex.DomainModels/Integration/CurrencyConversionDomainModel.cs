using System;
using System.Collections.Generic;
using System.Text;

namespace Forex.DomainModels.Integration
{
    public class CurrencyConversionDomainModel
    {
        public string FirstCurrencyCode { get; set; }
        public string SecondCurrencyCode { get; set; }
        public decimal CurrencyAmount { get; set; }
        public DateTime? ExchangeDate { get; set; }
        public decimal ExchangedAmount { get; set; }
    }
}
