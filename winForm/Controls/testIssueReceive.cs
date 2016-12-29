using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Rmes.WinForm.Base;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using Rmes.WinForm.Controls.DataGridViewColumns;
using Rmes.DA.Base;


namespace Rmes.WinForm.Controls
{
    public partial class testIssueReceive : Form
    {
        public delegate void IssueReceived(DataTable dt);
        public IssueReceived receive;
        public DataTable dt = new DataTable();
        public BindingSource bs = new BindingSource();
        public testIssueReceive(string tmbh)
        {
            InitializeComponent();
            dt.Columns.Add("TMBH");
            dt.Columns.Add("itemCode");
            dt.Columns.Add("itemName");
            dt.Columns.Add("itemQTY");
            dt.Columns.Add("receivedQTY");
            dt.Columns.Add("receiveQTY");
            FillTable(tmbh);

            
        }

        private void txtSN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13) return;
            if (string.IsNullOrWhiteSpace(txtSN.Text)) return;
            txtSN.Focus();
            txtSN.Select(0, txtSN.Text.Length);

            string txt = txtSN.Text;

            ////通过正则表达式判断扫描内容
            RMESEventArgs arg = new RMESEventArgs();
            MessageHeadEntity mse = MessageHeadFactory.GetByString(txt);
            if (mse == null) return;
            arg.MessageHead = mse.HEAD_CODE;
            arg.MessageBody = txtSN.Text;
            string msghead = arg.MessageHead;
            string msgbody = arg.MessageBody as string;
            if (msghead == "MESLL")
            {
                FillTable(txt);
                return;
            }
            else
            {
                MessageBox.Show("请输入正确的条码编号!");
                return;
            }
        }

        private void FillTable(string tmbh)
        {
            //若条码以扫  则直接返回
            foreach (DataRow dr in dt.Rows)
            {
                string _tmbh = dr["TMBH"].ToString();
                if (tmbh.Equals(_tmbh) || tmbh == _tmbh)
                    return;
            }
            InterIssueEntity ii = InterIssueFactory.getByTMBH(tmbh);
            if (ii == null)
            {
                MessageBox.Show("没有对应物料信息，请重新输入");
                return;
            }
            if (ii.LLBS == "Y" || ii.LLBS.Equals("Y"))
            {
                MessageBox.Show("物料以接收");
                return;
            }
            TeamEntity team = LoginInfo.TeamInfo;
            //判断是否是本小组物料
            //if (!(ii.LLZPXZ.Equals(team.RMES_ID)) || ii.LLZPXZ != team.RMES_ID)
            //{
            //    MessageBox.Show("非本装配小组物料");
            //    return;
            //}
            //根据条码标识获取物料以前接收记录
            List<IssueReceivedEntity> allReceived = IssueReceivedFactory.GetByDetailCode(tmbh);
            float sum = 0;
            foreach (IssueReceivedEntity a in allReceived)
            {
                sum += a.ITEM_QTY;
            }
            dt.Rows.Add(ii.TMBH,ii.LLXMDH,ii.LLXMMC,ii.LLSL,sum,ii.LLSL-sum);
            bs.DataSource = dt;
            dataGridView1.DataSource = bs;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            receive.Invoke(dt);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string tmbh = dataGridView1.Rows[e.RowIndex].Cells["TMBH"].Value.ToString().Trim();
            int receiveQTY ;
            try
            {
                receiveQTY = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["receiveQTY"].Value.ToString().Trim());
            }
            catch(Exception ex)
            {
                MessageBox.Show("请输入正确的数字");
                return;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string _tmbh = dt.Rows[i]["TMBH"].ToString().Trim();
                if (_tmbh.Equals(tmbh) || _tmbh == tmbh)
                    dt.Rows[i]["receiveQTY"] = receiveQTY;
            }
        }

        private void testIssueReceive_Load(object sender, EventArgs e)
        {
            txtSN.Focus();
            txtSN.Select();
        }
    }
}
