using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Rmes.WinForm.Base;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using Rmes.Pub.Data1;

namespace Rmes.WinForm.Controls
{
    public partial class ctrl_ecmfctm : BaseControl
    {
        //ecm防错条码打印
        private string CompanyCode, PlineCode, PlineID, StationCode, stationname, TheSo, TheSn, ThePlancode;
        //dataConn dc = new dataConn();
        ProductInfoEntity product;
        public ctrl_ecmfctm()
        {
            InitializeComponent();
            CompanyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            PlineID = LoginInfo.ProductLineInfo.RMES_ID;
            PlineCode = LoginInfo.ProductLineInfo.PLINE_CODE;
            StationCode = LoginInfo.StationInfo.RMES_ID;
            stationname = LoginInfo.StationInfo.STATION_NAME;
            dgvInsite.AutoGenerateColumns = false;
            dgvInsite.RowHeadersVisible = false;
            dgvInsite.DataSource = null;
            txtLjh.Text = "";
            txtLsh.Text = "";
            txtSo.Text = "";
            GetFocused(txtLsh);
            refreash();
            this.RMesDataChanged += new RMesEventHandler(ctrl_Tj_RMesDataChanged);
        }
        private void GetFocused(TextBox textBox)
        {
            textBox.Focus();
            textBox.Select(0, textBox.Text.Length);
        }
        void ctrl_Tj_RMesDataChanged(object obj, Rmes.WinForm.Base.RMESEventArgs e)
        {
            RMESEventArgs arg = new RMESEventArgs();
            arg.MessageHead = "";
            arg.MessageBody = "";
            if (e.MessageHead == null) return;
        }
        private void refreash()
        {
            txtLsh.Text = "";
            string sql = "select a.sn,a.plan_so,a.work_time from data_product a where a.pline_code='" + PlineCode + "' and get_sfdk(a.plan_so)='是' and a.work_time>=sysdate-100 and exists (select * from atpukhtm b where b.khmc='东风' and b.so=a.plan_so) and not exists (select * from atpuecmfctm c where c.ghtm=a.sn) order by a.work_time ";
            DataTable dt = dataConn.GetTable(sql);
            dgvInsite.DataSource = dt;
            dgvInsite.ClearSelection();
            if (dt.Rows.Count > 0)
                txtLsh.Text = dt.Rows[0][0].ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //判断没有打条码的上线发动机
            //如果有改制条码先打印
            string sql = "select ghtm from sjecmgztm a where a.gzdd='" + PlineCode + "' and a.handle_flag='N'  and get_sfdk(a.so)='是' and exists (select * from atpukhtm b where b.khmc='东风' and b.so=a.so) and rownum=1";
            DataTable dt = dataConn.GetTable(sql);
            if (dt.Rows.Count > 0)
            {
                txtLsh.Text = dt.Rows[0][0].ToString();
            }
            else
            {
                refreash();
            }
            if (txtLsh.Text != "")
            {
                button1_Click(button1,new EventArgs());
            }
        }

        private void txtLsh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13) return;
            if (string.IsNullOrWhiteSpace(txtLsh.Text)) return;
            string thisinput = "";
            thisinput = txtLsh.Text.Trim().ToUpper();
            txtSo.Text = "";
            txtLjh.Text = "";
            //
            string sql = "SELECT a.plan_so,b.wlh FROM data_product a,atpukhtm b WHERE a.sn='" + thisinput + "' and a.plan_so=b.so";
            DataTable dt = dataConn.GetTable(sql);
            if (dt.Rows.Count > 0)
            {
                txtSo.Text = dt.Rows[0][0].ToString();
                txtLjh.Text = dt.Rows[0][1].ToString();
            }
            else
            {
                MessageBox.Show("非法流水号","提示");
                GetFocused(txtLsh);
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtLsh_KeyPress(txtLsh,new KeyPressEventArgs((char)13));
            if (txtLsh.Text != "")
            {
                PublicClass.printECMFC(txtLsh.Text,txtSo.Text);
                string sql = "insert into atpuecmfctm(ghtm,so,partnum,rqsj) values('" + txtLsh.Text + "','" + txtSo.Text + "','" + txtLjh.Text + "',sysdate)";
                dataConn.ExeSql(sql);
            }
            GetFocused(txtLsh);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //自动打印
            if (timer1.Enabled == false)
            {
                timer1.Enabled = true;
                txtLsh.Enabled = false;
                txtSo.Enabled = false;
                txtLjh.Enabled = false;
                button1.Enabled = false;
            }
            else
            {
                timer1.Enabled = false;
                txtLsh.Enabled = true;
                txtSo.Enabled = true;
                txtLjh.Enabled = true;
                button1.Enabled = true;
            }
            if (button2.Text == "自动打印")
                button2.Text = "暂停";
            else
                button2.Text = "自动打印";

        }

        private void dgvInsite_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            txtLsh.Text = dgvInsite.Rows[e.RowIndex].Cells["colSn"].Value.ToString();
            txtLsh_KeyPress(txtLsh,new KeyPressEventArgs((char)13));
            GetFocused(txtLsh);
        }



    }
}
