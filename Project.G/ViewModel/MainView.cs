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
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Xu.Common;

namespace Project.G.ViewModel
{
    class MainView : ValidationBase
    {
        public List<Key_Value> ListButton { get; set; }
        public List<Key_Value> Constraints { get; set; }
        public MainView()
        {
            SqlString = Common.SetConfig("SqlString");
            TableName = Common.SetConfig("DBName");
            ListButton = new List<Key_Value>();
            ListButton.Add(new Key_Value { label = "生成SQL", value = 0 });
            ListButton.Add(new Key_Value { label = "打开MODEL", value = 1 });
            ListButton.Add(new Key_Value { label = "导出", value = 2 });
            ListButton.Add(new Key_Value { label = "添加监视", value = 3 });
            ListButton.Add(new Key_Value { label = "格式化JSON", value = 4 });
            Filter = ListButton[3];

            ////
            Constraints = new List<Key_Value>();
            Constraints.Add(new Key_Value { label = "", value = 0 });
            Constraints.Add(new Key_Value { label = "去掉小写", value = 1 });
            Constraints.Add(new Key_Value { label = "SqlServer", value = 2 });
            Constraint = Constraints[0];
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

        private string _SqlString;
        public string SqlString
        {
            get
            {
                return _SqlString;
            }
            set
            {
                _SqlString = value;
                Common.SetConfig("SqlString", value);
                NotifyPropertyChanged("SqlString");
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
                Common.SetConfig("DBName", value);
                NotifyPropertyChanged("TableName");
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

        #region Command

        public SimpleCommand CmdOpen => new SimpleCommand()
        {
            ExecuteDelegate = o =>
            {
                if(Filter.value == 0)
                {
                    string str = OpenFile(TableName);
                    SqlText = str;
                }
                else if(Filter.value == 1)
                {
                    OpenNotifyFile();
                }
                else if(Filter.value == 2)
                {
                    Common.Export(SorName = "models.cs", SqlText);
                }
                else if(Filter.value == 3)
                {
                    AddNotify_Click();
                }
                else
                {
                    FormatJson();
                }
            }
        };

        #endregion

        #region 方法
        private string OpenFile(string name)
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

                    string str = "CREATE TABLE " + name + "(";
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
                }
            }
            return "";
        }

        int cot;
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
                cot = 0;
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

            }

        }

        private void AddNotify_Click()
        {
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
            SqlText = Xu.Common.Common.format(sr);
        }

        private void FormatJson()
        {
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
        #endregion
    }
}
