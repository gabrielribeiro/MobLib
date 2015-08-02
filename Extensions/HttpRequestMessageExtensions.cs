using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace MobLib.Extensions
{
    public static class HttpRequestMessageExtensions
    {
        /// <summary>
        /// Initializes a new instance of HttpResponseException that represents an error message for status code 201
        /// </summary>
        /// <param name="request">HTTP request message</param>
        /// <param name="type"></param>
        /// <returns>Returns an exception to break the executing action with correct status code.</returns>
        public static HttpResponseException Created(this HttpRequestMessage request, string type)
        {
            return new HttpResponseException(request.CreateErrorResponse(HttpStatusCode.Created, type + " created."));
        }

        /// <summary>
        /// Initializes a new instance of HttpResponseException that represents an error message for status code 204
        /// </summary>
        /// <param name="request">HTTP request message</param>
        /// <param name="message"></param>
        /// <returns>Returns an exception to break the executing action with correct status code.</returns>
        public static HttpResponseException NoContent(this HttpRequestMessage request, string message)
        {
            return new HttpResponseException(request.CreateErrorResponse(HttpStatusCode.NoContent, message));
        }

        /// <summary>
        /// Initializes a new instance of HttpResponseException that represents an error message for status code 304
        /// </summary>
        /// <param name="request">HTTP request message</param>
        /// <param name="message"></param>
        /// <returns>Returns an exception to break the executing action with correct status code.</returns>
        public static HttpResponseException NotModified(this HttpRequestMessage request, string message)
        {
            return new HttpResponseException(request.CreateErrorResponse(HttpStatusCode.NotModified, message));
        }

        /// <summary>
        /// Initializes a new instance of HttpResponseException that represents an error message for status code 400
        /// </summary>
        /// <param name="request">HTTP request message</param>
        /// <param name="model"></param>
        /// <returns>Returns an exception to break the executing action with correct status code.</returns>
        public static HttpResponseException BadRequest(this HttpRequestMessage request, ModelStateDictionary model)
        {
            return new HttpResponseException(request.CreateErrorResponse(HttpStatusCode.BadRequest, model));
        }

        /// <summary>
        /// Initializes a new instance of HttpResponseException that represents an error message for status code 400
        /// </summary>
        /// <param name="request">HTTP request message</param>
        /// <param name="message"></param>
        /// <returns>Returns an exception to break the executing action with correct status code.</returns>
        public static HttpResponseException BadRequest(this HttpRequestMessage request, string message)
        {
            return new HttpResponseException(request.CreateErrorResponse(HttpStatusCode.BadRequest, message));
        }

        /// <summary>
        /// Initializes a new instance of HttpResponseException that represents an error message for status code 401
        /// </summary>
        /// <param name="request">HTTP request message</param>
        /// <returns>Returns an exception to break the executing action with correct status code.</returns>
        public static HttpResponseException Unauthorized(this HttpRequestMessage request)
        {
            return new HttpResponseException(request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Go away!"));
        }

        /// <summary>
        /// Initializes a new instance of HttpResponseException that represents an error message for status code 409
        /// </summary>
        /// <param name="request">HTTP request message</param>
        /// <param name="message"></param>
        /// <returns>Returns an exception to break the executing action with correct status code.</returns>
        public static HttpResponseException Conflict(this HttpRequestMessage request, string message)
        {
            return new HttpResponseException(request.CreateErrorResponse(HttpStatusCode.Conflict, message));
        }

        /// <summary>
        /// Initializes a new instance of HttpResponseException that represents an error message for status code 500
        /// </summary>
        /// <param name="request">HTTP request message</param>
        /// <param name="exception"></param>
        /// <returns>Returns an exception to break the executing action with correct status code.</returns>
        public static HttpResponseException InternalServerError(this HttpRequestMessage request, Exception exception)
        {
            return new HttpResponseException(request.CreateErrorResponse(HttpStatusCode.InternalServerError, exception.Message));
        }

        /// <summary>
        /// Initializes a new instance of HttpResponseException that represents an error message for status code 501
        /// </summary>
        /// <param name="request">HTTP request message</param>
        /// <param name="methodName"></param>
        /// <returns>Returns an exception to break the executing action with correct status code.</returns>
        public static HttpResponseException NotImplemented(this HttpRequestMessage request, string methodName)
        {
            return new HttpResponseException(request.CreateErrorResponse(HttpStatusCode.NotImplemented, methodName + " not implemented."));
        }


        /// <summary>
        /// Initializes a new instance of HttpResponseException that represents an error message for status code 200
        /// </summary>
        /// <param name="request">HTTP request message</param>
        /// <returns>Returns an exception to break the executing action with correct status code.</returns>
        public static HttpResponseException OK(this HttpRequestMessage request)
        {
            return new HttpResponseException(request.CreateResponse(HttpStatusCode.OK, "Result Ok."));
        }
    }
}
