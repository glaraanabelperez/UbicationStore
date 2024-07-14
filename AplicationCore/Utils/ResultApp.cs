

namespace AplicationCore.Utils
{
    public class ResultApp :IResultApp
    {
        public bool? Succeeded { get; set; }
        public ErrorResult? errors { get; set; } 
        public string? message { get; set; }
        public object? objectResult { get; set; }

        public ResultApp GetResult()
        {
            return this;
        }
        public void Send(bool succeeded, string? message_, ErrorResult error, object? data)
        {
            Succeeded = succeeded;
            errors = error;
            message = message_;
            objectResult = data;
        }
    }
}
