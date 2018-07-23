using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CodeConstructor.Properties;
using CodeConstructor.Support;

namespace CodeConstructor.Forms
{
    public partial class Main : Form
    {
        private bool isLoaded = false;
        private bool canCtrl_S = false;
        private string currentOperate = string.Empty;
        public Main()
        {
            InitializeComponent();
        }

        private void InitializeData()
        {
            DateTime timeStart = DateTime.Now;
            try
            {
                this.toolStripTextBox_namespace.Text = Settings.Default.Namespace;
                this.treeView_dbInfo.Nodes.Clear();

                this.treeView_dbInfo.Nodes.AddRange(Support.Tree.Bind());
                this.toolStripStatusLabel_statusInfo.Text = "Initialize data done.Time spend " + Time.TimeSpend.Spend(timeStart);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message:\r\n" + ex.Message, "InitializeData Error");
            }
            finally
            {
                
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            InitializeData();
            isLoaded = true;
        }

        private void bb_Click(object sender, EventArgs e)
        {
            DataBaseSet dbconn = new DataBaseSet();
            if(dbconn.ShowDialog() == DialogResult.OK)
            {
                InitializeData();
            }
        }

        private void treeView_dbInfo_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!isLoaded)
            {
                return;
            }
            toolStripStatusLabel_selectedNode.Text = e.Node.Text;
            Support.Model.Column c = new Support.Model.Column();
            bool operateEnable = this.treeView_dbInfo.SelectedNode.Tag.ToString() == "database";
            //this.toolStripButton_createModel.Enabled = this.treeView_dbInfo.SelectedNode.Tag.ToString() == "table";
            this.toolStripButton_createSelect.Enabled = operateEnable;
            this.toolStripButton_createUpdate.Enabled = operateEnable || this.treeView_dbInfo.SelectedNode.Tag.ToString() == "tables";
            this.toolStripButton_createInsert.Enabled = operateEnable || this.treeView_dbInfo.SelectedNode.Tag.ToString() == "tables";
            this.toolStripButton_createDelete.Enabled = operateEnable || this.treeView_dbInfo.SelectedNode.Tag.ToString() == "tables";
            this.toolStripButton_CreateXml.Enabled = operateEnable;
            this.toolStripButton_createQueryModel.Enabled = operateEnable;
            this.textBox_codeZone.Clear();
            switch(this.treeView_dbInfo.SelectedNode.Tag.ToString())
            {
                case "table":
                    break;
                case "column":
                    this.textBox_codeZone.Clear();
                    string tableType = this.treeView_dbInfo.SelectedNode.Parent.Parent.Tag.ToString().Substring(0, 1).ToUpper();
                    tableType = tableType != "V" ? "U" : "V";
                    DataTable table = Support.DataBaseHandler.Select.GetColumnInfo(tableType, null);
                    foreach (DataRow dr in table.Select("tableName = '" + this.treeView_dbInfo.SelectedNode.Parent.Text.Split('(')[0] + "' and name = '" + this.treeView_dbInfo.SelectedNode.Text.Split('(')[0] + "'"))
                    {
                        c.Initialize(dr, tableType);
                        for(int i=0 ;i<dr.ItemArray.Length ; i++)
                        {
                            this.textBox_codeZone.AppendText("[" + i.ToString("00") + "] " + (new TextTableHelper()).PaddingRight(dr.Table.Columns[i].ToString(),20,'.') + ": " + dr.ItemArray[i].ToString() + "\r\n");
                        }
                    }
                    break;
                default:
                    break;
            }

            this.toolStripStatusLabel_statusInfo.Text = "Ready";
        }

