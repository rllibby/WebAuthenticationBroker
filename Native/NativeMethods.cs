/*
 *  Copyright © 2016 Sage Software, Inc.
 */

using System;
using System.Runtime.InteropServices;

namespace Sage.WebAuthenticationBroker.Native
{
    [StructLayout(LayoutKind.Sequential)]
    // ReSharper disable once InconsistentNaming
    public struct MARGINS                          
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="lw">Left width value.</param>
        /// <param name="rw">Right width value.</param>
        /// <param name="th">Top height value.</param>
        /// <param name="bh">Bottom height value.</param>
        public MARGINS(int lw, int rw, int th, int bh)
        {
            leftWidth = lw;
            rightWidth = rw;
            topHeight = th;
            bottomHeight = bh;
        }

        #endregion

        #region Public fields

        public int leftWidth;
        public int rightWidth;
        public int topHeight;
        public int bottomHeight;

        #endregion
    }

    /// <summary>
    /// Class to maintain the native Win API methods being used by the launcher.
    /// </summary>
    public static class NativeMethods
    {
        #region PInvoke private imports

        [DllImport("dwmapi.dll", EntryPoint = "DwmExtendFrameIntoClientArea", SetLastError = true)]
        internal static extern int NativeDwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

        [DllImport("dwmapi.dll", EntryPoint = "DwmSetWindowAttribute", SetLastError = true)]
        internal static extern int NativeDwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("dwmapi.dll", EntryPoint = "DwmIsCompositionEnabled", SetLastError = true)]
        internal static extern int NativeDwmIsCompositionEnabled(ref int pfEnabled);

        [DllImport("user32.dll", EntryPoint = "IsWindow")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool NativeIsWindow(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "GetDesktopWindow", SetLastError = false)]
        internal static extern IntPtr NativeGetDesktopWindow();

        [DllImportAttribute("user32.dll", EntryPoint = "ReleaseCapture", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool NativeReleaseCapture();

        [DllImport("user32.dll", EntryPoint = "SendMessageA", CharSet = CharSet.Ansi)]
        internal static extern IntPtr NativeSendMessage(IntPtr hWnd, UInt32 msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "GetActiveWindow", CharSet = CharSet.Ansi)]
        internal static extern IntPtr NativeGetActiveWindow();

        #endregion

        // ReSharper disable InconsistentNaming
        #region Public constants

        /* Win32 class styles */
        public const int CS_DROPSHADOW = 0x00020000;

        /* Window message constants */
        public const uint WM_NCLBUTTONDOWN = 0x00A1;
        public const int WM_NCPAINT = 0x0085;

        /* NC hit test constants */
        public const int HT_CAPTION = 0x0002;

        #endregion

        // ReSharper restore InconsistentNaming
        #region Public methods

        /// <summary>
        /// Extends the window frame into the client area.
        /// </summary>
        /// <param name="hWnd">The handle to the window in which the frame will be extended into the client area.</param>
        /// <param name="pMarInset">A pointer to a MARGINS structure that describes the margins to use when extending the frame into the client area.</param>
        /// <returns>If this function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
        public static int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset)
        {
            return NativeDwmExtendFrameIntoClientArea(hWnd, ref pMarInset);
        }

        /// <summary>
        /// Sets the value of non-client rendering attributes for a window.
        /// </summary>
        /// <param name="hwnd">The handle to the window that will receive the attributes.</param>
        /// <param name="attr"> A single DWMWINDOWATTRIBUTE flag to apply to the window.</param>
        /// <param name="attrValue">A pointer to the value of the attribute specified in the dwAttribute parameter.</param>
        /// <param name="attrSize">The size, in bytes, of the value type pointed to by the pvAttribute parameter.</param>
        /// <returns>If this function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
        public static int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize)
        {
            return NativeDwmSetWindowAttribute(hwnd, attr, ref attrValue, attrSize);
        }

        /// <summary>
        /// Obtains a value that indicates whether Desktop Window Manager (DWM) composition is enabled.
        /// </summary>
        /// <param name="pfEnabled">A pointer to a value that, when this function returns successfully.</param>
        /// <returns>If this function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
        public static int DwmIsCompositionEnabled(ref int pfEnabled)
        {
            return NativeDwmIsCompositionEnabled(ref pfEnabled);
        }

        /// <summary>
        /// Releases the mouse capture from a window in the current thread and restores normal mouse input processing. 
        /// </summary>
        /// <returns>If the function succeeds, the return value is nonzero.</returns>
        public static bool ReleaseCapture()
        {
            return NativeReleaseCapture();
        }

        /// <summary>
        /// Retrieves the window handle to the desktop window.
        /// </summary>
        /// <returns>The return value is the handle to the desktop window.</returns>
        public static IntPtr GetDesktopWindow()
        {
            return NativeGetDesktopWindow();
        }

        /// <summary>
        /// Retrieves the window handle to the active window attached to the calling thread's message queue. 
        /// </summary>
        /// <returns>The return value is the handle to the active window attached to the calling thread's message queue. Otherwise, the return value is NULL.</returns>
        public static IntPtr GetActiveWindow()
        {
            return NativeGetActiveWindow();
        }

        /// <summary>
        /// Public wrapper method around the native API call.
        /// </summary>
        /// <param name="hWnd">The window handle to validate.</param>
        /// <returns>True if the window exists, otherwise false.</returns>
        public static bool IsWindow(IntPtr hWnd)
        {
            return NativeIsWindow(hWnd);
        }

        /// <summary>
        /// Public wrapper method around the native API call.
        /// </summary>
        /// <param name="hWnd">The window handle to send the message to.</param>
        /// <param name="msg">The message to send.</param>
        /// <param name="wParam">The wParam for the message.</param>
        /// <param name="lParam">The lParam for the message.</param>
        /// <returns>Depends on the message type being processed.</returns>
        public static IntPtr SendMessage(IntPtr hWnd, UInt32 msg, IntPtr wParam, IntPtr lParam)
        {
            return IsWindow(hWnd) ? NativeSendMessage(hWnd, msg, wParam, lParam) : IntPtr.Zero;
        }

        #endregion
    }
}