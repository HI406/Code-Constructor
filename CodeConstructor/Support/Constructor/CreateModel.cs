using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using CodeConstructor.Properties;

namespace CodeConstructor.Support.Constructor
{
    /// <summary>
    /// 生成 Model 类
    /// </summary>
    public class CreateModel
    {
        public CreateModel() { }

        public string GenerateModel(DataTable columns, string tableType = "U")
        {
            if (columns.Rows.Count > 0)
            {
                string xmlName = Application.StartupPath.ToString().Replace("\\bin\\Debug", "\\Support\\Constructor") + "\\Template.xml";
                string codeStr = string.Empty;
                string tableName = columns.Rows[0]["tableName"].ToString();
                string varStr = string.Empty;
                string setGetStr = string.Empty;
                foreach (DataRow column in columns.Rows)
                {
                    string type = ConvertType.SqlTypeString2CsharpTypeString(column["sqlType"].ToString());
                    string temp = XmlReader.GetStr(xmlName, "Template", "Model", "Var");
                    string variable = CheckKeywords.InCSharp(column["name"].ToString());
                    string defaultValue = string.IsNullOrEmpty(column["defaultValue"].ToString()) ? "" : column["defaultValue"].ToString().TrimStart('(').TrimEnd(')');
                    if (defaultValue.ToLower().Contains("getdate"))
                    {
                        defaultValue = "DateTime.Now";
                        if (!column["sqlType"].ToString().Contains("date"))
                        {
                            defaultValue = "DateTime.Now.ToString(\"yyyy-MM-dd HH:mm:ss.fff\")";
                        }
                    }
                    else if (defaultValue.ToLower().Contains("newid"))
                    {
                        defaultValue = "Guid.NewGuid()";
                    }
                    else if (column["sqlType"].ToString().Contains("date") && column["allowNull"].ToString() == "1")
                    {
                        type += "?";
                        defaultValue = "null";
                    }
                    else
                    {
                        defaultValue = defaultValue.Replace("'", "\"");
                    }

                    varStr += string.Format(temp, type, string.IsNullOrEmpty(defaultValue) ? variable : variable + " = " + defaultValue);
                    temp = XmlReader.GetStr(xmlName, "Template", "Model", "SetGet");
                    setGetStr += string.Format(temp, variable + " " + column["description"].ToString(), type, variable);
                }
                string bodyCodeStr = varStr + setGetStr;

                string tableDescription = columns.Rows[0]["tableDescription"].ToString();
                codeStr += string.Format(XmlReader.GetStr(xmlName, "Template", "Model", "Head"), Settings.Default.Namespace, tableName + " " + tableDescription, tableName, bodyCodeStr, DateTime.Now);
                return codeStr.Replace("@", "{").Replace("$", "}").Replace("\n            ", "\n").Trim();
            }
            else
            {
                return "";
            }
        }

        public string GenerateCollection(string tableName, string tableDescription = "")
        {
            string xmlName = Application.StartupPath.ToString().Replace("\\bin\\Debug", "\\Support\\Constructor") + "\\Template.xml";
            string codeStr = string.Empty;
            codeStr += string.Format(XmlReader.GetStr(xmlName, "Template", "Collection", "Head"), Settings.Default.Namespace, tableName, tableDescription, DateTime.Now.ToString());
            return codeStr.Replace("@", "{").Replace("$", "}").Replace("\n            ", "\n").Trim();
        }


