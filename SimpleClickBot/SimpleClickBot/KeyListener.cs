using SimpleClickBot.Properties;
using System.Runtime.InteropServices;
using System.Threading;

namespace SimpleClickBot
{
    public static class KeyListener
    {
        public static bool IsKeyPressed { get; private set; } = false;

        public static void Setup()
        {
            Thread listenThread = new Thread(DoListenForKeys);
            listenThread.IsBackground = true;
            listenThread.Start();
        }

        private static void DoListenForKeys()
        {
            while (true)
            {
                if (GetAsyncKeyState(Settings.Default.SettingTriggerKey) != 0)
                {
                    IsKeyPressed = true;
                }
                else
                {
                    IsKeyPressed = false;
                }
                Thread.Sleep(50);
            }
        }

        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);
    }
}
