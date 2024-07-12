
using AplicationCore.Utils;

namespace Utils.Exception
{
    public static class ExceptionHandler
    {
        public static ErrorResult CreateResult(System.Exception exception)
        {
            ErrorResult errorResult = new ErrorResult();
            List<string> list = new List<string>();
            string key = "";
            if(exception.InnerException != null)
            {
                errorResult.traceId = exception.InnerException.GetHashCode().ToString();
                errorResult.message = exception.InnerException.Message;

            }else
            {
                errorResult.traceId = exception.GetHashCode().ToString();
                errorResult.message = exception.Message;
            }

            //if (errorResult.message.Contains("no contains elements"))
            //{
            //    errorResult.status = "400";
            //    key = "No se encuentran los registros buscados";
            //    list.Add(errorResult.message);
            //}
            if (errorResult.message.Contains("Cannot insert duplicate key in object"))
            {
                errorResult.status = "452";
                key = "Ya existe un registro similar. Revisar valor: " ;
                list.Add(errorResult.message);
            }
            else if (errorResult.message.StartsWith("Invalid column"))
            {
                errorResult.status = "480";
                key = "Invalid Column, Revisar valor: ";
                list.Add(errorResult.message);
            }
            else if (errorResult.message.Contains("is not supported in calendar "))
            {
                errorResult.message.Substring(errorResult.message.IndexOf("'"), 11);
                errorResult.status = "481";
                key = "Formato de fecha no válido: Revisar valor: ";
                list.Add("Revisar valor: " + errorResult.message);
            }
            else
            {
                errorResult.status = "500";
            }


            if (list.Count> 0) { errorResult.result.Add(key, list); }
           
            return errorResult;
        }
    }
}
