using Microsoft.Win32;
using NPOI.XSSF.UserModel;
using Project.G.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using Model.Helper;
using System.Windows;
using Xu.Common;
using System.Threading.Tasks;
using GenerateToolbox.Loading;

namespace Project.G.ViewModel
{
    class MainView : ValidationBase
    {

        public MainView()
        {
            //Domain = Strings.LoadJson("a.txt");
            LoadCombox();
            ShareModel = Common.SetConfig("ShareModel");
            BoxNumber = 0;
        }

        #region Model Part
        public List<Key_Value> ListButton { get; set; }
        public List<Key_Value> Constraints { get; set; }
        public List<Key_Value> CreateBox { get; set; }


        private Key_Value _FilterBox;
        public Key_Value FilterBox
        {
            get
            {
                return _FilterBox;
            }
            set
            {
                _FilterBox = value;
                NotifyPropertyChanged("FilterBox");
            }
        }

        /// <summary>
        /// 服务名
        /// </summary>
        private string _ServerName;
        public string ServerName
        {
            get
            {
                return _ServerName;
            }
            set
            {
                _ServerName = value;
                NotifyPropertyChanged("ServerName");
            }
        }

        private Key_Value _Filter;
        public Key_Value Filter
        {
            get
            {
                return _Filter;
            }
            set
            {
                _Filter = value;
                NotifyPropertyChanged("Filter");
            }
        }

        private Key_Value _Constraint;
        public Key_Value Constraint
        {
            get
            {
                return _Constraint;
            }
            set
            {
                _Constraint = value;
                NotifyPropertyChanged("Constraint");
            }
        }

        /// <summary>
        /// 是否生成sugar模型
        /// </summary>
        private bool _IsSugar;
        public bool IsSugar
        {
            get
            {
                return _IsSugar;
            }
            set
            {
                _IsSugar = value;
                NotifyPropertyChanged("IsSugar");
            }
        }

        private string _SqlText;
        public string SqlText
        {
            get
            {
                return _SqlText;
            }
            set
            {
                _SqlText = value;
                NotifyPropertyChanged("SqlText");
            }
        }

        private string _DBName;
        public string DBName
        {
            get
            {
                return _DBName;
            }
            set
            {
                _DBName = value;
                Common.SetConfig("DBName", value);
                NotifyPropertyChanged("DBName");
            }
        }

        private bool _IsChecked;
        public bool IsChecked
        {
            get
            {
                return _IsChecked;
            }
            set
            {
                _IsChecked = value;
                NotifyPropertyChanged("IsChecked");
            }
        }

        //是否生成模型
        private bool _ModelChecked;
        public bool ModelChecked
        {
            get
            {
                return _ModelChecked;
            }
            set
            {
                _ModelChecked = value;
                NotifyPropertyChanged("ModelChecked");
            }
        }

        //检测模型是否存在
        private bool _IsExsitModel;
        public bool IsExsitModel
        {
            get
            {
                return _IsExsitModel;
            }
            set
            {
                _IsExsitModel = value;
                NotifyPropertyChanged("IsExsitModel");
            }
        }





        private bool _IsSqlServer;
        public bool IsSqlServer
        {
            get
            {
                return _IsSqlServer;
            }
            set
            {
                _IsSqlServer = value;
                NotifyPropertyChanged("IsSqlServer");
            }

        }

        private string _Json;
        public string Json
        {
            get
            {
                return _Json;
            }
            set
            {
                _Json = value;
                NotifyPropertyChanged("Json");
            }

        }

        private string _TableName;
        public string TableName
        {
            get
            {
                return _TableName;
            }
            set
            {
                _TableName = value;
                NotifyPropertyChanged("TableName");
            }
        }

        private bool _MySql = true;
        public bool MySql
        {
            get
            {
                return _MySql;
            }
            set
            {
                _MySql = value;
                NotifyPropertyChanged("MySql");
            }
        }
        #endregion

        #region Command

        public SimpleCommand CmdOpen => new SimpleCommand()
        {
            ExecuteDelegate = o =>
            {
                switch (Filter.value)
                {
                    case 0: SqlText = OpenFile(); break;
                    case 1: OpenNotifyFile(); break;
                    case 2: Common.Export(Common.SetConfig("DbName").ToUpper() + ".cs", SqlText); break;
                    case 3: AddNotify_Click(); break;
                    case 4: FormatJson(); break;
                    case 5: AddCommand(); break;
                    default: GenerateModel(); break;
                }
            }
        };




        #endregion

        #region 方法



