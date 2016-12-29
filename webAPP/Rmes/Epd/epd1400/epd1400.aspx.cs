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
 * 功能概述：现场公告维护
 * 作者：游晓航
 * 创建时间：2016-09-27
 * 修改时间：
 */



public partial class Rmes_epd1400 : Rmes.Web.Base.BasePage
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
        theProgramCode = "epd1400";
        //this.TranslateASPxControl(ASPxGridView1);


        if (!IsPostBack)
        {
            //OracleEntityTime = DB.GetServerTime();
            
            OracleSQLTime = DB.GetInstance().ExecuteScalar<DateTime>("Select sysdate from dual");
        }
        initCode();
        setCondition();
    }

    private void initCode()
    {
        //初始化生产线下拉列表
        string sql = "select distinct a.pline_id,b.pline_name from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'";
        SqlCode.SelectCommand = sql;
        SqlCode.DataBind();
       
    }

    private void setCondition()
    {
        //初始化GRIDVIEW
        string sql = "select a.*,b.pline_name from data_broadcast a left join code_product_line b on a.pline_code=b.rmes_id where a.pline_code in "
            +" (select pline_id from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') ";
        DataTable dt = dc.GetTable(sql);
        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
    }

    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {


        //判断当前记录是否可以删除
        string strDelCode = e.Values["RMES_ID"].ToString();
        string strTableName = "DATA_BROADCAST";

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
            string Sql = "delete from DATA_BROADCAST WHERE RMES_ID = '" + strDelCode + "'";
            dc.ExeSql(Sql);
        }


        setCondition();
        e.Cancel = true;
    }


    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        
        ASPxComboBox uPcode = ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;
        ASPxTextBox uBrcast = ASPxGridView1.FindEditFormTemplateControl("txtBrcast") as ASPxTextBox;


        string Sql = "INSERT INTO DATA_BROADCAST (RMES_ID,COMPANY_CODE,PLINE_CODE,BROADCAST_CONTENT) "
             + "VALUES(SEQ_RMES_ID.NextVal, '" + theCompanyCode + "','" + uPcode.Value.ToString()+ "','" + uBrcast.Text.Trim() + "')";
        dc.ExeSql(Sql);

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        setCondition();

    }


    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {

        ASPxComboBox uPcode = ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;
        ASPxTextBox uBrcast = ASPxGridView1.FindEditFormTemplateControl("txtBrcast") as ASPxTextBox;

        string Sql = "UPDATE DATA_BROADCAST SET  BROADCAST_CONTENT='" + uBrcast.Text.Trim() + "'"
             + " WHERE  PLINE_CODE='" + uPcode.Value.ToString() + "' ";
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
            (ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox).Enabled = false;
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
        if (e.NewValues["BROADCAST_CONTENT"].ToString().Length >200)
        {
            e.RowError = "公告字节长度不能超过30！";
        }

       

       
        if (e.NewValues["PLINE_CODE"].ToString() == "" || e.NewValues["PLINE_CODE"].ToString() == null)
        {
            e.RowError = "生产线代码不能为空！";
        }
        if (e.NewValues["BROADCAST_CONTENT"].ToString() == "" || e.NewValues["BROADCAST_CONTENT"].ToString() == null)
        {
            e.RowError = "公告字节不能为空！";
        }

        //判断是否重复
        if (ASPxGridView1.IsNewRowEditing)
        {
            string chSql = "SELECT PLINE_CODE, BROADCAST_CONTENT  FROM DATA_BROADCAST"
                + " WHERE COMPANY_CODE = '" + theCompanyCode + "' AND PLINE_CODE='" + e.NewValues["PLINE_CODE"].ToString() + "'";
            DataTable dt = dc.GetTable(chSql);
            if (dt.Rows.Count > 0)
            {
                e.RowError = "该生产线已存在公告！";
            }
        }
    }

}
