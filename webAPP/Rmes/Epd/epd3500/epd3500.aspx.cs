using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxEditors;

using Rmes.DA.Base;
using Rmes.DA.Procedures;
using Rmes.Pub.Data;
using Rmes.Web.Base;
using Rmes.DA;
using PetaPoco;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using DevExpress.Web.ASPxGridView;

    public partial class Rmes_epd3500 : BasePage
    {
        private dataConn dc = new dataConn();

        public string theCompanyCode, theUserId;

        public Database db = DB.GetInstance();

        public static string s1 = "", s2 = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();
            setCondition();
        }

        protected void WorkUnit_DataBinding(object sender, EventArgs e)
        {
            ASPxComboBox workUnit = sender as ASPxComboBox;
            DataTable dt = new DataTable();
            dt.Columns.Add("DISPLAY");
            dt.Columns.Add("VALUE");

            userManager theUserManager = (userManager)Session["theUserManager"];
            List<LocationEntity> locations = LocationFactory.GetAll();
            List<ProductLineEntity> PLines = ProductLineFactory.GetAll();
            foreach (ProductLineEntity p in PLines)
            {
                dt.Rows.Add(p.PLINE_NAME, p.RMES_ID);
            }
            foreach (LocationEntity l in locations)
            {
                dt.Rows.Add(l.LOCATION_NAME, l.RMES_ID);
            }
            workUnit.DataSource = dt;
            workUnit.ValueField = "VALUE";
            workUnit.TextField = "DISPLAY";
            //workUnit.DataBind();
        }

        protected void Pline_DataBinding(object sender, EventArgs e)
        {
            ASPxComboBox workUnit = sender as ASPxComboBox;
            DataTable dt = new DataTable();
            dt.Columns.Add("DISPLAY");
            dt.Columns.Add("VALUE");

            userManager theUserManager = (userManager)Session["theUserManager"];
            List<ProductLineEntity> PLines = ProductLineFactory.GetAll();
            foreach (ProductLineEntity p in PLines)
            {
                dt.Rows.Add(p.RMES_ID+"|"+p.PLINE_NAME, p.RMES_ID);
            }

            workUnit.DataSource = dt;
            workUnit.ValueField = "VALUE";
            workUnit.TextField = "DISPLAY";
           // workUnit.DataBind();
        }

        private void setCondition()
        {
            List<ProcessEntity> allEntity = ProcessFactory.GetAll();
            ASPxGridView1.DataSource = allEntity;

            DataTable dt = new DataTable();
            dt.Columns.Add("DISPLAY");
            dt.Columns.Add("VALUE");

            List<LocationEntity> locations = LocationFactory.GetAll();
            List<ProductLineEntity> PLines = ProductLineFactory.GetAll();
            foreach (ProductLineEntity p in PLines)
            {
                dt.Rows.Add(p.PLINE_NAME, p.RMES_ID);
            }
            foreach (LocationEntity l in locations)
            {
                dt.Rows.Add(l.LOCATION_NAME, l.RMES_ID);
            }

            GridViewDataComboBoxColumn col = ASPxGridView1.Columns["WORKUNIT_CODE"] as GridViewDataComboBoxColumn;
            col.PropertiesComboBox.DataSource = dt;
            col.PropertiesComboBox.ValueField = "VALUE";
            col.PropertiesComboBox.TextField = "DISPLAY";

            DataTable dt1 = new DataTable();
            dt1.Columns.Add("DISPLAY");
            dt1.Columns.Add("WORKSHOP_CODE");
            dt1.Rows.Add("园区", "8101");
            dt1.Rows.Add("基地", "8102");


            GridViewDataComboBoxColumn com = ASPxGridView1.Columns["WORKSHOP_CODE"] as GridViewDataComboBoxColumn;
            com.PropertiesComboBox.DataSource = dt1;
            com.PropertiesComboBox.ValueField = "WORKSHOP_CODE";
            com.PropertiesComboBox.TextField = "DISPLAY";

            ASPxGridView1.DataBind();
        }

        protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            //判断当前记录是否可以删除
            //string strDelCode = e.Values["PLAN_CODE"].ToString();

            string rmes_id = e.Values["RMES_ID"].ToString();
            ProcessEntity detEntity = new ProcessEntity { RMES_ID = rmes_id };

            db.Delete(detEntity);

            setCondition();
            e.Cancel = true;
        }


        protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxComboBox workUnit = ASPxGridView1.FindEditFormTemplateControl("WorkUnit") as ASPxComboBox;
            ASPxComboBox WorkShop = ASPxGridView1.FindEditFormTemplateControl("WorkShop") as ASPxComboBox;
            //ASPxTextBox company = ASPxGridView1.FindEditFormTemplateControl("COMPANY_CODE") as ASPxTextBox;
            ASPxTextBox pline = ASPxGridView1.FindEditFormTemplateControl("PLINE_CODE") as ASPxTextBox;
            ASPxTextBox hour = ASPxGridView1.FindEditFormTemplateControl("PROCESS_MANHOUR") as ASPxTextBox;
            ASPxTextBox sap = ASPxGridView1.FindEditFormTemplateControl("PROCESS_CODE_SAP") as ASPxTextBox;
            ASPxTextBox org = ASPxGridView1.FindEditFormTemplateControl("PROCESS_CODE_ORG") as ASPxTextBox;
            ASPxTextBox processcode = ASPxGridView1.FindEditFormTemplateControl("PROCESS_CODE") as ASPxTextBox;
            ASPxTextBox processname = ASPxGridView1.FindEditFormTemplateControl("PROCESS_NAME") as ASPxTextBox;

            ProcessEntity prEntity = new ProcessEntity();

            prEntity.WORKUNIT_CODE = workUnit.SelectedItem.Value as string;
            prEntity.WORKSHOP_CODE = WorkShop.SelectedItem.Value as string;
            prEntity.COMPANY_CODE = "01";
            prEntity.PROCESS_CODE = e.NewValues["PROCESS_CODE"] as string;
            prEntity.PROCESS_NAME = e.NewValues["PROCESS_NAME"] as string;
            prEntity.COMPANY_CODE = "01";
            prEntity.PLINE_CODE = e.NewValues["PLINE_CODE"] as string;
            prEntity.PROCESS_MANHOUR = Convert.ToInt32(e.NewValues["PROCESS_MANHOUR"] as string);
            prEntity.PROCESS_CODE_ORG = e.NewValues["PROCESS_CODE_ORG"] as string;
            prEntity.PROCESS_CODE_SAP = e.NewValues["PROCESS_CODE_SAP"] as string;
            //string sql = "select pline_code from CODE_LOCATION where RMES_ID='" + prEntity.WORKUNIT_CODE + "' union all select RMES_ID pline_code from CODE_PRODUCT_LINE where RMES_ID='" + prEntity.WORKUNIT_CODE + "'";

            //string pline_code = dc.GetValue(sql);

            //string strWorkShop = dc.GetValue("select WORKSHOP_CODE from rel_workshop_pline where PLINE_CODE='" + pline_code + "'");
            //switch (strWorkShop)
            //{
            //    case "XK_WS01":
            //        prEntity.PROJECT_CODE = "BWF";
            //        prEntity.PROJECT_NAME = "ZF8-500/ZHW-500/ZF-800/1100KV";
            //        break;
            //    case "XK_WS02":
            //        prEntity.PROJECT_CODE = "252";
            //        prEntity.PROJECT_NAME = "ZF9A-252/ZF9C-252";
            //        break;
            //    case "XK_WS03":
            //        prEntity.PROJECT_CODE = "126";
            //        prEntity.PROJECT_NAME = "ZF7C-126";
            //        break;
            //}

            //prEntity.ROUTING_CODE = e.NewValues["ROUTING_CODE"] as string;
            //prEntity.ROUTING_NAME = e.NewValues["ROUTING_NAME"] as string;

            //if (prEntity.FLAG != "A")
            //{
            //    prEntity.PROCESS_CODE = e.NewValues["ROUTING_CODE"] as string;
            //    prEntity.PROCESS_NAME = e.NewValues["ROUTING_NAME"] as string;
            //}

            ProcessFactory.Insert(prEntity);

            e.Cancel = true;
            ASPxGridView1.CancelEdit();
            setCondition();

        }

        
        protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            dataConn dc = new dataConn();
            ProcessEntity prEntity = new ProcessEntity();
            prEntity.RMES_ID = e.NewValues["RMES_ID"] as string;
            prEntity.WORKUNIT_CODE = e.NewValues["WORKUNIT_CODE"] as string;
            prEntity.WORKSHOP_CODE = e.NewValues["WORKSHOP_CODE"] as string;
            prEntity.PROCESS_CODE = e.NewValues["PROCESS_CODE"] as string;
            prEntity.PROCESS_NAME = e.NewValues["PROCESS_NAME"] as string;
            prEntity.COMPANY_CODE = "01";
            prEntity.PLINE_CODE = e.NewValues["PLINE_CODE"] as string;
            prEntity.PROCESS_MANHOUR = Convert.ToInt32(e.NewValues["PROCESS_MANHOUR"] as string);
            prEntity.PROCESS_CODE_ORG = e.NewValues["PROCESS_CODE_ORG"] as string;
            prEntity.PROCESS_CODE_SAP = e.NewValues["PROCESS_CODE_SAP"] as string;

            //string upSql = "UPDATE CODE_PROCESS SET WORKUNIT_CODE='" + workUnit.SelectedItem.Value.ToString() + "'," +
            //         "WORKSHOP_CODE='" + comWorkShop.SelectedItem.Value.ToString() + "' WHERE  COMPANY_CODE = '" + companyCode + "'  and PLINE_CODE='" + PlineCode.SelectedItem.Value.ToString() + "'" +
            //         " and STATION_SPECIAL_CODE='" + SCode.Text.Trim() + "'";
            //dc.ExeSql(upSql);

             //prEntity.ROUTING_NAME = e.NewValues["ROUTING_NAME"] as string;

            //if (prEntity.FLAG != "A")
            //{
            //    prEntity.PROCESS_NAME = e.NewValues["ROUTING_NAME"] as string;
            //}

            ProcessFactory.Update(prEntity);

            e.Cancel = true;
            ASPxGridView1.CancelEdit();
            setCondition();

        }
        protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
        {
            ASPxComboBox workUnit = ASPxGridView1.FindEditFormTemplateControl("WorkUnit") as ASPxComboBox;
            ASPxComboBox WorkShop = ASPxGridView1.FindEditFormTemplateControl("WorkShop") as ASPxComboBox;

            

            //ASPxTextBox routingCode = ASPxGridView1.FindEditFormTemplateControl("txtRoutingCode") as ASPxTextBox;

            if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
            {
                workUnit.Enabled = false;
                WorkShop.Enabled = true;
                //routingCode.Enabled = false;
            }
        }

    }
