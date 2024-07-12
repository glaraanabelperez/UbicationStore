using AplicationCore.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

using System.Net;

namespace Utils.Exception
{
    public class ExceptionHandlerFilter : IExceptionFilter, IFilterMetadata
    {
        public void OnException(ExceptionContext context)
        {

            //No Content
            if (context.Exception.Message.Contains("no contains elements"))
            {
                var Result = new ResultApp();
                    Result.message = "No Content";
                context.Result = new ObjectResult(Result)
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
            else
            {
            //Error Result
                var ErrorResult = ExceptionHandler.CreateResult(context.Exception);
                if (context.HttpContext.Response.StatusCode >= 400)
                    ErrorResult.tittle = "Aplication Error";
                else
                    ErrorResult.tittle = "Aplication";
                ErrorResult.status = context.HttpContext.Response.StatusCode.ToString();
                context.Result = new ObjectResult(ErrorResult)
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
           
        }

    }
}
