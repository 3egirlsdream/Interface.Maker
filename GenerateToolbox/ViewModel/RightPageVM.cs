using Newtonsoft.Json;
using Project.G.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Xu.Common;

namespace Project.G.ViewModel
{
    class RightPageVM : ValidationBase
    {
        public List<Key_Value> DbList { get; set; }
        RightPage plugin;
        public RightPageVM(RightPage plugin)
        {
            SqlString = Common.SetConfig("SqlString");
            DbList = new List<Key_Value>();
            DbList.Add(new Key_Value { label = "SqlServer", value = 0 });
            DbList.Add(new Key_Value { label = "MySql", value = 1 });
            DbList.Add(new Key_Value { label = "Oracle", value = 3 });
            FilterDb = DbList[0];
            LoadJson();
            Item = new ObservableCollection<string>();
            this.plugin = plugin;
            SourceUrl = Common.SetConfig("SourceUrl");
            AimUrl = Common.SetConfig("AimUrl");
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

        /// <summary>
        /// 源文件夹
        /// </summary>
        private string _SourceUrl;
        public string SourceUrl
        {
            get
            {
                return _SourceUrl;
            }
            set
            {
                _SourceUrl = value;
                Common.SetConfig("SourceUrl", SourceUrl);
                NotifyPropertyChanged("SourceUrl");
            }
        }


        /// <summary>
        /// 目标文件夹
        /// </summary>
        private string _AimUrl;
        public string AimUrl
        {
            get
            {
                return _AimUrl;
            }
            set
            {
                _AimUrl = value;
                Common.SetConfig("AimUrl", AimUrl);
                NotifyPropertyChanged("AimUrl");
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
        public async void LoadJson()
        {
            await Task.Run(() =>
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
                    throw e;
                    //Console.WriteLine(e.ToString());
                }
            });
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

        public SimpleCommand CmdStart => new SimpleCommand()
        {
            ExecuteDelegate = x => {
                if (plugin.cmdStart.Content.ToString() == "Running")
                    return;
                plugin.cmdStart.Content = "Running";
                new Task(() =>
                {
                    while (true)
                    {
                        CopyDir(SourceUrl, AimUrl);
                        Thread.Sleep(30000);
                    }
                    
                }).Start();
                
            },
            CanExecuteDelegate = o => {
                return true;
            }
        };

        static bool flag = false;
        static Dictionary<string, DateTime> dic = new Dictionary<string, DateTime>();
        private static void CopyDir(string srcPath, string aimPath)
        {
            try
            {
                string[] fileList = System.IO.Directory.GetFileSystemEntries(srcPath);
                //初始化文件夹
                if (!flag)
                {
                    foreach (string file in fileList)
                    {

                        // 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                        if (System.IO.Directory.Exists(file))
                        {
                            if (aimPath[aimPath.Length - 1] != System.IO.Path.DirectorySeparatorChar)
                            {
                                aimPath += System.IO.Path.DirectorySeparatorChar;
                            }
                            CopyDir(file, aimPath + System.IO.Path.GetFileName(file));
                        }
                        // 否则直接Copy文件
                        else
                        {
                            FileInfo info = new FileInfo(file);
                            dic[info.Name] = info.LastWriteTime;
                            //System.IO.File.Copy(file, aimPath + System.IO.Path.GetFileName(file), true);
                        }
                    }
                    flag = true;
                }
                // if(File)
                // 检查目标目录是否以目录分割字符结束如果不是则添加
                if (aimPath[aimPath.Length - 1] != System.IO.Path.DirectorySeparatorChar)
                {
                    aimPath += System.IO.Path.DirectorySeparatorChar;
                }
                // 判断目标目录是否存在如果不存在则新建
                if (!System.IO.Directory.Exists(aimPath))
                {
                    System.IO.Directory.CreateDirectory(aimPath);
                }
                // 得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组
                // 如果你指向copy目标文件下面的文件而不包含目录请使用下面的方法
                // string[] fileList = Directory.GetFiles（srcPath）；
                //string[] fileList = System.IO.Directory.GetFileSystemEntries(srcPath);
                // 遍历所有的文件和目录
                foreach (string file in fileList)
                {
                    
                    // 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                    if (System.IO.Directory.Exists(file))
                    {
                        CopyDir(file, aimPath + System.IO.Path.GetFileName(file));
                    }
                    // 否则直接Copy文件
                    else
                    {
                        FileInfo info = new FileInfo(file);
                        if (!dic.ContainsKey(info.Name))
                        {
                            System.IO.File.Copy(file, aimPath + System.IO.Path.GetFileName(file), true);
                            dic[info.Name] = info.LastWriteTime;
                        }
                        else if (dic.ContainsKey(info.Name) && dic[info.Name] != info.LastWriteTime)
                        {
                            System.IO.File.Copy(file, aimPath + System.IO.Path.GetFileName(file), true);
                            dic[info.Name] = info.LastWriteTime;
                        }
                        else
                        {
                            dic[info.Name] = info.LastWriteTime;
                        }

                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
