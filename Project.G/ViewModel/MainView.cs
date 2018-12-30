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

namespace Project.G.ViewModel
{
    class MainView : ValidationBase
    {
        
        public MainView()
        {
            LoadCombox();
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
                    case 0: SqlText = OpenFile();break;
                    case 1: OpenNotifyFile();break;
                    case 2: Common.Export(Common.SetConfig("DbName").ToUpper() + ".cs", SqlText); break;
                    case 3: AddNotify_Click();break;
                    case 4: FormatJson();break;
                    case 5: AddCommand();break;
                    default: GenerateModel();break;
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
                        if(Constraint.value != 2)
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
            SqlText = Xu.Common.Common.format(sr + '}');
        }

        private string AddNotify_Click(string s)
        {
            List<string> arrs = new List<string>(s.Split(' '));

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
            return Xu.Common.Common.format(sr + '}');
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
            str.Append("public SimpleCommand "+ SqlText.Replace("\n", "") + " => new SimpleCommand(){ExecuteDelegate = x =>{},CanExecuteDelegate = o =>{return true;}};");
            SqlText = Common.format(str.ToString());
        }


        /// <summary>
        /// 生成command
        /// </summary>
        /// <param name="s">名字，Cmdxxx形式</param>
        /// <param name="model">弹出框的数据，为了获取弹出框名 model.BOX_CODE</param>
        /// <param name="excel">获取IndexPage的查询字段</param>
        /// <returns></returns>
        private string AddCommand(string s, MyModel model = null, Excel excel = null)
        {
            StringBuilder str = new StringBuilder("");
            if(s == "CmdExport")
            {
                str.Append("//导出\r\n");
                str.Append("public SimpleCommand " + s.Replace("\n", "") + " => new SimpleCommand(){ExecuteDelegate = x =>{" +
                    "ExcelHelper.Export<Model>(DataSource, \""+ChineseName+"\");" +
                    "},CanExecuteDelegate = o =>{return true;}};");
            }
            else if(s == "CmdImport")
            {
                str.Append("//导入\r\n");
                str.Append("public SimpleCommand " + s.Replace("\n", "") + " => new SimpleCommand(){ExecuteDelegate = x =>{" +
                    "plugin.Framework.OpenWindowPlugin(\""+ProjectName+".dll;"+ProjectName+".ImportPage\",\"\");" +
                    "},CanExecuteDelegate = o =>{return true;}};");
            }
            else if(s == "CmdDelete")
            {
                str.Append("//删除选中的数据\r\n");
                str.Append("public SimpleCommand " + s.Replace("\n", "") + " => new SimpleCommand(){ExecuteDelegate = x =>{" +
                    "var list = DataSource.Where(e => e.IsChecked).Select(e => e.ID).ToList();" +
                    "plugin.Framework.PostData(Services.url_delete, new" +
                    "{" +
                    "ids = list" +
                    "});" +
                    "},CanExecuteDelegate = o =>{return DataSource != null && DataSource.Where(e => e.IsChecked).Count() > 0;}};");
            }
            else if(s == "CmdAdd")
            {
                str.Append("//新增\r\n");
                str.Append("public SimpleCommand " + s.Replace("\n", "") + " => new SimpleCommand(){ExecuteDelegate = x =>{" +
                    "plugin.Framework.OpenWindowPlugin(\"" + ProjectName + ".dll;" + ProjectName + ".Add\",\"\");" +
                    "},CanExecuteDelegate = o =>{return true;}};");
            }
            else if(s == "CmdRefresh")
            {
                str.Append("//刷新\r\n");
                str.Append("public SimpleCommand " + s.Replace("\n", "") + " => new SimpleCommand(){ExecuteDelegate = x =>{LoadData();},CanExecuteDelegate = o =>{return true;}};");
            }
            else
            {
                string ss = "public SimpleCommand " + s.Replace("\n", "") + " => new SimpleCommand(){ExecuteDelegate = x =>{";
                if(model != null)
                {
                    ss += "var res = plugin.Framework.OpenWindowPlugin(\"" + ProjectName + ".dll;" + ProjectName + "." + model.BOX_CODE + "\",\"\");" +
                    "if(res.success){" +
                    excel.SEARCH_CODE + " = res.data." + Reback(excel.SEARCH_CODE) + ";" +
                    "//" + excel.SEARCH_CODE.Replace("Desc", "Code") + " = res.data." + Reback(excel.SEARCH_CODE).Replace("DESC", "CODE") + ";\r\n" +
                    "}";
                }
                    ss += "},CanExecuteDelegate = o =>{return true;}};";
                str.Append(ss);
            }
            
            return Common.format(str.ToString());
        }

