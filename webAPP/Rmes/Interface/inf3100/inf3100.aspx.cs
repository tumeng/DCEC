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

namespace Rmes.WebApp.Rmes.Interface.inf3100
{
    public partial class inf3100 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindData();
        }

        public void BindData()
        {
            string orderCode = txtOrderCode.Text;
            if (string.IsNullOrWhiteSpace(orderCode)) return;
            List<SAPBOMEntity> entity = SAPBOMFactory.GetByOrderCode(orderCode);
            
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
            string orderCode = txtOrderCode.Text;
            if (string.IsNullOrWhiteSpace(orderCode))
            {
                Response.Write("<script>alert('订单号不能为空！');</script>");
                return;
            }
            List<SAPBOMEntity> entity = SAPBOMFactory.GetByOrderCode(orderCode);
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