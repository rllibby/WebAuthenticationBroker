/*
 *  Copyright © 2016 Sage Software, Inc.
 */

using System;
using System.Windows.Forms;

namespace Sage.WebAuthenticationBroker
{
    /// <summary>
    /// Window wrapper for unmanaged window handles.
    /// </summary>
    public class WindowWrapper :IWin32Window
    {
        #region Private fields

        private IntPtr _hwnd;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="handle">The window handle to wrap.</param>
        public WindowWrapper(IntPtr handle)
        {
            _hwnd = handle;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// The wrapped window handle.
        /// </summary>
        public IntPtr Handle
        {
            get { return _hwnd; }
        }

        #endregion
    }
}
