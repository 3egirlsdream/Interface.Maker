using GenerateToolbox;
using GenerateToolbox.ViewModel;
using Project.G;
using Project.G.Models;
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
using ViewModels;
using Grid = Project.G.Models.Grid;

namespace GenerateToolbox.NewPage
{
    /// <summary>
    /// NewPage.xaml 的交互逻辑
    /// </summary>
    public partial class NewPage : Page
    {
        NewPageVM vm;
        Point pos;
        public NewPage()
        {
            InitializeComponent();
            pos = new Point();
            vm = new NewPageVM();
            this.DataContext = vm;
        }
        public MainWindow ParentWindow { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MyBorder myBorder = new MyBorder();
            myBorder.Name = GetControlsName("BTN");
            myBorder.MouseDoubleClick += Button_MouseDown;
            grid.Children.Add(myBorder);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MyTextBox textBox = new MyTextBox();
            textBox.Name = GetControlsName("TB");
            textBox.MouseDoubleClick += TextBox_MouseDown;
            grid.Children.Add(textBox);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MyDataGrid datagrid = new MyDataGrid();
            datagrid.Name = GetControlsName("GRID");
            datagrid.MouseDoubleClick += DataGrid_MouseDown;
            grid.Children.Add(datagrid);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //Canvas canvas = new Canvas();
            //canvas.Width = 1150;
            //canvas.Height = 750;
            //canvas.Name = "canvas1";
            //father.Children.Add(canvas);

            MySidebar mySidebar = new MySidebar();
            mySidebar.Name = GetControlsName("BAR");
            grid.Children.Add(mySidebar);
        }

        //生成控件名称
        private string GetControlsName(string prex)
        {
            return prex + DateTime.Now.ToString("MMddHHmmss");
        }

        #region 打开控件配置界面
        private void Button_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var obj = sender as FrameworkElement;
            ButtonSetting buttonSetting = new ButtonSetting(obj);
            buttonSetting.ParentWindow = this;
            cc.Content = new Frame { Content = buttonSetting };
            ccp.Visibility = Visibility;
            cc.Visibility = Visibility.Visible;
            //MessageBox.Show(obj.Name);
        }

        private void TextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var obj = sender as FrameworkElement;
            TextBoxSetting textBoxSetting = new TextBoxSetting(obj);
            textBoxSetting.ParentWindow = this;
            cc.Content = new Frame { Content = textBoxSetting };
            ccp.Visibility = Visibility;
            cc.Visibility = Visibility.Visible;
            //MessageBox.Show(obj.Name);
        }

        private void DataGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var obj = sender as FrameworkElement;
            DataGridSetting dataGridSetting = new DataGridSetting(obj);
            dataGridSetting.ParentWindow = this;
            cc.Content = new Frame { Content = dataGridSetting };
            ccp.Visibility = Visibility;
            cc.Visibility = Visibility.Visible;
            //MessageBox.Show(obj.Name);
        }
        #endregion

        /// <summary>
        /// 生成代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Create(object sender, RoutedEventArgs e)
        {
            //拿到所有边框
            List<MySidebar> sider = new List<MySidebar>();
            foreach (dynamic ds in grid.Children)
            {
                string name = ds.Name as string;
                if (name.Contains("BAR"))
                {
                    sider.Add(ds);
                }
            }

            var result = GetData(sider);

            //生成界面

            vm.result = result;
            vm.Genetate();

        }


        public List<Grids> GetData(List<MySidebar> sider)
        {
            List<Grids> grids = new List<Grids>();
            Grids tmp = new Grids();
            tmp.Identity = "主页";
            tmp.PageCode = "IndexPage";
            tmp.PageName = "主页";
            foreach (var ds in sider)
            {
                //var borderMargin = getMargin(ds.RenderSize.Width, ds.RenderSize.Height, ds.ActualWidth, ds.ActualHeight);

                foreach (var control in grid.Children)
                {
                    var type = control.GetType();
                    if (type.Name != "MySidebar")//不包含边框元素
                    {
                        dynamic obj;
                        switch (type.Name)
                        {
                            case "MyBorder": obj = control as MyBorder; break;
                            case "MyDataGrid": obj = control as MyDataGrid; break;
                            case "MyTextBox": obj = control as MyTextBox; break;
                            default: obj = control as MyBorder; break;
                        }

                        //POSITION margin = getMargin(obj.RenderSize.Width, obj.RenderSize.Height, obj.ActualWidth, obj.ActualHeight);

                        if (obj.btn.Margin.Top > ds.btn.Margin.Top
                            && obj.btn.Margin.Left > ds.btn.Margin.Left
                            && obj.btn.Margin.Top + obj.btn.ActualHeight < ds.btn.Margin.Top + ds.btn.ActualHeight
                            && obj.btn.Margin.Left + obj.btn.ActualWidth < ds.btn.Margin.Left + ds.btn.ActualWidth)
                        {
                            //默认第一行全是按钮
                            if (sider.IndexOf(ds) == 0)
                                tmp.strs.Add(obj.tb.Text);
                            //按钮
                            else if (type.Name == "MyBorder")
                            {
                                Grid temp = new Grid
                                {
                                    CONTROL_NAME = "btn",
                                    NAME = obj.tb.Text
                                };
                                tmp.grids.Add(temp);
                            }
                            //文本框
                            else if (type.Name == "MyTextBox")
                            {
                                Grid temp = new Grid
                                {
                                    CONTROL_NAME = obj.BOX_TYPE,
                                    NAME = obj.tblock.Text,
                                    CODE = obj.NAME_ENG,
                                    IS_API = obj.IS_API
                                };
                                tmp.grids.Add(temp);
                            }

                            //表格
                            else if (type.Name == "MyDataGrid")
                            {
                                foreach (DataGridColumn rtl in obj.grid.Columns)
                                {
                                    Grid temp = new Grid
                                    {
                                        CONTROL_NAME = "DATAGRID",
                                        NAME = rtl.Header?.ToString(),
                                        CODE = rtl?.SortMemberPath
                                    };
                                    tmp.grids.Add(temp);
                                }

                            }
                        }
                    }
                }
                //换行
                Grid temp1 = new Grid
                {
                    CONTROL_NAME = "NEXT_LINE"
                };
                tmp.Level++;
                tmp.grids.Add(temp1);
            }
            grids.Add(tmp);
            return grids;
        }


    }

    
}
