using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxClasses;

//以下引用，如果采用实体类，请全部复制到新页面
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.Pub.Data;
using Rmes.Web.Base;

    public partial class Rmes_qms1100 : Rmes.Web.Base.BasePage
	{
        private dataConn dc = new dataConn();
        PubCs thePubCs = new PubCs();

        public string theCompanyCode;

        public Database db = DB.GetInstance();

        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
        
            setCondition();
        }

        private void setCondition()
        {
            //绑定表数据
            string sql = "select cp.pline_name,cd.* from code_detect_data cd,code_product_line cp where cp.pline_code=cd.pline_code and cd.company_code=" + theCompanyCode;
            DataTable dt = dc.GetTable(sql);
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
        }

        protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            //判断当前记录是否可以删除
            //string strDelCode = e.Values["PLAN_CODE"].ToString();

            string rmes_id = e.Values["RMES_ID"].ToString();
            DetectDataEntity detEntity = new DetectDataEntity { RMES_ID = rmes_id };

            db.Delete(detEntity);

            setCondition();
            e.Cancel = true;
        }


        protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxComboBox plineCode = ASPxGridView1.FindEditFormTemplateControl("plineCode") as ASPxComboBox;
            ASPxComboBox productSeries = ASPxGridView1.FindEditFormTemplateControl("productSeries") as ASPxComboBox;
            ASPxComboBox detectDataType = ASPxGridView1.FindEditFormTemplateControl("detectDataType") as ASPxComboBox;

            DetectDataEntity detEntity = new DetectDataEntity();

            detEntity.COMPANY_CODE = theCompanyCode;
            detEntity.PLINE_CODE = plineCode.SelectedItem.Value.ToString().Trim();
            detEntity.PRODUCT_SERIES = productSeries.SelectedItem.Value.ToString().Trim();

            detEntity.DETECT_ITEM_CODE = e.NewValues["DETECT_ITEM_CODE"].ToString().Trim();
            detEntity.DETECT_ITEM_NAME = e.NewValues["DETECT_ITEM_NAME"].ToString().Trim();

            detEntity.DETECT_ITEM_CODE = e.NewValues["DETECT_DATA_CODE"].ToString().Trim();
            detEntity.DETECT_ITEM_NAME = e.NewValues["DETECT_DATA_NAME"].ToString().Trim();

            detEntity.DETECT_DATA_TYPE = detectDataType.SelectedItem.Value.ToString().Trim();
            detEntity.DETECT_DATA_UNIT = e.NewValues["DETECT_DATA_UNIT"].ToString().Trim();

            if (e.NewValues["STANDARD_VALUE"] == null || e.NewValues["STANDARD_VALUE"].ToString().Trim() == "")
            {
                detEntity.STANDARD_VALUE = 0;
            }
            else
            {

                detEntity.STANDARD_VALUE = int.Parse(e.NewValues["STANDARD_VALUE"].ToString().Trim());

                detEntity.STANDARD_VALUE = int.Parse(e.NewValues["DETECT_DATA_STANDARD"].ToString().Trim());

            }
            if (e.NewValues["MAX_VALUE"] == null || e.NewValues["MAX_VALUE"].ToString().Trim() == "")
            {
                detEntity.MAX_VALUE = 0;
            }
            else
            {

                detEntity.MAX_VALUE = int.Parse(e.NewValues["MAX_VALUE"].ToString().Trim());

                detEntity.MAX_VALUE = int.Parse(e.NewValues["DETECT_DATA_UP"].ToString().Trim());

            }
            if (e.NewValues["MIN_VALUE"] == null || e.NewValues["MIN_VALUE"].ToString().Trim() == "")
            {
                detEntity.MIN_VALUE = 0;
            }
            else
            {

                detEntity.MIN_VALUE = int.Parse(e.NewValues["MIN_VALUE"].ToString().Trim());

                detEntity.MIN_VALUE = int.Parse(e.NewValues["DETECT_DATA_DOWN"].ToString().Trim());

            }

            db.Insert(detEntity);

            e.Cancel = true;
            ASPxGridView1.CancelEdit();
            setCondition();

        }


        protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string rmes_id = e.OldValues["RMES_ID"].ToString();
            ASPxComboBox plineCode = ASPxGridView1.FindEditFormTemplateControl("plineCode") as ASPxComboBox;
            ASPxComboBox productSeries = ASPxGridView1.FindEditFormTemplateControl("productSeries") as ASPxComboBox;
            ASPxComboBox detectDataType = ASPxGridView1.FindEditFormTemplateControl("detectDataType") as ASPxComboBox;

            DetectDataEntity detEntity = new DetectDataEntity();
            detEntity.RMES_ID = rmes_id;
            detEntity.COMPANY_CODE = theCompanyCode;
            detEntity.PLINE_CODE = plineCode.SelectedItem.Value.ToString().Trim();
            detEntity.PRODUCT_SERIES = productSeries.SelectedItem.Value.ToString().Trim();

            detEntity.DETECT_ITEM_CODE = e.NewValues["DETECT_ITEM_CODE"].ToString().Trim();
            detEntity.DETECT_ITEM_NAME = e.NewValues["DETECT_ITEM_NAME"].ToString().Trim();            

            detEntity.DETECT_ITEM_CODE = e.NewValues["DETECT_DATA_CODE"].ToString().Trim();
            detEntity.DETECT_ITEM_NAME = e.NewValues["DETECT_DATA_NAME"].ToString().Trim();            

            detEntity.DETECT_DATA_TYPE = detectDataType.SelectedItem.Value.ToString().Trim();
            detEntity.DETECT_DATA_UNIT = e.NewValues["DETECT_DATA_UNIT"].ToString().Trim();
            if (e.NewValues["STANDARD_VALUE"] == null || e.NewValues["STANDARD_VALUE"].ToString().Trim() == "")
            {
                detEntity.STANDARD_VALUE = 0;
            }
            else
            {

                detEntity.STANDARD_VALUE = int.Parse(e.NewValues["STANDARD_VALUE"].ToString().Trim());

                detEntity.STANDARD_VALUE = int.Parse(e.NewValues["DETECT_DATA_STANDARD"].ToString().Trim());

            }
            if (e.NewValues["MAX_VALUE"] == null || e.NewValues["MAX_VALUE"].ToString().Trim() == "")
            {
                detEntity.MAX_VALUE = 0;
            }
            else
            {

                detEntity.MAX_VALUE = int.Parse(e.NewValues["MAX_VALUE"].ToString().Trim());

                detEntity.MAX_VALUE = int.Parse(e.NewValues["DETECT_DATA_UP"].ToString().Trim());

            }
            if (e.NewValues["MIN_VALUE"] == null || e.NewValues["MIN_VALUE"].ToString().Trim() == "")
            {
                detEntity.MIN_VALUE = 0;
            }
            else
            {

                detEntity.MIN_VALUE = int.Parse(e.NewValues["MIN_VALUE"].ToString().Trim());

                detEntity.MIN_VALUE = int.Parse(e.NewValues["DETECT_DATA_DOWN"].ToString().Trim());

            }


            db.Update(detEntity);

            e.Cancel = true;
            ASPxGridView1.CancelEdit();
            setCondition();

        }
        protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            ASPxComboBox plineCode = ASPxGridView1.FindEditFormTemplateControl("plineCode") as ASPxComboBox;
            ASPxComboBox productSeries = ASPxGridView1.FindEditFormTemplateControl("productSeries") as ASPxComboBox;
            ASPxComboBox detectDataType = ASPxGridView1.FindEditFormTemplateControl("detectDataType") as ASPxComboBox;

            string sql1 = "select pline_code,pline_name from code_product_line where company_code=" + theCompanyCode;
            DataTable dt = dc.GetTable(sql1);
            plineCode.DataSource = dt;
            plineCode.TextField = "pline_name";
            plineCode.ValueField = "pline_code";
            //plineCode.DataBind();

            string sql2 = "select distinct MODEL_CODE from data_xk_oldbom";
            DataTable dt1 = dc.GetTable(sql2);
            productSeries.DataSource = dt1;
            productSeries.TextField = "MODEL_CODE";
            productSeries.ValueField = "MODEL_CODE";

            if (ASPxGridView1.IsNewRowEditing)
            {

            }
            //productSeries.DataBind();
            //创建Editform，对其中某些字段进行属性设置

            //if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
            //{
            //    ///主键不可以修改
            //    (ASPxGridView1.FindEditFormTemplateControl("txtPlanCode") as ASPxTextBox).Enabled = false;
            //}
        }

        protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            //数据提交前进行校验

            //if (thePubCs.IsNumeric(e.NewValues["PLAN_SEQUENCE"].ToString()) == false || thePubCs.IsNumeric(e.NewValues["PLAN_QTY"].ToString()) == false)
            //{
            //    e.RowError = "计划数量及执行序必须是正整数！";
            //}
        }
	}
