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
    public partial class ctrl_remark : BaseControl
    {
        private string CompanyCode, PlineCode, PlineID, StationCode, stationname, TheSo, TheSn, ThePlancode;
        //dataConn dc = new dataConn();
        ProductInfoEntity product;
        public ctrl_remark()
        {
            InitializeComponent();
            CompanyCode = LoginInfo.CompanyInfo.COMPANY_CODE;
            PlineID = LoginInfo.ProductLineInfo.RMES_ID;
            PlineCode = LoginInfo.ProductLineInfo.PLINE_CODE;
            StationCode = LoginInfo.StationInfo.RMES_ID;
            stationname = LoginInfo.StationInfo.STATION_NAME;
            txtRemark.Text = "";
            this.RMesDataChanged += new RMesEventHandler(ctrl_Tj_RMesDataChanged);
        }
        void ctrl_Tj_RMesDataChanged(object obj, Rmes.WinForm.Base.RMESEventArgs e)
        {
            RMESEventArgs arg = new RMESEventArgs();
            arg.MessageHead = "";
            arg.MessageBody = "";
            if (e.MessageHead == null) return;
            if (e.MessageHead == "SN" || e.MessageHead == "RECHECK" || e.MessageHead == "CHECKOK")
            {
                string sn = e.MessageBody.ToString();
                product = ProductInfoFactory.GetByCompanyCodeSNSingle(CompanyCode, sn);//获取sn信息
                string str1 = "";
                try
                {
                    str1 = dataConn.GetValue("select fr from data_sn_fr where sn='" + sn + "' and plan_code='" + product.PLAN_CODE + "'  ");
                }
                catch { }
                txtRemark.Text = str1 +"||"+ product.REMARK;
            }
        }

    }
}