        public string GenerateQueryXmlModel(string queryXmlPath)
        {
            string xmlName = Application.StartupPath.ToString().Replace("\\bin\\Debug", "\\Support\\Constructor") + "\\Template.xml";
            string codeStr = XmlReader.GetStr(xmlName, "Template", "QueryModel", "Head");
            string bodyStr = "";

            XmlDocument doc = new XmlDocument();
            doc.Load(queryXmlPath);
            XmlNodeList xNodes = doc.SelectSingleNode("SQL").ChildNodes;
            foreach (XmlNode xNode in xNodes)
            {
                if (xNode.Name == "#comment" && xNode.Name == "Config") continue;
                string tempBodyStr = "";
                string t0 = string.Empty;
                string t1 = string.Empty;
                string t2 = string.Empty;
                foreach (XmlNode childNode in xNode.ChildNodes)
                {
                    if (childNode.Name != "#comment")
                    {
                        t1 = childNode.Name;
                        t2 = childNode.InnerText.Trim();
                    }
                    else
                    {
                        t0 = childNode.Value;
                    }
                    if (string.IsNullOrEmpty(t1)) continue;
                    string ksdj = XmlReader.GetStr(xmlName, "Template", "QueryModel", "Declare");
                    tempBodyStr += string.Format(XmlReader.GetStr(xmlName, "Template", "QueryModel", "Declare"), t0, t1, t2);
                }
                if(string.IsNullOrEmpty(tempBodyStr)) continue;
                bodyStr += string.Format(XmlReader.GetStr(xmlName, "Template", "QueryModel", "Body"), DateTime.Now, xNode.Name, tempBodyStr);
            }
            return string.Format(codeStr, bodyStr).Replace("@", "{").Replace("$", "}").Replace("[at]","@").Trim();

            StringBuilder sb = new StringBuilder();
            sb.Append("using System;" + Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("namespace " + Settings.Default.Namespace + ".Support.Model" + Environment.NewLine);
            sb.Append("{" + Environment.NewLine);

            foreach (XmlNode xNode in xNodes)
            {
                if (xNode.Name != "#comment" && xNode.Name != "Config")
                {
                    sb.Append("    //生成时间:" + DateTime.Now.ToString() + Environment.NewLine);
                    sb.Append("    /// <summary> " + Environment.NewLine + "    /// 数据库 " + xNode.Name + " 实体类 " + Environment.NewLine + "    /// </summary> " + Environment.NewLine + "    [Serializable]" + Environment.NewLine);
                    sb.Append("    public class " + xNode.Name + Environment.NewLine);
                    sb.Append("    {" + Environment.NewLine);
                    sb.Append("        public " + xNode.Name + "()" + Environment.NewLine + "        {}" + Environment.NewLine);
                    sb.Append("        #region " + xNode.Name + " Model" + Environment.NewLine);
                    foreach (XmlNode childNode in xNode.ChildNodes)
                    {
                        if (childNode.Name != "#comment")
                        {
                            sb.Append("        private static string _" + childNode.Name + " = @\"" + childNode.InnerText + "\";" + Environment.NewLine);
                        }
                        else
                        {
                            sb.Append(Environment.NewLine + "        /*↓↓↓" + childNode.Value + "↓↓↓*/" + Environment.NewLine);
                        }
                    }
                    foreach (XmlNode childNode in xNode.ChildNodes)
                    {
                        if (childNode.ParentNode.Name != "Export")
                        {
                            if (childNode.Name != "#comment")
                            {
                                sb.Append("        " + childNode.Name + " " + Environment.NewLine + "        /// </summary> " + Environment.NewLine);
                                sb.Append("        public static string " + childNode.Name + Environment.NewLine);
                                sb.Append("        {" + Environment.NewLine);
                                sb.Append("            get { return _" + childNode.Name + "; }" + Environment.NewLine);
                                sb.Append(Environment.NewLine);
                                sb.Append("        }" + Environment.NewLine);
                            }
                            else
                            {
                                sb.Append("        /// <summary>" + Environment.NewLine + "        /// " + childNode.Value + " ");
                            }
                        }
                        else
                        {
                            if (childNode.Name != "#comment")
                            {
                                sb.Append("        /// <summary>" + Environment.NewLine + "        /// " + childNode.Name + " " + Environment.NewLine + "        /// </summary> " + Environment.NewLine);
                                sb.Append("        public static string " + childNode.Name + Environment.NewLine);
                                sb.Append("        {" + Environment.NewLine);
                                sb.Append("            get { return _" + childNode.Name + "; }" + Environment.NewLine);
                                sb.Append(Environment.NewLine);
                                sb.Append("        }" + Environment.NewLine);
                            }
                        }
                    }
                    sb.Append("        #endregion " + xNode.Name + " Model" + Environment.NewLine);
                    sb.Append("    }" + Environment.NewLine);
                }
            }
            sb.Append("}" + Environment.NewLine + Environment.NewLine);

            return sb.ToString();
        }
    }
}
