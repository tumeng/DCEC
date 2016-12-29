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
 * 功能概述：工艺属性基础数据维护,对原工艺路线数据进行维护
 * 作者：游晓航
 * 创建时间：2016-07-06
 * 修改时间：
 */



public partial class Rmes_atpu1900 : Rmes.Web.Base.BasePage
{
    private dataConn dc = new dataConn();
    private PubCs pc = new PubCs();
    public string theCompanyCode, theUserId, theUserCode;
    private string theProgramCode;
    public DateTime OracleEntityTime, OracleSQLTime;

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theUserId = theUserManager.getUserId();
        theUserCode = theUserManager.getUserCode();
        theProgramCode = "atpu1900";
        //初始化新增模版里生产线下拉列表
        initCode();

        if (!IsPostBack)
        {
            //OracleEntityTime = DB.GetServerTime();

            OracleSQLTime = DB.GetInstance().ExecuteScalar<DateTime>("Select sysdate from dual");
        }
        setCondition();
    }

    private void initCode()
    {
        //初始化用户下拉列表
        string sql = "SELECT PLINE_CODE,PLINE_NAME FROM CODE_PRODUCT_LINE WHERE COMPANY_CODE = '" + theCompanyCode + "' ORDER BY PLINE_CODE";
        SqlCode.SelectCommand = sql;
        SqlCode.DataBind();
    }
    private void setCondition()
    {
        //初始化GRIDVIEW
        string sql = "SELECT * FROM DATA_ROUNTING_REMARK WHERE PLINE_CODE IN (SELECT PLINE_CODE FROM VW_USER_ROLE_PROGRAM WHERE USER_ID='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') order by INPUT_TIME desc nulls last";
        DataTable dt = dc.GetTable(sql);

        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
    }

    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        //判断当前记录是否可以删除
        string strDelCode = e.Values["RMES_ID"].ToString();
        string strTableName = "DATA_ROUNTING_REMARK";

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
                string Sql1 = " SELECT * FROM DATA_ROUNTING_REMARK WHERE rmes_id='" + strDelCode + "'";
                dc.setTheSql(Sql1);
                string rmes_id = dc.GetTable().Rows[0]["RMES_ID"].ToString();
                string company_code = dc.GetTable().Rows[0]["COMPANY_CODE"].ToString();
                string ROUNTING_REMARK = dc.GetTable().Rows[0]["ROUNTING_REMARK"].ToString();
                string PLINE_CODE = dc.GetTable().Rows[0]["PLINE_CODE"].ToString();
                string GS = dc.GetTable().Rows[0]["GS"].ToString();
                string XL = dc.GetTable().Rows[0]["XL"].ToString();
                string PL = dc.GetTable().Rows[0]["PL"].ToString();
                string RL = dc.GetTable().Rows[0]["RL"].ToString();
                string ISDK = dc.GetTable().Rows[0]["ISDK"].ToString();
                string ISEGR = dc.GetTable().Rows[0]["ISEGR"].ToString();

                string Sql2 = "INSERT INTO DATA_ROUNTING_REMARK_LOG (RMES_ID,COMPANY_CODE,rounting_remark,pline_code,gs,xl,pl,rl,isdk,isegr,user_code,flag,rqsj)"
                         + "VALUES('" + strDelCode + "','" + theCompanyCode + "','" + ROUNTING_REMARK + "','" + PLINE_CODE + "','" + GS + "','" + XL + "','" + PL + "','" + RL + "','" + ISDK + "','" + ISEGR + "','" + theUserCode + "','DEL',SYSDATE)";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }
            //确认删除
            string Sql = "delete from DATA_ROUNTING_REMARK WHERE RMES_ID = '" + strDelCode + "'";
            dc.ExeSql(Sql);
        }


        setCondition();
        e.Cancel = true;
    }


    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        //ASPxComboBox uROUNT = ASPxGridView1.FindEditFormTemplateControl("comboxROUNT") as ASPxComboBox;
        ASPxComboBox uPCode = ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;
        ASPxTextBox uGS = ASPxGridView1.FindEditFormTemplateControl("txtGS") as ASPxTextBox;
        ASPxTextBox uXL = ASPxGridView1.FindEditFormTemplateControl("txtXL") as ASPxTextBox;
        ASPxTextBox uPL = ASPxGridView1.FindEditFormTemplateControl("txtPL") as ASPxTextBox;
        ASPxTextBox uRL = ASPxGridView1.FindEditFormTemplateControl("txtRL") as ASPxTextBox;
        ASPxComboBox uISDK = ASPxGridView1.FindEditFormTemplateControl("comboxISDK") as ASPxComboBox;
        ASPxComboBox uISEGR = ASPxGridView1.FindEditFormTemplateControl("comboxISEGR") as ASPxComboBox;


        //string strROUNT = uROUNT.Value.ToString();
        string strPCode = uPCode.Value.ToString();
        string strGS = uGS.Text.Trim();
        string strXL = uXL.Text.Trim();
        string strPL = uPL.Text.Trim();
        string strRL = uRL.Text.Trim();
        string strISDK = uISDK.Value.ToString();
        string strISEGR = uISEGR.Value.ToString();
        string strROUNT2 = strGS + strXL + strPL + strRL + strISDK + strISEGR;

        //取RMES_ID的值
        string sql_rmes_id = "SELECT SEQ_RMES_ID.NEXTVAL FROM DUAL ";
        dc.setTheSql(sql_rmes_id);
        string rmes_id = dc.GetTable().Rows[0][0].ToString();

        string Sql = "INSERT INTO DATA_ROUNTING_REMARK VALUES('" + rmes_id + "','" + theCompanyCode + "','" + strROUNT2 + "', '" + strPCode + "',"
             + " '" + strGS + "', '" + strXL + "','" + strPL + "','" + strRL + "','" + strISDK + "','" + strISEGR + "',sysdate,'"+theUserId+"')";
        dc.ExeSql(Sql);
        
        //插入到日志表
        try
        {
            string Sql2 = "INSERT INTO DATA_ROUNTING_REMARK_LOG (RMES_ID,COMPANY_CODE,rounting_remark,pline_code,gs,xl,pl,rl,isdk,isegr,user_code,flag,rqsj)"
                         + "VALUES('" + rmes_id + "','" + theCompanyCode + "','" + strROUNT2 + "', '" + strPCode + "',"
                 + " '" + strGS + "', '" + strXL + "','" + strPL + "','" + strRL + "','" + strISDK + "','" + strISEGR + "','" + theUserCode + "','ADD',SYSDATE)";
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
        ASPxComboBox uROUNT = ASPxGridView1.FindEditFormTemplateControl("comboxROUNT") as ASPxComboBox;
        ASPxComboBox uPCode = ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;
        ASPxTextBox uGS = ASPxGridView1.FindEditFormTemplateControl("txtGS") as ASPxTextBox;
        ASPxTextBox uXL = ASPxGridView1.FindEditFormTemplateControl("txtXL") as ASPxTextBox;
        ASPxTextBox uPL = ASPxGridView1.FindEditFormTemplateControl("txtPL") as ASPxTextBox;
        ASPxTextBox uRL = ASPxGridView1.FindEditFormTemplateControl("txtRL") as ASPxTextBox;
        ASPxComboBox uISDK = ASPxGridView1.FindEditFormTemplateControl("comboxISDK") as ASPxComboBox;
        ASPxComboBox uISEGR = ASPxGridView1.FindEditFormTemplateControl("comboxISEGR") as ASPxComboBox;


        string strROUNT = uROUNT.Value.ToString();
        string strPCode = uPCode.Value.ToString();
        string strGS = uGS.Text.Trim();
        string strXL = uXL.Text.Trim();
        string strPL = uPL.Text.Trim();
        string strRL = uRL.Text.Trim();
        string strISDK = uISDK.Value.ToString();
        string strISEGR = uISEGR.Value.ToString();
        string strROUNT2 = strGS + strXL + strPL + strRL + strISDK + strISEGR;
        string Sql = "UPDATE DATA_ROUNTING_REMARK SET ROUNTING_REMARK='" + strROUNT2 + "',PLINE_CODE='" + strPCode + "',GS='" + strGS + "',XL='" + strXL + "',"
             + " PL='" + strPL + "',RL='" + strRL + "',ISDK='" + strISDK + "',ISEGR='" + strISEGR + "' WHERE  ROUNTING_REMARK='" + strROUNT + "'";
        dc.ExeSql(Sql);

        //取RMES_ID的值
        string sql_rmes_id = "SELECT SEQ_RMES_ID.NEXTVAL FROM DUAL ";
        dc.setTheSql(sql_rmes_id);
        string rmes_id = dc.GetTable().Rows[0][0].ToString();
        //插入到日志表
        try
        {
            string Sql2 = " INSERT INTO DATA_ROUNTING_REMARK_LOG (RMES_ID,COMPANY_CODE,rounting_remark,pline_code,gs,xl,pl,rl,isdk,isegr,user_code,flag,rqsj)"
                           + " VALUES( '" + rmes_id + "','" + theCompanyCode + "','" + strROUNT + "','"
                           + e.OldValues["PLINE_CODE"].ToString() + "','"
                           + e.OldValues["GS"].ToString() + "','"
                           + e.OldValues["XL"].ToString() + "','"
                           + e.OldValues["PL"].ToString() + "','"
                           + e.OldValues["RL"].ToString() + "','"
                           + e.OldValues["ISDK"].ToString() + "','"
                           + e.OldValues["ISEGR"].ToString() + "','"
                           + theUserCode + "','BEFOREEDIT',SYSDATE) ";
            dc.ExeSql(Sql2);
            string Sql3 = " INSERT INTO DATA_ROUNTING_REMARK_LOG (RMES_ID,COMPANY_CODE,rounting_remark,pline_code,gs,xl,pl,rl,isdk,isegr,user_code,flag,rqsj)"
                           + " VALUES( '" + rmes_id + "','" + theCompanyCode + "','" + strROUNT2 + "','"
                           + e.NewValues["PLINE_CODE"].ToString() + "','"
                           + e.NewValues["GS"].ToString() + "','"
                           + e.NewValues["XL"].ToString() + "','"
                           + e.NewValues["PL"].ToString() + "','"
                           + e.NewValues["RL"].ToString() + "','"
                           + e.NewValues["ISDK"].ToString() + "','"
                           + e.NewValues["ISEGR"].ToString() + "','"
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
        //创建EditForm前
        //string Sql = "select a.pline_code,a.pline_code||' '||a.pline_name as showtext from code_product_line a ";
        string Sql = "select DISTINCT A.PLINE_CODE,A.PLINE_CODE||' '||a.pline_name AS SHOWTEXT FROM CODE_PRODUCT_LINE A where pline_code in( select pline_code from vw_user_role_program where USER_ID='" + theUserId + "' and program_code='" + theProgramCode + "' )  ";
        DataTable dt = dc.GetTable(Sql);

        ASPxComboBox uPCode = ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;

        uPCode.DataSource = dt;
        uPCode.TextField = dt.Columns[1].ToString();
        uPCode.ValueField = dt.Columns[0].ToString();
        uPCode.SelectedIndex = uPCode.Items.Count >= 0 ? 0 : -1;
        string Sql2 = "select ROUNTING_REMARK,ROUNTING_REMARK as showtext from DATA_ROUNTING_REMARK ";
        DataTable dt2 = dc.GetTable(Sql2);

        ASPxComboBox uROUNT = ASPxGridView1.FindEditFormTemplateControl("comboxROUNT") as ASPxComboBox;

        uROUNT.DataSource = dt2;
        uROUNT.TextField = dt2.Columns[1].ToString();
        uROUNT.ValueField = dt2.Columns[0].ToString();


        if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
        {
            ///主键不可以修改
            (ASPxGridView1.FindEditFormTemplateControl("comboxROUNT") as ASPxComboBox).Enabled = false;
        }
        else
        {
            //uTcode.SelectedIndex = 0;
        }
    }

    protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        //判断是否重复

        string strPCode = e.NewValues["PLINE_CODE"].ToString().Trim();
        string strGS = e.NewValues["GS"].ToString().Trim();
        string strXL = e.NewValues["XL"].ToString().Trim();
        string strPL = e.NewValues["PL"].ToString().Trim();
        string strRL = e.NewValues["RL"].ToString().Trim();
        string strISDK = e.NewValues["ISDK"].ToString().Trim();
        string strISEGR = e.NewValues["ISEGR"].ToString().Trim();
        
        string strROUNT = strGS + strXL + strPL + strRL + strISDK + strISEGR;
        if (ASPxGridView1.IsEditing)
        {
            string chSql = "SELECT ROUNTING_REMARK  FROM DATA_ROUNTING_REMARK"
                + " WHERE  ROUNTING_REMARK='" + strROUNT + "'";
            DataTable dt = dc.GetTable(chSql);
            if (dt.Rows.Count > 0)
            {
                e.RowError = "已存在相同的工艺路线！";
            }
        }
        




       

    }



}
