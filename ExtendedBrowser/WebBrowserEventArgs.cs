/*
 *  Copyright © 2016 Sage Software, Inc.
 */

using System;
using System.ComponentModel;

namespace Sage.WebAuthenticationBroker.ExtendedBrowser
{
    /// <summary>
    /// Event class for BeforeNavigate2.
    /// </summary>
    public class BeforeNavigate2EventArgs : CancelEventArgs
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dispatchObject">The dispatch object for the browser control.</param>
        /// <param name="url">The url address to navigate to.</param>
        /// <param name="flags">The flags for the navigation.</param>
        /// <param name="targetFrameName">The name of the frame in which the resource will be displayed.</param>
        /// <param name="postData">Data to send to the server if the HTTP POST transaction is being used.</param>
        /// <param name="headers">Value that specifies the additional HTTP headers to send to the server (HTTP URLs only).</param>
        /// <param name="cancel">The cancellation flag.</param>
        public BeforeNavigate2EventArgs(object dispatchObject, object url, object flags, object targetFrameName, object postData, object headers, ref bool cancel) : base(cancel)
        {
            DispatchObject = dispatchObject;
            Url = url;
            Flags = flags;
            TargetFrameName = targetFrameName;
            PostData = postData;
            Headers = headers;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// The dispatch object.
        /// </summary>
        public object DispatchObject { get; set; }

        /// <summary>
        /// The url address that was loaded.
        /// </summary>
        public object Url { get; private set; }

        /// <summary>
        /// The flags for the navigation.
        /// </summary>
        public object Flags { get; private set; }

        /// <summary>
        /// The name of the frame in which the resource will be displayed.
        /// </summary>
        public object TargetFrameName { get; private set; }

        /// <summary>
        /// Data to send to the server if the HTTP POST transaction is being used.
        /// </summary>
        public object PostData { get; private set; }

        /// <summary>
        /// Value that specifies the additional HTTP headers to send to the server (HTTP URLs only).
        /// </summary>
        public object Headers { get; private set; }

        #endregion
    }

    /// <summary>
    /// Event class for the NavigateError event.
    /// </summary>
    public class NavigateErrorEventArgs : CancelEventArgs
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dispatchObject">The dispatch object for the browser control.</param>
        /// <param name="url">The url address that failed to load.</param>
        /// <param name="targetFrameName">The name of the frame in which the resource will be displayed.</param>
        /// <param name="statusCode">The HTTP status code for the error.</param>
        /// <param name="cancel">The cancellation flag.</param>
        public NavigateErrorEventArgs(object dispatchObject, object url, object targetFrameName, object statusCode, ref bool cancel) : base(cancel)
        {
            DispatchObject = dispatchObject;
            Url = url;
            TargetFrameName = targetFrameName;
            StatusCode = statusCode;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// The dispatch object.
        /// </summary>
        public object DispatchObject { get; set; }

        /// <summary>
        /// The url address that was loaded.
        /// </summary>
        public object Url { get; private set; }

        /// <summary>
        /// The name of the frame in which the resource will be displayed.
        /// </summary>
        public object TargetFrameName { get; private set; }

        /// <summary>
        /// The HTTP status code for the error.
        /// </summary>
        public object StatusCode { get; private set; }

        #endregion
    }

    /// <summary>
    /// Event class for NewWindow2.
    /// </summary>
    public class NewWindow2EventArgs : CancelEventArgs
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dispatchObject">The dispatch object for the browser control.</param>
        /// <param name="cancel">The cancellation flag.</param>
        public NewWindow2EventArgs(ref object dispatchObject, ref bool cancel)
            : base(cancel)
        {
            DispatchObject = dispatchObject;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// The dispatch object.
        /// </summary>
        public object DispatchObject { get; set; }

        #endregion
    }

    /// <summary>
    /// Event class for NewWindow3.
    /// </summary>
    public class NewWindow3EventArgs : NewWindow2EventArgs
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dispatchObject">The dispatch object for the browser control.</param>
        /// <param name="cancel">The cancellation flag.</param>
        public NewWindow3EventArgs(ref object dispatchObject, ref bool cancel) : base(ref dispatchObject, ref cancel)
        {
        }

        /// <summary>
        /// The URL of the page that is opening the new page.
        /// </summary>
        public string UrlContext { get; set; }

        /// <summary>
        /// The URL of the page that is about to be opened.
        /// </summary>
        public string Url { get; set; }
    }

    /// <summary>
    /// Event class for DocumentComplete.
    /// </summary>
    public class DocumentCompleteEventArgs : EventArgs
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dispatchObject">The dispatch object for the browser control.</param>
        /// <param name="url">The url address that was loaded.</param>
        public DocumentCompleteEventArgs(object dispatchObject, object url)
        {
            DispatchObject = dispatchObject;
            Url = url;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// The dispatch object.
        /// </summary>
        public object DispatchObject { get; set; }

        /// <summary>
        /// The url address that was loaded.
        /// </summary>
        public object Url { get; set; }

        #endregion
    }

    /// <summary>
    /// Event class for CommandStateChange.
    /// </summary>
    public class CommandStateChangeEventArgs : EventArgs
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="command">The command state.</param>
        /// <param name="enable">True if enabled, otherwise false.</param>
        public CommandStateChangeEventArgs(long command, ref bool enable)
        {
            Command = command;
            Enable = enable;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// The command state.
        /// </summary>
        public long Command { get; set; }

        /// <summary>
        /// True if enabled, otherwise false.
        /// </summary>
        public bool Enable { get; set; }

        #endregion
    }

    /// <summary>
    /// Event class for TitleChange.
    /// </summary>
    public class TitleChangeEventArgs : EventArgs
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="title">The new title string.</param>
        public TitleChangeEventArgs(string title)
        {
            Title = title;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// The title string.
        /// </summary>
        public string Title { get; private set; }

        #endregion
    }
}
