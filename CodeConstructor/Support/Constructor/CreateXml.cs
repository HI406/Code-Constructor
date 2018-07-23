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
    /// 生成 Query.xml 类
    /// </summary>
    public class CreateXml
    {
        public CreateXml() { }

        public string GenerateQueryXml(DataTable tables)
        {
            string xmlName = Application.StartupPath.ToString().Replace("\\bin\\Debug", "\\Support\\Constructor") + "\\Template.xml";
            string codeStr = string.Empty;

            string selectStr = string.Empty;
            string updateStr = string.Empty;
            string insertStr = string.Empty;
            string deleteStr = string.Empty;

            foreach(DataRow table in tables.Rows)
            {
                string tableName = table["name"].ToString();
                string tableType = table["xtype"].ToString().Trim().ToUpper();
                DataTable columns = Support.DataBaseHandler.Select.GetColumnInfo(tableType, tableName);

                string selectSqlStr = "SELECT * FROM " + tableName;
                string updateSqlStr = string.Empty;
                string insertSqlStr = string.Empty;
                string deleteSqlStr = string.Empty;
                string primaryKey = columns.Rows.Count > 0 ? columns.Rows[0]["name"].ToString() : string.Empty;
                string tableDescription = string.Empty;
                string updateSet = string.Empty;
                string insertInto = string.Empty;
                string insertValue = string.Empty;
                string columnsName = string.Empty;

                for(int i = 0 ;i < columns.Rows.Count ;i++)
                {
                    tableDescription = columns.Rows[i]["tableDescription"].ToString();
                    if(columns.Rows[i]["isPrimaryKey"].ToString() == "1")
                    {
                        primaryKey = columns.Rows[i]["name"].ToString();
                    }
                    else if (columns.Rows[i]["name"].ToString().ToUpper().Contains("ID"))
                    {
                        primaryKey = columns.Rows[i]["name"].ToString();
                    }

                    updateSet += columns.Rows[i]["name"].ToString() + " = '@" + i + "$', ";
                    insertInto += columns.Rows[i]["name"].ToString() + ", ";
                    insertValue += "'@" + i + "$', ";
                }
                updateSet = updateSet.Length > 2 ? updateSet.Remove(updateSet.Length - 2) : updateSet;
                insertInto = insertInto.Length > 2 ? insertInto.Remove(insertInto.Length - 2) : insertInto;
                insertValue = insertValue.Length > 2 ? insertValue.Remove(insertValue.Length - 2) : insertValue;
                string tableNameUp = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(tableName);
                selectSqlStr = string.Format(XmlReader.GetStr(xmlName, "Template", "Select", "SQL.xml"), tableDescription + " " + tableNameUp, tableNameUp, selectSqlStr, selectSqlStr + (string.IsNullOrEmpty(primaryKey) ?"":" WHERE " + primaryKey + " = @0$"));
                selectStr += selectSqlStr;

                if (tableType == "U")
                {
                    updateSqlStr = string.Format(XmlReader.GetStr(xmlName, "Template", "Update", "SQL.xml"), tableDescription + " " + tableNameUp, tableNameUp, tableName, updateSet, primaryKey, columns.Rows.Count);
                    updateStr += updateSqlStr;

                    insertSqlStr = string.Format(XmlReader.GetStr(xmlName, "Template", "Insert", "SQL.xml"), tableDescription + " " + tableNameUp, tableNameUp, tableName, insertInto, insertValue);
                    insertStr += insertSqlStr;

                    //deleteSqlStr= string.Format(XmlReader.GetStr(xmlName, "Template", "Delete", "SQL.xml"), tableDescription + " " + tableNameUp, tableNameUp, tableName, primaryKey);
                    //deleteStr += deleteSqlStr;
                }
            }

            codeStr += string.Format(XmlReader.GetStr(xmlName, "Template", "Query.xml"), selectStr, updateStr, insertStr, deleteStr);
            return codeStr.Replace("@", "{").Replace("$", "}").Replace("\n            ", "\n").Trim();
        }

        public string GenerateXmlReader()
        {
            string xmlName = Application.StartupPath.ToString().Replace("\\bin\\Debug", "\\Support\\Constructor") + "\\Template.xml";
            string codeStr = string.Empty;
            string tempStr = string.Empty;
            string[] types = XmlReader.GetStr(xmlName, "Template", "XmlReader", "Types").Split(',');
            foreach (string type in types)
            {
                tempStr += string.Format(XmlReader.GetStr(xmlName, "Template", "XmlReader", "Case"), type.Trim().ToUpper(), type.Trim().ToLower());
            }
            codeStr = string.Format(XmlReader.GetStr(xmlName, "Template", "XmlReader", "Head"), Settings.Default.Namespace, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), tempStr.Trim());
            return codeStr.Replace("@", "{").Replace("$", "}").Replace("\n            ", "\n").Trim();

            //StringBuilder sb = new StringBuilder();
            //sb.Append("using System;" + Environment.NewLine);
            //sb.Append("using System.Reflection;" + Environment.NewLine);
            //sb.Append("using System.Xml;" + Environment.NewLine);
            //sb.Append("using " + Settings.Default.Namespace + ".Support.Model;" + Environment.NewLine);
            //sb.Append("" + Environment.NewLine);
            //sb.Append("namespace " + Settings.Default.Namespace + ".Support" + Environment.NewLine + "{" + Environment.NewLine);
            //sb.Append("    //生成时间:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + Environment.NewLine);
            //sb.Append("    /// <summary>" + Environment.NewLine + "    /// 读取XML类" + Environment.NewLine + "    /// </summary>" + Environment.NewLine);
            //sb.Append("    public class XmlReader" + Environment.NewLine + "    {" + Environment.NewLine);
            //sb.Append(
            //    "        /// <summary>" + Environment.NewLine + 
            //    "        /// 从 XML 文件获取 SQL 命令" + Environment.NewLine + 
            //    "        /// </summary>" + Environment.NewLine + 
            //    "        /// <param name=\"type\">命令类型(如 SELECT/UPDATE/INSERT/DELETE 等)</param>" + Environment.NewLine + 
            //    "        /// <param name=\"node\">命令名称</param>" + Environment.NewLine +
            //    "        /// <param name=\"xmlFilePath\">作为来源的 xml 文件路径,为空则表示从 Model 中获取</param>" + Environment.NewLine + 
            //    "        /// <returns></returns>" + Environment.NewLine);
            //sb.Append("        public static string GetCommStr(string type, string node, string xmlFilePath = null)" + Environment.NewLine + "        {" + Environment.NewLine);
            //sb.Append("            try" + Environment.NewLine);
            //sb.Append("            {" + Environment.NewLine);
            //sb.Append("                if (!string.IsNullOrEmpty(xmlFilePath))" + Environment.NewLine);
            //sb.Append("                {//从给定的 xml 文件获取 SQL 命令" + Environment.NewLine);
            //sb.Append("                    XmlDocument doc = new XmlDocument();" + Environment.NewLine);
            //sb.Append("                    doc.Load(xmlFilePath);" + Environment.NewLine);
            //sb.Append("                    return doc.SelectSingleNode(\"SQL\").SelectSingleNode(type).SelectSingleNode(node).InnerText;" + Environment.NewLine);
            //sb.Append("                }" + Environment.NewLine);
            //sb.Append("                else" + Environment.NewLine);
            //sb.Append("                {//从 Model 获取 SQL 命令" + Environment.NewLine);
            //sb.Append("                    return GetCommStr1(type, node);" + Environment.NewLine);
            //sb.Append("                }" + Environment.NewLine);
            //sb.Append("            }" + Environment.NewLine);
            //sb.Append("            catch (Exception ex)" + Environment.NewLine);
            //sb.Append("            {" + Environment.NewLine);
            //sb.Append("                throw ex;" + Environment.NewLine);
            //sb.Append("            }" + Environment.NewLine);
            //sb.Append("        }" + Environment.NewLine);
            //sb.Append("" + Environment.NewLine);
            //sb.Append("" + Environment.NewLine);
            //sb.Append(
            //    "        /// <summary>" + Environment.NewLine +
            //    "        /// 从 Model 获取 SQL 命令" + Environment.NewLine +
            //    "        /// </summary>" + Environment.NewLine +
            //    "        /// <param name=\"type\">命令类型(如 SELECT/UPDATE/INSERT/DELETE 等)</param>" + Environment.NewLine +
            //    "        /// <param name=\"node\">命令名称</param>" + Environment.NewLine +
            //    "        /// <returns></returns>" + Environment.NewLine);
            //sb.Append("        public static string GetCommStr1(string type, string node)" + Environment.NewLine);
            //sb.Append("        {//通过 反射 获取属性值" + Environment.NewLine);
            //sb.Append("            switch (type.ToUpper())" + Environment.NewLine);
            //sb.Append("            {" + Environment.NewLine);
            //sb.Append("                case \"SELECT\":" + Environment.NewLine);
            //sb.Append("                {" + Environment.NewLine);
            //sb.Append("                    SELECT select = new SELECT();" + Environment.NewLine);
            //sb.Append("                    Type t = select.GetType();//获取类型" + Environment.NewLine);
            //sb.Append("                    PropertyInfo pInfo = t.GetProperty(node);//获取指定名称的属性" + Environment.NewLine);
            //sb.Append("                    return (string)pInfo.GetValue(select, null);//返回属性值" + Environment.NewLine);
            //sb.Append("                }" + Environment.NewLine);
            //sb.Append("                case \"UPDATE\":" + Environment.NewLine);
            //sb.Append("                {" + Environment.NewLine);
            //sb.Append("                    UPDATE update = new UPDATE();" + Environment.NewLine);
            //sb.Append("                    Type t = update.GetType();//获取类型" + Environment.NewLine);
            //sb.Append("                    PropertyInfo pInfo = t.GetProperty(node);//获取指定名称的属性" + Environment.NewLine);
            //sb.Append("                    return (string)pInfo.GetValue(update, null);//返回属性值" + Environment.NewLine);
            //sb.Append("                }" + Environment.NewLine);
            //sb.Append("                case \"DELETE\":" + Environment.NewLine);
            //sb.Append("                {" + Environment.NewLine);
            //sb.Append("                    DELETE delete = new DELETE();" + Environment.NewLine);
            //sb.Append("                    Type t = delete.GetType();//获取类型" + Environment.NewLine);
            //sb.Append("                    PropertyInfo pInfo = t.GetProperty(node);//获取指定名称的属性" + Environment.NewLine);
            //sb.Append("                    return (string)pInfo.GetValue(delete, null);//返回属性值" + Environment.NewLine);
            //sb.Append("                }" + Environment.NewLine);
            //sb.Append("                case \"INSERT\":" + Environment.NewLine);
            //sb.Append("                {" + Environment.NewLine);
            //sb.Append("                    INSERT insert = new INSERT();" + Environment.NewLine);
            //sb.Append("                    Type t = insert.GetType();//获取类型" + Environment.NewLine);
            //sb.Append("                    PropertyInfo pInfo = t.GetProperty(node);//获取指定名称的属性" + Environment.NewLine);
            //sb.Append("                    return (string)pInfo.GetValue(insert, null);//返回属性值" + Environment.NewLine);
            //sb.Append("                }" + Environment.NewLine);
            //sb.Append("                default:" + Environment.NewLine);
            //sb.Append("                {" + Environment.NewLine);
            //sb.Append("                    return \"\";" + Environment.NewLine);
            //sb.Append("                }" + Environment.NewLine);
            //sb.Append("                " + Environment.NewLine);
            //sb.Append("            }" + Environment.NewLine);
            //sb.Append("        }" + Environment.NewLine);
            //sb.Append("    }" + Environment.NewLine);
            //sb.Append("}" + Environment.NewLine);

            //return sb.ToString();
        }
    }
}
