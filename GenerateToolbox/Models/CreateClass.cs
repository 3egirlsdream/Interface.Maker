using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xu.Common;

namespace Project.G.Models
{
    public static class CreateClass
    {

        /// <summary>
        /// 生成模型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string LoadModel(List<Excel> lists, string ProjectName)
        {
            string s = "using DAF.Plugin.Common;using System;using System.Collections.Generic;using System.Linq;using System.Text;namespace " + ProjectName + "{public class Model : ValidationBase{";
            s += "public string ID {get;set;}";
            s += "public string Color {get;set;}";
            s += "public string TextState {get; set;}";
            foreach (var marx in lists)
            {
                string model = "";
                model += "//[Excel(Width =5000, Title =\"" + marx.GRID_NAME + "\")]\r\n";
                model += "        public string " + marx.GRID_CODE.ToUpper() + " {get;set;}";
                s += model;
            }
            s += "private bool _IsChecked;public bool IsChecked{get{return _IsChecked;}set{_IsChecked = value;NotifyPropertyChanged(\"IsChecked\");}}}}";
            return Common.format(s);
        }

        /// <summary>
        /// [新]生成模型
        /// </summary>
        public static string LoadModel_new(List<Grids> lists, string projName)
        {
            List<Key_Value> vs = new List<Key_Value>();
            foreach(var grid in lists)
            {
                foreach(var ds in grid.grids)
                {
                    var key = new Key_Value
                    {
                        Key = ds.CODE,
                        Value = ds.NAME
                    };
                    if (ds.CONTROL_NAME == "DATAGRID" && !Contains(vs, key))
                    {
                        vs.Add(key);
                    }
                }
            }

            string s = "using DAF.Plugin.Common;using System;using System.Collections.Generic;using System.Linq;using System.Text;namespace " + projName + "{public class Model : ValidationBase{";
            s += "public string ID {get;set;}";
            s += "public string Color {get;set;}";
            s += "public string TextState {get;set;}";
            foreach (var marx in vs)
            {
                string model = "";
                model += "        //[Excel(Width =5000, Title =\"" + marx.Value + "\")]\r\n";
                model += "        public string " + marx.Key.ToUpper() + " {get;set;}";
                s += model;
            }
            s += "private bool _IsChecked;public bool IsChecked{get{return _IsChecked;}set{_IsChecked = value;NotifyPropertyChanged(\"IsChecked\");}}}}";
            return Common.format(s);
        }

        /// <summary>
        /// 生成下拉框模型类
        /// </summary>
        /// <returns></returns>
        public static string LoadModel(string ProjectName)
        {
            string s = "using DAF.Plugin.Common;using System;using System.Collections.Generic;using System.Linq;using System.Text;namespace " + ProjectName + "{public class ComboxModel : ValidationBase{";
            s += "//下拉框模型\n\t#region models \n\t" +
                "public string Label{get;set;}" +
                "public string Value{get;set;}" +
                "\t#endregion\n//重载ToString()以显示SelectedItem\n";
            s += "public override string ToString()" +
            "{" +
            "    return Label?.ToString();" +
            "}";
            s += "}}";
            return Common.format(s);
        }

        [Obsolete]
        private static bool Equals(Key_Value key1, Key_Value key2)
        {
            if (key1.Key == key2.Key && key1.Value == key2.Value)
                return true;
            else return false;
        }

        public static bool Contains(List<Key_Value> vs, Key_Value key)
        {
            var cot = vs.Where(e => e.Key == key.Key).Count();
            return cot > 0 ? true : false;
        }
        
    }
    
}
