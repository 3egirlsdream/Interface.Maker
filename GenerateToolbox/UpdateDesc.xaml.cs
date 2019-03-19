using Project.G.Models;
using System.Windows;
using System.Windows.Input;

namespace Project.G
{
    /// <summary>
    /// UpdateDesc.xaml 的交互逻辑
    /// </summary>
    public partial class UpdateDesc : Window
    {
        public UpdateDesc()
        {
            InitializeComponent();
            Title.Text = "使用说明";
            tb1.Text = Strings.LoadJson("Update.txt");
        }

        public UpdateDesc(string str)
        {
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

        public bool IsChecked()
        {
            return cb.IsChecked.Value;
        }
    }
}
