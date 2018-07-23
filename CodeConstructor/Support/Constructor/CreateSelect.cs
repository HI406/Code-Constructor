using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CodeConstructor.Properties;

namespace CodeConstructor.Support.Constructor
{
    /// <summary>
    /// 生成 Select 类
    /// </summary>
    public class CreateSelect
    {
        public CreateSelect() { }

        public string GenerateSelect(DataTable tables)
        {
            string xmlName = Application.StartupPath.ToString().Replace("\\bin\\Debug", "\\Support\\Constructor") + "\\Template.xml";
            string codeStr = string.Empty;
            string temp = string.Empty;
            string tableDesc = string.Empty;
            foreach (DataRow table in tables.Rows)
            {
                string tableName = table["name"].ToString();
                string tableType = table["xtype"].ToString().Trim().ToUpper();
                DataTable columns = Support.DataBaseHandler.Select.GetColumnInfo(tableType, tableName);
                if (columns.Rows.Count > 0)
                {
                    string primaryKey = columns.Rows[0]["name"].ToString();
                    string primaryKeyDesc = columns.Rows.Count > 0 ? columns.Rows[0]["description"].ToString() : string.Empty;
                    //string primaryKeyType = columns.Rows.Count > 0 ? ConvertType.SqlTypeString2CsharpTypeString(columns.Rows[0]["sqlType"].ToString()) : string.Empty;
                    tableDesc = columns.Rows[0]["tableDescription"].ToString();
                    string summaryParam = string.Empty;
                    string body = string.Empty;
                    foreach (DataRow column in columns.Rows)
                    {
                        if (column["isPrimaryKey"].ToString() == "1")
                        {
                            primaryKey = column["name"].ToString();
                            primaryKeyDesc = column["description"].ToString();
                            //primaryKeyType = ConvertType.SqlTypeString2CsharpTypeString(column["sqlType"].ToString());
                            //primaryKeyType = "string";
                        }
                        else if (column["name"].ToString().ToUpper().Contains("ID"))
                        {
                            primaryKey = column["name"].ToString();
                        }
                    }
                    primaryKey = CheckKeywords.InCSharp(primaryKey);
                    summaryParam = string.Format(XmlReader.GetStr(xmlName, "Template", "Select", "SummaryParam"), primaryKey, primaryKeyDesc);
                    string tableNameUp = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(tableName);
                    body = string.Format(XmlReader.GetStr(xmlName, "Template", "Select", "Body"), tableName + " " + tableDesc, summaryParam, tableNameUp, primaryKey);
                    temp += body;
                }
            }
            codeStr = string.Format(XmlReader.GetStr(xmlName, "Template", "Select", "Head"), Settings.Default.Namespace, DateTime.Now,  temp);
            return codeStr.Replace("@", "{").Replace("$", "}").Replace("\n            ", "\n").Trim();
        }
    }
}
