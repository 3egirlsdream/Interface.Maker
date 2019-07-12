using GenerateToolbox.Models;
using GenerateToolbox.ViewModel;
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
using System.Windows.Shapes;

namespace GenerateToolbox
{
    /// <summary>
    /// CsprojName.xaml 的交互逻辑
    /// </summary>
    public partial class CsprojName : Window
    {
        public CsprojName()
        {
            InitializeComponent();
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            ExcelHelper.zh = name_zh.Text;
            ExcelHelper.en = name_eng.Text;
            this.Close();
        }
    }
}
