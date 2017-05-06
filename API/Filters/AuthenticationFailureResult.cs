using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace API.Filters
{
    public class AuthenticationFailureResult : IHttpActionResult
    {

        /// <summary>
        /// Authentication failure result
        /// </summary>
        /// <param name="reasonPhrase">reason</param>
        /// <param name="request">Http request</param>
        public AuthenticationFailureResult(string reasonPhrase, HttpRequestMessage request)
        {
            ReasonPhrase = reasonPhrase;
            Request = request;
        }

        /// <summary>
        /// Gets or sets the Reason
        /// </summary>
        public string ReasonPhrase { get; private set; }

        /// <summary>
        /// Gets or sets the request
        /// </summary>
        public HttpRequestMessage Request { get; private set; }

        /// <summary>
        /// Execute error result
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }


        /// <summary>
        /// Add authentication failed reason and request to response
        /// </summary>
        /// <returns>Http Response</returns>
        private HttpResponseMessage Execute()
        {
            HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            response.RequestMessage = Request;
            response.ReasonPhrase = ReasonPhrase;
            return response;
        }
    }
}