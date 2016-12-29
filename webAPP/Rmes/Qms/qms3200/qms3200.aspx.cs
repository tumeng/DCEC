using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rmes.Web.Base;
using Rmes.Pub.Data;
using Rmes.DA.Base;
using PetaPoco;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
using DevExpress.Web.ASPxGridView;
using System.Data;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxClasses;

namespace Rmes.WebApp.Rmes.Qms.qms3200
{
    public partial class qms3200 : BasePage
    {
        private dataConn dc = new dataConn();
        PubCs thePubCs = new PubCs();

        public string theCompanyCode;

        public Database db = DB.GetInstance();
        DetectDataEntity die;

        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();

            string sql = "select t.pline_code,t.pline_name from CODE_PRODUCT_LINE t";
            sqlPline.SelectCommand = sql;
            sqlPline.DataBind();
            //ListEditItem s = _combPline.Items.FindByText("");
            
            setCondition();
        }

        private void setCondition()
        {
            //绑定表数据
            //string sql = "select t.RMES_ID,t.plineCODE,b.ROUTING_NAME,t.ITEMCODE,t.ITEMNAME,t.ITEMDESCRIPTION,t.MINVALUE,t.MAXVALUE,t.STANDARDVALUE,t.UNITNAME,decode(t.UNITTYPE,'N','数值','T','文字','B','判断','F','文件'）UNITTYPE,t.ORDERING from qms_standard_item t "
            //    + " left join data_pline_routing b on t.plineCODE=b.ROUTING_CODE order by t.plineCODE,t.ITEMCODE";
            //DataTable dt = dc.GetTable(sql);
            List<DetectErrorItemEntity> entities = db.Fetch<DetectErrorItemEntity>("");
            ASPxGridView1.DataSource = entities;

            GridViewDataComboBoxColumn colPline = ASPxGridView1.Columns["PLINE_CODE"] as GridViewDataComboBoxColumn;
            List<ProductLineEntity> plineEntities = ProductLineFactory.GetAll();
            colPline.PropertiesComboBox.DataSource = plineEntities;
            colPline.PropertiesComboBox.ValueField = "PLINE_CODE";
            colPline.PropertiesComboBox.TextField = "PLINE_NAME";


            GridViewDataComboBoxColumn colworkUnit = ASPxGridView1.Columns["WORKUNIT_CODE"] as GridViewDataComboBoxColumn;
            List<StationEntity> statinEntities = StationFactory.GetAll();
            colworkUnit.PropertiesComboBox.DataSource = statinEntities;
            colworkUnit.PropertiesComboBox.ValueField = "WORKUNIT_CODE";
            colworkUnit.PropertiesComboBox.TextField = "STATION_NAME";

            ASPxGridView1.DataBind();
        }

        protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string rmes_id = e.Values["RMES_ID"].ToString();
            DetectErrorItemEntity detEntity = new DetectErrorItemEntity { RMES_ID = rmes_id };
            //QualityStandardItem detEntity = db.First<QualityStandardItem>("where rmes_id=@0", rmes_id);

            db.Delete(detEntity);

