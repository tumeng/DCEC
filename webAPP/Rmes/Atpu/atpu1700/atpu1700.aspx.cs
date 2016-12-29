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
using DevExpress.Web.ASPxGridLookup;

using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxClasses;

using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.Pub.Data;

/**
 * 功能概述：东区喷漆颜色设定
 * 作者：游晓航
 * 创建时间：2016-06-24
 * 修改时间：
 */



public partial class Rmes_atpu1700 : Rmes.Web.Base.BasePage
{
    private dataConn dc = new dataConn();
    private PubCs pc = new PubCs();
    public string theCompanyCode, theUserId, theUserCode;
    public string theProgramCode;

    public DateTime OracleEntityTime, OracleSQLTime;

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theUserId = theUserManager.getUserId();
        theUserCode = theUserManager.getUserCode();
        theProgramCode = "atpu1700";

        //初始化新增模版里生产线下拉列表
        //initCode();

        if (!IsPostBack)
        {
            //OracleEntityTime = DB.GetServerTime();

            OracleSQLTime = DB.GetInstance().ExecuteScalar<DateTime>("Select sysdate from dual");
        }
        setCondition();
    }

    private void setCondition()
    {
        //初始化GRIDVIEW
        string sql = "SELECT * FROM ATPUEPAINTPROCOLOR WHERE SITE IN (SELECT PLINE_CODE FROM VW_USER_ROLE_PROGRAM WHERE USER_ID='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') order by INPUT_TIME desc nulls last";
        DataTable dt = dc.GetTable(sql);

        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
    }

    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        //判断当前记录是否可以删除
        string strDelCode = e.Values["SO"].ToString();
        string strTableName = "ATPUEPAINTPROCOLOR";

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
            //插入到日志表
            try
            {
                string Sql1 = " SELECT * FROM ATPUEPAINTPROCOLOR WHERE SO='" + strDelCode + "'";
                dc.setTheSql(Sql1);
                string SITE = dc.GetTable().Rows[0]["SITE"].ToString();
                string PROC = dc.GetTable().Rows[0]["PROC"].ToString();
                string COLOR = dc.GetTable().Rows[0]["COLOR"].ToString();

                string Sql2 = "INSERT INTO ATPUEPAINTPROCOLOR_LOG(SO,SITE,PROC,COLOR,USER_CODE,FLAG,RQSJ)"
                         + "VALUES('" + strDelCode + "','" + SITE + "','" + PROC + "','" + COLOR + "','" + theUserCode + "','DEL',SYSDATE)";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }
            //确认删除
            string Sql = "delete from ATPUEPAINTPROCOLOR WHERE   SO = '" + strDelCode + "'";
            dc.ExeSql(Sql);
        }


        setCondition();
        e.Cancel = true;
    }


    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxTextBox uSO = ASPxGridView1.FindEditFormTemplateControl("txtSO") as ASPxTextBox;
        ASPxTextBox uCOLOR = ASPxGridView1.FindEditFormTemplateControl("txtColor") as ASPxTextBox;
        ASPxTextBox uPRO = ASPxGridView1.FindEditFormTemplateControl("textPro") as ASPxTextBox;
        ASPxComboBox GridLookupCode = ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;

        string code = GridLookupCode.Value.ToString();
        
        string Sql = "INSERT INTO ATPUEPAINTPROCOLOR (SO,SITE,PROC,COLOR,input_person,input_time) "
             + "VALUES( '" + uSO.Text.Trim().ToUpper() + "', '" + code + "','" + uPRO.Text.Trim() + "','" + uCOLOR.Text.Trim() + "','"+theUserId+"',sysdate)";
        dc.ExeSql(Sql);
        //插入到日志表
        try
        {
            string Sql2 = "INSERT INTO ATPUEPAINTPROCOLOR_LOG(SO,SITE,PROC,COLOR,USER_CODE,FLAG,RQSJ)"
                         + "VALUES('" + uSO.Text.Trim().ToUpper() + "', '" + code + "','" + uPRO.Text.Trim() + "','" + uCOLOR.Text.Trim() + "','" + theUserCode + "','ADD',SYSDATE)";
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


    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {

        ASPxTextBox uSO = ASPxGridView1.FindEditFormTemplateControl("txtSO") as ASPxTextBox;
        ASPxTextBox uCOLOR = ASPxGridView1.FindEditFormTemplateControl("txtColor") as ASPxTextBox;
        ASPxTextBox uPRO = ASPxGridView1.FindEditFormTemplateControl("textPro") as ASPxTextBox;
        ASPxComboBox GridLookupCode = ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;


        string code = GridLookupCode.Value.ToString();

        string Sql = "UPDATE ATPUEPAINTPROCOLOR SET SITE='" + code + "',PROC='" + uPRO.Text.Trim() + "',COLOR='" + uCOLOR.Text.Trim() + "',input_person='"+theUserId+"',input_time=sysdate "
             + " WHERE   SO = '" + uSO.Text.Trim() + "'";
        dc.ExeSql(Sql);
        //插入到日志表
        try
        {
            string Sql2 = " INSERT INTO ATPUEPAINTPROCOLOR_LOG(SO,SITE,PROC,COLOR,USER_CODE,FLAG,RQSJ)"
                           + " VALUES( '"
                           + e.OldValues["SO"].ToString() + "','"
                           + e.OldValues["SITE"].ToString() + "','"
                           + e.OldValues["PROC"].ToString() + "','"
                           + e.OldValues["COLOR"].ToString() + "','"
                           + theUserCode + "','BEFOREEDIT',SYSDATE) ";
            dc.ExeSql(Sql2);
            string Sql3 = " INSERT INTO ATPUEPAINTPROCOLOR_LOG(SO,SITE,PROC,COLOR,USER_CODE,FLAG,RQSJ)"
                           + " VALUES( '"
                           + e.NewValues["SO"].ToString() + "','"
                           + e.NewValues["SITE"].ToString() + "','"
                           + e.NewValues["PROC"].ToString() + "','"
                           + e.NewValues["COLOR"].ToString() + "','"
                           + theUserCode + "','AFTEREDIT',SYSDATE) ";
            dc.ExeSql(Sql3);
        }
        catch
        {
            return;
        }

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        setCondition();

    }
    protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
    {
        string Sql = " select distinct a.pline_code,a.pline_code||' '||b.pline_name as showtext from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "' ";
        DataTable dt = dc.GetTable(Sql);

        ASPxComboBox uPCode = ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;

        uPCode.DataSource = dt;
        uPCode.TextField = dt.Columns[1].ToString();
        uPCode.ValueField = dt.Columns[0].ToString();
        uPCode.SelectedIndex = uPCode.Items.Count >= 0 ? 0 : -1;
        if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
        {
            ///主键不可以修改
            (ASPxGridView1.FindEditFormTemplateControl("txtSO") as ASPxTextBox).Enabled = false;
        }
        else
        {
            //uTcode.SelectedIndex = 0;
        }
    }

    protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        //判断是否重复
        if (ASPxGridView1.IsNewRowEditing)
        {
            string chSql = "SELECT SO  FROM ATPUEPAINTPROCOLOR"
                + " WHERE  SO='" + e.NewValues["SO"].ToString() + "'";
            DataTable dt = dc.GetTable(chSql);
            if (dt.Rows.Count > 0)
            {
                e.RowError = "已存在相同的SO！";
            }
        }
        //判断是否存在SO
         if (ASPxGridView1.IsNewRowEditing)
        {
            string chSql = "SELECT * FROM COPY_PT_MSTR WHERE UPPER(PT_PART)='" + e.NewValues["SO"].ToString().ToUpper() + "'";
            DataTable dt = dc.GetTable(chSql);
            if (dt.Rows.Count==0)
            {
                e.RowError = "该SO不存在！";
            }
        }
        
        if (e.NewValues["SO"].ToString().Length > 30)
        {
            e.RowError = "SO号字节长度不能超过30！";
        }
    }

}
