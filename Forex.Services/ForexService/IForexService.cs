using Forex.DomainModels.Integration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forex.Services.ForexService
{
    public interface IForexService
    {
        bool SyncLatestExchangeRatesInDB();
        RateHistoryDomainModel GetRateHistory(string currencyCode, string from, string to);
    }
}
