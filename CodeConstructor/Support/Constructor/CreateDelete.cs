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
    public class CreateDelete
    {
        public CreateDelete() { }
        public string GenerateDelete(DataTable tables)
        {
            string xmlName = Application.StartupPath.ToString().Replace("\\bin\\Debug", "\\Support\\Constructor") + "\\Template.xml";
            string codeStr = string.Empty;
            string temp = string.Empty;
            //foreach (DataRow table in tables.Rows)
            //{
            //    string tableName = table["name"].ToString();
            //    DataTable columns = Support.DataBaseHandler.Select.GetColumnInfo(tableName);
            //    string tableNameUp = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(tableName);
            //    string tableDesc = columns.Rows[0]["tableDescription"].ToString();
            //    string primaryKey = columns.Rows.Count > 0 ? columns.Rows[0]["name"].ToString() : string.Empty;

            //    string summaryParam = string.Format(XmlReader.GetStr(xmlName, "Template", "Delete", "SummaryParam"), tableNameUp, tableDesc);
            //    string body = string.Empty;
            //    string values = string.Empty;
            //    foreach (DataRow column in columns.Rows)
            //    {
            //        if (column["isPrimaryKey"].ToString() == "1")
            //        {
            //            primaryKey = column["name"].ToString();
            //        }
            //        if (column["sqlType"].ToString().Contains("date"))
            //        {
            //            values += "_" + tableName + "." + column["name"].ToString() + " == null ? \"NULL\" : _" + tableName + "." + column["name"].ToString() + ", ";
            //        }
            //        else
            //        {
            //            values += "_" + tableName + "." + column["name"].ToString() + ", ";
            //        }
            //    }
            //    values += "_" + tableName + "." + primaryKey;
            //    body = string.Format(XmlReader.GetStr(xmlName, "Template", "Delete", "Body"), tableName + " " + tableDesc, summaryParam, tableNameUp, tableName, values);
            //    temp += body;
            //}
            codeStr = string.Format(XmlReader.GetStr(xmlName, "Template", "Delete", "Head"), Settings.Default.Namespace, DateTime.Now, temp);
            return codeStr.Replace("@", "{").Replace("$", "}").Replace("\n            ", "\n").Trim();
            //Int32? dd = null;
            //return string.Format("nullTest = {0}", dd==null?"NULL":dd.ToString());
        }
    }
}
