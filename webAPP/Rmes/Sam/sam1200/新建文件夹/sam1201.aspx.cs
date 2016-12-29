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
using Rmes.Pub.Data;
using Rmes.Pub.Function;
using System.Security.Cryptography;
using System.Text;
using System.IO;

/**
 * 功能概述：现场用户定义
 * 作者：唐海林
 * 创建时间：2016-09-10
 */
public partial class Rmes_Sam_sam1200_sam1201 : Rmes.Web.Base.BasePage
{
    public string theCompanyCode;
    public dataConn theDc = new dataConn();
    public PubCs thePubPc = new PubCs();

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();

        queryFunction();
        string a = thePubPc.AESEncrypt("admin");
    }

    //初始化GRIDVIEW
    private void queryFunction()
    {
        string sql = "SELECT A.COMPANY_CODE, A.USER_ID, A.USER_CODE, A.USER_NAME FROM CODE_USER A WHERE A.COMPANY_CODE = '" + theCompanyCode + "' and user_type='B'";
        DataTable dt = theDc.GetTable(sql);

        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
    }

    // 修改
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string userId = (ASPxGridView1.FindEditFormTemplateControl("HidUserId") as HiddenField).Value;
        ASPxTextBox uCode = ASPxGridView1.FindEditFormTemplateControl("txtUCode") as ASPxTextBox;
        ASPxTextBox uName = ASPxGridView1.FindEditFormTemplateControl("txtUName") as ASPxTextBox;

        string upSql = "UPDATE CODE_USER SET USER_CODE='" + uCode.Text.Trim().ToUpper() + "',USER_NAME='" + uName.Text.Trim() + "' "
                     + "WHERE  COMPANY_CODE = '" + theCompanyCode + "' AND USER_CODE='" + uCode.Text.Trim().ToUpper() + "'";
        theDc.ExeSql(upSql);

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        queryFunction();
    }
    // 新增
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxTextBox uCode = ASPxGridView1.FindEditFormTemplateControl("txtUCode") as ASPxTextBox;
        ASPxTextBox uName = ASPxGridView1.FindEditFormTemplateControl("txtUName") as ASPxTextBox;

        string inSql = "INSERT INTO CODE_USER (COMPANY_CODE, USER_ID, USER_CODE, USER_NAME,user_type) "
                 + "VALUES('" + theCompanyCode + "',TRIM(TO_CHAR(SEQ_USER_ID.NEXTVAL,'000000')),'" + uCode.Text.Trim().ToUpper() + "','" + uName.Text.Trim() + "' ,'B')";
        dataConn inDc = new dataConn();
        inDc.ExeSql(inSql);
        inDc.CloseConn();

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        queryFunction();
    }
    // 进入编辑界面时的处理
    protected void ASPxGridView1_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {

    }
    // 数据校验
    protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        //判断超长
        if (e.NewValues["USER_CODE"].ToString().Length > 30)
        {
            e.RowError = "用户代码字节长度不能超过30！";
        }
        if (e.NewValues["USER_NAME"].ToString().Length > 30)
        {
            e.RowError = "姓名字节长度不能超过30！";
        }
        
        //判断为空
        if (e.NewValues["USER_CODE"].ToString() == "" || e.NewValues["USER_CODE"].ToString() == null)
        {
            e.RowError = "用户代码不能为空！";
        }
        if (e.NewValues["USER_NAME"].ToString() == "" || e.NewValues["USER_NAME"].ToString() == null)
        {
            e.RowError = "姓名不能为空！";
        }
       
        if (!ASPxGridView1.IsNewRowEditing && (ASPxGridView1.FindEditFormTemplateControl("chValidFlag") as ASPxCheckBox).Checked == true)
        {
            if (e.OldValues["USER_CODE"].ToString() != e.NewValues["USER_CODE"].ToString())
            {
                //string chSql = "SELECT COMPANY_CODE, USER_CODE  FROM CODE_USER"
                //            + " WHERE COMPANY_CODE = '" + theCompanyCode + "' AND USER_CODE='" + e.NewValues["USER_CODE"].ToString() + "' AND VALID_FLAG='Y'";
                //DataTable dt = theDc.GetTable(chSql);
                //if (dt.Rows.Count == 1)
                //{
                //    e.RowError = "用户代码重复！";
                //}
                checkUnique(e);
            }

        }
    }
    private void checkUnique(DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        string chSql = "SELECT COMPANY_CODE, USER_CODE  FROM CODE_USER"
                                + " WHERE COMPANY_CODE = '" + theCompanyCode + "' AND USER_CODE='" + e.NewValues["USER_CODE"].ToString() + "' ";
        DataTable dt = theDc.GetTable(chSql);
        if (dt.Rows.Count == 1)
        {
            e.RowError = "用户代码重复！";
        }
    }
    // 删除
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        //判断当前记录是否可以删除
        {
            string delStr = e.Values["USER_ID"].ToString();
            //删除操作
            //string dSql = "DELETE FROM CODE_USER WHERE COMPANY_CODE='" + theCompanyCode + "' AND USER_CODE='" + e.Values["USER_CODE"].ToString() + "'";
            //theDc.ExeSql(dSql);
            string dSql = "DELETE FROM CODE_USER WHERE COMPANY_CODE='" + theCompanyCode + "' AND USER_ID='" + e.Values["USER_ID"].ToString() + "'";
            theDc.ExeSql(dSql);
        }
        e.Cancel = true;
        queryFunction();
    }

}
