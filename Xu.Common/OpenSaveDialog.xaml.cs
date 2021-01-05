using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Xu.Common
{
    /// <summary>
    /// OpenSaveDialog.xaml 的交互逻辑
    /// </summary>
    public partial class OpenSaveDialog : Window
    {
        System.Timers.Timer timer;
        int timespan = 3;
        public OpenSaveDialog(string str)
        {
            InitializeComponent();
            tb1.Text = str;
            timespan = 3;
            if (timer == null)
            {
                timer = new System.Timers.Timer();
                timer.Interval = 1000;
                timer.Elapsed += Timer_Elapsed;        
            }
            timer.Start();
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Task.Run(() =>
            {
                this.Dispatcher.BeginInvoke(new System.Action(() =>
                {
                    surtbtn.Content = $"确   定({--timespan})";
                    if (timespan == 0)
                    {
                        timer.Stop();
                        timer.Close();
                        Close();
                    }
                }), null);
            });
        }

        public OpenSaveDialog()
        {
            InitializeComponent();
        }
        public OpenSaveDialog(warn TYPE) {
            InitializeComponent();
        }

        public static void ShowMsg(string ex)
        {
            OpenSaveDialog warning = new OpenSaveDialog(ex);
            warning.ShowDialog();
            
        }
        
        private void CLOSE_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public async void HideWindow()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(3000);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Close();
                });
            }).ConfigureAwait(true);
        }
    }

}
