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
using Microsoft.VisualBasic;
using System.Diagnostics;

namespace Rmes.WinForm.Controls
{
    public partial class ctrl_sgbd : BaseControl
    {
        //手工标定
        private string CompanyCode, PlineCode, PlineID, StationCode, stationname, TheSo, TheSn, ThePlancode, oalsh = "", oaso = "", sl = "", sbd = "";
        //dataConn dc = new dataConn();
        ProductInfoEntity product;
        public ctrl_sgbd()
        {
            InitializeComponent();
            CompanyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            PlineID = LoginInfo.ProductLineInfo.RMES_ID;
            PlineCode = LoginInfo.ProductLineInfo.PLINE_CODE;
            StationCode = LoginInfo.StationInfo.RMES_ID;
            stationname = LoginInfo.StationInfo.STATION_NAME;
            dgvSg.AutoGenerateColumns = false;
            dgvSg.RowHeadersVisible = false;
            dgvSg.DataSource = null;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.DataSource = null;
            refreash();
            GetFocused(txtoalsh);
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
            if (e.MessageHead == "REFRESHPLAN")
            {
                refreash();
            }
        }
        private void refreash()
        {
            //刷新数据
            string sql = "select t.oalsh ,t.so ,t.soqy ,t.sl ,t.count2 ,t.ECMCODE,t.yhmc ,second_bd  from vw_ecm_oa t where upper(t.so) like '%" + txtoaso.Text.Trim().ToUpper() + "%' and t.oalsh like '%" + txtoalsh.Text.Trim().ToUpper() + "%' and gzqy='" + PlineCode + "' and (to_number(sl)*secondbd)>to_number(count2) order by oalsh";
            DataTable dt = dataConn.GetTable(sql);
            dgvSg.DataSource = dt;
            dgvSg.ClearSelection();
            GetFocused(txtlsh);
        }
        private void refreash2()
        {
            //刷新数据
            string sql = "select t.oalsh ,t.so ,t.soqy ,t.sl ,t.count2 ,t.ECMCODE,t.yhmc ,second_bd  from vw_ecm_oa t where  gzqy='" + PlineCode + "' and (to_number(sl)*secondbd)>to_number(count2) order by oalsh";
            DataTable dt = dataConn.GetTable(sql);
            dgvSg.DataSource = dt;
            dgvSg.ClearSelection();
            GetFocused(txtlsh);
        }
        private void refreash3()
        {
            //刷新数据
            string sql = "select oalsh ,fdjlsh ,to_char(record_time,'yyyy-mm-dd hh24:mi:ss') record_time  from atpuecm_lsh where oalsh='" + txtoalsh.Text.Trim().ToUpper() + "'";
            DataTable dt = dataConn.GetTable(sql);
            dataGridView1.DataSource = dt;
            dataGridView1.ClearSelection();
            GetFocused(txtlsh);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //申请单与流水号绑定
            if (txtoalsh.Text.Trim() == "")
            {
                MessageBox.Show("请选择申请单", "提示");
                GetFocused(txtlsh);
                return;
            }
            //查询申请单是否存在
            string sql = "select * from vw_ecm_oa t where oalsh ='" + txtoalsh.Text.Trim().ToUpper() + "' and gzqy='" + PlineCode + "' and (to_number(sl)*secondbd)>to_number(count2) ";
            DataTable dt = dataConn.GetTable(sql);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("该申请单不存在或数量已满，无法标定", "提示");
                GetFocused(txtlsh);
                return;
            }
            //查询流水号是否出库
            sql = "select * from dp_rckwcb t where ghtm ='" + txtlsh.Text.Trim().ToUpper() + "' and  rklx='正常出库' ";
            dt = dataConn.GetTable(sql);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("该流水号已正常出库，无法标定", "提示");
                GetFocused(txtlsh);
                return;
            }
            //查询流水号是否手工标定
            sql = "select * from data_plan where plan_code=(select plan_code from data_product where sn='" + txtlsh.Text.Trim() + "' and rownum=1) and  remark like '%手工标定%'";
            dt = dataConn.GetTable(sql);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("该流水号非法，不是手工标定流水号", "提示");
                GetFocused(txtlsh);
                return;
            }
            //查询流水号与so是否匹配
            sql = "select * from data_product where sn='" + txtlsh.Text.Trim().ToUpper() + "' and upper(plan_so)=upper((select so from vw_ecm_oa where oalsh='" + txtoalsh.Text.Trim().ToUpper() + "' and rownum=1))";
            dt = dataConn.GetTable(sql);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("该流水号非法，与SO不匹配", "提示");
                GetFocused(txtlsh);
                return;
            }
            //查询是否重复记录
            sql = "select * from atpuecm_lsh where fdjlsh='" + txtlsh.Text.Trim().ToUpper() + "' and oalsh!='" + txtoalsh.Text.Trim().ToUpper() + "'";
            dt = dataConn.GetTable(sql);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("该流水号已绑定其他申请单", "提示");
                GetFocused(txtlsh);
                return;
            }
            //查询是否二次标定 判断是否超出数量 一个流水号同一个申请单最多扫两次
            sql = "select * from atpuecm_lsh where fdjlsh='" + txtlsh.Text.Trim().ToUpper() + "' and oalsh='" + txtoalsh.Text.Trim().ToUpper() + "'";
            dt = dataConn.GetTable(sql);
            if (sbd == "Y")
            {
                if (dt.Rows.Count == 2)
                {
                    MessageBox.Show("该流水号已二次标定", "提示");
                    GetFocused(txtlsh);
                    return;
                }
                else
                {
                    //新流水号需要判断申请单绑定数量是否已满 二次标定的不能多
                    if (dt.Rows.Count == 0)
                    {
                        //判断是否超出数量
                        sql = "select distinct fdjlsh from atpuecm_lsh where oalsh='" + txtoalsh.Text.Trim() + "'";
                        DataTable dt1 = dataConn.GetTable(sql);
                        if (dt1.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(sl) <= dt1.Rows.Count)
                            {
                                MessageBox.Show("超出该申请单申请数量", "提示");
                                GetFocused(txtlsh);
                                return;
                            }
                        }
                    }
                }
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("该流水号已绑定该申请单", "提示");
                    GetFocused(txtlsh);
                    return;
                }
            }

            sql = "insert into atpuecm_lsh(oalsh,fdjlsh,czy)values('" + txtoalsh.Text.Trim() + "','" + txtlsh.Text.Trim() + "','" + LoginInfo.UserInfo.USER_CODE + "')";
            dataConn.ExeSql(sql);
            refreash2();
            refreash3();
            GetFocused(txtlsh);
        }

        private void dgvSg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            oalsh = dgvSg.Rows[e.RowIndex].Cells["coloalsh"].Value.ToString();
            oaso = dgvSg.Rows[e.RowIndex].Cells["coloaso"].Value.ToString();
            sl = dgvSg.Rows[e.RowIndex].Cells["colsl"].Value.ToString();
            sbd = dgvSg.Rows[e.RowIndex].Cells["colsbd"].Value.ToString();
            txtoalsh.Text = oalsh;
            txtoaso.Text = oaso;

            refreash3();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string password = Microsoft.VisualBasic.Interaction.InputBox("请输入密码","输入密码");
            if (password.Trim() == "")
            {
                return;
            }
            if (password.Trim() != "dcecsgbd")
            {
                MessageBox.Show("密码错误", "提示");
                return;
            }
            if (txtoldsqd.Text.Trim() == "" || txtnewlsh.Text.Trim() == "" || txtoldlsh.Text.Trim() == "")
            {
                MessageBox.Show("请选择申请单并输入替换流水号", "提示");
                return;
            }
            //变更 判断旧流水号是否正常出库
            string sql = "select * from dp_rckwcb t where ghtm ='" + txtoldlsh.Text.Trim().ToUpper() + "' and  rklx='正常出库' ";
            DataTable dt = dataConn.GetTable(sql);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("该流水号已正常出库，无法标定", "提示");
                return;
            }
            sql = "select * from atpuecm_lsh where fdjlsh='" + txtnewlsh.Text.Trim().ToUpper() + "' ";
            dt = dataConn.GetTable(sql);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("该流水号已绑定其他申请单", "提示");
                return;
            }
            sql = "select * from data_plan where plan_code=(select plan_code from data_product where sn='" + txtnewlsh.Text.Trim() + "' and rownum=1) and  remark like '%手工标定%'";
            dt = dataConn.GetTable(sql);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("该流水号非法，不是手工标定流水号", "提示");
                return;
            }
            //查询流水号与so是否匹配
            sql = "select * from data_product where sn='" + txtnewlsh.Text.Trim().ToUpper() + "' and upper(plan_so)=upper((select so from vw_ecm_oa where oalsh='" + txtoldsqd.Text.Trim().ToUpper() + "' and rownum=1))";
            dt = dataConn.GetTable(sql);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("该流水号非法，与SO不匹配", "提示");
                return;
            }
            //记录更新日志
            sql = "insert into atpuecmlshthtmp(oalsh,oldlsh,newlsh,czy)values('" + txtoldsqd.Text.Trim() + "','" + txtoldlsh.Text.Trim() + "','" + txtnewlsh.Text.Trim() + "','" + LoginInfo.UserInfo.USER_CODE + "')";
            dataConn.ExeSql(sql);
            //更新对应关系
            sql = "update atpuecm_lsh set fdjlsh='" + txtnewlsh.Text.Trim() + "' where oalsh='" + txtoldsqd.Text.Trim() + "' and fdjlsh='" + txtoldlsh.Text.Trim() + "'";
            dataConn.ExeSql(sql);
            refreash3();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            txtoldlsh.Text = dataGridView1.Rows[e.RowIndex].Cells["colfdjlsh"].Value.ToString();
            txtoldsqd.Text = dataGridView1.Rows[e.RowIndex].Cells["colsqd"].Value.ToString();
            txtnewlsh.Text = "";
        }

        private void txtlsh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13) return;
            if (string.IsNullOrWhiteSpace(txtlsh.Text)) return;
            button1_Click(button1, new EventArgs());
        }

        private void txtnewlsh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13) return;
            if (string.IsNullOrWhiteSpace(txtnewlsh.Text)) return;
            button2_Click(button2, new EventArgs());
        }

        private void txtoalsh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13) return;
            if (string.IsNullOrWhiteSpace(txtoalsh.Text)) return;
            string thisinput = "";
            thisinput = txtoalsh.Text.Trim().ToUpper();
            if (thisinput.StartsWith("$")) //单独指令
            {
                switch (thisinput)
                {
                    case "$QUIT": //关闭计算机
                        Process.Start("shutdown.exe", "-s -t 10");
                        break;
                    case "$EXIT"://退出程序
                        BaseForm tempForm1 = (BaseForm)this.ParentForm;
                        PublicClass.ClearEvents(tempForm1);
                        Application.Exit();
                        break;
                    case "$CANC"://重新登录
                        BaseForm tempForm = (BaseForm)this.ParentForm;
                        PublicClass.ClearEvents(tempForm);
                        Application.Restart();
                        break;
                    case "$LJQD"://显示零件清单
                        break;
                    case "$CZTS"://显示装机提示
                        break;
                    case "$JHCX"://显示前后30天计划信息，双击查对应bom
                        break;
                    case "$LJDCL"://录入线边待处理零件信息frmMs003
                        break;
                    case "$LJGYS"://录入零件供应商信息frmMs001
                        break;
                    case "$LJQL"://录入缺料信息frmZlbj
                        break;
                    case "$LSYL"://录入现场临时要料信息frmMs002
                        break;
                    case "$QXJL"://显示返修质量记录frmQxInput
                        break;
                    case "$SBGZ"://录入设备故障frmZlbj
                        break;
                    case "$SKIP"://物料核减控制frmMs004
                        break;
                    case "$ZDTPTS"://查看装机图片FrmShowPic
                        break;
                    case "$ZLBG"://录入质量缺陷frmZlbj
                        break;
                    case "$ZPZYZDS"://查看工艺文件frmGywjSel
                        break;
                    case "$ZPJL"://显示、录入装配记录frmZpjlInput
                        break;
                    case "$BHGP"://处理不合格品
                        break;
                    case "$CXGL"://VEPS重新灌录
                        CsGlobalClass.NEEDVEPS = true;
                        //arg.MessageHead = "VEPS";
                        //arg.MessageBody = "";
                        //SendDataChangeMessage(arg);
                        break;
                }
                refreash();
            }
        }

        private void txtoaso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13) return;
            if (string.IsNullOrWhiteSpace(txtoaso.Text)) return;
            string thisinput = "";
            thisinput = txtoaso.Text.Trim().ToUpper();
            if (thisinput.StartsWith("$")) //单独指令
            {
                switch (thisinput)
                {
                    case "$QUIT": //关闭计算机
                        Process.Start("shutdown.exe", "-s -t 10");
                        break;
                    case "$EXIT"://退出程序
                        BaseForm tempForm1 = (BaseForm)this.ParentForm;
                        PublicClass.ClearEvents(tempForm1);
                        Application.Exit();
                        break;
                    case "$CANC"://重新登录
                        BaseForm tempForm = (BaseForm)this.ParentForm;
                        PublicClass.ClearEvents(tempForm);
                        Application.Restart();
                        break;
                    case "$LJQD"://显示零件清单
                        break;
                    case "$CZTS"://显示装机提示
                        break;
                    case "$JHCX"://显示前后30天计划信息，双击查对应bom
                        break;
                    case "$LJDCL"://录入线边待处理零件信息frmMs003
                        break;
                    case "$LJGYS"://录入零件供应商信息frmMs001
                        break;
                    case "$LJQL"://录入缺料信息frmZlbj
                        break;
                    case "$LSYL"://录入现场临时要料信息frmMs002
                        break;
                    case "$QXJL"://显示返修质量记录frmQxInput
                        break;
                    case "$SBGZ"://录入设备故障frmZlbj
                        break;
                    case "$SKIP"://物料核减控制frmMs004
                        break;
                    case "$ZDTPTS"://查看装机图片FrmShowPic
                        break;
                    case "$ZLBG"://录入质量缺陷frmZlbj
                        break;
                    case "$ZPZYZDS"://查看工艺文件frmGywjSel
                        break;
                    case "$ZPJL"://显示、录入装配记录frmZpjlInput
                        break;
                    case "$BHGP"://处理不合格品
                        break;
                    case "$CXGL"://VEPS重新灌录
                        CsGlobalClass.NEEDVEPS = true;
                        //arg.MessageHead = "VEPS";
                        //arg.MessageBody = "";
                        //SendDataChangeMessage(arg);
                        break;
                }

            }
            refreash();
        }

        private void txtoldsqd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13) return;
            if (string.IsNullOrWhiteSpace(txtoldsqd.Text)) return;
            string thisinput = "";
            thisinput = txtoldsqd.Text.Trim().ToUpper();
            txtlsh.Text = "";
            txtoldlsh.Text = "";
            txtnewlsh.Text = "";
            
            string sql = "select t.oalsh ,t.so,t.soqy ,t.sl ,t.count2 ,t.ECMCODE,t.yhmc ,second_bd from vw_ecm_oa t where t.oalsh like '%" + txtoldsqd.Text.Trim() + "%' and gzqy='" + PlineCode + "' ";
            DataTable dt = dataConn.GetTable(sql);
            dgvSg.DataSource = dt;
            dgvSg.ClearSelection();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            refreash2();
            refreash3();
        }



    }
}
