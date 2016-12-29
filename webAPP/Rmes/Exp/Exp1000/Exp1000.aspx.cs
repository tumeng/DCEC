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
using DevExpress.Web.ASPxGridView;
using System.Data;

namespace Rmes.WebApp.Rmes.Exp
{
    public partial class Exp1000 : BasePage
    {
        private dataConn dc = new dataConn();
        PubCs thePubCs = new PubCs();

        public string theCompanyCode, theUserID;

        public Database db = DB.GetInstance();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //List<ProductLineEntity> all = ProductLineFactory.GetAll();
                //ASPxComboBox1.DataSource = all;
                //ASPxComboBox1.TextField = "PLINE_NAME";
                //ASPxComboBox1.ValueField = "RMES_ID";
                //ASPxComboBox1.DataBind();
            }
            BindData();
        }

        protected void ASPxButton1_Click(object sender, EventArgs e)
        {
            BindData();
        }

        private void BindData()
        {
            ////if (ASPxComboBox1.SelectedIndex < 0) return;
            //userManager theUserManager = (userManager)Session["theUserManager"];
            //string userID = theUserManager.getUserId();
            ////string plineCode = ASPxComboBox1.SelectedItem.Value.ToString();
            //List<PlanEntity> planEntities = PlanFactory.GetByUserID(userID);
            //List<PlanEntity> dataSource = (from s in planEntities where (s.RUN_FLAG == "N" || s.RUN_FLAG == "Y") && s.CREATE_USER_ID == userID select s).ToList<PlanEntity>();

            //List<string> orders = new List<string>();
            //List<string> plans = new List<string>();
            //List<string> plines = new List<string>();
            //List<string> workUnits = new List<string>();
            //foreach (var e in dataSource)
            //{
            //    if ((!string.IsNullOrWhiteSpace(e.ORDER_CODE)) && (!orders.Contains(e.ORDER_CODE)))
            //        orders.Add(e.ORDER_CODE);
            //    if ((!string.IsNullOrWhiteSpace(e.PLAN_CODE)) && (!plans.Contains(e.PLAN_CODE)))
            //        plans.Add(e.PLAN_CODE);
            //    if ((!plines.Contains(e.PLINE_CODE)) && (!string.IsNullOrWhiteSpace(e.PLINE_CODE)))
            //        plines.Add(e.PLINE_CODE);
            //}
            //GridViewDataComboBoxColumn colOrder = ASPxGridView1.Columns["ORDER_CODE"] as GridViewDataComboBoxColumn;
            //colOrder.PropertiesComboBox.DataSource = orders;


            //GridViewDataComboBoxColumn colPlan = ASPxGridView1.Columns["PLAN_CODE"] as GridViewDataComboBoxColumn;
            //colPlan.PropertiesComboBox.DataSource = plans;

            //GridViewDataComboBoxColumn colPline = ASPxGridView1.Columns["PLINE_CODE"] as GridViewDataComboBoxColumn;
            //List<ProductLineEntity> plineEntities = ProductLineFactory.GetByIDs(plines.ToArray());
            //colPline.PropertiesComboBox.DataSource = plineEntities;
            //colPline.PropertiesComboBox.ValueField = "RMES_ID";
            //colPline.PropertiesComboBox.TextField = "PLINE_NAME";


            //DataTable itemDT = new DataTable();
            //itemDT.Columns.Add("Value");
            //itemDT.Columns.Add("Text");
            //itemDT.Rows.Add("Y", "已收料");
            //itemDT.Rows.Add("B", "已发料单");
            //itemDT.Rows.Add("N", "未确认");
            //GridViewDataComboBoxColumn itemCol = ASPxGridView1.Columns["ITEM_FLAG"] as GridViewDataComboBoxColumn;
            //itemCol.PropertiesComboBox.DataSource = itemDT;
            //itemCol.PropertiesComboBox.ValueField = "Value";
            //itemCol.PropertiesComboBox.TextField = "Text";


            //DataTable runDT = new DataTable();
            //runDT.Columns.Add("Value");
            //runDT.Columns.Add("Text");
            //runDT.Rows.Add("Y", "执行中");
            //runDT.Rows.Add("N", "未执行");
            //runDT.Rows.Add("P", "暂停中");
            //runDT.Rows.Add("F", "已完成");
            //GridViewDataComboBoxColumn runCol = ASPxGridView1.Columns["RUN_FLAG"] as GridViewDataComboBoxColumn;
            //runCol.PropertiesComboBox.DataSource = runDT;
            //runCol.PropertiesComboBox.ValueField = "Value";
            //runCol.PropertiesComboBox.TextField = "Text";


            //ASPxGridView1.DataSource = dataSource;

            
            //ASPxGridView1.DataBind();
        }

        protected void ASPxGridView2_DataSelect(object sender, EventArgs e)
        {
            ASPxGridView grid = (ASPxGridView)sender;
            string id = grid.GetMasterRowFieldValues("PLAN_CODE").ToString();

            //string id = (sender as ASPxGridView).GetMasterRowKeyValue().ToString();

            //string sql = "select * from data_plan_standard_bom where plan_code='"+id+"'";
            //DataTable dt = dc.GetTable(sql);
            //List<PlanStandardBOMEntity> allEntity = db.Fetch<PlanStandardBOMEntity>("where plan_code=@0",id);
            DataTable dataSource = dc.GetTable("select t.*,p.process_name,p.standard_machine_worktime,p.standard_man_worktime,p.machine_worktime_unit,p.man_worktime_unit from DATA_PLAN_BOM t left join DATA_PLAN_PROCESS p on t.process_code=p.process_code and t.plan_code=p.plan_code where t.plan_code='" + id + "'");
            List<PlanBomEntity> allEntity = db.Fetch<PlanBomEntity>("where plan_code=@0 order by flag,item_code", id);

            grid.DataSource = dataSource;
            GridViewDataComboBoxColumn comboCol = grid.Columns["PLINE_CODE"] as GridViewDataComboBoxColumn;
            comboCol.PropertiesComboBox.DataSource = ProductLineFactory.GetAll();
            comboCol.PropertiesComboBox.ValueField = "RMES_ID";
            comboCol.PropertiesComboBox.TextField = "PLINE_NAME";


            DataTable dt = new DataTable();
            dt.Columns.Add("VIEW");
            dt.Columns.Add("VALUE");
            dt.Rows.Add("新建", "N");
            dt.Rows.Add("已发料单", "B");
            dt.Rows.Add("部分收料", "R");
            dt.Rows.Add("已收料", "Y");
            GridViewDataComboBoxColumn colFlag = grid.Columns["FLAG"] as GridViewDataComboBoxColumn;
            colFlag.PropertiesComboBox.DataSource = dt;
            colFlag.PropertiesComboBox.ValueField = "VALUE";
            colFlag.PropertiesComboBox.TextField = "VIEW";
        }

        protected void btnXlsExport_Click(object sender, EventArgs e)
        {
            ASPxGridViewExporter1.WriteXlsToResponse("导出计划");
        }
    }
}