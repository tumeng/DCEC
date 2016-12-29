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
 * 功能概述：撤销SAP订单
 * 作    者：TuMeng
 * 创建时间：2014-11-13
 * 修改时间：
 */

namespace Rmes.WebApp.Rmes.Interface.inf1000
{
    public partial class inf1000 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindData();
        }

        public void BindData()
        {

            List<SAPPlanEntity> entity = SAPPlanFactory.GetAll();
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

        protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string orderCode = e.Values["AUFNR"] as string;
            SAPPlanFactory.CancleOrder(orderCode);
            e.Cancel = true;
            BindData();
        }
    }
}