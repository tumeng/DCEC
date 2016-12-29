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
 * 功能概述：排放参数维护
 * 作者：游晓航
 * 创建时间：2016-06-24
 * 修改时间：
 */



public partial class Rmes_atpu1600 : Rmes.Web.Base.BasePage
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
        theProgramCode = "atpu1600";


        //初始化新增模版里生产线下拉列表
         

        if (!IsPostBack)
        {
            //OracleEntityTime = DB.GetServerTime();

            OracleSQLTime = DB.GetInstance().ExecuteScalar<DateTime>("Select sysdate from dual");
        }
        setCondition();
    }

    //private void initCode()
    //{
    //    //初始化生产线下拉列表
    //    string sql = "select distinct a.pline_code,b.pline_name  from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'";
    //    SqlCode.SelectCommand = sql;
    //    SqlCode.DataBind();
    //}
    private void setCondition()
    {
        //初始化GRIDVIEW
        string sql = "SELECT * FROM DMSOPFB where DD in (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') order by INPUT_TIME desc nulls last ";
        DataTable dt = dc.GetTable(sql);

        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
    }

    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        //判断当前记录是否可以删除
        string strDelCode = e.Values["XH"].ToString();
        string strTableName = "DMSOPFB";

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
            //插入到日志表20161101
            try
            {
                string Sql2 = "INSERT INTO DMSOPFB_LOG (XH,DD,SO,PF,BZ,INPUT_TIME,INPUT_PERSON,user_code,flag,rqsj)"
                    + " SELECT XH,DD,SO,PF,BZ,INPUT_TIME,INPUT_PERSON,'"
                    + theUserCode + "' , 'DEL', SYSDATE FROM DMSOPFB WHERE  XH =  '" + strDelCode + "' ";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }
            //确认删除
            string Sql = "delete from DMSOPFB WHERE   XH = '" + strDelCode + "'";
            dc.ExeSql(Sql);
            
        }
        setCondition();
        e.Cancel = true;
    }


    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxTextBox uSO = ASPxGridView1.FindEditFormTemplateControl("txtSO") as ASPxTextBox;
        ASPxTextBox uPF = ASPxGridView1.FindEditFormTemplateControl("txtPF") as ASPxTextBox;
        ASPxTextBox uXH = ASPxGridView1.FindEditFormTemplateControl("txtXH") as ASPxTextBox;
        ASPxComboBox uPCode = ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;

        ASPxTextBox uBZ = ASPxGridView1.FindEditFormTemplateControl("txtBZ") as ASPxTextBox;

        string code = uPCode.Value.ToString();
        string sqlxh = "SELECT nvl(max(to_number(XH))+1,0) FROM DMSOPFB";

        string xh = dc.GetValue(sqlxh);
       
        // DataTable dt = dc.GetTable(xh);
        //int xh = Convert.ToInt32(dc.GetValue(sqlxh));


        string Sql = "INSERT INTO DMSOPFB (XH,DD,SO,PF,BZ,INPUT_PERSON,INPUT_TIME) "
             + "VALUES( '" + xh + "', '" + code + "','" + uSO.Text.Trim() + "','" + uPF.Text.Trim() + "','" + uBZ.Text.Trim() + "','"+theUserId+"',SYSDATE)";
        dc.ExeSql(Sql);
        //插入到日志表20161101
        try
        {
            string Sql2 = "INSERT INTO DMSOPFB_LOG (XH,DD,SO,PF,BZ,INPUT_TIME,INPUT_PERSON,user_code,flag,rqsj)"
                + " SELECT XH,DD,SO,PF,BZ,INPUT_TIME,INPUT_PERSON,'"
                + theUserCode + "' , 'ADD', SYSDATE FROM DMSOPFB WHERE  XH =  '" + xh + "' ";
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
        ASPxTextBox uPF = ASPxGridView1.FindEditFormTemplateControl("txtPF") as ASPxTextBox;
        ASPxTextBox uXH = ASPxGridView1.FindEditFormTemplateControl("txtXH") as ASPxTextBox;
        ASPxComboBox uPCode = ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;

        ASPxTextBox uBZ = ASPxGridView1.FindEditFormTemplateControl("txtBZ") as ASPxTextBox;

        string code = uPCode.Value.ToString();

        string Sql = "UPDATE DMSOPFB SET DD='" + code + "',SO='" + uSO.Text.Trim() + "',PF='" + uPF.Text.Trim() + "',BZ='" + uBZ.Text.Trim() + "',input_person='"+theUserId+"',input_time=sysdate "
             + " WHERE   XH = '" + uXH.Text.Trim() + "'";
        dc.ExeSql(Sql);
        //插入到日志表20161101
        try
        {
            string Sql2 = "INSERT INTO DMSOPFB_LOG (XH,DD,SO,PF,BZ,INPUT_TIME,INPUT_PERSON,user_code,flag,rqsj)"
                + " VALUES( '" + uXH.Text.Trim() + "','" + e.OldValues["DD"].ToString() + "','" + e.OldValues["SO"].ToString() + "',"
                + "'" + e.OldValues["PF"].ToString() + "','" + e.OldValues["BZ"].ToString() + "',SYSDATE,'"+theUserId+"','"
                + theUserCode + "' , 'BEFOREEDIT', SYSDATE )";
            dc.ExeSql(Sql2);

            string Sql3 = "INSERT INTO DMSOPFB_LOG (XH,DD,SO,PF,BZ,INPUT_TIME,INPUT_PERSON,user_code,flag,rqsj)"
                + " VALUES( '" + uXH.Text.Trim() + "','" + e.NewValues["DD"].ToString() + "','" + e.NewValues["SO"].ToString() + "',"
                + "'" + e.NewValues["PF"].ToString() + "','" + e.NewValues["BZ"].ToString() + "',SYSDATE,'" + theUserId + "','"
                + theUserCode + "' , 'AFTEREDIT', SYSDATE )";
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
        string PSql = "select  PLINE_CODE,PLINE_NAME from CODE_PRODUCT_LINE where pline_code in (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "')";
        DataTable Pdt = dc.GetTable(PSql);
        ASPxComboBox uPcode = ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;
        uPcode.DataSource = Pdt;
        uPcode.TextField = Pdt.Columns[1].ToString();
        uPcode.ValueField = Pdt.Columns[0].ToString();
        
        if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
        {
            ///主键不可以修改
            (ASPxGridView1.FindEditFormTemplateControl("txtXH") as ASPxTextBox).Enabled = false;
        }
        else
        {
            uPcode.SelectedIndex = uPcode.Items.Count >= 0 ? 0 : -1;
            //uTcode.SelectedIndex = 0;
        }
    }

    protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        //判断是否重复
        if (ASPxGridView1.IsNewRowEditing)
        {
            string chSql = "SELECT SO  FROM DMSOPFB"
                + " WHERE  SO='" + e.NewValues["SO"].ToString() + "'";
            DataTable dt = dc.GetTable(chSql);
            if (dt.Rows.Count > 0)
            {
                e.RowError = "已存在相同的SO！";
            }
        }
        if (e.NewValues["SO"].ToString().Length > 30)
        {
            e.RowError = "SO号字节长度不能超过30！";
        }

        if (e.NewValues["PF"].ToString().Length > 50)
        {
            e.RowError = "排放参数字节长度不能超过50！";
        }

        
        if (e.NewValues["SO"].ToString() == "" || e.NewValues["SO"].ToString() == null)
        {
            e.RowError = "SO号 不能为空！";
        }

    }

    

}
