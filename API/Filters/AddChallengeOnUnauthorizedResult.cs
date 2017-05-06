using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace API.Filters
{
    public class AddChallengeOnUnauthorizedResult : IHttpActionResult
    {
        /// <summary>
        /// Adds an authentication challenge to the HTTP response
        /// </summary>
        /// <param name="challenge">Authentication header</param>
        /// <param name="innerResult">httpAction result</param>
        public AddChallengeOnUnauthorizedResult(AuthenticationHeaderValue challenge, IHttpActionResult innerResult)
        {
            Challenge = challenge;
            InnerResult = innerResult;
        }

        /// <summary>
        /// Gets or sets the Authentication header
        /// </summary>
        public AuthenticationHeaderValue Challenge { get; private set; }

        /// <summary>
        /// Gets or sets the HttpAction result
        /// </summary>
        public IHttpActionResult InnerResult { get; private set; }


        /// <summary>
        /// Execute challenge
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            //get http response
            HttpResponseMessage response = await InnerResult.ExecuteAsync(cancellationToken);

            //if unauthorized
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Only add one challenge per authentication scheme.
                if (!response.Headers.WwwAuthenticate.Any((h) => h.Scheme == Challenge.Scheme))
                {
                    response.Headers.WwwAuthenticate.Add(Challenge);
                }
            }
            //return response
            return response;
        }
    }
}