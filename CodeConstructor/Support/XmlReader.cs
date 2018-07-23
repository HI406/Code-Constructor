using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using CodeConstructor.Properties;

namespace CodeConstructor.Support
{
    /// <summary>
    /// 读取XML类
    /// </summary>
    public class XmlReader
    {
        /// <summary>
        /// 从 XML 文件获取 SQL 命令
        /// </summary>
        /// <param name="type">命令类型</param>
        /// <param name="node">命令名称</param>
        /// <returns></returns>
        public static string GetCommStr(string type, string node)
        {
            try
            {
                if (Settings.Default.IsTest)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(Application.StartupPath.ToString().Replace("\\bin\\Debug", "\\Support\\DataBaseHandler") + "\\Query.xml");
                    return doc.SelectSingleNode("SQL").SelectSingleNode(type).SelectSingleNode(node).InnerText;
                }
                else
                {
                    //return GetCommStr1(type, node);
                    return "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 从 XML 读取数据
        /// </summary>
        /// <param name="xml">xml完整路径</param>
        /// <param name="root">根节点</param>
        /// <param name="type">类型</param>
        /// <param name="node">名称</param>
        /// <returns></returns>
        public static string GetStr(string xml, string root, string type, string node = null)
        {
            try
            {
                if(Settings.Default.IsTest)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(xml);
                    if(string.IsNullOrEmpty(node))
                    {
                        return doc.SelectSingleNode(root).SelectSingleNode(type).InnerText;
                    }
                    return doc.SelectSingleNode(root).SelectSingleNode(type).SelectSingleNode(node).InnerText;
                }
                else
                {
                    //return GetCommStr1(type, node);
                    return "";
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /*
        /// <summary>
        /// 获取 SQL 命令
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="node">名称</param>
        /// <returns></returns>
        public static string GetCommStr1(string type, string node)
        {
            //通过反射获取属性值
            try
            {
                switch (type.ToUpper())
                {
                    case "SELECT":
                        {
                            SELECT select = new SELECT();
                            Type t = select.GetType();//获取类型
                            PropertyInfo pInfo = t.GetProperty(node);//获取指定名称的属性
                            return (string)pInfo.GetValue(select, null);//返回属性值
                        }
                    case "UPDATE":
                        {
                            UPDATE update = new UPDATE();
                            Type t = update.GetType();
                            PropertyInfo pInfo = t.GetProperty(node);
                            return (string)pInfo.GetValue(update, null);
                        }
                    case "DELETE":
                        {
                            DELETE delete = new DELETE();
                            Type t = delete.GetType();
                            PropertyInfo pInfo = t.GetProperty(node);
                            return (string)pInfo.GetValue(delete, null);
                        }
                    case "INSERT":
                        {
                            INSERT insert = new INSERT();
                            Type t = insert.GetType();
                            PropertyInfo pInfo = t.GetProperty(node);
                            return (string)pInfo.GetValue(insert, null);
                        }
                    case "EXPORT":
                        {
                            Export export = new Export();
                            Type t = export.GetType();
                            PropertyInfo pInfo = t.GetProperty(node);
                            return (string)pInfo.GetValue(export, null);
                        }
                    default:
                        {
                            return "";
                        }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//*/

        public static bool SetCommStr(string type, string node, string value)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                string xmlPath = Application.StartupPath.ToString() + "\\Query.xml";
                //string xmlPath = Application.StartupPath.ToString().Replace("\\bin\\Debug", "\\Support\\SQL") + "\\Query.xml";
                doc.Load(xmlPath);
                XmlNode xn = doc.SelectSingleNode("SQL").SelectSingleNode(type).SelectSingleNode(node);
                xn.InnerText = value;
                doc.Save(xmlPath);



                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
