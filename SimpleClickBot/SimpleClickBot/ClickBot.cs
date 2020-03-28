using SimpleClickBot.Properties;
using System.Runtime.InteropServices;
using System.Threading;

namespace SimpleClickBot
{
    public class ClickBot
    {
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;

        public static void Setup()
        {
            Thread clickThread = new Thread(DoClick);
            clickThread.IsBackground = true;
            clickThread.Start();
        }

        private static void DoClick()
        {
            while (true)
            {
                if (KeyListener.IsKeyPressed)
                {
                    PerformClick();
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
        }

        private static void PerformClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            Thread.Sleep(5);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            Thread.Sleep(Settings.Default.SettingDelay);
        }

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
    }
}
