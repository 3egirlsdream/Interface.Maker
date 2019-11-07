using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xu.Common
{
    /*
    public class SugarContext
    {
        public SugarContext()
        {

        }
        public SugarContext(DbType DbType = DbType.SqlServer)
        {
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = Common.SetConfig("SqlString"),
                DbType = DbType,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });
        }

        public static SqlSugarClient NoTransContext
        {
            get
            {
                SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
                {
                    ConnectionString = Common.SetConfig("SqlString"),
                    DbType = DbType.SqlServer,
                    IsAutoCloseConnection = true,
                    InitKeyType = InitKeyType.Attribute
                });

                return db;
            }
        }
    }
    */
}
