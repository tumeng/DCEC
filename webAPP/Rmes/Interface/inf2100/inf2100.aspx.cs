﻿using System;
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
 * 功能概述：SAP接口完工入库查询
 * 作    者：TuMeng
 * 创建时间：2014-08-25
 * 修改时间：
 */

namespace Rmes.WebApp.Rmes.Interface.inf2100
{
    public partial class inf2100 : BasePage
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
            string planSO = txtPlanSO.Text;
            

            string _b = cdate1.Date.ToString("yyyyMMdd") + "000000";
            string _e = cdate2.Date.ToString("yyyyMMdd") + "235959";

            List<IMESCompleteInstoreEntity> entity = db.Fetch<IMESCompleteInstoreEntity>("where SERIAL between @0 and @1 order by AUFNR",_b,_e);
            if (!string.IsNullOrEmpty(planSO))
            {
                entity = (from s in entity where s.MATNR == planSO select s).ToList<IMESCompleteInstoreEntity>();
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

            ASPxGridView1.DataBind();
        }

        public void ASPxButton1_Click(object sender, EventArgs e)
        {
            string orderCode = txtPlanSO.Text;
            if (string.IsNullOrWhiteSpace(orderCode))
            {
                Response.Write("<script>alert('成品号不能为空！');</script>");
                return;
            }
            List<IMESCompleteInstoreEntity> entity = IMESCompleteInstoreFactory.GetByOrderCode(orderCode);
            if (entity == null || entity.Count == 0)
            {
                Response.Write("<script>alert('没有该成品号的相关记录！');</script>");
                return;
            }
            ASPxGridView1.DataSource = entity;
            ASPxGridView1.DataBind();
        }
    }
}