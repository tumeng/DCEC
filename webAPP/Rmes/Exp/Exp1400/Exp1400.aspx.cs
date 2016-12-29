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
    public partial class Exp1400 : BasePage
    {
        private dataConn dc = new dataConn();
        PubCs thePubCs = new PubCs();

        public string theCompanyCode, theUserID;

        public Database db = DB.GetInstance();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<ProductLineEntity> plineEntities = ProductLineFactory.GetAll();
                foreach (var p in plineEntities)
                {
                    ComPline.Items.Add(p.PLINE_NAME, p.PLINE_CODE);
                }
                cdate1.Value = DateTime.Today.AddDays(-3);
                cdate2.Value = DateTime.Today.AddDays(1);

                
            }
            LookUpItemBindData();
            BindData();
        }

        public void ASPxCallbackPanel1_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            //gridLookupItem.GridView.Selection.SelectAll();
        }

        public void LookUpItemBindData()
        {
            //string _b = cdate1.Date.ToShortDateString() + " 00:00:00";
            //string _e = cdate2.Date.ToShortDateString() + " 23:59:59";
            //string plineCode;
            
            //if (ComPline.SelectedItem != null)
            //{
            //    plineCode = ComPline.SelectedItem.Value.ToString();
            //}
            //else
            //{
            //    plineCode = "";
            //}
            

            //List<PlanEntity> plans = db.Fetch<PlanEntity>("where ORDER_CODE in(select AUFNR from IMES_DATA_STORE2LINE where WKDT between to_date(@0,'yyyy-mm-dd hh24:mi:ss') and to_date(@1,'yyyy-mm-dd hh24:mi:ss'))", _b, _e);
            //if (!string.IsNullOrEmpty(plineCode))
            //{
            //    plans = (from s in plans where s.PLINE_CODE == plineCode select s).ToList<PlanEntity>();
            //}
            //if (!string.IsNullOrEmpty(txtProjectCode.Text))
            //{
            //    plans = (from s in plans where s.PROJECT_CODE == txtProjectCode.Text.Trim().ToUpper() select s).ToList<PlanEntity>();
            //}
            //    gridLookupItem.GridView.DataSource = plans;
            //    gridLookupItem.GridView.DataBind();

            
            
            
        }

        public void BindData()
        {
            string plineCode;
            List<object> _planSO = gridLookupItem.GridView.GetSelectedFieldValues("PLAN_SO");
            if (_planSO == null || _planSO.Count == 0) return;
            List<object> _WBS_Code = gridLookupItem.GridView.GetSelectedFieldValues("WBS_CODE");
            if (_WBS_Code == null || _WBS_Code.Count == 0) return;
            if (ComPline.SelectedItem != null)
            {
                plineCode = ComPline.SelectedItem.Value.ToString();
            }
            else
            {
                plineCode = "";
            }
            //if (string.IsNullOrEmpty(plineCode)) return;
            string _b = cdate1.Date.ToShortDateString() + " 00:00:00";
            string _e = cdate2.Date.ToShortDateString() + " 23:59:59";

            List<LineSideStoreEntity> stocks = db.Fetch<LineSideStoreEntity>("");
            List<IMESStore2LineEntity> entity = db.Fetch<IMESStore2LineEntity>("where WKDT between to_date(@0,'yyyy-mm-dd hh24:mi:ss') and to_date(@1,'yyyy-mm-dd hh24:mi:ss')", _b, _e);
            List<PlanEntity> plans = db.Fetch<PlanEntity>("where ORDER_CODE in(select AUFNR from IMES_DATA_STORE2LINE where WKDT between to_date(@0,'yyyy-mm-dd hh24:mi:ss') and to_date(@1,'yyyy-mm-dd hh24:mi:ss'))", _b, _e);


            //如果是灌封制造单元，机构装配，母线加工单元，仪表箱装配则成品号显示WBS元素
            List<string> plines = new List<string>();
            plines.Add("M090");
            plines.Add("M101");
            plines.Add("M110");
            plines.Add("M102");
            
            if (_planSO!=null&&_planSO.Count>0)
            {
                //if (plines.Contains(plineCode))
                //{
                //    plans = (from s in plans where _WBS_Code.Contains(s.WBS_CODE) select s).ToList<PlanEntity>();

                //    entity = (from s in entity where plans.Select(m => m.ORDER_CODE).Contains(s.AUFNR) select s).ToList<IMESStore2LineEntity>();
                //}
                //else
                //{
                //    plans = (from s in plans where _planSO.Contains(s.PLAN_SO) select s).ToList<PlanEntity>();

                //    entity = (from s in entity where plans.Select(m => m.ORDER_CODE).Contains(s.AUFNR) select s).ToList<IMESStore2LineEntity>();
                //}
            }
            if (!string.IsNullOrEmpty(plineCode))
            {

                //entity = (from s in entity where (from st in stocks where st.PLINE_CODE == plineCode select st.STORE_CODE).Contains(s.TLGORT) select s).ToList<IMESStore2LineEntity>();

                plans = (from s in plans where s.PLINE_CODE == plineCode select s).ToList<PlanEntity>();
                entity = (from s in entity where plans.Select(m => m.ORDER_CODE).Contains(s.AUFNR) select s).ToList<IMESStore2LineEntity>();


            }
            ASPxGridView1.DataSource = entity;
            //如果是灌封制造单元，机构装配，母线加工单元，仪表箱装配则成品号显示WBS元素
            
            if (plines.Contains(plineCode))
            {
                XtraReport2 re = new XtraReport2(entity, _WBS_Code);
                ReportViewer1.Report = re;
            }
            else
            {
                XtraReport2 re = new XtraReport2(entity, _planSO);
                ReportViewer1.Report = re;
            }
            
            

            DataTable dt = new DataTable();
            dt.Columns.Add("Text");
            dt.Columns.Add("Value");
            dt.Rows.Add("初始状态", "0");
            dt.Rows.Add("传送中", "1");
            dt.Rows.Add("已传送", "2");

            GridViewDataComboBoxColumn colPrind = ASPxGridView1.Columns["PRIND"] as GridViewDataComboBoxColumn;
            colPrind.PropertiesComboBox.DataSource = dt;
            colPrind.PropertiesComboBox.TextField = "Text";
            colPrind.PropertiesComboBox.ValueField = "Value";

            DataTable workshopDT = new DataTable();
            workshopDT.Columns.Add("Value");
            workshopDT.Columns.Add("Text");
            workshopDT.Rows.Add("8101", "园区");
            workshopDT.Rows.Add("8102", "基地");
            GridViewDataComboBoxColumn workshopCol = ASPxGridView1.Columns["WERKS"] as GridViewDataComboBoxColumn;
            workshopCol.PropertiesComboBox.DataSource = workshopDT;
            workshopCol.PropertiesComboBox.ValueField = "Value";
            workshopCol.PropertiesComboBox.TextField = "Text";

            GridViewDataComboBoxColumn lineSide = ASPxGridView1.Columns["TLGORT"] as GridViewDataComboBoxColumn;
            lineSide.PropertiesComboBox.DataSource = stocks;
            lineSide.PropertiesComboBox.ValueField = "STORE_CODE";
            lineSide.PropertiesComboBox.TextField = "STORE_NAME";

            

            ASPxGridView1.DataBind();
        }

        public void ASPxButton1_Click(object sender, EventArgs e)
        {
            BindData();
        }
        protected void btnXlsExport_Click(object sender, EventArgs e)
        {
            ASPxGridViewExporter1.WriteXlsToResponse("拉料导出");
        }
    }
}