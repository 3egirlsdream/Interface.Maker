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
    /// ButtonSetting.xaml 的交互逻辑
    /// </summary>
    public partial class ButtonSetting : Page
    {
        private string name;
        MyBorder myborder;
        public ButtonSetting(FrameworkElement name)
        {
            InitializeComponent();
            myborder = name as MyBorder;
            this.name = name.Name;
            name_eg.Text = myborder.NAME_ENG as string;
            name_zh.Text = myborder.tb.Text as string;
        }

        public NewPage.NewPage ParentWindow { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            myborder.NAME_ENG = name_eg.Text;
            myborder.tb.Text = name_zh.Text;
            this.Visibility = Visibility.Hidden;
        }
    }
}
