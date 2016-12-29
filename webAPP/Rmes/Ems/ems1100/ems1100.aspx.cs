
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rmes.Pub.Data;
using System.Data;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxClasses;

/**
 * 功能概述：设备定义义
 * 作者：李蒙蒙
 * 创建时间：2013-06-02
 */


public partial class Rmes_ems1100 : Rmes.Web.Base.BasePage
{
    private dataConn dc = new dataConn();
    private PubCs pc = new PubCs();
    private string theCompanyCode;

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();

        //初始化设备类别列表
        initAssetClass();

        if (!IsPostBack)
        {
            //初始化设备列表
            setCondition();
        }
    }

    
    //初始化设备类别列表
    private void initAssetClass()
    {
        string sql = "SELECT ASSET_CLASS_CODE,ASSET_CLASS_NAME FROM CODE_ASSET_CLASS WHERE COMPANY_CODE = '" + theCompanyCode + "'";
        SqlAssetClass.SelectCommand = sql;
        SqlAssetClass.DataBind();
    }


    private void setCondition()
    {
        //初始化GRIDVIEW
        string sql = "SELECT A.COMPANY_CODE, A.ASSET_CODE, A.ASSET_NAME,ASSET_SPEC,ASSET_MODEL,A.ASSET_CLASS_CODE,B.ASSET_CLASS_NAME,ASSET_REMARK "
                   + "FROM CODE_ASSET A "
                   + "LEFT JOIN CODE_ASSET_CLASS B ON A.ASSET_CLASS_CODE = B.ASSET_CLASS_CODE AND A.COMPANY_CODE = B.COMPANY_CODE "
                   + "WHERE A.COMPANY_CODE = '" + theCompanyCode + "' ";
 
        sql += " ORDER BY A.ASSET_CODE";

        ASPxGridView1.DataSource = dc.GetTable(sql);
        ASPxGridView1.DataBind();

    }

    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {

        //判断当前记录是否可以删除
        string strDelCode = e.Values["ASSET_CODE"].ToString();
        string SQL = "select * from data_asset_detal a where asset_code='" + strDelCode + "'";
        DataTable dt = dc.GetTable(SQL);
       
        if (dt.Rows.Count>0)
        {
            ASPxGridView1.JSProperties.Add("cpCallbackName", "Delete");
            ASPxGridView1.JSProperties.Add("cpCallbackValue", "当前设备台帐里存在此项设备定义，请先维护台帐！");
        }
        else
        {
            //删除操作
            string Sql = "delete from CODE_ASSET WHERE  COMPANY_CODE = '" + theCompanyCode + "' and ASSET_CODE = '" + strDelCode + "'";
            dc.ExeSql(Sql);
           
        }
        setCondition();
        e.Cancel = true;
    }
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        //新增
        ASPxTextBox AssetCode = ASPxGridView1.FindEditFormTemplateControl("txtAssetCode") as ASPxTextBox;
        ASPxTextBox AssetName = ASPxGridView1.FindEditFormTemplateControl("txtAssetName") as ASPxTextBox;
        
        ASPxTextBox AssetSpec = ASPxGridView1.FindEditFormTemplateControl("txtSpec") as ASPxTextBox;
        ASPxTextBox AssetModel = ASPxGridView1.FindEditFormTemplateControl("txtModel") as ASPxTextBox;
        ASPxComboBox AssetClassCode = ASPxGridView1.FindEditFormTemplateControl("txtClass") as ASPxComboBox;
        ASPxTextBox remark = ASPxGridView1.FindEditFormTemplateControl("txtRemark") as ASPxTextBox;



        string Sql = "INSERT INTO CODE_ASSET (COMPANY_CODE,ASSET_CODE,ASSET_NAME,  ASSET_SPEC,ASSET_MODEL,ASSET_CLASS_CODE,ASSET_REMARK) "
             + "VALUES('" + theCompanyCode + "','" + AssetCode.Text.Trim() + "','" + AssetName.Text.Trim() + "','" + AssetSpec.Text.Trim() + "','" 
             + AssetModel.Text.Trim() + "','" + AssetClassCode.SelectedItem.Value.ToString().Trim()+ "','" + remark.Text.Trim() + "')";
             
        dc.ExeSql(Sql);

        setCondition();
        e.Cancel = true;

        ASPxGridView1.CancelEdit();
    }


    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //修改
        ASPxTextBox AssetCode = ASPxGridView1.FindEditFormTemplateControl("txtAssetCode") as ASPxTextBox;
        ASPxTextBox AssetName = ASPxGridView1.FindEditFormTemplateControl("txtAssetName") as ASPxTextBox;

        ASPxTextBox AssetSpec = ASPxGridView1.FindEditFormTemplateControl("txtSpec") as ASPxTextBox;
        ASPxTextBox AssetModel = ASPxGridView1.FindEditFormTemplateControl("txtModel") as ASPxTextBox;
        ASPxComboBox AssetClassCode = ASPxGridView1.FindEditFormTemplateControl("txtClass") as ASPxComboBox;
        ASPxTextBox remark = ASPxGridView1.FindEditFormTemplateControl("txtRemark") as ASPxTextBox;


        string Sql = "UPDATE CODE_Asset SET ASSET_NAME='" + AssetName.Text.Trim() + "',ASSET_SPEC='" + AssetSpec.Text.Trim() 
            + "', ASSET_MODEL='" +AssetModel.Text.Trim() + "', ASSET_CLASS_CODE='" + AssetClassCode.SelectedItem.Value.ToString().Trim() 
            + "', ASSET_REMARK='" + remark.Text.Trim()  + "' WHERE  COMPANY_CODE = '" + theCompanyCode + "' and ASSET_CODE = '" + AssetCode.Text.Trim() + "'";
        dc.ExeSql(Sql);

        setCondition();
        e.Cancel = true;
        ASPxGridView1.CancelEdit();

    }
    protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
    {
        if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
        {
            ///主键不可以修改
            (ASPxGridView1.FindEditFormTemplateControl("txtAssetCode") as ASPxTextBox).Enabled = false;

        }



    }
    protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        //判断是否重复
        if (ASPxGridView1.IsNewRowEditing)
        {
            string chSql = "SELECT ASSET_CODE, ASSET_NAME  FROM CODE_ASSET"
                + " WHERE COMPANY_CODE = '" + theCompanyCode + "' AND ASSET_CODE='" + e.NewValues["ASSET_CODE"].ToString() + "'";
            DataTable dt = dc.GetTable(chSql);
            if (dt.Rows.Count > 0)
            {
                e.RowError = "已存在相同的设备代码！";
            }
        }
    }
    
   
   

  

}
