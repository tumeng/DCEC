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
 * 功能概述：完工入库的记录查询
 * 作    者：涂猛
 * 创建时间：2014-08-14
 */

namespace Rmes.WebApp.Rmes.Inv.inv2400
{
    public partial class inv2400 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindData();
        }

        private void BindData()
        {
            List<SNCompleteInstoreEntity> entity = SNCompleteInstoreFactory.GetAll();
            ASPxGridView1.DataSource = entity;

            List<string> orders = new List<string>();
            List<string> plans = new List<string>();

            foreach (var e in entity)
            {
                if ((!string.IsNullOrWhiteSpace(e.ORDER_CODE)) && (!orders.Contains(e.ORDER_CODE)))
                    orders.Add(e.ORDER_CODE);
                if ((!string.IsNullOrWhiteSpace(e.PLAN_CODE)) && (!plans.Contains(e.PLAN_CODE)))
                    plans.Add(e.PLAN_CODE);
            }
            GridViewDataComboBoxColumn colOrder = ASPxGridView1.Columns["ORDER_CODE"] as GridViewDataComboBoxColumn;
            colOrder.PropertiesComboBox.DataSource = orders;


            GridViewDataComboBoxColumn colPlan = ASPxGridView1.Columns["PLAN_CODE"] as GridViewDataComboBoxColumn;
            colPlan.PropertiesComboBox.DataSource = plans;

            DataTable itemDT = new DataTable();
            itemDT.Columns.Add("Value");
            itemDT.Columns.Add("Text");
            itemDT.Rows.Add("1", "成品收货");
            itemDT.Rows.Add("2", "拆卸收货");
            itemDT.Rows.Add("3", "停线待处理入库");
            GridViewDataComboBoxColumn itemCol = ASPxGridView1.Columns["INSTORE_TYPE_CODE"] as GridViewDataComboBoxColumn;
            itemCol.PropertiesComboBox.DataSource = itemDT;
            itemCol.PropertiesComboBox.ValueField = "Value";
            itemCol.PropertiesComboBox.TextField = "Text";

            ASPxGridView1.DataBind();

        }
    }
}