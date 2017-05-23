using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace MazeGUI.etc
{
    /// <summary>
    /// a window with no X button on the top
    /// </summary>
    public partial class NonClosableWindow : Window
    {
        // cancel the control box of the window
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        /// <summary>
        /// event to cancel the X button
        /// </summary>
        /// <param name="sender">the window</param>
        /// <param name="e">Routed event args</param>
        private void singlePlayerMazeWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }
    }
}
