﻿using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Journey.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is JourneyException)
            {
                var journeyException  = (JourneyException)context.Exception;
                
                context.HttpContext.Response.StatusCode = (int)journeyException.GetStatusCode();
                
                var responseJson = new ResponseErrorsJson(journeyException.GetErrorMessages());
                
                context.Result = new ObjectResult(responseJson);
            }
            else
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var responseJson = new ResponseErrorsJson(new List<string> { ResourceErrorMessages.ERROR_STATUS_CODE_500  });

                context.Result = new ObjectResult(responseJson);
            }            
        }
    }
}
