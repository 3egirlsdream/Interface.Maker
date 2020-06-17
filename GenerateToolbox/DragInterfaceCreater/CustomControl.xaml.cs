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

namespace GenerateToolbox.DragInterfaceCreater
{
    /// <summary>
    /// CustomControl.xaml 的交互逻辑
    /// </summary>
    public partial class CustomControl : UserControl
    {
        public CustomControl()
        {
            InitializeComponent();
            ccgrid.MouseLeftButtonDown += Btn_MouseLeftButtonDown;
            ccgrid.MouseMove += Btn_MouseMove;
            ccgrid.MouseLeftButtonUp += Btn_MouseLeftButtonUp;
        }
        Point pos = new Point();
        private void Btn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Grid tmp = (Grid)sender;
            pos = e.GetPosition(null);
            tmp.CaptureMouse();
            tmp.Cursor = Cursors.Hand;
        }

        private void Btn_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Grid tmp = (Grid)sender;
                double dx = e.GetPosition(null).X - pos.X + tmp.Margin.Left;
                double dy = e.GetPosition(null).Y - pos.Y + tmp.Margin.Top;
                tmp.Margin = new Thickness(dx, dy, 0, 0);
                pos = e.GetPosition(null);
            }
        }

        private void Btn_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Grid tmp = (Grid)sender;
            int xx = (int)(ccgrid.Margin.Left / 10);
            int yy = (int)(ccgrid.Margin.Top / 10);
            //MessageBox.Show(xx + " " + yy);
            tmp.Margin = new Thickness(xx * 10, yy * 10, 0, 0);
            tmp.ReleaseMouseCapture();
        }
    }
}
