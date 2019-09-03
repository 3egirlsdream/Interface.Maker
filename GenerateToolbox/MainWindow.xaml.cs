using GenerateToolbox.NewPage;
using GenerateToolbox.NotifyBox;
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

            LoadMode();
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
            //tb3.Text = " <— 在这里输入表名(要换行),服务端可以自动链接模型！";//\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n <— 勾选后不存在的模型不会链接。";
            cm.ToolTip = "勾选后会生成在右侧文本框输入的表的模型";
            vm = new MainView();
            this.DataContext = vm;

            RunNotifyBox();
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

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Setting(object sender, MouseButtonEventArgs e)
        {
            page.Visibility = page.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void MinWindow(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaxWindow(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else
                this.WindowState = WindowState.Normal;

        }

        private void Service_GotFocus(object sender, RoutedEventArgs e)
        {
            bd1.Background = GetColor("#DD335D");
            bd2.Background = GetColor("#DD335D");
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
            bd1.Background = GetColor("#4F4150");
            bd2.Background = GetColor("#4F4150");
        }

        private void CLOSE_MouseEnter(object sender, MouseEventArgs e)
        {
            CLOSE.Background = GetColor("#4F4150");
        }

        private void bodyDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Transfer();
        }

        private void Transfer()
        {
            bd1.Visibility = bd1.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
            tabcontol.Visibility = tabcontol.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
            bd2.Height = (bd2.Height - 30) <= 10 ? 70 : 35;
            setting.Visibility = setting.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
            if (setting.Visibility == Visibility.Collapsed)
            {
                zz.Visibility = Visibility.Visible;
                zz.Content = new Frame() { Content = newPage };
            }
            else
            {
                zz.Visibility = Visibility.Collapsed;
                Preview.Content = new Frame() { Content = newPage };
            }
        }

        private void LoadMode()
        {
            if (Common.SetConfig("Mode") == "1")
            {
                bd1.Visibility = Visibility.Collapsed;
                tabcontol.Visibility = Visibility.Collapsed;
                bd2.Height = 70;
                setting.Visibility = Visibility.Collapsed;
                zz.Content = new Frame() { Content = newPage };
            }
            else
            {
                Preview.Content = new Frame { Content = newPage };
            }
        }

        private async void RunNotifyBox()
        {
            await Task.Run(() =>
            {
                for (int i = 0; ; i++)
                {
                    if((DateTime.Now.Minute * DateTime.Now.Second * DateTime.Now.Hour) % (40 * 60) == 0)
                        App.Current.Dispatcher.Invoke(() =>
                        {
                            NofifyBox page = new NofifyBox();
                            page.ShowDialog();
                        });
                }
            });
        }
    }
}
