/**
 * 功能概述：人员角色定义
 * 作者：徐莹
 * 创建时间：2011-07-25
 */
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rmes.Pub.Data;
using System.Data;
using System.Collections;
using DevExpress.Web.ASPxGridLookup;
using Oracle.DataAccess.Client;
using Rmes.DA.Procedures;

public partial class Rmes_Sam_sam2100_sam2100 : Rmes.Web.Base.BasePage  
{
    private dataConn dc = new dataConn();
    private PubCs pc = new PubCs();
    public string theCompanyCode;

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();

        //初始化新增模版里员工下拉列表
        initUser();

        //初始化新增模版里角色下拉列表
        initRole();

        setCondition();
    }

    private void initRole()
    {
        //初始化角色下拉列表
        string sql = "SELECT ROLE_CODE,ROLE_NAME FROM CODE_ROLE WHERE COMPANY_CODE = '" + theCompanyCode + "'";
        SqlRole.SelectCommand = sql;
        SqlRole.DataBind();
    }

    private void initUser()
    {
        //初始化用户下拉列表
        string sql = "SELECT USER_ID,USER_CODE,USER_NAME FROM VW_CODE_USER WHERE COMPANY_CODE = '" + theCompanyCode + "' ORDER BY USER_CODE";
        SqlUser.SelectCommand = sql;
        SqlUser.DataBind();
    }
    private void setCondition()
    {
        //初始化GRIDVIEW
        string sql = "SELECT COMPANY_CODE,USER_CODE,ROLE_CODE,USER_NAME,ROLE_NAME,USER_ID FROM VW_REL_USER_ROLE WHERE COMPANY_CODE = '" + theCompanyCode + "' ORDER BY USER_CODE,ROLE_CODE";
        DataTable dt = dc.GetTable(sql);

        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
    }
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        //删除
        //调用存储过程进行处理

        MW_MODIFY_USER_ROLE sp = new MW_MODIFY_USER_ROLE()
        {
            THEFUNCTION1 = "DELETE",
            THECOMPANYCODE1 = theCompanyCode,
            THEUSERID1 = e.Keys["USER_ID"].ToString(),
            THEROLECODE1 = e.Values["ROLE_CODE"].ToString()
        };
        Rmes.DA.Base.Procedure.run(sp);

        //dataConn theDataConn = new dataConn();
        //theDataConn.theComd.CommandType = CommandType.StoredProcedure;
        //theDataConn.theComd.CommandText = "MW_MODIFY_USER_ROLE";

        //theDataConn.theComd.Parameters.Add("THEFUNCTION1", OracleDbType.Varchar2).Value = "DELETE";
        //theDataConn.theComd.Parameters.Add("THEFUNCTION1", OracleDbType.Varchar2).Direction = ParameterDirection.Input;

        //theDataConn.theComd.Parameters.Add("THECOMPANYCODE1", OracleDbType.Varchar2).Value = theCompanyCode;
        //theDataConn.theComd.Parameters.Add("THECOMPANYCODE1", OracleDbType.Varchar2).Direction = ParameterDirection.Input;

        //theDataConn.theComd.Parameters.Add("THEUSERID1", OracleDbType.Varchar2).Value = e.Keys["USER_ID"].ToString();
        //theDataConn.theComd.Parameters.Add("THEUSERID1", OracleDbType.Varchar2).Direction = ParameterDirection.Input;

        //theDataConn.theComd.Parameters.Add("THEROLECODE1", OracleDbType.Varchar2).Value = e.Values["ROLE_CODE"].ToString();
        //theDataConn.theComd.Parameters.Add("THEROLECODE1", OracleDbType.Varchar2).Direction = ParameterDirection.Input;

        //theDataConn.OpenConn();
        //theDataConn.theComd.ExecuteNonQuery();

        //theDataConn.CloseConn();

        setCondition();
        e.Cancel = true;
    }
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        //新增
        int indexUser, indexRole;
        ASPxGridLookup gridLookupUser = ASPxGridView1.FindEditFormTemplateControl("GridLookupUser") as ASPxGridLookup;
        ASPxGridLookup gridLookupRole = ASPxGridView1.FindEditFormTemplateControl("GridLookupRole") as ASPxGridLookup;

        List<object> Users = gridLookupUser.GridView.GetSelectedFieldValues("USER_ID");
        List<object> Roles = gridLookupRole.GridView.GetSelectedFieldValues("ROLE_CODE");

        for (indexUser = 0; indexUser < Users.Count; indexUser++)
        {
            for (indexRole = 0; indexRole < Roles.Count; indexRole++)
            {

                MW_MODIFY_USER_ROLE sp = new MW_MODIFY_USER_ROLE() { 
                     THEFUNCTION1 = "ADD",
                     THECOMPANYCODE1 = theCompanyCode,
                     THEUSERID1 = Users[indexUser].ToString(),
                     THEROLECODE1 = Roles[indexRole].ToString()
                };
                Rmes.DA.Base.Procedure.run(sp);

                //dataConn theDataConn = new dataConn();
                //theDataConn.theComd.CommandType = CommandType.StoredProcedure;
                //theDataConn.theComd.CommandText = "MW_MODIFY_USER_ROLE";

                //theDataConn.theComd.Parameters.Add("THEFUNCTION1", OracleDbType.Varchar2).Value = "ADD";
                //theDataConn.theComd.Parameters.Add("THEFUNCTION1", OracleDbType.Varchar2).Direction = ParameterDirection.Input;

                //theDataConn.theComd.Parameters.Add("THECOMPANYCODE1", OracleDbType.Varchar2).Value = theCompanyCode;
                //theDataConn.theComd.Parameters.Add("THECOMPANYCODE1", OracleDbType.Varchar2).Direction = ParameterDirection.Input;

                //theDataConn.theComd.Parameters.Add("THEUSERID1", OracleDbType.Varchar2).Value = Users[indexUser].ToString();
                //theDataConn.theComd.Parameters.Add("THEUSERID1", OracleDbType.Varchar2).Direction = ParameterDirection.Input;

                //theDataConn.theComd.Parameters.Add("THEROLECODE1", OracleDbType.Varchar2).Value = Roles[indexRole].ToString();
                //theDataConn.theComd.Parameters.Add("THEROLECODE1", OracleDbType.Varchar2).Direction = ParameterDirection.Input;

                //theDataConn.OpenConn();
                //theDataConn.theComd.ExecuteNonQuery();

                //theDataConn.CloseConn();
            }
        }
        setCondition();
        e.Cancel = true;
        ASPxGridView1.CancelEdit();
    }
    protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
    {
        ASPxGridLookup gridLookupUser = ASPxGridView1.FindEditFormTemplateControl("GridLookupUser") as ASPxGridLookup;
        gridLookupUser.GridView.Width = 250;

        ASPxGridLookup gridLookupRole = ASPxGridView1.FindEditFormTemplateControl("GridLookupRole") as ASPxGridLookup;
        gridLookupRole.GridView.Width = 250;
    }
}
