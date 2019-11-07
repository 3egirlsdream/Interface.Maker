using Job.Common;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using Xu.Common;
using SqlSugar;
#pragma warning disable S1144 // Unused private types or members should be removed
#pragma warning disable S1643 // Strings should not be concatenated using '+' in a loop

namespace Model.Helper
{

    public class ModelHelper
    {
        public ModelHelper() { }

        public ModelHelper(string _json, string _namespace) { }

        /// <summary>
        /// 模型生成
        /// </summary>
        /// <param name="ColJson">表JSON</param>
        /// <param name="NameSpace">命名空间</param>
        /// <returns></returns>
        public string ModelCreate(string ColJson, string NameSpace)
        {
            ColJson = ColJson.Replace(@"\", "");
            List<Models> models = JsonConvert.DeserializeObject<List<Models>>(ColJson);
            ArrayList Cols = new ArrayList();
            string table_name = models[0].TABLE_NAME.ToUpper();
            string arr = "﻿using System;using System.Data;using System.Collections.Generic;using Creative.ODA;using MeiCloud.DataAccess;namespace " + NameSpace +"{internal partial class "
                + table_name + " : OrgModelBase{public " + table_name + "(){//this.ID = GenerateNewID();this.STATE = \"A\";}public " + table_name + "(string id){this.ID = id;this.STATE = \"A\";}";

            foreach (var model in models)
            {
                string str = " public ";
                Cols.Add(Col(model.COLUMN_NAME));
                if (model.COLUMN_NAME.ToUpper() != "ID" 
                    && model.COLUMN_NAME.ToUpper() != "STATE"
                    && model.COLUMN_NAME.ToUpper() != "DATETIME_CREATED"
                    && model.COLUMN_NAME.ToUpper() != "ORG_ID"
                    && model.COLUMN_NAME.ToUpper() != "ENTERPRISE_ID"
                    && model.COLUMN_NAME.ToUpper() != "USER_CREATED"
                    && model.COLUMN_NAME.ToUpper() != "DATETIME_MODIFIED"
                    && model.COLUMN_NAME.ToUpper() != "USER_MODIFIED")
                {
                    str += GetType(model.DATA_TYPE, model.IS_NULLABLE) + " " + model.COLUMN_NAME.ToUpper() + " {get;set;}";
                    arr += str;
                }
            }
            arr += "}";

            arr += "internal partial class " + GetCmd(table_name) + " : OrgDbSet<" + table_name + ">{public override string CmdName => \"" + table_name + "\";";

            for(int i = 0; i < models.Count; i++)
            {
                string str = "public ODAColumns " + Cols[i] + " => new ODAColumns(this, \"" + models[i].COLUMN_NAME.ToUpper() + "\", ODAdbType." + CharType(models[i].DATA_TYPE) + "," + CharLength(models[i].COLUMN_TYPE) + ");";
                arr += str;
            }

            arr += "public override List<ODAColumns> GetColumnList(){return new List<ODAColumns>(){";
            for(int i = 0; i < Cols.Count; i++)
            {
                if (i != Cols.Count - 1)
                    arr += (Cols[i] + ",");
                else
                    arr += Cols[i];
            }
            arr += "};}}}";
            return arr;
        }

        public string ModelCreate(string ColJson, string NameSpace, string Sugar)
        {
            ColJson = ColJson.Replace(@"\", "");
            List<Models> models = JsonConvert.DeserializeObject<List<Models>>(ColJson);
            ArrayList Cols = new ArrayList();
            string table_name = models[0].TABLE_NAME.ToUpper();
            string arr = "﻿using System;using System.Data;using System.Collections.Generic;namespace SugarModel{internal partial class "
                + table_name + " {public " + table_name + "(){//this.ID = GenerateNewID();this.STATE = \"A\";}public " + table_name + "(string id){this.ID = id;this.STATE = \"A\";}";

            foreach (var model in models)
            {
                string str = " public ";
                Cols.Add(Col(model.COLUMN_NAME));
                
                    str += GetType(model.DATA_TYPE, model.IS_NULLABLE) + " " + model.COLUMN_NAME.ToUpper() + " {get;set;}";
                    arr += str;
                
            }
            arr += " }}";

            
            return arr;
        }

        /// <summary>
        /// 模型的数据库类型
        /// </summary>
        /// <param name="str">原类型</param>
        /// <returns></returns>
        private string CharType(string str)
        {
            str = str.ToLower();
            if (str == "decimal" || str == "decimal?" || str == "number")
                return "ODecimal";
            else if (str == "datetime" || str == "datetime?" || str == "date")
                return "ODatetime";
            else if (str == "int" || str == "int?" || str == "mediumint" || str.Contains("int"))
                return "OInt";
            else if (str == "char" || str == "nchar")
                return "OChar";
            else if (str == "varchar" || str == "text" || str == "varchar2" || str == "blob" || str == "nvarchar2" || str == "clob")
                return "OVarchar";
            return null;
        }

        /// <summary>
        /// 获取字段长度
        /// </summary>
        /// <param name="str">MySql:COLUME_TYPE  SqlServer:CHARACTER_MAXIMUM_LENGTH</param>
        /// <returns></returns>
        private string CharLength(string str)
        {
            if (str == "datetime")
                return "9";
            if (str == "text") return "2000";
            string s = "";
            if (str == null || str == "")
                return "9";
            for(int i = 0; i < str.Length; i++)
            {
                if (Char.IsDigit(str[i]))
                    s += str[i];
            }
            return !String.IsNullOrEmpty(s) ? s : "8";
        }

        public string Col(string str)
        {
            string s = "Col" + str[0].ToString().ToUpper();
            for(int i = 1; i < str.Length; i++)
            {
                if(str[i] == '_')
                {
                    ++i;
                    s += str[i].ToString().ToUpper();
                }
                else
                   s += str[i].ToString().ToLower();
            }
            return s;
        }

        public string GetCmd(string str)
        {
            if (!str.Contains('_'))
                return "Cmd" + str;
            else
            {
                str = "Cmd" + Col(str);
                return str.Replace("Col", "");
            }
        }

        /// <summary>
        /// 获取数据库字段类型
        /// </summary>
        /// <param name="str">DATA_TYPE字段</param>
        /// <param name="IsNullAble">是否为空</param>
        /// <returns></returns>
        private string GetType(string str, string IsNullAble)
        {
            str = str.ToLower();
            if (str == "decimal" || str == "decimal?" || str == "number")
                str = "decimal";
            else if (str == "datetime" || str == "datetime?" || str == "date")
                str = "DateTime";
            else if (str == "int" || str == "int?" || str == "mediumint" || str.Contains("int"))
                str = "int";
            else if (str == "char" || str == "nchar")
                return "string";
            else if (str == "varchar" || str == "text" || str == "varchar2" || str == "blob" || str == "nvarchar2" || str == "clob")
                return "string";
            if (IsNullAble == "YES" || IsNullAble == "Y")
                str += "?";
            return str;
        }
        /*
         * 
         * 旧版已弃用
        public string GetTableJson(string TableName, Xu.Common.DbType type)
        {
            if (type == Xu.Common.DbType.MySql)
            {
                MySqlConnection conn = new MySqlConnection(Xu.Common.Common.SetConfig("SqlString"));
                string sql = "select * from information_schema.COLUMNS where table_name = '" + TableName + "'";
                MySqlDataAdapter ds = new MySqlDataAdapter(sql, conn);
                conn.Open();
                DataTable dt = new DataTable();
                ds.Fill(dt);
                conn.Close();
                return JsonConvert.SerializeObject(dt);
            }
            else if (type == Xu.Common.DbType.SqlServer)
            {
                SqlConnection conn = new SqlConnection(Common.SetConfig("SqlString"));
                string sql = "select * from "+ Common.SetConfig("DataBaseName") +".information_schema.COLUMNS where table_name = '" + TableName +"'";
                SqlDataAdapter apter = new SqlDataAdapter(sql, conn);
                conn.Open();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                apter.Fill(ds);
                dt = ds.Tables[0];
                conn.Close();
                return JsonConvert.SerializeObject(dt);
            }
            else return null;
        }
        */

        /// <summary>
        /// 获取表JSON
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="type">数据库类型</param>
        /// <returns></returns>
        public string GetTableJson(string TableName, SqlSugar.DbType type)
        {
            SqlSugarClient db = new SqlSugarClient(
            new ConnectionConfig()
            {
                ConnectionString = Common.SetConfig("SqlString"),
                DbType = type,//设置数据库类型
                IsAutoCloseConnection = true,//自动释放数据务，如果存在事务，在事务结束后释放
                InitKeyType = InitKeyType.Attribute //从实体特性中读取主键自增列信息
            });

            DataTable list = new DataTable();
            if (type == SqlSugar.DbType.MySql) {
                list = db.Queryable<Models>().Where(e => e.TABLE_NAME == TableName).ToDataTable();
            }//
            else if(type == SqlSugar.DbType.SqlServer)
            {
                list = db.Queryable<INFORMATION_SCHEMA>().Where(e=>e.TABLE_NAME == TableName).ToDataTable();
                list.Columns["CHARACTER_MAXIMUM_LENGTH"].ColumnName = "COLUMN_TYPE";
            }
            else if(type == SqlSugar.DbType.Oracle)
            {
                list = db.Queryable<USER_TAB_COLUMNS>().Where(e => e.TABLE_NAME == TableName.ToUpper()).ToDataTable();
                list.Columns["NULLABLE"].ColumnName = "IS_NULLABLE";
            }
            return JsonConvert.SerializeObject(list);
        }

        public string GetTableJson(string TableName, SqlSugar.DbType type, string ConnString)
        {
            SqlSugarClient db = new SqlSugarClient(
            new ConnectionConfig()
            {
                ConnectionString = ConnString,
                DbType = type,//设置数据库类型
                IsAutoCloseConnection = true,//自动释放数据务，如果存在事务，在事务结束后释放
                InitKeyType = InitKeyType.Attribute //从实体特性中读取主键自增列信息
            });

            DataTable list = new DataTable();
            if (type == SqlSugar.DbType.MySql)
            {
                list = db.Queryable<Models>().Where(e => e.TABLE_NAME == TableName).ToDataTable();
            }//
            else if (type == SqlSugar.DbType.SqlServer)
            {
                list = db.Queryable<INFORMATION_SCHEMA>().Where(e => e.TABLE_NAME == TableName).ToDataTable();
                list.Columns["CHARACTER_MAXIMUM_LENGTH"].ColumnName = "COLUMN_TYPE";
            }
            return JsonConvert.SerializeObject(list);
        }
    }
}
#pragma warning restore S1144 // Unused private types or members should be removed
#pragma warning restore S1643 // Strings should not be concatenated using '+' in a loop

