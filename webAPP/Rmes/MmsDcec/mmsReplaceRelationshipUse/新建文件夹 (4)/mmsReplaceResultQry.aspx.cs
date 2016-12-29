using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rmes.Pub.Data;

namespace Rmes.WebApp.Rmes.MmsDcec.mmsReplaceRelationshipUse
{
    public partial class mmsReplaceResultQry : System.Web.UI.Page
    {
        private dataConn dc = new dataConn();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DatePlan_Init(object sender, EventArgs e)
        {
            DatePlan.Date = System.DateTime.Today;
        }

        protected void cmbPlan_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string planDate = e.Parameter;
            string sql= "select plan_code from data_plan where pline_code='" + Request["plineCode"].ToString()
                + "' and BEGIN_DATE = to_date('" + DatePlan.Text + "','YYYY-MM-DD') order by plan_code";
            cmbPlan.DataSource = dc.GetTable(sql);
            cmbPlan.TextField = "plan_code";
            cmbPlan.ValueField = "plan_code";
            cmbPlan.DataBind();
        }

        protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            string sql = "select JHDM ,so ,ljdm1 ,ljdm2 ,gwmc ,to_char(lrsj,'YYYY-MM-DD HH24:MI:SS') lrsj "
             + ",bz ,ygmc ,qrygmc ,to_char(qrsj,'YYYY-MM-DD HH24:MI;SS') qrsj,istrue  from sjbomsoth_manual "
             + " where gzdd='" + Request["plineCode"].ToString() + "' ";

            if (DatePlan.Text != "")
                sql += " and jhrq=to_date('" + DatePlan.Text + "','YYYY-MM-DD') ";


            if (cmbPlan.Text != "")
                sql += " and jhdm='" + cmbPlan.Text + "'";


            sql += " order by jhdm,ljdm1";
            ASPxGridView1.DataSource = dc.GetTable(sql);
            ASPxGridView1.DataBind();
        }

        protected void ASPxGridView1_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            if (e.VisibleIndex < 0) return;
            string status = e.GetValue("ISTRUE").ToString(); //已确认绿色
            if (status == "1")
            {
                e.Row.BackColor = System.Drawing.Color.YellowGreen;
            }
        }
    }
}