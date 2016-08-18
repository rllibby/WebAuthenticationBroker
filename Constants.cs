/*
 *  Copyright © 2016 Sage Software, Inc.
 */

using System.Drawing;

namespace System.WebAuthenticationBroker
{
    /// <summary>
    /// Internal class for maintaing constant values used in the oAuth process.
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// Default client back color.
        /// </summary>
        public static Color BackColor = Color.White;

        /// <summary>
        /// Default client forecolor.
        /// </summary>
        public static Color ForeColor = Color.Black;

        /// <summary>
        /// Default login form width.
        /// </summary>
        public const int ClientWidth = 650;

        /// <summary>
        /// Default login form hheight.
        /// </summary>
        public const int ClientHeight = 580;

        /// <summary>
        /// Emulation key for the browser component.
        /// </summary>
        public const string EmulationKey = @"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION";

        /// <summary>
        /// Error message to display when oAuth navigation fails due to HTTP code.
        /// </summary>
        public const string NavError = "Authentication failed with HTTP status code {0}.";

        /// <summary>
        /// Error message to display when oAuth navigation fails due to navigation error code.
        /// </summary>
        public const string InetError = "Authentication failed with navigation error code {0}.";

        /// <summary>
        /// The media type for application form encoded data.
        /// </summary>
        public const string FormMediaType = "application/x-www-form-urlencoded";
    }
}
