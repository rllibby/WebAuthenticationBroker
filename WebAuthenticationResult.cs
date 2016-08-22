/*
 *  Copyright © 2016 Sage Software, Inc.
 */

using System.Collections.Specialized;
using System.Web;

namespace Sage.WebAuthenticationBroker
{
    /// <summary>
    /// Class that indicates the result of the authentication operation.
    /// </summary>
    public class WebAuthenticationResult
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public WebAuthenticationResult()
        {
            ResponseData = string.Empty;
            ResponseErrorDetail = 0;
            ResponseStatus = WebAuthenticationStatus.UserCancel;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="data">The response data from the oAuth call.</param>
        /// <param name="error">The http error code.</param>
        /// <param name="status">The authentication status.</param>
        public WebAuthenticationResult(string data, int error, WebAuthenticationStatus status)
        {
            ResponseData = data;
            ResponseErrorDetail = error;
            ResponseStatus = status;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Parse the response data which is normally the redirect uri + the query string into name/value pairs.
        /// </summary>
        /// <returns>The collection of name/value pairs</returns>
        public NameValueCollection ParseResponse()
        {
            if (string.IsNullOrEmpty(ResponseData)) return new NameValueCollection();

            var query = ResponseData.IndexOf('?');

            return (query < 0) ? new NameValueCollection() : HttpUtility.ParseQueryString(ResponseData.Substring(query + 1));
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Contains the protocol data when the operation successfully completes.
        /// </summary>
        public string ResponseData { get; private set; }

        /// <summary>
        /// Returns the HTTP error code when ResponseStatus is equal to WebAuthenticationStatus.ErrorHttp. 
        /// This is only available if there is an error.
        /// </summary>
        public int ResponseErrorDetail { get; private set; }

        /// <summary>
        /// Contains the status of the operation when it completes.
        /// </summary>
        public WebAuthenticationStatus ResponseStatus { get; private set; }

        #endregion
    }
}
