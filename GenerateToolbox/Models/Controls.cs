using Model.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xu.Common;

namespace Project.G.Models
{
    public static class Controls
    {
        /// <summary>
        /// 生成查询条件xaml代码
        /// </summary>
        /// <returns></returns>
        public static string CreateContents(List<Excel> Contents)
        {
            string s = "<WrapPanel>\r\n";

            for (int j = 0; j < Contents.Count; j++)
            {
                s += "<WrapPanel  Margin=\"10,5,0,5\">";
                s += CreateTextBlock(Contents[j].SEARCH_CODE);
                switch (Contents[j].CONTROL_CODE)
                {
                    case "TextBox": s += CreateTextBox(Contents[j].SEARCH_CODE); break;
                    case "TextBox带弹出框": s += CreateTextBoxWithCommand(Contents[j].SEARCH_CODE); break;
                    case "Combox": s += CreateCombox(Contents[j].SEARCH_CODE); break;
                    case "DatePicker": s += CreateDatePicker(Contents[j].SEARCH_CODE); break;
                    case "占位控件": s += EmptyControl(); break;
                    case "只读TextBox": s += ReadonlyTextbox(Contents[j].SEARCH_CODE); break;
                    default: break;
                }
                s += "</WrapPanel>";
            }

            s += "\r\n</WrapPanel>";
            return s;
        }


        /// <summary>
        /// [新]生成查询条件xaml代码
        /// </summary>
        /// <returns></returns>
        public static object CreateContents_new(List<Grid> Contents, int j)
        {
            string s = "<GroupBox\r\n                " +
                "Margin=\"{DynamicResource ContainerPadding}\"\r\n                " +
                "Padding=\"5\"\r\n                " +
                "BorderBrush=\"#D8D8D9\"\r\n                " +
                "BorderThickness=\"1\"\r\n                " +
                "Header=\"{DynamicResource QueryCriteria}\">\r\n                \r\n            " +
                "<WrapPanel>\r\n";

            for (; j < Contents.Count; j++)
            {
                if (Contents[j].CONTROL_NAME == "NEXT_LINE")
                    break;

                s += "<WrapPanel  Margin=\"10,5,0,5\">";
                if (Contents[j].CONTROL_NAME != "btn")
                {
                    s += CreateTextBlock(Contents[j].CODE);
                }
                switch (Contents[j].CONTROL_NAME)
                {
                    case "TextBox": s += CreateTextBox(Contents[j].CODE); break;
                    case "TextBox带弹出框": s += CreateTextBoxWithCommand(Contents[j].CODE); break;
                    case "Combox": s += CreateCombox(Contents[j].CODE); break;
                    case "DatePicker": s += CreateDatePicker(Contents[j].CODE); break;
                    case "占位控件": s += EmptyControl(); break;
                    case "只读TextBox": s += ReadonlyTextbox(Contents[j].CODE); break;
                    case "btn": s += Buttons.CreateButton_Custom(Contents[j].NAME); break;
                    default: break;
                }
                s += "</WrapPanel>\r\n";
            }
            s += "\r\n</WrapPanel>" + "</GroupBox>";
            return new
            {
                str = s,
                count = j
            };
        }



        /// <summary>
        /// 新增的界面控件
        /// </summary>
        /// <param name="Contents"></param>
        /// <returns></returns>
        public static string CreateAddContents(List<Excel> Contents)
        {
            string s = "";
            for (int i = 0; i < Contents.Count(); i++)
            {
                s += "<WrapPanel Grid.Row=\"" + i + "\" Margin=\"70,15,0,0\">\r\n";

                s += CreateTextBlock(Contents[i].SEARCH_CODE);
                switch (Contents[i].CONTROL_CODE)
                {
                    case "TextBox": s += CreateTextBox(Contents[i].SEARCH_CODE); break;
                    case "TextBox带弹出框": s += CreateTextBoxWithCommand(Contents[i].SEARCH_CODE); break;
                    case "Combox": s += CreateCombox(Contents[i].SEARCH_CODE); break;
                    case "DatePicker": s += CreateDatePicker(Contents[i].SEARCH_CODE); break;
                    case "占位控件": s += EmptyControl(); break;
                    default: break;
                }

                s += "\r\n</WrapPanel>";
            }
            return s;
        }



