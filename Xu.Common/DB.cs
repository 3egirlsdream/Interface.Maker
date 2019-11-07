using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xu.Common
{

    public enum TextType
    {
        /// <summary>
        /// 查询表字段
        /// </summary>
        Table = 0,
        /// <summary>
        /// 查询所有数据库
        /// </summary>
        DataBase = 1
    }

    public enum DbType
    {
        SqlServer = 0, 
        MySql =  1,
        Oracle = 2
    }

    public class DB
    {
        public static List<string> GetName(string name, TextType type)
        {
            List<string> columnName = new List<string>();
            try
            {
                SqlConnection conn = new SqlConnection(Common.SetConfig("SqlString"));
                string sql = "";
                if (type == TextType.DataBase)
                    sql = "select name from syscolumns where id = object_id('" + name + "')";
                if (type == TextType.Table)
                    sql = "SELECT Name FROM " + name + "..SysObjects Where XType='U' ORDER BY Name ";//点表示层级

                conn.Open();
                SqlCommand com = new SqlCommand(sql, conn);
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    columnName.Add(dr[0].ToString());
                }
                dr.Close(); conn.Close();
            }
            catch (Exception e)
            {
                //Warning warning = new Warning("Only support SQL server!");
                //warning.ShowDialog();
            }
            return columnName;
        }

        public static List<string> GetName(DbType dbType)
        {
            List<string> columnName = new List<string>();
            try
            {
                SqlConnection conn = new SqlConnection(Common.SetConfig("SqlString"));
                string sql = "Select Name FROM Master.dbo.SysDatabases orDER BY Name";

                conn.Open();
                SqlCommand com = new SqlCommand(sql, conn);
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    columnName.Add(dr[0].ToString());
                }
                dr.Close(); conn.Close();
            }
            catch (Exception e)
            {
                //Warning warning = new Warning("Only support SQL server!");
                //warning.ShowDialog();
            }
            return columnName;
        }
    }
}
