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
using System.Windows.Forms;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxClasses;
using Rmes.Pub.Data;
using Rmes.DA.Base;
/**
 * 功能概述：JIT计算参数定义
 * 作者：游晓航
 * 创建时间：2016-08-07
 */

public partial class Rmes_part1200 : Rmes.Web.Base.BasePage
{
    private dataConn dc = new dataConn();
    private PubCs pc = new PubCs();
    public string theCompanyCode, theUserId, theUserCode;
    public string theProgramCode;
    public DateTime  OracleSQLTime;
    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theUserId = theUserManager.getUserId();
        theUserCode = theUserManager.getUserCode();

        theProgramCode = "part1200";
        OracleSQLTime = DB.GetInstance().ExecuteScalar<DateTime>("Select sysdate from dual");
        setCondition();
       

    }
    
    protected void btnCheck_Click(object sender, EventArgs e)
    {
        string sql = "select * from ms_jit_parameter where (parameter_value is null or parameter_value='') and "
                    + "GZDD IN (SELECT PLINE_CODE FROM VW_USER_ROLE_PROGRAM WHERE USER_ID='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "')  "
                    + " order by INPUT_TIME desc nulls last ";

        DataTable dt = dc.GetTable(sql);
        if (dt.Rows.Count > 0)
        {
            Response.Write("<script>alert('还有未维护的参数！')</script>");
        }
        else 
        {
            Response.Write("<script>alert('参数已全部维护！')</script>");
        }
            
    }
    private void setCondition()
    {

        string sql = "select *  from ms_jit_parameter where "
                    + "GZDD IN (SELECT PLINE_CODE FROM VW_USER_ROLE_PROGRAM WHERE USER_ID='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "')  "
                    + "order by INPUT_TIME desc nulls last,PARAMETER_CODE";
        
        DataTable dt = dc.GetTable(sql);
        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();

        
    }

    //修改
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {

        ASPxTextBox uPmCode = ASPxGridView1.FindEditFormTemplateControl("TextPmCode") as ASPxTextBox;
        ASPxTextBox uPmValue = ASPxGridView1.FindEditFormTemplateControl("TextPmValue") as ASPxTextBox;
        ASPxTextBox uPmDesc = ASPxGridView1.FindEditFormTemplateControl("TextPmDesc") as ASPxTextBox;
        ASPxTextBox uPmTemp02 = ASPxGridView1.FindEditFormTemplateControl("TextPmTemp02") as ASPxTextBox;
        ASPxTextBox uGzdd = ASPxGridView1.FindEditFormTemplateControl("TextGzdd") as ASPxTextBox;
        ASPxDateEdit dPmValue = ASPxGridView1.FindEditFormTemplateControl("DatePmValue") as ASPxDateEdit;
        string strPmCode = uPmCode.Text.Trim();
        string strPmValue = uPmValue.Text.Trim();
        string strPmDesc = uPmDesc.Text.Trim();
        string strPmTemp02 = uPmTemp02.Text.Trim();
        string strGzdd = uGzdd.Text.Trim();
        
        //插入到日志表
        try
        {
            string Sql2 = "INSERT INTO MS_JIT_PARAMETER_LOG (PARAMETER_CODE,PARAMETER_VALUE,PARAMETER_DESC,TEMP01,TEMP02,GZDD,user_code,flag,rqsj)"
                + " SELECT PARAMETER_CODE,PARAMETER_VALUE,PARAMETER_DESC,TEMP01,TEMP02,GZDD,'"
                + theUserCode + "' , 'BEFOREEDIT', SYSDATE FROM MS_JIT_PARAMETER WHERE PARAMETER_CODE = '" + strPmCode + "' and GZDD = '" + strGzdd + "'";
            dc.ExeSql(Sql2);
        }
        catch
        {
            return;
        }

        if (strPmCode == "$INNER_INIT_CALCULATE_TIME" || strPmCode == "$THIRD_INIT_CALCULATE_TIME" || strPmCode == "$THIRD_INIT_PLANDATE" || strPmCode == "$INNER_INIT_PLANDATE")
        {
            //TextPmDesc.Visible = false;
            string Sql = "UPDATE MS_JIT_PARAMETER SET PARAMETER_DESC='" + strPmDesc + "',parameter_value='" + dPmValue.Text + "',temp02='" + strPmTemp02 + "' "
            + " WHERE PARAMETER_CODE = '" + strPmCode + "' and GZDD = '" + strGzdd + "'";
            dc.ExeSql(Sql);
        }
        else 
        {
            string Sql = "UPDATE MS_JIT_PARAMETER SET PARAMETER_DESC='" + strPmDesc + "',parameter_value='" + strPmValue + "',temp02='" + strPmTemp02 + "',input_person='"+theUserId+"',input_time=sysdate "
            + " WHERE PARAMETER_CODE = '" + strPmCode + "' and GZDD = '" + strGzdd + "'";
            dc.ExeSql(Sql);
        }

        //插入到日志表
        try
        {
            string Sql2 = "INSERT INTO MS_JIT_PARAMETER_LOG (PARAMETER_CODE,PARAMETER_VALUE,PARAMETER_DESC,TEMP01,TEMP02,GZDD,user_code,flag,rqsj)"
                + " SELECT PARAMETER_CODE,PARAMETER_VALUE,PARAMETER_DESC,TEMP01,TEMP02,GZDD,'"
                + theUserCode + "' , 'AFTEREDIT', SYSDATE FROM MS_JIT_PARAMETER WHERE PARAMETER_CODE = '" + strPmCode + "' and GZDD = '" + strGzdd + "'";
            dc.ExeSql(Sql2);
        }
        catch
        {
            return;
        }
        

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        setCondition();
    }
    //创建EDITFORM前
    protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
    {
        if (ASPxGridView1.IsEditing)
        {

            //获取editform中某一个控件之前存的数据
            string strPmCode = ASPxGridView1.GetRowValues(ASPxGridView1.EditingRowVisibleIndex, new string[] { "PARAMETER_CODE" }).ToString();




            ///主键不可以修改
            (ASPxGridView1.FindEditFormTemplateControl("TextPmCode") as ASPxTextBox).Enabled = false;
            if (strPmCode == "$INNER_INIT_CALCULATE_TIME" || strPmCode == "$THIRD_INIT_CALCULATE_TIME" || strPmCode == "$THIRD_INIT_PLANDATE" || strPmCode == "$INNER_INIT_PLANDATE")
            {
                (ASPxGridView1.FindEditFormTemplateControl("TextPmValue") as ASPxTextBox).Visible = false;
                (ASPxGridView1.FindEditFormTemplateControl("DatePmValue") as ASPxDateEdit).Visible = true;

            }

            else
            {
                (ASPxGridView1.FindEditFormTemplateControl("TextPmValue") as ASPxTextBox).Visible = true;
                (ASPxGridView1.FindEditFormTemplateControl("DatePmValue") as ASPxDateEdit).Visible = false;
            }
        }
    
    }

    //修改数据校验
    protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        

    }

    protected void btnXlsExport_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter1.WriteXlsToResponse("JIT计算参数");
    }

}
