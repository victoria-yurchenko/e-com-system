using Application.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Base
{
    public abstract class BaseResponseService<T> : ObjectResult
    {
        public int StatusCode { get; protected set; }
        public bool Success { get; protected set; }
        public string ErrorMessage { get; protected set; }
        public T Data { get; protected set; }

        protected BaseResponseService(HttpStatusCodes statusCode, string errorMessage = null, T data = default)
        : base(new { success = statusCode >= HttpStatusCodes.OK && statusCode < HttpStatusCodes.Ambiguous, errorMessage, data })
        {
            StatusCode = (int)statusCode;
        }
    }
}
