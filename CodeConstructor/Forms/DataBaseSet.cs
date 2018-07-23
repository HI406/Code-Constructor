using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CodeConstructor.Properties;

namespace CodeConstructor.Forms
{
    public partial class DataBaseSet : Form
    {
        public DataBaseSet()
        {
            InitializeComponent();
        }

        string connStr = "Data Source={0};Initial Catalog={1};User ID={2};Password={3};Connect Timeout=30";
        private string _connectionStrings;
        private bool isLoaded = false;
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string connectionStrings
        {
            set { _connectionStrings = value; }
            get { return _connectionStrings; }
        }

        private void InitializeData()
        {
            try
            {
                string[] Conn = _connectionStrings.Split(';');
                this.textBox_connectionStrings.Text = Settings.Default.connectionStrings;
                this.textBox_dataSource.Text = Conn[0].Split('=')[1];
                this.comboBox_initialCatalog.DataSource = Support.DataBaseHandler.Select.GetDBInfo("GetAllDataBase");
                this.comboBox_initialCatalog.DisplayMember = "name";
                this.comboBox_initialCatalog.ValueMember = "name";
                this.comboBox_initialCatalog.Text = Conn[1].Split('=')[1];
                this.textBox_userId.Text = Conn[2].Split('=')[1];
                this.textBox_password.Text = Conn[3].Split('=')[1];
                this.textBox_dataSource.AutoCompleteCustomSource.AddRange(Settings.Default.DataSource.Split(','));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message:\r\n" + ex.Message, "InitializeData Error");
            }
        }

        private void DataBaseSet_Load(object sender, EventArgs e)
        {//
            _connectionStrings = Settings.Default.connectionStrings;
            InitializeData();
            isLoaded = true;
        }

        private void SetConnStr()
        {
            if (string.IsNullOrEmpty(this.comboBox_initialCatalog.Text))
            {
                this.comboBox_initialCatalog.Text = "master";
            }
            this.textBox_connectionStrings.Text = string.Format(connStr, this.textBox_dataSource.Text, this.comboBox_initialCatalog.Text, this.textBox_userId.Text, this.textBox_password.Text);
            _connectionStrings = this.textBox_connectionStrings.Text;
        }

        private void textBox_dataSource_TextChanged(object sender, EventArgs e)
        {
            SetConnStr();
            this.textBox_dataSource.Text = this.textBox_dataSource.Text.Replace("。", ".");
        }

        private void comboBox_initialCatalog_SelectedValueChanged(object sender, EventArgs e)
        {
            SetConnStr();
        }

        private void textBox_userId_TextChanged(object sender, EventArgs e)
        {
            SetConnStr();
        }

        private void textBox_password_TextChanged(object sender, EventArgs e)
        {
            SetConnStr();
        }

        private void button_confirm_Click(object sender, EventArgs e)
        {
            Settings.Default.DataSource += this.textBox_dataSource.Text + ",";
            Settings.Default.connectionStrings = _connectionStrings;
            Settings.Default.InitialCatalog = this.comboBox_initialCatalog.Text;
            Settings.Default.UserID = this.textBox_userId.Text;
            Settings.Default.Password = this.textBox_password.Text;
            Settings.Default.Save();
            this.DialogResult = DialogResult.OK;
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void button_test_Click(object sender, EventArgs e)
        {
            try
            {
                string ip = this.textBox_dataSource.Text;
                if (isLoaded && ip.Replace(".", "").Length + 3 == ip.Length)
                {
                    string oldConnStr = Settings.Default.connectionStrings;
                    Settings.Default.connectionStrings = this.textBox_connectionStrings.Text;
                    Settings.Default.Save();
                    InitializeData();
                    Settings.Default.connectionStrings = oldConnStr;
                    Settings.Default.Save();
                }
                MessageBox.Show("Database test passed!", "Message");
                this.button_confirm.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Error");
            }
        }
    }
}
