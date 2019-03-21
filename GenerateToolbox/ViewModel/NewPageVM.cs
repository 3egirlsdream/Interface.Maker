using GenerateToolbox.Models;
using Project.G.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Xu.Common;

namespace GenerateToolbox.ViewModel
{
    class NewPageVM : ValidationBase
    {
        public NewPageVM()
        {
            LoadConfig();
        }

        #region

        private List<string> btns { get; set; }

        /// <summary>
        /// 配置文件
        /// </summary>
        private string _config;
        public string config
        {
            get
            {
                return _config;
            }
            set
            {
                _config = value;
                NotifyPropertyChanged("config");
            }
        }
        #endregion

        #region Command
        /// <summary>
        /// 生成函数
        /// </summary>
        public SimpleCommand CmdGenerate => new SimpleCommand()
        {
            ExecuteDelegate = c =>
            {
                Strings.Write(config, "config.xml");
                dynamic res = ExcelHelper.LoadXml();
                ExcelHelper helper = new ExcelHelper();
                var result = helper.OpenExcel((int)res.count);
            }
        };
        #endregion

        #region 私有方法
        /// <summary>
        /// 加载配置文件
        /// </summary>
        private void LoadConfig()
        {
            config = Strings.LoadJson("config.xml");
        }

        
        #endregion
    }
}
