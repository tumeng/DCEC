using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Rmes.WinForm.Base;
using Rmes.Pub.Data1;
using System.IO;
using System.Diagnostics;

namespace Rmes.WinForm.Controls
{
    public partial class FrmShowPrinted : Form
    {
        //dataConn dc = new dataConn();
        public FrmShowPrinted()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.DataSource = null;
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now.AddDays(1);
            showPrint(DateTime.Now.ToString(), DateTime.Now.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string bdate = dateTimePicker1.Value.ToShortDateString();
            string edate = dateTimePicker2.Value.ToShortDateString();
            showPrint(bdate, edate);
        }
        void showPrint(string bdate, string edate)
        {
            DataTable dt = new DataTable();
            string sql = "";
            sql = string.Format("select lsh,so,jx,pfdj,pfbgh,rq,jyy,xlh FROM atpuhgz t WHERE t.rq>=to_date('" + bdate + "','yyyy/mm/dd hh24:mi:ss') and  t.rq<=to_date('" + edate + "','yyyy/mm/dd hh24:mi:ss')  and  lsh like '%" + textBox1.Text + "%' ");
            dt = dataConn.GetTable(sql);
            dataGridView1.DataSource = dt;
        }
    }
}