        /// <summary>
        /// 生成表格
        /// </summary>
        /// <returns></returns>
        public static string CreateDataGrid(List<Excel> Bodies)
        {
            string s = "<DataGridTemplateColumn>\r\n" +
                            "<DataGridTemplateColumn.HeaderTemplate>\r\n" +
                                "<DataTemplate>\r\n" +
                                    "<CheckBox IsChecked=\"{Binding DataContext.IsSelectedAll, RelativeSource={RelativeSource AncestorType=common:PagePlugin, Mode=FindAncestor}, UpdateSourceTrigger=PropertyChanged, Mode=TwoWay}\"/>\r\n" +
                                "</DataTemplate>\r\n" +
                           "</DataGridTemplateColumn.HeaderTemplate>\r\n" +
                            "<DataGridTemplateColumn.CellTemplate>\r\n" +
                                "<DataTemplate>\r\n" +
                                    "<CheckBox IsChecked=\"{Binding IsChecked, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}\"/>\r\n" +
                                "</DataTemplate>\r\n" +
                            "</DataGridTemplateColumn.CellTemplate>\r\n" +
                        "</DataGridTemplateColumn>\r\n";

            foreach (var ds in Bodies)
            {
                string tmp = "<DataGridTemplateColumn Width=\"150\" HeaderStyle=\"{DynamicResource DataGridColumnHeader_Center}\" CellStyle=\"{DynamicResource DataGridCell_Center}\">\r\n" +
                            "<DataGridTemplateColumn.Header>\r\n" +
                                "<TextBlock Text=\"{DynamicResource Grid_" + ds.GRID_CODE + "}\"/>\r\n" +
                            "</DataGridTemplateColumn.Header>\r\n" +
                            "<DataGridTemplateColumn.CellTemplate>\r\n" +
                                "<DataTemplate>\r\n" +
                                    "<TextBlock Text=\"{Binding " + ds.GRID_CODE + "}\"/>\r\n" +
                                "</DataTemplate>\r\n" +
                            "</DataGridTemplateColumn.CellTemplate>\r\n" +
                        "</DataGridTemplateColumn>\r\n";
                s += tmp;
            }
            return s;
        }

        /// <summary>
        /// 生成导入界面表格
        /// </summary>
        /// <returns></returns>
        public static string CreateDataGrid(ImportClass Bodies)
        {
            string s = "<DataGridTemplateColumn>\r\n" +
                            "<DataGridTemplateColumn.Header>\r\n" +
                                "<TextBlock Text=\"{DynamicResource Status}\" />\r\n" +
                           "</DataGridTemplateColumn.Header>\r\n" +
                            "<DataGridTemplateColumn.CellTemplate>\r\n" +
                                "<DataTemplate>\r\n" +
                                    "<TextBlock Foreground=\"{Binding Color}\" Text=\"{Binding TextState}\" />\r\n" +
                                "</DataTemplate>\r\n" +
                            "</DataGridTemplateColumn.CellTemplate>\r\n" +
                        "</DataGridTemplateColumn>\r\n";
            foreach (var ds in Bodies.Body)
            {
                string tmp = "<DataGridTemplateColumn Width=\"150\" HeaderStyle=\"{DynamicResource DataGridColumnHeader_Center}\" CellStyle=\"{DynamicResource DataGridCell_Center}\">\r\n" +
                            "<DataGridTemplateColumn.Header>\r\n" +
                                "<TextBlock Text=\"{DynamicResource Grid_" + ds.GRID_CODE + "}\"/>\r\n" +
                            "</DataGridTemplateColumn.Header>\r\n" +
                            "<DataGridTemplateColumn.CellTemplate>\r\n" +
                                "<DataTemplate>\r\n" +
                                    "<TextBlock Text=\"{Binding " + ds.GRID_CODE + "}\"/>\r\n" +
                                "</DataTemplate>\r\n" +
                            "</DataGridTemplateColumn.CellTemplate>\r\n" +
                        "</DataGridTemplateColumn>\r\n";
                s += tmp;
            }
            return s;
        }

