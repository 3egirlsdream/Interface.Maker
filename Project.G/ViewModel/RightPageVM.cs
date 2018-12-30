using Newtonsoft.Json;
using Project.G.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Xu.Common;

namespace Project.G.ViewModel
{
    class RightPageVM : ValidationBase
    {
        public List<Key_Value> DbList { get; set; }
        public RightPageVM()
        {
            SqlString = Common.SetConfig("SqlString");
            DbList = new List<Key_Value>();
            DbList.Add(new Key_Value { label = "SqlServer", value = 0 });
            DbList.Add(new Key_Value { label = "MySql", value = 1 });
            DbList.Add(new Key_Value { label = "Oracle", value = 3 });
            FilterDb = DbList[0];
            LoadJson();
            Item = new ObservableCollection<string>();
        }

        #region

        private List<Connection> _HeaderMain;
        public List<Connection> HeaderMain
        {
            get
            {
                return _HeaderMain;
            }
            set
            {
                _HeaderMain = value;
                NotifyPropertyChanged("HeaderMain");
            }
        }




        private List<Connection> _Header;
        public List<Connection> Header
        {
            get
            {
                return _Header;
            }
            set
            {
                _Header = value;
                NotifyPropertyChanged("Header");
                HeaderMain = Header.Where((x, i) => Header.FindIndex(e => e.Header == x.Header) == i).ToList();
                if (FilterHeader != null && FilterHeader.Header != null)
                    LoadItem(FilterHeader.Header);
            }
        }


        //不知道为什么用普通的List数据不会刷新
        private ObservableCollection<string> _Item;
        public ObservableCollection<string> Item 
        {
            get
            {
                return _Item;
            }
            set
            {
                _Item = value;
                NotifyPropertyChanged("Item");
            }
        }

        private Connection _FilterHeader;
        public Connection FilterHeader
        {
            get
            {
                return _FilterHeader;
            }
            set
            {
                _FilterHeader = value;
                if (FilterHeader != null && FilterHeader.Header != null)
                    LoadItem(FilterHeader.Header);
                NotifyPropertyChanged("FilterHeader");
            }
        }

        private string _FilterItem;
        public string FilterItem
        {
            get
            {
                return _FilterItem;
            }
            set
            {
                _FilterItem = value;
                NotifyPropertyChanged("FilterItem");
            }
        }

        private string _ConnStr;
        public string ConnStr
        {
            get
            {
                return _ConnStr;
            }
            set
            {
                _ConnStr = value;
                NotifyPropertyChanged("ConnStr");
            }
        }

        




        private Key_Value _FilterDb;
        public Key_Value FilterDb
        {
            get
            {
                return _FilterDb;
            }
            set
            {
                _FilterDb = value;
                NotifyPropertyChanged("FilterDb");
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
        #endregion

        #region 配置连接条件

        private string _HeaderText;
        public string HeaderText
        {
            get
            {
                return _HeaderText;
            }
            set
            {
                _HeaderText = value;
                NotifyPropertyChanged("HeaderText");
            }
        }

        private string _ItemText;
        public string ItemText
        {
            get
            {
                return _ItemText;
            }
            set
            {
                _ItemText = value;
                NotifyPropertyChanged("ItemText");
            }
        }

        private string _SrchConnStr;
        public string SrchConnStr
        {
            get
            {
                return _SrchConnStr;
            }
            set
            {
                _SrchConnStr = value;
                NotifyPropertyChanged("SrchConnStr");
            }
        }

        #endregion

        /// <summary>
        /// 读取Json
        /// </summary>
        public void LoadJson()
        {
            try
            {
                FileStream file = new FileStream("ConnectionConditions.json", FileMode.Open);
                StreamReader sr = new StreamReader(file, Encoding.Default);
                string json = sr.ReadToEnd();
                Header = JsonConvert.DeserializeObject<List<Connection>>(json);
                //MessageBox.Show(json);
                file.Close();
            }
            catch (IOException e)
            {
                //Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// 将数据转化为Json保存
        /// </summary>
        public void SaveJson()
        {
            FileStream fs = new FileStream("ConnectionConditions.json", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            //开始写入
            sw.Write(JsonConvert.SerializeObject(Header));
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();
        }

        /// <summary>
        /// 根据条件筛选第二下拉框内容
        /// </summary>
        /// <param name="str">条件</param>
        public void LoadItem(string str)
        {
            Item.Clear();
            foreach (var ds in Header)
            {
                if (ds.Header == str)
                    Item.Add(ds.Items);
            }
        }



        public SimpleCommand CmdLuRu => new SimpleCommand()
        {
             ExecuteDelegate = x =>
             {
                 bool flag = true;
                 if (Header == null || Header.Count == 0)//第一次插入的时候
                 {
                     Connection c = new Connection(HeaderText.ToUpper(), ItemText.ToUpper(), ConnStr.ToUpper());
                     Header.Add(c);
                     SaveJson();
                     LoadJson();
                 }
                 else
                 {
                     foreach (var ds in Header)
                     {
                         if (HeaderText != null && ItemText != null && ds.Header == HeaderText.ToUpper() && ds.Items == ItemText.ToUpper())
                         {
                             ConnStr = ConnStr == null ? ConnStr : ConnStr.ToUpper();
                             ds.Add(ConnStr);
                             SaveJson();
                             LoadJson();
                             flag = false;
                             break;
                         }
                     }
                     if (flag && !String.IsNullOrEmpty(HeaderText) && !String.IsNullOrEmpty(ItemText) && !String.IsNullOrEmpty(ConnStr))
                     {

                         Connection c = new Connection(HeaderText.ToUpper(), ItemText.ToUpper(), ConnStr.ToUpper());
                         Header.Add(c);
                         SaveJson();
                         LoadJson();
                     }
                 }
             },
             CanExecuteDelegate = x =>
             {
                 return !String.IsNullOrEmpty(ConnStr);
             }

        };

        public SimpleCommand CmdSearch => new SimpleCommand()
        {
            ExecuteDelegate = x =>
            {
                SrchConnStr = "";
                foreach(var ds in Header)
                {
                    if(ds.Header == FilterHeader.Header && ds.Items == FilterItem)
                    {
                        foreach(var s in ds.ConnectionConditions)
                            SrchConnStr += s + "\n";
                    }
                }
            },
            CanExecuteDelegate = o =>
            {
                return true;
            }
        };




        
    }
}
