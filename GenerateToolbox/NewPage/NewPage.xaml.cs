using GenerateToolbox.ViewModel;
using Project.G;
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

namespace GenerateToolbox.NewPage
{
    /// <summary>
    /// NewPage.xaml 的交互逻辑
    /// </summary>
    public partial class NewPage : Page
    {
        NewPageVM vm;
        public NewPage()
        {
            InitializeComponent();
            vm = new NewPageVM();
            this.DataContext = vm;
        }
        public MainWindow ParentWindow { get; set; }


    }

    
}