        /// <summary>
        /// [新]生成表格
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        public static object CreateDataGrid_new(List<Grid> grids, int i)
        {
            string s = "<DataGridTemplateColumn>\r\n" +
                            "<DataGridTemplateColumn.HeaderTemplate>\r\n" +
                                "<DataTemplate>\r\n" +
                                    "<CheckBox IsChecked=\"{Binding DataContext.IsSelectedAll, RelativeSource={RelativeSource AncestorType=common:PagePlugin, Mode=FindAncestor}, UpdateSourceTrigger=PropertyChanged, Mode=TwoWay}\"/>\r\n" +
                                "</DataTemplate>\r\n" +
                           "</DataGridTemplateColumn.HeaderTemplate>\r\n" +
                            "<DataGridTemplateColumn.CellTemplate>\r\n" +
                                "<DataTemplate>\r\n" +
                                    "<CheckBox IsChecked=\"{Binding IsChecked, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}\"/>\r\n" +
                                "</DataTemplate>\r\n" +
                            "</DataGridTemplateColumn.CellTemplate>\r\n" +
                        "</DataGridTemplateColumn>\r\n";

            for (;i < grids.Count; i++)
            {
                if(grids[i].CONTROL_NAME == "换行")
                    break;
                string tmp = "<DataGridTemplateColumn Width=\"150\" HeaderStyle=\"{DynamicResource DataGridColumnHeader_Center}\" CellStyle=\"{DynamicResource DataGridCell_Center}\">\r\n" +
                            "<DataGridTemplateColumn.Header>\r\n" +
                                "<TextBlock Text=\"{DynamicResource Grid_" + grids[i].CODE + "}\"/>\r\n" +
                            "</DataGridTemplateColumn.Header>\r\n" +
                            "<DataGridTemplateColumn.CellTemplate>\r\n" +
                                "<DataTemplate>\r\n" +
                                    "<TextBlock Text=\"{Binding " + grids[i].CODE + "}\"/>\r\n" +
                                "</DataTemplate>\r\n" +
                            "</DataGridTemplateColumn.CellTemplate>\r\n" +
                        "</DataGridTemplateColumn>\r\n";
                if(grids[i].CONTROL_NAME == "表格")
                    s += tmp;
            }
            return new
            {
                str = s,
                count = i
            };
        }

        /// <summary>
        /// 生成绑定字段
        /// </summary>
        /// <returns></returns>
        public static string CreateWord(List<Excel> Contents)
        {
            Model.Helper.ModelHelper model = new ModelHelper();
            string s = "#region 字段\r\n\t";
            foreach (var ds in Contents)
            {
                if (ds.CONTROL_CODE == "TextBox" || ds.CONTROL_CODE == "TextBox带弹出框" || ds.CONTROL_CODE == "DatePicker")
                {
                    s += AddNotify_Click(ds.SEARCH_CODE, ds.SEARCH_NAME) + "\r\n";
                }

                if (ds.CONTROL_CODE == "Combox")
                {
                    s += "\t//" + ds.SEARCH_NAME + "\r\n\t";
                    s += AddNotify_Click("ObservableCollection<ComboxModel> " + ds.SEARCH_CODE, ds.SEARCH_NAME) + "\r\n";
                    s += AddNotify_Click("ComboxModel " + "Filter_" + ds.SEARCH_CODE, ds.SEARCH_NAME) + "\r\n";
                }
            }
            s += "#endregion\r\n";
            return s;
        }

