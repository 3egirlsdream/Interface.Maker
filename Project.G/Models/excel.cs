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
        public string label { get; set; }
        public int value { get; set; }
        public override string ToString()
        {
            return label.ToString();
        }
    }
}
