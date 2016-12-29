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

using System.Text.RegularExpressions;

using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxClasses;

using Rmes.Pub.Data;


/**
 * 功能概述：往来单位定义
 * 作者：徐莹
 * 创建时间：2011-11-08
 * 修改：曹路圆  2012-07-23
 * 将供应商和客户合并一个表，增加类别
 */


public partial class Rmes_mms1300 : Rmes.Web.Base.BasePage
{
    private dataConn dc = new dataConn();
    private PubCs pc = new PubCs();
    public string theCompanyCode;

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        setCondition();
    }


    private void setCondition()
    {
        //初始化GRIDVIEW
        string sql = "SELECT VENDOR_CODE,VENDOR_NAME,DECODE(TYPE,'A','供应商','B','客户',TYPE) TYPE FROM CODE_VENDOR WHERE COMPANY_CODE = '" + theCompanyCode + "' ORDER BY VENDOR_CODE";
        DataTable dt = dc.GetTable(sql);

        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
    }

    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {

        //判断当前记录是否可以删除
        string strDelCode = e.Values["VENDOR_CODE"].ToString();
        string strTableName = "CODE_VENDOR";

        dataConn theDataConn = new dataConn("select func_check_delete_data('" + strTableName + "','" + theCompanyCode + "','MES','MES','MES','" + strDelCode + "') from dual");

        theDataConn.OpenConn();
        string theRet = theDataConn.GetValue();
        if (theRet != "Y")
        {
            ASPxGridView1.JSProperties.Add("cpCallbackName", "Delete");
            ASPxGridView1.JSProperties.Add("cpCallbackRet", theRet);
            theDataConn.CloseConn();
        }
        else
        {
            //删除操作
            string Sql = "delete from CODE_VENDOR WHERE  COMPANY_CODE = '" + theCompanyCode + "' and VENDOR_CODE = '" + strDelCode + "'";
            dc.ExeSql(Sql);
        }
                

        setCondition();
        e.Cancel = true;
    }


    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        //新增
        ASPxTextBox uCode = ASPxGridView1.FindEditFormTemplateControl("txtVendorCode") as ASPxTextBox;
        ASPxTextBox uName = ASPxGridView1.FindEditFormTemplateControl("txtVendorName") as ASPxTextBox;
        ASPxComboBox uType = ASPxGridView1.FindEditFormTemplateControl("ASPxComType") as ASPxComboBox;

        string Sql = "INSERT INTO CODE_VENDOR (COMPANY_CODE,VENDOR_CODE,VENDOR_NAME,TYPE) "
             + "VALUES('" + theCompanyCode + "','" + uCode.Text.Trim() + "','" + uName.Text.Trim() + "','" + uType.SelectedItem.Value + "')";
        dc.ExeSql(Sql);
        
        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        setCondition();
    }


    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //修改
        ASPxTextBox uCode = ASPxGridView1.FindEditFormTemplateControl("txtVendorCode") as ASPxTextBox;
        ASPxTextBox uName = ASPxGridView1.FindEditFormTemplateControl("txtVendorName") as ASPxTextBox;
        ASPxComboBox uType = ASPxGridView1.FindEditFormTemplateControl("ASPxComType") as ASPxComboBox;

        string Sql = "UPDATE CODE_VENDOR SET VENDOR_NAME='" + uName.Text.Trim() + "', TYPE ='" + uType.SelectedItem.Value + "'" 
             + " WHERE  COMPANY_CODE = '" + theCompanyCode + "' and VENDOR_CODE = '" + uCode.Text.Trim() + "'";
        dc.ExeSql(Sql);

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        setCondition();

    }
    protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
    {
        if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
        {
            ///主键不可以修改
            (ASPxGridView1.FindEditFormTemplateControl("txtVendorCode") as ASPxTextBox).Enabled = false;
        }
    }
    protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        //判断是否重复
        if (ASPxGridView1.IsNewRowEditing)
        {
            string chSql = "SELECT VENDOR_CODE FROM CODE_VENDOR"
                + " WHERE COMPANY_CODE = '" + theCompanyCode + "' AND VENDOR_CODE='" + e.NewValues["VENDOR_CODE"].ToString() + "'";
            DataTable dt = dc.GetTable(chSql);
            if (dt.Rows.Count > 0)
            {
                e.RowError = "已存在相同的往来单位代码！";
            }
        }
    }

    protected void btnXlsExport_Click(object sender, EventArgs e)
    {
        //gridExport.WriteXlsToResponse("往来单位清单");
    }
}
