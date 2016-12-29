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
    public partial class ctrl_insite : BaseControl
    {
        //insite标定
        private string CompanyCode, PlineCode, PlineID, StationCode, stationname, TheSo, TheSn, ThePlancode;
        //dataConn dc = new dataConn();
        ProductInfoEntity product;

        public ctrl_insite()
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
            refreash();
            this.RMesDataChanged += new RMesEventHandler(ctrl_Tj_RMesDataChanged);
        }
        void ctrl_Tj_RMesDataChanged(object obj, Rmes.WinForm.Base.RMESEventArgs e)
        {
            RMESEventArgs arg = new RMESEventArgs();
            arg.MessageHead = "";
            arg.MessageBody = "";
            if (e.MessageHead == null) return;
            if (e.MessageHead == "REFRESHPLAN" || e.MessageHead == "RESN")
            {
                //校验sn后进行insite标定
                timer1.Enabled = false;
                string sn = e.MessageBody.ToString();
                product = ProductInfoFactory.GetByCompanyCodeSNSingle(CompanyCode, sn);//获取sn信息
                if (product == null) return;
                //是否改制返修发动机
                string sql = "";
                if (product.PLAN_TYPE != "C" && product.PLAN_TYPE != "D")
                {
                    sql = "select * from dp_rckwcb where ghtm='"+sn+"' and rklx='正常出库' ";
                    DataTable dt = dataConn.GetTable(sql);
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("该流水号已正常出库，无法标定", "提示");
                        return;
                    }
                }
                sql = "select insite_bd from atpuecmlsh where ghtm='" + sn + "' ";
                DataTable dt1 = dataConn.GetTable(sql);
                if (dt1.Rows.Count == 0)
                {
                    MessageBox.Show("此发动机号非法", "提示");
                    return;
                }
                else
                {
                    if (dt1.Rows[0][0].ToString() == "")
                    {
                        DialogResult drt = MessageBox.Show("非INSITE版标定发动机!是否标定?", "提示", MessageBoxButtons.YesNo);
                        if (drt == DialogResult.No) return;
                    }
                    else if (dt1.Rows[0][0].ToString() == "Y")
                    {
                        DialogResult drt = MessageBox.Show("已刷过INSITE版标定!是否重复标定?", "提示", MessageBoxButtons.YesNo);
                        if (drt == DialogResult.No) return;
                    }
                }
                sql = "update atpuecmlsh set insite_bd='Y',insite_time=sysdate,INSITE_CZY='" + LoginInfo.UserInfo.USER_CODE + "' where ghtm='" + sn + "' ";
                dataConn.ExeSql(sql);
                refreash();
                timer1.Enabled = true;
            }
        }
        private void refreash()
        {
            string sql = "select t.ghtm,a.plan_so,a.plan_code from atpuecmlsh t left join data_product a on t.ghtm=a.sn where  t.insite_bd='N' and a.pline_code='" + PlineCode + "' and t.rqsj1>sysdate-3 order by a.plan_code";
            dgvInsite.DataSource = dataConn.GetTable(sql);
            dgvInsite.ClearSelection();
        }

        private void dgvTj_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            refreash();
        }

        private void dgvTj_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //双击流水号进行标定
            if (e.RowIndex < 0) return;
            timer1.Enabled = false;
            RMESEventArgs thisEventArg = new RMESEventArgs();
            thisEventArg.MessageHead = "";
            thisEventArg.MessageBody = "";
            dgvInsite.ClearSelection();
            string sn = dgvInsite.Rows[e.RowIndex].Cells["colSn"].Value.ToString();
            string plan_so = dgvInsite.Rows[e.RowIndex].Cells["colSo"].Value.ToString();
            string plan_code = dgvInsite.Rows[e.RowIndex].Cells["colPlancode"].Value.ToString();
            List<ProductInfoEntity> newProduct = ProductInfoFactory.GetByCompanyCodeSN(LoginInfo.CompanyInfo.COMPANY_CODE, sn);
            if (newProduct.Count != 0)
            {
                thisEventArg.MessageHead = "FILLSN";
                thisEventArg.MessageBody = sn + "^" + plan_so + "^" + newProduct.First<ProductInfoEntity>().PRODUCT_MODEL + "^" + plan_code;
                SendDataChangeMessage(thisEventArg);
            }
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            refreash();
        }
    }
}
