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
using Rmes.Pub.Data;
using Rmes.Pub.Function;
using DevExpress.Web.ASPxEditors;

/**
 * 功能概述：公司定义
 * 作者：李崇
 * 创建时间：2011-07-26
 */

public partial class Rmes_Sam_sam1100_sam1100 : Rmes.Web.Base.BasePage
{
    public dataConn theDc = new dataConn();
    PubJs js = new PubJs();

    protected void Page_Load(object sender, EventArgs e)
    {
        queryFunction();
    }


    //初始化GRIDVIEW
    private void queryFunction()
    {
        string sql = "SELECT COMPANY_CODE, COMPANY_NAME, COMPANY_NAME_BRIEF, COMPANY_NAME_EN, COMPANY_WEBSITE, COMPANY_ADDRESS FROM CODE_COMPANY";
        DataTable dt = theDc.GetTable(sql);

        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
    }

    //删除
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {

        theDc.setTheSql("select func_check_delete_data('CODE_COMPANY','MES','MES','MES','MES','" + e.Values["COMPANY_CODE"].ToString() + "') from dual");
        theDc.OpenConn();
        string theRet = theDc.GetValue();
        if (theRet != "Y")
        {
            ASPxGridView1.JSProperties.Add("cpCallbackName", "Delete");
            ASPxGridView1.JSProperties.Add("cpCompanyName", theRet);
           // theDc.CloseConn();
        }


        else
        {
            string dSql = "DELETE FROM CODE_COMPANY WHERE COMPANY_CODE='" + e.Values["COMPANY_CODE"].ToString() + "'";
            theDc.ExeSql(dSql);
        }

        e.Cancel = true;
        queryFunction();
    }
    protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
    {
        if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
        {
            //主键不可以修改
            (ASPxGridView1.FindEditFormTemplateControl("txtCCode") as ASPxTextBox).Enabled = false;

            
        }
    }
    protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        //判断超长
        if (e.NewValues["COMPANY_CODE"].ToString().Length > 30)
        {
            e.RowError = "公司代码字节长度不能超过30！";
        }
        if (e.NewValues["COMPANY_NAME"].ToString().Length > 30)
        {
            e.RowError = "公司名称字节长度不能超过30！";
        }
        if (e.NewValues["COMPANY_NAME_BRIEF"].ToString().Length > 30)
        {
            e.RowError = "公司简称字节长度不能超过30！";
        }
        if (e.NewValues["COMPANY_NAME_EN"].ToString().Length > 30)
        {
            e.RowError = "英文名称字节长度不能超过30！";
        }

        if (e.NewValues["COMPANY_WEBSITE"].ToString().Length > 30)
        {
            e.RowError = "公司网址字节长度不能超过30！";
        }

        if (e.NewValues["COMPANY_ADDRESS"].ToString().Length > 50)
        {
            e.RowError = "公司地址字节长度不能超过50！";
        }

        //判断为空
        if (e.NewValues["COMPANY_CODE"].ToString() == "" || e.NewValues["COMPANY_CODE"].ToString() == null)
        {
            e.RowError = "公司代码不能为空！";
        }
        if (e.NewValues["COMPANY_NAME"].ToString() == "" || e.NewValues["COMPANY_NAME"].ToString() == null)
        {
            e.RowError = "公司名称不能为空！";
        }
        if (e.NewValues["COMPANY_NAME_BRIEF"].ToString() == "" || e.NewValues["COMPANY_NAME_BRIEF"].ToString() == null)
        {
            e.RowError = "公司简称不能为空！";
        }
        if (e.NewValues["COMPANY_NAME_EN"].ToString() == "" || e.NewValues["COMPANY_NAME_EN"].ToString() == null)
        {
            e.RowError = "英文名称不能为空！";
        }

        if (e.NewValues["COMPANY_WEBSITE"].ToString() == "" || e.NewValues["COMPANY_WEBSITE"].ToString() == null)
        {
            e.RowError = "公司网址不能为空！";
        }



        //新增判断键值重复
        if (ASPxGridView1.IsNewRowEditing)
        {
            string chSql = "SELECT COMPANY_CODE, COMPANY_NAME, COMPANY_NAME_BRIEF, COMPANY_NAME_EN, COMPANY_WEBSITE, COMPANY_ADDRESS FROM CODE_COMPANY"
                + " WHERE COMPANY_CODE = '" + e.NewValues["COMPANY_CODE"].ToString() + "'";
            dataConn dc = new dataConn(chSql);
            if (dc.GetState() == true)
            {
                e.RowError = "公司代码重复，请重新命名或分配！";
            }
        }
    }
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxTextBox CCode = ASPxGridView1.FindEditFormTemplateControl("txtCCode") as ASPxTextBox;
        ASPxTextBox CName = ASPxGridView1.FindEditFormTemplateControl("txtCName") as ASPxTextBox;
        ASPxTextBox CNameBrief = ASPxGridView1.FindEditFormTemplateControl("txtCNameBrief") as ASPxTextBox;
        ASPxTextBox CNameEn = ASPxGridView1.FindEditFormTemplateControl("txtCNameEn") as ASPxTextBox;


        ASPxTextBox CWebSite = ASPxGridView1.FindEditFormTemplateControl("txtCWebSite") as ASPxTextBox;
        ASPxTextBox CAddress = ASPxGridView1.FindEditFormTemplateControl("txtCAddress") as ASPxTextBox;





        string inSql = "INSERT INTO CODE_COMPANY (COMPANY_CODE, COMPANY_NAME, COMPANY_NAME_BRIEF, COMPANY_NAME_EN, COMPANY_WEBSITE, COMPANY_ADDRESS) "
                 + "VALUES('" + CCode.Text.Trim() + "','" + CName.Text.Trim() + "','" + CNameBrief.Text.Trim() + "','" + CNameEn.Text.Trim() + "','" + CWebSite.Text.Trim() + "','" + CAddress.Text.Trim() + "')";
        dataConn inDc = new dataConn();
        inDc.ExeSql(inSql);
        inDc.CloseConn();

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        queryFunction();
    }
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxTextBox CCode = ASPxGridView1.FindEditFormTemplateControl("txtCCode") as ASPxTextBox;
        ASPxTextBox CName = ASPxGridView1.FindEditFormTemplateControl("txtCName") as ASPxTextBox;
        ASPxTextBox CNameBrief = ASPxGridView1.FindEditFormTemplateControl("txtCNameBrief") as ASPxTextBox;
        ASPxTextBox CNameEn = ASPxGridView1.FindEditFormTemplateControl("txtCNameEn") as ASPxTextBox;


        ASPxTextBox CWebSite = ASPxGridView1.FindEditFormTemplateControl("txtCWebSite") as ASPxTextBox;
        ASPxTextBox CAddress = ASPxGridView1.FindEditFormTemplateControl("txtCAddress") as ASPxTextBox;


        string upSql = "UPDATE CODE_COMPANY SET COMPANY_NAME='" + CName.Text.Trim() + "',COMPANY_NAME_BRIEF='" + CNameBrief.Text.Trim() + "',COMPANY_NAME_EN='" + CNameEn.Text.Trim() + "',"
                     + "COMPANY_WEBSITE='" + CWebSite.Text.Trim() + "', COMPANY_ADDRESS='" + CAddress.Text.Trim() + "' WHERE  COMPANY_CODE = '" + CCode.Text.Trim() + "'";
        theDc.ExeSql(upSql);

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        queryFunction();
    }
}
