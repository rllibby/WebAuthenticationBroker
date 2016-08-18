/*
 *  Copyright © 2016 Sage Software, Inc.
 */

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Sage.WebAuthenticationBroker.Interfaces;

namespace Sage.WebAuthenticationBroker.ExtendedBrowser
{
    /// <summary>
    /// Extended version of the WebBrowser control.
    /// </summary>
    public class Browser : WebBrowser
    {
        #region Private fields

        private AxHost.ConnectionPointCookie _cookie;
        private WebBrowserExtendedEvents _events;

        #endregion

        #region Private classes

        /// <summary>
        /// Implementation for the browser events 2 interface.
        /// </summary>
        private class WebBrowserExtendedEvents : StandardOleMarshalObject, IWebBrowserEvents2
        {
            #region Private fields

            private readonly Browser _browser;

            #endregion

            #region Constructor

            /// <summary>
            /// Constructor.
            /// </summary>
            /// <param name="browser">The browser control to bind to.</param>
            public WebBrowserExtendedEvents(Browser browser)
            {
                _browser = browser;
            }

            #endregion

            #region Public methods

            /// <summary>
            /// Event handler for the TitleChange event.
            /// </summary>
            /// <param name="title">The new title string.</param>
            public void TitleChange(string title)
            {
                _browser.OnTitleChange(title);
            }

            /// <summary>
            /// Event handler for the DownloadComplete event.
            /// </summary>
            public void DownloadComplete()
            {
                _browser.OnDownloadComplete();
            }

            /// <summary>
            /// Event handler for the NavigateError event.
            /// </summary>
            /// <param name="dispatchObject">The dispatch object for the browser control.</param>
            /// <param name="url">The url address that failed to load.</param>
            /// <param name="targetFrameName">The name of the frame in which the resource will be displayed.</param>
            /// <param name="statusCode">The HTTP status code for the error.</param>
            /// <param name="cancel">The cancellation flag.</param>
            public void NavigateError(object dispatchObject, ref object url, ref object targetFrameName, ref object statusCode, ref bool cancel)
            {
                _browser.OnNavigateError(dispatchObject, url, targetFrameName, statusCode, ref cancel);
            }

            /// <summary>
            /// Event handler for BeforeNavigate2 event.
            /// </summary>
            /// <param name="dispatchObject">The dispatch object for the browser control.</param>
            /// <param name="url">The url address to navigate to.</param>
            /// <param name="flags">The flags for the navigation.</param>
            /// <param name="targetFrameName">The name of the frame in which the resource will be displayed.</param>
            /// <param name="postData">Data to send to the server if the HTTP POST transaction is being used.</param>
            /// <param name="headers">Value that specifies the additional HTTP headers to send to the server (HTTP URLs only).</param>
            /// <param name="cancel">The cancellation flag.</param>
            public void BeforeNavigate2(object dispatchObject, ref object url, ref object flags, ref object targetFrameName, ref object postData, ref object headers, ref bool cancel)
            {
                _browser.OnBeforeNavigate2(dispatchObject, url, flags, targetFrameName, postData, headers, ref cancel);
            }

            /// <summary>
            /// Event handler for NewWindow2 event.
            /// </summary>
            /// <param name="dispatchObject">The dispatch object for the browser control.</param>
            /// <param name="cancel">The cancellation flag.</param>
            public void NewWindow2(ref object dispatchObject, ref bool cancel)
            {
                _browser.OnNewWindow2(ref dispatchObject, ref cancel);
            }

            /// <summary>
            /// Event handler for NewWindow3 event.
            /// </summary>
            /// <param name="dispatchObject">The dispatch object for the browser control.</param>
            /// <param name="cancel">The cancellation flag.</param>
            /// <param name="dwFlags">The flags for the new window event.</param>
            /// <param name="bstrUrlContext">The url of the page requesting the new window.</param>
            /// <param name="bstrUrl">The url of the page that will be opened.</param>
            public void NewWindow3(ref object dispatchObject, ref bool cancel, uint dwFlags, string bstrUrlContext, string bstrUrl)
            {
                _browser.OnNewWindow3(ref dispatchObject, ref cancel, dwFlags, bstrUrlContext, bstrUrl);
            }

