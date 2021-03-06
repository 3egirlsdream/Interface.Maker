﻿using GenerateToolbox.Controlsz;
using GenerateToolbox.NewPage;
using GenerateToolbox.NotifyBox;
using Project.G.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
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

        static TabItemClose PreviewItem = new TabItemClose();
        static TabItemClose ModelItem = new TabItemClose();
        static TabItemClose ServiceItem = new TabItemClose();
        static TabItemClose MakeDataItem = new TabItemClose();

        RightPage page = new RightPage();
        LeftPage left = new LeftPage();
        NewPage newPage = new NewPage();
        GenerateToolbox.MakeData.MakeData makeData = new GenerateToolbox.MakeData.MakeData();
        MainView vm;
        public MainWindow()
        {
            InitializeComponent();

            PreviewItem = this.Preview_item;
            ModelItem = Model_item;
            ServiceItem = Service_item;
            MakeDataItem = MakeData_item;

            foreach(TabItemClose item in tabcontol.Items)
            {
                item.Visibility = Visibility.Collapsed;
            }

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
            MakeData.Content = new Frame() { Content = makeData };
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
            vm = new MainView(this);
            this.DataContext = vm;

            //RunNotifyBox();
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
            //this.Close();
            System.Environment.Exit(0);
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
            //bd1.Background = GetColor("#DD335D");
            //bd2.Background = GetColor("#DD335D");
        }

        public static SolidColorBrush GetColor(string str)
        {
            return new SolidColorBrush((Color)ColorConverter.ConvertFromString(str));
        }

        private void Model_GotFocus(object sender, RoutedEventArgs e)
        {
            //bd1.Background = GetColor("#404671");
            //bd2.Background = GetColor("#404671");
        }

        private void Domain_GotFocus(object sender, RoutedEventArgs e)
        {
            //bd1.Background = GetColor("#242225");
            //bd2.Background = GetColor("#242225");
        }

        private void Preview_GotFocus(object sender, RoutedEventArgs e)
        {
            //bd1.Background = GetColor("#dddddd");
            //bd2.Background = GetColor("#dddddd");
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
                while(true)
                {
                    Thread.Sleep(1000);
                    if((DateTime.Now.Minute == 30 || DateTime.Now.Minute == 1) && DateTime.Now.Second == 1)
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            NofifyBox page = new NofifyBox();
                            page.ShowDialog();
                        });
                }
            }).ConfigureAwait(true);
        }

        private void MetroTabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            
            
            
        }

        private void FileClick(object sender, RoutedEventArgs e)
        {
            if (PreviewItem.Parent == null) tabcontol.Items.Add(PreviewItem);
            PreviewItem.Visibility = Visibility;
            PreviewItem.Focus();
        }

        private void ServiceClick(object sender, RoutedEventArgs e)
        {
            if (ServiceItem.Parent == null) tabcontol.Items.Add(ServiceItem);
            ServiceItem.Visibility = Visibility;
            ServiceItem.Focus();
        }

        private void ModelClick(object sender, RoutedEventArgs e)
        {
            if (ModelItem.Parent == null) tabcontol.Items.Add(ModelItem);
            ModelItem.Visibility = Visibility;
            ModelItem.Focus();
        }

        private void DataClick(object sender, RoutedEventArgs e)
        {
            if (MakeDataItem.Parent == null) tabcontol.Items.Add(MakeDataItem);
            MakeDataItem.Visibility = Visibility;
            MakeDataItem.Focus();
        }


        private void settingClick(object sender, RoutedEventArgs e)
        {
            page.Visibility = page.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}
