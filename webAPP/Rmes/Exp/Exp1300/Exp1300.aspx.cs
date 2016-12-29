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
 * 功能概述：SAP接口完工入库查询
 * 作    者：TuMeng
 * 创建时间：2014-08-25
 * 修改时间：
 */

namespace Rmes.WebApp.Rmes.Exp
{
    public partial class Exp1300 : BasePage
    {
        private dataConn dc = new dataConn();
        PubCs thePubCs = new PubCs();

        public string theCompanyCode, theUserID;

        public Database db = DB.GetInstance();
        protected void Page_Load(object sender, EventArgs e)
        {

            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserID = theUserManager.getUserId();
            if (!IsPostBack)
            {
                //cdate1.Value = DateTime.Today.AddDays(-3);
                //cdate2.Value = DateTime.Today.AddDays(1);
                List<ProductLineEntity> plineEntities = ProductLineFactory.GetAll();
                foreach (var p in plineEntities)
                {
                    ComPline.Items.Add(p.PLINE_NAME, p.PLINE_CODE);
                }
                
            }
            BindData();
            
            
        }

        public void BindData()
        {
            
            string orderCode = txtOrderCode.Text.ToUpper();
            string plineCode = ComPline.SelectedItem.Value.ToString();
            List<PlanEntity> plans = PlanFactory.GetByCreatePeriod(cdate1.Date, cdate2.Date);
            string _b = cdate1.Date.ToShortDateString() + " 00:00:00";
            string _e = cdate2.Date.ToShortDateString() + " 23:59:59";
            List<IMESCompleteInstoreEntity> entity = db.Fetch<IMESCompleteInstoreEntity>("where aufnr in (select ORDER_CODE from DATA_PLAN where create_time between to_date(@0,'yyyy-mm-dd hh24:mi:ss') and to_date(@1,'yyyy-mm-dd hh24:mi:ss'))", _b, _e);
            UserEntity users = UserFactory.GetByUserCode(theUserID);


            if (!string.IsNullOrWhiteSpace(orderCode))
            {
                //entity = (from s in entity where (from pl in plans where pl.PROJECT_CODE == orderCode.ToUpper() && pl.RUN_FLAG == "F" select pl.ORDER_CODE).Contains(s.AUFNR) select s).ToList<IMESCompleteInstoreEntity>();
            }
            if (!ComPline.SelectedItem.Value.ToString().Equals("All"))
            {
                entity = (from s in entity where (from pl in plans where pl.PLINE_CODE == plineCode && pl.RUN_FLAG =="F" select pl.ORDER_CODE).Contains(s.AUFNR) select s).ToList<IMESCompleteInstoreEntity>();
            }
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("AUFNR");
            //ASPxGridView1.DataSource = entity;
            XtraReport1 re = new XtraReport1(entity,theUserID,orderCode);
            ReportViewer1.Report = re;
      

            //DataTable dt = new DataTable();
            //dt.Columns.Add("Text");
            //dt.Columns.Add("Value");
            //dt.Rows.Add("初始状态", "0");
            //dt.Rows.Add("传送中", "1");
            //dt.Rows.Add("已传送", "2");

            //GridViewDataComboBoxColumn colPrind = ASPxGridView1.Columns["PRIND"] as GridViewDataComboBoxColumn;
            //colPrind.PropertiesComboBox.DataSource = dt;
            //colPrind.PropertiesComboBox.TextField = "Text";
            //colPrind.PropertiesComboBox.ValueField = "Value";

            //DataTable workshopDT = new DataTable();
            //workshopDT.Columns.Add("Value");
            //workshopDT.Columns.Add("Text");
            //workshopDT.Rows.Add("8101", "园区");
            //workshopDT.Rows.Add("8102", "基地");
            //GridViewDataComboBoxColumn workshopCol = ASPxGridView1.Columns["WERKS"] as GridViewDataComboBoxColumn;
            //workshopCol.PropertiesComboBox.DataSource = workshopDT;
            //workshopCol.PropertiesComboBox.ValueField = "Value";
            //workshopCol.PropertiesComboBox.TextField = "Text";
            
            
            //ASPxGridView1.DataBind();


          

        }

       

        public void ASPxButton1_Click(object sender, EventArgs e)
        {

            //BindData();
            
        }
        //protected void btnXlsExport_Click(object sender, EventArgs e)
        //{
        //    ASPxGridViewExporter1.WriteXlsToResponse("完工入库导出");
        //}
    }
}