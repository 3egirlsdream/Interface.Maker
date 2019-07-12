using GenerateToolbox.ViewModel;
using Microsoft.Win32;
using NPOI.XSSF.UserModel;
using Project.G.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Xu.Common;

namespace GenerateToolbox.Models
{
    public class ExcelHelper
    {
        
        /// <summary>
        /// 读取表格
        /// </summary>
        /// <param name="pages">Excel页数</param>
        /// <returns></returns>
        public List<Grids> OpenExcel(int pages)
        {
            OpenFileDialog open = new OpenFileDialog
            {
                Filter = "Excel (*.XLSX)|*.xlsx|all file|*.*"
            };

            List<Grids> listGrids = new List<Grids>();

            open.ShowDialog();
            try
            {
                XSSFWorkbook xss;
                using (FileStream fs = new FileStream(open.FileName, FileMode.Open, FileAccess.Read))
                {
                    xss = new XSSFWorkbook(fs);
                }
                for (int t = 0; t < pages; t++)
                {
                    Grids grids = new Grids();
                    XSSFSheet sheet = (XSSFSheet)xss.GetSheetAt(t);
                    int cot = sheet.LastRowNum;
                    //取第一行标识
                    string Identity = sheet.GetRow(0).GetCell(0) == null ? "" : sheet.GetRow(0).GetCell(0).ToString();
                    string PageCode = sheet.GetRow(0).GetCell(1) == null ? "" : sheet.GetRow(0).GetCell(1).ToString();
                    string PageName = sheet.GetRow(0).GetCell(2) == null ? "" : sheet.GetRow(0).GetCell(2).ToString();
                    //string Level = sheet.GetRow(0).GetCell(3) == null ? "" : sheet.GetRow(0).GetCell(3).ToString();
                    grids.Identity = Identity;
                    grids.PageCode = PageCode;
                    grids.PageName = PageName;
                    //grids.Level = Convert.ToInt32(Level);
                    //取第二行按钮
                    for (int i = 0; i < 13; i++)
                    {
                        string btn = sheet.GetRow(1).GetCell(i) == null ? "" : sheet.GetRow(1).GetCell(i).ToString();
                        if (String.IsNullOrEmpty(btn))
                            break;
                        grids.strs.Add(btn);
                    }

                    //读取其他控件
                    for (int i = 2; i <= cot; i++)
                    {
                        string Signal = sheet.GetRow(i).GetCell(0) == null ? "" : sheet.GetRow(i).GetCell(0).ToString();
                        switch (Signal)
                        {
                            case "按钮":
                                {
                                    var name = sheet.GetRow(i).GetCell(1) == null ? "" : sheet.GetRow(i).GetCell(1).ToString();
                                    Grid grid = new Grid
                                    {
                                        CONTROL_NAME = "btn",
                                        NAME = name
                                    };
                                    grids.grids.Add(grid);
                                }
                                break;
                            case "普通控件":
                                {
                                    var controlName = sheet.GetRow(i).GetCell(1) == null ? "" : sheet.GetRow(i).GetCell(1).ToString();
                                    var Code = sheet.GetRow(i).GetCell(2) == null ? "" : sheet.GetRow(i).GetCell(2).ToString();
                                    var Name = sheet.GetRow(i).GetCell(3) == null ? "" : sheet.GetRow(i).GetCell(3).ToString();
                                    var isApi = sheet.GetRow(i).GetCell(4) == null ? "" : sheet.GetRow(i).GetCell(4).ToString();
                                    Grid grid = new Grid
                                    {
                                        CONTROL_NAME = controlName,
                                        NAME = Name,
                                        CODE = Code,
                                        IS_API = isApi == "YES" ? true : false
                                    };
                                    grids.grids.Add(grid);
                                }
                                break;
                            case "表格":
                                {
                                    var gridCode = sheet.GetRow(i).GetCell(1) == null ? "" : sheet.GetRow(i).GetCell(1).ToString();
                                    var gridName = sheet.GetRow(i).GetCell(2) == null ? "" : sheet.GetRow(i).GetCell(2).ToString();
                                    Grid grid = new Grid
                                    {
                                        CONTROL_NAME = "DATAGRID",
                                        NAME = gridName,
                                        CODE = gridCode
                                    };
                                    grids.grids.Add(grid);
                                }
                                break;
                            case "换行":
                                {
                                    Grid grid = new Grid
                                    {
                                        CONTROL_NAME = "NEXT_LINE"
                                    };
                                    grids.Level++;
                                    grids.grids.Add(grid);
                                }
                                break;
                            default: break;
                        }
                    }
                    listGrids.Add(grids);   
                }
                return listGrids;
            }
            catch (Exception ex)
            {
                Warning warning = new Warning(ex.Message);
                warning.ShowDialog();
                return null;
            }


        }

