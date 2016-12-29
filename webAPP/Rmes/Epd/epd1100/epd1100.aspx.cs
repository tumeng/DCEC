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

using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.Pub.Data;

/**
 * 功能概述：生产线代码表维护
 * 作者：游晓航
 * 创建时间：2016-06-12
 * 修改时间：
 */



public partial class Rmes_epd1100 : Rmes.Web.Base.BasePage
{
    private dataConn dc = new dataConn();
    private PubCs pc = new PubCs();
    public string theCompanyCode;
    private string theUserId;
    public string theProgramCode;
    public DateTime OracleEntityTime, OracleSQLTime;

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theUserId = theUserManager.getUserId();
        theProgramCode = "epd1100";
        //this.TranslateASPxControl(ASPxGridView1);


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
        string sql = "SELECT A.RMES_ID,A.PLINE_CODE,A.PLINE_NAME,A.PLINE_TYPE_CODE,A.THIRD_FLAG,A.STOCK_FLAG,A.SAP_CODE,B.PLINE_TYPE_NAME"
            +" FROM CODE_PRODUCT_LINE A"
            + " LEFT JOIN CODE_PLINE_TYPE B ON A.COMPANY_CODE = B.COMPANY_CODE AND A.PLINE_TYPE_CODE = B.PLINE_TYPE_CODE"
            + " WHERE A.COMPANY_CODE = '" + theCompanyCode + "' and a.pline_code in (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') ORDER BY PLINE_TYPE_CODE,PLINE_CODE";
        DataTable dt = dc.GetTable(sql);

