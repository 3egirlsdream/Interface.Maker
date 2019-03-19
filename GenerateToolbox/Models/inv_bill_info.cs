using System;
using System.Data;
using System.Collections.Generic;
using Creative.ODA;

namespace MeiCloud.DataAccess
{
    internal partial class INV_BILL_INFO : ModelBase
    {
		#region Constructor 
		/// <summary>
        /// 实例化数据模型，将自动生成数据行 ID 号及指定行状态。
        /// </summary>
        public INV_BILL_INFO()
        {
            //this.ID = GenerateNewID();
            this.STATE = "A";
        }

        /// <summary>
        /// 实例化数据模型，用户指定数据行 ID 号并设置数据行默认状态。
        /// </summary>
        /// <param name="id">数据行 ID 号。</param>
        public INV_BILL_INFO(string id)
        {
            this.ID = id;
            this.STATE = "A";
        }
        #endregion

    } 

	internal partial class CmdInvBillInfo : BaseDbSet<INV_BILL_INFO>
	{
		public override string CmdName { get { return "INV_BILL_INFO"; }}

		public ODAColumns ColId { get { return new ODAColumns(this, "ID", ODAdbType.OVarchar, 21); } }
		public ODAColumns ColDatetimeCreated { get { return new ODAColumns(this, "DATETIME_CREATED", ODAdbType.ODatetime, 8); } }


        public override List<ODAColumns> GetColumnList()
        {
            return new List<ODAColumns>()
            {
				ColId,
				ColDatetimeCreated
            };
        }
    }
}
