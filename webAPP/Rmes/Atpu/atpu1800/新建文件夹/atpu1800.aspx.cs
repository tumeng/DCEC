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
using Rmes.Pub.Data;
using Rmes.DA.Base;
/**
 * 功能概述：金未来SO凸出量维护
 * 作者：游晓航
 * 创建时间：2016-06-15
 */

public partial class Rmes_atpu1800 : Rmes.Web.Base.BasePage
{
    private dataConn dc = new dataConn();
    private PubCs pc = new PubCs();
    public string theCompanyCode, theUserId,theUserCode;
    public string theProgramCode;

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theUserId = theUserManager.getUserId();
        theUserCode = theUserManager.getUserCode();
        theProgramCode = "atpu1800";

        initCode();

        setCondition();

    }

    private void initCode()
    {
        //初始化用户下拉列表
        string sql = "select distinct a.pline_code,b.pline_name from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'";
        SqlCode.SelectCommand = sql;
        SqlCode.DataBind();
    }
    private void setCondition()
    {


        string sql = "select * from JWTCMSO where PLINE_CODE IN (SELECT PLINE_CODE FROM VW_USER_ROLE_PROGRAM WHERE USER_ID='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') order by input_time desc nulls last";
        DataTable dt = dc.GetTable(sql);
        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();


        string sql2 = "select * from JWYF order by input_time desc nulls last";

        DataTable dt2 = dc.GetTable(sql2);

        ASPxGridView2.DataSource = dt2;
        ASPxGridView2.DataBind();
    }

    //新增
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxComboBox uPCode = ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;
        ASPxTextBox txtTCL = ASPxGridView1.FindEditFormTemplateControl("txtTCL") as ASPxTextBox;
        ASPxTextBox txtTCL1 = ASPxGridView1.FindEditFormTemplateControl("txtTCL1") as ASPxTextBox;
        
        ASPxTextBox txtJX = ASPxGridView1.FindEditFormTemplateControl("txtJX") as ASPxTextBox;
        ASPxTextBox txtGNUM = ASPxGridView1.FindEditFormTemplateControl("txtGNUM") as ASPxTextBox;
        ASPxComboBox txtSO = ASPxGridView1.FindEditFormTemplateControl("txtSO") as ASPxComboBox;
        ASPxTextBox txtBZ = ASPxGridView1.FindEditFormTemplateControl("txtBZ") as ASPxTextBox;

        string strPCode = uPCode.Value.ToString();
        string strTCL = txtTCL.Text.Trim();
        string strTCL1 = txtTCL1.Text.Trim();
        string strJX = txtJX.Text.Trim();
        string strGNUM = txtGNUM.Text.Trim();
        string strBZ = txtBZ.Text.Trim();
        string strSO = txtSO.Value.ToString();

        string Sql = "INSERT INTO JWTCMSO (PLINE_CODE,SO,JX,TCL,TCL1,REMARK,GNUM,INPUT_PERSON,INPUT_TIME) "
                   + "VALUES('" + strPCode + "','" + strSO + "', '" + strJX + "','" + strTCL + "','" + strTCL1 + "','" + strBZ + "','" + strGNUM + "','"+theUserId+"',SYSDATE)";
        dc.ExeSql(Sql);

        //插入到日志表
        try
        {
            string Sql2 = "INSERT INTO JWTCMSO_LOG (PLINE_CODE,SO,JX,TCL,TCL1,REMARK,GNUM,user_code,flag,rqsj)"
                + " VALUES('" + strPCode + "','" + strSO + "', '" + strJX + "','" + strTCL + "','" + strTCL1 + "','" + strBZ + "','" + strGNUM + "','" + theUserCode + "' , 'ADD', SYSDATE) ";
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

    //删除
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        //判断当前记录是否可以删除
        string strCode = e.Values["SO"].ToString();
        string strTableName = "JWTCMSO";

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

            //插入到日志表
            try
            {
                string Sql2 = "INSERT INTO JWTCMSO_LOG (PLINE_CODE,SO,JX,TCL,TCL1,REMARK,GNUM,user_code,flag,rqsj)"
                    + " SELECT PLINE_CODE,SO,JX,TCL,TCL1,REMARK,GNUM,'" + theUserCode + "' , 'DEL', SYSDATE FROM JWTCMSO WHERE SO = '" + strCode + "'";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }

            //确认删除
            string Sql = "delete from JWTCMSO WHERE SO = '" + strCode + "'";
            dc.ExeSql(Sql);
        }


        setCondition();
        e.Cancel = true;
    }

    //修改
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {


        ASPxComboBox uPCode = ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;
        ASPxTextBox txtTCL = ASPxGridView1.FindEditFormTemplateControl("txtTCL") as ASPxTextBox;
        ASPxTextBox txtTCL1 = ASPxGridView1.FindEditFormTemplateControl("txtTCL1") as ASPxTextBox;

        ASPxTextBox txtJX = ASPxGridView1.FindEditFormTemplateControl("txtJX") as ASPxTextBox;
        ASPxTextBox txtGNUM = ASPxGridView1.FindEditFormTemplateControl("txtGNUM") as ASPxTextBox;
        ASPxComboBox txtSO = ASPxGridView1.FindEditFormTemplateControl("txtSO") as ASPxComboBox;
        ASPxTextBox txtBZ = ASPxGridView1.FindEditFormTemplateControl("txtBZ") as ASPxTextBox;

        string strPCode = uPCode.Value.ToString();
        string strTCL = txtTCL.Text.Trim();
        string strTCL1 = txtTCL1.Text.Trim();
        string strJX = txtJX.Text.Trim();
        string strGNUM = txtGNUM.Text.Trim();
        string strBZ = txtBZ.Text.Trim();
        string strSO = txtSO.Value.ToString();

        //插入到日志表
        try
        {
            string Sql2 = "INSERT INTO JWTCMSO_LOG (PLINE_CODE,SO,JX,TCL,TCL1,REMARK,GNUM,user_code,flag,rqsj)"
                + " SELECT PLINE_CODE,SO,JX,TCL,TCL1,REMARK,GNUM,'" + theUserCode + "' , 'BEFOREEDIT', SYSDATE FROM JWTCMSO WHERE SO = '" + strSO + "'";
            dc.ExeSql(Sql2);
        }
        catch
        {
            return;
        }


        string Sql = "UPDATE JWTCMSO SET PLINE_CODE='" + strPCode + "',JX='" + strJX + "',TCL='" + strTCL + "',TCL1='" + strTCL1 + "',REMARK='" + strBZ + "',GNUM='" + strGNUM + "',INPUT_PERSON='" + theUserId + "',INPUT_TIME=SYSDATE"
             + " WHERE SO = '" + strSO + "'";
        dc.ExeSql(Sql);

        //插入到日志表
        try
        {
            string Sql2 = "INSERT INTO JWTCMSO_LOG (PLINE_CODE,SO,JX,TCL,TCL1,REMARK,GNUM,user_code,flag,rqsj)"
                + " SELECT PLINE_CODE,SO,JX,TCL,TCL1,REMARK,GNUM,'" + theUserCode + "' , 'AFTEREDIT', SYSDATE FROM JWTCMSO WHERE SO = '" + strSO + "'";
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

        string Sql = "select distinct a.pline_code,a.pline_code||' '||b.pline_name as showtext from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'  ";
        DataTable dt = dc.GetTable(Sql);


        ASPxComboBox uPCode = ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;
        uPCode.DataSource = dt;
        uPCode.TextField = dt.Columns[1].ToString();
        uPCode.ValueField = dt.Columns[0].ToString();

       
      //  string strPCode = uPCode.Value.ToString();

        string Sql1 = "select distinct PLAN_SO,PLAN_SO as showtext from DATA_PLAN ";
        DataTable dt1 = dc.GetTable(Sql1);

        ASPxComboBox txtSO = ASPxGridView1.FindEditFormTemplateControl("txtSO") as ASPxComboBox;

        txtSO.DataSource = dt1;
        txtSO.TextField = dt1.Columns[1].ToString();
        txtSO.ValueField = dt1.Columns[0].ToString();
        if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
        {
            ///主键不可以修改
            (ASPxGridView1.FindEditFormTemplateControl("txtSO") as ASPxComboBox).Enabled = false;
        }
        else
        {
            //uTcode.SelectedIndex = 0;
        }

    }

    //修改数据校验
    protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        //判断为空
        if (e.NewValues["SO"].ToString() == "" || e.NewValues["SO"].ToString() == null)
        {
            e.RowError = "SO号不能为空！";
        }
       

        string strSO = e.NewValues["SO"].ToString().Trim();
        string strJX = e.NewValues["JX"].ToString().Trim();

        //判断超长
        if (strSO.Length > 30)
        {
            e.RowError = "SO字节长度不能超过30！";
        }
        if (strJX.Length > 30)
        {
            e.RowError = "机型字节长度不能超过30！";
        }

        //判断是否重复
        if (ASPxGridView1.IsNewRowEditing)
        {
            string chSql = "SELECT SO FROM JWTCMSO WHERE SO='" + strSO + "'";
            DataTable dt = dc.GetTable(chSql);
            if (dt.Rows.Count > 0)
            {
                e.RowError = "已存在相同的SO号！";
            }

        }

    }
    protected void ASPxGridView2_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxComboBox uJX = ASPxGridView2.FindEditFormTemplateControl("txtJX") as ASPxComboBox;
       
        ASPxComboBox uPD = ASPxGridView2.FindEditFormTemplateControl("txtPD") as ASPxComboBox;


        string strJX = uJX.Value.ToString();
        
        string strPD = uPD.Value.ToString();


        string Sql = "INSERT INTO JWYF (JXREMARK,PANDUAN,INPUT_PERSON,INPUT_TIME) "
             + "VALUES('" + strJX + "', '" + strPD + "','"+theUserId+"',SYSDATE)";
        dc.ExeSql(Sql);

        //插入到日志表
        try
        {
            string Sql2 = "INSERT INTO JWYF_LOG (JXREMARK,PANDUAN,user_code,flag,rqsj)"
                + " VALUES('" + strJX + "', '" + strPD + "','" + theUserCode + "' , 'ADD', SYSDATE) ";
            dc.ExeSql(Sql2);
        }
        catch
        {
            return;
        }

        e.Cancel = true;
        ASPxGridView2.CancelEdit();
        setCondition();
    }

    //删除
    protected void ASPxGridView2_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        //判断当前记录是否可以删除
        string strJX = e.Values["JXREMARK"].ToString();
        string strPD = e.Values["PANDUAN"].ToString();
        string strTableName = "JWYF";
        dataConn theDataConn = new dataConn("select func_check_delete_data('" + strTableName + "','" + theCompanyCode + "','MES','MES','MES','" + strJX + "') from dual");

       

        theDataConn.OpenConn();
        string theRet = theDataConn.GetValue();
        if (theRet != "Y")
        {
            ASPxGridView2.JSProperties.Add("cpCallbackName", "Delete");
            ASPxGridView2.JSProperties.Add("cpCallbackRet", theRet);
            theDataConn.CloseConn();
        }
        else
        {
            //插入到日志表
            try
            {
                string Sql2 = "INSERT INTO JWYF_LOG (JXREMARK,PANDUAN,user_code,flag,rqsj)"
                    + " SELECT JXREMARK,PANDUAN,'" + theUserCode + "' , 'DEL', SYSDATE FROM JWYF WHERE PANDUAN = '" + strPD + "'";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }

            //确认删除
            string Sql = "delete from JWYF WHERE JXREMARK = '" + strJX + "'and PANDUAN = '" + strPD + "'";
            dc.ExeSql(Sql);
        }


        setCondition();
        e.Cancel = true;
    }

    //修改
    protected void ASPxGridView2_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {

        ASPxComboBox uJX = ASPxGridView2.FindEditFormTemplateControl("txtJX") as ASPxComboBox;

        ASPxComboBox uPD = ASPxGridView2.FindEditFormTemplateControl("txtPD") as ASPxComboBox;


        string strJX = uJX.Value.ToString();

        string strPD = uPD.Value.ToString();

        //插入到日志表
        try
        {
            string Sql2 = "INSERT INTO JWYF_LOG (JXREMARK,PANDUAN,user_code,flag,rqsj)"
                + " SELECT JXREMARK,PANDUAN,'" + theUserCode + "' , 'BEFOREEDIT', SYSDATE FROM JWYF WHERE PANDUAN = '" + strPD + "'";
            dc.ExeSql(Sql2);
        }
        catch
        {
            return;
        }
        string Sql = "UPDATE JWYF SET PANDUAN='" + strPD + "',INPUT_PERSON='"+theUserId+"',INPUT_TIME=SYSDATE"
             + " WHERE JXREMARK='" + strJX + "'";
        dc.ExeSql(Sql);

        //插入到日志表
        try
        {
            string Sql2 = "INSERT INTO JWYF_LOG (JXREMARK,PANDUAN,user_code,flag,rqsj)"
                + " SELECT JXREMARK,PANDUAN,'" + theUserCode + "' , 'AFTEREDIT', SYSDATE FROM JWYF WHERE PANDUAN = '" + strPD + "'";
            dc.ExeSql(Sql2);
        }
        catch
        {
            return;
        }

        e.Cancel = true;
        ASPxGridView2.CancelEdit();
        setCondition();
    }

    //创建EDITFORM前
    protected void ASPxGridView2_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
    {
        string Sql = "select JXREMARK,JXREMARK as showtext from JWYF";
        DataTable dt = dc.GetTable(Sql);

        ASPxComboBox uPCode = ASPxGridView2.FindEditFormTemplateControl("txtJX") as ASPxComboBox;

        uPCode.DataSource = dt;
        uPCode.TextField = dt.Columns[1].ToString();
        uPCode.ValueField = dt.Columns[0].ToString();

        if (!ASPxGridView2.IsNewRowEditing && ASPxGridView2.IsEditing)
        {
            ///主键不可以修改
            (ASPxGridView2.FindEditFormTemplateControl("txtJX") as ASPxComboBox).Enabled = false;
        }
        else
        {
            //uTcode.SelectedIndex = 0;
        }

    }

    //修改数据校验
    protected void ASPxGridView2_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
     
        string strJX = e.NewValues["JXREMARK"].ToString().Trim();

        string strPD = e.NewValues["PANDUAN"].ToString().Trim();
        //判断是否重复
        if (ASPxGridView2.IsNewRowEditing)
        {
            string chSql = "SELECT * FROM JWYF WHERE JXREMARK = '" + strJX + "' and PANDUAN = '" + strPD + "'";
            DataTable dt = dc.GetTable(chSql);
            if (dt.Rows.Count > 0)
            {
                e.RowError = "已存在相同的数据！";
            }

        }
    }
    

}
