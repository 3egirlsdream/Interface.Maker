using MySqlX.XDevAPI.Relational;
using NPOI.XSSF.UserModel;
using Project.G.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateToolbox.Models
{
    public class Excel2Sql
    {
        public static string Excel2Oracle(XSSFSheet sheet, string tableName, int cot, ref string comment)
        {
            var str = "";
            var primarykey = "";
            for (int i = 0; i < cot; i++)
            {
                excel sFC = new excel();
                sFC.name = sheet.GetRow(i).GetCell(1) == null ? "" : sheet.GetRow(i).GetCell(1).ToString();
                if (string.IsNullOrEmpty(sFC.name))
                {
                    cot -= 1;
                    str = str.Remove(str.Length - 3, 2);
                    continue;
                }
                str += (sFC.name + " ");
                sFC.desc = sheet.GetRow(i).GetCell(2) == null ? "" : sheet.GetRow(i).GetCell(2).ToString();
                sFC.type = sheet.GetRow(i).GetCell(3) == null ? "" : sheet.GetRow(i).GetCell(3).ToString();
                str += sFC.type;

                sFC.isNull = sheet.GetRow(i).GetCell(5) == null ? "" : sheet.GetRow(i).GetCell(5).ToString();
                var length = sheet.GetRow(i).GetCell(4) == null ? "0" : sheet.GetRow(i).GetCell(4).ToString();
                sFC.length = Convert.ToInt32(length);
                sFC.defaultContext = sheet.GetRow(i).GetCell(6) == null ? "" : sheet.GetRow(i).GetCell(6).ToString();
                if(sFC.type.ToLower() != "date")
                {
                    str += $"({sFC.length}) ";
                }

                if (sFC.defaultContext.Length != 0)
                {
                    str += (" DEFAULT " + sFC.defaultContext + " ");
                }
                if (sFC.name == "ID")
                {
                    primarykey = sheet.GetRow(i).GetCell(7) == null ? "" : sheet.GetRow(i).GetCell(7).ToString();
                }

                if (sFC.isNull.Length != 0)
                {
                    str += (" NOT NULL ");
                }
                str += " ,\n";
                comment += $"COMMENT ON COLUMN {tableName}.{sFC.name} IS '{sFC.desc}';\r\n";
            }
            str += $"CONSTRAINT {primarykey} PRIMARY KEY ( id ) ENABLE\n";
            return str;
        }
    }
}
