using Model.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.G.Models
{
    public static class Import
    {

        /// <summary>
        /// 生成读取excel的代码
        /// </summary>
        /// <param name="import"></param>
        /// <returns></returns>
        public static string CreateXss(ImportClass import)
        {
            string s = "";
            for (int i = 0; i < import.Body.Count; i++)
            {
                string tmp = "model." + import.Body[i].GRID_CODE + " = sheet.GetRow(i).GetCell(" + i + ") == null ? \"\" : sheet.GetRow(i).GetCell(" + i + ").ToString();\r\n\r\n                        ";
                s += tmp;
            }
            return s;
        }

        /// <summary>
        /// 生成校正导入数据是否为空
        /// </summary>
        /// <param name="import"></param>
        /// <returns></returns>
        public static string CreateNull(ImportClass import)
        {
            string s = "";
            for (int i = 0; i < import.EMPTY_CODE.Count; i++)
            {
                string tmp = "";
                if (i != 0) tmp += "else ";
                tmp += "if (string.IsNullOrEmpty(model." + import.EMPTY_CODE[i] + "))\r\n                        {\r\n                            " +
                "model.TextState = \"失败！\" + Translator.Get(\"Grid_" + import.EMPTY_CODE[i] + "\") + \"不能为空\";\r\n                            " +
                "model.Color = \"Red\";\r\n              " +
                "model.IsChecked = false;\r\n              ";
                s += tmp + "}\r\n";
            }
            return s;
        }

        /// <summary>
        /// 生成校验是否重复的代码
        /// </summary>
        /// <param name="import"></param>
        /// <returns></returns>
        public static string CreateRepeat(ImportClass import)
        {
            string s = "else if (HasWord(model." + import.REPEAT_CODE[0];
            for (int i = 1; i < import.REPEAT_CODE.Count; i++)
            {
                string tmp = ",model." + import.REPEAT_CODE[i];
                s += tmp;
            }
            return s += "))\r\n                        " +
                "{\r\n                            model.TextState = \"失败！词条数据已存在\";\r\n                            " +
                "model.Color = \"Red\";\r\n                        " +
                "model.IsChecked = false;\r\n              " +
            "}\r\n                        else\r\n                        {\r\n                            model.IsChecked = true;\r\n                            model.TextState = \"成功！\";\r\n                            model.Color = \"Green\";\r\n                        }\r\n\r\n                        "; ;
        }

        /// <summary>
        /// 生成校验重复的函数体
        /// </summary>
        /// <param name="import"></param>
        /// <returns></returns>
        public static string CreateRepeatFunction(ImportClass import)
        {
            string s = "private bool HasWord(string " + import.REPEAT_CODE[0];
            for (int i = 1; i < import.REPEAT_CODE.Count; i++)
            {
                string tmp = ", string " + import.REPEAT_CODE[i];
                s += tmp;
            }
            s += ")\r\n        {\r\n            var res = plugin.Framework.GetData(Services.url_hasword";
            for (int i = 0; i < import.REPEAT_CODE.Count; i++)
            {
                ModelHelper model = new ModelHelper();
                string tmp = ", " + import.REPEAT_CODE[i];
                s += tmp;
            }
            return s + ");";
        }


        /// <summary>
        /// 生成导入的url
        /// </summary>
        /// <param name="ProjectName"></param>
        /// <param name="import"></param>
        /// <returns></returns>
        public static string CreateImportUrl(string ProjectName, ImportClass import)
        {
            var ls = ProjectName.Split('.');
            string s = "\r\n\t\tpublic const string url_hasword = \"/api/" + ls.Last().ToLower() + "/" + ls.Last().ToLower() + "/hasword?";
            for (int i = 0; i < import.REPEAT_CODE.Count(); i++)
            {
                ModelHelper model = new ModelHelper();
                string tmp = "";
                if (i > 0) tmp += "&";
                tmp += model.Col(import.REPEAT_CODE[i]).Replace("Col", "") + "={" + i + "}";
                s += tmp;
            }
            return s + "\";";
        }


        /// <summary>
        /// 检测List内是否有重复数据
        /// </summary>
        /// <param name="import"></param>
        /// <returns></returns>
        public static string CheckImportData(ImportClass import)
        {
            string s = "";
            foreach (var ds in import.REPEAT_CODE)
            {
                s += "x." + ds + " == model." + ds;
                if (ds != import.REPEAT_CODE.Last())
                    s += "&&";
            }
            return s;
        }
    }
}
