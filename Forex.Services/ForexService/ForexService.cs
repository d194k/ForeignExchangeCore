using Forex.DomainModels.Integration;
using Forex.Entities.Models;
using Forex.Entities.Repository;
using Forex.Entities.UOW;
using Forex.Services.FixerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace Forex.Services.ForexService
{
    public class ForexService : IForexService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository _repository;
        private readonly IFixerService _fixerService;

        public ForexService(IRepository repository, IUnitOfWork unitOfWork, IFixerService fixerService)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _fixerService = fixerService;
        }

        #region Public Methods
        public bool SyncLatestExchangeRatesInDB()
        {
            bool result = false;
            try
            {
                var fixerModel = _fixerService.GetLatestExchangeRateDomainModel();
                var syncedExchangeRate = GetAllExchangeRateSyncHistory();
                if (fixerModel != null && fixerModel.Success)
                {
                    if (!_repository.Get<ExchangeRateSyncBase>(x => x.BaseCurrency == fixerModel.Base && x.SyncDate == fixerModel.Date).Any())
                    {
                        var exchangeRateSyncBase = new ExchangeRateSyncBase();
                        exchangeRateSyncBase.BaseCurrency = fixerModel.Base;
                        exchangeRateSyncBase.SyncDate = fixerModel.Date;
                        exchangeRateSyncBase.Timestamp = fixerModel.Timestamp;
                        exchangeRateSyncBase.CreatedDate = DateTime.Now;

                        _repository.Add(exchangeRateSyncBase);
                        _unitOfWork.Commit();

                        var rates = fixerModel.Rates.Properties();
                        var exchangeRateList = new List<ExchangeRate>();
                        foreach (var rate in rates)
                        {
                            var exchangeRate = new ExchangeRate();
                            exchangeRate.SyncId = exchangeRateSyncBase.Id;
                            exchangeRate.Currency = rate.Name;
                            exchangeRate.Rate = (decimal)rate.Value;
                            exchangeRate.CreatedDate = DateTime.Now;
                            exchangeRateList.Add(exchangeRate);
                        }
                        if (exchangeRateList.Any())
                        {
                            //exchangeRateSyncBase.ExchangeRate = exchangeRateList;
                            //_repository.Add(exchangeRateSyncBase);

                            _repository.AddRange(exchangeRateList);
                            _unitOfWork.Commit();
                            result = true;
                        }
                    }
                }
                else
                {
                    throw new Exception("Error with Fixer API");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public RateHistoryDomainModel GetRateHistory(string currencyCode, string from, string to)
        {
            RateHistoryDomainModel result = default;
            try
            {
                if (string.IsNullOrEmpty(currencyCode) || string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to))
                {
                    throw new Exception("Invalid request data");
                }
                //TODO RegEx change to be done
                var dateRegEx = new Regex(@"^\d{4}\-(0?[1-9]|1[012])\-(0?[1-9]|[12][0-9]|3[01])$");

                DateTime outFrom = DateTime.Now;
                DateTime outTo = DateTime.Now;
                if (_fixerService.CheckCurrencyCodeExists(currencyCode) && DateTime.TryParse(from, out outFrom) && DateTime.TryParse(to, out outTo) && outTo >= outFrom)
                {
                    var rateList = _repository.Get<ExchangeRateSyncBase>(x => x.IsDeleted == false && x.SyncDate >= outFrom && x.SyncDate <= outTo)
                        .Include(x => x.ExchangeRate)
                        .Select(x => new RateDomainModel
                        {
                            RecordDate = x.SyncDate,
                            ExchangeRate = x.ExchangeRate == null ? 0 : x.ExchangeRate.Single(y => y.Currency == currencyCode).Rate
                        }).OrderBy(x => x.RecordDate).ToList();
                    if (rateList.Any())
                    {
                        result = result ?? new RateHistoryDomainModel();
                        result.CurrencyCode = currencyCode.ToUpper();
                        result.FromDate = outFrom;
                        result.ToDate = outTo;
                        result.Records = rateList;
                    }
                }
                else
                {
                    throw new Exception("Invalid request data");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        #endregion

        #region Private Methods
        private List<ExchangeRateSyncBaseDomainModel> GetAllExchangeRateSyncHistory()
        {
            List<ExchangeRateSyncBaseDomainModel> result = default;
            try
            {
                var dbData = _repository.Get<ExchangeRateSyncBase>(x => x.IsDeleted == false);
                if (dbData != null)
                {
                    result = dbData.Select(x => new ExchangeRateSyncBaseDomainModel
                    {
                        Id = x.Id,
                        BaseCurrency = x.BaseCurrency,
                        SyncDate = x.SyncDate
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        #endregion
    }
}
