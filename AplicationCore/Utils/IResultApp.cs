using AplicationCore.Models;

namespace AplicationCore.Utils
{
    public interface IResultApp
    {
        public bool? Succeeded { get; set; }
        public ErrorResult? errors { get; set; }
        public string? message { get; set; }
        public object? objectResult { get; set; }
        public ResultApp GetResult();
        public void Send(bool succeeded, string? message, ErrorResult error, object? data);

    }
}
