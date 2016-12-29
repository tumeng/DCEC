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
using Rmes.Pub.Data;
using Rmes.Pub.Function;
using System.Security.Cryptography;
using System.Text;
using System.IO;

/**
 * 功能概述：现场用户定义
 * 作者：唐海林
 * 创建时间：2016-09-10
 */
public partial class Rmes_Sam_sam1200_sam1201 : Rmes.Web.Base.BasePage
{
    public dataConn theDc = new dataConn();
    public PubCs thePubPc = new PubCs();
    public string theCompanyCode, theUserId;
    private string theProgramCode;
    public string thePlCode = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theUserId = theUserManager.getUserId();
        theProgramCode = "sam1201";
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
        queryFunction();
        string a = thePubPc.AESEncrypt("admin");
    }
    private void initPLine()
    {
        //初始化生产线
        string sql = "SELECT A.PLINE_CODE,D.PLINE_NAME FROM vw_user_role_program A  "
                   + "LEFT JOIN CODE_PRODUCT_LINE D ON A.COMPANY_CODE=D.COMPANY_CODE AND A.PLINE_CODE=D.PLINE_CODE  "
                   + "WHERE A.COMPANY_CODE = '" + theCompanyCode + "' AND A.USER_ID='" + theUserId + "' and a.program_code='" + theProgramCode + "' "
                   + "ORDER BY A.PLINE_CODE";
        SqlPLine.SelectCommand = sql;
        SqlPLine.DataBind();

        CombPlCodeQ.DataSource = SqlPLine;
        CombPlCodeQ.TextField = "PLINE_NAME";
        CombPlCodeQ.ValueField = "PLINE_CODE";
        CombPlCodeQ.DataBind();
        CombPlCodeQ.SelectedIndex = 0;
    }

    //初始化GRIDVIEW
    private void queryFunction()
    {
        string sql = "SELECT A.COMPANY_CODE, A.USER_ID, A.USER_CODE, A.USER_NAME,B.TEAM_NAME,B.TEAM_CODE FROM CODE_USER A LEFT JOIN VW_REL_TEAM_USER B ON A.USER_ID=B.USER_ID WHERE A.COMPANY_CODE = '" + theCompanyCode + "' and user_type='B'";
        DataTable dt = theDc.GetTable(sql);

        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
    }

    // 修改
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string userId = (ASPxGridView1.FindEditFormTemplateControl("HidUserId") as HiddenField).Value;
        ASPxTextBox uCode = ASPxGridView1.FindEditFormTemplateControl("txtUCode") as ASPxTextBox;
        ASPxTextBox uName = ASPxGridView1.FindEditFormTemplateControl("txtUName") as ASPxTextBox;
        ASPxComboBox uTeam = ASPxGridView1.FindEditFormTemplateControl("CombTeam") as ASPxComboBox;

        string upSql = "UPDATE CODE_USER SET USER_CODE='" + uCode.Text.Trim().ToUpper() + "',USER_NAME='" + uName.Text.Trim() + "' "
                     + "WHERE  COMPANY_CODE = '" + theCompanyCode + "' AND USER_CODE='" + uCode.Text.Trim().ToUpper() + "'";
        theDc.ExeSql(upSql);
        string upSql2 = "UPDATE REL_TEAM_USER SET PLINE_CODE='" + thePlCode + "',TEAM_CODE='" + uTeam.Value.ToString() + "' and USER_ID='" + userId + "' "
                     + "WHERE  COMPANY_CODE = '" + theCompanyCode + "' AND USER_ID='" + userId + "'";
        theDc.ExeSql(upSql2);
        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        queryFunction();
    }
    // 新增
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxTextBox uCode = ASPxGridView1.FindEditFormTemplateControl("txtUCode") as ASPxTextBox;
        ASPxTextBox uName = ASPxGridView1.FindEditFormTemplateControl("txtUName") as ASPxTextBox;
        ASPxComboBox uTeam = ASPxGridView1.FindEditFormTemplateControl("CombTeam") as ASPxComboBox;
        dataConn theDataConn1 = new dataConn(" select TRIM(TO_CHAR(SEQ_USER_ID.NEXTVAL,'000000')) from dual");
        theDataConn1.OpenConn();
        string userId = theDataConn1.GetValue();
       
        string inSql = "INSERT INTO CODE_USER (COMPANY_CODE, USER_ID, USER_CODE, USER_NAME,user_type) "
                 + "VALUES('" + theCompanyCode + "','" + userId + "','" + uCode.Text.Trim().ToUpper() + "','" + uName.Text.Trim() + "' ,'B')";
        dataConn inDc = new dataConn();
        inDc.ExeSql(inSql);
        inDc.CloseConn();
        string inSql2 = "INSERT INTO REL_TEAM_USER (COMPANY_CODE,USER_ID, PLINE_CODE, TEAM_CODE) "
                + "VALUES('" + theCompanyCode + "','" + userId + "','" + thePlCode + "','" + uTeam.Value.ToString() + "')";
        theDc.ExeSql(inSql2);
        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        queryFunction();
    }
    // 进入编辑界面时的处理
    protected void ASPxGridView1_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        //ASPxComboBox dropPline = ASPxGridView1.FindEditFormTemplateControl("CombPlCodeQ") as ASPxComboBox;
        ASPxComboBox dropTeam = ASPxGridView1.FindEditFormTemplateControl("CombTeam") as ASPxComboBox;


        string Sql2 = "SELECT A.TEAM_CODE,A.TEAM_CODE||' '||A.TEAM_NAME as showtext FROM CODE_TEAM A LEFT JOIN CODE_PRODUCT_LINE B ON A.PLINE_CODE=B.PLINE_CODE WHERE A.COMPANY_CODE = '" + theCompanyCode + "' AND B.pline_code='" + thePlCode + "' ";
        DataTable dt2 = theDc.GetTable(Sql2);

        dropTeam.DataSource = dt2;
        dropTeam.TextField = dt2.Columns[1].ToString();
        dropTeam.ValueField = dt2.Columns[0].ToString();


    }
    // 数据校验
    protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        //判断超长
        if (e.NewValues["USER_CODE"].ToString().Length > 30)
        {
            e.RowError = "用户代码字节长度不能超过30！";
        }
        if (e.NewValues["USER_NAME"].ToString().Length > 30)
        {
            e.RowError = "姓名字节长度不能超过30！";
        }
        
        //判断为空
        if (e.NewValues["USER_CODE"].ToString() == "" || e.NewValues["USER_CODE"].ToString() == null)
        {
            e.RowError = "用户代码不能为空！";
        }
        if (e.NewValues["USER_NAME"].ToString() == "" || e.NewValues["USER_NAME"].ToString() == null)
        {
            e.RowError = "姓名不能为空！";
        }
        if (ASPxGridView1.IsNewRowEditing)
        {
            string chSql = "SELECT COMPANY_CODE, USER_CODE  FROM CODE_USER"
                               + " WHERE COMPANY_CODE = '" + theCompanyCode + "' AND USER_CODE='" + e.NewValues["USER_CODE"].ToString() + "' ";
            DataTable dt = theDc.GetTable(chSql);
            if (dt.Rows.Count > 0)
            {
                e.RowError = "员工编号重复！";
            }
            else
            {
                //判断同一个用户只能分配一个班组
                string sql = "select user_id from code_user where user_code='" + e.NewValues["USER_CODE"].ToString() + "'";
                DataTable dt3 = theDc.GetTable(sql);
                if (dt3.Rows.Count > 0)
                {
                    string userId = dt3.Rows[0][0].ToString();
                    string chSql2 = "SELECT COMPANY_CODE,PLINE_CODE,TEAM_CODE,USER_ID FROM REL_TEAM_USER WHERE COMPANY_CODE='" + theCompanyCode + "' AND PLINE_CODE='" + thePlCode + "' AND USER_ID='" + userId + "'";
                    DataTable dt2 = theDc.GetTable(chSql);
                    if (dt2.Rows.Count > 0)
                    {
                        e.RowError = e.NewValues["USER_ID"].ToString() + "已经被分配过班组！";
                    }
                }
            }
 
        }
        if (!ASPxGridView1.IsNewRowEditing && (ASPxGridView1.FindEditFormTemplateControl("chValidFlag") as ASPxCheckBox).Checked == true)
        {
            if (e.OldValues["USER_CODE"].ToString() != e.NewValues["USER_CODE"].ToString())
            {
                //string chSql = "SELECT COMPANY_CODE, USER_CODE  FROM CODE_USER"
                //            + " WHERE COMPANY_CODE = '" + theCompanyCode + "' AND USER_CODE='" + e.NewValues["USER_CODE"].ToString() + "' AND VALID_FLAG='Y'";
                //DataTable dt = theDc.GetTable(chSql);
                //if (dt.Rows.Count == 1)
                //{
                //    e.RowError = "用户代码重复！";
                //}
                checkUnique(e);
            }

        }
    }
    private void checkUnique(DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        string chSql = "SELECT COMPANY_CODE, USER_CODE  FROM CODE_USER"
                                + " WHERE COMPANY_CODE = '" + theCompanyCode + "' AND USER_CODE='" + e.NewValues["USER_CODE"].ToString() + "' ";
        DataTable dt = theDc.GetTable(chSql);
        if (dt.Rows.Count > 0)
        {
            e.RowError = "员工编号重复！";
        }
        //判断同一个用户只能分配一个班组
        string userId = (ASPxGridView1.FindEditFormTemplateControl("HidUserId") as HiddenField).Value;
        string chSql2 = "SELECT COMPANY_CODE,PLINE_CODE,TEAM_CODE,USER_ID FROM REL_TEAM_USER WHERE COMPANY_CODE='" + theCompanyCode + "' AND PLINE_CODE='" + thePlCode + "' AND USER_ID='" + userId + "'";
        DataTable dt2 = theDc.GetTable(chSql);
        if (dt2.Rows.Count > 0)
        {
            e.RowError = e.NewValues["USER_ID"].ToString() + "已经被分配过班组！";
        }
    }
    // 删除
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        //判断当前记录是否可以删除
        {
            string delStr = e.Values["USER_ID"].ToString();
            //删除操作
            //string dSql = "DELETE FROM CODE_USER WHERE COMPANY_CODE='" + theCompanyCode + "' AND USER_CODE='" + e.Values["USER_CODE"].ToString() + "'";
            //theDc.ExeSql(dSql);
            string dSql = "DELETE FROM CODE_USER WHERE COMPANY_CODE='" + theCompanyCode + "' AND USER_ID='" + e.Values["USER_ID"].ToString() + "'";
            theDc.ExeSql(dSql);
            string dSql2 = "DELETE FROM REL_TEAM_USER WHERE COMPANY_CODE='" + theCompanyCode + "' AND PLINE_CODE='" + thePlCode + "' AND USER_ID='" + e.Values["USER_ID"].ToString() + "'";
            theDc.ExeSql(dSql2);
        }
        e.Cancel = true;
        queryFunction();
    }
    protected void CombPlCodeQ_SelectedIndexChanged(object sender, EventArgs e)
    {
        queryFunction();
    }
    protected void btnXlsExport_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter1.WriteXlsToResponse("现场人员信息导出");
    }
}
