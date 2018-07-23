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
    /// 生成 Update 类
    /// </summary>
    public class CreateUpdate
    {
        public CreateUpdate() { }

        public string GenerateUpdate(DataTable tables)
        {
            string xmlName = Application.StartupPath.ToString().Replace("\\bin\\Debug", "\\Support\\Constructor") + "\\Template.xml";
            string codeStr = string.Empty;
            string temp = string.Empty;
            foreach (DataRow table in tables.Rows)
            {
                string tableName = table["name"].ToString();
                DataTable columns = Support.DataBaseHandler.Select.GetColumnInfo(tableName);
                if (columns.Rows.Count > 0)
                {
                    string tableNameUp = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(tableName);
                    string tableDesc = columns.Rows[0]["tableDescription"].ToString();
                    string primaryKey = columns.Rows.Count > 0 ? columns.Rows[0]["name"].ToString() : string.Empty;
                    
                    string summaryParam = string.Format(XmlReader.GetStr(xmlName, "Template", "Update", "SummaryParam"), tableNameUp, tableDesc);
                    string body = string.Empty;
                    string values = string.Empty;
                    foreach (DataRow column in columns.Rows)
                    {
                        string variable = CheckKeywords.InCSharp(column["name"].ToString());
                        if (column["isPrimaryKey"].ToString() == "1")
                        {
                            primaryKey = variable;
                        }
                        else if (column["name"].ToString().ToUpper().Contains("ID"))
                        {
                            primaryKey = column["name"].ToString();
                        }
                        if (column["sqlType"].ToString().Contains("date"))
                        {
                            values += "_" + tableName + "." + variable + " == null ? \"NULL\" : _" + tableName + "." + variable + ".ToString(), ";
                        }
                        else
                        {
                            values += "_" + tableName + "." + variable + ", ";
                        }
                    }
                    primaryKey = CheckKeywords.InCSharp(primaryKey);
                    values += "_" + tableName + "." + primaryKey;
                    body = string.Format(XmlReader.GetStr(xmlName, "Template", "Update", "Body"), tableName + " " + tableDesc, summaryParam, tableNameUp, tableName, values);
                    temp += body;
                }
            }
            codeStr = string.Format(XmlReader.GetStr(xmlName, "Template", "Update", "Head"), Settings.Default.Namespace, DateTime.Now, temp);
            return codeStr.Replace("@", "{").Replace("$", "}").Replace("\n            ", "\n").Trim();
            //Int32? dd = null;
            //return string.Format("nullTest = {0}", dd==null?"NULL":dd.ToString());
        }
    }
}
