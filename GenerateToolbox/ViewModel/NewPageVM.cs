using GenerateToolbox.Models;
using Project.G.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Xu.Common;

namespace GenerateToolbox.ViewModel
{
    class NewPageVM : ValidationBase
    {
        public NewPageVM()
        {
            LoadConfig();
            EndOfMonth = DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month);
            BorderWidth = DateTime.Today.Day * (600 / EndOfMonth);
        }

        #region

        readonly string dir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        readonly string reset = "<root>\r\n    <page>\r\n        <count>1</count>\r\n    </page>\r\n    <project>\r\n        <name>\r\n            <en>Server</en>\r\n            <zh>服务</zh>\r\n        </name>\r\n        <path>\r\n            <sharemodel>E:\\MyData\\jiangxj.CN\\source\\CATL\\Service\\SharedModels</sharemodel>\r\n        </path>\r\n    </project>\r\n    <datagrid>\r\n        <ItemsSource>DataSource</ItemsSource>\r\n        <SelectedItem>SelectedRow</SelectedItem>\r\n        <PageSize>PageSize</PageSize>\r\n        <TotalCount>TotalCount</TotalCount>\r\n        <PageIndex>PageIndex</PageIndex>\r\n    </datagrid>\r\n    <datagrid>\r\n        <ItemsSource>DataSource2</ItemsSource>\r\n        <SelectedItem>SelectedRow2</SelectedItem>\r\n        <PageSize>PageSize2</PageSize>\r\n        <TotalCount>TotalCount2</TotalCount>\r\n        <PageIndex>PageIndex2</PageIndex>\r\n    </datagrid>\r\n    <datagrid>\r\n        <ItemsSource>DataSource3</ItemsSource>\r\n        <SelectedItem>SelectedRow3</SelectedItem>\r\n        <PageSize>PageSize3</PageSize>\r\n        <TotalCount>TotalCount3</TotalCount>\r\n        <PageIndex>PageIndex3</PageIndex>\r\n    </datagrid>\r\n</root>";
        /// <summary>
        /// 配置文件
        /// </summary>
        private string _config;
        public string config
        {
            get
            {
                return _config;
            }
            set
            {
                _config = value;
                NotifyPropertyChanged("config");
            }
        }

        /// <summary>
        /// 长度
        /// </summary>
        private int _BorderWidth;
        public int BorderWidth
        {
            get
            {
                return _BorderWidth;
            }
            set
            {
                _BorderWidth = value;
                NotifyPropertyChanged("BorderWidth");
            }
        }

        /// <summary>
        /// 月末
        /// </summary>
        private int _EndOfMonth;
        public int EndOfMonth
        {
            get
            {
                return _EndOfMonth;
            }
            set
            {
                _EndOfMonth = value;
                NotifyPropertyChanged("EndOfMonth");
            }
        }
        #endregion

