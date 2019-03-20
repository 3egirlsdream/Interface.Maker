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
    class ExcelHelper
    {
        /// <summary>
        /// 读取表格
        /// </summary>
        /// <param name="pages">Excel页数</param>
        /// <returns></returns>
        public bool OpenExcel(int pages)
        {
            OpenFileDialog open = new OpenFileDialog
            {
                Filter = "Excel (*.XLSX)|*.xlsx|all file|*.*"
            };

            List<string> btns = new List<string>();
            List<Grid> grids = new List<Grid>();

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
                    if (t == 0)
                    {
                        XSSFSheet sheet = (XSSFSheet)xss.GetSheetAt(t);
                        int cot = sheet.LastRowNum;
                        //取第一行按钮
                        for (int i = 0; i < 13; i++)
                        {
                            string btn = sheet.GetRow(1).GetCell(i) == null ? "" : sheet.GetRow(1).GetCell(i).ToString();
                            if (String.IsNullOrEmpty(btn))
                                break;
                            btns.Add(btn);
                        }
                        
                        //读取其他控件
                        for(int i = 2; i <= cot; i++)
                        {
                            string Signal = sheet.GetRow(i).GetCell(0) == null ? "" : sheet.GetRow(i).GetCell(0).ToString();
                            switch (Signal)
                            {
                                case "按钮":break;
                                case "普通控件":break;
                                case "表格":break;
                                default:break;
                            }
                            string btn = sheet.GetRow(1).GetCell(i) == null ? "" : sheet.GetRow(1).GetCell(i).ToString();
                        }

                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Warning warning = new Warning(ex.Message);
                warning.ShowDialog();
                return false;
            }


        }


        /// <summary>
        /// 加载XML配置文件
        /// </summary>
        /// <returns></returns>
        public static object LoadXml()
        {
            int count = 0;
            string en = "";
            string zh = "";
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

            return new
            {
                count,
                en,
                zh,
                sharemodel
            };
        }
    }
}
