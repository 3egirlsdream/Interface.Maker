using Project.G;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GenerateToolbox.Loading
{
    /// <summary>
    /// Loading.xaml 的交互逻辑
    /// </summary>
    public partial class Loading : Window
    {
        System.Timers.Timer timer;
        public Loading()
        {
            InitializeComponent();
            if(timer == null)
            {
                timer = new System.Timers.Timer();
                timer.Interval = 1000;
                timer.Elapsed += Timer_Elapsed;
            }
            timer.Start();
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (Framework.tb.Text.Length >= 6)
                {
                    Framework.tb.Text = "加载中";
                }
                else
                {
                    Framework.tb.Text += "·";
                }
            }), null);
        }

        private string Text { get; set; }
        public Loading(string text)
        {
            InitializeComponent();
            Text = text;
        }

        /// <summary>
        /// 打开加载动画
        /// </summary>
        public async void ShowLoading()
        {
            await Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Framework = new Loading();
                    Framework.Show();
                });
            }).ConfigureAwait(true);
        }

        /// <summary>
        /// 关闭加载动画
        /// </summary>
        public async void HideLoading()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(2000);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Visibility = Visibility.Collapsed;
                    Framework.Close();
                    Framework.timer.Close();
                });
            }).ConfigureAwait(true);
        }
        public static Loading Framework = new Loading();
    }

    
}
