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

namespace Rmes.WebApp.Rmes.Exp
{
    public partial class Exp1200 : BasePage
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
                cdate1.Value = DateTime.Today.AddDays(-1);
                cdate2.Value = DateTime.Today;
                List<ProductLineEntity> plineEntities = ProductLineFactory.GetAll();
                foreach (var p in plineEntities)
                {
                    ComPline.Items.Add(p.PLINE_NAME, p.PLINE_CODE);
                }

            }
            LookUpItemBindData();
            BindData();
        }


        public void ASPxCallbackPanel1_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            gridLookupItem.GridView.Selection.SelectAll();
        }

        public void LookUpItemBindData()
        {
            string _b = cdate1.Date.ToShortDateString() + " 00:00:00";
            string _e = cdate2.Date.ToShortDateString() + " 23:59:59";
            string plineCode;

            if (string.IsNullOrEmpty(txtProjectCode.Text)) return;
            string projectCode = txtProjectCode.Text.ToUpper();

            if (ComPline.SelectedItem != null)
            {
                plineCode = ComPline.SelectedItem.Value.ToString();
            }
            else
            {
                plineCode = "";
            }
            
            List<PlanEntity> plans = db.Fetch<PlanEntity>("where CREATE_TIME between to_date(@0,'yyyy-mm-dd hh24:mi:ss') and to_date(@1,'yyyy-mm-dd hh24:mi:ss')", _b, _e);

            if (!string.IsNullOrEmpty(plineCode))
            {
                plans = (from s in plans where s.PLINE_CODE == plineCode select s).ToList<PlanEntity>();
            }
            if (!string.IsNullOrEmpty(txtProjectCode.Text))
            {
                //plans = (from s in plans where s.PROJECT_CODE == txtProjectCode.Text.Trim().ToUpper() select s).ToList<PlanEntity>();
            }
            gridLookupItem.GridView.DataSource = plans;
            gridLookupItem.GridView.DataBind();




        }

        public void BindData()
        {
            string plineCode;
            List<object> _planSO = gridLookupItem.GridView.GetSelectedFieldValues("WBS_CODE");
            if (_planSO == null || _planSO.Count == 0) return;
            if (ComPline.SelectedItem != null)
            {
                plineCode = ComPline.SelectedItem.Value.ToString();
            }
            else
            {
                plineCode = "";
            }
            //if (string.IsNullOrEmpty(plineCode)) return;
            List<PlanEntity> entity = PlanFactory.GetByCreatePeriod(cdate1.Date, cdate2.Date);


            if (_planSO != null && _planSO.Count > 0)
            {
                //entity = (from s in entity where _planSO.Contains(s.WBS_CODE) select s).ToList<PlanEntity>();
            }

            if (!ComPline.SelectedItem.Value.ToString().Equals("All"))
            {
                entity = (from s in entity where s.PLINE_CODE == plineCode select s).ToList<PlanEntity>();
            }
            ASPxGridView1.DataSource = entity;
            XtraReport3 re = new XtraReport3(entity);
            ReportViewer1.Report = re;
            GridViewDataComboBoxColumn colPline = ASPxGridView1.Columns["PLINE_CODE"] as GridViewDataComboBoxColumn;
            List<ProductLineEntity> plineEntities = ProductLineFactory.GetAll();
            colPline.PropertiesComboBox.DataSource = plineEntities;
            colPline.PropertiesComboBox.ValueField = "RMES_ID";
            colPline.PropertiesComboBox.TextField = "PLINE_NAME";

            DataTable itemDT = new DataTable();
            itemDT.Columns.Add("Value");
            itemDT.Columns.Add("Text");
            itemDT.Rows.Add("Y", "已收料");
            itemDT.Rows.Add("B", "已发料单");
            itemDT.Rows.Add("N", "未确认");
            GridViewDataComboBoxColumn itemCol = ASPxGridView1.Columns["ITEM_FLAG"] as GridViewDataComboBoxColumn;
            itemCol.PropertiesComboBox.DataSource = itemDT;
            itemCol.PropertiesComboBox.ValueField = "Value";
            itemCol.PropertiesComboBox.TextField = "Text";


            DataTable runDT = new DataTable();
            runDT.Columns.Add("Value");
            runDT.Columns.Add("Text");
            runDT.Rows.Add("Y", "执行中");
            runDT.Rows.Add("N", "未执行");
            runDT.Rows.Add("P", "暂停中");
            runDT.Rows.Add("F", "已完成");
            GridViewDataComboBoxColumn runCol = ASPxGridView1.Columns["RUN_FLAG"] as GridViewDataComboBoxColumn;
            runCol.PropertiesComboBox.DataSource = runDT;
            runCol.PropertiesComboBox.ValueField = "Value";
            runCol.PropertiesComboBox.TextField = "Text";


            List<UserEntity> allusers = UserFactory.GetAll();
            GridViewDataComboBoxColumn create_user = ASPxGridView1.Columns["CREATE_USER_ID"] as GridViewDataComboBoxColumn;
            create_user.PropertiesComboBox.DataSource = allusers;
            create_user.PropertiesComboBox.ValueField = "USER_ID";
            create_user.PropertiesComboBox.TextField = "USER_NAME";
            DataTable typeDT = new DataTable();
            typeDT.Columns.Add("Value");
            typeDT.Columns.Add("Text");
            typeDT.Rows.Add("ZP01", "标准");
            typeDT.Rows.Add("ZP02", "返工");
            typeDT.Rows.Add("ZP03", "拆解");
            GridViewDataComboBoxColumn typeCol = ASPxGridView1.Columns["PLAN_TYPE_CODE"] as GridViewDataComboBoxColumn;
            typeCol.PropertiesComboBox.DataSource = typeDT;
            typeCol.PropertiesComboBox.ValueField = "Value";
            typeCol.PropertiesComboBox.TextField = "Text";


            ASPxGridView1.DataBind();
        }
        public void ASPxButton1_Click(object sender, EventArgs e)
        {

            BindData();

        }

        protected void btnXlsExport_Click(object sender, EventArgs e)
        {
            ASPxGridViewExporter1.WriteXlsToResponse("计划导出");
        }
    }
}