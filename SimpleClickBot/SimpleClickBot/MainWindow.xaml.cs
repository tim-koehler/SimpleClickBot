using SimpleClickBot.Properties;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;

namespace SimpleClickBot
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Listener

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                comboKey.Items.Add(key);
            }

            comboKey.SelectedItem = Settings.Default.SettingTriggerKey;
            txtDelay.Text = Settings.Default.SettingDelay.ToString();

            KeyListener.Setup();
            ClickBot.Setup();
        }

        private void txtDelay_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void txtDelay_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtDelay.Text != String.Empty)
            {
                Settings.Default.SettingDelay = int.Parse(((System.Windows.Controls.TextBox)sender).Text);
                Settings.Default.Save();
            }
        }

        private void comboKey_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Settings.Default.SettingTriggerKey = (Keys)comboKey.SelectedItem;
            Settings.Default.Save();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.Save();
            Environment.Exit(0);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                ReleaseCapture();
                SendMessage(new WindowInteropHelper(this).Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        #endregion

        #region Dlls

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        #endregion

    }
}
