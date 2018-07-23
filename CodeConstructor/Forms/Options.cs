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
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
        }

        private void InitializeData()
        {
            try
            {
                this.textBox_createFilePath.Text = Settings.Default.CreateFilePath;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message:\r\n" + ex.Message, "InitializeData Error");
            }
        }

        private void Options_Load(object sender, EventArgs e)
        {
            InitializeData();
        }

        private void button_selectCreateFilePath_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.textBox_createFilePath.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Settings.Default.CreateFilePath = this.textBox_createFilePath.Text;
            Settings.Default.Save();
            this.DialogResult = DialogResult.OK;
        }
    }
}
