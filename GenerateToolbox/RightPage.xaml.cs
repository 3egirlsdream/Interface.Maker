using Project.G.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Project.G
{
    /// <summary>
    /// RightPage.xaml 的交互逻辑
    /// </summary>
    public partial class RightPage : Page
    {
        RightPageVM vm;
        
        public RightPage()
        {
            InitializeComponent();
            vm = new RightPageVM();
            this.DataContext = vm;
            this.Visibility = Visibility.Hidden;

        }
        public MainWindow ParentWindow { get; set; }

        

        private void Save(object sender, RoutedEventArgs e)
        {
            //var ts = GetName("inv_bill_info");
            //var ts = DB.GetName();

            //INV_BILL_INFO dt = new INV_BILL_INFO(Name = "aa");
            //dt.ID = "";
            //var type = dt.GetType();
            //foreach (PropertyInfo t in type.GetProperties())
            //{
            //    object obj = t.GetValue(dt, null);
            //    string name = t.Name;
            //    MessageBox.Show(name);
            //}
            this.ParentWindow.LoadLeftPage(vm.FilterDb.value);
            this.Visibility = Visibility.Hidden;
        }
    }
}
