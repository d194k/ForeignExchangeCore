using Forex.DomainModels.Common;
using Forex.DomainModels.Integration;
using Forex.Infrastructure;
using Forex.Services.FixerService;
using Forex.Services.ForexService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Forex.WebApi.Controllers
{
    [Route("forex/api")]
    [ApiController]
    public class ForexController : ControllerBase
    {
        private readonly IFixerService _fixerService;
        private readonly IForexService _forexService;

        public ForexController(IFixerService fixerService, IForexService forexService)
        {
            _fixerService = fixerService;
            _forexService = forexService;
        }

        [Route("exchange")]
        [HttpPost]
        public APIResponseMessage CurrencyConversion([FromBody] CurrencyConversionDomainModel model)
        {
            HttpStatusCode statusCode = HttpStatusCode.OK;
            string message = string.Empty;
            dynamic result = null;
            try
            {
                result = _fixerService.CurrencyConversion(model);
            }
            catch (Exception ex)
            {
                APIHelper.GetErrorAPIResponseMessage(ref ex, ref statusCode, ref message);
            }
            return APIHelper.CreateAPIResponseMessage(statusCode, message, result);
        }

        [Route("ratehistory")]
        [HttpGet]
        public APIResponseMessage CurrencyConversion(string currency = null, string from = null, string to = null)
        {
            HttpStatusCode statusCode = HttpStatusCode.OK;
            string message = string.Empty;
            dynamic result = null;
            try
            {
                result = _forexService.GetRateHistory(currency, from, to);
            }
            catch (Exception ex)
            {
                APIHelper.GetErrorAPIResponseMessage(ref ex, ref statusCode, ref message);
            }
            return APIHelper.CreateAPIResponseMessage(statusCode, message, result);
        }
    }
}
