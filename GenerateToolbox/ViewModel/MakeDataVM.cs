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
using GenerateToolbox.Models;
using ControlzEx.Native;
using System.Windows.Controls;
using System.Data;
using Newtonsoft.Json;

namespace GenerateToolbox.ViewModel
{
    class MakeDataVM : ValidationBase
    {
        #region construct

        MakeData.MakeData plugin;
        static int start_seq = 0;
        static string name { get; set; } = "";
        public MakeDataVM(MakeData.MakeData Iplugin)
        {
            plugin = Iplugin;
            start_seq = GenerateUniqueRandom(1, 10, 1)[0];
        }

        #endregion


        #region properties

        private List<TABLE_COLUMNS> dataSource;
        public List<TABLE_COLUMNS> DataSource
        {
            get
            {
                return dataSource;
            }
            set
            {
                dataSource = value;
                NotifyPropertyChanged(nameof(DataSource));
            }
        }

        #endregion


        #region public function

        public void GetAllUserTable(string tableName)
        {
            try
            {
                using (var db = SugarContext.OracleContext)
                {
                    var table = db.SqlQueryable<dynamic>($"SELECT TABLE_NAME FROM USER_TABLES WHERE TABLE_NAME LIKE '%{tableName}%'").ToList();
                    plugin.wrappanel.Children.Clear();
                    foreach (var ds in table)
                    {
                        var name = (string)(ds.TABLE_NAME);
                        var tname = string.Join("", name.Where(x => char.IsLetter(x) || x == '_').ToArray());
                        //var stackpanel = new StackPanel();
                        //stackpanel.Name = tname + "_S";
                        var cb = new CheckBox();
                        cb.Name = tname + "_cb";
                        cb.Content = tname;
                        //cb.IsChecked = false;
                        //stackpanel.Children.Add(cb);

                        plugin.wrappanel.Children.Add(cb);
                    }

                }
            }
            catch(Exception ex)
            {
                Warning.ShowMsg(ex.Message);
            }
        }

        
        public void GetDataTable()
        {
            try
            {
                foreach (var p in plugin.wrappanel.Children)
                {
                    var pp = (p as CheckBox);
                    if (pp != null && pp.IsChecked == true)
                    {
                        name = pp.Name.Replace("_cb", "");
                        break;
                    }
                }
                if (string.IsNullOrEmpty(name)) return;
                var sql = $@"SELECT T.COLUMN_NAME, JT.DATA_TYPE, t.COMMENTS,T.OWNER, t.TABLE_NAME  FROM ALL_COL_COMMENTS T 
LEFT JOIN USER_TAB_COLUMNS JT ON t.TABLE_NAME = JT.TABLE_NAME and t.COLUMN_NAME = JT.COLUMN_NAME
WHERE  T.TABLE_NAME='{name}' ORDER BY nvl(length(trim(T.COLUMN_NAME)),0)";
                using (var db = SugarContext.OracleContext)
                {
                    var table = db.SqlQueryable<TABLE_COLUMNS>(sql).ToList();
                    DataSource = table;
                }
            }
            catch(Exception ex)
            {
                Warning.ShowMsg(ex.Message);
            }
        }

        public void Insert(int t)
        {
            using(var db = SugarContext.OracleContext)
            {
                db.Ado.BeginTran();
                try
                {

                    while (t-- > 0)
                    {
                        var sql = GetInsertSql();
                        db.Ado.ExecuteCommand(sql);
                    }
                    db.Ado.CommitTran();
                    Warning.ShowMsg("Create Success!");
                }
                catch (Exception ex)
                {
                    db.Ado.RollbackTran();
                    Warning.ShowMsg(ex.Message);
                }
                
            }
        }

        #endregion


        #region private function

        private string GetInsertSql()
        {
            if (string.IsNullOrEmpty(name)) return "";
            var sql = $"INSERT INTO {name.ToUpper()} (" + string.Join(",", DataSource.Select(x=>x.COLUMN_NAME)) + ") VALUES(";
            foreach(var item in DataSource)
            {
                var temp = "";

                if(item.DATA_TYPE == "DATE")
                {
                    if(item.SelectedItem == "DateTime.Now") temp += $@"TO_DATE('{DateTime.Now.ToString()}', 'yyyy-mm-dd hh24:mi:ss')";
                    else
                    {
                        DateTime date = new DateTime();
                        if(item.START_TIME != null)
                        {
                            date = item.START_TIME.Value.AddMinutes(100*start_seq++);
                        }
                        if(item.END_TIME != null)
                        {
                            if (date > item.END_TIME) date = item.END_TIME.Value;
                        }
                        if (item.START_TIME is null && item.END_TIME is null)
                            date = DateTime.Now;
                        temp += $@"TO_DATE('{date.ToString()}', 'yyyy-mm-dd hh24:mi:ss')";
                    }
                }
                else if(item.DATA_TYPE == "NUMBER")
                {
                    if (item.SelectedItem == "CONST") temp += $@"{item.CONST_STRING}";
                    else temp += $@"{10*start_seq++}";
                }
                else if(item.DATA_TYPE == "VARCHAR2")
                {
                    if (item.SelectedItem == "GUID") temp += $@"'{Guid.NewGuid().ToString("N").ToUpper()}'";
                    else if (item.SelectedItem == "CONST") temp += $@"'{item.CONST_STRING}'";
                    else temp += $@"'{GetRandomString()}'";
                }
                else if(item.DATA_TYPE == "CHAR")
                {
                    if (item.SelectedItem == "CONST") temp += $@"'{item.CONST_STRING}'";
                    else temp += $@"'A'";
                }

                if (dataSource.IndexOf(item) != dataSource.Count - 1) temp += ",";

                sql += temp;
                
            }
            return sql + ")";
        }


        private string GetRandomString()
        {
            char s = 'A';
            var array = GenerateUniqueRandom(1, 20, 4);
            var res = new char[4];
            for(int i = 0; i < array.Length; i++)
            {
                var c = (int)s + array[i] + start_seq++;
                c %= (int)'z';
                if (c < (int)'A') c = (int)'A';
                res[i] = (char)(c);
            }
            return string.Join("", res) + GetRandom(100);
        }

        /// n 为生成随机数个数
        private int[] GenerateUniqueRandom(int minValue, int maxValue, int n)
        {
            //如果生成随机数个数大于指定范围的数字总数，则最多只生成该范围内数字总数个随机数
            if (n > maxValue - minValue + 1)
                n = maxValue - minValue + 1;

            int maxIndex = maxValue - minValue + 2;// 索引数组上限
            int[] indexArr = new int[maxIndex];
            for (int i = 0; i < maxIndex; i++)
            {
                indexArr[i] = minValue - 1;
                minValue++;
            }

            Random ran = new Random();
            int[] randNum = new int[n];
            int index;
            for (int j = 0; j < n; j++)
            {
                index = ran.Next(1, maxIndex - 1);// 生成一个随机数作为索引

                //根据索引从索引数组中取一个数保存到随机数数组
                randNum[j] = indexArr[index];

                // 用索引数组中最后一个数取代已被选作随机数的数
                indexArr[index] = indexArr[maxIndex - 1];
                maxIndex--; //索引上限减 1
            }
            return randNum;
        }


        private int GetRandom(int range)
        {
            var arr = GenerateUniqueRandom(5, range, 1);
            Random rd = new Random();
            return rd.Next(arr[0]) + start_seq++;
        }
        #endregion

    }
}
