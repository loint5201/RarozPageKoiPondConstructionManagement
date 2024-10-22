using System.Net;

namespace Domain.ResponseData
{
    public class ServiceResponse
    {
        public bool Success { get; set; } = true;
        public int StatusCode { get; set; } = (int)HttpStatusCode.OK;
        public string? Message { get; set; }
        public object? Data { get; set; }
        public DateTime ServerTime { get; set; } = DateTime.Now;
        public ServiceResponse OnSuccess(object? data = null)
        {
            if (data != null)
            {
                Data = data;
            }
            return this;
        }
        public ServiceResponse OnException(Exception ex)
        {
            if (ex != null)
            {
                Success = false;
                StatusCode = (int)HttpStatusCode.InternalServerError;
                Message = ex.Message;
            }
            return this;
        }
        public ServiceResponse OnError(object? data = null, string message = "Error while procees request.", int statusCode = (int)HttpStatusCode.BadRequest)
        {
            Success = false;
            StatusCode = statusCode;
            if (data != null)
            {
                Data = data;
            }
            if (string.IsNullOrEmpty(Message))
            {
                Message = message;
            }

            return this;
        }
        public ServiceResponse OnError(string message = "Error while procees request.", int statusCode = (int)HttpStatusCode.BadRequest)
        {
            Success = false;
            StatusCode = statusCode;
            if (string.IsNullOrEmpty(Message))
            {
                Message = message;
            }

            return this;
        }
    }
}
