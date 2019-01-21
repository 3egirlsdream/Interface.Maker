using Project.G.Models;
using System.Windows;
using System.Windows.Input;
using Xu.Common;

namespace Project.G
{
    /// <summary>
    /// JiHuo.xaml 的交互逻辑
    /// </summary>
    public partial class JiHuo : Window
    {
        public JiHuo()
        {
            InitializeComponent();
        }

        public JiHuo(string str)
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tb1.Text = Common.UUID();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Common.SetConfig("Password", Strings.LoadJson("file.obs"));
            if(Common.SetConfig("Password") == Common.Key())
            {
                this.Close();
            }
            else
            {
                tb1.Text += "\r\n" + "激活失败";
            }
        }
    }
}
