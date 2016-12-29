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
using Rmes.DA.Procedures;
using DevExpress.Web.ASPxGridView;
using System.Data;


/**
 * 功能概述：工序工时记录查询
 * 作    者：涂猛
 * 创建时间：2014-08-15
 */


namespace Rmes.WebApp.Rmes.Inv.inv2600
{
    public partial class inv2600 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<SNProcessEntity> entity = SNProcessFactory.GetAll();

                
                List<string> plines = new List<string>();
                List<string> plans = new List<string>();

               
                plans = entity.Select(s => s.PLAN_CODE).Distinct().ToList<string>();
                foreach (var en in entity)
                {
                    if ((!plines.Contains(en.PLINE_CODE)) && (!string.IsNullOrWhiteSpace(en.PLINE_CODE)))
                        plines.Add(en.PLINE_CODE);
                }
                List<ProductLineEntity> plineEntities = ProductLineFactory.GetByIDs(plines.ToArray());

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
            List<SNProcessEntity> entity = SNProcessFactory.GetAll();

        
            if (planCode.SelectedItem != null && !planCode.SelectedItem.Value.ToString().Equals("All"))
            {
                entity = (from s in entity where s.PLAN_CODE.Equals(planCode.SelectedItem.Value.ToString()) select s).ToList<SNProcessEntity>();
            }
            if (plineCode.SelectedItem != null && !plineCode.SelectedItem.Value.ToString().Equals("All"))
            {
                entity = (from s in entity where s.PLINE_CODE.Equals(plineCode.SelectedItem.Value.ToString()) select s).ToList<SNProcessEntity>();
            }

            ASPxGridView1.DataSource = entity;
            List<string> orders = new List<string>();
            List<string> plans = new List<string>();
            List<string> plines = new List<string>();
            List<string> workUnits = new List<string>();
            foreach (var e in entity)
            {
                
                if ((!string.IsNullOrWhiteSpace(e.PLAN_CODE)) && (!plans.Contains(e.PLAN_CODE)))
                    plans.Add(e.PLAN_CODE);
                if ((!plines.Contains(e.PLINE_CODE)) && (!string.IsNullOrWhiteSpace(e.PLINE_CODE)))
                    plines.Add(e.PLINE_CODE);
                if ((!workUnits.Contains(e.WORKUNIT_CODE)) && (!string.IsNullOrWhiteSpace(e.WORKUNIT_CODE)))
                    workUnits.Add(e.WORKUNIT_CODE);
            }
           


            GridViewDataComboBoxColumn colPlan = ASPxGridView1.Columns["PLAN_CODE"] as GridViewDataComboBoxColumn;
            colPlan.PropertiesComboBox.DataSource = plans;

            GridViewDataComboBoxColumn colPline = ASPxGridView1.Columns["PLINE_CODE"] as GridViewDataComboBoxColumn;
            List<ProductLineEntity> plineEntities = ProductLineFactory.GetByIDs(plines.ToArray());
            colPline.PropertiesComboBox.DataSource = plineEntities;
            colPline.PropertiesComboBox.ValueField = "RMES_ID";
            colPline.PropertiesComboBox.TextField = "PLINE_NAME";

            DataTable itemDT = new DataTable();
            itemDT.Columns.Add("Value");
            itemDT.Columns.Add("Text");
            itemDT.Rows.Add("Y", "装配完成");
            itemDT.Rows.Add("R", "正在装配");
            itemDT.Rows.Add("N", "未开始");
            GridViewDataComboBoxColumn itemCol = ASPxGridView1.Columns["COMPLETE_FLAG"] as GridViewDataComboBoxColumn;
            itemCol.PropertiesComboBox.DataSource = itemDT;
            itemCol.PropertiesComboBox.ValueField = "Value";
            itemCol.PropertiesComboBox.TextField = "Text";

            GridViewDataComboBoxColumn colWorkUnit = ASPxGridView1.Columns["WORKUNIT_CODE"] as GridViewDataComboBoxColumn;
            colWorkUnit.PropertiesComboBox.DataSource = workUnits;

            ASPxGridView1.DataBind();


        }

        public void ASPxButton1_Click(object sender, EventArgs e)
        {
            BindData();
        }
    }
}