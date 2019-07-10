using GenerateToolbox.ViewModels;
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
    /// DataGridSetting.xaml 的交互逻辑
    /// </summary>
    public partial class DataGridSetting : Page
    {
        private string name;
        MyDataGrid MyDataGrid;
        MyDataGridVM vm;
        public DataGridSetting(FrameworkElement name)
        {
            InitializeComponent();
            MyDataGrid = name as MyDataGrid;
            this.name = name.Name;

            vm = new MyDataGridVM(name, this);
            this.DataContext = vm;

        }

        public NewPage.NewPage ParentWindow { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            ParentWindow.ccp.Visibility = Visibility.Hidden;
        }
    }
}
