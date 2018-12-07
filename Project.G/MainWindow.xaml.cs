using Project.G.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project.G
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        RightPage page = new RightPage();
        LeftPage left = new LeftPage();
        MainView vm = new MainView();
        public MainWindow()
        {
            InitializeComponent();

            left.ParentWindow = this;
            page.ParentWindow = this;

            right.Content = new Frame() { Content = page };
            right.Visibility = Visibility.Visible;

            
            vm = new MainView();
            this.DataContext = vm;
        }

        public void LoadLeftPage()
        {
            left = new LeftPage();
            Left.Content = new Frame() { Content = left };
            Left.Visibility = Visibility.Visible;
        }

        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void CloseWindow(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Setting(object sender, MouseButtonEventArgs e)
        {
            page.Visibility = page.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}
