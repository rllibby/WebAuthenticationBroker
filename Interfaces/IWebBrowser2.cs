/*
 *  Copyright © 2016 Sage Software, Inc.
 */

using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Sage.WebAuthenticationBroker.Interfaces
{
    /// <summary>
    /// Interface for extended version of browser control.
    /// </summary>
    [ComImport, Guid("D30C1661-CDAF-11d0-8A3E-00C04FC9E26E"), TypeLibType(TypeLibTypeFlags.FOleAutomation | TypeLibTypeFlags.FDual | TypeLibTypeFlags.FHidden)]
    public interface IWebBrowser2
    {
        /// <summary>
        /// Method to navigate back to the previous page.
        /// </summary>
        [DispId(100)]
        void GoBack();

        /// <summary>
        /// Method to navigate forward to the prior page.
        /// </summary>
        [DispId(0x65)]
        void GoForward();

        /// <summary>
        /// Method to navigate to the defined home page.
        /// </summary>
        [DispId(0x66)]
        void GoHome();

        /// <summary>
        /// Navigates to the current search page.
        /// </summary>
        [DispId(0x67)]
        void GoSearch();

        /// <summary>
        /// Navigates to a resource identified by a URL or to a file identified by a full path. 
        /// </summary>
        /// <param name="url">Specifies the URL.</param>
        /// <param name="flags">Specifies the flags.</param>
        /// <param name="targetFrameName">Specifies the flags.</param>
        /// <param name="postData">Specifies the post data.</param>
        /// <param name="headers">Specifies the headers.</param>
        [DispId(0x68)]
        void Navigate([In] string url, [In] ref object flags, [In] ref object targetFrameName, [In] ref object postData, [In] ref object headers);

        /// <summary>
        /// Reloads the file that is currently displayed in the object.
        /// </summary>
        [DispId(-550)]
        void Refresh();

        /// <summary>
        /// Reloads the file that is currently displayed with the specified refresh level. 
        /// </summary>
        /// <param name="level">Specifies the refresh level.</param>
        [DispId(0x69)]
        void Refresh2([In] ref object level);

        /// <summary>
        /// Cancels a pending navigation or download, and stops dynamic page elements, such as background
        /// sounds and animations. 
        /// </summary>
        [DispId(0x6a)]
        void Stop();

        /// <summary>
        /// Gets the automation object for the application that is hosting the WebBrowser Control.
        /// </summary>
        [DispId(200)]
        object Application { [return: MarshalAs(UnmanagedType.IDispatch)] get; }

        /// <summary>
        /// Gets the parent of the object.
        /// </summary>
        [DispId(0xc9)]
        object Parent { [return: MarshalAs(UnmanagedType.IDispatch)] get; }

        /// <summary>
        /// Gets an object reference to a container.
        /// </summary>
        [DispId(0xca)]
        object Container { [return: MarshalAs(UnmanagedType.IDispatch)] get; }

        /// <summary>
        /// Gets the automation object of the active document, if any.
        /// </summary>
        [DispId(0xcb)]
        object Document { [return: MarshalAs(UnmanagedType.IDispatch)] get; }

        /// <summary>
        /// Gets the automation object of the active document, if any.
        /// </summary>
        [DispId(0xcc)]
        bool TopLevelContainer { get; }

        /// <summary>
        /// Gets the user type name of the contained document object.
        /// </summary>
        [DispId(0xcd)]
        string Type { get; }

        /// <summary>
        /// Gets or sets the coordinate of the left edge of the object.
        /// </summary>
        [DispId(0xce)]
        int Left { get; set; }

        /// <summary>
        /// Gets or gets the coordinates of the top edge of the object.
        /// </summary>
        [DispId(0xcf)]
        int Top { get; set; }

        /// <summary>
        /// Gets or sets the width of the object.
        /// </summary>
        [DispId(0xd0)]
        int Width { get; set; }

        /// <summary>
        /// Gets or sets the height of the object.
        /// </summary>
        [DispId(0xd1)]
        int Height { get; set; }

        /// <summary>
        /// Retrieves the path or title of the resource that is currently displayed.
        /// </summary>
        [DispId(210)]
        string LocationName { get; }

        /// <summary>
        /// Gets the URL of the resource that is currently displayed.
        /// </summary>
        [DispId(0xd3)]
        string LocationURL { get; }

        /// <summary>
        /// Gets a value that indicates whether the object is engaged in a navigation or downloading
        /// operation.
        /// </summary>
        [DispId(0xd4)]
        bool Busy { get; }

        /// <summary>
        /// Closes the object.
        /// </summary>
        [DispId(300)]
        void Quit();

        /// <summary>
        /// Computes the full size of the browser window when given the specified width and height of the content area. 
        /// </summary>
        /// <param name="pcx">Specifies the width of the content area.</param>
        /// <param name="pcy">Specifies the height of the content area.</param>
        [DispId(0x12d)]
        void ClientToWindow(out int pcx, out int pcy);

        /// <summary>
        /// Associates a user-defined name/value pair with the object. 
        /// </summary>
        /// <param name="property">Specifies the property name.</param>
        /// <param name="vtValue">Specifies the property value.</param>
        [DispId(0x12e)]
        void PutProperty([In] string property, [In] object vtValue);

        /// <summary>
        /// Gets the value associated with a user-defined property name. 
        /// </summary>
        /// <param name="property">Specifies the property name.</param>
        /// <returns>The property value.</returns>
        [DispId(0x12f)]
        object GetProperty([In] string property);

        /// <summary>
        /// Retrieves the frame name or application name of the object.
        /// </summary>
        [DispId(0)]
        string Name { get; }

        /// <summary>
        /// Gets the handle of the Internet Explorer main window.
        /// </summary>
        [DispId(-515)]
        int HWND { get; }

        /// <summary>
        /// Retrieves the fully qualified path of the Windows Internet Explorer executable.
        /// </summary>
        [DispId(400)]
        string FullName { get; }

        /// <summary>
        /// Retrieves the system folder of the Internet Explorer executable.
        /// </summary>
        [DispId(0x191)]
        string Path { get; }
        
        /// <summary>
        /// Gets or sets a value that indicates whether the object is visible or hidden.
        /// </summary>
        [DispId(0x192)]
        bool Visible { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the status bar for the object is visible.
        /// </summary>
        [DispId(0x193)]
        bool StatusBar { get; set; }

        /// <summary>
        /// Gets or sets the text in the status bar for the object.
        /// </summary>
        [DispId(0x194)]
        string StatusText { get; set; }

        /// <summary>
        /// Gets or sets the toolbars for the object.
        /// </summary>
        [DispId(0x195)]
        int ToolBar { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the Internet Explorer menu bar is visible.
        /// </summary>
        [DispId(0x196)]
        bool MenuBar { get; set; }

        /// <summary>
        /// Sets or gets a value that indicates whether Internet Explorer is in full-screen mode or
        /// normal window mode.
        /// </summary>
        [DispId(0x197)]
        bool FullScreen { get; set; }

        /// <summary>
        /// Navigates the browser to a location that might not be expressed as a URL, such as a pointer
        /// to an item identifier list (PIDL) for an entity in the Windows Shell namespace. 
        /// </summary>
        /// <param name="url">Specifies the URL.</param>
        /// <param name="flags">Specifies the flags.</param>
        /// <param name="targetFrameName">Specifies the flags.</param>
        /// <param name="postData">Specifies the post data.</param>
        /// <param name="headers">Specifies the headers.</param>
        [DispId(500)]
        void Navigate2([In] ref object url, [In] ref object flags, [In] ref object targetFrameName, [In] ref object postData, [In] ref object headers);

        /// <summary>
        /// Shows or hides a specified browser bar. 
        /// </summary>
        /// <param name="pvaClsid">The CLSID of the browser bar to show or hide.</param>
        /// <param name="pvarShow">True to show, false to hide.</param>
        /// <param name="pvarSize">The size of the bar.</param>
        [DispId(0x1f7)]
        void ShowBrowserBar([In] ref object pvaClsid, [In] ref object pvarShow, [In] ref object pvarSize);

        /// <summary>
        /// Gets the ready state of the object.
        /// </summary>
        [DispId(-525)]
        WebBrowserReadyState ReadyState { get; }

        /// <summary>
        /// Gets or sets a value that indicates whether the object is operating in offline mode.
        /// </summary>
        [DispId(550)]
        bool Offline { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the object can display dialog boxes.
        /// </summary>
        [DispId(0x227)]
        bool Silent { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the object is registered as a top-level browser
        /// window.
        /// </summary>
        [DispId(0x228)]
        bool RegisterAsBrowser { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the object is registered as a drop target for 
        /// navigation.
        /// </summary>
        [DispId(0x229)]
        bool RegisterAsDropTarget { get; set; }

        /// <summary>
        /// Gets or sets a value that specifies whether the object is in theater mode.
        /// </summary>
        [DispId(0x22a)]
        bool TheaterMode { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the address bar of the object is visible or hidden.
        /// </summary>
        [DispId(0x22b)]
        bool AddressBar { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the object can be resized.
        /// </summary>
        [DispId(0x22c)]
        bool Resizable { get; set; }
    }
}
