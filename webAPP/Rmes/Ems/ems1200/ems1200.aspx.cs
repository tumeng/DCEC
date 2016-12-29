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
 * 功能概述：设备类别定义
 * 作者：李蒙蒙
 * 创建时间：2013-07-02
 */


public partial class Rmes_ems1200 : Rmes.Web.Base.BasePage
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
        string sql = "SELECT * FROM CODE_ASSET_CLASS WHERE COMPANY_CODE = '" + theCompanyCode + "' ORDER BY ASSET_CLASS_CODE";
        DataTable dt = dc.GetTable(sql);

        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
    }


    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        //新增
        ASPxTextBox uCode = ASPxGridView1.FindEditFormTemplateControl("txtClassCode") as ASPxTextBox;
        ASPxTextBox uName = ASPxGridView1.FindEditFormTemplateControl("txtClassName") as ASPxTextBox;

        string tc = uCode.Text.ToString().Trim();
        string tn = uName.Text.ToString().Trim();

        string Sql = "INSERT INTO CODE_ASSET_CLASS (COMPANY_CODE,ASSET_CLASS_CODE,ASSET_CLASS_NAME) "
             + "VALUES('" + theCompanyCode + "','" + tc + "','" + tn + "')";
        dc.ExeSql(Sql);

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        setCondition();
    }


    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //修改
        ASPxTextBox uCode = ASPxGridView1.FindEditFormTemplateControl("txtClassCode") as ASPxTextBox;
        ASPxTextBox uName = ASPxGridView1.FindEditFormTemplateControl("txtClassName") as ASPxTextBox;

        string tc = uCode.Text.ToString().Trim();
        string tn = uName.Text.ToString().Trim();

        string Sql = "UPDATE CODE_ASSET_CLASS SET ASSET_CLASS_NAME='" + tn + "'"
             + " WHERE  COMPANY_CODE = '" + theCompanyCode + "' and ASSET_CLASS_CODE = '" + tc + "'";
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
            (ASPxGridView1.FindEditFormTemplateControl("txtClassCode") as ASPxTextBox).Enabled = false;
        }
    }
    protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        //判断是否重复
        if (ASPxGridView1.IsNewRowEditing)
        {
            string chSql = "SELECT ASSET_CLASS_CODE FROM CODE_ASSET_CLASS"
                + " WHERE COMPANY_CODE = '" + theCompanyCode + "' AND ASSET_CLASS_CODE='" + e.NewValues["ASSET_CLASS_CODE"].ToString() + "'";
            DataTable dt = dc.GetTable(chSql);
            if (dt.Rows.Count > 0)
            {
                e.RowError = "已存在相同的代码！";
            }
        }
    }
}
