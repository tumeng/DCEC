using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rmes.Pub.Data;
using DevExpress.Utils;
using DevExpress.Web.ASPxGridView;
using Rmes.DA.Factory;

//徐莹 2016.9.8
//生产替换确认(多对多)

namespace Rmes.WebApp.Rmes.MmsDcec.mmsProductConfirm
{
    public partial class mmsMultisReplaceProductConfirm : System.Web.UI.Page
    {
        private dataConn dc = new dataConn();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dateFrom.Date = System.DateTime.Today;
                dateTo.Date = System.DateTime.Today;
            }
        }

        protected void cmbPline_Init(object sender, EventArgs e)
        {
            //显示生产线列表
            userManager theUserManager = (userManager)Session["theUserManager"];
            string userId = theUserManager.getUserId();
            string companyCode = theUserManager.getCompanyCode();

            string sql = "select pline_code,pline_name from vw_user_role_program a "
                + "where user_id='" + userId + "' and program_code='mmsMultiReplaceProductConfirm' and company_code='" + companyCode + "'";
            dc.setTheSql(sql);
            cmbPline.DataSource = dc.GetTable();
            cmbPline.TextField = "pline_name";
            cmbPline.ValueField = "pline_code";
            cmbPline.DataBind();
        }

        protected void gridMultiPlace_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            //显示待确认的替换
            setCondition();
        }
        private void setCondition()
        {
            string sql = "select *,case flag  when '2' then '库房已确认' when '1' then '生产已确认' when '0' then '未确认' end flagName "
                + "from SJBOMTHMUTICFM where to_date(to_char(tjsj,'yyyy-mm-dd'),'yyyy-mm-dd')>=to_date('" + dateFrom.Text
                + "','yyyy-mm-dd') AND to_date(to_char(tjsj,'yyyy-mm-dd'),'yyyy-mm-dd')<=to_date('" + dateTo.Text 
                + "','yyyy-mm-dd') AND GZDD='" + cmbPline.Value.ToString()
                + "' ORDER BY jhdm";
            gridMultiPlace.DataSource = dc.GetTable(sql);
            gridMultiPlace.DataBind();        
        }

        protected void gridMultiPlace_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomButtonEventArgs e)
        {
            //已确认的显示加入替换按钮，未确认的显示删除和确认按钮
            if (e.VisibleIndex < 0) return;

            string flag = gridMultiPlace.GetRowValues(e.VisibleIndex, "FLAG") as string;
            if (e.ButtonID == "Confirm")
            {
                if (flag == "0")
                    e.Visible = DefaultBoolean.True;
            }
        }

        protected void gridMultiPlace_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            string userName = theUserManager.getUserName();


            string THEGROUP = gridMultiPlace.GetRowValues(e.VisibleIndex, "THEGROUP") as string;
            string flag = gridMultiPlace.GetRowValues(e.VisibleIndex, "FLAG") as string;
            string JHDM = gridMultiPlace.GetRowValues(e.VisibleIndex, "JHDM") as string;
            string SO = gridMultiPlace.GetRowValues(e.VisibleIndex, "SO") as string;

            //确认
            if (e.ButtonID == "Confirm")
            {
                string sql = "update SJBOMTHMUTICFM set qrsj=sysdate,qryh='" + userName + "',flag='1' where jhdm='" + JHDM + "' and THEGROUP='" + THEGROUP + "' ";
                dc.ExeSql(sql);
            }

            setCondition();
        }
        protected void grid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string par = e.Parameters;
            string[] al = par.Split(',');
            string planCode = al[0].ToString();
            string locationCode = al[1].ToString();

            string sql = "select a.SN ,a.PLAN_SO ,a.PRODUCT_MODEL ,nvl(b.STATION_CODE,'下线') ZDMC  from DATA_PRODUCT a "
                + "left join data_store b on a.SN=b.SN where a.PLAN_CODE='" + planCode + "'";
            grid.DataSource = dc.GetTable(sql);
            grid.DataBind();
        }
    }
}