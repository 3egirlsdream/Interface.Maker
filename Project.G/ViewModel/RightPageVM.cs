using Project.G.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        #region
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
    }
}
