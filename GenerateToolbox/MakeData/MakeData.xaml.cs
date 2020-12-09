using GenerateToolbox;
using GenerateToolbox.DragInterfaceCreater;
using GenerateToolbox.DragInterfaceCreater.Setting;
using GenerateToolbox.Models;
using GenerateToolbox.ViewModel;
using Project.G;
using Project.G.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using ViewModels;
using Xu.Common;

namespace GenerateToolbox.MakeData
{
    /// <summary>
    /// MakeData.xaml 的交互逻辑
    /// </summary>
    public partial class MakeData : Page
    {
        MakeDataVM vm;
        public MakeData()
        {
            InitializeComponent();
            vm = new MakeDataVM(this);
            this.DataContext = vm;
            searchbox.KeyDown += Searchbox_KeyDown;
            yesbtn.Click += Yesbtn_Click;
            insert.Click += Insert_Click;
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(count.Text)) return;
            vm.Insert(Convert.ToInt32(count.Text));
        }

        private void Yesbtn_Click(object sender, RoutedEventArgs e)
        {
            vm.GetDataTable();
        }

        private void Searchbox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                vm.GetAllUserTable(searchbox.Text);
            }
        }
    }
}
