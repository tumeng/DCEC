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
using System.IO;
using DevExpress.Web.ASPxGridView;

//导出物料信息
//2014-06-06 TuMeng
//
//
namespace Rmes.WebApp.Rmes.Exp
{
    public partial class Exp1100 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<ProductLineEntity> all = ProductLineFactory.GetAll();
                ASPxComboBox1.DataSource = all;
                ASPxComboBox1.TextField = "PLINE_NAME";
                ASPxComboBox1.ValueField = "RMES_ID";
                ASPxComboBox1.DataBind();
            }
            BindData();
        }

        private void BindData()
        {
            //if (ASPxComboBox1.SelectedIndex < 0) return;
            //userManager theUserManager = (userManager)Session["theUserManager"];
            //string userID = theUserManager.getUserId();
            //string plineCode = ASPxComboBox1.SelectedItem.Value.ToString();
            //List<PlanEntity> plans = PlanFactory.GetByPline(plineCode);
            //plans = (from s in plans where s.ITEM_FLAG != "N" && s.CREATE_USER_ID == userID select s).ToList<PlanEntity>();
            //string projectCode = txtProjectCode.Text;
            //if (!string.IsNullOrWhiteSpace(projectCode))
            //{
            //    plans = (from s in plans where s.PROJECT_CODE.Equals(projectCode) select s).ToList<PlanEntity>();
            //}
            //string planSO = txtPlanSO.Text;
            //if (!string.IsNullOrWhiteSpace(planSO))
            //{
            //    plans = (from s in plans where s.PLAN_SO == planSO select s).ToList<PlanEntity>();
            //}
            //List<string> planCodes = new List<string>();
            //for (int i = 0; i < plans.Count; i++)
            //{
            //    planCodes.Add(plans[i].PLAN_CODE);
            //}
            //List<PlanBomEntity> dataSource = PlanBOMFactory.GetByPlanCodes(planCodes.ToArray());
            //ASPxGridView1.DataSource = dataSource;


            //List<string> orders = new List<string>();
            //List<string> _planCodes = new List<string>();
            //List<string> plines = new List<string>();
            //List<string> workUnits = new List<string>();
            //foreach (var e in dataSource)
            //{
            //    if ((!string.IsNullOrWhiteSpace(e.ORDER_CODE)) && (!orders.Contains(e.ORDER_CODE)))
            //        orders.Add(e.ORDER_CODE);
            //    if ((!string.IsNullOrWhiteSpace(e.PLAN_CODE)) && (!_planCodes.Contains(e.PLAN_CODE)))
            //        _planCodes.Add(e.PLAN_CODE);
            //    if ((!plines.Contains(e.PLINE_CODE)) && (!string.IsNullOrWhiteSpace(e.PLINE_CODE)))
            //        plines.Add(e.PLINE_CODE);
            //}
            //GridViewDataComboBoxColumn colOrder = ASPxGridView1.Columns["ORDER_CODE"] as GridViewDataComboBoxColumn;
            //colOrder.PropertiesComboBox.DataSource = orders;


            //GridViewDataComboBoxColumn colPlan = ASPxGridView1.Columns["PLAN_CODE"] as GridViewDataComboBoxColumn;
            //colPlan.PropertiesComboBox.DataSource = _planCodes;

            //GridViewDataComboBoxColumn colPline = ASPxGridView1.Columns["PLINE_CODE"] as GridViewDataComboBoxColumn;
            //List<ProductLineEntity> plineEntities = ProductLineFactory.GetByIDs(plines.ToArray());
            //colPline.PropertiesComboBox.DataSource = plineEntities;
            //colPline.PropertiesComboBox.ValueField = "RMES_ID";
            //colPline.PropertiesComboBox.TextField = "PLINE_NAME";
            
            //ASPxGridView1.DataBind();
        }

        protected void ASPxButton1_Click(object sender, EventArgs e)
        {
            BindData();
        }

        protected void btnXlsExport_Click(object sender, EventArgs e)
        {
            ASPxGridViewExporter1.WriteXlsToResponse("物料导出");
        }
    }
}