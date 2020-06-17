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
        bool IsGenerate = false;
        public static CustomSetting customSetting { get; set; } = new CustomSetting();
        public static Dictionary<string, string> CCDic { get; set; } = new Dictionary<string, string>();
        public static Dictionary<string, List<string>> PropertiesDic { get; set; } = new Dictionary<string, List<string>>();
        public NewPage()
        {
            InitializeComponent();
            pos = new Point();
            vm = new NewPageVM();
            this.DataContext = vm;
            UserControls.Control().GetFiles.ForEach(z =>
            {
                var name = z.Substring(z.LastIndexOf(@"\") + 1).Replace(".txt", "");
                UserControls.Control().CreateBtn(wrappanel, name, (x, y) =>
                {
                    CustomControl myControl = new CustomControl();
                    myControl.Name = GetControlsName("myControl");
                    myControl.MouseDoubleClick += (m, n)=>
                    {
                        UserControls.Control().CreateProperties(name);
                        //var obj = m as FrameworkElement;
                        //DataGridSetting dataGridSetting = new DataGridSetting(obj);
                        //dataGridSetting.ParentWindow = this;
                        cc.Content = new Frame { Content = customSetting };
                        ccp.Visibility = Visibility;
                        cc.Visibility = Visibility.Visible;
                    };
                    myControl.ccgrid.Margin = new Thickness(15, 20, 5, 5);
                    grid.Children.Add(myControl);
                });
            });
            
        }
        public MainWindow ParentWindow { get; set; }

        int cot = 0;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MyBorder myBorder = new MyBorder();
            myBorder.Name = GetControlsName("BTN");
            myBorder.MouseDoubleClick += Button_MouseDown;
            myBorder.btn.Margin = new Thickness(15 + cot++ * 100, 20, 5, 5);
            grid.Children.Add(myBorder);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MyTextBox textBox = new MyTextBox();
            textBox.Name = GetControlsName("TB");
            textBox.MouseDoubleClick += TextBox_MouseDown;
            textBox.btn.Margin = new Thickness(15, 5, 5, 5);
            grid.Children.Add(textBox);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MyDataGrid datagrid = new MyDataGrid();
            datagrid.Name = GetControlsName("GRID");
            datagrid.MouseDoubleClick += DataGrid_MouseDown;
            datagrid.btn.Margin = new Thickness(15, 5, 5, 5);
            grid.Children.Add(datagrid);
        }

        int t = 0;
        private void Button_Click_3(/*object sender, RoutedEventArgs e*/)
        {

            MySidebar mySidebar = new MySidebar();
            mySidebar.Name = GetControlsName("BAR");
            Thickness thickness;
            if (t == 0)
            {
                thickness = new Thickness(10, 10, 0, 0);
                t++;
            }
            else
            {
                thickness = new Thickness(10, t++ * 160 - 70, 0, 0);
            }
            mySidebar.btn.Margin = thickness;
            if (t == 1)
                mySidebar.border.Height = 60;
            Canvas.SetZIndex(mySidebar, -1);
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
        private void Create(/*object sender, RoutedEventArgs e*/)
        {
            try
            {
                if (!IsGenerate)
                {
                    sider = new List<MySidebar>();
                    foreach (dynamic ds in grid.Children)
                    {
                        string name = ds.Name as string;
                        if (!string.IsNullOrEmpty(name) && name.Contains("BAR"))
                        {
                            sider.Add(ds);
                        }
                    }

                    GetData(sider);
                    IsGenerate = true;
                }
                //生成界面
                vm.IsEnabled = false;
                //create.IsEnabled = true;
                vm.result = grids;
                vm.Genetate(vm.result);

                Warning warning = new Warning("生成成功");
                warning.ShowDialog();
            }
            catch (Exception ex)
            {
                App.Current.Dispatcher.Invoke(new Action(() =>
                {
                    Warning warning = new Warning("生成失败：" + ex.Message);
                    warning.ShowDialog();
                }));
            }
        }

        public List<Grids> grids = new List<Grids>();
        public void GetData(List<MySidebar> sider)
        {

            Grids tmp = new Grids();
            tmp.Identity = vm.FilterPageType.Value.Key;
            tmp.PageCode = vm.FilterPageType.Value.Value;
            tmp.PageName = vm.FilterPageType.Value.Key == "普通弹出框" ? vm.BoxName : vm.FilterPageType.Value.Value;
            foreach (var ds in sider)
            {
                //var borderMargin = getMargin(ds.RenderSize.Width, ds.RenderSize.Height, ds.ActualWidth, ds.ActualHeight);
                grid.Dispatcher.Invoke(new Action(() =>
                {
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
                                case "MyFooter": obj = control as MyFooter;break;
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
                                        NAME = obj.tb.Text,
                                        CODE = obj.NAME_ENG
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
                                else if(type.Name == "MyFooter")
                                {
                                    Grid temp = new Grid
                                    {
                                        CONTROL_NAME = "FOOTER"
                                    };
                                    tmp.grids.Add(temp);
                                }
                            }
                        }
                    }
                }));
                //换行
                Grid temp1 = new Grid
                {
                    CONTROL_NAME = "NEXT_LINE"
                };
                tmp.Level++;
                tmp.grids.Add(temp1);
            }
            grids.Add(tmp);
        }
        List<MySidebar> sider;
        private void Add_Page()
        {
            //拿到所有边框
            sider = new List<MySidebar>();
            foreach (dynamic ds in grid.Children)
            {
                string name = ds.Name as string;
                if (name.Contains("BAR"))
                {
                    sider.Add(ds);
                }
            }

            GetData(sider);
            grid.Children.Clear();

            cot = 0;
            t = 0;
            vm.FilterPageType = null;
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink link = sender as Hyperlink;
            System.Diagnostics.Process.Start("https://github.com/3egirlsdream/CodeGenerate/blob/master/README.md");
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MyTextBox textBox = new MyTextBox();
            textBox.Name = GetControlsName("TB");
            textBox.MouseDoubleClick += TextBox_MouseDown;
            textBox.btn.Margin = new Thickness(20, 20, 5, 5);
            grid.Children.Add(textBox);
        }

        int height = 20;
        /// <summary>
        /// 生成按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StackPanel_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            MyBorder myBorder = new MyBorder();
            myBorder.Name = GetControlsName("BTN");
            myBorder.MouseDoubleClick += Button_MouseDown;

            myBorder.btn.Margin = new Thickness(20 + cot++ * 100, height, 5, 5);
            //换行
            if (cot > 10)
            {
                cot = 0;
                height += 40;
            }
            grid.Children.Add(myBorder);
        }

        private void StackPanel_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            MyDataGrid datagrid = new MyDataGrid();
            datagrid.Name = GetControlsName("GRID");
            datagrid.MouseDoubleClick += DataGrid_MouseDown;
            datagrid.btn.Margin = new Thickness(20, 20, 5, 5);
            grid.Children.Add(datagrid);
        }


        private void AddPage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Add_Page();
        }


        private void Create_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Create();
        }

        private void StackPanel_MouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
        {
            Button_Click_3();
        }

        private void StackPanel_MouseLeftButtonDown_4(object sender, MouseButtonEventArgs e)
        {
            xmlbox.Visibility = xmlbox.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
        }

        private void Reset_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            grids = new List<Grids>();
            grid.Children.Clear();
            cot = 0;
            t = 0;
            vm.FilterPageType = null;
            IsGenerate = false;
        }

        private void StackPanel_MouseLeftButtonDown_5(object sender, MouseButtonEventArgs e)
        {
            MyFooter footer = new MyFooter();
            //footer.MouseDoubleClick += TextBox_MouseDown;
            footer.btn.Margin = new Thickness(20, 20, 5, 5);
            grid.Children.Add(footer);
        }

    }
}
