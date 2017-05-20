﻿using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace MazeGUI.etc
{
    public class NonClosableWindow : Window
    {
        // cancel the control box of the window
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private void singlePlayerMazeWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }

        protected static Dictionary<Key, Direction> directions = new Dictionary<Key, Direction>
        {
            { Key.Up, Direction.Up },
            { Key.Down, Direction.Down},
            { Key.Left, Direction.Left},
            { Key.Right, Direction.Right}
        };
    }
}