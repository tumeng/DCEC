/**
 * 功能概述：用户生产线权限定义
 * 作者：游晓航
 * 创建时间：2016-06-12
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

using Rmes.DA.Base;
using Rmes.DA.Procedures;
using Rmes.Web.Base;

public partial class Rmes_Sam_sam2500_sam2500 : BasePage
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

        //初始化新增模版里生产线下拉列表
        initRole();

        setCondition();
    }

    private void initRole()
    {
        //初始化生产线下拉列表
        string sql = "SELECT RMES_ID,PLINE_CODE,PLINE_NAME FROM code_product_line WHERE COMPANY_CODE = '" + theCompanyCode + "'";
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
        string sql = "SELECT * FROM vw_rel_user_pline WHERE COMPANY_CODE = '" + theCompanyCode + "' and USER_CODE is not null";
         //string sql="select A.RMES_ID,A.COMPANY_CODE,A.USER_ID,B.USER_NAME,C.Rmes_Id,C.PLINE_CODE,C.PLINE_NAME,B.USER_CODE "
         //    +"from REL_USER_PLINE A LEFT JOIN CODE_PRODUCT_LINE C ON A.COMPANY_CODE=C.COMPANY_CODE AND C.Rmes_Id=A.Pline_Code "
         //    + "  LEFT JOIN CODE_USER B ON A.COMPANY_CODE=B.COMPANY_CODE AND A.USER_ID=B.USER_ID "
         //    + "WHERE COMPANY_CODE = '" + theCompanyCode + "' ORDER BY 1,2,4 ";
    



  
    
   
        DataTable dt = dc.GetTable(sql);

        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
    }
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {

        string strCode = e.Values["RMES_ID"].ToString();
        string strID = e.Values["USER_ID"].ToString();
        string strTableName = "REL_USER_PLINE";

        dataConn theDataConn = new dataConn("select func_check_delete_data('" + strTableName + "','" + theCompanyCode + "','MES','MES','MES','" + strCode + "') from dual");

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
            //确认删除
            string Sql = "delete from REL_USER_PLINE WHERE  COMPANY_CODE = '" + theCompanyCode + "' and USER_ID = '" + strID + "'and RMES_ID = '" + strCode + "'";
            dc.ExeSql(Sql);
        }


        setCondition();
        e.Cancel = true;
        ////删除
        ////调用存储过程进行处理
        //userManager theUserManager = (userManager)Session["theUserManager"];
        //MW_MODIFY_USER_PLINE sp = new MW_MODIFY_USER_PLINE() { 
        //    THEFUNCTION1 = "DELETE",
        //    THECOMPANYCODE1 = theCompanyCode,
        //    THEUSERID1 = e.Values["USER_ID"].ToString(),
        //    THEPLINECODE1 = e.Values["PLINE_CODE"].ToString()
        //};

        //Procedure.run(sp);

        //dataConn theDataConn = new dataConn();
        //theDataConn.theComd.CommandType = CommandType.StoredProcedure;
        //theDataConn.theComd.CommandText = "MW_MODIFY_USER_PLINE";

        //theDataConn.theComd.Parameters.Add("THEFUNCTION1", OracleDbType.Varchar2).Value = "DELETE";
        //theDataConn.theComd.Parameters.Add("THEFUNCTION1", OracleDbType.Varchar2).Direction = ParameterDirection.Input;

        //theDataConn.theComd.Parameters.Add("THECOMPANYCODE1", OracleDbType.Varchar2).Value = theCompanyCode;
        //theDataConn.theComd.Parameters.Add("THECOMPANYCODE1", OracleDbType.Varchar2).Direction = ParameterDirection.Input;

        //theDataConn.theComd.Parameters.Add("THEUSERID1", OracleDbType.Varchar2).Value = e.Keys["USER_ID"].ToString();
        //theDataConn.theComd.Parameters.Add("THEUSERID1", OracleDbType.Varchar2).Direction = ParameterDirection.Input;

        //theDataConn.theComd.Parameters.Add("THEPLINECODE1", OracleDbType.Varchar2).Value = e.Keys["PLINE_CODE"].ToString();
        //theDataConn.theComd.Parameters.Add("THEPLINECODE1", OracleDbType.Varchar2).Direction = ParameterDirection.Input;

        //theDataConn.OpenConn();
        //theDataConn.theComd.ExecuteNonQuery();

        //theDataConn.CloseConn();

        setCondition();
        e.Cancel = true;
    }

    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {

             int indexPline, indexUser;
        //string sql;
        ASPxGridLookup GridLookupRole = ASPxGridView1.FindEditFormTemplateControl("GridLookupRole") as ASPxGridLookup;
        ASPxGridLookup gridLookupUser = ASPxGridView1.FindEditFormTemplateControl("GridLookupUser") as ASPxGridLookup;

        List<object> Plines = GridLookupRole.GridView.GetSelectedFieldValues("RMES_ID");
        List<object> Users = gridLookupUser.GridView.GetSelectedFieldValues("USER_ID");

        for (indexUser = 0; indexUser < Users.Count; indexUser++)
        {
            for (indexPline = 0; indexPline < Plines.Count; indexPline++)
            {
                string Sql1 = "delete from REL_USER_PLINE WHERE  COMPANY_CODE = '" + theCompanyCode + "' and USER_ID = '" + Users[indexUser] + "'and PLINE_CODE = '" + Plines[indexPline] + "'";
                dc.ExeSql(Sql1);

                string Sql = "INSERT INTO REL_USER_PLINE (COMPANY_CODE,PLINE_CODE,USER_ID,RMES_ID) "
                             + "VALUES('" + theCompanyCode + "','" + Plines[indexPline] + "','" + Users[indexUser] + "',SEQ_RMES_ID.NextVal)";
                dc.ExeSql(Sql);
            }
        }
        ////新增
        //int indexUser, indexRole;
        //ASPxGridLookup gridLookupUser = ASPxGridView1.FindEditFormTemplateControl("GridLookupUser") as ASPxGridLookup;
        //ASPxGridLookup gridLookupRole = ASPxGridView1.FindEditFormTemplateControl("GridLookupRole") as ASPxGridLookup;

        //List<object> Users = gridLookupUser.GridView.GetSelectedFieldValues("USER_ID");
        //List<object> Roles = gridLookupRole.GridView.GetSelectedFieldValues("PLINE_CODE");
        //for (indexUser = 0; indexUser < Users.Count; indexUser++)
        //{
        //    for (indexRole = 0; indexRole < Roles.Count; indexRole++)
        //    {

        //        MW_MODIFY_USER_PLINE sp = new MW_MODIFY_USER_PLINE()
        //        {
        //            THEFUNCTION1 = "ADD",
        //            THECOMPANYCODE1 = theCompanyCode,
        //            THEUSERID1 = Users[indexUser].ToString(),
        //            THEPLINECODE1 = Roles[indexRole].ToString()
        //        };

        //        Procedure.run(sp);



                //dataConn theDataConn = new dataConn();
                //theDataConn.theComd.CommandType = CommandType.StoredProcedure;
                //theDataConn.theComd.CommandText = "MW_MODIFY_USER_PLINE";

                //theDataConn.theComd.Parameters.Add("THEFUNCTION1", OracleDbType.Varchar2).Value = "ADD";
                //theDataConn.theComd.Parameters.Add("THEFUNCTION1", OracleDbType.Varchar2).Direction = ParameterDirection.Input;

                //theDataConn.theComd.Parameters.Add("THECOMPANYCODE1", OracleDbType.Varchar2).Value = theCompanyCode;
                //theDataConn.theComd.Parameters.Add("THECOMPANYCODE1", OracleDbType.Varchar2).Direction = ParameterDirection.Input;

                //theDataConn.theComd.Parameters.Add("THEUSERID1", OracleDbType.Varchar2).Value = Users[indexUser].ToString();
                //theDataConn.theComd.Parameters.Add("THEUSERID1", OracleDbType.Varchar2).Direction = ParameterDirection.Input;

                //theDataConn.theComd.Parameters.Add("THEPLINECODE1", OracleDbType.Varchar2).Value = Roles[indexRole].ToString();
                //theDataConn.theComd.Parameters.Add("THEPLINECODE1", OracleDbType.Varchar2).Direction = ParameterDirection.Input;

                //theDataConn.OpenConn();
                //theDataConn.theComd.ExecuteNonQuery();

                //theDataConn.CloseConn();
        //    }
        //}
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