        //DataTable autotype = new DataTable();
        //autotype.Columns.Add("value");
        //autotype.Columns.Add("text");
        //autotype.Rows.Add("A","自动线");
        //autotype.Rows.Add("H", "人工线");
        //GridViewDataComboBoxColumn comboCol = ASPxGridView1.Columns["AUTO_TYPE"] as GridViewDataComboBoxColumn;
        //comboCol.PropertiesComboBox.DataSource = autotype;
        //comboCol.PropertiesComboBox.ValueField = "value";
        //comboCol.PropertiesComboBox.TextField = "text";


        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
    }

    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {


        //判断当前记录是否可以删除
        string strDelCode = e.Values["PLINE_CODE"].ToString();
        string strTableName = "CODE_PRODUCT_LINE";

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
            //确认删除
            string Sql = "delete from CODE_PRODUCT_LINE WHERE  COMPANY_CODE = '" + theCompanyCode + "' and PLINE_CODE = '" + strDelCode + "'";
            dc.ExeSql(Sql);
        }


        setCondition();
        e.Cancel = true;
    }


    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxTextBox uCode = ASPxGridView1.FindEditFormTemplateControl("txtPlineCode") as ASPxTextBox;
        ASPxTextBox uName = ASPxGridView1.FindEditFormTemplateControl("txtPlineName") as ASPxTextBox;

        ASPxComboBox uTcode = ASPxGridView1.FindEditFormTemplateControl("dropPlineType") as ASPxComboBox;
        ASPxTextBox uScode = ASPxGridView1.FindEditFormTemplateControl("txtSapCode") as ASPxTextBox;
        ASPxComboBox uSflag = ASPxGridView1.FindEditFormTemplateControl("ASPxComboBox2") as ASPxComboBox;
        ASPxComboBox uTflag = ASPxGridView1.FindEditFormTemplateControl("ASPxComboBox1") as ASPxComboBox;


        string Sql = "INSERT INTO CODE_PRODUCT_LINE (RMES_ID,COMPANY_CODE,PLINE_CODE,PLINE_NAME,PLINE_TYPE_CODE,THIRD_FLAG,STOCK_FLAG,SAP_CODE) "
             + "VALUES(SEQ_RMES_ID.NextVal, '" + theCompanyCode + "','" + uCode.Text.Trim() + "','" + uName.Text.Trim() + "','" + uTcode.Value.ToString() + "','" + uTflag.Value.ToString() + "','" + uSflag.Value.ToString() + "','" + uScode.Value.ToString() + "')";
        dc.ExeSql(Sql);

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        setCondition();

    }


    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {

        ASPxTextBox uCode = ASPxGridView1.FindEditFormTemplateControl("txtPlineCode") as ASPxTextBox;
        ASPxTextBox uName = ASPxGridView1.FindEditFormTemplateControl("txtPlineName") as ASPxTextBox;
        ASPxComboBox uTcode = ASPxGridView1.FindEditFormTemplateControl("dropPlineType") as ASPxComboBox;
        ASPxTextBox uScode = ASPxGridView1.FindEditFormTemplateControl("txtSapCode") as ASPxTextBox;
        ASPxComboBox uSflag = ASPxGridView1.FindEditFormTemplateControl("ASPxComboBox2") as ASPxComboBox;
        ASPxComboBox uTflag = ASPxGridView1.FindEditFormTemplateControl("ASPxComboBox1") as ASPxComboBox;

        string Sql = "UPDATE CODE_PRODUCT_LINE SET PLINE_NAME='" + uName.Text.Trim() + "', PLINE_TYPE_CODE='" + uTcode.Value.ToString() + "',THIRD_FLAG='" + uTflag.Value.ToString() + "',STOCK_FLAG='" + uSflag.Value.ToString() + "',SAP_CODE='" + uScode.Text.Trim() + "'"
             + " WHERE  COMPANY_CODE = '" + theCompanyCode + "' and PLINE_CODE = '" + uCode.Text.Trim() + "'";
        dc.ExeSql(Sql);

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        setCondition();

    }
    protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
    {
        string Sql = "select pline_type_code,pline_type_code||' '||pline_type_name as showtext from code_pline_type";
        DataTable dt = dc.GetTable(Sql);

        ASPxComboBox uTcode = ASPxGridView1.FindEditFormTemplateControl("dropPlineType") as ASPxComboBox;

        uTcode.DataSource = dt;
        uTcode.TextField = dt.Columns[1].ToString();
        uTcode.ValueField = dt.Columns[0].ToString();

        if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
        {
            ///主键不可以修改
            (ASPxGridView1.FindEditFormTemplateControl("txtPlineCode") as ASPxTextBox).Enabled = false;
        }
        else
        {
            //uTcode.SelectedIndex = 0;
        }
    }

    protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        //判断超长
        if (e.NewValues["PLINE_CODE"].ToString().Length > 30)
        {
            e.RowError = "生产线代码字节长度不能超过30！";
        }
        if (e.NewValues["PLINE_NAME"].ToString().Length > 30)
        {
            e.RowError = "生产线名称字节长度不能超过30！";
        }

        if (e.NewValues["PLINE_TYPE_CODE"].ToString().Length > 30)
        {
            e.RowError = "生产线类型字节长度不能超过30！";
        }

        //if (e.NewValues["PLINE_PLAN_CODE"].ToString().Length > 10)
        //{
        //    e.RowError = "生产线计划代码字节长度不能超过10！";
        //}

        //判断为空
        if (e.NewValues["PLINE_CODE"].ToString() == "" || e.NewValues["PLINE_CODE"].ToString() == null)
        {
            e.RowError = "生产线代码不能为空！";
        }
        if (e.NewValues["PLINE_NAME"].ToString() == "" || e.NewValues["PLINE_NAME"].ToString() == null)
        {
            e.RowError = "生产线名称不能为空！";
        }

        //判断是否重复
        if (ASPxGridView1.IsNewRowEditing)
        {
            string chSql = "SELECT PLINE_CODE, PLINE_NAME  FROM CODE_PRODUCT_LINE"
                + " WHERE COMPANY_CODE = '" + theCompanyCode + "' AND PLINE_CODE='" + e.NewValues["PLINE_CODE"].ToString() + "'";
            DataTable dt = dc.GetTable(chSql);
            if (dt.Rows.Count > 0)
            {
                e.RowError = "已存在相同的生产线代码！";
            }
        }
    }

}