            /// <summary>
            /// Event handler for the DocumentComplete event.
            /// </summary>
            /// <param name="dispatchObject">The dispatch object for the browser control.</param>
            /// <param name="url">The url address that was loaded.</param>
            public void DocumentComplete(object dispatchObject, ref object url)
            {
                _browser.OnDocumentComplete(dispatchObject, url);
            }

            /// <summary>
            /// Event handler for the CommandStateChange event.
            /// </summary>
            /// <param name="command">The command state.</param>
            /// <param name="enable">True if enabled, otherwise false.</param>
            public void CommandStateChange(long command, bool enable)
            {
                _browser.OnCommandStateChange(command, ref enable);
            }

            #endregion
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Creates the connection point sink.
        /// </summary>
        protected override void CreateSink()
        {
            base.CreateSink();

            _events = new WebBrowserExtendedEvents(this);
            _cookie = new AxHost.ConnectionPointCookie(ActiveXInstance, _events, typeof(IWebBrowserEvents2));
        }

        /// <summary>
        /// Detaches from the connection point container.
        /// </summary>
        protected override void DetachSink()
        {
            try
            {
                if (_cookie == null) return;

                _cookie.Disconnect();
                _cookie = null;
            }
            finally
            {
                base.DetachSink();
            }
        }

        /// <summary>
        /// DocumentComplete handler.
        /// </summary>
        /// <param name="dispatchObject">The dispatch object for the browser control.</param>
        /// <param name="url">The url address that was loaded.</param>
        protected void OnDocumentComplete(object dispatchObject, object url)
        {
            var handler = DocumentComplete;

            if (handler == null) return;

            handler(this, new DocumentCompleteEventArgs(dispatchObject, url));
        }

        /// <summary>
        /// Event handler for the TitleChange event.
        /// </summary>
        /// <param name="title">The new title string.</param>
        protected void OnTitleChange(string title)
        {
            var handler = TitleChange;

            if (handler == null) return;

            handler(this, new TitleChangeEventArgs(title));
        }

        /// <summary>
        /// NewWindow3 handler.
        /// </summary>
        /// <param name="dispatchObject">The dispatch object for the browser control.</param>
        /// <param name="cancel">The cancellation flag.</param>
        /// <param name="dwFlags">The flags for the new window event.</param>
        /// <param name="bstrUrlContext">The url of the page requesting the new window.</param>
        /// <param name="bstrUrl">The url of the page that will be opened.</param>
        protected void OnNewWindow3(ref object dispatchObject, ref bool cancel, uint dwFlags, string bstrUrlContext, string bstrUrl)
        {
            var handler = NewWindow3;

            if (handler == null) return;

            var args = new NewWindow3EventArgs(ref dispatchObject, ref cancel)
            {
                UrlContext = bstrUrlContext,
                Url = bstrUrl
            };

            handler(this, args);

            cancel = args.Cancel;
            dispatchObject = args.DispatchObject;

        }

        /// <summary>
        /// NewWindow2 handler.
        /// </summary>
        /// <param name="dispatchObject">The dispatch object for the browser control.</param>
        /// <param name="cancel">The cancel flag.</param>
        protected void OnNewWindow2(ref object dispatchObject, ref bool cancel)
        {
            var handler = NewWindow2;

            if (handler == null) return;

            var args = new NewWindow2EventArgs(ref dispatchObject, ref cancel);

            handler(this, args);

            cancel = args.Cancel;
            dispatchObject = args.DispatchObject;
        }

