/*
 *  Copyright © 2016 Sage Software, Inc.
 */

namespace Sage.WebAuthenticationBroker
{
    /// <summary>
    /// Contains the status of the authentication operation.
    /// </summary>
    public enum WebAuthenticationStatus
    {
        /// <summary>
        /// The operation succeeded.
        /// </summary>
        Success,

        /// <summary>
        /// The operation was cancelled by the user.
        /// </summary>
        UserCancel,

        /// <summary>
        /// The operation failed because a specific HTTP error was returned.
        /// </summary>
        ErrorHttp
    }
}