            setCondition();
            e.Cancel = true;
        }


        protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxComboBox pline = ASPxGridView1.FindEditFormTemplateControl("combPline") as ASPxComboBox;
            ASPxComboBox workUnit = ASPxGridView1.FindEditFormTemplateControl("comWorkUnit") as ASPxComboBox;
            ASPxComboBox detectData = ASPxGridView1.FindEditFormTemplateControl("comDetectItem") as ASPxComboBox;

            DetectErrorItemEntity detEntity = new DetectErrorItemEntity();

            detEntity.COMPANY_CODE = theCompanyCode;
            detEntity.DETECT_ITEM_CODE = detectData.Value.ToString();
            detEntity.DETECT_ITEM_NAME = detectData.Text.ToString();
            detEntity.ERROR_ITEM_CODE = e.NewValues["ERROR_ITEM_CODE"].ToString();
            detEntity.ERROR_ITEM_NAME = e.NewValues["ERROR_ITEM_NAME"].ToString();


            detEntity.PLINE_CODE = pline.SelectedItem.Value.ToString();
            
            detEntity.WORKUNIT_CODE = e.NewValues["WORKUNIT_CODE"].ToString();

            

            db.Insert(detEntity);

            e.Cancel = true;
            ASPxGridView1.CancelEdit();
            setCondition();

        }


        protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string rmes_id = e.OldValues["RMES_ID"].ToString();

            DetectErrorItemEntity detEntity = db.First<DetectErrorItemEntity>("where rmes_id=@0", rmes_id);

            ASPxComboBox pline = ASPxGridView1.FindEditFormTemplateControl("combPline") as ASPxComboBox;
            ASPxComboBox workUnit = ASPxGridView1.FindEditFormTemplateControl("comWorkUnit") as ASPxComboBox;
            ASPxComboBox detectData = ASPxGridView1.FindEditFormTemplateControl("comDetectItem") as ASPxComboBox;

            detEntity.COMPANY_CODE = theCompanyCode;
            detEntity.PLINE_CODE = pline.SelectedItem.Value.ToString();
            detEntity.WORKUNIT_CODE = e.NewValues["WORKUNIT_CODE"].ToString();

            detEntity.DETECT_ITEM_CODE = detectData.Value.ToString();
            detEntity.DETECT_ITEM_NAME = detectData.Text.ToString();
            detEntity.ERROR_ITEM_CODE = e.NewValues["ERROR_ITEM_CODE"].ToString();
            detEntity.ERROR_ITEM_NAME = e.NewValues["ERROR_ITEM_NAME"].ToString();

            

            db.Update(detEntity);

            e.Cancel = true;
            ASPxGridView1.CancelEdit();
            setCondition();

        }
        protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
        {
            ASPxComboBox pline = ASPxGridView1.FindEditFormTemplateControl("combPline") as ASPxComboBox;
            ASPxComboBox workUnit = ASPxGridView1.FindEditFormTemplateControl("comWorkUnit") as ASPxComboBox;
            ASPxComboBox detectItem = ASPxGridView1.FindEditFormTemplateControl("comDetectItem") as ASPxComboBox;
            pline.TextField = "PLINE_NAME";
            pline.ValueField = "PLINE_CODE";

            string plineCode = ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "PLINE_CODE").ToString();
            string workUnitCode = ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "WORKUNIT_CODE").ToString();


            List<StationEntity> stations = StationFactory.GetByProductLine(plineCode);
            workUnit.DataSource = stations;
            workUnit.TextField = "STATION_NAME";
            workUnit.ValueField = "WORKUNIT_CODE";

           

            List<DetectDataEntity> detectItems = DetectDataFactory.GetByWorkunit(theCompanyCode, workUnitCode);

            detectItem.DataSource = detectItems;
            detectItem.TextField = "DETECT_ITEM_NAME";
            detectItem.ValueField = "DETECT_ITEM_CODE";
           


            if (ASPxGridView1.IsEditing)
            {
                //string _workUnit = ASPxGridView1.GetRowValues(ASPxGridView1.EditingRowVisibleIndex, "WORKUNIT_CODE").ToString();
                //for (int i = 0; i < workUnit.Items.Count; i++)
                //{
                //    if (_workUnit == workUnit.Items[i].Value.ToString())
                //        workUnit.Items[i].Selected = true;
                //}
            }
            
        }

        protected void comWorkUnit_Callback(object sender, CallbackEventArgsBase e)
        {
            //ASPxComboBox _pline = ASPxGridView1.FindEditFormTemplateControl("combPline") as ASPxComboBox;
            string pline = e.Parameter;
            
            List<StationEntity> stations = StationFactory.GetByProductLine(pline);
            ASPxComboBox workUnit = (ASPxComboBox)sender;
            workUnit.DataSource = stations;
            workUnit.TextField = "STATION_NAME";
            workUnit.ValueField = "WORKUNIT_CODE";
            
            workUnit.DataBind();
            
        }

        protected void comDetectItem_Callback(object sender, CallbackEventArgsBase e)
        {
            string workUnit = e.Parameter;
            List<DetectDataEntity> stations = DetectDataFactory.GetByWorkunit(theCompanyCode, workUnit);
            ASPxComboBox detectItem = (ASPxComboBox)sender;
            detectItem.DataSource = stations;
            detectItem.TextField = "DETECT_ITEM_NAME";
            detectItem.ValueField = "DETECT_ITEM_CODE";
            detectItem.DataBind();
        }

        //protected void _comWorkUnit_Callback(object sender, CallbackEventArgsBase e)
        //{
        //    //ASPxComboBox _pline = ASPxGridView1.FindEditFormTemplateControl("combPline") as ASPxComboBox;
        //    string pline = e.Parameter;



        //    List<StationEntity> stations = StationFactory.GetByProductLine(pline);
        //        ASPxComboBox workUnit = (ASPxComboBox)sender;
        //        workUnit.DataSource = stations;
        //        workUnit.TextField = "STATION_NAME";
        //        workUnit.ValueField = "WORKUNIT_CODE";

        //        workUnit.DataBind();

        //}

        //protected void _comDetectItem_Callback(object sender, CallbackEventArgsBase e)
        //{
        //    userManager theUserManager = (userManager)Session["theUserManager"];
        //    theCompanyCode = theUserManager.getCompanyCode();
        //    string parameters = e.Parameter;
        //        List<DetectDataEntity> stations = DetectDataFactory.GetByWorkunit(theCompanyCode, parameters);
        //        ASPxComboBox detectItem = (ASPxComboBox)sender;
        //        detectItem.DataSource = stations;
        //        detectItem.TextField = "DETECT_ITEM_NAME";
        //        detectItem.ValueField = "DETECT_ITEM_CODE";
        //        detectItem.DataBind();

        //}

        protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
           
        }

        //protected void ASPxGridView1_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
        //{
        //    string rmes_id = _txtRmesID.Text.Trim();

        //    if (_combPline.SelectedItem == null)
        //    {
        //        ErrorLabel.Text = "生产线不能为空！";
        //        ErrorLabel.Visible = true;
        //        return;
        //    }
        //    if (_comWorkUnit.Value == null)
        //    {
        //        ErrorLabel.Text = "工作中心不能为空！";
        //        ErrorLabel.Visible = true;
        //        return;
        //    }
        //    if (_comDetectItem.Value == null)
        //    {
        //        ErrorLabel.Text = "质检项目不能为空！";
        //        ErrorLabel.Visible = true;
        //        return;
        //    }

        //    if (string.IsNullOrEmpty(_texErrorDataCode.Text))
        //    {
        //        ErrorLabel.Text = "不合格代码不能为空！";
        //        ErrorLabel.Visible = true;
        //        return;
        //    }
        //    if (string.IsNullOrEmpty(_texErrorDataName.Text))
        //    {
        //        ErrorLabel.Text = "不合格原因不能为空！";
        //        ErrorLabel.Visible = true;
        //        return;
        //    }
        //    DetectErrorItemEntity detEntity = db.First<DetectErrorItemEntity>("where rmes_id=@0", rmes_id);
        //    detEntity.COMPANY_CODE = theCompanyCode;
        //    detEntity.PLINE_CODE = _combPline.SelectedItem.Value.ToString();
        //    detEntity.WORKUNIT_CODE = _comWorkUnit.Value.ToString();

        //    detEntity.DETECT_ITEM_CODE = _comDetectItem.Value.ToString();
        //    detEntity.DETECT_ITEM_NAME = _comDetectItem.Text.ToString();
        //    detEntity.ERROR_ITEM_CODE = _texErrorDataCode.Text.Trim();
        //    detEntity.ERROR_ITEM_NAME = _texErrorDataName.Text.Trim();



        //    db.Update(detEntity);
        //    setCondition();
        //    e.Result = "success";
        //}

        //protected void UpdateButton_Click(object sender, EventArgs e)
        //{
        //    string rmes_id = _txtRmesID.Text.Trim();

        //    if (_combPline.SelectedItem == null)
        //    {
        //        ErrorLabel.Text = "生产线不能为空！";
        //        ErrorLabel.Visible = true;
        //        return;
        //    }
        //    if (_comWorkUnit.SelectedItem == null)
        //    {
        //        ErrorLabel.Text = "工作中心不能为空！";
        //        ErrorLabel.Visible = true;
        //        return;
        //    }
        //    if (_comDetectItem.SelectedItem == null)
        //    {
        //        ErrorLabel.Text = "质检项目不能为空！";
        //        ErrorLabel.Visible = true;
        //        return;
        //    }

        //    if (string.IsNullOrEmpty(_texErrorDataCode.Text))
        //    {
        //        ErrorLabel.Text = "不合格代码不能为空！";
        //        ErrorLabel.Visible = true;
        //        return;
        //    }
        //    if (string.IsNullOrEmpty(_texErrorDataName.Text))
        //    {
        //        ErrorLabel.Text = "不合格原因不能为空！";
        //        ErrorLabel.Visible = true;
        //        return;
        //    }
        //    DetectErrorItemEntity detEntity = db.First<DetectErrorItemEntity>("where rmes_id=@0", rmes_id);
        //    detEntity.COMPANY_CODE = theCompanyCode;
        //    detEntity.PLINE_CODE = _combPline.SelectedItem.Value.ToString();
        //    detEntity.WORKUNIT_CODE = _comWorkUnit.SelectedItem.Value.ToString();

        //    detEntity.DETECT_ITEM_CODE = _comDetectItem.SelectedItem.Value.ToString();
        //    detEntity.DETECT_ITEM_NAME = _comDetectItem.SelectedItem.Text.ToString();
        //    detEntity.ERROR_ITEM_CODE = _texErrorDataCode.Text.Trim();
        //    detEntity.ERROR_ITEM_NAME = _texErrorDataName.Text.Trim();



        //    db.Update(detEntity);
        //    setCondition();
        //}
    }
}