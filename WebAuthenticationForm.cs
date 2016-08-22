/*
 *  Copyright © 2016 Sage Software, Inc.
 */

using Sage.WebAuthenticationBroker.ExtendedBrowser;
using Sage.WebAuthenticationBroker.Native;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.WebAuthenticationBroker;
using System.Windows.Forms;

namespace Sage.WebAuthenticationBroker
{
    /// <summary>
    /// The dialog used to perform the oAuth login authentication.
    /// </summary>
    public partial class WebAuthenticationForm : Form
    {
        #region Private fields

        private WebAuthenticationResult _result = new WebAuthenticationResult();
        private Browser _browser;
        private string _requestUri;
        private string _callbackUri;
        private bool _useWebTitle;
        private bool _isAeroEnabled;

        #endregion

        #region Private methods

        /// <summary>
        /// Determines if Aero composition is enabled for this window.
        /// </summary>
        /// <returns>True if Aero composition is enabled, otherwise false.</returns>
        private static bool CheckAeroEnabled()
        {
            var enabled = 0;

            return (Environment.OSVersion.Version.Major < 6) ? false : ((NativeMethods.DwmIsCompositionEnabled(ref enabled) == 0) && (enabled != 0));
        }

        /// <summary>
        /// Determines if the address is the final uri that signals the end of the oAuth process. 
        /// </summary>
        /// <param name="value">The url address to evaluate.</param>
        /// <param name="args">The cancel event argument to flag if this is the final navigation.</param>
        /// <returns>True if this is the final address. On success, the response will be set.</returns>
        private bool IsFinal(string address, CancelEventArgs args)
        {
            Debug.WriteLine("{0}", address);

            if (string.IsNullOrEmpty(address)) return false;
            if (!address.StartsWith(_callbackUri, StringComparison.OrdinalIgnoreCase)) return false;
            if (args != null) args.Cancel = true;

            _result = new WebAuthenticationResult(address, 0, WebAuthenticationStatus.Success);

            DialogResult = DialogResult.OK;            

            return true;
        }

        /// <summary>
        /// Event that is triggered on form load.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnFormLoad(object sender, EventArgs e)
        {
            InitializeBrowser();

            BackColor = WebAuthenticationBroker.BackColor;
            ForeColor = WebAuthenticationBroker.ForeColor;
            Caption.ForeColor = ForeColor;
            CloseButton.BackColor = BackColor;
            CloseButton.SetBounds(ClientSize.Width - 32, 0, 30, 30);

            _browser.SetBounds(1, 36, Width - 2, Height - 37);
            _browser.Navigate(_requestUri, false);
        }

        /// <summary>
        /// Event that is triggered the form is closing.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            WaitState.HideFinal();
        }

        /// <summary>
        /// Event that is triggered when the mouse is over the close button.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnMouseEnter(object sender, EventArgs e)
        {
            CloseButton.BackColor = Color.Red;
        }

        /// <summary>
        /// Event that is triggered when the mouse is no longer over the close button.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnMouseLeave(object sender, EventArgs e)
        {
            CloseButton.BackColor = BackColor;
        }

        /// <summary>
        /// Event that is triggered when the close button needs to be drawn.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void DrawCloseButton(object sender, PaintEventArgs e)
        {
            const int offset = 10;

            var rect = CloseButton.ClientRectangle;
            var down = CloseButton.Capture;
            var hover = CloseButton.RectangleToScreen(rect).Contains(Cursor.Position);
            var pen = new Pen((hover || down )? Color.White : ForeColor, 1.7f);
            var color = CloseButton.BackColor;

            if (down) color = Color.FromArgb(255, 241, 112, 122);
            
            e.Graphics.Clear(color);
            e.Graphics.DrawLine(pen, new Point(rect.Left + offset, rect.Top + offset), new PointF(rect.Right - offset, rect.Bottom - offset));
            e.Graphics.DrawLine(pen, new Point(rect.Left + offset, rect.Bottom - offset), new PointF(rect.Right - offset, rect.Top + offset));
        }

        /// <summary>
        /// Event that is triggered when the close button is clicked.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnCloseClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Event that is triggered when the left mouse down is pressed over the header/caption controls.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnHeaderMouseDown(object sender, MouseEventArgs e)
        {
            NativeMethods.ReleaseCapture();
            NativeMethods.SendMessage(Handle, NativeMethods.WM_NCLBUTTONDOWN, new IntPtr(NativeMethods.HT_CAPTION), IntPtr.Zero);
        }

