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
    /// 生成 Insert 类
    /// </summary>
    public class CreateInsert
    {
        public CreateInsert() { }

        public string GenerateInsert(DataTable tables)
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
                    string summaryParam = string.Format(XmlReader.GetStr(xmlName, "Template", "Insert", "SummaryParam"), tableNameUp, tableDesc);
                    string body = string.Empty;
                    string values = string.Empty;
                    foreach (DataRow column in columns.Rows)
                    {
                        string variable = CheckKeywords.InCSharp(column["name"].ToString());
                        if (column["sqlType"].ToString().Contains("date"))
                        {
                            values += "_" + tableName + "." + variable + " == null ? \"NULL\" : _" + tableName + "." + variable + ".ToString(), ";
                        }
                        else
                        {
                            values += "_" + tableName + "." + variable + ", ";
                        }
                    }
                    values = values.Length > 2 ? values.Remove(values.Length - 2) : values;
                    body = string.Format(XmlReader.GetStr(xmlName, "Template", "Insert", "Body"), tableName + " " + tableDesc, summaryParam, tableNameUp, tableName, values);
                    temp += body;
                }
            }
            codeStr = string.Format(XmlReader.GetStr(xmlName, "Template", "Insert", "Head"), Settings.Default.Namespace, DateTime.Now, temp);
            return codeStr.Replace("@", "{").Replace("$", "}").Replace("\n            ", "\n").Trim();
            //Int32? dd = null;
            //return string.Format("nullTest = {0}", dd==null?"NULL":dd.ToString());
        }
    }
}
