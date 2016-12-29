/**
 * 功能概述：人员权限定义
 * 作者：游晓航
 * 创建时间：2016-06-08
 * 修改时间：2016-07-27
 */
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Rmes.Pub.Data;
using System.Data;
using System.Collections;
using DevExpress.Web.ASPxGridLookup;
using Oracle.DataAccess.Client;
using Rmes.Web.Base;
using Rmes.DA.Base;
using Rmes.DA.Procedures;

public partial class Rmes_sam2300 :Rmes.Web.Base.BasePage 
{
    private dataConn dc = new dataConn();
    private PubCs pc = new PubCs();
    public string theCompanyCode;

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();

        //初始化新增模版里程序下拉列表
        initProgram();

        //初始化新增模版里人员下拉列表
        initUser();

        //初始化新增模版里生产线下拉列表
        initPline();


        setCondition();
    }

    private void initUser()
    {
        //初始化人员下拉列表
        string sql = "SELECT USER_ID,USER_CODE,USER_NAME FROM CODE_USER WHERE COMPANY_CODE = '" + theCompanyCode + "' and user_type='A' ";
        SqlUser.SelectCommand = sql;
        SqlUser.DataBind();
    }

    private void initPline()
    {
        //初始化新增模版里程序下拉列表
        string sql = " SELECT PLINE_CODE,PLINE_NAME FROM CODE_PRODUCT_LINE WHERE COMPANY_CODE = '" + theCompanyCode + "'";
        SqlPline.SelectCommand = sql;
        SqlPline.DataBind();
    }
    
    
    private void initProgram()
    {
        //初始化新增模版里程序下拉列表
        string sql = "SELECT PROGRAM_CODE,PROGRAM_NAME FROM CODE_PROGRAM WHERE COMPANY_CODE = '" + theCompanyCode + "'";
        SqlProgram.SelectCommand = sql;
        SqlProgram.DataBind();
    }
    private void setCondition()
    {
        //初始化GRIDVIEW
        string sql = "SELECT COMPANY_CODE,USER_CODE,PROGRAM_CODE,USER_NAME,PROGRAM_NAME,USER_ID,PLINE_CODE,PLINE_NAME FROM VW_REL_USER_PROGRAM WHERE COMPANY_CODE = '" + theCompanyCode + "' ORDER BY USER_CODE,PROGRAM_CODE";
        DataTable dt = dc.GetTable(sql);

        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();

        string sql2 = "SELECT DISTINCT A.USER_ID,A.USER_CODE,A.USER_NAME,B.PROGRAM_CODE, C.PROGRAM_NAME,b.pline_code FROM VW_USER_ROLE_PROGRAM B  LEFT JOIN CODE_USER A "
                    + " ON A.USER_ID=B.USER_ID LEFT JOIN CODE_PROGRAM C ON B.PROGRAM_CODE = C.PROGRAM_CODE WHERE A.COMPANY_CODE = '" + theCompanyCode + "' ORDER BY A.USER_CODE";
        DataTable dt2 = dc.GetTable(sql2);
        ASPxGridView2.DataSource = dt2;
        ASPxGridView2.DataBind();
    }
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        //删除
        //调用存储过程进行处理

        MW_MODIFY_USER_PROGRAM sp = new MW_MODIFY_USER_PROGRAM()
        {
            THEFUNCTION1 = "DELETE",
            THECOMPANYCODE1 = theCompanyCode,
            THEUSERID1 = e.Keys["USER_ID"].ToString(),
            THEPROGRAMCODE1 = e.Values["PROGRAM_CODE"].ToString(),
            THEPLINECODE1 = e.Values["PLINE_CODE"].ToString()
        };

        Procedure.run(sp);

       

        setCondition();
        e.Cancel = true;
    }
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        //新增
        int indexProgram, indexUser, indexPline;
        ASPxGridLookup gridLookupProgram = ASPxGridView1.FindEditFormTemplateControl("GridLookupProgram") as ASPxGridLookup;
        ASPxGridLookup gridLookupUser = ASPxGridView1.FindEditFormTemplateControl("GridLookupUser") as ASPxGridLookup;
        ASPxGridLookup gridLookupPline = ASPxGridView1.FindEditFormTemplateControl("GridLookupPline") as ASPxGridLookup;

        List<object> Programs = gridLookupProgram.GridView.GetSelectedFieldValues("PROGRAM_CODE");
        List<object> Users = gridLookupUser.GridView.GetSelectedFieldValues("USER_ID");
        List<object> Plines = gridLookupPline.GridView.GetSelectedFieldValues("PLINE_CODE");

        for (indexProgram = 0; indexProgram < Programs.Count; indexProgram++)
        {
            for (indexUser = 0; indexUser < Users.Count; indexUser++)
            {
                //调用存储过程进行处理
                for (indexPline = 0; indexPline < Plines.Count; indexPline++)
                {
                    MW_MODIFY_USER_PROGRAM sp = new MW_MODIFY_USER_PROGRAM()
                    {
                        THEFUNCTION1 = "ADD",
                        THECOMPANYCODE1 = theCompanyCode,
                        THEUSERID1 = Users[indexUser].ToString(),
                        THEPROGRAMCODE1 = Programs[indexProgram].ToString(),
                        THEPLINECODE1 = Plines[indexPline].ToString()
                    };

                    Procedure.run(sp);
                }

                
            }
        }
        setCondition();
        e.Cancel = true;
        ASPxGridView1.CancelEdit();
    }
    protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
    {
        ASPxGridLookup gridLookupProgram = ASPxGridView1.FindEditFormTemplateControl("GridLookupProgram") as ASPxGridLookup;
        gridLookupProgram.GridView.Width = 250;

        ASPxGridLookup gridLookupUser = ASPxGridView1.FindEditFormTemplateControl("GridLookupUser") as ASPxGridLookup;
        gridLookupUser.GridView.Width = 250;

        ASPxGridLookup gridLookupPline = ASPxGridView1.FindEditFormTemplateControl("GridLookupPline") as ASPxGridLookup;
        gridLookupPline.GridView.Width = 250;
    }
}
