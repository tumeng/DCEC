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
using System.Collections.Generic;
using Rmes.DA.Procedures;
using DevExpress.Web.ASPxGridView;
using System.Data;


/**
 * 功能概述：物料消耗记录查询
 * 作    者：涂猛
 * 创建时间：2014-08-15
 */

namespace Rmes.WebApp.Rmes.Inv.inv2500
{
    public partial class inv2500 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<SNBomEntity> entity = SNBomFactory.GetAll();

                List<string> orders = new List<string>();
                List<string> plines = new List<string>();
                List<string> plans = new List<string>();

                orders = (from s in entity select s.ORDER_CODE).Distinct().ToList<string>();
                plans = entity.Select(s => s.PLAN_CODE).Distinct().ToList<string>();
                foreach (var en in entity)
                {
                    if ((!plines.Contains(en.PLINE_CODE)) && (!string.IsNullOrWhiteSpace(en.PLINE_CODE)))
                        plines.Add(en.PLINE_CODE);
                }
                List<ProductLineEntity> plineEntities = ProductLineFactory.GetByIDs(plines.ToArray());

                foreach (var o in orders)
                {
                    orderCode.Items.Add(o, o);
                }
                foreach (var p in plans)
                {
                    planCode.Items.Add(p, p);
                }
                foreach (var p in plineEntities)
                {
                    plineCode.Items.Add(p.PLINE_NAME, p.PLINE_CODE);
                }
            }
            BindData();
        }

        private void BindData()
        {
            List<SNBomEntity> entity = SNBomFactory.GetAll();

            if (orderCode.SelectedItem != null&&!orderCode.SelectedItem.Value.ToString().Equals("All"))
            {
                entity = (from s in entity where s.ORDER_CODE.Equals(orderCode.SelectedItem.Value.ToString()) select s).ToList<SNBomEntity>();
            }
            if (planCode.SelectedItem != null && !planCode.SelectedItem.Value.ToString().Equals("All"))
            {
                entity = (from s in entity where s.PLAN_CODE.Equals(planCode.SelectedItem.Value.ToString()) select s).ToList<SNBomEntity>();
            }
            if (plineCode.SelectedItem != null && !plineCode.SelectedItem.Value.ToString().Equals("All"))
            {
                entity = (from s in entity where s.PLINE_CODE.Equals(plineCode.SelectedItem.Value.ToString()) select s).ToList<SNBomEntity>();
            }

            ASPxGridView1.DataSource = entity;

            List<string> orders = new List<string>();
            List<string> plans = new List<string>();
            List<string> plines = new List<string>();
            foreach (var e in entity)
            {
                if ((!string.IsNullOrWhiteSpace(e.ORDER_CODE)) && (!orders.Contains(e.ORDER_CODE)))
                    orders.Add(e.ORDER_CODE);
                if ((!string.IsNullOrWhiteSpace(e.PLAN_CODE)) && (!plans.Contains(e.PLAN_CODE)))
                    plans.Add(e.PLAN_CODE);
                if ((!plines.Contains(e.PLINE_CODE)) && (!string.IsNullOrWhiteSpace(e.PLINE_CODE)))
                    plines.Add(e.PLINE_CODE);
            }
            GridViewDataComboBoxColumn colOrder = ASPxGridView1.Columns["ORDER_CODE"] as GridViewDataComboBoxColumn;
            colOrder.PropertiesComboBox.DataSource = orders;


            GridViewDataComboBoxColumn colPlan = ASPxGridView1.Columns["PLAN_CODE"] as GridViewDataComboBoxColumn;
            colPlan.PropertiesComboBox.DataSource = plans;

            GridViewDataComboBoxColumn colPline = ASPxGridView1.Columns["PLINE_CODE"] as GridViewDataComboBoxColumn;
            List<ProductLineEntity> plineEntities = ProductLineFactory.GetByIDs(plines.ToArray());
            colPline.PropertiesComboBox.DataSource = plineEntities;
            colPline.PropertiesComboBox.ValueField = "RMES_ID";
            colPline.PropertiesComboBox.TextField = "PLINE_NAME";




            ASPxGridView1.DataBind();
        }

        public void ASPxButton1_Click(object sender, EventArgs e)
        {
            BindData();
        }
    }
}