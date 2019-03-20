using GenerateToolbox.NewPage;
using Project.G.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
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
using Xu.Common;

namespace Project.G
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        RightPage page = new RightPage();
        LeftPage left = new LeftPage();
        NewPage newPage = new NewPage();
        MainView vm = new MainView();
        public MainWindow()
        {
            InitializeComponent();

            #region 激活校验
            /*
             * 不启用激活校验
             */
            //if (Common.Key() != Common.SetConfig("Password"))
            //{
            //    JiHuo ji = new JiHuo();
            //    ji.ShowDialog();
            //    if (Common.Key() != Common.SetConfig("Password"))
            //        this.Close();
            //}

            //if (Common.SetConfig("Date") == "0")
            //{
            //    this.Close();
            //}
            //else
            //{
            //    Common.SetConfig("Date", (Convert.ToInt32(Common.SetConfig("Date")) - 1).ToString());
            //}
#endregion

            left.ParentWindow = this;
            page.ParentWindow = this;
            newPage.ParentWindow = this;
            Preview.Content = new Frame { Content = newPage };

            right.Content = new Frame() { Content = page };
            right.Visibility = Visibility.Visible;
            //打开更新日志界面
            if (Common.SetConfig("Update") == "0")
            {
                UpdateDesc u = new UpdateDesc();
                u.ShowDialog();
                if (u.IsChecked())
                    Common.SetConfig("Update", "1");
            }
            ProjName.ToolTip = "项目名必须包含Plugin,否则不会生成服务端代码";
            ProjName1.ToolTip = "项目名必须包含Plugin,否则不会生成服务端代码";
            tb3.Text = " <— 在这里输入表名(要换行),服务端可以自动链接模型！\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n <— 勾选后不存在的模型不会链接。";
            cm.ToolTip = "勾选后会生成在右侧文本框输入的表的模型";
            vm = new MainView();
            this.DataContext = vm;
        }

        public void LoadLeftPage(int t)
        {
            left = new LeftPage(t);
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

        private void MinWindow(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Service_GotFocus(object sender, RoutedEventArgs e)
        {
            bd1.Background = GetColor("#C83C56");
            bd2.Background = GetColor("#C83C56");
        }

        private SolidColorBrush GetColor(string str)
        {
            return new SolidColorBrush((Color)ColorConverter.ConvertFromString(str));
        }

        private void Model_GotFocus(object sender, RoutedEventArgs e)
        {
            bd1.Background = GetColor("#404671");
            bd2.Background = GetColor("#404671");
        }

        private void Domain_GotFocus(object sender, RoutedEventArgs e)
        {
            bd1.Background = GetColor("#242225");
            bd2.Background = GetColor("#242225");
        }

        private void Preview_GotFocus(object sender, RoutedEventArgs e)
        {
            bd1.Background = GetColor("#AA96BE");
            bd2.Background = GetColor("#AA96BE");
        }
    }
}
