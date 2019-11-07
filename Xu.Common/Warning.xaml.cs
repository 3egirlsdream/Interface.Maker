using System.Windows;
using System.Windows.Input;

namespace Xu.Common
{
    /// <summary>
    /// Warning.xaml 的交互逻辑
    /// </summary>
    public partial class Warning : Window
    {
        public Warning(string str)
        {
            InitializeComponent();
            tb1.Text = str;
        }

        public Warning()
        {
            InitializeComponent();
        }
        public Warning(warn TYPE) {
            InitializeComponent();
        }
        
        private void CLOSE_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
