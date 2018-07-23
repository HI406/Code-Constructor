namespace CodeConstructor.Forms
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_statusInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer_main = new System.Windows.Forms.SplitContainer();
            this.treeView_dbInfo = new System.Windows.Forms.TreeView();
            this.imageList_tree = new System.Windows.Forms.ImageList(this.components);
            this.textBox_codeZone = new System.Windows.Forms.TextBox();
            this.contextMenuStrip_textBox = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox_namespace = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton_xmlReader = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_createModel = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_createCollection = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_CreateXml = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_createQueryModel = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_createSelect = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_createUpdate = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_createInsert = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_createDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_createFile = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem_file = new System.Windows.Forms.ToolStripMenuItem();
            this.bb = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_tools = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_options = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripStatusLabel_selectedNode = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_main)).BeginInit();
            this.splitContainer_main.Panel1.SuspendLayout();
            this.splitContainer_main.Panel2.SuspendLayout();
            this.splitContainer_main.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_selectedNode,
            this.toolStripStatusLabel_statusInfo});
            this.statusStrip1.Location = new System.Drawing.Point(0, 609);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1303, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel_statusInfo
            // 
            this.toolStripStatusLabel_statusInfo.Name = "toolStripStatusLabel_statusInfo";
            this.toolStripStatusLabel_statusInfo.Size = new System.Drawing.Size(12, 17);
            this.toolStripStatusLabel_statusInfo.Text = " ";
            // 
            // splitContainer_main
            // 
            this.splitContainer_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_main.Location = new System.Drawing.Point(0, 25);
            this.splitContainer_main.Name = "splitContainer_main";
            // 
            // splitContainer_main.Panel1
            // 
            this.splitContainer_main.Panel1.Controls.Add(this.treeView_dbInfo);
            // 
            // splitContainer_main.Panel2
            // 
            this.splitContainer_main.Panel2.Controls.Add(this.textBox_codeZone);
            this.splitContainer_main.Panel2.Controls.Add(this.toolStrip1);
            this.splitContainer_main.Panel2MinSize = 1000;
            this.splitContainer_main.Size = new System.Drawing.Size(1303, 584);
            this.splitContainer_main.SplitterDistance = 257;
            this.splitContainer_main.TabIndex = 2;
            // 
            // treeView_dbInfo
            // 
            this.treeView_dbInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_dbInfo.ImageIndex = 0;
            this.treeView_dbInfo.ImageList = this.imageList_tree;
            this.treeView_dbInfo.Location = new System.Drawing.Point(0, 0);
            this.treeView_dbInfo.Name = "treeView_dbInfo";
            this.treeView_dbInfo.SelectedImageIndex = 0;
            this.treeView_dbInfo.Size = new System.Drawing.Size(257, 584);
            this.treeView_dbInfo.TabIndex = 0;
            this.treeView_dbInfo.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_dbInfo_AfterSelect);
            // 
            // imageList_tree
            // 
            this.imageList_tree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_tree.ImageStream")));
            this.imageList_tree.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList_tree.Images.SetKeyName(0, "Database_32x.png");
            this.imageList_tree.Images.SetKeyName(1, "Table_32x.png");
            this.imageList_tree.Images.SetKeyName(2, "Column_32x.png");
            this.imageList_tree.Images.SetKeyName(3, "TableGroup_16x.png");
            this.imageList_tree.Images.SetKeyName(4, "PerspectiveTableGroup_16x.png");
            // 
            // textBox_codeZone
            // 
            this.textBox_codeZone.ContextMenuStrip = this.contextMenuStrip_textBox;
            this.textBox_codeZone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_codeZone.Location = new System.Drawing.Point(0, 25);
            this.textBox_codeZone.Multiline = true;
            this.textBox_codeZone.Name = "textBox_codeZone";
            this.textBox_codeZone.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_codeZone.Size = new System.Drawing.Size(1042, 559);
            this.textBox_codeZone.TabIndex = 1;
            this.textBox_codeZone.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_codeZone_KeyDown);
            // 
            // contextMenuStrip_textBox
            // 
            this.contextMenuStrip_textBox.Name = "contextMenuStrip_textBox";
            this.contextMenuStrip_textBox.Size = new System.Drawing.Size(61, 4);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripTextBox_namespace,
            this.toolStripButton_xmlReader,
            this.toolStripButton_createModel,
            this.toolStripButton_createCollection,
            this.toolStripButton_CreateXml,
            this.toolStripButton_createQueryModel,
            this.toolStripButton_createSelect,
            this.toolStripButton_createUpdate,
            this.toolStripButton_createInsert,
            this.toolStripButton_createDelete,
            this.toolStripSeparator1,
            this.toolStripButton_createFile});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1042, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(77, 22);
            this.toolStripLabel1.Text = "namespace:";
            // 
            // toolStripTextBox_namespace
            // 
            this.toolStripTextBox_namespace.Name = "toolStripTextBox_namespace";
            this.toolStripTextBox_namespace.Size = new System.Drawing.Size(150, 25);
            this.toolStripTextBox_namespace.TextChanged += new System.EventHandler(this.toolStripTextBox_namespace_TextChanged);
            // 
            // toolStripButton_xmlReader
            // 
            this.toolStripButton_xmlReader.Image = global::CodeConstructor.Properties.Resources.SearchContract_16x;
            this.toolStripButton_xmlReader.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_xmlReader.Name = "toolStripButton_xmlReader";
            this.toolStripButton_xmlReader.Size = new System.Drawing.Size(96, 22);
            this.toolStripButton_xmlReader.Text = "Xml Reader";
            this.toolStripButton_xmlReader.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripButton_xmlReader.Click += new System.EventHandler(this.toolStripButton_xmlReader_Click);
            // 
            // toolStripButton_createModel
            // 
            this.toolStripButton_createModel.Image = global::CodeConstructor.Properties.Resources.Model3D_26x;
            this.toolStripButton_createModel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_createModel.Name = "toolStripButton_createModel";
            this.toolStripButton_createModel.Size = new System.Drawing.Size(66, 22);
            this.toolStripButton_createModel.Text = "Model";
            this.toolStripButton_createModel.Click += new System.EventHandler(this.toolStripButton_createModel_Click);
            // 
            // toolStripButton_createCollection
            // 
            this.toolStripButton_createCollection.Image = global::CodeConstructor.Properties.Resources.Collection_16x;
            this.toolStripButton_createCollection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_createCollection.Name = "toolStripButton_createCollection";
            this.toolStripButton_createCollection.Size = new System.Drawing.Size(85, 22);
            this.toolStripButton_createCollection.Text = "Collection";
            this.toolStripButton_createCollection.Click += new System.EventHandler(this.toolStripButton_createCollection_Click);
            // 
            // toolStripButton_CreateXml
            // 
            this.toolStripButton_CreateXml.Enabled = false;
            this.toolStripButton_CreateXml.Image = global::CodeConstructor.Properties.Resources.XMLFile_32x;
            this.toolStripButton_CreateXml.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_CreateXml.Name = "toolStripButton_CreateXml";
            this.toolStripButton_CreateXml.Size = new System.Drawing.Size(86, 22);
            this.toolStripButton_CreateXml.Text = "Query.xml";
            this.toolStripButton_CreateXml.Click += new System.EventHandler(this.toolStripButton_CreateXml_Click);
            // 
            // toolStripButton_createQueryModel
            // 
            this.toolStripButton_createQueryModel.Enabled = false;
            this.toolStripButton_createQueryModel.Image = global::CodeConstructor.Properties.Resources.XMLFile_32x;
            this.toolStripButton_createQueryModel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_createQueryModel.Name = "toolStripButton_createQueryModel";
            this.toolStripButton_createQueryModel.Size = new System.Drawing.Size(105, 22);
            this.toolStripButton_createQueryModel.Text = "Query Model";
            this.toolStripButton_createQueryModel.Click += new System.EventHandler(this.toolStripButton_createQueryModel_Click);
            // 
            // toolStripButton_createSelect
            // 
            this.toolStripButton_createSelect.Enabled = false;
            this.toolStripButton_createSelect.Image = global::CodeConstructor.Properties.Resources.Select_16x;
            this.toolStripButton_createSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_createSelect.Name = "toolStripButton_createSelect";
            this.toolStripButton_createSelect.Size = new System.Drawing.Size(62, 22);
            this.toolStripButton_createSelect.Text = "Select";
            this.toolStripButton_createSelect.Click += new System.EventHandler(this.toolStripButton_createSelect_Click);
            // 
            // toolStripButton_createUpdate
            // 
            this.toolStripButton_createUpdate.Enabled = false;
            this.toolStripButton_createUpdate.Image = global::CodeConstructor.Properties.Resources.UpdateDatabase_16x;
            this.toolStripButton_createUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_createUpdate.Name = "toolStripButton_createUpdate";
            this.toolStripButton_createUpdate.Size = new System.Drawing.Size(71, 22);
            this.toolStripButton_createUpdate.Text = "Update";
            this.toolStripButton_createUpdate.Click += new System.EventHandler(this.toolStripButton_createUpdate_Click);
            // 
            // toolStripButton_createInsert
            // 
            this.toolStripButton_createInsert.Enabled = false;
            this.toolStripButton_createInsert.Image = global::CodeConstructor.Properties.Resources.InsertSnippet_16x;
            this.toolStripButton_createInsert.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_createInsert.Name = "toolStripButton_createInsert";
            this.toolStripButton_createInsert.Size = new System.Drawing.Size(61, 22);
            this.toolStripButton_createInsert.Text = "Insert";
            this.toolStripButton_createInsert.Click += new System.EventHandler(this.toolStripButton_createInsert_Click);
            // 
            // toolStripButton_createDelete
            // 
            this.toolStripButton_createDelete.Enabled = false;
            this.toolStripButton_createDelete.Image = global::CodeConstructor.Properties.Resources.DeleteDatabase_16x;
            this.toolStripButton_createDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_createDelete.Name = "toolStripButton_createDelete";
            this.toolStripButton_createDelete.Size = new System.Drawing.Size(65, 22);
            this.toolStripButton_createDelete.Text = "Delete";
            this.toolStripButton_createDelete.Click += new System.EventHandler(this.toolStripButton_createDelete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton_createFile
            // 
            this.toolStripButton_createFile.Image = global::CodeConstructor.Properties.Resources.CheckboxUncheck_16x;
            this.toolStripButton_createFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_createFile.Name = "toolStripButton_createFile";
            this.toolStripButton_createFile.Size = new System.Drawing.Size(89, 22);
            this.toolStripButton_createFile.Tag = "0";
            this.toolStripButton_createFile.Text = "Create File";
            this.toolStripButton_createFile.Click += new System.EventHandler(this.toolStripButton_createFile_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_file,
            this.toolStripMenuItem_tools});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1303, 25);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem_file
            // 
            this.toolStripMenuItem_file.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bb});
            this.toolStripMenuItem_file.Name = "toolStripMenuItem_file";
            this.toolStripMenuItem_file.Size = new System.Drawing.Size(59, 21);
            this.toolStripMenuItem_file.Text = "Files(&F)";
            // 
            // bb
            // 
            this.bb.Name = "bb";
            this.bb.Size = new System.Drawing.Size(217, 22);
            this.bb.Text = "Database Connection(&D)";
            this.bb.Click += new System.EventHandler(this.bb_Click);
            // 
            // toolStripMenuItem_tools
            // 
            this.toolStripMenuItem_tools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_options});
            this.toolStripMenuItem_tools.Name = "toolStripMenuItem_tools";
            this.toolStripMenuItem_tools.Size = new System.Drawing.Size(67, 21);
            this.toolStripMenuItem_tools.Text = "Tools(&T)";
            // 
            // toolStripMenuItem_options
            // 
            this.toolStripMenuItem_options.Name = "toolStripMenuItem_options";
            this.toolStripMenuItem_options.Size = new System.Drawing.Size(140, 22);
            this.toolStripMenuItem_options.Text = "Options(&O)";
            this.toolStripMenuItem_options.Click += new System.EventHandler(this.toolStripMenuItem_options_Click);
            // 
            // toolStripStatusLabel_selectedNode
            // 
            this.toolStripStatusLabel_selectedNode.Name = "toolStripStatusLabel_selectedNode";
            this.toolStripStatusLabel_selectedNode.Size = new System.Drawing.Size(12, 17);
            this.toolStripStatusLabel_selectedNode.Text = " ";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1303, 631);
            this.Controls.Add(this.splitContainer_main);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Code Constructor";
            this.Load += new System.EventHandler(this.Main_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer_main.Panel1.ResumeLayout(false);
            this.splitContainer_main.Panel2.ResumeLayout(false);
            this.splitContainer_main.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_main)).EndInit();
            this.splitContainer_main.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer_main;
        private System.Windows.Forms.TreeView treeView_dbInfo;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_file;
        private System.Windows.Forms.ToolStripMenuItem bb;
        private System.Windows.Forms.ImageList imageList_tree;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TextBox textBox_codeZone;
        private System.Windows.Forms.ToolStripButton toolStripButton_createModel;
        private System.Windows.Forms.ToolStripButton toolStripButton_createSelect;
        private System.Windows.Forms.ToolStripButton toolStripButton_createDelete;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox_namespace;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_textBox;
        private System.Windows.Forms.ToolStripButton toolStripButton_CreateXml;
        private System.Windows.Forms.ToolStripButton toolStripButton_createUpdate;
        private System.Windows.Forms.ToolStripButton toolStripButton_createInsert;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton_createFile;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_tools;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_options;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_statusInfo;
        private System.Windows.Forms.ToolStripButton toolStripButton_createCollection;
        private System.Windows.Forms.ToolStripButton toolStripButton_createQueryModel;
        private System.Windows.Forms.ToolStripButton toolStripButton_xmlReader;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_selectedNode;
    }
}