        private string OpenFile()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Excel (*.XLSX)|*.xlsx|all file|*.*";
            open.ShowDialog();
            if (true)
            {
                try
                {
                    XSSFWorkbook xss;
                    using (FileStream fs = new FileStream(open.FileName, FileMode.Open, FileAccess.Read))
                    {
                        xss = new XSSFWorkbook(fs);
                    }

                    List<excel> sFCs = new List<excel>();
                    XSSFSheet sheet = (XSSFSheet)xss.GetSheetAt(0);

                    //if (sheet.GetRow(0).Cells.Count != 7)
                    //{
                    //    //Invaliable
                    //    MessageBox.Show(Translator.Get("InvaliableColume"));
                    //    return;
                    //}
                    string[] url = open.FileName.Split('\\');

                    string[] name = url.Last().Split(' ');
                    string str = "CREATE TABLE " + name[0] + "(";
                    int cot = sheet.LastRowNum;
                    for (int i = 1; i <= cot; i++)
                    {
                        excel sFC = new excel();
                        sFC.name = sheet.GetRow(i).GetCell(0) == null ? "" : sheet.GetRow(i).GetCell(0).ToString();
                        if (sFC.name.Length == 0 || sFC.name == "" || sFC.name == null)
                        {

                            cot -= 1;
                            str = str.Remove(str.Length - 3, 2);

                            continue;
                        }
                        str += (sFC.name + " ");
                        sFC.desc = sheet.GetRow(i).GetCell(1) == null ? "" : sheet.GetRow(i).GetCell(1).ToString();
                        sFC.type = sheet.GetRow(i).GetCell(2) == null ? "" : sheet.GetRow(i).GetCell(2).ToString();
                        str += (sFC.type + " ");
                        sFC.isNull = sheet.GetRow(i).GetCell(3) == null ? "" : sheet.GetRow(i).GetCell(3).ToString();

                        sFC.defaultContext = sheet.GetRow(i).GetCell(4) == null ? "" : sheet.GetRow(i).GetCell(4).ToString();
                        if (sFC.isNull.Length != 0)
                        {
                            str += ("NOT NULL ");
                        }
                        else
                        {
                            str += (" NULL ");
                        }
                        if (sFC.defaultContext.Length != 0)
                        {
                            str += ("DEFAULT " + sFC.defaultContext + " ");
                        }
                        if (sFC.name == "ID")
                            str += "primary key ";
                        if (Constraint.value != 2)
                            str += ("COMMENT '" + sFC.desc + "' ");
                        if (i != cot) str += " ,\n";

                    }
                    str += ")";

                    return str;
                }
                catch (Exception ex)
                {
                    Warning warning = new Warning(ex.Message);
                    warning.ShowDialog();
                }
            }
            return "";
        }

