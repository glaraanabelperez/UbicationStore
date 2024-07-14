using AplicationCore.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

using System.Net;

namespace Utils.Exception
{
    public class ExceptionHandlerFilter : IExceptionFilter, IFilterMetadata
    {
        private IResultApp result;

        public ExceptionHandlerFilter(IResultApp result)
        {
            this.result = result;
        }
        public void OnException(ExceptionContext context)
        {
            //No Content
            if (context.Exception.Message.Contains("no contains elements"))
            {
                result.Send(false, "No Content", null, null);
                context.Result = new ObjectResult(result.GetResult())
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
                result.Send(false, "Error", ErrorResult, null);
                context.Result = new ObjectResult(result.GetResult())
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
           
        }

    }
}
