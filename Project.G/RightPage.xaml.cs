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
using System.Configuration;
using System.ComponentModel;
using Xu.Common;
using Project.G.ViewModel;
using Creative.ODA;
using Project.G.Models;
using MeiCloud.DataAccess;
using System.Reflection;
using System.Data.SqlClient;
using System.Threading;

namespace Project.G
{
    /// <summary>
    /// RightPage.xaml 的交互逻辑
    /// </summary>
    public partial class RightPage : Page
    {
        MainView vm;
        public RightPage()
        {
            InitializeComponent();
            vm = new MainView();
            this.DataContext = vm;
            this.Visibility = Visibility.Hidden;
        }

        #region
        MainWindow _ParentWindow;
        public MainWindow ParentWindow
        {
            get
            {
                return _ParentWindow;
            }
            set
            {
                _ParentWindow = value;
            }

        }
        #endregion

        private void Save(object sender, RoutedEventArgs e)
        {
            //var ts = GetName("inv_bill_info");
            var ts = DB.GetName();

            //INV_BILL_INFO dt = new INV_BILL_INFO(Name = "aa");
            //dt.ID = "";
            //var type = dt.GetType();
            //foreach (PropertyInfo t in type.GetProperties())
            //{
            //    object obj = t.GetValue(dt, null);
            //    string name = t.Name;
            //    MessageBox.Show(name);
            //}
            this.ParentWindow.LoadLeftPage();
            this.Visibility = Visibility.Hidden;
        }

        

    }
}
