﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace form1
{
    public partial class ReadCsvFile : Form
    {
        public ReadCsvFile()
        {
            InitializeComponent();
        }
        public DataTable ReadCsv(string fileName)
        {
            DataTable dt = new DataTable("Data");
            using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft.jet.OLEDB.4.0;Data Source=\"" + 
                Path.GetDirectoryName(fileName) + "\";Extended Properties='text;HDR=yes;FMT=Delimited(.)';"))
            {
                using (OleDbCommand cmd = new OleDbCommand(string.Format("select *from[{0}]", new FileInfo(fileName).Name), cn))
                {
                    cn.Open();
                    using(OleDbDataAdapter adapter=new OleDbDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }
        private void BtnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd= new OpenFileDialog() { Filter = "CSV|*.csv", ValidateNames = true, Multiselect = false })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                        dataGridView.DataSource = ReadCsv(ofd.FileName);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
