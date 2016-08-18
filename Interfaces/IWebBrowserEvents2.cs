/*
 *  Copyright © 2016 Sage Software, Inc.
 */

using System.Runtime.InteropServices;
using System.Security;

namespace Sage.WebAuthenticationBroker.Interfaces
{
    /// <summary>
    /// Interface for the second version of browser events.
    /// </summary>
    [ComImport]
    [SuppressUnmanagedCodeSecurity]
    [Guid("34A715A0-6587-11D0-924A-0020AFC7AC4D")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [TypeLibType(TypeLibTypeFlags.FHidden)]
    public interface IWebBrowserEvents2
    {
        /// <summary>
        /// Event handler for BeforeNavigate2 event.
        /// </summary>
        /// <param name="pDisp">The dispatch object for the browser control.</param>
        /// <param name="url">The url address to navigate to.</param>
        /// <param name="flags">The flags for the navigation.</param>
        /// <param name="targetFrameName">The name of the frame in which the resource will be displayed.</param>
        /// <param name="postData">Data to send to the server if the HTTP POST transaction is being used.</param>
        /// <param name="headers">Value that specifies the additional HTTP headers to send to the server (HTTP URLs only).</param>
        /// <param name="cancel">The cancellation flag.</param>
        [DispId(0xfa)]
        void BeforeNavigate2([In, MarshalAs(UnmanagedType.IDispatch)] object pDisp, [In] ref object url, [In] ref object flags, [In] ref object targetFrameName, [In] ref object postData, [In] ref object headers, [In, Out, MarshalAs(UnmanagedType.VariantBool)] ref bool cancel);

        /// <summary>
        /// Event handler for the CommandStateChange event.
        /// </summary>
        /// <param name="command">The command state.</param>
        /// <param name="enable">True if enabled, otherwise false.</param>
        [DispId(0x69)]
        void CommandStateChange([In] long command, [In] bool enable);

        /// <summary>
        /// Event handler for the TitleChange event.
        /// </summary>
        /// <param name="text">The new title string.</param>
        [DispId(0x71)]
        void TitleChange([In, MarshalAs(UnmanagedType.BStr)] string text);

        /// <summary>
        /// Event handler for the DocumentComplete event.
        /// </summary>
        /// <param name="pDisp">The dispatch object for the browser control.</param>
        /// <param name="url">The url address that was loaded.</param>
        [DispId(0x103)]
        void DocumentComplete([In, MarshalAs(UnmanagedType.IDispatch)] object pDisp, [In] ref object url);

        /// <summary>
        /// Event handler for the NavigateError event.
        /// </summary>
        /// <param name="pDisp">The dispatch object for the browser control.</param>
        /// <param name="url">The url address that failed to loaded.</param>
        /// <param name="frame">The name of the frame.</param>
        /// <param name="statusCode">The status code for the error.</param>
        /// <param name="cancel">True to cancel the navigation to an error page, otherwise false.</param>
        [DispId(0x10f)]
        void NavigateError([In, MarshalAs(UnmanagedType.IDispatch)] object pDisp, [In] ref object url, [In] ref object frame, [In] ref object statusCode, [In, Out] ref bool cancel);

        /// <summary>
        /// DownloadComplete handler.
        /// </summary>
        [DispId(0x68)]
        void DownloadComplete();

        /// <summary>
        /// Event handler for NewWindow2 event.
        /// </summary>
        /// <param name="pDisp">The dispatch object for the browser control.</param>
        /// <param name="cancel">The cancellation flag.</param>
        [DispId(0xfb)]
        void NewWindow2([In, Out, MarshalAs(UnmanagedType.IDispatch)] ref object pDisp, [In, Out] ref bool cancel);

        /// <summary>
        /// Event handler for NewWindow3 event.
        /// </summary>
        /// <param name="pDisp">The dispatch object for the browser control.</param>
        /// <param name="cancel">The cancellation flag.</param>
        /// <param name="dwFlags">The flags for the new window event.</param>
        /// <param name="bstrUrlContext">The url of the page requesting the new window.</param>
        /// <param name="bstrUrl">The url of the page that will be opened.</param>
        [DispId(0x111)]
        void NewWindow3([In, Out, MarshalAs(UnmanagedType.IDispatch)] ref object pDisp, [In, Out] ref bool cancel, [In] uint dwFlags, [In] string bstrUrlContext, [In] string bstrUrl);
    }
}
