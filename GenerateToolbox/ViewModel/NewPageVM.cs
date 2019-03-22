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
        }

        #region

        private List<string> btns { get; set; }
        string dir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
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
                                    Strings.Write(Strings.GetIndexVM(res.en, "", ""), dir + "\\" + res.en + "\\ViewModels\\" + result[i].PageCode + "VM.cs");
                                }
                                break;
                            case "新增":
                                {
                                    Strings.Write(excels[i], dir + "\\" + res.en + "\\Views\\" + result[i].PageCode + ".xaml");
                                    Strings.Write(Strings.GetAddPageXamlCs(res.en), dir + "\\" + res.en + "\\Views\\" + result[i].PageCode + ".xaml.cs");
                                    Strings.Write(Strings.GetAddVM(res.en, "", ""), dir + "\\" + res.en + "\\ViewModels\\" + result[i].PageCode + "VM.cs");
                                }
                                break;
                            case "编辑":
                                {
                                    Strings.Write(excels[i], dir + "\\" + res.en + "\\Views\\" + result[i].PageCode + ".xaml");
                                    Strings.Write(Strings.GetEditPageXamlCs(res.en), dir + "\\" + res.en + "\\Views\\" + result[i].PageCode + ".xaml.cs");
                                    Strings.Write(Strings.GetEditVM(res.en, "", ""), dir + "\\" + res.en + "\\ViewModels\\" + result[i].PageCode + "VM.cs");
                                }
                                break;
                            case "导入":
                                {
                                    Strings.Write(excels[i], dir + "\\" + res.en + "\\Views\\" + result[i].PageCode + ".xaml");
                                    Strings.Write(Strings.GetImportXamlCs(res.en), dir + "\\" + res.en + "\\Views\\" + result[i].PageCode + ".xaml.cs");
                                    Strings.Write(Strings.GetImprotVM_new(res.en, result[i].grids, "", "", "", "", ""), dir + "\\" + res.en + "\\ViewModels\\" + result[i].PageCode + "VM.cs");
                                }
                                break;
                            default:
                                {
                                    Strings.Write(excels[i], dir + "\\" + res.en + "\\Views\\" + result[i].PageCode + ".xaml");
                                    Strings.Write(Strings.GetBoxesXamlCs(res.en, result[i].PageCode), dir + "\\" + res.en + "\\Views\\" + result[i].PageCode + ".xaml.cs");
                                    Strings.Write(Strings.GetBoxesVM(res.en, result[i].PageCode), dir + "\\" + res.en + "\\ViewModels\\" + result[i].PageCode + "VM.cs");
                                }
                                break;
                        }

                    }

                    Strings.Write(Strings.GetResource(res.en, ""), dir + "\\" + res.en + "\\Resources\\Strings.zh-CN.xaml");//资源文件

                    string Include = Strings.GetInclude_new(result);
                    string Complie = Strings.GetComplie_new(result);

                    Strings.Write(Strings.GetCsproj_new(res.en, Complie, Include), dir + "\\" + res.en + "\\" + res.en + ".csproj");


                    Strings.Write(Strings.GetServices(res.en), dir + "\\" + res.en + "\\Services.cs");
                }
                catch (Exception ex)
                {
                    Warning warning = new Warning(ex.Message);
                    warning.ShowDialog();
                }
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
