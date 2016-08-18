/*
 *  Copyright © 2016 Sage Software, Inc.
 */

using System;

namespace Sage.WebAuthenticationBroker
{
    /// <summary>
    /// Authentication options.
    /// </summary>
    [Flags]
    public enum WebAuthenticationOptions : uint
    {
        /// <summary>
        /// No options are requested.
        /// </summary>
        None = 0,

        /// <summary>
        /// Not supported.
        /// </summary>
        SilentMode = 1,

        /// <summary>
        /// Tells the web authentication broker to return the window title string of the webpage in the ResponseData property.
        /// </summary>
        UseTitle = 2
    }
}
