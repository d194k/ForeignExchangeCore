using Forex.DomainModels.Common;
using System;
using System.Net;

namespace Forex.Infrastructure
{
    public static class APIHelper
    {
        public static APIResponseMessage CreateAPIResponseMessage(HttpStatusCode statusCode = HttpStatusCode.BadRequest, string message = null, dynamic result = null)
        {
            return new APIResponseMessage() { StatusCode = (int)statusCode, StatusDescription = statusCode.ToString(), Message = message, Result = result };
        }

        public static void GetErrorAPIResponseMessage(ref Exception ex, ref HttpStatusCode statusCode, ref string message)
        {
            statusCode = HttpStatusCode.BadRequest;
            message = ex.Message;
        }
    }
}
