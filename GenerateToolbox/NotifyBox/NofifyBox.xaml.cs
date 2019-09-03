using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Xu.Common;

namespace GenerateToolbox.NotifyBox
{
    /// <summary>
    /// NofifyBox.xaml 的交互逻辑
    /// </summary>
    public partial class NofifyBox : Window
    {
        public NofifyBox()
        {
            InitializeComponent();
            this.Topmost = true;
            //SetWindowPos(this.Handle, -1, 0, 0, 0, 0, 1 | 2);
            this.Loaded += Load_box;
            var str = Common.SetConfig("Content");
            tb.Text = str;
        }

        private void Load_box(object sender, RoutedEventArgs e)
        {
            var page = sender as NofifyBox;

            DoubleAnimation animation = new DoubleAnimation();
            animation.From = SystemParameters.WorkArea.Right;
            animation.To = SystemParameters.WorkArea.Right - 250;
            animation.Duration = new Duration(TimeSpan.FromMilliseconds(500));
            page.BeginAnimation(Window.LeftProperty, animation);
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
