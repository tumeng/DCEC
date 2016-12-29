using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.Pub.Data;
using Rmes.Web.Base;
using System.Data;
using DevExpress.Web.ASPxGridView;

namespace Rmes.WebApp.Rmes.Interface.inf2400
{
    public partial class inf2400 : BasePage
    {
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
        public void BindData()
        {
            

            string _b = cdate1.Date.ToString("yyyyMMdd") + "000000";
            string _e = cdate2.Date.ToString("yyyyMMdd") + "235959";
            List<IMESPlanProcessEntity> entity = db.Fetch<IMESPlanProcessEntity>("where SERIAL between @0 and @1 order by AUFNR", _b, _e);

            string planSO = txtPlanSO.Text;
            string orderCode = "";
            if (!string.IsNullOrEmpty(planSO))
            {

                List<PlanEntity> plan = PlanFactory.GetByPlanSO(planSO);


                if (plan != null && plan.Count > 0)
                {
                    orderCode = plan[0].ORDER_CODE;
                }

                if (!string.IsNullOrEmpty(orderCode))
                {
                    entity = (from s in entity where s.AUFNR == orderCode select s).ToList<IMESPlanProcessEntity>();
                }
            }

            ASPxGridView1.DataSource = entity;

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
            

            DataTable dt1 = new DataTable();
            dt1.Columns.Add("Text");
            dt1.Columns.Add("Value");
            dt1.Rows.Add("完成", "X");
          

            GridViewDataComboBoxColumn colPrind1 = ASPxGridView1.Columns["OPFLG"] as GridViewDataComboBoxColumn;
            colPrind1.PropertiesComboBox.DataSource = dt1;
            colPrind1.PropertiesComboBox.TextField = "Text";
            colPrind1.PropertiesComboBox.ValueField = "Value";

            ASPxGridView1.DataBind();
        }

        public void ASPxButton1_Click(object sender, EventArgs e)
        {
            string planSO = txtPlanSO.Text;

            if (string.IsNullOrWhiteSpace(planSO))
            {
                Response.Write("<script>alert('成品号不能为空！');</script>");
                return;
            }

            List<PlanEntity> plan = PlanFactory.GetByPlanSO(planSO);
            string orderCode = "";

            if (plan != null && plan.Count > 0)
            {
                orderCode = plan[0].ORDER_CODE;
            }
            List<IMESPlanProcessEntity> entity = IMESPlanProcessFactory.GetByOrderCode(orderCode);
            if (entity == null || entity.Count == 0)
            {
                Response.Write("<script>alert('没有该订单号的相关记录！');</script>");
                return;
            }
            ASPxGridView1.DataSource = entity;
            ASPxGridView1.DataBind();
        }
    }
}