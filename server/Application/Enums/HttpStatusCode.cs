namespace Application.Enums
{
    public enum HttpStatusCodes
    {
        // Informational
        Continue = 100,
        SwitchingProtocols = 101,

        // Success
        OK = 200,
        Created = 201,
        Accepted = 202,

        // Redirection
        Ambiguous = 300,
        MovedPermanently = 301,
        Found = 302,
        NotModified = 304,

        // Client Errors
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        Conflict = 409,

        // Server Errors
        InternalServerError = 500,
        NotImplemented = 501,
        BadGateway = 502,
        ServiceUnavailable = 503
    }
}