        #region Command
        /// <summary>
        /// 生成函数
        /// </summary>
        public SimpleCommand CmdGenerate => new SimpleCommand()
        {
            ExecuteDelegate = c =>
            {
                try
                {
                    Strings.Write(config, "config.xml");
                    dynamic res = ExcelHelper.LoadXml();
                    CreateFile(res.en);
                    ExcelHelper helper = new ExcelHelper();
                    var result = helper.OpenExcel((int)res.count);
                    List<string> excels = assemblyPage(res.en, result, res.datas);
                    for (int i = 0; i < result.Count; i++)
                    {
                        switch (result[i].Identity)
                        {
                            case "主页":
                                {
                                    Strings.Write(excels[i], dir + "\\" + res.en + "\\Views\\" + result[i].PageCode + ".xaml");
                                    Strings.Write(Strings.GetIndexXamlCs(res.en), dir + "\\" + res.en + "\\Views\\" + result[i].PageCode + ".xaml.cs");
                                    Strings.Write(Strings.GetIndexVM(res.en, Controls.CreateWord_new(result[i]), ""), dir + "\\" + res.en + "\\ViewModels\\" + result[i].PageCode + "VM.cs");
                                }
                                break;
                            case "新增":
                                {
                                    Strings.Write(excels[i], dir + "\\" + res.en + "\\Views\\" + result[i].PageCode + ".xaml");
                                    Strings.Write(Strings.GetAddPageXamlCs(res.en), dir + "\\" + res.en + "\\Views\\" + result[i].PageCode + ".xaml.cs");
                                    Strings.Write(Strings.GetAddVM(res.en, Controls.CreateWord_new(result[i]), ""), dir + "\\" + res.en + "\\ViewModels\\" + result[i].PageCode + "VM.cs");
                                }
                                break;
                            case "编辑":
                                {
                                    Strings.Write(excels[i], dir + "\\" + res.en + "\\Views\\" + result[i].PageCode + ".xaml");
                                    Strings.Write(Strings.GetEditPageXamlCs(res.en), dir + "\\" + res.en + "\\Views\\" + result[i].PageCode + ".xaml.cs");
                                    Strings.Write(Strings.GetEditVM(res.en, Controls.CreateWord_new(result[i]), ""), dir + "\\" + res.en + "\\ViewModels\\" + result[i].PageCode + "VM.cs");
                                }
                                break;
                            case "导入":
                                {
                                    Strings.Write(excels[i], dir + "\\" + res.en + "\\Views\\" + result[i].PageCode + ".xaml");
                                    Strings.Write(Strings.GetImportXamlCs(res.en), dir + "\\" + res.en + "\\Views\\" + result[i].PageCode + ".xaml.cs");
                                    Strings.Write(Strings.GetImprotVM_new(res.en, result[i].grids, Controls.CreateWord_new(result[i]) , "", "", "", "", ""), dir + "\\" + res.en + "\\ViewModels\\" + result[i].PageCode + "VM.cs");
                                }
                                break;
                            default:
                                {
                                    Strings.Write(excels[i], dir + "\\" + res.en + "\\Views\\" + result[i].PageCode + ".xaml");
                                    Strings.Write(Strings.GetBoxesXamlCs(res.en, result[i].PageCode), dir + "\\" + res.en + "\\Views\\" + result[i].PageCode + ".xaml.cs");
                                    Strings.Write(Strings.GetBoxesVM_new(res.en, result[i].PageCode, Controls.CreateWord_new(result[i])), dir + "\\" + res.en + "\\ViewModels\\" + result[i].PageCode + "VM.cs");
                                }
                                break;
                        }

                    }

                    Strings.Write(Strings.GetResource(res.en, ""), dir + "\\" + res.en + "\\Resources\\Strings.zh-CN.xaml");//资源文件

                    string Include = Strings.GetInclude_new(result);
                    string Complie = Strings.GetComplie_new(result);

                    Strings.Write(Strings.GetCsproj_new(res.en, Complie, Include), dir + "\\" + res.en + "\\" + res.en + ".csproj");
                    
                    Strings.Write(Strings.GetServices(res.en), dir + "\\" + res.en + "\\Services.cs");
                    //两个model
                    Strings.Write(CreateClass.LoadModel_new(result, res.en), dir + "\\" + res.en + "\\Models\\Model.cs");
                    Strings.Write(CreateClass.LoadModel(res.en), dir + "\\" + res.en + "\\Models\\ComboxModel.cs");

                    //Resource文件
                    Strings.Write(Strings.GetResource(res.en, Resources.CreateResorce_new(Resources.AllRes_new(result))), dir + "\\" + res.en + "\\Resources\\Strings.zh-CN.xaml");//资源文件
                }
                catch (Exception ex)
                {
                    Warning warning = new Warning(ex.Message);
                    warning.ShowDialog();
                }
            }
        };

        public SimpleCommand CmdReset => new SimpleCommand()
        {
            ExecuteDelegate = x =>
            {
                config = reset;
            }
        };
        #endregion

        #region 私有方法
        /// <summary>
        /// 加载配置文件
        /// </summary>
        private void LoadConfig()
        {
            config = Strings.LoadJson("config.xml");
        }


        public List<string> assemblyPage(string projName, List<Grids> grids, List<DataGrids> datas)
        {
            List<string> vs = new List<string>();
            foreach (var ds in grids)
            {
                int t = 0;
                int ts = 0;
                Strings strings = new Strings();
                string xaml = strings.borderLevel(t++, Buttons.CreateButton(ds.strs));//整个界面

                string tmp = "";
                int j = 0;
                for (; j < ds.grids.Count; j++)
                {
                    var rs = ds.grids[j];
                    if (rs.CONTROL_NAME == "btn")
                    {
                        var res = Buttons.CreateButton_new(rs.NAME);
                        tmp += res;
                    }
                    else if (rs.CONTROL_NAME == "NEXT_LINE")
                    {
                        var borders = strings.borderLevel(t++, tmp);
                        tmp = "";
                        xaml += borders;
                    }
                    else if (rs.CONTROL_NAME == "DATAGRID")
                    {
                        dynamic res = strings.DataGrid(datas[ts].ItemsSource, datas[ts].SelectedItem, datas[ts].PageSize, datas[ts].TotalCount, datas[ts].PageIndex, ds.grids, j);
                        ts++;
                        tmp += res.str;
                        j = res.count - 1;
                    }
                    else
                    {
                        dynamic res = Controls.CreateContents_new(ds.grids, j);
                        tmp += res.str;
                        j = res.count - 1;
                    }
                    
                }


                string xamlCode = "";
                if(ds.Identity == "主页")
                    xamlCode = strings.PageXaml(projName, ds.PageCode, xaml);
                else
                    xamlCode = strings.WindowXaml(projName, ds.PageCode, xaml);
                //Strings.Write(xamlCode, dir + "\\" + projName + "\\Views\\IndexPage.xaml");
                vs.Add(xamlCode);
            }
            return vs;
        }


        private void CreateFile(string csproj)
        {
            Directory.CreateDirectory(dir + "\\" + csproj);
            Directory.CreateDirectory(dir + "\\" + csproj + "\\Models");
            Directory.CreateDirectory(dir + "\\" + csproj + "\\Properties");
            Directory.CreateDirectory(dir + "\\" + csproj + "\\Resources");
            Directory.CreateDirectory(dir + "\\" + csproj + "\\ViewModels");
            Directory.CreateDirectory(dir + "\\" + csproj + "\\Views");
        }
        #endregion
    }
}
