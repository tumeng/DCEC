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
    public partial class ctrl_tjonline : BaseControl
    {
        //托架上线
        private string CompanyCode, PlineCode, PlineID, StationCode, stationname, TheSo, TheSn, ThePlancode;
        //dataConn dc = new dataConn();
        ProductInfoEntity product;
        public ctrl_tjonline()
        {
            InitializeComponent();
            CompanyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            PlineID = LoginInfo.ProductLineInfo.RMES_ID;
            PlineCode = LoginInfo.ProductLineInfo.PLINE_CODE;
            StationCode = LoginInfo.StationInfo.RMES_ID;
            stationname = LoginInfo.StationInfo.STATION_NAME;
            dgvTj.AutoGenerateColumns = false;
            dgvTj.RowHeadersVisible = false;
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
            string sql = "select ghtm,jhdm,so,to_char(rqsj,'YYYY-MM-DD HH24:MI;SS') rqsj,ggxhmc,zdmc,ygmc from sjtjb where zdmc='" + stationname + "' and rqsj>sysdate-1 order by rqsj desc";
            DataTable dt = dataConn.GetTable(sql);
            dgvTj.DataSource = dt;
            dgvTj.ClearSelection();
            for (int i = 0; i < dgvTj.Rows.Count; i++)
            {
                if (i == 0)
                {
                    dgvTj.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                    //for (int j = 0; j < dgvTj.Columns.Count; j++)
                    //    dgvTj.Rows[i].Cells[j].Style.BackColor = Color.Green;
                }
                if (i > 0)
                {
                    dgvTj.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    //for (int j = 0; j < dgvTj.Columns.Count; j++)
                    //    dgvTj.Rows[i].Cells[j].Style.BackColor = Color.Yellow;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            refreash();
        }
    }
}
