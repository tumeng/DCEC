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
 * 功能概述：SAP导入订单查询
 * 作    者：TuMeng
 * 创建时间：2014-08-25
 * 修改时间：
 */

namespace Rmes.WebApp.Rmes.Interface.inf3000
{
    public partial class inf3000 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindData();
        }

        public void BindData()
        {
            string projectCode = txtProjectCode.Text;
            if (string.IsNullOrEmpty(projectCode)) return;
            List<SAPPlanEntity> entity = new List<SAPPlanEntity>(); //SAPPlanFactory.GetByOrderCode(orderCode);

            entity.AddRange(SAPPlanFactory.GetByProjectCode(projectCode));

            ASPxGridView1.DataSource = entity;

            DataTable dt = new DataTable();
            dt.Columns.Add("Text");
            dt.Columns.Add("Value");
            dt.Rows.Add("初始状态", "0");
            dt.Rows.Add("导入中", "1");
            dt.Rows.Add("已导入", "2");

            GridViewDataComboBoxColumn colPrind = ASPxGridView1.Columns["PRIND"] as GridViewDataComboBoxColumn;
            colPrind.PropertiesComboBox.DataSource = dt;
            colPrind.PropertiesComboBox.TextField = "Text";
            colPrind.PropertiesComboBox.ValueField = "Value";

            ASPxGridView1.DataBind();
        }

        public void ASPxButton1_Click(object sender, EventArgs e)
        {

        }
    }
}