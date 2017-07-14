using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFTest
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private const int WM_SYSKEYDOWN = 0x0104;
        private const int WM_SYSKEYUP = 0x0105;
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static int _hookID = 0;
        private delegate int LowLevelKeyboardProc(int nCode, int wParam, ref KBDLLHOOKSTRUCT IParam);
        private static int HookCallback(int nCode, int wParam, ref KBDLLHOOKSTRUCT IParam)
        {
            bool bReturn = false;
            switch (wParam)
            {
                case WM_KEYDOWN:
                case WM_KEYUP:
                case WM_SYSKEYDOWN:
                case WM_SYSKEYUP:

                    bReturn = ((IParam.vkCode == 0x09) && (IParam.flags == 0x20)) ||    //Alt + Tab
                        ((IParam.vkCode == 0x1B) && (IParam.flags == 0x20)) ||  //Alt + Esc
                        ((IParam.vkCode == 0x1B) && (IParam.flags == 0x00)) ||  //Ctrl + Esc
                        ((IParam.vkCode == 0x5B) && (IParam.flags == 0x01)) ||  //Left Windows Key
                        ((IParam.vkCode == 0x5C) && (IParam.flags == 0x01)) ||  //Right Windows Key
                        ((IParam.vkCode == 0x73) && (IParam.flags == 0x20));    //Alt + F4

                    break;
            }
            if (bReturn == true)
                return 1;
            else
                return CallNextHookEx(0, nCode, wParam, ref IParam);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int SetWindowsHookEx(int idHook, LowLevelKeyboardProc Ipfn, IntPtr hMod, uint dwThredId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(int hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int CallNextHookEx(int hhk, int nCode, int wParam, ref KBDLLHOOKSTRUCT IParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string IpModulName);

        public struct KBDLLHOOKSTRUCT
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }
        //

        private static int SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);

            }
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _hookID = SetHook(_proc);
        }
    }
}
