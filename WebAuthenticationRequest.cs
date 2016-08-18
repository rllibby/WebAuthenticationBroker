/*
 *  Copyright © 2016 Sage Software, Inc.
 */

using System;

namespace Sage.WebAuthenticationBroker
{
    /// <summary>
    /// Wrapper for the authentication request.
    /// </summary>
    public class WebAuthenticationRequest
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="requestUri">The request address.</param>
        /// <param name="callbackUri">The callback address that signals completion.</param>
        public WebAuthenticationRequest(string requestUri, string callbackUri)
        {
            if (string.IsNullOrEmpty(requestUri)) throw new ArgumentNullException("requestUri");
            if (string.IsNullOrEmpty(callbackUri)) throw new ArgumentNullException("callbackUri");

            RequestUri = requestUri;
            CallbackUri = callbackUri;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// The request address.
        /// </summary>
        public string RequestUri { get; set; }

        /// <summary>
        /// The callback address that signals completion.
        /// </summary>
        public string CallbackUri { get; set; }

        /// <summary>
        /// The authentication result.
        /// </summary>
        public WebAuthenticationResult Result { get; set; }

        #endregion
    }
}
