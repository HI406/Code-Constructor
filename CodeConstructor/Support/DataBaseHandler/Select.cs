using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using CodeConstructor.Properties;

namespace CodeConstructor.Support.DataBaseHandler
{
    public class Select
    {
        #region 获取数据库信息
        /// <summary>
        /// 获取数据库信息
        /// </summary>
        /// <param name="tag">要查询的信息</param>
        /// <returns></returns>
        public static DataTable GetDBInfo(string tag)
        {
            string commStr = XmlReader.GetCommStr("SELECT", tag);
            return Go(commStr);
        }
        #endregion

        #region 获取数据库信息
        /// <summary>
        /// 获取数据库信息
        /// </summary>
        /// <param name="tableName">数据表名</param>
        /// <returns></returns>
        public static DataTable GetColumnInfo(string tableName)
        {
            string commStr = XmlReader.GetCommStr("SELECT", "GetAllColumn");
            if(!string.IsNullOrEmpty(tableName))
            {
                tableName = " where d.name='" + tableName + "' ";
            }
            commStr = string.Format(commStr, tableName);
            return Go(commStr);
        }

        /// <summary>
        /// 获取数据库信息
        /// </summary>
        /// <param name="tableName">数据表名</param>
        /// <param name="tableType">数据表类型(U:用户表;V:视图)</param>
        /// <returns></returns>
        public static DataTable GetColumnInfo(string tableType, string tableName)
        {
            switch (tableType)
            {
                case "U":
                    return GetColumnInfo(tableName);
                case "V":
                    {
                        string commStr = XmlReader.GetCommStr("SELECT", "GetAllViewColumn");
                        if (!string.IsNullOrEmpty(tableName))
                        {
                            tableName = " where d.name='" + tableName + "' ";
                        }
                        commStr = string.Format(commStr, tableName);
                        return Go(commStr);
                    }
                default:
                    return null;
            }
        }
        #endregion

        #region 执行查询操作
        /// <summary>
        /// 执行查询操作
        /// </summary>
        /// <param name="commStr">指定的查询命令</param>
        /// <returns></returns>
        public static DataTable Go(string commStr)
        {
            SqlConnection conn = new SqlConnection(Settings.Default.connectionStrings);
            SqlDataAdapter sda = new SqlDataAdapter(commStr, conn);
            DataTable table = new DataTable();
            sda.Fill(table);
            return table;
        }
        #endregion

        #region 通用查询语句
        /// <summary>
        /// 通用查询
        /// </summary>
        /// <param name="column">要查询的列</param>
        /// <param name="table">要查询的表</param>
        /// <param name="filter">查询条件</param>
        /// <returns></returns>
        public static DataTable UniversalSelect(string column, string table, string filter)
        {
            string commStr = XmlReader.GetCommStr("SELECT", "UniversalSelect");
            commStr = string.Format(commStr, column, table, filter);
            return Go(commStr);
        }
        #endregion

    }
}
