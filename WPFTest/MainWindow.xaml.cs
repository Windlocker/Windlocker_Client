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
using System.Windows.Forms;
using System.Reflection;
using Microsoft.Win32;

namespace WPFTest
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public static LockWindow[] lw = new LockWindow[5];
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new WPFTest.Pages.DownloadPage());
            RegistryKey k = Registry.CurrentUser.OpenSubKey("Windlocker");
            var a = k.GetValue("token");
            if (a!=null)
            {
                if(Server.Auto(a.ToString()))
                {
                    Session.Token = a.ToString();
                }
            }
            //
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            NotifyIcon n = new NotifyIcon();
            n.Icon = Properties.Resources.Logo1;
            n.Visible = true;
            n.DoubleClick += delegate(object senders,EventArgs ev)
            {
                Show();
                n.Dispose();
            };
            n.BalloonTipTitle = "Windlocker가 백그라운드에서 실행됩니다.";
            n.BalloonTipText = "Windlocker Service가 백그라운드에서 계속 실행됩니다.";
            n.ShowBalloonTip(1000);
            Hide();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }

        private void lblLogin_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void lblClose_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void lblLogin_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            lw.ShowDialog();
        }

        private void lblLock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Lock();
        }

        public static void Lock()
        {
            int cnt = 0;
            var dpiX = (int)typeof(SystemParameters).GetProperty("DpiX", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null, null);
            var dpiY = (int)typeof(SystemParameters).GetProperty("Dpi", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null, null);
            foreach (var scr in Screen.AllScreens)
            {
                LockWindow l = new LockWindow()
                {
                    WindowStartupLocation = WindowStartupLocation.Manual
                };
                lw[cnt] = l;
                System.Drawing.Rectangle workingArea = scr.WorkingArea;
                l.Left = workingArea.Left * (96 / dpiX);
                l.Top = workingArea.Top * (96 / dpiY);
                l.Width = workingArea.Width;
                l.Height = workingArea.Height+40;
                //l.WindowState = WindowState.Maximized;
                l.WindowStyle = WindowStyle.None;
                l.Topmost = true;
                l.Show();
            }
        }

        public static void UnLock()
        {
            for(int i = 0;i<5;i++)
            {
                if(lw[i] != null)
                {
                    lw[i].Close();
                }
            }
        }

        private void lblUpload_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            frame.Navigate(new WPFTest.Pages.UploadPage());
        }
    }
}