        /// <summary>
        /// Event that is triggered when the form needs to be painted.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnFormPaint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Color.FromArgb(200, 147, 147, 147), ButtonBorderStyle.Solid);
        }

        /// <summary>
        /// Event that is triggered before the browser loads the page url.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnBeforeNavigate2(object sender, BeforeNavigate2EventArgs e)
        {
            if (IsFinal(e.Url.ToString(), e)) return;
        }

        /// <summary>
        /// Event that is triggered when the browser is navigating.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnNavigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (IsFinal(e.Url.AbsoluteUri, e)) return;

            WaitState.Show();
        }

        /// <summary>
        /// Event that is triggered before the browser finished loading a page.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnDocumentComplete(object sender, DocumentCompleteEventArgs e)
        {
            try
            {
                if (IsFinal(e.Url.ToString(), null) || !_useWebTitle) return;
                
                var title = _browser.DocumentTitle;

                if (!string.IsNullOrEmpty(title)) Caption.Text = title; 
            }
            finally
            {
                WaitState.HideFinal();
            }
        }

        /// <summary>
        /// Event that is triggered when the browser fails to navigate to the specified url.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnNavigateError(object sender, NavigateErrorEventArgs e)
        {
            var code = (int)e.StatusCode;

            e.Cancel = true;

            if (IsFinal(e.Url.ToString(), e)) return;
            if (code < 0) throw new ApplicationException(string.Format(Constants.InetError, code));

            _result = new WebAuthenticationResult(string.Empty, code, WebAuthenticationStatus.ErrorHttp);
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Initializes the extended browser component.
        /// </summary>
        private void InitializeBrowser()
        {
            _browser = new Browser
            {
                Anchor = AnchorStyles.None,
                Location = new Point(30, 66),
                Margin = new Padding(3, 4, 3, 4),
                Name = "Browser",
                AllowWebBrowserDrop = false,
                ScriptErrorsSuppressed = true,
                IsWebBrowserContextMenuEnabled = false,
                ScrollBarsEnabled = true,
            };

            _browser.Navigating += OnNavigating;
            _browser.BeforeNavigate2 += OnBeforeNavigate2;
            _browser.DocumentComplete += OnDocumentComplete;
            _browser.NavigateError += OnNavigateError;

            Controls.Add(_browser);
        }

        /// <summary>
        /// Set the window frame margins for aero drawing.
        /// </summary>
        private void SetWindowMargins()
        {
            if (!_isAeroEnabled) return;

            var attr = 2;
            var margins = new MARGINS(1, 1, 1, 1);

            NativeMethods.DwmSetWindowAttribute(Handle, 2, ref attr, 4);
            NativeMethods.DwmExtendFrameIntoClientArea(Handle, ref margins);
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Window message handler.
        /// </summary>
        /// <param name="m">The current message to process.</param>
        protected override void WndProc(ref Message m)
        {
            try
            {
                if (m.Msg == NativeMethods.WM_NCPAINT) SetWindowMargins();
            }
            finally
            {
                base.WndProc(ref m);
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public WebAuthenticationForm(string requestUri, string callbackUri)
        {
            if (string.IsNullOrEmpty(requestUri)) throw new ArgumentNullException("requestUri");
            if (string.IsNullOrEmpty(callbackUri)) throw new ArgumentNullException("callbackUri");

            _requestUri = requestUri;
            _callbackUri = callbackUri;

            WaitState.Show();

            InitializeComponent();

            Width = WebAuthenticationBroker.Width;
            Height = WebAuthenticationBroker.Height;
        }

        #endregion

        #region Protected properties

        /// <summary>
        /// Returns the creation parameters.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                _isAeroEnabled = CheckAeroEnabled();

                var cp = base.CreateParams;

                if (!_isAeroEnabled) cp.ClassStyle |= NativeMethods.CS_DROPSHADOW;

                return cp;
            }
        }
        
        #endregion

        #region Public properties

        /// <summary>
        /// Returns the result of the authentication process.
        /// </summary>
        public WebAuthenticationResult Result
        {
            get { return _result; }
        }

        /// <summary>
        /// True if the web page title will be used.
        /// </summary>
        public bool UseWebTitle
        {
            get { return _useWebTitle; }
            set { _useWebTitle = value; }
        }

        #endregion
    }
}