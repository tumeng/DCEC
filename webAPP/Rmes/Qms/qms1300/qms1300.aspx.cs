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
using System.Collections.Generic;

/**
 * 功能概述：检验项目定义
 * 作    者：caoly
 * 创建时间：2014-04-12
 */

public partial class Rmes_qms1300 : Rmes.Web.Base.BasePage
{
    private dataConn dc = new dataConn();
    PubCs thePubCs = new PubCs();

    public string theCompanyCode, theProgramCode, theUserId, theUserCode;

    public Database db = DB.GetInstance();

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theProgramCode = "qms1300";
        theUserId = theUserManager.getUserId();
        theUserCode = theUserManager.getUserCode();

        string sql = "select RMES_ID PLINE_CODE,PLINE_NAME from code_product_line where pline_code in ( select distinct pline_code from vw_user_role_program t where company_code='" + theCompanyCode + "' and program_code='" + theProgramCode + "' and user_id='" + theUserId + "' )";
        sqlPline.SelectCommand = sql;
        sqlPline.DataBind();

        setCondition();
    }

    private void setCondition()
    {
        //绑定表数据
        //string sql = "select t.RMES_ID,t.plineCODE,b.ROUTING_NAME,t.ITEMCODE,t.ITEMNAME,t.ITEMDESCRIPTION,t.MINVALUE,t.MAXVALUE,t.STANDARDVALUE,t.UNITNAME,decode(t.UNITTYPE,'N','数值','T','文字','B','判断','F','文件'）UNITTYPE,t.ORDERING from qms_standard_item t "
        //    + " left join data_pline_routing b on t.plineCODE=b.ROUTING_CODE order by t.plineCODE,t.ITEMCODE";
        //DataTable dt = dc.GetTable(sql);
        List<DetectDataEntity> entities = DetectDataFactory.GetByUser(theUserId,theProgramCode);
        string sql = "select t.*,a.pline_code plinecode1 from code_detect t left join code_product_line a on t.pline_code=a.rmes_id where t.pline_code in (select pline_id from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "' ) and (t.temp01 is null or t.temp01='Y') order by t.INPUT_TIME desc nulls last";
        DataTable dt1 = dc.GetTable(sql);
        ASPxGridView1.DataSource = dt1;

        //GridViewDataComboBoxColumn colPline = ASPxGridView1.Columns["PLINE_CODE"] as GridViewDataComboBoxColumn;
        //List<ProductLineEntity> plineEntities = ProductLineFactory.GetByUserID(theUserId,theProgramCode);
        //colPline.PropertiesComboBox.DataSource = plineEntities;
        //colPline.PropertiesComboBox.ValueField = "PLINE_CODE";
        //colPline.PropertiesComboBox.TextField = "PLINE_NAME";


        //GridViewDataComboBoxColumn colworkUnit = ASPxGridView1.Columns["WORKUNIT_CODE"] as GridViewDataComboBoxColumn;
        //List<StationEntity> statinEntities = StationFactory.GetAll();
        //colworkUnit.PropertiesComboBox.DataSource = statinEntities;
        //colworkUnit.PropertiesComboBox.ValueField = "WORKUNIT_CODE";
        //colworkUnit.PropertiesComboBox.TextField = "STATION_NAME";

        GridViewDataComboBoxColumn colDetectType = ASPxGridView1.Columns["DETECT_TYPE"] as GridViewDataComboBoxColumn;
        DataTable dt = new DataTable();
        dt.Columns.Add("Text");
        dt.Columns.Add("Value");

        dt.Rows.Add("计量型", "0");
        dt.Rows.Add("计点型", "1");
        dt.Rows.Add("文本型", "2");
        dt.Rows.Add("零件条码", "3");
        //dt.Rows.Add("文件", "F");

        colDetectType.PropertiesComboBox.DataSource = dt;
        colDetectType.PropertiesComboBox.ValueField = "Value";
        colDetectType.PropertiesComboBox.TextField = "Text";

        ASPxGridView1.DataBind();
    }

    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        string rmes_id = e.Values["RMES_ID"].ToString();
        
        DetectDataEntity detEntity = new DetectDataEntity { RMES_ID = rmes_id };

        //插入到日志表20161101
        try
        {
            string Sql2 = "INSERT INTO CODE_DETECT_LOG (RMES_ID,COMPANY_CODE,PLINE_CODE,PRODUCT_SERIES,DETECT_CODE,DETECT_NAME,DETECT_TYPE,DETECT_STANDARD,DETECT_MAX,DETECT_MIN,DETECT_UNIT,ASSOCIATION_TYPE,TEMP01,TEMP02,user_code,flag,rqsj)"
                + " SELECT RMES_ID,COMPANY_CODE,PLINE_CODE,PRODUCT_SERIES,DETECT_CODE,DETECT_NAME,DETECT_TYPE,DETECT_STANDARD,DETECT_MAX,DETECT_MIN,DETECT_UNIT,ASSOCIATION_TYPE,TEMP01,TEMP02,'"
                + theUserCode + "' , 'DEL', SYSDATE FROM CODE_DETECT WHERE  RMES_ID =  '" + rmes_id + "' ";
            dc.ExeSql(Sql2);
        }
        catch
        {
            return;
        }
        dc.ExeSql("update CODE_DETECT set temp01='N' where RMES_ID =  '" + rmes_id + "'  ");
        //db.Delete(detEntity);

        setCondition();
        e.Cancel = true;
    }


    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxComboBox pline = ASPxGridView1.FindEditFormTemplateControl("combPline") as ASPxComboBox;
        //ASPxComboBox workUnit = ASPxGridView1.FindEditFormTemplateControl("comWorkUnit") as ASPxComboBox;
        ASPxComboBox detectDataType = ASPxGridView1.FindEditFormTemplateControl("detectDataType") as ASPxComboBox;
        string sql = "select 'DECT'||TRIM(TO_CHAR(SEQ_USER_ID.NEXTVAL,'000000')) from dual";
        dc.setTheSql(sql);
        string dece_code1=dc.GetValue();
        DetectDataEntity detEntity = new DetectDataEntity();

        detEntity.COMPANY_CODE = theCompanyCode;
        //detEntity.DETECT_CODE = e.NewValues["DETECT_CODE"].ToString();
        detEntity.DETECT_CODE = dece_code1;
        detEntity.DETECT_NAME = e.NewValues["DETECT_NAME"].ToString();
        detEntity.DETECT_UNIT = e.NewValues["DETECT_UNIT"].ToString();
        detEntity.PRODUCT_SERIES = "";// e.NewValues["PRODUCT_SERIES"].ToString();
        detEntity.ASSOCIATION_TYPE = e.NewValues["ASSOCIATION_TYPE"].ToString();
        

        detEntity.PLINE_CODE = pline.SelectedItem.Value.ToString();
        detEntity.DETECT_TYPE = detectDataType.SelectedItem.Value.ToString();
        detEntity.INPUT_TIME = DateTime.Now;
        detEntity.INPUT_PERSON = theUserId;

        if (detEntity.DETECT_TYPE != "0")  //非计量型无上下限   记点型限定01
        {
            //if (detEntity.DETECT_TYPE == "1")
            //{
            //    try
            //    {
            //        detEntity.DETECT_STANDARD = Convert.ToInt32(e.NewValues["DETECT_STANDARD"].ToString().Trim());
            //    }
            //    catch
            //    {
            //        detEntity.DETECT_STANDARD = 0;
            //    }
            //}
            //else
            //{
            //    detEntity.DETECT_STANDARD = 0;
            //}
            detEntity.DETECT_STANDARD = 0;
            detEntity.DETECT_MAX = 0;
            detEntity.DETECT_MIN = 0;
        }
        else  //计量型
        {
            if (e.NewValues["DETECT_STANDARD"] == null || e.NewValues["DETECT_STANDARD"].ToString().Trim() == "")
            {
                detEntity.DETECT_STANDARD = 0;
            }
            else
            {
                detEntity.DETECT_STANDARD = Convert.ToDouble(e.NewValues["DETECT_STANDARD"].ToString().Trim());
            }

            if (e.NewValues["DETECT_MAX"] == null || e.NewValues["DETECT_MAX"].ToString().Trim() == "")
            {
                detEntity.DETECT_MAX = 0;
            }
            else
            {
                detEntity.DETECT_MAX = Convert.ToDouble(e.NewValues["DETECT_MAX"].ToString());
            }

            if (e.NewValues["DETECT_MIN"] == null || e.NewValues["DETECT_MIN"].ToString().Trim() == "")
            {
                detEntity.DETECT_MIN = 0;
            }
            else
            {
                detEntity.DETECT_MIN = Convert.ToDouble(e.NewValues["DETECT_MIN"].ToString());
            }
        }

        db.Insert(detEntity);
        //插入到日志表20161101
        try
        {
            string Sql2 = "INSERT INTO CODE_DETECT_LOG (RMES_ID,COMPANY_CODE,PLINE_CODE,PRODUCT_SERIES,DETECT_CODE,DETECT_NAME,DETECT_TYPE,DETECT_STANDARD,DETECT_MAX,DETECT_MIN,DETECT_UNIT,ASSOCIATION_TYPE,TEMP01,TEMP02,user_code,flag,rqsj)"
                + " SELECT RMES_ID,COMPANY_CODE,PLINE_CODE,PRODUCT_SERIES,DETECT_CODE,DETECT_NAME,DETECT_TYPE,DETECT_STANDARD,DETECT_MAX,DETECT_MIN,DETECT_UNIT,ASSOCIATION_TYPE,TEMP01,TEMP02,'"
                + theUserCode + "' , 'ADD', SYSDATE FROM CODE_DETECT WHERE  DETECT_CODE =  '"
                + dece_code1 + "' AND DETECT_NAME = '" + e.NewValues["DETECT_NAME"].ToString() + "' ";
            dc.ExeSql(Sql2);
        }
        catch
        {
            return;
        }


        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        setCondition();

    }


    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string rmes_id = e.OldValues["RMES_ID"].ToString();

        DetectDataEntity detEntity = db.First<DetectDataEntity>("where rmes_id=@0", rmes_id);

        ASPxComboBox pline = ASPxGridView1.FindEditFormTemplateControl("combPline") as ASPxComboBox;
        //ASPxComboBox workUnit = ASPxGridView1.FindEditFormTemplateControl("comWorkUnit") as ASPxComboBox;

        detEntity.PLINE_CODE = pline.SelectedItem.Value.ToString();
        //detEntity.WORKUNIT_CODE = e.NewValues["WORKUNIT_CODE"].ToString();

        detEntity.DETECT_CODE = e.NewValues["DETECT_CODE"].ToString();
        detEntity.DETECT_NAME = e.NewValues["DETECT_NAME"].ToString();
        detEntity.DETECT_UNIT = e.NewValues["DETECT_UNIT"].ToString();
        detEntity.PRODUCT_SERIES = "";// e.NewValues["PRODUCT_SERIES"].ToString();
        detEntity.ASSOCIATION_TYPE = e.NewValues["ASSOCIATION_TYPE"].ToString();
        detEntity.INPUT_TIME = DateTime.Now;
        detEntity.INPUT_PERSON = theUserId;

        if (detEntity.DETECT_TYPE != "0")
        {
            //if (detEntity.DETECT_TYPE == "1")
            //{
            //    try
            //    {
            //        detEntity.DETECT_STANDARD = Convert.ToInt32(e.NewValues["DETECT_STANDARD"].ToString().Trim());
            //    }
            //    catch
            //    {
            //        detEntity.DETECT_STANDARD = 0;
            //    }
            //}
            //else
            //{
            //    detEntity.DETECT_STANDARD = 0;
            //}
            detEntity.DETECT_STANDARD = 0;
            detEntity.DETECT_MAX = 0;
            detEntity.DETECT_MIN = 0;
        }
        else
        {
            detEntity.DETECT_STANDARD = Convert.ToDouble(e.NewValues["DETECT_STANDARD"].ToString());
            detEntity.DETECT_MAX = Convert.ToDouble(e.NewValues["DETECT_MAX"].ToString());
            detEntity.DETECT_MIN = Convert.ToDouble(e.NewValues["DETECT_MIN"].ToString());
        }

        db.Update(detEntity);
        //插入到日志表20161101
        try
        {   
            //string Sql2 = "INSERT INTO CODE_DETECT_LOG (RMES_ID,COMPANY_CODE,PLINE_CODE,PRODUCT_SERIES,DETECT_CODE,DETECT_NAME,DETECT_TYPE,DETECT_STANDARD,DETECT_MAX,DETECT_MIN,DETECT_UNIT,ASSOCIATION_TYPE,TEMP01,TEMP02,user_code,flag,rqsj)"
            //    + " SELECT RMES_ID,COMPANY_CODE,PLINE_CODE,PRODUCT_SERIES,DETECT_CODE,DETECT_NAME,DETECT_TYPE,DETECT_STANDARD,DETECT_MAX,DETECT_MIN,DETECT_UNIT,ASSOCIATION_TYPE,TEMP01,TEMP02,'"
            //    + theUserCode + "' , 'BEFOREEDIT', SYSDATE FROM CODE_DETECT WHERE  RMES_ID =  '" + rmes_id + "' ";
           
            string Sql2 = " INSERT INTO CODE_DETECT_LOG (RMES_ID,COMPANY_CODE,PLINE_CODE,PRODUCT_SERIES,DETECT_CODE,DETECT_NAME,DETECT_TYPE,DETECT_STANDARD,DETECT_MAX,DETECT_MIN,DETECT_UNIT,ASSOCIATION_TYPE,user_code,flag,rqsj)"
                           + " VALUES( '" + rmes_id + "','" + theCompanyCode + "','" + e.OldValues["PLINE_CODE"].ToString() + "','" + e.OldValues["PRODUCT_SERIES"].ToString() + "','"
                           + e.OldValues["DETECT_CODE"].ToString() + "','"
                           + e.OldValues["DETECT_NAME"].ToString() + "','"
                           + e.OldValues["DETECT_TYPE"].ToString() + "','"
                           + e.OldValues["DETECT_STANDARD"].ToString() + "','"
                           + e.OldValues["DETECT_MAX"].ToString() + "','"
                           + e.OldValues["DETECT_MIN"].ToString() + "','"
                           + e.OldValues["DETECT_UNIT"].ToString() + "','"
                           + e.OldValues["ASSOCIATION_TYPE"].ToString() + "','"
                           + theUserCode + "','BEFOREEDIT',SYSDATE) ";
             dc.ExeSql(Sql2);
            string Sql3 = " INSERT INTO CODE_DETECT_LOG (RMES_ID,COMPANY_CODE,PLINE_CODE,PRODUCT_SERIES,DETECT_CODE,DETECT_NAME,DETECT_TYPE,DETECT_STANDARD,DETECT_MAX,DETECT_MIN,DETECT_UNIT,ASSOCIATION_TYPE,user_code,flag,rqsj)"
                           + " VALUES( '" + rmes_id + "','" + theCompanyCode + "','" + e.NewValues["PLINE_CODE"].ToString() + "','" + e.NewValues["PRODUCT_SERIES"].ToString() + "','"
                           + e.NewValues["DETECT_CODE"].ToString() + "','"
                           + e.NewValues["DETECT_NAME"].ToString() + "','"
                           + e.NewValues["DETECT_TYPE"].ToString() + "','"
                           + e.NewValues["DETECT_STANDARD"].ToString() + "','"
                           + e.NewValues["DETECT_MAX"].ToString() + "','"
                           + e.NewValues["DETECT_MIN"].ToString() + "','"
                           + e.NewValues["DETECT_UNIT"].ToString() + "','"
                           + e.NewValues["ASSOCIATION_TYPE"].ToString() + "','"
                           + theUserCode + "','AFTEREDIT',SYSDATE) ";
            dc.ExeSql(Sql3);
        }
        catch
        {
            return;
        }

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        setCondition();

    }
    protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
        
        ASPxComboBox pline = ASPxGridView1.FindEditFormTemplateControl("combPline") as ASPxComboBox;
        ASPxComboBox detectDataType = ASPxGridView1.FindEditFormTemplateControl("detectDataType") as ASPxComboBox;
        pline.TextField = "PLINE_NAME";
        pline.ValueField = "PLINE_CODE";
        
        if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
        {
            string productName = grid.GetRowValues(grid.EditingRowVisibleIndex, new string[] { "DETECT_TYPE" }).ToString();
            //(ASPxGridView1.FindEditFormTemplateControl("combPline") as ASPxComboBox).Enabled = false;
            (ASPxGridView1.FindEditFormTemplateControl("detectDataType") as ASPxComboBox).Enabled = false;
            (ASPxGridView1.FindEditFormTemplateControl("texDetectDataCode") as ASPxTextBox).Enabled = false;
            if (productName == "0")
            {
                (ASPxGridView1.FindEditFormTemplateControl("ASPxLabel9") as ASPxLabel).ClientVisible = true;
                (ASPxGridView1.FindEditFormTemplateControl("txtDetectDataDown") as ASPxTextBox).ClientVisible = true;
                (ASPxGridView1.FindEditFormTemplateControl("ASPxLabel6") as ASPxLabel).ClientVisible = true;
                (ASPxGridView1.FindEditFormTemplateControl("txtDetectDataUp") as ASPxTextBox).ClientVisible = true;
            }
            else
            {
                (ASPxGridView1.FindEditFormTemplateControl("ASPxLabel9") as ASPxLabel).ClientVisible = false;
                (ASPxGridView1.FindEditFormTemplateControl("txtDetectDataDown") as ASPxTextBox).ClientVisible = false;
                (ASPxGridView1.FindEditFormTemplateControl("ASPxLabel6") as ASPxLabel).ClientVisible = false;
                (ASPxGridView1.FindEditFormTemplateControl("txtDetectDataUp") as ASPxTextBox).ClientVisible = false;
                (ASPxGridView1.FindEditFormTemplateControl("ASPxLabel2") as ASPxLabel).ClientVisible = false;
                (ASPxGridView1.FindEditFormTemplateControl("txtDetectDataUnit") as ASPxTextBox).ClientVisible = false;
            }
        }
        if (ASPxGridView1.IsNewRowEditing)
        {
            (ASPxGridView1.FindEditFormTemplateControl("texDetectDataCode") as ASPxTextBox).Enabled = false;
            (ASPxGridView1.FindEditFormTemplateControl("texDetectDataCode") as ASPxTextBox).Visible = false;
            (ASPxGridView1.FindEditFormTemplateControl("ASPxLabel7") as ASPxLabel).Visible = false;
        }
    }

    //protected void comWorkUnit_Callback(object sender, CallbackEventArgsBase e)
    //{
    //    string pline = e.Parameter;
    //    List<StationEntity> stations = StationFactory.GetByProductLine(pline);
    //    ASPxComboBox workUnit = (ASPxComboBox)sender;
    //    workUnit.DataSource = stations;
    //    workUnit.TextField = "STATION_NAME";
    //    workUnit.ValueField = "WORKUNIT_CODE";
    //    workUnit.DataBind();
    //}

    protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        if (ASPxGridView1.IsNewRowEditing)//新增
        {
            ASPxComboBox pline = ASPxGridView1.FindEditFormTemplateControl("combPline") as ASPxComboBox;
            ASPxComboBox detectDataType = ASPxGridView1.FindEditFormTemplateControl("detectDataType") as ASPxComboBox;
            ASPxTextBox detectstandard = ASPxGridView1.FindEditFormTemplateControl("txtDetectDataStandard") as ASPxTextBox;
            ASPxTextBox detectup = ASPxGridView1.FindEditFormTemplateControl("txtDetectDataUp") as ASPxTextBox;
            ASPxTextBox detectdown = ASPxGridView1.FindEditFormTemplateControl("txtDetectDataDown") as ASPxTextBox;
            ASPxTextBox detectseries = ASPxGridView1.FindEditFormTemplateControl("ASPxTextBox2") as ASPxTextBox;
            ASPxTextBox detectname = ASPxGridView1.FindEditFormTemplateControl("texDetectDataName") as ASPxTextBox;
            if (detectDataType.Value == null)
            {
                e.RowError = "请选择检验类型！";
            }

            if (pline.Value == null)
            {
                e.RowError = "请选择产线！";
            }
            if (detectDataType.Value.ToString() == "3")//零件类型
            {
                string sql = "select * from copy_pt_mstr where pt_desc2 ='" + detectname.Text.Trim() + "'";
                dataConn dc1 = new dataConn(sql);
                DataTable dt = dc1.GetTable();
                if (dt.Rows.Count <= 0)
                {
                    e.RowError = "项目名称必须与零件名称相同！";
                    return;
                }

            }
            if (detectDataType.Value.ToString() == "0")//计量型
            {
                if (detectup.Value == null || detectdown.Value == null)
                {
                    e.RowError = "请输入上下限！";
                    return;
                }
                try
                {
                    if (Convert.ToDouble(detectup.Value.ToString()) < Convert.ToDouble(detectdown.Value.ToString()))
                    {
                        e.RowError = "最大值小于最小值！";
                        return;
                    }
                }
                catch
                {
                    e.RowError = "录入值不合法！";
                    return;
                }
            }
            string aa = dc.GetValue("select count(1) from code_detect where pline_code='" + pline.Value.ToString() + "' and detect_name='" + detectname.Value.ToString() + "' "); //and product_series='" + detectseries.Value.ToString() + "'
            if (aa != "0") 
            {
                e.RowError = "录入数据重复！";
                return;
            }
        }
        if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
        {
            ASPxComboBox pline = ASPxGridView1.FindEditFormTemplateControl("combPline") as ASPxComboBox;
            ASPxComboBox detectDataType = ASPxGridView1.FindEditFormTemplateControl("detectDataType") as ASPxComboBox;
            ASPxTextBox detectstandard = ASPxGridView1.FindEditFormTemplateControl("txtDetectDataStandard") as ASPxTextBox;
            ASPxTextBox detectup = ASPxGridView1.FindEditFormTemplateControl("txtDetectDataUp") as ASPxTextBox;
            ASPxTextBox detectdown = ASPxGridView1.FindEditFormTemplateControl("txtDetectDataDown") as ASPxTextBox;
            ASPxTextBox detectname = ASPxGridView1.FindEditFormTemplateControl("texDetectDataName") as ASPxTextBox;
            //if (detectDataType.Value.ToString() == "1")//记点型限定01
            //{
            //    if (detectstandard.Value == null)
            //    {
            //        e.RowError = "请输入标准值！";
            //        return;
            //    }
            //    try
            //    {
            //        if (detectstandard.Value.ToString() != "1" && detectstandard.Value.ToString() != "0")
            //        {
            //            e.RowError = "记点型限定输入0、1！";
            //        }
            //    }
            //    catch
            //    {
            //        e.RowError = "标准值输入错误！";
            //    }
            //}
            if (detectDataType.Value.ToString() == "0")//计量型
            {
                if (detectup.Value == null || detectdown.Value == null)
                {
                    e.RowError = "请输入上下限！";
                    return;
                }
                try
                {
                    if (Convert.ToDouble(detectup.Value.ToString()) < Convert.ToDouble(detectdown.Value.ToString()))
                    {
                        e.RowError = "最大值小于最小值！";
                        return;
                    }
                }
                catch
                {
                    e.RowError = "录入值不合法！";
                    return;
                }
            }

            if (detectDataType.Value.ToString() == "3")//零件类型
            {
                string sql = "select * from copy_pt_mstr where pt_desc2 ='" + detectname.Text.Trim() + "'";
                dataConn dc1 = new dataConn(sql);
                DataTable dt = dc1.GetTable();
                if (dt.Rows.Count <= 0)
                {
                    e.RowError = "项目名称必须与零件名称相同！";
                    return;
                }

            }
        }
    }
}