        string SorName;
        private void OpenNotifyFile()
        {
            try
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "all file|*.*";
                open.ShowDialog();
                StreamReader file, f;
                string str;
                ArrayList use;
                using (FileStream fs = new FileStream(open.FileName, FileMode.Open, FileAccess.Read))
                {
                    SorName = open.FileName;
                    use = new ArrayList();
                    file = new StreamReader(open.FileName, Encoding.Default);
                    f = new StreamReader(fs, Encoding.Default);
                    str = file.ReadToEnd();

                    string line;
                    while ((line = f.ReadLine()) != null)
                    {
                        if (line.Contains("using")) use.Add(line);
                        if (line.Contains("namespace")) use.Add(line + "{");
                        if (line.Contains("class"))
                        {
                            use.Add(line);
                            break;
                        }
                    }
                }
                ArrayList arr = new ArrayList(str.Split(' '));
                ArrayList marxs = new ArrayList();


                //get using 

                for (int i = 0; i < arr.Count; i++)
                {
                    if (Common.IsType(arr[i].ToString()))
                    {
                        marxs.Add(arr[i + 1]);
                    }
                }

                string FileContent = "";
                foreach (string s in use)
                {
                    FileContent += s;
                }
                if (FileContent.Length != 0) FileContent += "{";
                foreach (var t in marxs)
                {
                    string pri = "private string _" + t.ToString() + ";";
                    string pub = "public string " + t.ToString() + "{ get{return _" + t.ToString() + ";} set{_" + t.ToString() + " = value; NotifyPropertyChanged(\"" + t.ToString() + "\");}}";
                    FileContent += pri + pub;
                }

                FileContent = Common.format(FileContent);
                SqlText = FileContent;
            }
            catch (Exception ex)
            {
                Warning warning = new Warning(ex.Message);
                warning.ShowDialog();
            }

        }

        private void AddNotify_Click()
        {
            if (String.IsNullOrEmpty(SqlText))
                return;
            List<string> arrs = new List<string>(SqlText.Split(' '));

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
            SqlText = "/// <summary>\r\n///\r\n/// </summary>\r\n" + Xu.Common.Common.format(sr + '}');
        }



        private void FormatJson()
        {
            if (String.IsNullOrEmpty(SqlText))
                return;
            string source = SqlText;
            ArrayList arr = new ArrayList();
            Stack<char> stack = new Stack<char>();
            foreach (var c in source)
                stack.Push(c);

            while (stack.Count > 0)
            {
                if (stack.Peek() == ':')
                {
                    stack.Pop();
                    if (stack.Peek() == '"')
                    {
                        stack.Pop();
                        string str = string.Empty;
                        while (stack.Peek() != '"')
                        {
                            str += stack.Pop();
                        }
                        stack.Pop();
                        var marx = str.ToCharArray();
                        Array.Reverse(marx);
                        str = "";
                        foreach (var t in marx)
                        {
                            str += t;
                        }
                        if (!arr.Contains(str))
                        {
                            if (Constraint.value == 1)
                            {
                                if (Common.HasLower(str))
                                    arr.Add(str);
                            }
                            else
                            {
                                arr.Add(str);
                            }
                        }
                    }
                }
                else stack.Pop();
            }
            arr.Sort(new SortByLength());

            ArrayList models = new ArrayList();
            SqlText = "";
            SqlText = "namespace json\n{\n\tpublic class models\n\t{\n";
            foreach (var marx in arr)
            {
                string model = "";
                model += "\t\tpublic string " + marx + " {get;set;}";
                SqlText += model + "\n";
                models.Add(model);
            }
            SqlText += "\t}\n}";
        }

        private void AddCommand()
        {
            if (String.IsNullOrEmpty(SqlText))
            {
                return;
            }
            StringBuilder str = new StringBuilder("");
            str.Append("public SimpleCommand " + SqlText.Replace("\n", "") + " => new SimpleCommand(){ExecuteDelegate = x =>{},CanExecuteDelegate = o =>{return true;}};");
            SqlText = Common.format(str.ToString());
        }

        private async void Run()
        {
            await Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Loading loading = new Loading();
                    loading.Show();
                });
            }).ConfigureAwait(true);
        }

        private async void ModelRun()
        {
            await Task.Run(() =>
            {
                if (!string.IsNullOrEmpty(DBName))
                {
                    ModelHelper model = new ModelHelper();
                    //DbType type = DbType.SqlServer;
                    SqlSugar.DbType ty = SqlSugar.DbType.SqlServer;
                    if (Constraint.value == 3)
                    {
                        //type = DbType.MySql;
                        ty = SqlSugar.DbType.MySql;
                    }
                    else if (Constraint.value == 4)
                    {
                        //type = DbType.MySql;
                        ty = SqlSugar.DbType.Oracle;
                    }
                    string json = model.GetTableJson(DBName, ty);
                    if (IsSugar)
                        SqlText = Common.format(model.ModelCreate(json, "MeiCloud.DataAccess", "Sugar"));
                    else
                        SqlText = Common.format(model.ModelCreate(json, "MeiCloud.DataAccess"));

                    SqlText = SqlText.Replace("\nusing Creative.ODA;", "");
                }
            }).ConfigureAwait(true);
        }


        private /*async*/ void GenerateModel()
        {
            //
            try
            {
                Run();

                ModelRun();
                
            }
            catch (Exception ex)
            {
                Warning warning = new Warning(ex.Message);
                warning.ShowDialog();
            }

            //App.Current.Dispatcher.Invoke(() =>
            //{
            //    loading.Close();
            //});

        }

        private string GenerateModel(string DBName)
        {
            try
            {
                if (!String.IsNullOrEmpty(DBName))
                {
                    ModelHelper model = new ModelHelper();
                    //DbType type = DbType.SqlServer;
                    SqlSugar.DbType ty = SqlSugar.DbType.SqlServer;
                    if (Constraint.value == 3)
                    {
                        //type = DbType.MySql;
                        ty = SqlSugar.DbType.MySql;
                    }
                    else if (Constraint.value == 4)
                    {
                        //type = DbType.MySql;
                        ty = SqlSugar.DbType.Oracle;
                    }
                    string json = model.GetTableJson(DBName, ty);
                    return Common.format(model.ModelCreate(json, "MeiCloud.DataAccess"));
                }
                return "";
            }
            catch (Exception ex)
            {
                Warning warning = new Warning(ex.Message);
                warning.ShowDialog();
                return "";
            }
        }

        /// <summary>
        /// 加载下拉框
        /// </summary>
        private async void LoadCombox()
        {
            await Task.Run(() =>
            {
                DBName = Common.SetConfig("DBName");
                ListButton = new List<Key_Value>();
                ListButton.Add(new Key_Value { label = "生成SQL", value = 0 });
                ListButton.Add(new Key_Value { label = "打开MODEL", value = 1 });
                ListButton.Add(new Key_Value { label = "导出", value = 2 });
                ListButton.Add(new Key_Value { label = "添加监视", value = 3 });
                ListButton.Add(new Key_Value { label = "格式化JSON", value = 4 });
                ListButton.Add(new Key_Value { label = "添加Command", value = 5 });
                ListButton.Add(new Key_Value { label = "生成模型", value = 6 });
                Filter = ListButton[3];

                ////
                Constraints = new List<Key_Value>();
                Constraints.Add(new Key_Value { label = "", value = 0 });
                Constraints.Add(new Key_Value { label = "去掉小写", value = 1 });
                Constraints.Add(new Key_Value { label = "SqlServer", value = 2 });
                Constraints.Add(new Key_Value { label = "MySql", value = 3 });
                Constraints.Add(new Key_Value { label = "Oracle", value = 4 });
                Constraint = Constraints[0];


                ////
                CreateBox = new List<Key_Value>();
                CreateBox.Add(new Key_Value { label = "生成弹出框", value = 0 });
                FilterBox = CreateBox[0];
            }).ConfigureAwait(true);
        }
        #endregion

        #region 生成界面部分

        //项目名
        private string _ProjectName;
        public string ProjectName
        {
            get
            {
                return _ProjectName;
            }
            set
            {
                _ProjectName = value;
                NotifyPropertyChanged("ProjectName");
            }
        }

        private string _ChineseName;
        public string ChineseName
        {
            get
            {
                return _ChineseName;
            }
            set
            {
                _ChineseName = value;
                NotifyPropertyChanged("ChineseName");
            }
        }



        //表格字段
        private string _DataGridHeader;
        public string DataGridHeader
        {
            get
            {
                return _DataGridHeader;
            }
            set
            {
                _DataGridHeader = value;
                NotifyPropertyChanged("DataGridHeader");
            }
        }

        /// <summary>
        /// 弹出框数量
        /// </summary>
        private int _BoxNumber;
        public int BoxNumber
        {
            get
            {
                return _BoxNumber;
            }
            set
            {
                _BoxNumber = value;
                NotifyPropertyChanged("BoxNumber");
            }
        }
        #region 勾选框
        private bool _AddChecked;
        public bool AddChecked
        {
            get
            {
                return _AddChecked;
            }
            set
            {
                _AddChecked = value;
                NotifyPropertyChanged("AddChecked");
            }
        }

        private bool _EditChecked;
        public bool EditChecked
        {
            get
            {
                return _EditChecked;
            }
            set
            {
                _EditChecked = value;
                NotifyPropertyChanged("EditChecked");
            }
        }




        /// <summary>
        /// 表名
        /// </summary>
        private string _Tables;
        public string Tables
        {
            get
            {
                return _Tables;
            }
            set
            {
                _Tables = value;
                NotifyPropertyChanged("Tables");
            }
        }


        private bool _ImportChecked;
        public bool ImportChecked
        {
            get
            {
                return _ImportChecked;
            }
            set
            {
                _ImportChecked = value;
                NotifyPropertyChanged("ImportChecked");
            }
        }
        #endregion

        #region Command
        [Obsolete]
        public SimpleCommand CmdCreate => new SimpleCommand()
        {
            ExecuteDelegate = x =>
            {
                switch (FilterBox.value)
                {
                    case 0: OpenExcel(BoxNumber); break;
                    default: break;
                }
            },
            CanExecuteDelegate = o =>
            {
                return true;
            }
        };

        public SimpleCommand CmdGenerate => new SimpleCommand()
        {
            ExecuteDelegate = x =>
            {
                //Loading loading = new Loading();
                try
                {
                    OpenExcel(BoxNumber);
                    //loading.Show();
                    if (CreateProj(ProjectName, "还没写"))
                    {
                        Warning warning = new Warning("生成成功！");
                        warning.ShowDialog();
                    }
                    else
                    {
                        Warning warning = new Warning("生成失败！");
                        warning.ShowDialog();
                    }
                    //Json = Strings.LoadJson("a.txt");
                    //Json = Strings.GetCsproj("a", "b");
                }
                catch (Exception ex)
                {
                    Warning warning = new Warning(ex.Message);
                    warning.ShowDialog();
                }
                //loading.Close();
            }
        };

        #endregion



        /// <summary>
        /// 
        /// </summary>
        /// <param name="csproj">项目名称</param>
        /// <param name="Model">模型</param>
        /// <param name="Extend">扩展内容</param>
        /// <returns></returns>
        private bool CreateProj(string csproj, string Model, string Extend = "")
        {
            try
            {
                #region
                if (String.IsNullOrEmpty(ProjectName))
                    return false;
                string dir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                Directory.CreateDirectory(dir + "\\" + csproj);
                Directory.CreateDirectory(dir + "\\" + csproj + "\\Models");
                Directory.CreateDirectory(dir + "\\" + csproj + "\\Properties");
                Directory.CreateDirectory(dir + "\\" + csproj + "\\Resources");
                Directory.CreateDirectory(dir + "\\" + csproj + "\\ViewModels");
                Directory.CreateDirectory(dir + "\\" + csproj + "\\Views");



                Strings.Write(Strings.GetAssembly(ProjectName), dir + "\\" + csproj + "\\Properties\\AssemblyInfo.cs");
                //Write(Strings.resx, dir + "\\" + csproj + "\\Properties\\Resources.resx");
                Strings.Write(CreateClass.LoadModel(IndexBodies, ProjectName), dir + "\\" + csproj + "\\Models\\Model.cs");
                Strings.Write(CreateClass.LoadModel(ProjectName), dir + "\\" + csproj + "\\Models\\ComboxModel.cs");
                Strings.Write(Strings.GetIndexPage(csproj, Buttons.CreateButton(Buttones), Controls.CreateContents(IndexContents), Controls.CreateDataGrid(IndexBodies)), dir + "\\" + csproj + "\\Views\\IndexPage.xaml");//indexpage.xaml
                Strings.Write(Strings.GetIndexXamlCs(csproj), dir + "\\" + csproj + "\\Views\\IndexPage.xaml.cs");//xaml.cs
                Strings.Write(Strings.GetIndexVM(csproj, Controls.CreateWord(IndexContents) + CreateCommand(IndexContents, Buttones, boxes), Strings.CreateLoadData(IndexContents)), dir + "\\" + csproj + "\\ViewModels\\IndexPageVM.cs");//indexVM

                string res = Strings.CreateGetallUrl(csproj, IndexContents, 1) + Strings.CreateDeleteUrl(csproj) + Strings.CreateUpdateUrl(csproj);
                string Include = "";
                string Complie = "";
                //Add
                if (AddChecked)
                {
                    Strings.Write(Strings.GetAddPageXaml(csproj, Controls.CreateAddContents(AddContents), "auto"), dir + "\\" + csproj + "\\Views\\Add.xaml");//Add.xaml
                    Strings.Write(Strings.GetAddPageXamlCs(csproj), dir + "\\" + csproj + "\\Views\\Add.xaml.cs");//xaml.cs
                    Strings.Write(Strings.GetAddVM(csproj, Controls.CreateWord(AddContents) + CreateCommand(AddContents, new List<MyModel>() { }), Strings.PostData(AddContents), Strings.IsLegal(AddContents)), dir + "\\" + csproj + "\\ViewModels\\AddVM.cs");//AddVM
                    res += Strings.CreateAddUrl(csproj);
                    Include += GetAddInclude;
                    Complie += GetAddComplie;
                }

                if (EditChecked)
                {
                    Strings.Write(Strings.GetEditPageXaml(csproj, Controls.CreateAddContents(AddContents), "auto"), dir + "\\" + csproj + "\\Views\\Edit.xaml");//Edit.xaml
                    Strings.Write(Strings.GetEditPageXamlCs(csproj), dir + "\\" + csproj + "\\Views\\Edit.xaml.cs");//xaml.cs
                    Strings.Write(Strings.GetEditVM(csproj, Controls.CreateWord(AddContents) + CreateCommand(AddContents, new List<MyModel>() { }), Strings.PostData(AddContents), Strings.IsLegal(AddContents), Strings.CreateEditLoadData(AddContents)), dir + "\\" + csproj + "\\ViewModels\\EditVM.cs");//AddVM
                    res += Strings.CreateEditUrl(csproj);
                    Include += GetEditInclude;
                    Complie += GetEditComplie;
                }

                //Import
                if (ImportChecked)
                {
                    Strings.Write(Strings.GetImportXaml(csproj, Controls.CreateDataGrid(ImportBidies)), dir + "\\" + csproj + "\\Views\\ImportPage.xaml");//Import.xaml
                    Strings.Write(Strings.GetImportXamlCs(csproj), dir + "\\" + csproj + "\\Views\\ImportPage.xaml.cs");//xaml.cs
                    Strings.Write(Strings.GetImprotVM(csproj, ImportBidies, Import.CreateXss(ImportBidies), Import.CreateNull(ImportBidies), Import.CreateRepeat(ImportBidies), Import.CreateRepeatFunction(ImportBidies), Import.CheckImportData(ImportBidies)), dir + "\\" + csproj + "\\ViewModels\\ImportPageVM.cs");//ImportVM
                    res += Import.CreateImportUrl(csproj, ImportBidies);
                    Include += GetImportInclude;
                    Complie += GetImportComplie;
                }

                if (boxes.Count > 0)
                {
                    foreach (var ds in boxes)
                    {
                        Strings.Write(Strings.GetBoxesXaml(csproj, ds.BOX_CODE, Controls.CreateDataGrid(ds.Body), ds.SEARCH_CODE), dir + "\\" + csproj + "\\Views\\" + ds.BOX_CODE + ".xaml");//Add.xaml
                        Strings.Write(Strings.GetBoxesXamlCs(csproj, ds.BOX_CODE), dir + "\\" + csproj + "\\Views\\" + ds.BOX_CODE + ".xaml.cs");//xaml.cs
                        Strings.Write(Strings.GetBoxesVM(csproj, ds.BOX_CODE, Controls.CreateWord(ds.Body), Strings.CreateBoxUrl(csproj, ds.BOX_CODE, ds.SEARCH_CODE)), dir + "\\" + csproj + "\\ViewModels\\" + ds.BOX_CODE + "VM.cs");//AddVM
                    }
                    Include += GetInclude(boxes);
                    Complie += GetComplie(boxes);
                }

                Strings.Write(Strings.GetResource(ChineseName, Resources.CreateResorce(Resources.AllRes(IndexContents, IndexBodies, boxes))), dir + "\\" + csproj + "\\Resources\\Strings.zh-CN.xaml");//资源文件
                Strings.Write(Strings.GetCsproj(csproj, Complie, Include), dir + "\\" + csproj + "\\" + csproj + ".csproj");


                Strings.Write(Strings.GetServices(csproj, res), dir + "\\" + csproj + "\\Services.cs");

                Domain = Strings.CreateGetallUrl(csproj, IndexContents).Replace("\r\n\t\t", "");
                #endregion

                #region 生成后台
                Directory.CreateDirectory(dir + "\\" + ServerName);
                Directory.CreateDirectory(dir + "\\" + ServerName + "\\Models");
                Directory.CreateDirectory(dir + "\\" + ServerName + "\\Properties");
                Directory.CreateDirectory(dir + "\\" + ServerName + "\\Domains");
                Directory.CreateDirectory(dir + "\\" + ServerName + "\\Services");

                var ls = ServerName.Split('.');
                Strings.Write(Domains.GetAssembly(ServerName, ChineseName), dir + "\\" + ServerName + "\\Properties\\AssemblyInfo.cs");
                Strings.Write(Domains.GetCsproj(ServerName, LoadTables()), dir + "\\" + ServerName + "\\" + ServerName + ".csproj");
                Strings.Write(Domains.GetDomain(ServerName, Domains.GetAllUrlBody(IndexContents, 1), Domains.GetHasWordUrl(ImportBidies, 1), Domains.GetHasWrodFunction(ImportBidies), Domains.GetAllFunction(Tables, IndexContents)), dir + "\\" + ServerName + "\\Domains\\" + ls.Last() + "Domain.cs");
                Strings.Write(Domains.GetService(ServerName, Domains.GetAllUrlHeader(ServerName, IndexContents), Domains.GetAllUrlBody(IndexContents, 1), Domains.GetAllUrlBody(IndexContents, 0), ChineseName, Domains.GetHasWordUrl(ImportBidies, 0), Domains.GetHasWordUrl(ImportBidies, 1), Domains.GetHasWordUrl(ImportBidies, 2)), dir + "\\" + ServerName + "\\Services\\" + ls.Last() + "Service.cs");
                if (ModelChecked)
                    CreateDbModel();
                #endregion
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }




        #region 保存表格数据
        List<Excel> IndexHeaders;
        List<string> Buttones;
        List<Excel> IndexContents;
        List<Excel> IndexBodies;

        List<Excel> AddContents;

        //导入
        ImportClass ImportBidies;

        //弹出框
        List<MyModel> boxes;

        #endregion

        private void OpenExcel(int BoxNum)
        {
            OpenFileDialog open = new OpenFileDialog
            {
                Filter = "Excel (*.XLSX)|*.xlsx|all file|*.*"
            };
            open.ShowDialog();
            try
            {
                XSSFWorkbook xss;
                using (FileStream fs = new FileStream(open.FileName, FileMode.Open, FileAccess.Read))
                {
                    xss = new XSSFWorkbook(fs);
                }

                boxes = new List<MyModel>();

                IndexHeaders = new List<Excel>();
                IndexContents = new List<Excel>();
                IndexBodies = new List<Excel>();
                Buttones = new List<string>();

                AddContents = new List<Excel>();

                ImportBidies = new ImportClass();
                for (int t = 0; t < BoxNum + 4; t++)
                {
                    if (t == 0)
                    {
                        XSSFSheet sheet = (XSSFSheet)xss.GetSheetAt(t);

                        int cot = sheet.LastRowNum;
                        //按钮
                        for (int i = 1; i <= cot; i++)
                        {
                            string btn = sheet.GetRow(i).GetCell(6) == null ? "" : sheet.GetRow(i).GetCell(6).ToString();
                            if (String.IsNullOrEmpty(btn))
                                break;
                            Buttones.Add(btn);
                        }

                        //控件、查询字段
                        for (int i = 1; i <= cot; i++)
                        {
                            Excel sFC = new Excel();
                            sFC.CONTROL_CODE = sheet.GetRow(i).GetCell(2) == null ? "" : sheet.GetRow(i).GetCell(2).ToString();
                            sFC.SEARCH_CODE = sheet.GetRow(i).GetCell(3) == null ? "" : sheet.GetRow(i).GetCell(3).ToString();
                            sFC.SEARCH_NAME = sheet.GetRow(i).GetCell(4) == null ? "" : sheet.GetRow(i).GetCell(4).ToString();
                            string api = sheet.GetRow(i).GetCell(5) == null ? "" : sheet.GetRow(i).GetCell(5).ToString();
                            sFC.IsApi = api == "YES" ? true : false;
                            if (string.IsNullOrEmpty(sFC.CONTROL_CODE))
                                break;
                            IndexContents.Add(sFC);
                        }
                        //表格
                        for (int i = 1; i <= cot; i++)
                        {
                            Excel sFC = new Excel();
                            sFC.GRID_CODE = sheet.GetRow(i).GetCell(0) == null ? "" : sheet.GetRow(i).GetCell(0).ToString();
                            sFC.GRID_NAME = sheet.GetRow(i).GetCell(1) == null ? "" : sheet.GetRow(i).GetCell(1).ToString();
                            if (String.IsNullOrEmpty(sFC.GRID_CODE) || string.IsNullOrEmpty(sFC.GRID_NAME))
                                break;
                            IndexBodies.Add(sFC);
                        }
                    }
                    else if (t == 1 && AddChecked)
                    {
                        List<excel> sFCs = new List<excel>();
                        XSSFSheet sheet = (XSSFSheet)xss.GetSheetAt(t);

                        int cot = sheet.LastRowNum;
                        //控件、查询字段
                        for (int i = 1; i <= cot; i++)
                        {
                            Excel sFC = new Excel();
                            sFC.CONTROL_CODE = sheet.GetRow(i).GetCell(0) == null ? "" : sheet.GetRow(i).GetCell(0).ToString();
                            sFC.SEARCH_CODE = sheet.GetRow(i).GetCell(1) == null ? "" : sheet.GetRow(i).GetCell(1).ToString();
                            sFC.SEARCH_NAME = sheet.GetRow(i).GetCell(2) == null ? "" : sheet.GetRow(i).GetCell(2).ToString();

                            if (string.IsNullOrEmpty(sFC.CONTROL_CODE))
                                break;
                            AddContents.Add(sFC);
                        }
                    }
                    else if (t == 3 && ImportChecked)
                    {
                        XSSFSheet sheet = (XSSFSheet)xss.GetSheetAt(t);

                        int cot = sheet.LastRowNum;
                        for (int i = 1; i <= cot; i++)
                        {
                            Im im = new Im();
                            im.GRID_CODE = sheet.GetRow(i).GetCell(0) == null ? "" : sheet.GetRow(i).GetCell(0).ToString();
                            im.GRID_NAME = sheet.GetRow(i).GetCell(1) == null ? "" : sheet.GetRow(i).GetCell(1).ToString();
                            if (string.IsNullOrEmpty(im.GRID_CODE) || string.IsNullOrEmpty(im.GRID_NAME))
                                break;
                            ImportBidies.Body.Add(im);
                        }

                        for (int i = 1; i <= cot; i++)
                        {
                            string EMPTY_CODE = sheet.GetRow(i).GetCell(2) == null ? "" : sheet.GetRow(i).GetCell(2).ToString();
                            if (string.IsNullOrEmpty(EMPTY_CODE))
                                break;
                            ImportBidies.EMPTY_CODE.Add(EMPTY_CODE);
                        }

                        for (int i = 1; i <= cot; i++)
                        {
                            string REPEAT_CODE = sheet.GetRow(i).GetCell(3) == null ? "" : sheet.GetRow(i).GetCell(3).ToString();
                            if (string.IsNullOrEmpty(REPEAT_CODE))
                                break;
                            ImportBidies.REPEAT_CODE.Add(REPEAT_CODE);
                        }
                    }
                    else if (t > 3)
                    {
                        XSSFSheet sheet = (XSSFSheet)xss.GetSheetAt(t);

                        int cot = sheet.LastRowNum;
                        MyModel my = new MyModel();
                        my.BOX_NAME = sheet.GetRow(1).GetCell(0) == null ? "" : sheet.GetRow(1).GetCell(0).ToString();
                        my.BOX_CODE = sheet.GetRow(1).GetCell(1) == null ? "" : sheet.GetRow(1).GetCell(1).ToString();
                        my.SEARCH_CODE = sheet.GetRow(1).GetCell(4) == null ? "" : sheet.GetRow(1).GetCell(4).ToString();
                        for (int i = 1; i <= cot; i++)
                        {
                            Excel excel = new Excel();
                            excel.GRID_CODE = sheet.GetRow(i).GetCell(2) == null ? "" : sheet.GetRow(i).GetCell(2).ToString();
                            excel.GRID_NAME = sheet.GetRow(i).GetCell(3) == null ? "" : sheet.GetRow(i).GetCell(3).ToString();
                            if (String.IsNullOrEmpty(excel.GRID_CODE) || String.IsNullOrEmpty(excel.GRID_NAME))
                                break;
                            my.Body.Add(excel);
                        }
                        boxes.Add(my);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }






        /// <summary>
        /// 生成Command
        /// </summary>
        /// <returns></returns>
        private string CreateCommand(List<Excel> Contents, List<string> btn, List<MyModel> models)
        {
            string s = "#region Command\r\n\t";
            int t = 0;
            Model.Helper.ModelHelper model = new ModelHelper();
            for (int i = 0; i < Contents.Count; i++)
            {
                if (Contents[i].CONTROL_CODE == "TextBox带弹出框")
                {
                    if (models.Count > 0 && i < models.Count)
                        s += "\t" + Controls.AddCommand(model.GetCmd(Contents[i].SEARCH_CODE), ProjectName, models[t++], Contents[i]) + "\r\n";
                    else
                        s += "\t" + Controls.AddCommand(model.GetCmd(Contents[i].SEARCH_CODE), ProjectName, null, Contents[i]) + "\r\n";
                }
            }

            foreach (var ds in btn)
            {
                s += "\t" + Controls.AddCommand(model.GetCmd(Buttons.command(ds, "")), ProjectName) + "\r\n";
            }
            return s + "#endregion\r\n";
        }

        /// <summary>
        /// 生成新增编辑的Command
        /// </summary>
        /// <returns></returns>
        private string CreateCommand(List<Excel> Contents, List<MyModel> models)
        {
            string s = "#region Command\r\n\t";
            int t = 0;
            Model.Helper.ModelHelper model = new ModelHelper();
            for (int i = 0; i < Contents.Count; i++)
            {
                if (Contents[i].CONTROL_CODE == "TextBox带弹出框")
                {
                    if (models.Count > 0)
                        s += "\t" + Controls.AddCommand(model.GetCmd(Contents[i].SEARCH_CODE), ProjectName, models[t++], Contents[i]) + "\r\n";
                    else
                        s += "\t" + Controls.AddCommand(model.GetCmd(Contents[i].SEARCH_CODE), ProjectName, null, Contents[i]) + "\r\n";
                }
            }

            return s + "#endregion\r\n";
        }


        /// <summary>
        /// 生成项目文件内容
        /// </summary>
        /// <param name="BoxName"></param>
        /// <returns></returns>
        private string GetComplie(List<MyModel> BoxName)
        {
            string s = "";
            foreach (var ds in BoxName)
            {
                s += "<Compile Include=\"Views\\" + ds.BOX_CODE + ".xaml.cs\">\r\n" +
                        "<DependentUpon>" + ds.BOX_CODE + ".xaml</DependentUpon>\r\n" +
                        "<SubType>Code</SubType>" +
                        "</Compile>\r\n" +
                        "<Compile Include=\"ViewModels\\" + ds.BOX_CODE + "VM.cs\" />\r\n";
            }

            return s;
        }

        private string GetInclude(List<MyModel> BoxName)
        {
            string s = "";
            foreach (var ds in BoxName)
            {
                s += "<Page Include=\"Views\\" + ds.BOX_CODE + ".xaml\">\r\n" +
                "<SubType>Designer</SubType>\r\n" +
                "<Generator>MSBuild:Compile</Generator>\r\n" +
                "</Page>\r\n";
            }
            return s;
        }


        #region 导入
        /// <summary>
        /// 生成项目文件内容
        /// </summary>
        /// <param name="BoxName"></param>
        /// <returns></returns>
        private const string GetImportComplie =
            "<Compile Include=\"Views\\ImportPage.xaml.cs\">\r\n" +
            "<DependentUpon>ImportPage.xaml</DependentUpon>\r\n" +
            "<SubType>Code</SubType>" +
            "</Compile>\r\n" +
            "<Compile Include=\"ViewModels\\ImportPageVM.cs\" />\r\n";


        private const string GetImportInclude =
            "<Page Include=\"Views\\ImportPage.xaml\">\r\n" +
            "<SubType>Designer</SubType>\r\n" +
            "<Generator>MSBuild:Compile</Generator>\r\n" +
            "</Page>\r\n";
        #endregion

        #region 新增
        #endregion
        /// <summary>
        /// 生成项目文件内容
        /// </summary>
        /// <param name="BoxName"></param>
        /// <returns></returns>
        private const string GetAddComplie =
            "<Compile Include=\"Views\\Add.xaml.cs\">\r\n" +
            "<DependentUpon>Add.xaml</DependentUpon>\r\n" +
            "<SubType>Code</SubType>" +
            "</Compile>\r\n" +
            "<Compile Include=\"ViewModels\\AddVM.cs\" />\r\n";


        private const string GetAddInclude =
            "<Page Include=\"Views\\Add.xaml\">\r\n" +
            "<SubType>Designer</SubType>\r\n" +
            "<Generator>MSBuild:Compile</Generator>\r\n" +
            "</Page>\r\n";

        private const string GetEditComplie =
            "<Compile Include=\"Views\\Edit.xaml.cs\">\r\n" +
            "<DependentUpon>Edit.xaml</DependentUpon>\r\n" +
            "<SubType>Code</SubType>" +
            "</Compile>\r\n" +
            "<Compile Include=\"ViewModels\\EditVM.cs\" />\r\n";


        private const string GetEditInclude =
            "<Page Include=\"Views\\Edit.xaml\">\r\n" +
            "<SubType>Designer</SubType>\r\n" +
            "<Generator>MSBuild:Compile</Generator>\r\n" +
            "</Page>\r\n";

        /// <summary>
        /// 添加模型链接
        /// </summary>
        /// <returns></returns>
        private string LoadTables()
        {
            if (String.IsNullOrEmpty(Tables))
                return "";
            else
            {
                var ds = Tables.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                return Strings.ModelLink(ShareModel, ds.ToList(), IsExsitModel);
            }
        }
        #endregion

        #region  Domain

        private string _Domain;
        public string Domain
        {
            get
            {
                return _Domain;
            }
            set
            {
                _Domain = value;
                NotifyPropertyChanged("Domain");
            }
        }

        private string _ShareModel;
        public string ShareModel
        {
            get
            {
                return _ShareModel;
            }
            set
            {
                _ShareModel = value;
                Common.SetConfig("ShareModel", value);
                NotifyPropertyChanged("ShareModel");
            }
        }




        #endregion

        /// <summary>
        /// 生成没有的模型
        /// </summary>
        private void CreateDbModel()
        {
            var tables = Tables.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string s = "已生成(Models文件夹)：\r\n";
            foreach (var ds in tables)
            {
                if (!File.Exists(ShareModel + "\\" + ds.ToUpper() + ".cs"))
                {
                    Strings.Write(GenerateModel(ds), dir + "\\" + ServerName + "\\Models\\" + ds.ToUpper() + ".cs");
                    s += ds.ToUpper() + "\r\n";
                }
            }
        }
    }
}