        private void toolStripTextBox_namespace_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.Namespace = this.toolStripTextBox_namespace.Text.Trim();
            Settings.Default.Save();
        }

        private void toolStripButton_createModel_Click(object sender, EventArgs e)
        {
            if (this.treeView_dbInfo.SelectedNode == null)
            {
                return;
            }
            DateTime timeStart = DateTime.Now;
            Support.Constructor.CreateModel cm = new Support.Constructor.CreateModel();
            string nodeTag = this.treeView_dbInfo.SelectedNode.Tag.ToString();
            if (nodeTag == "database")
            {
                canCtrl_S = false;
                this.textBox_codeZone.Clear();
                foreach (TreeNode tn_type in this.treeView_dbInfo.SelectedNode.Nodes)
                {
                    string tableType = tn_type.Tag.ToString().Substring(0, 1).ToUpper();
                    tableType = tableType != "V" ? "U" : "V";
                    this.textBox_codeZone.AppendText(tableType != "V" ? "Creating user table models...\r\n" : "Creating user view models...\r\n");
                    for (int i = 0; i < tn_type.Nodes.Count; i++)
                    {
                        TreeNode tn = tn_type.Nodes[i];
                        (new Support.Constructor.CreateCSFile()).Write(cm.GenerateModel(Support.DataBaseHandler.Select.GetColumnInfo(tableType, tn.Text.Split('(')[0])), Settings.Default.CreateFilePath + "\\Model", tn.Text.Split('(')[0] + ".cs");
                        this.textBox_codeZone.AppendText("  [" + i.ToString("000") + "] " + "Successfully created model file: " + Settings.Default.CreateFilePath + "\\Model\\" + tn.Text.Split('(')[0] + ".cs\r\n");
                    }
                    this.textBox_codeZone.AppendText("Done.\r\n");
                }
            }
            else if (nodeTag == "tables" || nodeTag == "views")
            {
                canCtrl_S = false;
                string tableType = nodeTag.Substring(0, 1).ToUpper();
                tableType = tableType != "V" ? "U" : "V";
                this.textBox_codeZone.Clear();
                this.textBox_codeZone.AppendText(tableType != "V" ? "Creating user table models...\r\n" : "Creating user view models...\r\n");
                for (int i = 0; i < this.treeView_dbInfo.SelectedNode.Nodes.Count; i++)
                {
                    TreeNode tn = this.treeView_dbInfo.SelectedNode.Nodes[i];
                    (new Support.Constructor.CreateCSFile()).Write(cm.GenerateModel(Support.DataBaseHandler.Select.GetColumnInfo(tableType, tn.Text.Split('(')[0])), Settings.Default.CreateFilePath + "\\Model", tn.Text.Split('(')[0] + ".cs");
                    this.textBox_codeZone.AppendText("  [" + i.ToString("000") + "] " + "Successfully created model file: " + Settings.Default.CreateFilePath + "\\Model\\" + tn.Text.Split('(')[0] + ".cs\r\n");
                }
                this.textBox_codeZone.AppendText("Done.\r\n");
            }
            else if (nodeTag == "table")
            {
                canCtrl_S = true;
                string tableType = this.treeView_dbInfo.SelectedNode.Parent.Tag.ToString().Substring(0, 1).ToUpper();
                tableType = tableType != "V" ? "U" : "V";
                this.textBox_codeZone.Text = cm.GenerateModel(Support.DataBaseHandler.Select.GetColumnInfo(tableType, this.treeView_dbInfo.SelectedNode.Text.Split('(')[0]), tableType);
                if (this.toolStripButton_createFile.Tag.ToString() == "1")
                {
                    SaveFile(Settings.Default.CreateFilePath + "\\Model", this.treeView_dbInfo.SelectedNode.Text.Split('(')[0] + ".cs");
                }
            }
            currentOperate = "model";
            this.toolStripStatusLabel_statusInfo.Text = "Time spend " + Time.TimeSpend.Spend(timeStart);
        }


        private void toolStripButton_createCollection_Click(object sender, EventArgs e)
        {
            if (this.treeView_dbInfo.SelectedNode == null)
            {
                return;
            }
            DateTime timeStart = DateTime.Now;
            Support.Constructor.CreateModel cm = new Support.Constructor.CreateModel();
            string nodeTag = this.treeView_dbInfo.SelectedNode.Tag.ToString();
            if (nodeTag == "database")
            {
                canCtrl_S = false;
                this.textBox_codeZone.Clear();
                foreach (TreeNode tn_type in this.treeView_dbInfo.SelectedNode.Nodes)
                {
                    string tableType = tn_type.Tag.ToString().Substring(0, 1).ToUpper();
                    tableType = tableType != "V" ? "U" : "V";
                    this.textBox_codeZone.AppendText(tableType != "V" ? "Creating user table models...\r\n" : "Creating user view models...\r\n");
                    for (int i = 0; i < tn_type.Nodes.Count; i++)
                    {
                        TreeNode tn = tn_type.Nodes[i];
                        (new Support.Constructor.CreateCSFile()).Write(cm.GenerateCollection(tn.Text.Split('(')[0]), Settings.Default.CreateFilePath + "\\Model", tn.Text.Split('(')[0] + "Collection.cs");
                        this.textBox_codeZone.AppendText("  [" + i.ToString("000") + "] " + "Successfully created model file: " + Settings.Default.CreateFilePath + "\\Model\\" + tn.Text.Split('(')[0] + "Collection.cs\r\n");
                    }
                    this.textBox_codeZone.AppendText("Done.\r\n");
                }
            }
            else if (nodeTag == "tables" || nodeTag == "views")
            {
                canCtrl_S = false;
                string tableType = nodeTag.Substring(0, 1).ToUpper();
                tableType = tableType != "V" ? "U" : "V";
                this.textBox_codeZone.Clear();
                this.textBox_codeZone.AppendText(tableType != "V" ? "Creating user table models...\r\n" : "Creating user view models...\r\n");
                for (int i = 0; i < this.treeView_dbInfo.SelectedNode.Nodes.Count; i++)
                {
                    TreeNode tn = this.treeView_dbInfo.SelectedNode.Nodes[i];
                    (new Support.Constructor.CreateCSFile()).Write(cm.GenerateCollection(tn.Text.Split('(')[0]), Settings.Default.CreateFilePath + "\\Model", tn.Text.Split('(')[0] + "Collection.cs");
                    this.textBox_codeZone.AppendText("  [" + i.ToString("000") + "] " + "Successfully created model file: " + Settings.Default.CreateFilePath + "\\Model\\" + tn.Text.Split('(')[0] + "Collection.cs\r\n");
                }
                this.textBox_codeZone.AppendText("Done.\r\n");
            }
            else if (nodeTag == "table")
            {
                canCtrl_S = true;
                string tableType = this.treeView_dbInfo.SelectedNode.Parent.Tag.ToString().Substring(0, 1).ToUpper();
                tableType = tableType != "V" ? "U" : "V";
                this.textBox_codeZone.Text = cm.GenerateCollection(this.treeView_dbInfo.SelectedNode.Text.Split('(')[0]);
                if (this.toolStripButton_createFile.Tag.ToString() == "1")
                {
                    SaveFile(Settings.Default.CreateFilePath + "\\Model", this.treeView_dbInfo.SelectedNode.Text.Split('(')[0] + "Collection.cs");
                }
            }
            currentOperate = "collection";
            this.toolStripStatusLabel_statusInfo.Text = "Time spend " + Time.TimeSpend.Spend(timeStart);
        }

        private void toolStripButton_createSelect_Click(object sender, EventArgs e)
        {
            if (this.treeView_dbInfo.SelectedNode == null)
            {
                return;
            }
            canCtrl_S = true;
            DateTime timeStart = DateTime.Now;
            Support.Constructor.CreateSelect cs = new Support.Constructor.CreateSelect();
            this.textBox_codeZone.Text = cs.GenerateSelect(Support.DataBaseHandler.Select.GetDBInfo("GetAllTableAndView"));
            if (this.toolStripButton_createFile.Tag.ToString() == "1")
            {
                SaveFile(Settings.Default.CreateFilePath + "\\SQL", "Select.cs");
            }
            currentOperate = "select";
            this.toolStripStatusLabel_statusInfo.Text = "Time spend " + Time.TimeSpend.Spend(timeStart);
        }

        private void toolStripButton_createUpdate_Click(object sender, EventArgs e)
        {
            if (this.treeView_dbInfo.SelectedNode == null)
            {
                return;
            }
            canCtrl_S = true;
            DateTime timeStart = DateTime.Now;
            Support.Constructor.CreateUpdate cu = new Support.Constructor.CreateUpdate();
            this.textBox_codeZone.Text = cu.GenerateUpdate(Support.DataBaseHandler.Select.GetDBInfo("GetAllTable"));
            if (this.toolStripButton_createFile.Tag.ToString() == "1")
            {
                SaveFile(Settings.Default.CreateFilePath + "\\SQL", "Update.cs");
            }
            currentOperate = "update";
            this.toolStripStatusLabel_statusInfo.Text = "Time spend " + Time.TimeSpend.Spend(timeStart);
        }

        private void toolStripButton_createInsert_Click(object sender, EventArgs e)
        {
            if (this.treeView_dbInfo.SelectedNode == null)
            {
                return;
            }
            canCtrl_S = true;
            DateTime timeStart = DateTime.Now;
            Support.Constructor.CreateInsert ci = new Support.Constructor.CreateInsert();
            this.textBox_codeZone.Text = ci.GenerateInsert(Support.DataBaseHandler.Select.GetDBInfo("GetAllTable"));
            if (this.toolStripButton_createFile.Tag.ToString() == "1")
            {
                SaveFile(Settings.Default.CreateFilePath + "\\SQL", "Insert.cs");
            }
            currentOperate = "insert";
            this.toolStripStatusLabel_statusInfo.Text = "Time spend " + Time.TimeSpend.Spend(timeStart);
        }

        private void toolStripButton_createDelete_Click(object sender, EventArgs e)
        {
            if (this.treeView_dbInfo.SelectedNode == null)
            {
                return;
            }
            canCtrl_S = true;
            DateTime timeStart = DateTime.Now;
            Support.Constructor.CreateDelete cd = new Support.Constructor.CreateDelete();
            this.textBox_codeZone.Text = cd.GenerateDelete(Support.DataBaseHandler.Select.GetDBInfo("GetAllTable"));
            if (this.toolStripButton_createFile.Tag.ToString() == "1")
            {
                SaveFile(Settings.Default.CreateFilePath + "\\SQL", "Delete.cs");
            }
            currentOperate = "delete";
            this.toolStripStatusLabel_statusInfo.Text = "Time spend " + Time.TimeSpend.Spend(timeStart);
        }

        private void toolStripButton_CreateXml_Click(object sender, EventArgs e)
        {
            if (this.treeView_dbInfo.SelectedNode == null)
            {
                return;
            }
            canCtrl_S = true;
            DateTime timeStart = DateTime.Now;
            Support.Constructor.CreateXml cx = new Support.Constructor.CreateXml();
            this.textBox_codeZone.Text = cx.GenerateQueryXml(Support.DataBaseHandler.Select.GetDBInfo("GetAllTableAndView"));
            if (this.toolStripButton_createFile.Tag.ToString() == "1")
            {
                SaveFile(Settings.Default.CreateFilePath + "\\SQL", "Query.xml");
            }
            currentOperate = "xml";

            this.toolStripStatusLabel_statusInfo.Text = "Time spend " + Time.TimeSpend.Spend(timeStart);
        }

        private void toolStripButton_createQueryModel_Click(object sender, EventArgs e)
        {
            if (this.treeView_dbInfo.SelectedNode == null)
            {
                return;
            }
            else if (!File.Exists(Settings.Default.CreateFilePath + "\\SQL\\Query.xml"))
            {
                toolStripButton_CreateXml_Click(sender, e);
            }
            canCtrl_S = true;
            DateTime timeStart = DateTime.Now;
            Support.Constructor.CreateModel cm = new Support.Constructor.CreateModel();
            this.textBox_codeZone.Text = cm.GenerateQueryXmlModel(Settings.Default.CreateFilePath + "\\SQL\\Query.xml");
            if (this.toolStripButton_createFile.Tag.ToString() == "1")
            {
                SaveFile(Settings.Default.CreateFilePath + "\\Model", "Query.cs");
            }
            currentOperate = "xmlModel";

            this.toolStripStatusLabel_statusInfo.Text = "Time spend " + Time.TimeSpend.Spend(timeStart);
        }


        private void toolStripButton_xmlReader_Click(object sender, EventArgs e)
        {
            canCtrl_S = true;
            DateTime timeStart = DateTime.Now;
            Support.Constructor.CreateXml cx = new Support.Constructor.CreateXml();
            this.textBox_codeZone.Text = cx.GenerateXmlReader();
            if (this.toolStripButton_createFile.Tag.ToString() == "1")
            {
                SaveFile(Settings.Default.CreateFilePath, "XmlReader.xml");
            }
            currentOperate = "xmlReader";

            this.toolStripStatusLabel_statusInfo.Text = "Time spend " + Time.TimeSpend.Spend(timeStart);
        }

        private void textBox_codeZone_KeyDown(object sender, KeyEventArgs e)
        {
            if(this.textBox_codeZone.Text.Length > 0 && e.Modifiers == Keys.Control)
            {
                switch(e.KeyCode)
                {
                    case Keys.A:
                        ((TextBox)sender).SelectAll();
                        break;
                    case Keys.S:
                        {
                            if (canCtrl_S)
                            {
                                string file = SaveFile();
                                FileInfo fi = new FileInfo(file);
                                Clipboard.SetDataObject(fi.DirectoryName);
                                if (MessageBox.Show("Successfully created file:\r\n" + file + "\r\nOpen the directory?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                                {
                                    Process.Start("explorer.exe", @"/select," + file);
                                }
                            }
                        }
                        break;
                }
            }
        }

        private void toolStripButton_createFile_Click(object sender, EventArgs e)
        {
            bool isCreateFile = this.toolStripButton_createFile.Tag.ToString() == "1";
            this.toolStripButton_createFile.Image = isCreateFile ? global::CodeConstructor.Properties.Resources.CheckboxUncheck_16x : global::CodeConstructor.Properties.Resources.CheckBox_16x;
            this.toolStripButton_createFile.Tag = isCreateFile ? "0":"1";
            //this.toolStripButton_createModel.Enabled = !isCreateFile;// && this.treeView_dbInfo.SelectedNode.Tag.ToString() == "database";
        }

        private void toolStripMenuItem_options_Click(object sender, EventArgs e)
        {
            Options options = new Options();
            if (options.ShowDialog() == DialogResult.OK)
            {
                
            }
        }

        private string SaveFile(string filePath = null, string fileName = null)
        {
            if (string.IsNullOrEmpty(filePath) && string.IsNullOrEmpty(fileName))
            {
                filePath = Settings.Default.CreateFilePath + "\\SQL";
                switch (currentOperate)
                {
                    case "model":
                        filePath = Settings.Default.CreateFilePath + "\\Model";
                        fileName = this.treeView_dbInfo.SelectedNode.Text.Split('(')[0] + ".cs";
                        break;
                    case "select":
                        fileName = "Select.cs";
                        break;
                    case "update":
                        fileName = "Update.cs";
                        break;
                    case "insert":
                        fileName = "Insert.cs";
                        break;
                    case "delete":
                        fileName = "Delete.cs";
                        break;
                    case "xml":
                        fileName = "Query.xml";
                        break;
                    case "collection":
                        filePath = Settings.Default.CreateFilePath + "\\Model";
                        fileName = this.treeView_dbInfo.SelectedNode.Text.Split('(')[0] + "Collection.cs";
                        break;
                    case "xmlModel":
                        filePath = Settings.Default.CreateFilePath + "\\Model";
                        fileName = "Query.cs";
                        break;
                    case "xmlReader":
                        filePath = Settings.Default.CreateFilePath;
                        fileName = "xmlReader.cs";
                        break;
                }
            }
            (new Support.Constructor.CreateCSFile()).Write(this.textBox_codeZone.Text, filePath, fileName);
            return filePath+"\\"+fileName;
        }

    }
}
