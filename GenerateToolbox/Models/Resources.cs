using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.G.Models
{
    public static class Resources
    {
        /// <summary>
        /// 资源文件拓展
        /// </summary>
        /// <returns></returns>
        public static string CreateResorce(List<Excel> Contents)
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
                    tmp += "    <sys:String x:Key=\"" + ds.SEARCH_CODE + "_Watermark\">请输入" + ds.SEARCH_NAME + "</sys:String>\r\n";
                s += tmp;
                str.Add(ds.SEARCH_CODE);
            }


            foreach (var ds in Contents)
            {
                if (mark.Contains(ds.SEARCH_CODE))
                    continue;
                string tmp = "";
                if (!String.IsNullOrEmpty(ds.SEARCH_CODE) && !String.IsNullOrEmpty(ds.SEARCH_NAME))
                    tmp += "    <sys:String x:Key=\"Grid_" + ds.SEARCH_CODE + "\">" + ds.SEARCH_NAME + "</sys:String>\r\n";
                s += tmp;
                mark.Add(ds.SEARCH_CODE);
            }
            return s;
        }

        /// <summary>
        /// 取所有资源文件key的参数
        /// </summary>
        /// <returns></returns>
        public static List<Excel> AllRes(List<Excel> IndexContents, List<Excel> IndexBodies, List<MyModel> boxes)
        {
            List<Excel> excels = new List<Excel>();
            foreach (var ds in IndexContents)
            {
                Excel ex = new Excel();
                ex.SEARCH_CODE = ds.SEARCH_CODE;
                ex.SEARCH_NAME = ds.SEARCH_NAME;
                excels.Add(ex);
            }

            foreach (var ds in IndexBodies)
            {
                Excel ex = new Excel();
                ex.SEARCH_CODE = ds.GRID_CODE;
                ex.SEARCH_NAME = ds.GRID_NAME;
                excels.Add(ex);
            }

            foreach (var ds in boxes)
            {
                foreach (var ls in ds.Content)
                {
                    Excel ex = new Excel();
                    ex.SEARCH_CODE = ls.GRID_CODE;
                    ex.SEARCH_NAME = ls.GRID_NAME;
                    excels.Add(ex);
                }
            }
            return excels;
        }


        #region 新版资源文件
        /// <summary>
        /// [新]资源文件拓展
        /// </summary>
        /// <returns></returns>
        public static string CreateResorce_new(List<Key_Value> Contents)
        {
            string s = "";
            List<string> str = new List<string>();
            List<string> mark = new List<string>();
            List<string> mark2 = new List<string>();
            foreach (var ds in Contents)
            {
                if (str.Contains(ds.Key))
                    continue;
                string tmp = "";
                if (!String.IsNullOrEmpty(ds.Key) && !String.IsNullOrEmpty(ds.Value))
                    tmp += "    <sys:String x:Key=\"" + ds.Key + "_Watermark\">请输入" + ds.Value + "</sys:String>\r\n";
                s += tmp;
                str.Add(ds.Key);
            }

            foreach (var ds in Contents)
            {
                if (mark.Contains(ds.Key))
                    continue;
                string tmp = "";
                if (!String.IsNullOrEmpty(ds.Key) && !String.IsNullOrEmpty(ds.Value))
                    tmp += "    <sys:String x:Key=\"Grid_" + ds.Key + "\">" + ds.Value + "</sys:String>\r\n";
                s += tmp;
                mark.Add(ds.Key);
            }
            foreach (var ds in Contents)
            {
                if (mark2.Contains(ds.Key))
                    continue;
                string tmp = "";
                if (!String.IsNullOrEmpty(ds.Key) && !String.IsNullOrEmpty(ds.Value))
                    tmp += "    <sys:String x:Key=\"" + ds.Key + "\">" + ds.Value + "</sys:String>\r\n";
                s += tmp;
                mark2.Add(ds.Key);
            }

            return s;
        }

        /// <summary>
        /// [新]取所有资源文件key的参数
        /// </summary>
        /// <returns></returns>
        public static List<Key_Value> AllRes_new(List<Grids> grids)
        {
            List<Key_Value> vs = new List<Key_Value>();
            foreach (var marx in grids)
            {
                foreach(var ds in marx.grids)
                {
                    if(ds.CONTROL_NAME != "NEXT_LINE")
                    {
                        var key = new Key_Value
                        {
                            Key = ds.CODE,
                            Value = ds.NAME
                        };
                        if (!CreateClass.Contains(vs, key))
                        {
                            vs.Add(key);
                        }    
                    }
                }
                var pageName = new Key_Value
                {
                    Key = marx.PageCode,
                    Value = marx.PageName
                };
                if (!CreateClass.Contains(vs, pageName))
                {
                    vs.Add(pageName);
                }
            }
            return vs;
        }
        #endregion


    }
}
