using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.G.Models
{
    /// <summary>
    /// 录入连接条件数据结构
    /// </summary>
    public class Connection
    {
        public Connection(string Header, string Item, string Conditions)
        {
            ConnectionConditions = new List<string>();
            this.Header = Header;
            this.Items = Item;
            ConnectionConditions.Add(Conditions);
        }

        public void Add(string Conditions)
        {
            ConnectionConditions.Add(Conditions);
        }

        public string Header;
        public string Items;
        public List<string> ConnectionConditions { get; set; }

        public override string ToString()
        {
            return Header.ToString();
        }
    }
}
