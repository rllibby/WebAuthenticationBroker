/*
 *  Copyright © 2016 Sage Software, Inc.
 */

using System.Windows.Forms;

namespace Sage.WebAuthenticationBroker
{
    /// <summary>
    /// Class for showing and hiding the application wait cursor using a ref counter.
    /// </summary>
    internal static class WaitState
    {
        #region Private fields

        private static int _refCount;

        #endregion

        #region Public methods

        /// <summary>
        /// Shows the wait cursor.
        /// </summary>
        public static void Show()
        {
            _refCount++;

            Application.UseWaitCursor = true;
        }

        /// <summary>
        /// Hides the wait cursor if the ref count hits zero
        /// </summary>
        public static void Hide()
        {
            if (_refCount == 0) return;

            _refCount--;

            if (_refCount == 0) Application.UseWaitCursor = false;
        }

        /// <summary>
        /// Hides the wait cursor after setting the ref count to zero.
        /// </summary>
        public static void HideFinal()
        {
            _refCount = 0;

            Application.UseWaitCursor = false;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// True if the cursor is active.
        /// </summary>
        public static bool Active
        {
            get { return (_refCount > 0); }
        }

        #endregion
    }
}
