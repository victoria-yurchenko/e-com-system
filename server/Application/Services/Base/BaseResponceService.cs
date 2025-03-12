using Application.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Base
{
    public abstract class BaseResponseService<T> : ObjectResult where T : class
    {
        public string Message { get; set; }
        public int ResponseStatusCode { get; protected set; }
        public T? Data { get; protected set; }

        public BaseResponseService(int statusCode, string message, T? data)
            : base(new { StatusCode = statusCode, ErrorMessage = message, Data = data })
        {
            ResponseStatusCode = statusCode;
            Message = message;
            Data = data;
        }
    }
}
