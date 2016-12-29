using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//以下引用，如果采用实体类，请全部复制到新页面
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.Pub.Data;
using Rmes.Web.Base;
using System.Data;
using DevExpress.Web.ASPxGridView;

/**
 * 功能概述：SAP接口线边拉料查询
 * 作    者：TuMeng
 * 创建时间：2014-08-25
 * 修改时间：
 */

namespace Rmes.WebApp.Rmes.Exp
{
    public partial class Exp1500 : BasePage
    {
        private dataConn dc = new dataConn();
        PubCs thePubCs = new PubCs();

        public string theCompanyCode, theUserID;

        public Database db = DB.GetInstance();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                cdate1.Value = DateTime.Today.AddDays(-3);
                cdate2.Value = DateTime.Today.AddDays(1);

               
            }
            
            BindData();
        }

        //public void ASPxCallbackPanel1_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        //{
        //    string _b = cdate1.Date.ToShortDateString() + " 00:00:00";
        //    string _e = cdate2.Date.ToShortDateString() + " 23:59:59";
        //    string plineCode = ComPline.SelectedItem.Value.ToString();
        //    List<PlanEntity> plans = db.Fetch<PlanEntity>("where ORDER_CODE in(select AUFNR from IMES_DATA_STORE2LINE where WKDT between to_date(@0,'yyyy-mm-dd hh24:mi:ss') and to_date(@1,'yyyy-mm-dd hh24:mi:ss'))", _b, _e);
        //    if (!ComPline.SelectedItem.Value.ToString().Equals("All"))
        //    {
        //        plans = (from s in plans where s.PLINE_CODE == plineCode select s).ToList<PlanEntity>();

        //    }
        //    gridLookupItem.GridView.DataSource = plans;
        //    gridLookupItem.GridView.DataBind();
        //    gridLookupItem.GridView.Selection.SelectAll();
        //}

        public void BindData()
        {
            userManager userManage=(userManager)Session["theUserManager"];
            theUserID = userManage.getUserId();

            string _b = cdate1.Date.ToShortDateString() + " 00:00:00";
            string _e = cdate2.Date.ToShortDateString() + " 23:59:59";

            List<LineSideStoreEntity> stocks = db.Fetch<LineSideStoreEntity>("");
            List<ItemLineSideStore2LineEntity> entity = db.Fetch<ItemLineSideStore2LineEntity>("where CREATE_TIME between to_date(@0,'yyyy-mm-dd hh24:mi:ss') and to_date(@1,'yyyy-mm-dd hh24:mi:ss') and CREATE_USER_ID = @2", _b, _e,theUserID);

            //ASPxGridView1.DataSource = entity;
            XtraReport4 re = new XtraReport4(entity);
            ReportViewer1.Report = re;
            

           

            //DataTable workshopDT = new DataTable();
            //workshopDT.Columns.Add("Value");
            //workshopDT.Columns.Add("Text");
            //workshopDT.Rows.Add("8101", "园区");
            //workshopDT.Rows.Add("8102", "基地");
            //GridViewDataComboBoxColumn workshopCol = ASPxGridView1.Columns["WORKSHOP"] as GridViewDataComboBoxColumn;
            //workshopCol.PropertiesComboBox.DataSource = workshopDT;
            //workshopCol.PropertiesComboBox.ValueField = "Value";
            //workshopCol.PropertiesComboBox.TextField = "Text";

            //GridViewDataComboBoxColumn lineSide = ASPxGridView1.Columns["T_LINESIDESTORE"] as GridViewDataComboBoxColumn;
            //lineSide.PropertiesComboBox.DataSource = stocks;
            //lineSide.PropertiesComboBox.ValueField = "STORE_CODE";
            //lineSide.PropertiesComboBox.TextField = "STORE_NAME";

            

            //ASPxGridView1.DataBind();
        }

        public void ASPxButton1_Click(object sender, EventArgs e)
        {
            //BindData();
        }
        protected void btnXlsExport_Click(object sender, EventArgs e)
        {
            ASPxGridViewExporter1.WriteXlsToResponse("拉料导出");
        }
    }
}