        #region 新版生成vm字段
        /// <summary>
        /// [新]生成绑定字段
        /// </summary>
        /// <returns></returns>
        public static string CreateWord_new(Grids Contents)
        {
            Model.Helper.ModelHelper model = new ModelHelper();
            string s = "#region 字段\r\n\t";
            foreach (var ds in Contents.grids)
            {
                if (ds.CONTROL_NAME == "TextBox" || ds.CONTROL_NAME == "TextBox带弹出框" || ds.CONTROL_NAME == "DatePicker")
                {
                    s += AddNotify_Click(ds.CODE, ds.NAME) + "\r\n";
                }

                if (ds.CONTROL_NAME == "Combox")
                {
                    //s += "\t//" + ds.NAME + "\r\n\t";
                    s += AddNotify_Click("ObservableCollection<ComboxModel> " + ds.CODE, ds.NAME) + "\r\n";
                    s += AddNotify_Click("ComboxModel " + "Filter_" + ds.CODE, ds.NAME) + "\r\n";
                }
            }
            s += "#endregion\r\n";
            return s;
        }
        #endregion

        #region 私有函数

        /// <summary>
        /// 生成TextBox代码
        /// </summary>
        /// <returns></returns>
        private static string CreateTextBox(string Binding)
        {
            string s = "\r\n<TextBox Text=\"{Binding " + Binding + ", Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}\" " +
                "controls:TextBoxHelper.Watermark=\"{DynamicResource " + Binding + "_Watermark}\" " +
                "Margin=\"0,0,30,0\" />\r\n";
            return s;
        }

        private static string ReadonlyTextbox(string Binding)
        {
            string s = "\r\n<TextBox Text=\"{Binding " + Binding + ", Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}\" " +
                "controls:TextBoxHelper.ClearTextButton = \"False\" " +
                "controls:TextBoxHelper.SelectAllOnFocus = \"True\" " +
                "IsReadOnly=\"True\" " +
                "controls:TextBoxExHelper.IsUnderLine = \"True\" " +
                "controls:TextBoxHelper.Watermark=\"{DynamicResource " + Binding + "_Watermark}\" " +
                "Margin=\"0,0,30,0\" />\r\n";
            return s;
        }

        /// <summary>
        /// 生成带弹出框的TextBox
        /// </summary>
        /// <param name="Binding">绑定的值</param>
        /// <param name="name">查询条件名字</param>
        /// <returns></returns>
        private static string CreateTextBoxWithCommand(string Binding)
        {
            Model.Helper.ModelHelper model = new ModelHelper();

            string s = "<TextBox Text=\"{Binding " + Binding + ", Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}\" " +
                "Margin=\"0,0,30,0\" " +
                "controls:TextBoxHelper.ButtonCommand=\"{Binding " + model.GetCmd(Binding) + "}\" " +
                "controls:TextBoxHelper.ButtonContent=\"2\" " +
                "controls:TextBoxHelper.ClearTextButton=\"True\" " +
                "controls:TextBoxHelper.Watermark=\"{DynamicResource " + Binding + "_Watermark}\" Style=\"{DynamicResource MetroButtonTextBox}\"/>\r\n";
            return s;
        }

        /// <summary>
        /// 生成下拉框
        /// </summary>
        /// <param name="DataSource">下拉框的英文名字</param>
        /// <returns></returns>
        private static string CreateCombox(string DataSource)
        {
            Model.Helper.ModelHelper model = new ModelHelper();
            string s = "<ComboBox Margin=\"0,0,30,0\" " +
                "controls:TextBoxHelper.Watermark=\"{DynamicResource Grid_" + DataSource + "}\" " +
                "ItemsSource=\"{Binding " + DataSource + ", Mode=TwoWay}\" " +
                "SelectedItem=\"{Binding Filter_" + DataSource + ", Mode=TwoWay}\"/>\r\n";
            return s;
        }