        /// <summary>
        /// BeforeNavigate2 handler.
        /// </summary>
        /// <param name="dispatchObject">The dispatch object for the browser control.</param>
        /// <param name="url">The url address to navigate to.</param>
        /// <param name="flags">The flags for the navigation.</param>
        /// <param name="targetFrameName">The name of the frame in which the resource will be displayed.</param>
        /// <param name="postData">Data to send to the server if the HTTP POST transaction is being used.</param>
        /// <param name="headers">Value that specifies the additional HTTP headers to send to the server (HTTP URLs only).</param>
        /// <param name="cancel">The cancellation flag.</param>
        protected void OnBeforeNavigate2(object dispatchObject, object url, object flags, object targetFrameName, object postData, object headers, ref bool cancel)
        {
            var handler = BeforeNavigate2;

            if (handler == null) return;

            var args = new BeforeNavigate2EventArgs(dispatchObject, url, flags, targetFrameName, postData, headers, ref cancel);

            handler(this, args);

            cancel = args.Cancel;
        }

        /// <summary>
        /// Event handler for the NavigateError event.
        /// </summary>
        /// <param name="dispatchObject">The dispatch object for the browser control.</param>
        /// <param name="url">The url address that failed to loaded.</param>
        /// <param name="targetFrameName">The name of the frame.</param>
        /// <param name="statusCode">The status code for the error.</param>
        /// <param name="cancel">True to cancel the navigation to an error page, otherwise false.</param>
        protected void OnNavigateError(object dispatchObject, object url, object targetFrameName, object statusCode, ref bool cancel)
        {
            var handler = NavigateError;

            if (handler == null) return;

            var args = new NavigateErrorEventArgs(dispatchObject, url, targetFrameName, statusCode, ref cancel);

            handler(this, args);

            cancel = args.Cancel;
        }

        /// <summary>
        /// DownloadComplete handler.
        /// </summary>
        protected void OnDownloadComplete()
        {
            var handler = DownloadComplete;

            if (handler == null) return;

            handler(this, EventArgs.Empty);
        }

        /// <summary>
        /// CommandStateChange handler.
        /// </summary>
        /// <param name="command">The command state.</param>
        /// <param name="enable">True if enabled, otherwise false.</param>
        protected void OnCommandStateChange(long command, ref bool enable)
        {
            var handler = CommandStateChange;

            if (handler == null) return;

            handler(this, new CommandStateChangeEventArgs(command, ref enable));
        }

        #endregion

        #region Public events

        /// <summary>
        /// TitleChange event.
        /// </summary>
        public event EventHandler<TitleChangeEventArgs> TitleChange;

        /// <summary>
        /// BeforeNavigate2 event.
        /// </summary>
        public event EventHandler<BeforeNavigate2EventArgs> BeforeNavigate2;

        /// <summary>
        /// NavigateError event.
        /// </summary>
        public event EventHandler<NavigateErrorEventArgs> NavigateError;

        /// <summary>
        /// NewWindow2 event.
        /// </summary>
        public event EventHandler<NewWindow2EventArgs> NewWindow2;

        /// <summary>
        /// NewWindow3 event.
        /// </summary>
        public event EventHandler<NewWindow3EventArgs> NewWindow3;

        /// <summary>
        /// DocumentComplete event.
        /// </summary>
        public event EventHandler<DocumentCompleteEventArgs> DocumentComplete;

        /// <summary>
        /// DownloadComplete event.
        /// </summary>
        public event EventHandler DownloadComplete;

        /// <summary>
        /// CommandStateChange event.
        /// </summary>
        public event EventHandler<CommandStateChangeEventArgs> CommandStateChange;

        #endregion

        #region Public properties

        /// <summary>
        /// Application object
        /// </summary>
        public object Application
        {
            get
            {
                var axWebBrowser = ActiveXInstance as IWebBrowser2;

                return (axWebBrowser != null) ? axWebBrowser.Application : null;
            }
        }

        #endregion
    }
}
