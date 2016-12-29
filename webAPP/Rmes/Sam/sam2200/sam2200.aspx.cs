/**
 * 功能概述：角色权限定义
 * 作者：游晓航
 * 创建时间：2016-06-08
 * 修改时间：2016-07-27
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
using Rmes.Web.Base;
using Rmes.DA.Base;
using Rmes.DA.Procedures;

public partial class Rmes_Sam_sam2200_sam2200 : Rmes.Web.Base.BasePage
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

        //初始化新增模版里角色下拉列表
        initRole();
        //初始化新增模版里生产线下拉列表
        initPline();

        setCondition();
    }

    private void initRole()
    {
        //初始化角色下拉列表
        string sql = "SELECT ROLE_CODE,ROLE_NAME FROM CODE_ROLE WHERE COMPANY_CODE = '" + theCompanyCode + "'";
        SqlRole.SelectCommand = sql;
        SqlRole.DataBind();
    }

    private void initProgram()
    {
        //初始化新增模版里程序下拉列表
        string sql = "SELECT PROGRAM_CODE,PROGRAM_NAME FROM CODE_PROGRAM WHERE COMPANY_CODE = '" + theCompanyCode + "'";
        SqlProgram.SelectCommand = sql;
        SqlProgram.DataBind();
    }
    private void initPline()
    {
        //初始化新增模版里程序下拉列表
        string sql = " SELECT PLINE_CODE,PLINE_NAME FROM CODE_PRODUCT_LINE WHERE COMPANY_CODE = '" + theCompanyCode + "'";
        SqlPline.SelectCommand = sql;
        SqlPline.DataBind();
    }
    private void setCondition()
    {
        //初始化GRIDVIEW
        string sql = "SELECT COMPANY_CODE,ROLE_CODE,PROGRAM_CODE,ROLE_NAME,PROGRAM_NAME,PLINE_CODE,PLINE_NAME FROM VW_REL_ROLE_PROGRAM WHERE COMPANY_CODE = '" + theCompanyCode + "' ORDER BY ROLE_CODE,PROGRAM_CODE";
        DataTable dt = dc.GetTable(sql);

        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
    }
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        //删除
        //调用存储过程进行处理

        MW_MODIFY_ROLE_PROGRAM sp = new MW_MODIFY_ROLE_PROGRAM()
        {
            THEFUNCTION1 = "DELETE",
            THECOMPANYCODE1 = theCompanyCode,
            THEPROGRAMCODE1 = e.Values["PROGRAM_CODE"].ToString(),
            THEROLECODE1 = e.Values["ROLE_CODE"].ToString(),
            THEPLINECODE1 = e.Values["PLINE_CODE"].ToString()
        };

        Procedure.run(sp);


        setCondition();
        e.Cancel = true;
    }
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        //新增
        int indexProgram, indexRole,indexPline;
        ASPxGridLookup gridLookupProgram = ASPxGridView1.FindEditFormTemplateControl("GridLookupProgram") as ASPxGridLookup;
        ASPxGridLookup gridLookupRole = ASPxGridView1.FindEditFormTemplateControl("GridLookupRole") as ASPxGridLookup;
        ASPxGridLookup gridLookupPline = ASPxGridView1.FindEditFormTemplateControl("GridLookupPline") as ASPxGridLookup;

        List<object> Programs = gridLookupProgram.GridView.GetSelectedFieldValues("PROGRAM_CODE");
        List<object> Roles = gridLookupRole.GridView.GetSelectedFieldValues("ROLE_CODE");
        List<object> Plines = gridLookupPline.GridView.GetSelectedFieldValues("PLINE_CODE");

        for (indexProgram = 0; indexProgram < Programs.Count; indexProgram++)
        {
            for (indexRole = 0; indexRole < Roles.Count; indexRole++)
            {
                for (indexPline = 0; indexPline < Plines.Count; indexPline++)
                {

                    MW_MODIFY_ROLE_PROGRAM sp = new MW_MODIFY_ROLE_PROGRAM()
                    {
                        THEFUNCTION1 = "ADD",
                        THECOMPANYCODE1 = theCompanyCode,
                        THEPROGRAMCODE1 = Programs[indexProgram].ToString(),
                        THEROLECODE1 = Roles[indexRole].ToString(),
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

        ASPxGridLookup gridLookupRole = ASPxGridView1.FindEditFormTemplateControl("GridLookupRole") as ASPxGridLookup;
        gridLookupRole.GridView.Width = 250;

        ASPxGridLookup gridLookupPline = ASPxGridView1.FindEditFormTemplateControl("GridLookupPline") as ASPxGridLookup;
        gridLookupPline.GridView.Width = 250;
    }
}
