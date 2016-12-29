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
    public partial class ctrl_ecmQuery : BaseControl
    {
        private string CompanyCode, PlineCode, PlineID, StationCode, stationname, TheSo, TheSn, ThePlancode;
        //dataConn dc = new dataConn();
        ProductInfoEntity product;
        public ctrl_ecmQuery()
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
            dtBegin.Value = DateTime.Now;
            dtEnd.Value = DateTime.Now;
            //this.RMesDataChanged += new RMesEventHandler(ctrl_Tj_RMesDataChanged);
        }
        void ctrl_Tj_RMesDataChanged(object obj, Rmes.WinForm.Base.RMESEventArgs e)
        {
            RMESEventArgs arg = new RMESEventArgs();
            arg.MessageHead = "";
            arg.MessageBody = "";
            if (e.MessageHead == null) return;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "";
            if (txtLsh.Text == "")
            {
                if (CsGlobalClass.GWMC == "ECM")
                {
                    sql = "select ghtm ,ecm ,unitno ,ygmc1 ,rqsj1 ,zdmc1 ,prt1 ,pn ,sn ,ec ,dc ,ygmc2 ,rqsj2 ,zdmc2 ,prt2  from atpuecmlsh where to_char(rqsj2,'yyyy-mm-dd')>='" + dtBegin.Value.ToString("yyyy-MM-dd") + "' and to_char(rqsj2,'yyyy-mm-dd')<='" + dtEnd.Value.ToString("yyyy-MM-dd") + "'";
                }
                else
                {
                    sql = "select ghtm ,ecm ,unitno ,ygmc1 ,rqsj1 ,zdmc1 ,prt1 ,pn ,sn ,ec ,dc ,ygmc2 ,rqsj2 ,zdmc2 ,prt2  from atpuecmlsh where to_char(rqsj1,'yyyy-mm-dd')>='" + dtBegin.Value.ToString("yyyy-MM-dd") + "' and to_char(rqsj1,'yyyy-mm-dd')<='" + dtEnd.Value.ToString("yyyy-MM-dd") + "'";
                }
            }
            else
            {
                sql = "select ghtm ,ecm ,unitno ,ygmc1 ,rqsj1 ,zdmc1 ,prt1 ,pn ,sn ,ec ,dc ,ygmc2 ,rqsj2 ,zdmc2 ,prt2  from atpuecmlsh where ghtm='" + txtLsh.Text + "'";
            }
            DataTable dt = dataConn.GetTable(sql);
            dgvInsite.DataSource = dt;
        }

    }
}
