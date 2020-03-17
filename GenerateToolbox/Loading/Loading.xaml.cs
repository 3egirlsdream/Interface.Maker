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
            //close();
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
            //TextChange(Text);
            //close();
        }

        /// <summary>
        /// 打开加载动画
        /// </summary>
        public async void ShowLoading()
        {
            await Task.Run(() =>
            {
                //Thread.Sleep(2000);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Framework = new Loading();
                    Framework.Show();
                });
            }).ConfigureAwait(true);
            //await Task.Run(() =>
            //{
            //    Application.Current.Dispatcher.Invoke(() =>
            //    {
            //        while (true)
            //        {
            //            Thread.Sleep(500);
            //            if (Framework.tb.Text.Length > 3)
            //            {
            //                Framework.tb.Text = "加载中";
            //            }
            //            Framework.tb.Text += "·";
            //        }
            //    });
            //});
        }



        private async void TextChange(string text)
        {
            await Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    while (true) 
                    {
                        Thread.Sleep(1000);
                        text += "·";
                        if(text.ToCharArray().Count(c=>c == '·') > 3)
                        {
                            text = text.Replace("·", "");
                        }
                        this.tb.Text = text;
                    }
                });
            });
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
