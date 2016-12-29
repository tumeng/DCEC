using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rmes.Pub.Data;

//徐莹 2016.9.5
//库房删除确认（一对一）

namespace Rmes.WebApp.Rmes.MmsDcec.mmsWarehouseConfirm
{
    public partial class mmsWarehouseDelConfirmOne : System.Web.UI.Page
    {
        private dataConn dc = new dataConn();
        private string userId, companyCode;
        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            userId = theUserManager.getUserId();
            companyCode = theUserManager.getCompanyCode();

            if (!IsPostBack)
            {
                dateFrom.Date = System.DateTime.Today;
                dateTo.Date = System.DateTime.Today;
            }
        }

        protected void cmbPline_Init(object sender, EventArgs e)
        {
            //显示生产线列表
            string sql = "select pline_code,pline_name from vw_user_role_program a "
                + "where user_id='" + userId + "' and program_code='mmsWarehouseDelConfirmOne' and company_code='" + companyCode + "'";
            dc.setTheSql(sql);
            cmbPline.DataSource = dc.GetTable();
            cmbPline.TextField = "pline_name";
            cmbPline.ValueField = "pline_code";
            cmbPline.DataBind();
        }

        protected void gridOnePlace_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            string sql = "select jhdm,so ,ljdm1 ,ljmc1 ,ljdm2 ,ljmc2 ,gwdm ,tjsj ,qrsj ,tjyh ,qryh ,gzdd ,flag "
                + "from sjbomthcfm_del where to_date(to_char(tjsj,'yyyy-mm-dd'),'yyyy-mm-dd')>='" + dateFrom.Text
                + "' AND to_date(to_char(tjsj,'yyyy-mm-dd'),'yyyy-mm-dd')<='" + dateFrom.Text + "' AND GZDD='" + cmbPline.Value.ToString()
                + "' ORDER BY jhdm,gwdm";
            gridOnePlace.DataSource = dc.GetTable(sql);
            gridOnePlace.DataBind();
        }
    }
}