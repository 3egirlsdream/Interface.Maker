using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.G.Models
{
    public class excel
    {
        public string name { get; set; }
        public string desc { get; set; }
        public string type { get; set; }
        public string isNull { get; set; }
        public string defaultContext { get; set; }
    }

    public class Excel
    {
        public bool IsApi { get; set; }
        public string GRID_CODE { get; set; }
        public string GRID_NAME { get; set; }
        public string CONTROL_CODE { get; set; }
        public string SEARCH_CODE { get; set; }
        public string SEARCH_NAME { get; set; }
        public string BUTTON { get; set; }
    }

    public class Grid
    {
        public bool IS_API { get; set; }
        public string CONTROL_NAME { get;set;}
        public string NAME { get; set; }
        public string CODE { get; set; }

    }

    public class Grids
    {
        public int Level { get; set; }
        public string Identity { get; set; }
        public string PageName { get; set; }
        public string PageCode { get; set; }
        public List<Grid> grids { get; set; }
        public List<string> strs { get; set; }

        public Grids()
        {
            Level = 0;
            grids = new List<Grid>();
            strs = new List<string>();
        }
    }

    /// <summary>
    /// 根据长度排序
    /// </summary>
    public class SortByLength : IComparer
    {
        int IComparer.Compare(object x, object y)
        {
            string a = x as string;
            string b = y as string;
            if (a.Length > b.Length) return 1;
            else if (a.Length < b.Length) return -1;
            else return 0;
        }
    }

    public class Key_Value
    {
        public string Key { get; set; }
        public string Value { get; set; }
        //兼容旧的
        public string label { get; set; }
        public int value { get; set; }
        public override string ToString()
        {
            return label.ToString();
        }
    }

    public class MyModel
    {
        public MyModel()
        {
            Header = new List<Excel>();
            Content = new List<Excel>();
            Body = new List<Excel>();
        }

        public string BOX_CODE { get; set; }
        public string BOX_NAME { get; set; }
        public string SEARCH_CODE { get; set; }

        public List<Excel> Header;
        public List<Excel> Content;
        public List<Excel> Body;
    }

    public class ImportClass
    {
        public ImportClass()
        {
            Body = new List<Im>();
            EMPTY_CODE = new List<string>();
            REPEAT_CODE = new List<string>();
        }
        public List<string> EMPTY_CODE { get; set; }
        public List<string> REPEAT_CODE { get; set; }
        public List<Im> Body { get; set; }
        public string CheckTable { get; set; }
    }

    public class Im
    {
        public string GRID_CODE { get; set; }
        public string GRID_NAME { get; set; }
    }

    public class DataGrids
    {
        public string ItemsSource { get; set; }
        public string SelectedItem { get; set; }
        public string PageSize { get; set; }
        public string TotalCount { get; set; }
        public string PageIndex { get; set; }
        public string IsSelectedAll { get; set; }
    }
}
