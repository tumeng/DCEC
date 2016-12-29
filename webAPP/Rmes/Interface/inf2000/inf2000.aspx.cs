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
 * 功能概述：SAP接口物料消耗查询
 * 作    者：TuMeng
 * 创建时间：2014-08-25
 * 修改时间：
 */

namespace Rmes.WebApp.Rmes.Interface.inf2000
{
    public partial class inf2000 : BasePage
    {
        private dataConn dc = new dataConn();
        PubCs thePubCs = new PubCs();

        public string theCompanyCode, theUserID;

        public Database db = DB.GetInstance();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cdate1.Value = DateTime.Today.AddDays(-3);
                cdate2.Value = DateTime.Today.AddDays(1);
                List<ProductLineEntity> plines = ProductLineFactory.GetAll();
                foreach (var p in plines)
                {
                    ComPline.Items.Add(p.PLINE_NAME,p.RMES_ID);
                }
            }
            BindData();
        }

        public void BindData()
        {
            string planSO = txtPlanSO.Text;
            string plineCode = ComPline.SelectedItem.Value.ToString();
            //if (string.IsNullOrWhiteSpace(orderCode)) return;
            List<PlanEntity> plans = PlanFactory.GetByCreatePeriod(cdate1.Date, cdate2.Date);
            //string _b = cdate1.Date.ToString("yyyyMMdd") + "000000";
            //string _e = cdate2.Date.ToString("yyyyMMdd") + "235959";
            //List<IMESPlanBOMEntity> entity = db.Fetch<IMESPlanBOMEntity>("where SERIAL between @0 and @1 order by AUFNR", _b, _e);
            string _b = "B"+cdate1.Date.ToString("yyyyMMdd") + "0000";
            string _e = "B"+cdate2.Date.ToString("yyyyMMdd") + "9999";
            List<IMESPlanBOMEntity> entity = db.Fetch<IMESPlanBOMEntity>("where BATCH between @0 and @1 order by AUFNR", _b, _e);



            if (!string.IsNullOrWhiteSpace(planSO))
            {
                entity = (from s in entity where s.MATNR == planSO select s).ToList<IMESPlanBOMEntity>();
            }
            if (!ComPline.SelectedItem.Value.ToString().Equals("All"))
            {
                entity = (from s in entity where (from pl in plans where pl.PLINE_CODE == plineCode && pl.RUN_FLAG == "F" select pl.ORDER_CODE).Contains(s.AUFNR) select s).ToList<IMESPlanBOMEntity>();
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
            //string orderCode = txtOrderCode.Text;
            //if (string.IsNullOrWhiteSpace(orderCode)) 
            //{
            //    Response.Write("<script>alert('订单号不能为空！');</script>");
            //    return;
            //}
            //List<IMESPlanBOMEntity> entity = IMESPlanBOMFactory.GetByOrderCode(orderCode);
            //if (entity == null || entity.Count == 0)
            //{
            //    Response.Write("<script>alert('没有该订单号的相关记录！');</script>");
            //    return;
            //}
            //ASPxGridView1.DataSource = entity;
            //ASPxGridView1.DataBind();

        }
    }
}