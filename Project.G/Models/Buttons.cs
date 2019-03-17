using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.G.Models
{
    public static class Buttons
    {

        /// <summary>
        /// 生成Command的名字
        /// </summary>
        /// <param name="s"></param>
        /// <param name="pre">前缀</param>
        /// <returns></returns>
        public static string command(string s, string pre)
        {
            switch (s)
            {
                case "刷新": return pre + "Refresh";
                case "打印": return pre + "Print";
                case "导出": return pre + "Export";
                case "导入": return pre + "Import";
                case "生成": return pre + "Create";
                case "编辑": return pre + "Edit";
                case "新增": return pre + "Add";
                case "删除": return pre + "Delete";
                case "保存": return pre + "Save";
                case "禁用": return pre + "Forbiden";
                case "重置": return pre + "Reset";
                case "优先": return pre + "First";
                case "冻结": return pre + "Frozen";
                case "取消优先/冻结": return pre + "FirstFrozenCancel";
                default: return s;
            }
        }


        /// <summary>
        /// 生成按钮的xaml代码
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        public static string CreateButton(List<string> btn)
        {
            string s = "<WrapPanel>\r\n";
            foreach (var ds in btn)
            {
                string style = ds == "刷新" || ds == "保存" ? "Style=\"{DynamicResource HighLightButtonStyle}\"" : "";
                string tmp = "<Button Content=\"{DynamicResource " + command(ds, "") + "}\" " +
                    "Margin=\"{DynamicResource BtnMargin}\" " +
                    "Command=\"{Binding " + command(ds, "Cmd") + "}\" " +
                    "controls:ButtonHelper.IconContent=\"{DynamicResource " + command(ds, "Icon_") + "}\" " +
                    style +
                    "/>\r\n";
                s += tmp;
            }
            s += "</WrapPanel>";
            return s;
        }


    }
}