        /// <summary>
        /// 生成日期控件
        /// </summary>
        /// <param name="Binding">绑定的值</param>
        /// <returns></returns>
        private static string CreateDatePicker(string Binding)
        {
            string s = "<DatePicker Text=\"{Binding " + Binding + ", Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}\" Margin=\"0,0,30,0\"/>\r\n";
            return s;
        }

        private static string CreateTextBlock(string name)
        {
            string s = "";
            if (string.IsNullOrEmpty(name))
            {
                s += "<TextBlock VerticalAlignment=\"Center\" Width=\"70\" TextWrapping=\"Wrap\"/>\r\n";
            }
            else
                s += "<TextBlock Text=\"{DynamicResource Grid_" + name + "}\" VerticalAlignment=\"Center\" Width=\"70\"  TextWrapping=\"Wrap\"/>\r\n";
            return s;
        }

        //占位
        private static string EmptyControl()
        {
            string s = "<TextBox Text=\"{Binding ABC, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}\" Margin=\"0,0,30,0\" Foreground=\"Transparent\" BorderThickness=\"0\" Background=\"Transparent\" IsEnabled=\"False\"/>\r\n";
            return s;
        }

        /// <summary>
        /// 生成监视
        /// </summary>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static string AddNotify_Click(string code, string name)
        {
            List<string> arrs = new List<string>(code.Split(' '));

            string sr = "";
            if (arrs.Count == 1)
            {
                sr += "private string _" + arrs[0] + ";public string " + arrs[0]
                    + "{get{return _" + arrs[0] + ";}set{_" + arrs[0] + " = value;NotifyPropertyChanged(\"" + arrs[0] + "\");}";
            }
            else if (arrs.Count == 2)
            {
                sr += "private " + arrs[0] + " _" + arrs[1] + ";public " + arrs[0] + " " + arrs[1]
                    + "{get{return _" + arrs[1] + ";}set{_" + arrs[1] + " = value;NotifyPropertyChanged(\"" + arrs[1] + "\");}";
            }
            else if (arrs.Count > 2)
            {
                sr += "private " + arrs[1] + " _" + arrs[2] + ";"
                    + arrs[0] + " " + arrs[1] + " " + arrs[2]
                    + "{get{return _" + arrs[2] + ";}set{_" + arrs[2] + " = value;NotifyPropertyChanged(\"" + arrs[2] + "\");}";
            }
            sr = System.Text.RegularExpressions.Regex.Replace(sr, "[\r\n\t]", "");
            return "/// <summary>\r\n/// " + name + "\r\n/// </summary>\r\n" + Xu.Common.Common.format(sr + '}');
        }

