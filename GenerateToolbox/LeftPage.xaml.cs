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

namespace Project.G
{
    /// <summary>
    /// LeftPage.xaml 的交互逻辑
    /// </summary>
    public partial class LeftPage : Page
    {
        MainView vm;
        List<string> tables;

        public LeftPage()
        {
            InitializeComponent();
        }

        public LeftPage(int t)
        {
            InitializeComponent();

            DbType db = t == 0 ? DbType.SqlServer : DbType.MySql;

            vm = new MainView();
            this.DataContext = vm;
            //this.Visibility = Visibility.Collapsed;
            //生成表列表
            tables = new List<string>();
            tables = DB.GetName(db);
            Thickness th = new Thickness();
            th.Bottom = 5;
            th.Left = 10;
            th.Top = 5;
            th.Right = 10;
            foreach(var ds in tables)
            {
                CheckBox check = new CheckBox();
                check.Content = ds;
                check.Margin = th;
                addCheckbox.Children.Add(check);
                addCheckbox.RegisterName("CheckBox" + ds, check);
            }
            Button btn = new Button();
            btn.Margin = th;
            btn.Width = 80;
            btn.Content = "确定";
            btn.Command = CmdSave;
            addCheckbox.Children.Add(btn);
            addCheckbox.RegisterName("btn", btn);
            
        }
        public MainWindow ParentWindow { get; set; }

        List<string> Columes;
        public SimpleCommand CmdSave => new SimpleCommand()
        {
            ExecuteDelegate = o =>
            {
                Columes = new List<string>();
                //Columes = DB.GetName();
                Thickness th = new Thickness();
                th.Left = 10;
                th.Right = 10;
                th.Top = 3;
                th.Bottom = 3;
                foreach (var ds in tables)
                {
                    CheckBox check = addCheckbox.FindName("CheckBox" + ds) as CheckBox;
                    if(check.IsChecked == true)
                    {
                        Columes = DB.GetName(check.Content.ToString(), TextType.Table);
                        
                        foreach(var dt in Columes)
                        {
                            CheckBox box = new CheckBox();
                            box.Content = dt;
                            box.Margin = th;
                            CheckboxColume.Children.Add(box);
                            CheckboxColume.RegisterName("C" + dt, box);
                        }
                    }
                }
                Button btnCloume = new Button();
                btnCloume.Margin = th;
                btnCloume.Width = 80;
                btnCloume.Content = "确定";
                btnCloume.Command = CmdGenerate;
                CheckboxColume.Children.Add(btnCloume);
                CheckboxColume.RegisterName("btnC", btnCloume);
            }
        };


        List<string> arrs;
        public SimpleCommand CmdGenerate => new SimpleCommand()
        {
            ExecuteDelegate = o =>
            {
                arrs = new List<string>();
                //Columes = DB.GetName();
                Thickness th = new Thickness();
                th.Left = 10;
                th.Right = 10;
                th.Top = 3;
                th.Bottom = 3;
                foreach (var ds in Columes)
                {
                    CheckBox check = addCheckbox.FindName("C" + ds) as CheckBox;
                    if (check.IsChecked == true)
                    {
                        arrs = DB.GetName(check.Content.ToString(), TextType.DataBase);

                        foreach (var dt in arrs)
                        {
                            CheckBox box = new CheckBox();
                            box.Content = dt;
                            box.Margin = th;
                            try
                            {
                                C3.Children.Add(box);
                                C3.RegisterName("C3" + dt.Replace('_', '0'), box);
                            }
                            catch (Exception) { }
                            
                        }
                    }
                }
            }
        };

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

            this.Visibility = Visibility.Hidden;
        }

    }
}
