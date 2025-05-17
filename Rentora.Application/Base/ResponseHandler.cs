using System.Net;

namespace Rentora.Application.Base
{

    public static class ResponseHandler
    {
        public static Response<T> Success<T>(T entity, string message = null, object meta = null)
        {
            return new Response<T>
            {
                Data = entity,
                StatusCode = HttpStatusCode.OK,
                Succeeded = true,
                Message = message ?? Messages.Success,
                Meta = meta
            };
        }

        public static Response<T> Created<T>(T entity, object meta = null)
        {
            var response = Success(entity, Messages.Created, meta);
            response.StatusCode = HttpStatusCode.Created;
            return response;
        }
        public static Response<T> Deleted<T>()
        {
            return CreateResponse<T>(HttpStatusCode.OK, true, Messages.Deleted);
        }

        public static Response<T> Unauthorized<T>()
        {
            return CreateResponse<T>(HttpStatusCode.Unauthorized, false, Messages.Unauthorized);
        }

        public static Response<T> BadRequest<T>(string message = null)
        {
            return CreateResponse<T>(HttpStatusCode.BadRequest, false, message ?? Messages.BadRequest);
        }

        public static Response<T> UnprocessableEntity<T>(string message = null)
        {
            return CreateResponse<T>(HttpStatusCode.UnprocessableEntity, false, message ?? Messages.Unprocessable);
        }

        public static Response<T> NotFound<T>(string message = null)
        {
            return CreateResponse<T>(HttpStatusCode.NotFound, false, message ?? Messages.NotFound);
        }

        public static Response<T> InternalServerError<T>(string message = null)
        {
            return CreateResponse<T>(HttpStatusCode.InternalServerError, false, message ?? Messages.InternalServerError);
        }

        // Helper method to reduce repetition
        private static Response<T> CreateResponse<T>(HttpStatusCode statusCode, bool succeeded, string message)
        {
            return new Response<T>
            {
                StatusCode = statusCode,
                Succeeded = succeeded,
                Message = message
            };
        }
    }
}