        /// <summary>
        /// 生成command
        /// </summary>
        /// <param name="s">名字，Cmdxxx形式</param>
        /// <param name="model">弹出框的数据，为了获取弹出框名 model.BOX_CODE</param>
        /// <param name="excel">获取IndexPage的查询字段</param>
        /// <returns></returns>
        public static string AddCommand(string s, string ProjectName, MyModel model = null, Excel excel = null)
        {
            StringBuilder str = new StringBuilder("");
            if (s == "CmdExport")
            {
                str.Append("/// <summary>\r\n/// 导出\r\n/// </summary>\r\n");
                str.Append("public SimpleCommand " + s.Replace("\n", "") + " => new SimpleCommand(){ExecuteDelegate = x =>{" +
                    "ExcelHelper.Export<Model>(DataSource, Translator.Get(\"Title\") );" +
                    "},CanExecuteDelegate = o =>{return true;}};");
            }
            else if (s == "CmdImport")
            {
                str.Append("/// <summary>\r\n/// 导入\r\n/// </summary>\r\n");
                str.Append("public SimpleCommand " + s.Replace("\n", "") + " => new SimpleCommand(){ExecuteDelegate = x =>{" +
                    "plugin.Framework.OpenWindowPlugin(\"" + ProjectName + ".dll;" + ProjectName + ".ImportPage\",\"\");" +
                    "},CanExecuteDelegate = o =>{return true;}};");
            }
            else if (s == "CmdDelete")
            {
                str.Append("/// <summary>\r\n/// 删除选中的数据\r\n/// </summary>\r\n");
                str.Append("public SimpleCommand " + s.Replace("\n", "") + " => new SimpleCommand(){ExecuteDelegate = x =>{" +
                    "var list = DataSource.Where(e => e.IsChecked).Select(e => e.ID).ToList();" +
                    "plugin.Framework.PostData(Services.url_delete, new" +
                    "{" +
                    "ids = list" +
                    "});" +
                    "},CanExecuteDelegate = o =>{return DataSource != null && DataSource.Where(e => e.IsChecked).Count() > 0;}};");
            }
            else if (s == "CmdAdd")
            {
                str.Append("/// <summary>\r\n/// 新增\r\n/// </summary>\r\n");
                str.Append("public SimpleCommand " + s.Replace("\n", "") + " => new SimpleCommand(){ExecuteDelegate = x =>{" +
                    "plugin.Framework.OpenWindowPlugin(\"" + ProjectName + ".dll;" + ProjectName + ".Add\",\"\");" +
                    "},CanExecuteDelegate = o =>{return true;}};");
            }
            else if (s == "CmdRefresh")
            {
                str.Append("/// <summary>\r\n/// 刷新\r\n/// </summary>\r\n");
                str.Append("public SimpleCommand " + s.Replace("\n", "") + " => new SimpleCommand(){ExecuteDelegate = x =>{LoadData();},CanExecuteDelegate = o =>{return true;}};");
            }
            else
            {
                str.Append("/// <summary>\r\n/// 打开新窗体\r\n/// </summary>\r\n");
                string ss = "public SimpleCommand " + s.Replace("\n", "") + " => new SimpleCommand(){ExecuteDelegate = x =>{";
                if (model != null)
                {
                    ss += "var res = plugin.Framework.OpenWindowPlugin(\"" + ProjectName + ".dll;" + ProjectName + "." + model.BOX_CODE + "\",\"\");" +
                    "if(res.success){" +
                    excel.SEARCH_CODE + " = res.data." + Reback(excel.SEARCH_CODE) + ";" +
                    "}";
                }
                ss += "},CanExecuteDelegate = o =>{return true;}};";
                str.Append(ss);
            }

            return Common.format(str.ToString());
        }

        /// <summary>
        /// [新]生成command
        /// </summary>
        /// <param name="s">名字，Cmdxxx形式</param>
        /// <param name="model">弹出框的数据，为了获取弹出框名 model.BOX_CODE</param>
        /// <param name="excel">获取IndexPage的查询字段</param>
        /// <returns></returns>
        public static string AddCommand_new(string s, string ProjectName, string code)
        {
            StringBuilder str = new StringBuilder("");
            str.Append("/// <summary>\r\n///打开新窗体\r\n/// </summary>\r\n");
            string ss = "public SimpleCommand " + s.Replace("\n", "") + " => new SimpleCommand(){ExecuteDelegate = x =>{";

            ss += "var res = plugin.Framework.OpenWindowPlugin(\"" + ProjectName + ".dll;" + ProjectName + ".\",\"\");" +
            "if(res.success){" +
            code + " = res.data." + Reback(code) + ";" +
            "}";

            ss += "},CanExecuteDelegate = o =>{return true;}};";
            str.Append(ss);
            
            return Common.format(str.ToString());
        }

