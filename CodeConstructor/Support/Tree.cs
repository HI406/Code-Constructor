using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CodeConstructor.Properties;

namespace CodeConstructor.Support
{
    public class Tree
    {
        public static TreeNode[] Bind()
        {
            List<TreeNode> nodes = new List<TreeNode>();
            
            string dbName = Settings.Default.connectionStrings.Split(';')[1].Split('=')[1];
            TreeNode tn_database = new TreeNode(dbName, 0, 0);
            tn_database.Tag = "database";
            tn_database.Text = Settings.Default.InitialCatalog;
            tn_database.Nodes.Add(GetNode("U"));
            tn_database.Nodes.Add(GetNode("V"));
            tn_database.Expand();

            nodes.Add(tn_database);
            
            return nodes.ToArray();
        }

        private static TreeNode GetNode(string xtype)
        {
            TreeNode tn = new TreeNode();
            DataTable dt_table = new DataTable();
            switch (xtype)
            {
                case "U":
                    dt_table = Support.DataBaseHandler.Select.GetDBInfo("GetAllTable");
                    tn.Text = "Tables";
                    tn.Tag = "tables";
                    tn.ImageIndex = 3;
                    tn.SelectedImageIndex = 3;
                    break;
                case "V":
                    dt_table = Support.DataBaseHandler.Select.GetDBInfo("GetAllView");
                    tn.Text = "Views";
                    tn.Tag = "views";
                    tn.ImageIndex = 4;
                    tn.SelectedImageIndex = 4;
                    break;
            }
            foreach (DataRow table in dt_table.Rows)
            {
                TreeNode tn_table = new TreeNode(table["name"].ToString(), 1, 1);
                tn_table.Tag = "table";
                DataTable columns = Support.DataBaseHandler.Select.GetColumnInfo(xtype, table["name"].ToString());
                foreach (DataRow column in columns.Rows)
                {
                    string tn_column_text = column["name"].ToString();
                    tn_column_text += string.IsNullOrEmpty(column["sqlType"].ToString()) ? "" : ("(" + column["sqlType"].ToString() + (string.IsNullOrEmpty(column["length"].ToString()) ? ")" : "," + column["length"].ToString() + ")"));
                    TreeNode tn_column = new TreeNode(tn_column_text, 2, 2);
                    tn_column.Tag = "column";
                    tn_table.Nodes.Add(tn_column);
                }
                tn_table.Text += "(" + tn_table.Nodes.Count + ")";
                tn.Nodes.Add(tn_table);
            }
            tn.Expand();
            tn.Text += "(" + tn.Nodes.Count + ")";
            return tn;
        }
    }
}
