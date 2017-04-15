using Common;
using Refit;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MVC.Refit
{
    public class ApiAuthenticationHeaderAttribute: HeadersAttribute
    {
        /// <summary>
        /// Appends authorization header attribute details from configuration
        /// </summary>
        public ApiAuthenticationHeaderAttribute() : base(SetAuthenticationSettingsFromConfiguration())
        {

        }

        private static string SetAuthenticationSettingsFromConfiguration()
        {
            //get API user name and password from configuration
            string apiUserName = ConfigurationManager.AppSettings[Constant.ApiUserNameKey];
            string apiPassword = ConfigurationManager.AppSettings[Constant.ApiPasswordKey];

            //set the header values
            string headerValues = $"Authorization:{Constant.BasicAuthResponseHeaderValue} {Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{apiUserName}:{apiPassword}"))}";
            return headerValues;
        }
    }

}
