/*
 *  Copyright © 2016 Sage Software, Inc.
 */

using Microsoft.Win32;
using Sage.WebAuthenticationBroker.Native;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.WebAuthenticationBroker;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Sage.WebAuthenticationBroker
{
    /// <summary>
    /// An oAuth authentication broker for legacy desktop and WPF applications.
    /// </summary>
    public static class WebAuthenticationBroker
    {
        #region Private fields

        private static Color _backColor = Constants.BackColor;
        private static Color _foreColor = Constants.ForeColor;
        private static IntPtr _mainHandle;
        private static bool _initialized;
        private static int _width;
        private static int _height;

        #endregion

        #region Private methods

        /// <summary>
        /// Initialization code for the authentication broker class. 
        /// </summary>
        private static void Initialize()
        {
            if (_initialized) return;

            _width = Constants.ClientWidth;
            _height = Constants.ClientHeight;

            try
            {
                Application.EnableVisualStyles();
                Application.VisualStyleState = VisualStyleState.ClientAndNonClientAreasEnabled;

                var process = Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);
                var processes = Process.GetProcesses();
                var processId = Process.GetCurrentProcess().Id;
                var p = processes.Where(x => x.Id == processId).FirstOrDefault();

                _mainHandle = (p == null) ? IntPtr.Zero : p.MainWindowHandle;

                using (var root = Registry.CurrentUser)
                {
                    using (var key = root.CreateSubKey(Constants.EmulationKey, RegistryKeyPermissionCheck.ReadWriteSubTree))
                    {
                        if (key == null) return;

                        var emulationMode = (int) key.GetValue(process, 0);

                        if (emulationMode >= 9000) return;

                        emulationMode = 11000;
                        key.SetValue(process, emulationMode, RegistryValueKind.DWord);
                    }
                }
            }
            finally
            {
                _initialized = true;
            }
        }

        /// <summary>
        /// Performs the authentication in a single threaded apartment.
        /// </summary>
        /// <param name="request">The authentication request.</param>
        /// <remarks>
        /// This method requires a thread marked as STA. The authentication method ensures that this will always happen, regardless of being
        /// called from the main UI thread or not.
        /// </remarks>
        private static void ThreadedAuthenticate(WebAuthenticationRequest request)
        {
            using (var authForm = new WebAuthenticationForm(request.RequestUri, request.CallbackUri))
            {
                authForm.UseWebTitle = (request.Options == WebAuthenticationOptions.UseTitle);
                authForm.ShowDialog(new WindowWrapper((_mainHandle == IntPtr.Zero) ? NativeMethods.GetDesktopWindow() : _mainHandle));

                request.Result = authForm.Result;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Static constructor.
        /// </summary>
        static WebAuthenticationBroker()
        {
            Initialize();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Starts the authentication operation.
        /// </summary>
        /// <param name="requestUri">The starting URI of the web service; this must be a secure address of https://.</param>
        /// <param name="callbackUri">The callback URI that indicates the completion of the web authentication.</param>
        /// <returns>The status and results of the authentication operation, throws on non HTTP error codes.</returns>
        public static WebAuthenticationResult Authenticate(string requestUri, string callbackUri)
        {
            return Authenticate(WebAuthenticationOptions.None, new Uri(requestUri), new Uri(callbackUri));
        }

        /// <summary>
        /// Starts the authentication operation.
        /// </summary>
        /// <param name="requestUri">The starting URI of the web service; this must be a secure address of https://.</param>
        /// <param name="callbackUri">The callback URI that indicates the completion of the web authentication.</param>
        /// <returns>The status and results of the authentication operation, throws on non HTTP error codes.</returns>
        public static WebAuthenticationResult Authenticate(Uri requestUri, Uri callbackUri)
        {
           return Authenticate(WebAuthenticationOptions.None, requestUri, callbackUri);
        }

        /// <summary>
        /// Starts the authentication operation.
        /// </summary>
        /// <param name="options">The authentication options</param>
        /// <param name="requestUri">The starting URI of the web service; this must be a secure address of https://.</param>
        /// <param name="callbackUri">The callback URI that indicates the completion of the web authentication.</param>
        /// <returns>The status and results of the authentication operation, throws on non HTTP error codes.</returns>
        public static WebAuthenticationResult Authenticate(WebAuthenticationOptions options, string requestUri, string callbackUri)
        {
            return Authenticate(options, new Uri(requestUri), new Uri(callbackUri));
        }

        /// <summary>
        /// Starts the authentication operation.
        /// </summary>
        /// <param name="options">The authentication options</param>
        /// <param name="requestUri">The starting URI of the web service; this must be a secure address of https://.</param>
        /// <param name="callbackUri">The callback URI that indicates the completion of the web authentication.</param>
        /// <returns>The status and results of the authentication operation, throws on non HTTP error codes.</returns>
        public static WebAuthenticationResult Authenticate(WebAuthenticationOptions options, Uri requestUri, Uri callbackUri)
        {
            var authenticate = AuthenticateAsync(options, requestUri, callbackUri);

            authenticate.WaitWithPumping();

            if (authenticate.IsCompleted) return authenticate.Result;
            if (authenticate.IsFaulted) throw authenticate.Exception;

            throw new ApplicationException("Authentication failed with an unknown exception.");
        }

        /// <summary>
        /// Starts the authentication operation.
        /// </summary>
        /// <param name="requestUri">The starting URI of the web service; this must be a secure address of https://.</param>
        /// <param name="callbackUri">The callback URI that indicates the completion of the web authentication.</param>
        /// <returns>The status and results of the authentication operation, throws on non HTTP error codes.</returns>
        public static async Task<WebAuthenticationResult> AuthenticateAsync(string requestUri, string callbackUri)
        {
            return await AuthenticateAsync(WebAuthenticationOptions.None, new Uri(requestUri), new Uri(callbackUri));
        }

        /// <summary>
        /// Starts the authentication operation.
        /// </summary>
        /// <param name="requestUri">The starting URI of the web service; this must be a secure address of https://.</param>
        /// <param name="callbackUri">The callback URI that indicates the completion of the web authentication.</param>
        /// <returns>The status and results of the authentication operation, throws on non HTTP error codes.</returns>
        public static async Task<WebAuthenticationResult> AuthenticateAsync(Uri requestUri, Uri callbackUri)
        {
            return await AuthenticateAsync(WebAuthenticationOptions.None, requestUri, callbackUri);
        }

        /// <summary>
        /// Starts the authentication operation.
        /// </summary>
        /// <param name="options">The authentication options</param>
        /// <param name="requestUri">The starting URI of the web service; this must be a secure address of https://.</param>
        /// <param name="callbackUri">The callback URI that indicates the completion of the web authentication.</param>
        /// <returns>The status and results of the authentication operation, throws on non HTTP error codes.</returns>
        public static async Task<WebAuthenticationResult> AuthenticateAsync(WebAuthenticationOptions options, string requestUri, string callbackUri)
        {
            return await AuthenticateAsync(WebAuthenticationOptions.None, new Uri(requestUri), new Uri(callbackUri));
        }

        /// <summary>
        /// Starts the authentication operation.
        /// </summary>
        /// <param name="options">The authentication options</param>
        /// <param name="requestUri">The starting URI of the web service; this must be a secure address of https://.</param>
        /// <param name="callbackUri">The callback URI that indicates the completion of the web authentication.</param>
        /// <returns>The status and results of the authentication operation, throws on non HTTP error codes.</returns>
        public static async Task<WebAuthenticationResult> AuthenticateAsync(WebAuthenticationOptions options, Uri requestUri, Uri callbackUri)
        {
            if (requestUri == null) throw new ArgumentNullException("requestUri");
            if (callbackUri == null) throw new ArgumentNullException("callbackUri");

            return await Task.Run(() =>
            {
                var request = new WebAuthenticationRequest(options, requestUri.AbsoluteUri, callbackUri.AbsoluteUri);
                var worker = new Thread(() => ThreadedAuthenticate(request));

                worker.SetApartmentState(ApartmentState.STA);
                worker.Start();
                worker.Join();

                return request.Result;
            });
        }

        /// <summary>
        /// Resets the static theme and size information to the default values.
        /// </summary>
        public static void SetDefaults()
            {
                _backColor = Constants.BackColor;
                _foreColor = Constants.ForeColor;
                _width = Constants.ClientWidth;
                _height = Constants.ClientHeight;
            }

        /// <summary>
        /// Sets the theme colors for the oAuth dialog.
        /// </summary>
        /// <param name="foreColor">The forecolor to use.</param>
        /// <param name="backColor">The backcolor to use.</param>
        public static void SetTheme(Color foreColor, Color backColor)
        {
            _foreColor = foreColor;
            _backColor = backColor;
        }

        /// <summary>
        /// Gets the current application callback uri.
        /// </summary>
        /// <returns>The current application callback uri.</returns>
        public static Uri GetCurrentApplicationCallbackUri()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Public properties

        /// <summary>
        /// The back color to apply to the oAuth login dialog.
        /// </summary>
        public static Color BackColor
        {
            get { return _backColor; }
            set { _backColor = value; }
        }

        /// <summary>
        /// The forecolor to apply to the oAuth dialog.
        /// </summary>
        public static Color ForeColor
        {
            get { return _foreColor; }
            set { _foreColor = value; }
        }

        /// <summary>
        /// The width of the login window.
        /// </summary>
        public static int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        /// <summary>
        /// The height of the login window.
        /// </summary>
        public static int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        #endregion
    }
}
