using Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace API.Filters
{
    public class MyAuthenticationFilter : Attribute, IAuthenticationFilter
    {

        #region Authentication Filter members
        /// <summary>
        /// Gets AllowMultiple
        /// </summary>
        public bool AllowMultiple { get; }

        /// <summary>
        /// Authenticate
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            //look for credentials in the request.
            HttpRequestMessage request = context.Request;
            AuthenticationHeaderValue authenticationHeader = request.Headers.Authorization;

            Tuple<string, string> userNameAndPasword = ParseAuthorizationHeader(authenticationHeader);

            if (userNameAndPasword == null)
            {
                //context.ErrorResult = new AuthenticationFailureResult(BusinessValidationMessage.ApiAuthenticationMissingCredentials, request);
            }
            else
            {
                //get username and password
                string userName = userNameAndPasword.Item1;
                string password = userNameAndPasword.Item2;
                //create principle
                IPrincipal principal = await CreatePrincipal(userName, password);
                if (principal == null)
                {
                   // context.ErrorResult = new AuthenticationFailureResult(BusinessValidationMessage.ApiAuthenticationInvalidCredentials, request);
                }
                //If the credentials are valid, set principal.
                else
                {
                    context.Principal = principal;
                }
            }

        }

        /// <summary>
        /// Challenge
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        //public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        //{
        //    var challenge = new AuthenticationHeaderValue(Constant.BasicAuthResponseHeaderValue);
        //    context.Result = new AddChallengeOnUnauthorizedResult(challenge, context.Result);
        //    return Task.FromResult(0);
        //}

        #endregion

        #region Private methods

        /// <summary>
        /// Private method to parse Base64 encoded username/password
        /// </summary>
        /// <param name="authHeader"></param>
        /// <returns></returns>
        private Tuple<string, string> ParseAuthorizationHeader(AuthenticationHeaderValue authenticationHeader)
        {
            Tuple<string, string> credentialDetails = null;

            //if authentication header is available 
            if (authenticationHeader != null && authenticationHeader.Scheme.Equals(Constant.BasicAuthResponseHeaderValue) &&
                !string.IsNullOrEmpty(authenticationHeader.Parameter))
            {
                // Decode and split authentication header parameter
                string[] credentials = System.Text.Encoding.ASCII.GetString(Convert.FromBase64String(authenticationHeader.Parameter)).Split(new[] { ':' });
                // Check authentication header is in the correct format
                if (credentials.Length == 2 && !string.IsNullOrEmpty(credentials[0]) && !string.IsNullOrEmpty(credentials[1]))
                {
                    credentialDetails = new Tuple<string, string>(credentials[0], credentials[1]);
                }
            }
            return credentialDetails;



        }


        /// <summary>
        /// Creates the principal.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        private Task<IPrincipal> CreatePrincipal(string username, string password)
        {
            return Task.Run(() =>
            {
                IPrincipal principle;

                if (!ValidateUser(username, password))
                    return null;

                var identity = new GenericIdentity(username, Constant.BasicAuthResponseHeaderValue);

                principle = new GenericPrincipal(identity, null);
                return principle;
            }
            );

        }


        /// <summary>
        /// Validates the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        private bool ValidateUser(string userName, string password)
        {
            string apiUserName = ConfigurationManager.AppSettings[Constant.ApiUserNameKey];
            string apiPassword = ConfigurationManager.AppSettings[Constant.ApiPasswordKey];

            return (apiUserName.Equals(userName) && apiPassword.Equals(password));

        }

        #endregion

    }
}