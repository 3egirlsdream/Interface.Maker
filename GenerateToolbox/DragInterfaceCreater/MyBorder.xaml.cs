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

namespace GenerateToolbox
{
    /// <summary>
    /// MyBorder.xaml 的交互逻辑
    /// </summary>
    public partial class MyBorder : UserControl
    {
        public MyBorder()
        {
            InitializeComponent();
            btn.MouseLeftButtonDown += Btn_MouseLeftButtonDown;
            btn.MouseMove += Btn_MouseMove;
            btn.MouseLeftButtonUp += Btn_MouseLeftButtonUp;

        }
        public string NAME_ENG { get; set; }
        Point pos = new Point();
        private void Btn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Border tmp = (Border)sender;
            pos = e.GetPosition(null);
            tmp.CaptureMouse();

            tmp.Cursor = Cursors.Hand;
        }

        private void Btn_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Border tmp = (Border)sender;
                double dx = e.GetPosition(null).X - pos.X + tmp.Margin.Left;
                double dy = e.GetPosition(null).Y - pos.Y + tmp.Margin.Top;
                
                
                tmp.Margin = new Thickness(dx, dy, 0, 0);
                
                pos = e.GetPosition(null);
            }
        }

        private void Btn_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Border tmp = (Border)sender;
            int xx = (int)(btn.Margin.Left / 20);
            int yy = (int)(btn.Margin.Top / 20);
            //MessageBox.Show(xx + " " + yy);
            tmp.Margin = new Thickness(xx * 20, yy * 20, 0, 0);

           
            tmp.ReleaseMouseCapture();
        }

        //private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    if(e.ClickCount == 2)
        //    {
        //        tb.Text = "HELLO";
        //    }
        //}
    }
}
