using Forex.DomainModels.Integration;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Forex.Services.FixerService
{
    public class FixerService : IFixerService
    {
        private readonly IConfiguration _config;
        private readonly string _fixerApiBaseUrl;
        private readonly string _fixerApiKey;
        private readonly JObject _allRates;

        public FixerService(IConfiguration config)
        {
            _config = config;
            _fixerApiBaseUrl = _config.GetValue<string>("FixerApiBaseUrl");
            _fixerApiKey = _config.GetValue<string>("FixerApiKey");
            _allRates = GetAllLatestRates();
        }

        #region Public Methods       
        public bool CheckCurrencyCodeExists(string currencyCode)
        {
            if (currencyCode != null && _allRates.ContainsKey(currencyCode.ToUpper()))
            {
                return true;
            }
            return false;
        }
        
        public ExchangeRatesDomainModel GetLatestExchangeRateDomainModel()
        {
            return GetExchangeRatesDomainModel();
        }

        public CurrencyConversionDomainModel CurrencyConversion(CurrencyConversionDomainModel model)
        {
            CurrencyConversionDomainModel result = default;
            try
            {
                if (!CheckCurrencyCodeExists(model.FirstCurrencyCode) || !CheckCurrencyCodeExists(model.SecondCurrencyCode) || model.CurrencyAmount < 0)
                {
                    throw new Exception("Invalid request data");
                }
                var exchangedAmount = CurrencyConversion(model.FirstCurrencyCode.ToUpper(), model.SecondCurrencyCode.ToUpper(), model.CurrencyAmount, model.ExchangeDate);
                result = result ?? new CurrencyConversionDomainModel();
                result.FirstCurrencyCode = model.FirstCurrencyCode.ToUpper();
                result.CurrencyAmount = model.CurrencyAmount;
                result.SecondCurrencyCode = model.SecondCurrencyCode.ToUpper();
                result.ExchangedAmount = exchangedAmount;
                result.ExchangeDate = model.ExchangeDate;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        #endregion


        #region Private Methods
        private ExchangeRatesDomainModel GetExchangeRatesDomainModel(string[] currencyCodes = null, DateTime? date = null)
        {
            ExchangeRatesDomainModel model = default(ExchangeRatesDomainModel);
            var url = _fixerApiBaseUrl;
            if (date.HasValue)
            {
                url = url + $"/{date.Value.ToString("yyyy-MM-dd")}?access_key={_fixerApiKey}";
            }
            else
            {
                url = url + $"/latest?access_key={_fixerApiKey}";
            }
            if (currencyCodes != null && currencyCodes.Length > 0)
            {
                url = url + $"&symbols={string.Join(",", currencyCodes)}";
            }
            try
            {
                HttpWebRequest request = WebRequest.CreateHttp(url);
                request.Method = "GET";
                HttpWebResponse respone = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(respone.GetResponseStream()))
                {
                    if (respone.StatusCode == HttpStatusCode.OK)
                    {
                        var responseData = streamReader.ReadToEnd();
                        model = JsonConvert.DeserializeObject<ExchangeRatesDomainModel>(responseData);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return model;
        }

        private JObject GetAllLatestRates()
        {
            return GetExchangeRates();
        }

        private decimal CurrencyConversion(string firstCurrencyCode, string secondCurrencyCode, decimal amount, DateTime? date)
        {
            try
            {
                string[] currencyCodes = new string[] { firstCurrencyCode, secondCurrencyCode };
                var exchangeRates = GetExchangeRates(currencyCodes, date);
                if (exchangeRates != null && exchangeRates.ContainsKey(firstCurrencyCode) && exchangeRates.ContainsKey(secondCurrencyCode))
                {
                    var firstCurrencyRate = (decimal)exchangeRates[firstCurrencyCode];
                    var secondCurrencyRate = (decimal)exchangeRates[secondCurrencyCode];

                    return (amount / firstCurrencyRate) * secondCurrencyRate;
                }
                return default(decimal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private JObject GetExchangeRates(string[] currencyCodes = null, DateTime? date = null)
        {
            try
            {
                var model = GetExchangeRatesDomainModel(currencyCodes, date);
                if (model != null)
                {
                    return model.Rates;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