        /// <summary>
        /// 获取表格绑定字段
        /// </summary>
        /// <returns></returns>
        public static string GetDataGridBinding(List<DataGrids> datas)
        {
            string str = "";
            foreach(var data in datas)
            {
                str += " \t#region 表格绑定\r\n        /// <summary>\r\n        /// 表格主数据\r\n        /// </summary>\r\n        " +
                                "private List<Model> _"+data.ItemsSource+";\r\n        " +
                                "public List<Model> "+data.ItemsSource+"\r\n        {\r\n            get\r\n            {\r\n                " +
                                "return _"+data.ItemsSource+";\r\n            }\r\n            set\r\n            {\r\n                " +
                                "_"+data.ItemsSource+" = value;\r\n                " +
                                "NotifyPropertyChanged(\""+data.ItemsSource+"\");\r\n            }\r\n        }\r\n\r\n\r\n\r\n\r\n        /// <summary>\r\n        /// 选中行\r\n        /// </summary>\r\n        " +
                                "private Model _"+data.SelectedItem+";\r\n        " +
                                "public Model "+data.SelectedItem+"\r\n        {\r\n            get\r\n            {\r\n                " +
                                "return _"+data.SelectedItem+";\r\n            }\r\n            set\r\n            {\r\n                " +
                                "_"+data.SelectedItem+" = value;\r\n                " +
                                "NotifyPropertyChanged(\""+data.SelectedItem+"\");\r\n            }\r\n        }\r\n\r\n        #region 分页\r\n\r\n        " +
                                "private int _"+data.PageIndex+" = 1;\r\n        " +
                                "public int "+data.PageIndex+"\r\n        {\r\n            " +
                                "get { return _"+data.PageIndex+"; }\r\n            set\r\n            {\r\n                " +
                                "if (_"+data.PageIndex+" != value)\r\n                {\r\n                    " +
                                "_"+data.PageIndex+" = value;\r\n                    " +
                                "NotifyPropertyChanged(\""+data.PageIndex+"\");\r\n                    " +
                                "LoadData();\r\n                }\r\n            }\r\n        }\r\n\r\n        " +
                                "private int _"+data.PageSize+" = 20;\r\n        " +
                                "public int "+data.PageSize+"\r\n        {\r\n            " +
                                "get { return _"+data.PageSize+"; }\r\n            " +
                                "set\r\n            {\r\n                " +
                                "_"+data.PageSize+" = value;\r\n                " +
                                "NotifyPropertyChanged(\""+data.PageSize+"\");\r\n                " +
                                "LoadData();\r\n            }\r\n        }\r\n\r\n\r\n        " +
                                "private int _"+data.TotalCount+";\r\n        " +
                                "public int "+data.TotalCount+"\r\n        {\r\n            " +
                                "get { return _"+data.TotalCount+"; }\r\n            " +
                                "set\r\n            {\r\n                " +
                                "if (_"+data.TotalCount+" != value)\r\n                {\r\n                    " +
                                "_"+data.TotalCount+" = value;\r\n                    " +
                                "NotifyPropertyChanged(\""+data.TotalCount+"\");\r\n                }\r\n            }\r\n        }\r\n\r\n        \t#endregion\r\n" +
                                "\r\n\r\n\r\n        " +
                                "private bool _"+data.IsSelectedAll+";\r\n        /// <summary>\r\n        /// 是否全部勾选\r\n        /// </summary>\r\n        " +
                                "public bool "+data.IsSelectedAll+"\r\n        {\r\n            " +
                                "get { return _"+data.IsSelectedAll+"; }\r\n            set\r\n            {\r\n                " +
                                "_"+data.IsSelectedAll+" = value;\r\n                " +
                                "foreach(var ds in " + data.ItemsSource + ")\r\n                {\r\n                    " +
                                "ds.IsChecked = value;\r\n                }\r\n                " +
                                "NotifyPropertyChanged(\""+data.IsSelectedAll+ "\");\r\n            }\r\n        }\r\n\r\n                \t#endregion\r\n";
            }
            return str;
        }


        private static string Reback(string s)
        {
            if (s.Contains("_"))
                return s;
            string str = s[0].ToString();
            for (int i = 1; i < s.Length - 1; i++)
            {
                str += s[i];
                if (Char.IsUpper(s[i + 1]))
                {
                    str += "_";
                }
            }
            str += s[s.Length - 1];
            return str.ToUpper();
        }
        #endregion
    }
}
