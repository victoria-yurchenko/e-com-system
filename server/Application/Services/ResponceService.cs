using Application.Enums;
using Application.Services.Base;

namespace Application.Services
{
    public class ResponseService<T> : BaseResponseService<T>
    {
        public ResponseService(HttpStatusCodes statusCode, string errorMessage = null, T data = default)
            : base(statusCode, errorMessage, data) { }

        public ResponseService<T> SuccessResponse(T data, HttpStatusCodes statusCode = HttpStatusCodes.OK)
        {
            return new ResponseService<T>(statusCode, string.Empty, data);
        }

        public ResponseService<T> ClientErrorResponse(string errorMessage, HttpStatusCodes statusCode = HttpStatusCodes.BadRequest)
        {
            return new ResponseService<T>(statusCode, errorMessage);
        }

        public ResponseService<T> ServerErrorResponse(string errorMessage, HttpStatusCodes statusCode = HttpStatusCodes.InternalServerError)
        {
            return new ResponseService<T>(statusCode, errorMessage);
        }
    }
}
