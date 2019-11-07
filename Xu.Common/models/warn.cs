using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xu.Common
{
    public enum warn
    {
        /// <summary>
        ///警告
        /// </summary>
        WARNING = 1,
        /// <summary>
        /// 普通提示
        /// </summary>
        ALERT = 2,
        /// <summary>
        /// 带确定 取消的提示框
        /// </summary>
        OK_CANCEL = 3,
        /// <summary>
        /// 带标题的确定、取消提示框
        /// </summary>
        OK_CANCEL_WITH_TITILE = 4,
        /// <summary>
        /// 带标题的警告框
        /// </summary>
        WARNING_WITH_TITLE = 5
    }
}
