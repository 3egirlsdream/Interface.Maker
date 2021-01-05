using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Xu.Common
{

    public static class Common
    {
        /// <summary>
        /// 是否为系统保留类型关键字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsType(string str)
        {
            switch (str)
            {
                case "string": return true;
                case "DateTime": return true;
                case "int": return true;
                case "DateTime?": return true;
                case "char": return true;
                case "String": return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// 字符串是否有小写字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool HasLower(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (Char.IsLower(str[i])) return false;
            }
            return true;
        }
        /// <summary>
        /// 对大括号进行配对
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static int count(char str)
        {
            int t = 0;

            if (str == '{') t++;
            if (str == '}') t--;
            //if (str == ';') t--;
            return t;
        }
        /// <summary>
        /// 返回t个Tab
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private static string reTab(int t)
        {
            string str = "";
            while (t-- > 0)
            {
                str += "\t";
            }
            return str;
        }

        /// <summary>
        /// 格式化代码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string format(string str)
        {
            str += "}";
            int cot = 0;
            int len = str.Length;
            string s = "";
            s += str[0];
            for (int i = 1; i < len - 1; i++)
            {
                cot += count(str[i]);
                s += str[i];

                if (str[i] == ';' && str[i - 1] == 'l' && str[i - 2] == 'l')
                    continue;
                if(len - i > 5 && str[i] == '{' && str[i + 4] == ';')
                {
                    continue;
                }
                if(str[i] == '}' && str[i + 1] == ',')
                {
                    continue;
                }
                if(str[i] == '{')
                {
                    if(str[i + 1] == '}')
                    {
                        s += '\n';
                        s += reTab(cot - 1);
                    }
                    else
                    {
                        s += '\n';
                        s += reTab(cot);
                    }
                }
                if(str[i] == '}' && str[i + 1] == ';')
                {
                    continue;
                }
                if(str[i] == ';' && str[i - 1] == '{')
                {
                    s += '\n';
                    s += reTab(cot);
                }
                if(len - i > 5 && (str[i] == ';' && (str[i - 4] == ';' )||(str[i] == ';' && i < len - 4 && str[i + 4] == ';')))
                {
                    continue;
                }
                if(str[i] == '}' && str[i - 1] != ';' && str[i + 1] != ';')
                {
                    s += '\n';
                    s += reTab(cot);
                }
                if(str[i] == ';')
                {
                    if(str[i + 1] == '}')
                    {
                        s += '\n';
                        s += reTab(cot - 1);
                    }
                    else
                    {
                        s += '\n';
                        s += reTab(cot);
                    }
                }
                if(str[i] == '}')
                {
                    if (len - i > 3 && str[i + 1] == '}' && str[i + 2] == ';')
                    {

                    }
                    else if(str[i + 1] == '}')
                    {
                        s += '\n';
                        s += reTab(cot - 1);
                    }
                    else
                    {
                        s += '\n';
                        s += reTab(cot);
                    }
                }
                if (len - i > 3 && str[i + 1] == '}' && i < len - 2 && str[i + 2] == ';')
                {
                    s += '\n';
                    s += reTab(cot - 1);
                }
                if(str[i] == ',' && str[i - 1] == '}')
                {
                    s += '\n';
                    s += reTab(cot);
                }
                /*
                if (str[i] == ';')
                {
                    s += '\n';
                    s += reTab(cot);
                }
                if (str[i] == '{' || str[i + 1] == '{')
                {
                    s += '\n';
                    s += reTab(cot);
                }
                else if (str[i] == '}' || (str[i + 1] == '}' && str[i] != ';'))
                {
                    s += '\n';
                    s += reTab(cot);
                }*/

            }
            return s + "\n";
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="fileName">原文件路径</param>
        /// <param name="aimPlace">文本内容</param>
        public static void Export(string fileName, string aimPlace)
        {
            System.Windows.Forms.SaveFileDialog objSave = new System.Windows.Forms.SaveFileDialog();
            objSave.Filter = "(*.txt)|*.txt|" + "(*.*)|*.*";

            objSave.FileName = fileName;

            if (objSave.ShowDialog() == DialogResult.OK)
            {
                StreamWriter FileWriter = new StreamWriter(objSave.FileName, true); //写文件

                FileWriter.Write(aimPlace);//将字符串写入
                FileWriter.Close(); //关闭StreamWriter对象
            }
        }

        public static string Export(string fileName, string text, string filter = "(*.txt)|*.txt|" + "(*.*)|*.*")
        {
            System.Windows.Forms.SaveFileDialog objSave = new System.Windows.Forms.SaveFileDialog();
            objSave.Filter = filter;

            objSave.FileName = fileName;
            if (objSave.ShowDialog() == DialogResult.OK)
            {
                FileStream FileWriter = new FileStream(objSave.FileName, FileMode.Create); //写文件
                var bts = System.Text.Encoding.Default.GetBytes(text);
                FileWriter.Write(bts, 0, bts.Length);//将字符串写入
                FileWriter.Close(); //关闭StreamWriter对象
            }
            return objSave.FileName;
        }

        public static void SaveFile(string path, string text)
        {
            FileStream FileWriter = new FileStream(path, FileMode.Create); //写文件
            var bts = System.Text.Encoding.Default.GetBytes(text);
            FileWriter.Write(bts, 0, bts.Length);//将字符串写入
            FileWriter.Close(); //关闭StreamWriter对象
        }


        /////////
        /// <summary>
        /// 保存到配置文件
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void SetConfig(string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[key].Value = value;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        /// <summary>
        /// 返回对应键的值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static string SetConfig(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
        
        public static string UUID()
        {
            string code = null;
            SelectQuery query = new SelectQuery("select * from Win32_ComputerSystemProduct");
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
            {
                foreach (var item in searcher.Get())
                {
                    using (item) code = item["UUID"].ToString();
                }
            }
            return code;
        }
        public static string Key()
        {
            MD5 Md5 = new MD5CryptoServiceProvider();
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(UUID().Replace("-", "") + "CATLNMSL", "MD5");
        }



        public static T Copy<T>(T model)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(Newtonsoft.Json.JsonConvert.SerializeObject(model));
        }

    }

}
