using Application.Enums;
using Application.Services.Base;

namespace Application.Services.ServerResponces
{
    public class ResponseService<T> : BaseResponseService<T> where T : class
    {
        public ResponseService() : base((int)HttpStatusCodes.OK, "", default) { }

        public ResponseService<T> SuccessResponse(T data, HttpStatusCodes statusCode = HttpStatusCodes.OK, string message = "Success")
        {
            return new ResponseService<T> { StatusCode = (int)statusCode, Data = data, Message = message };
        }

        public ResponseService<T> ClientErrorResponse(string errorMessage = "Bad Request", HttpStatusCodes statusCode = HttpStatusCodes.BadRequest, T? data = null)
        {
            return new ResponseService<T> { StatusCode = (int)statusCode, Data = data, Message = errorMessage };
        }

        public ResponseService<T> ServerErrorResponse(string errorMessage = "Internal Server Error", HttpStatusCodes statusCode = HttpStatusCodes.InternalServerError, T? data = null)
        {
            return new ResponseService<T> { StatusCode = (int)statusCode, Data = data, Message = errorMessage };
        }
    }
}
