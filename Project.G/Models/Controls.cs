using Model.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            string s = "<WrapPanel Grid.Row=\"0\">\r\n";
            int t = Contents.Count % 3 == 0 ? Contents.Count / 3 : (Contents.Count / 3) + 1;

            for (int j = 0; j < Contents.Count; j++)
            {
                s += "<WrapPanel  Margin=\"10,10,0,0\">";
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

        #region 私有函数

        /// <summary>
        /// 生成TextBox代码
        /// </summary>
        /// <returns></returns>
        private static string CreateTextBox(string Binding)
        {
            string s = "\r\n<TextBox Text=\"{Binding " + Binding + ", Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}\" " +
                "controls:TextBoxHelper.Watermark=\"{DynamicResource " + Binding + "_Watermark}\" " +
                "Margin=\"0,0,50,0\" />\r\n";
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
                "Margin=\"0,0,50,0\" />\r\n";
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
                "Margin=\"0,0,50,0\" " +
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
            string s = "<ComboBox Margin=\"0,0,50,0\" " +
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
            string s = "<DatePicker Text=\"{Binding " + Binding + ", Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}\" Margin=\"0,0,50,0\"/>\r\n";
            return s;
        }

        private static string CreateTextBlock(string name)
        {
            string s = "";
            if (string.IsNullOrEmpty(name))
            {
                s += "<TextBlock VerticalAlignment=\"Center\" Width=\"60\" TextWrapping=\"Wrap\"/>\r\n";
            }
            else
                s += "<TextBlock Text=\"{DynamicResource Grid_" + name + "}\" VerticalAlignment=\"Center\" Width=\"60\"  TextWrapping=\"Wrap\"/>\r\n";
            return s;
        }

        //占位
        private static string EmptyControl()
        {
            string s = "<TextBox Text=\"{Binding ABC, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}\" Margin=\"0,0,50,0\" Foreground=\"Transparent\" BorderThickness=\"0\" Background=\"Transparent\" IsEnabled=\"False\"/>\r\n";
            return s;
        }

        #endregion
    }
}
