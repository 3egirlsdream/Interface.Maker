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
        public Loading()
        {
            InitializeComponent();
            //close();
        }
        private string Text { get; set; }
        public Loading(string text)
        {
            InitializeComponent();
            Text = text;
            //TextChange(Text);
            //close();
        }

        public async void ShowWindow()
        {
            await Task.Run(() =>
            {
                //Thread.Sleep(2000);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Show();
                });
            }).ConfigureAwait(true);
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


        public async void Shutdown()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(2000);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Visibility = Visibility.Collapsed;
                });
            }).ConfigureAwait(true);
        }

    }
}
