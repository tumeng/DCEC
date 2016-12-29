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
    public partial class ctrl_tjoffline : BaseControl
    {
        //托架下线
        private string CompanyCode, PlineCode, PlineID, StationCode, stationname, TheSo, TheSn, ThePlancode;
        //dataConn dc = new dataConn();
        ProductInfoEntity product;
        public ctrl_tjoffline()
        {
            InitializeComponent();
            CompanyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            PlineID = LoginInfo.ProductLineInfo.RMES_ID;
            PlineCode = LoginInfo.ProductLineInfo.PLINE_CODE;
            StationCode = LoginInfo.StationInfo.RMES_ID;
            stationname = LoginInfo.StationInfo.STATION_NAME;
            dgvTj.AutoGenerateColumns = false;
            dgvTj.RowHeadersVisible = false;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.RowHeadersVisible = false;
            refreash();
            this.RMesDataChanged += new RMesEventHandler(ctrl_Tj_RMesDataChanged);
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
            string sql = "select ghtm,so,get_sotj(so) tj,get_jhyh(jhdm) yhdm,rownum from sjtjb  where zdmc='" + stationname + "' and wcbs='N' order by rqsj";
            DataTable dt = dataConn.GetTable(sql);
            dgvTj.DataSource = dt;
            dgvTj.ClearSelection();
            for (int i = 0; i < dgvTj.Rows.Count; i++)
            {
                if (i == 0)
                {
                    dgvTj.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                    string sn1 = dt.Rows[i][0].ToString(); ;
                    ProductInfoEntity product= ProductInfoFactory.GetByCompanyCodeSNSingle(CompanyCode, sn1);
                    if (product != null)
                    {
                        RMESEventArgs arg = new RMESEventArgs();
                        arg.MessageHead = "FILLSN";
                        arg.MessageBody = sn1 + "^" + product.PLAN_SO + "^" + product.PRODUCT_MODEL + "^" + product.PLAN_CODE;
                        SendDataChangeMessage(arg);
                    }
                }
                if (i > 1)
                {
                    dgvTj.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
            sql = "select ghtm,so,get_sotj(so) tj,to_char(wcsj,'YYYY-MM-DD HH24:MI:SS') wcsj,get_jhyh(jhdm) yhdm from sjtjb where zdmc='" + stationname + "' and wcbs='Y' and wcsj>sysdate-1 order by rqsj desc";
            dt = dataConn.GetTable(sql);
            dataGridView1.DataSource = dt;
            dataGridView1.ClearSelection();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            refreash();
        }

    }
}
