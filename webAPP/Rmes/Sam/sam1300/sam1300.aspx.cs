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
 * 功能概述：角色定义
 * 作者：曹路圆
 * 创建时间：2011-07-25
 * 修改时间：2011-07-27
 */


public partial class Rmes_Sam_sam1300_sam1300 : Rmes.Web.Base.BasePage
{
    private dataConn dc = new dataConn();
    private PubCs pc = new PubCs();
    public string theCompanyCode;

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();

        //this.TranslateASPxControl(ASPxGridView1);


        if (!IsPostBack)
        {

        }
        setCondition();
    }


    private void setCondition()
    {
        //初始化GRIDVIEW
        string sql = "SELECT ROLE_CODE,ROLE_NAME,role_desc FROM CODE_ROLE WHERE COMPANY_CODE = '" + theCompanyCode + "'";
        DataTable dt = dc.GetTable(sql);

        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
    }

    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {

        //判断当前记录是否可以删除
        string strDelCode = e.Values["ROLE_CODE"].ToString();
        string strTableName = "CODE_ROLE";

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
            string Sql = "delete from code_role WHERE  COMPANY_CODE = '" + theCompanyCode + "' and ROLE_CODE = '" + strDelCode + "'";
            dc.ExeSql(Sql);
        }
                

        setCondition();
        e.Cancel = true;
    }


    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxTextBox uCode = ASPxGridView1.FindEditFormTemplateControl("txtUserCode") as ASPxTextBox;
        ASPxTextBox uName = ASPxGridView1.FindEditFormTemplateControl("txtUserName") as ASPxTextBox;
        ASPxMemo uDesc = ASPxGridView1.FindEditFormTemplateControl("txtUserDesc") as ASPxMemo;
        string sql = "select 'ROLE'||TRIM(TO_CHAR(SEQ_USER_ID.NEXTVAL,'000000')) from dual";
        dc.setTheSql(sql);
        string dece_code1 = dc.GetValue();

        string Sql = "INSERT INTO CODE_ROLE (COMPANY_CODE,ROLE_CODE,ROLE_NAME,ROLE_DESC) "
             + "VALUES('" + theCompanyCode + "','" + dece_code1 + "','" + uName.Text.Trim() + "','" + uDesc.Text.Trim() + "')";
        dc.ExeSql(Sql);
        

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        setCondition();

    }


    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {

        ASPxTextBox uCode = ASPxGridView1.FindEditFormTemplateControl("txtUserCode") as ASPxTextBox;
        ASPxTextBox uName = ASPxGridView1.FindEditFormTemplateControl("txtUserName") as ASPxTextBox;
        ASPxMemo uDesc = ASPxGridView1.FindEditFormTemplateControl("txtUserDesc") as ASPxMemo;
        string Sql = "UPDATE CODE_ROLE SET ROLE_NAME='" + uName.Text.Trim() + "' ,role_desc='" + uDesc .Text.Trim()+ "'"
             + " WHERE  COMPANY_CODE = '" + theCompanyCode + "' and ROLE_CODE = '" + uCode.Text.Trim() + "'";
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
            (ASPxGridView1.FindEditFormTemplateControl("txtUserCode") as ASPxTextBox).Enabled = false;
        }
        if (ASPxGridView1.IsNewRowEditing)
        {
            ///主键不可以修改
            (ASPxGridView1.FindEditFormTemplateControl("txtUserCode") as ASPxTextBox).Enabled = false;
            (ASPxGridView1.FindEditFormTemplateControl("txtUserCode") as ASPxTextBox).Visible = false;
            (ASPxGridView1.FindEditFormTemplateControl("ASPxLabel5") as ASPxLabel).Visible = false;
        }
    }



    protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        //判断超长
        if (e.NewValues["ROLE_CODE"].ToString().Length > 15)
        {
            e.RowError = "角色代码字节长度不能超过15！";
        }
        if (e.NewValues["ROLE_NAME"].ToString().Length > 15)
        {
            e.RowError = "角色名称字节长度不能超过15！";
        }

        //判断为空
        //if (e.NewValues["ROLE_CODE"].ToString() == "" || e.NewValues["ROLE_CODE"].ToString() == null)
        //{
        //    e.RowError = "角色代码不能为空！";
        //}
        if (e.NewValues["ROLE_NAME"].ToString() == "" || e.NewValues["ROLE_NAME"].ToString() == null)
        {
            e.RowError = "角色名称不能为空！";
        }


        //判断是否重复
        if (ASPxGridView1.IsNewRowEditing)
        {
            string chSql = "SELECT ROLE_CODE, ROLE_NAME  FROM CODE_ROLE"
                + " WHERE COMPANY_CODE = '" + theCompanyCode + "' AND ROLE_CODE='" + e.NewValues["ROLE_CODE"].ToString() + "'";
            DataTable dt = dc.GetTable(chSql);
            if (dt.Rows.Count > 0)
            {
                e.RowError = "已存在相同的角色代码！";
            }
        }
    }
}
