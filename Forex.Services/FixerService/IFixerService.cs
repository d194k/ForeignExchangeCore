using Forex.DomainModels.Integration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forex.Services.FixerService
{
    public interface IFixerService
    {        
        bool CheckCurrencyCodeExists(string currencyCode);
        ExchangeRatesDomainModel GetLatestExchangeRateDomainModel();
        CurrencyConversionDomainModel CurrencyConversion(CurrencyConversionDomainModel model);        
    }
}