        public static string zh { get; set; }
        public static string en { get; set; }
        /// <summary>
        /// 加载XML配置文件
        /// </summary>
        /// <returns></returns>
        public static object LoadXml()
        {
            int count = 0;
            //string en = "";
            //string zh = "";
            string sharemodel = "";

            XmlDocument doc = new XmlDocument();
            doc.Load("config.xml");
            XmlElement root = doc.DocumentElement;
            XmlNodeList pages = root.GetElementsByTagName("page");
            foreach (XmlNode node in pages)
            {
                XmlNodeList cot = ((XmlElement)node).GetElementsByTagName("count");
                if (cot.Count == 1)
                {
                    count = Convert.ToInt32(cot[0].InnerText);
                }
            }

            XmlNodeList projects = root.GetElementsByTagName("project");
            foreach (XmlNode node in projects)
            {
                XmlNodeList names = ((XmlElement)node).GetElementsByTagName("name");
                foreach (XmlNode name in names)
                {
                    XmlNodeList english = ((XmlElement)node).GetElementsByTagName("en");
                    if (english.Count == 1)
                    {
                        en = english[0].InnerText;
                    }

                    XmlNodeList chinese = ((XmlElement)node).GetElementsByTagName("zh");
                    if (english.Count == 1)
                    {
                        zh = chinese[0].InnerText;
                    }
                }

                XmlNodeList paths = ((XmlElement)node).GetElementsByTagName("path");
                foreach (XmlNode path in paths)
                {
                    XmlNodeList ShareModel = ((XmlElement)node).GetElementsByTagName("sharemodel");
                    if (ShareModel.Count == 1)
                    {
                        sharemodel = ShareModel[0].InnerText;
                    }
                }
            }

            List<DataGrids> datas = new List<DataGrids>(); 

            //datagrid
            XmlNodeList datagrids = root.GetElementsByTagName("datagrid");
            foreach(XmlNode node in datagrids)
            {
                DataGrids data = new DataGrids();

                XmlNodeList ItemsSources = ((XmlElement)node).GetElementsByTagName("ItemsSource");
                XmlNodeList SelectedItems = ((XmlElement)node).GetElementsByTagName("SelectedItem");
                XmlNodeList PageSizes = ((XmlElement)node).GetElementsByTagName("PageSize");
                XmlNodeList TotalCounts = ((XmlElement)node).GetElementsByTagName("TotalCount");
                XmlNodeList PageIndexs = ((XmlElement)node).GetElementsByTagName("PageIndex");
                XmlNodeList IsSelectedAll = ((XmlElement)node).GetElementsByTagName("IsSelectedAll");

                data.ItemsSource = ItemsSources[0].InnerText;
                data.SelectedItem = SelectedItems[0].InnerText;
                data.PageIndex = PageIndexs[0].InnerText;
                data.PageSize = PageSizes[0].InnerText;
                data.TotalCount = TotalCounts[0].InnerText;
                data.IsSelectedAll = IsSelectedAll[0].InnerText;
                datas.Add(data);
            }

            CsprojName csproj = new CsprojName();
            csproj.ShowDialog();

            return new
            {
                count,
                en = en,
                zh,
                sharemodel,
                datas
            };
        }

        public static bool ExsitControlConfig(string control)
        {
            switch (control)
            {
                //case "TextBox": return true;
                case "TextBox带弹出框": return true;
                //case "Combox": return true;
                //case "DatePicker": return true;
                //case "只读TextBox": return true;
                //case "占位控件": return true;
                default:return false;
            }
        }
    }
}
