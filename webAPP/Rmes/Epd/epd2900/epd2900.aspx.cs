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
using DevExpress.Web.ASPxGridLookup;
using Oracle.DataAccess.Client;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxClasses.Internal;


using System.Collections.Generic;


/**
 * 功能概述：班组人员定义
 * 作者：杨少霞
 * 创建时间：2011-08-03
 */

public partial class Rmes_epd2900 : Rmes.Web.Base.BasePage
{
    private dataConn theDc = new dataConn();
    private PubCs thePc = new PubCs();
    public string theCompanyCode,theUserId;
    private string theProgramCode; 
    public string thePlCode = "";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theUserId = theUserManager.getUserId();
        theProgramCode = "epd2900";
        if (!IsPostBack)
        {
            //初始化生产线下拉列表
            initPLine();
        }
        //取得生产线的值
        if (CombPlCodeQ.SelectedIndex != -1)
        {
            thePlCode = CombPlCodeQ.SelectedItem.Value.ToString();
        }
        
        //初始化新增模版里员工下拉列表
        initUser();

        //初始化新增模版里班组下拉列表
        initTeam();

        queryFunction();
    }

    private void initPLine()
    {
        //初始化生产线
        string sql = "SELECT A.PLINE_CODE,D.PLINE_NAME FROM vw_user_role_program A  "
                   + "LEFT JOIN CODE_PRODUCT_LINE D ON A.COMPANY_CODE=D.COMPANY_CODE AND A.PLINE_CODE=D.PLINE_CODE  "
                   + "WHERE A.COMPANY_CODE = '" + theCompanyCode + "' AND A.USER_ID='" + theUserId + "' and a.program_code='"+theProgramCode+"' "
                   + "ORDER BY A.PLINE_CODE";
        SqlPLine.SelectCommand = sql;
        SqlPLine.DataBind();

        CombPlCodeQ.DataSource = SqlPLine;
        CombPlCodeQ.TextField = "PLINE_NAME";
        CombPlCodeQ.ValueField = "PLINE_CODE";
        CombPlCodeQ.DataBind();
        CombPlCodeQ.SelectedIndex = 0;
    }

    private void initTeam()
    {
        //初始化班组下拉列表
        string sql = "SELECT A.RMES_ID,A.TEAM_NAME,A.TEAM_CODE FROM CODE_TEAM A LEFT JOIN CODE_PRODUCT_LINE B ON A.PLINE_CODE=B.PLINE_CODE WHERE A.COMPANY_CODE = '" + theCompanyCode + "' AND B.pline_code='" + thePlCode + "' ";
        SqlTeam.SelectCommand = sql;
        SqlTeam.DataBind();
    }

    private void initUser()
    {
        //初始化用户下拉列表
        string sql = "SELECT A.USER_ID,A.USER_CODE,A.USER_NAME FROM CODE_USER A  WHERE A.COMPANY_CODE = '" + theCompanyCode + "' and user_type='B' ";
        SqlUser.SelectCommand = sql;
        SqlUser.DataBind();
    }



    private void queryFunction()
    {
        //初始化GRIDVIEW
        string sql = "SELECT COMPANY_CODE,PLINE_CODE,TEAM_CODE,USER_ID,USER_CODE,USER_NAME,TEAM_NAME,PLINE_NAME FROM VW_REL_TEAM_USER WHERE COMPANY_CODE = '" + theCompanyCode + "' AND PLINE_CODE='" + thePlCode + "' ORDER BY USER_CODE";
        DataTable dt = theDc.GetTable(sql);

        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
    }
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        //删除
        string delSql = "DELETE FROM REL_TEAM_USER WHERE COMPANY_CODE='" + theCompanyCode + "' AND PLINE_CODE='" + thePlCode + "' AND USER_ID='" + e.Keys["USER_ID"].ToString() + "'";
        theDc.ExeSql(delSql);

        queryFunction();
        e.Cancel = true;
        
    }
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        //新增
        int indexUser;
        ASPxGridLookup gridLookupUser = ASPxGridView1.FindEditFormTemplateControl("GridLookupUser") as ASPxGridLookup;
        ASPxGridLookup gridLookupTeam = ASPxGridView1.FindEditFormTemplateControl("GridLookupTeam") as ASPxGridLookup;

        List<object> Users = gridLookupUser.GridView.GetSelectedFieldValues("USER_ID");
        string Teams = gridLookupTeam.Value.ToString();

        for (indexUser = 0; indexUser < Users.Count; indexUser++)
        {
            string inSql = "INSERT INTO REL_TEAM_USER(COMPANY_CODE,PLINE_CODE,TEAM_CODE,USER_ID)"
                         + " VALUES('" + theCompanyCode + "','" + thePlCode + "','" + Teams + "','" + Users[indexUser].ToString() + "')";
            theDc.ExeSql(inSql);
        }  
        queryFunction();
        e.Cancel = true;
        ASPxGridView1.CancelEdit();
    }
    protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
    {
        ASPxGridLookup gridLookupUser = ASPxGridView1.FindEditFormTemplateControl("GridLookupUser") as ASPxGridLookup;
        gridLookupUser.GridView.Width = 250;

        ASPxGridLookup gridLookupTeam = ASPxGridView1.FindEditFormTemplateControl("gridLookupTeam") as ASPxGridLookup;
        gridLookupTeam.GridView.Width = 250;
    }
    protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        //判断为空
        ASPxGridLookup gridLookupTeam = ASPxGridView1.FindEditFormTemplateControl("GridLookupTeam") as ASPxGridLookup;
        if (gridLookupTeam.Value == null)
        {
            e.RowError = "班组代码不能为空！";
        }

        ASPxGridLookup gridLookupUser = ASPxGridView1.FindEditFormTemplateControl("GridLookupUser") as ASPxGridLookup;
        if (gridLookupUser.Value == null)
        {
            e.RowError = "用户代码不能为空！";
        }
        else
        {
            //判断同一个用户只能分配一个班组
            List<object> Users = gridLookupUser.GridView.GetSelectedFieldValues("USER_ID");
            List<object> UsersName = gridLookupUser.GridView.GetSelectedFieldValues("USER_NAME");
            for (int indexUser = 0; indexUser < Users.Count; indexUser++)
            {
                string chSql = "SELECT COMPANY_CODE,PLINE_CODE,TEAM_CODE,USER_ID FROM REL_TEAM_USER WHERE COMPANY_CODE='" + theCompanyCode + "' AND PLINE_CODE='" + thePlCode + "' AND USER_ID='" + Users[indexUser].ToString() + "'";
                DataTable dt = theDc.GetTable(chSql);
                if (dt.Rows.Count >= 1)
                {
                    e.RowError = UsersName[indexUser].ToString()+"已经被分配过班组！";
                }


            }
        }
    }
    protected void CombPlCodeQ_SelectedIndexChanged(object sender, EventArgs e)
    {
        queryFunction();
    }
}
