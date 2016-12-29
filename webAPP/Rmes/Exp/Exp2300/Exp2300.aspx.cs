using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.Pub.Data;
using Rmes.Web.Base;
using System.Data;

namespace Rmes.WebApp.Rmes.Exp.Exp2300
{
    public partial class Exp2300 : System.Web.UI.Page
    {
        private dataConn dc = new dataConn();
        PubCs thePubCs = new PubCs();
        public Database db = DB.GetInstance();
        protected void Page_Load(object sender, EventArgs e)
        {
            BindData();
        }
        public void BindData()
        {
            string sn = txtSN.Text.ToUpper();
            DataTable dt = dc.GetTable("select * from DATA_SN_DETECT_DATA where SN ='"+sn+"'");
            Report_Exp2300 re = new Report_Exp2300(dt);
            ReportViewer1.Report = re;
        }
        public void ASPxButton1_Click(object sender, EventArgs e)
        {
           
        }
    }
}