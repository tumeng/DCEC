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

//徐莹 2016.9.5
//库房删除确认（一对一）

namespace Rmes.WebApp.Rmes.MmsDcec.mmsWarehouseConfirm
{
    public partial class mmsOneReplaceDelWhConfirm : System.Web.UI.Page
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
                + "where user_id='" + userId + "' and program_code='mmsOneReplaceDelWhConfirm' and company_code='" + companyCode + "'";
            dc.setTheSql(sql);
            cmbPline.DataSource = dc.GetTable();
            cmbPline.TextField = "pline_name";
            cmbPline.ValueField = "pline_code";
            cmbPline.DataBind();
        }

        protected void gridOnePlace_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            //显示待确认的替换
            setCondition();
        }
        private void setCondition()
        {
            string sql = "select jhdm,so ,ljdm1 ,ljmc1 ,ljdm2 ,ljmc2 ,gwdm ,tjsj ,qrsj ,tjyh ,qryh ,gzdd ,flag,case flag when '2' then '库房已确认' when '1' then '生产已确认' when '0' then '未确认' end flagName "
                + "from sjbomthcfm_del where to_date(to_char(tjsj,'yyyy-mm-dd'),'yyyy-mm-dd')>=to_date('" + dateFrom.Text
                + "','yyyy-mm-dd') AND to_date(to_char(tjsj,'yyyy-mm-dd'),'yyyy-mm-dd')<=to_date('" + dateTo.Text 
                + "','yyyy-mm-dd') AND GZDD='" + cmbPline.Value.ToString()
                + "' ORDER BY jhdm,gwdm";
            gridOnePlace.DataSource = dc.GetTable(sql);
            gridOnePlace.DataBind();        
        }

        protected void gridOnePlace_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomButtonEventArgs e)
        {
            //已库房确认的显示加入替换按钮，未确认的显示删除按钮，已生产确认的显示确认按钮
            if (e.VisibleIndex < 0) return;

            string flag = gridOnePlace.GetRowValues(e.VisibleIndex, "FLAG") as string;
            if (e.ButtonID == "Confirm")
            {
                if (flag == "1")
                    e.Visible = DefaultBoolean.True;
            }
            if (e.ButtonID == "Delete")
            {
                if (flag == "0")
                    e.Visible = DefaultBoolean.True;
            }
        }

        protected void gridOnePlace_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            string userName = theUserManager.getUserName();


            string QRYH = gridOnePlace.GetRowValues(e.VisibleIndex, "QRYH") as string;
            string flag = gridOnePlace.GetRowValues(e.VisibleIndex, "FLAG") as string;
            string JHDM = gridOnePlace.GetRowValues(e.VisibleIndex, "JHDM") as string;
            string SO = gridOnePlace.GetRowValues(e.VisibleIndex, "SO") as string;
            string LJDM1 = gridOnePlace.GetRowValues(e.VisibleIndex, "LJDM1") as string;
            string LJDM2 = gridOnePlace.GetRowValues(e.VisibleIndex, "LJDM2") as string;
            string GWDM = gridOnePlace.GetRowValues(e.VisibleIndex, "GWDM") as string;

            //删除
            if (e.ButtonID == "Delete")
            {
                string sql = "delete from  sjbomthcfm_del  where jhdm='" + JHDM + "' and so='" + SO + "' and ljdm1='" + LJDM1
                    + "' and upper(gwdm)='" + GWDM + "'";
                dc.ExeSql(sql);
            }            
            //确认
            if (e.ButtonID == "Confirm")
            {
                string sql = "delete FROM SJBOMSOTH WHERE JHDM='" + JHDM + "' and ljdm1='" + LJDM1 + "' and ljdm2='" + LJDM2 + "' and so='" + SO + "' AND GWMC='" + GWDM + "' AND GZDD='" + cmbPline.Value.ToString() + "' and ygmc='" + userName + "'";
                dc.ExeSql(sql);

                sql = "delete from  sjbomthcfm_del  where jhdm='" + JHDM + "' and so='" + SO + "' and ljdm1='" + LJDM1 + "' and upper(gwdm)='" + GWDM + "'";
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

            string sql = "select a.SN ,a.PLAN_SO ,a.PRODUCT_MODEL ,nvl(b.STATION_CODE,'下线') ZDMC,IS_PASSGW(A.SN,'" + locationCode
                + "') GW  from DATA_PRODUCT a "
                + "left join data_store b on a.SN=b.SN where a.PLAN_CODE='" + planCode + "'";
            grid.DataSource = dc.GetTable(sql);
            grid.DataBind();
        }
    }
}