        private string Reback(string s)
        {
            string str = s[0].ToString();
            for(int i = 1; i < s.Length - 1; i++)
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

        private void GenerateModel()
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
                    string json = model.GetTableJson(DBName, ty);
                    SqlText = Common.format(model.ModelCreate(json, "MeiCloud.DataAccess"));
                }
            }
            catch (Exception ex)
            {
                Warning warning = new Warning(ex.Message);
                warning.ShowDialog();
            }
        }

        /// <summary>
        /// 加载下拉框
        /// </summary>
        private void LoadCombox()
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
            Constraint = Constraints[0];


            ////
            CreateBox = new List<Key_Value>();
            CreateBox.Add(new Key_Value { label = "生成弹出框", value = 0 });
            FilterBox = CreateBox[0];
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
        public SimpleCommand CmdCreate => new SimpleCommand()
        {
            ExecuteDelegate = x => {
                switch (FilterBox.value)
                {
                    case 0: OpenExcel(BoxNumber); break;
                    default: break;
                }
            },
            CanExecuteDelegate = o => {
                return true;
            }
        };

        public SimpleCommand CmdGenerate => new SimpleCommand()
        {
            ExecuteDelegate = x =>
            {
                try
                {
                    OpenExcel(BoxNumber);

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
                if (String.IsNullOrEmpty(ProjectName))
                    return false;
                string dir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                Directory.CreateDirectory(dir + "\\" + csproj);
                Directory.CreateDirectory(dir + "\\" + csproj + "\\Models");
                Directory.CreateDirectory(dir + "\\" + csproj + "\\Properties");
                Directory.CreateDirectory(dir + "\\" + csproj + "\\Resources");
                Directory.CreateDirectory(dir + "\\" + csproj + "\\ViewModels");
                Directory.CreateDirectory(dir + "\\" + csproj + "\\Views");



                Write(Strings.GetAssembly(ProjectName), dir + "\\" + csproj + "\\Properties\\AssemblyInfo.cs");
                //Write(Strings.resx, dir + "\\" + csproj + "\\Properties\\Resources.resx");
                Write(LoadModel(IndexBodies), dir + "\\" + csproj + "\\Models\\Model.cs");
                Write(Strings.GetIndexPage(csproj, CreateButton(Buttones), CreateContents(IndexContents), CreateDataGrid(IndexBodies)), dir + "\\" + csproj + "\\Views\\IndexPage.xaml");//indexpage.xaml
                Write(Strings.GetIndexXamlCs(csproj), dir + "\\" + csproj + "\\Views\\IndexPage.xaml.cs");//xaml.cs
                Write(Strings.GetIndexVM(csproj, CreateWord(IndexContents) + CreateCommand(IndexContents, Buttones, boxes), Strings.CreateLoadData(IndexContents)), dir + "\\" + csproj + "\\ViewModels\\IndexPageVM.cs");//indexVM

                string res = Strings.CreateGetallUrl(csproj, IndexContents, 1) + Strings.CreateDeleteUrl(csproj) + Strings.CreateUpdateUrl(csproj);
                string Include = "";
                string Complie = "";
                //Add
                if (AddChecked)
                {
                    Write(Strings.GetAddPageXaml(csproj, CreateAddContents(AddContents), "auto"), dir + "\\" + csproj + "\\Views\\Add.xaml");//Add.xaml
                    Write(Strings.GetAddPageXamlCs(csproj), dir + "\\" + csproj + "\\Views\\Add.xaml.cs");//xaml.cs
                    Write(Strings.GetAddVM(csproj, CreateWord(AddContents), Strings.PostData(AddContents), Strings.IsLegal(AddContents)), dir + "\\" + csproj + "\\ViewModels\\AddVM.cs");//AddVM
                    res += Strings.CreateAddUrl(csproj);
                    Include += GetInclude(boxes);
                    Complie += GetComplie(boxes);
                }

                //Import
                if (ImportChecked)
                {
                    Write(Strings.GetImportXaml(csproj, CreateDataGrid(ImportBidies)), dir + "\\" + csproj + "\\Views\\ImportPage.xaml");//Import.xaml
                    Write(Strings.GetImportXamlCs(csproj), dir + "\\" + csproj + "\\Views\\ImportPage.xaml.cs");//xaml.cs
                    Write(Strings.GetImprotVM(csproj, ImportBidies, Strings.CreateXss(ImportBidies), Strings.CreateNull(ImportBidies), Strings.CreateRepeat(ImportBidies), Strings.CreateRepeatFunction(ImportBidies)), dir + "\\" + csproj + "\\ViewModels\\ImportPageVM.cs");//ImportVM
                    res += Strings.CreateImportUrl(csproj, ImportBidies);
                    Include += GetImportInclude;
                    Complie += GetImportComplie;
                }

                if(boxes.Count > 0)
                {
                    foreach(var ds in boxes)
                    {
                        Write(Strings.GetBoxesXaml(csproj, ds.BOX_CODE, CreateDataGrid(ds.Body), ds.SEARCH_CODE), dir + "\\" + csproj + "\\Views\\"+ds.BOX_CODE+".xaml");//Add.xaml
                        Write(Strings.GetBoxesXamlCs(csproj, ds.BOX_CODE), dir + "\\" + csproj + "\\Views\\" + ds.BOX_CODE + ".xaml.cs");//xaml.cs
                        Write(Strings.GetBoxesVM(csproj, ds.BOX_CODE, CreateWord(ds.Body), Strings.CreateBoxUrl(csproj, ds.BOX_CODE, ds.SEARCH_CODE)), dir + "\\" + csproj + "\\ViewModels\\" + ds.BOX_CODE + "VM.cs");//AddVM
                    }
                }

                Write(Strings.GetResource(ChineseName, CreateResorce(AllRes())), dir + "\\" + csproj + "\\Resources\\Strings.zh-CN.xaml");//资源文件
                Write(Strings.GetCsproj(csproj, Complie, Include), dir + "\\" + csproj + "\\" + csproj + ".csproj");


                Write(Strings.GetServices(csproj, res), dir + "\\" + csproj + "\\Services.cs");
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void Write(string str, string path)
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            //开始写入
            sw.Write(str);
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();
        }

        /// <summary>
        /// 生成模型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string LoadModel(List<Excel> lists)
        {
            string s = "using DAF.Plugin.Common;using System;using System.Collections.Generic;using System.Linq;using System.Text;namespace "+ProjectName+ "{public class Model : ValidationBase{";
            s += "public string ID {get;set;}";
            s += "public string Color {get;set;}";
            s += "public string TextState {get; set;}";
            foreach (var marx in lists)
            {
                string model = "";
                model += "//[Excel(Width =5000, Title =\"" + marx.GRID_NAME + "\")]\r\n";
                model += "public string " + marx.GRID_CODE.ToUpper() + " {get;set;}";
                s += model;
            }
            s += "private bool _IsChecked;public bool IsChecked{get{return _IsChecked;}set{_IsChecked = value;NotifyPropertyChanged(\"IsChecked\");}}}}";
            return Common.format(s);
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

        private bool OpenExcel(int BoxNum)
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
                            string btn = sheet.GetRow(i).GetCell(5) == null ? "" : sheet.GetRow(i).GetCell(5).ToString();
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
                    else if(t == 1 && AddChecked)
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
                    else if(t == 3 && ImportChecked)
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
                            string EMPTY_CODE= sheet.GetRow(i).GetCell(2) == null ? "" : sheet.GetRow(i).GetCell(2).ToString();
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
                    else if(t > 3)
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
                return true;
            }
            catch (Exception ex)
            {
                Warning warning = new Warning(ex.Message);
                warning.ShowDialog();
                return false;
            }

            
        }

        private string CreateButton(List<string> btn)
        {
            string s = "<WrapPanel>\r\n";
            foreach(var ds in btn)
            {
                string tmp = "<Button Content=\"{DynamicResource " + command(ds, "") + "}\" " +
                    "Margin=\"{DynamicResource BtnMargin}\" " +
                    "Command=\"{Binding " + command(ds, "Cmd") + "}\" " +
                    "controls:TextBoxHelper.ButtonContent=\"{DynamicResource " + command(ds, "Icon_") + "}\"/>\r\n";
                s += tmp;
            }
            s += "</WrapPanel>";
            return s;
        }

        private string command(string s, string pre)
        {
            switch (s)
            {
                case "刷新": return pre + "Refresh";
                case "打印": return pre + "Print";
                case "导出": return pre + "Export";
                case "导入": return pre + "Import";
                case "生成": return pre + "Create";
                case "编辑": return pre + "Edit";
                case "新增": return pre + "Add";
                case "删除": return pre + "Delete";
                case "保存": return pre + "Save";
                case "禁用": return pre + "Forbiden";
                default:return "";
            }
        }

        /// <summary>
        /// 生成表格
        /// </summary>
        /// <returns></returns>
        private string CreateDataGrid(List<Excel> Bodies)
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

            foreach(var ds in Bodies)
            {
                string tmp = "<DataGridTemplateColumn Width=\"150\" HeaderStyle=\"{DynamicResource DataGridColumnHeader_Center}\" CellStyle=\"{DynamicResource DataGridCell_Center}\">\r\n" +
                            "<DataGridTemplateColumn.Header>\r\n" +
                                "<TextBlock Text=\"{DynamicResource Grid_"+ ds.GRID_CODE +"}\"/>\r\n" +
                            "</DataGridTemplateColumn.Header>\r\n" +
                            "<DataGridTemplateColumn.CellTemplate>\r\n" +
                                "<DataTemplate>\r\n" +
                                    "<TextBlock Text=\"{Binding "+ds.GRID_CODE+"}\"/>\r\n" +
                                "</DataTemplate>\r\n" +
                            "</DataGridTemplateColumn.CellTemplate>\r\n" +
                        "</DataGridTemplateColumn>\r\n";
                s += tmp;
            }
            return s;
        }

        /// <summary>
        /// 生成表格
        /// </summary>
        /// <returns></returns>
        private string CreateDataGrid(ImportClass Bodies)
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
        /// 资源文件拓展
        /// </summary>
        /// <returns></returns>
        private string CreateResorce(List<Excel> Contents)
        {
            string s = "";
            List<string> str = new List<string>();
            List<string> mark = new List<string>();
            foreach (var ds in Contents)
            {
                if (str.Contains(ds.SEARCH_CODE))
                    continue;
                string tmp = "";
                if (!String.IsNullOrEmpty(ds.SEARCH_CODE) && !String.IsNullOrEmpty(ds.SEARCH_NAME))
                    tmp += "<sys:String x:Key=\"" + ds.SEARCH_CODE + "_Watermark\">请输入" + ds.SEARCH_NAME + "</sys:String>\r\n";
                s += tmp;
                str.Add(ds.SEARCH_CODE);
            }


            foreach (var ds in Contents)
            {
                if (mark.Contains(ds.SEARCH_CODE))
                    continue;
                string tmp = "";
                if (!String.IsNullOrEmpty(ds.SEARCH_CODE) && !String.IsNullOrEmpty(ds.SEARCH_NAME))
                    tmp += "<sys:String x:Key=\"Grid_" + ds.SEARCH_CODE + "\">" + ds.SEARCH_NAME + "</sys:String>\r\n";
                s += tmp;
                mark.Add(ds.SEARCH_CODE);
            }
            return s;
        }

        private List<Excel> AllRes()
        {
            List<Excel> excels = new List<Excel>();
            foreach(var ds in IndexContents)
            {
                Excel ex = new Excel();
                ex.SEARCH_CODE = ds.SEARCH_CODE;
                ex.SEARCH_NAME = ds.SEARCH_NAME;
                excels.Add(ex);
            }

            foreach(var ds in IndexBodies)
            {
                Excel ex = new Excel();
                ex.SEARCH_CODE = ds.GRID_CODE;
                ex.SEARCH_NAME = ds.GRID_NAME;
                excels.Add(ex);
            }

            foreach(var ds in boxes)
            {
                foreach(var ls in ds.Content)
                {
                    Excel ex = new Excel();
                    ex.SEARCH_CODE = ls.GRID_CODE;
                    ex.SEARCH_NAME = ls.GRID_NAME;
                    excels.Add(ex);
                }
            }
            return excels;
        }

        /// <summary>
        /// 生成TextBox代码
        /// </summary>
        /// <returns></returns>
        private string CreateTextBox(string Binding)
        {
            string s = "\r\n<TextBox Text=\"{Binding "+Binding+", Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}\" " +
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
        private string CreateTextBoxWithCommand(string Binding)
        {
            Model.Helper.ModelHelper model = new ModelHelper();
            
            string s = "<TextBox Text=\"{Binding "+Binding+", Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}\" " +
                "Margin=\"0,0,50,0\" " +
                "controls:TextBoxHelper.ButtonCommand=\"{Binding "+model.GetCmd(Binding)+"}\" " +
                "controls:TextBoxHelper.ButtonContent=\"2\" " +
                "controls:TextBoxHelper.ClearTextButton=\"True\" " +
                "controls:TextBoxHelper.Watermark=\"{DynamicResource "+Binding+ "_Watermark}\" Style=\"{DynamicResource MetroButtonTextBox}\"/>\r\n";
            return s;
        }

        /// <summary>
        /// 生成下拉框
        /// </summary>
        /// <param name="DataSource">下拉框的英文名字</param>
        /// <returns></returns>
        private string CreateCombox(string DataSource)
        {
            Model.Helper.ModelHelper model = new ModelHelper();
            string s = "<ComboBox Margin=\"0,0,50,0\" " +
                "controls:TextBoxHelper.Watermark=\"{DynamicResource Grid_"+DataSource+"}\" " +
                "ItemsSource=\"{Binding Com"+model.Col(DataSource).Replace("Col", "")+"}\" " +
                "SelectedItem=\"{Binding Filter"+ model.Col(DataSource).Replace("Col", "") + "}\"/>\r\n";
            return s;
        }

        /// <summary>
        /// 生成日期控件
        /// </summary>
        /// <param name="Binding">绑定的值</param>
        /// <returns></returns>
        private string CreateDatePicker(string Binding)
        {
            string s = "<DatePicker Text=\"{Binding "+Binding+", Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}\" Margin=\"0,0,50,0\"/>\r\n";
            return s;
        }

        private string CreateTextBlock(string name)
        {
            string s = "<TextBlock Text=\"{DynamicResource Grid_" + name + "}\" Margin=\"0,0,5,0\" VerticalAlignment=\"Center\" Width=\"60\"/>\r\n";
            return s;
        }

        //占位
        private string EmptyControl()
        {
            string s = "<TextBox Text=\"{Binding ABC, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}\" Margin=\"0,0,50,0\" Foreground=\"Transparent\" BorderThickness=\"0\" Background=\"Transparent\" IsEnabled=\"False\"/>\r\n";
            return s;
        }

        /// <summary>
        /// 生成查询条件
        /// </summary>
        /// <returns></returns>
        private string CreateContents(List<Excel> Contents)
        {
            string s = "";
            int t = Contents.Count % 3 == 0 ? Contents.Count / 3 : (Contents.Count / 3) + 1;
            for (int i = 0; i < t; i ++)
            {
                s += "<WrapPanel Grid.Row=\"" + i + "\" Margin=\"0,0,0,10\">\r\n";
                for (int j = i * 3; j < Contents.Count && j < (i + 1) * 3; j++)
                {
                    s += CreateTextBlock(Contents[j].SEARCH_CODE);
                    switch (Contents[j].CONTROL_CODE)
                    {
                        case "TextBox": s += CreateTextBox(Contents[j].SEARCH_CODE); break;
                        case "TextBox带弹出框": s += CreateTextBoxWithCommand(Contents[j].SEARCH_CODE); break;
                        case "Combox": s += CreateCombox(Contents[j].SEARCH_CODE); break;
                        case "DatePicker": s += CreateDatePicker(Contents[j].SEARCH_CODE); break;
                        case "占位控件": s += EmptyControl(); break;
                        default: break;
                    }
                    
                }
                s += "\r\n</WrapPanel>";
            }
            return s;
        }

        /// <summary>
        /// 新增的界面控件
        /// </summary>
        /// <param name="Contents"></param>
        /// <returns></returns>
        private string CreateAddContents(List<Excel> Contents)
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
                    s += "\t" + AddCommand(model.GetCmd(Contents[i].SEARCH_CODE), models[t++], Contents[i]) + "\r\n";
                }
            }

            foreach(var ds in btn)
            {
                s += "\t" + AddCommand(model.GetCmd(command(ds, ""))) + "\r\n";
            }
            return s + "#endregion\r\n";
        }


        /// <summary>
        /// 生成绑定字段
        /// </summary>
        /// <returns></returns>
        private string CreateWord(List<Excel> Contents)
        {
            Model.Helper.ModelHelper model = new ModelHelper();
            string s = "#region 字段\r\n\t";
            foreach(var ds in Contents)
            {
                if(ds.CONTROL_CODE == "TextBox" || ds.CONTROL_CODE == "TextBox带弹出框" || ds.CONTROL_CODE == "DatePicker")
                {
                    s += "\t//" + ds.SEARCH_NAME + "\r\n\t";
                    s += AddNotify_Click(ds.SEARCH_CODE) + "\r\n";
                }

                if(ds.CONTROL_CODE == "Combox")
                {
                    s += "\t//" + ds.SEARCH_NAME + "\r\n\t";
                    s += AddNotify_Click("List<Model> " + ds.SEARCH_CODE) + "\r\n";
                    s += AddNotify_Click("Model " + "Filter_" + ds.SEARCH_CODE) + "\r\n";
                }
            }
            s += "#endregion\r\n";
            return s;
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

        #endregion
    }
}
