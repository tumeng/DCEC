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

/**
 * 功能概述：班组定义
 * 作者：白玥
 * 创建时间：2011-08-04
 */

public partial class Rmes_epd2000 : System.Web.UI.Page
{
    public string theCompanyCode;
    public dataConn theDc = new dataConn();
    private string theUserId;
    public string theProgramCode;
    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theUserId = theUserManager.getUserId();
        theProgramCode = "epd2000";
        queryFunction();

    }
    //初始化GRIDVIEW
    private void queryFunction()
    {
        string sql = "SELECT A.COMPANY_CODE, A.TEAM_CODE, A.TEAM_NAME, A.PLINE_CODE, A.LEADER_CODE, B.PLINE_NAME,C.USER_NAME  FROM CODE_TEAM A "
            + " LEFT JOIN code_product_line B ON A.PLINE_CODE=B.PLINE_CODE LEFT JOIN CODE_USER C ON A.LEADER_CODE=C.USER_CODE "
            + " WHERE A.COMPANY_CODE = '" + theCompanyCode + "' and a.pline_code in (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') ORDER BY PLINE_CODE,TEAM_CODE";
        DataTable dt = theDc.GetTable(sql);

        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
    }

    // 修改
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string code = e.OldValues["TEAM_CODE"].ToString().Trim();
        string name = e.NewValues["TEAM_NAME"].ToString().Trim();
        string pline = e.NewValues["PLINE_CODE"].ToString();
        string leader = e.NewValues["LEADER_CODE"].ToString();


        string upSql = "UPDATE CODE_TEAM SET TEAM_NAME='" + name + "',PLINE_CODE='" + pline + "',LEADER_CODE='" + leader + "' WHERE  COMPANY_CODE = '" + theCompanyCode + "' AND TEAM_CODE='" + code + "'";
        theDc.ExeSql(upSql);

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        queryFunction();
    }
    // 新增
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        string code = e.NewValues["TEAM_CODE"].ToString().Trim();
        string name = e.NewValues["TEAM_NAME"].ToString().Trim();
        string pline = e.NewValues["PLINE_CODE"].ToString();
        string leader = e.NewValues["LEADER_CODE"].ToString();

        string inSql = "INSERT INTO CODE_TEAM (RMES_ID, COMPANY_CODE, TEAM_CODE, TEAM_NAME, PLINE_CODE,LEADER_CODE) "
                 + "VALUES(SEQ_RMES_ID.NEXTVAL,'" + theCompanyCode + "','" + code + "','" + name + "','" + pline + "','" + leader + "')";
        dataConn inDc = new dataConn();
        inDc.ExeSql(inSql);
        inDc.CloseConn();

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        queryFunction();
    }
    // 进入修改界面时的处理
    protected void ASPxGridView1_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        ASPxComboBox dropPline = ASPxGridView1.FindEditFormTemplateControl("dropPlineCode") as ASPxComboBox;
        ASPxComboBox dropLeader = ASPxGridView1.FindEditFormTemplateControl("dropTeamLeader") as ASPxComboBox;
        string Sql1 = "select pline_code,pline_code||' '||pline_name as showtext from CODE_PRODUCT_LINE where pline_code in (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') order by pline_name";
        DataTable dt1 = theDc.GetTable(Sql1);

        dropPline.DataSource = dt1;
        dropPline.TextField = dt1.Columns[1].ToString();
        dropPline.ValueField = dt1.Columns[0].ToString();

        string Sql2 = "select USER_CODE,USER_NAME from CODE_USER where user_type='B' order by nlssort(USER_NAME,'NLS_SORT=SCHINESE_PINYIN_M')";
        DataTable dt2 = theDc.GetTable(Sql2);

        dropLeader.DataSource = dt2;
        dropLeader.TextField = dt2.Columns[1].ToString();
        dropLeader.ValueField = dt2.Columns[0].ToString();


        if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
        {
            ///主键不可以修改
            (ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxTextBox).Enabled = false;
        }
        else
        {
            dropPline.SelectedIndex = 0;
            dropLeader.SelectedIndex = 0;
        }
    }
    // 数据校验
    protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        //判断超长
        if (e.NewValues["TEAM_CODE"].ToString().Length > 15)
        {
            e.RowError = "班组代码字节长度不能超过15！";
        }
        if (e.NewValues["TEAM_NAME"].ToString().Length > 15)
        {
            e.RowError = "班组名称字节长度不能超过15！";
        }
        if (e.NewValues["PLINE_CODE"].ToString().Length > 15)
        {
            e.RowError = "生产线字节长度不能超过15！";
        }
        //判断为空
        if (e.NewValues["TEAM_CODE"].ToString() == "" || e.NewValues["TEAM_CODE"].ToString() == null)
        {
            e.RowError = "班组代码不能为空！";
        }
        if (e.NewValues["TEAM_NAME"].ToString() == "" || e.NewValues["TEAM_NAME"].ToString() == null)
        {
            e.RowError = "班组名称不能为空！";
        }
        if (e.NewValues["PLINE_CODE"].ToString() == "" || e.NewValues["PLINE_CODE"].ToString() == null)
        {
            e.RowError = "生产线不能为空！";
        }
        if (e.NewValues["LEADER_CODE"].ToString() == "" || e.NewValues["LEADER_CODE"].ToString() == null)
        {
            e.RowError = "班组长不能为空！";
        }

        //新增判断键值重复
        if (ASPxGridView1.IsNewRowEditing)
        {
            string chSql = "SELECT COMPANY_CODE, TEAM_CODE FROM CODE_TEAM"
                + " WHERE COMPANY_CODE = '" + theCompanyCode + "' AND TEAM_CODE='" + e.NewValues["TEAM_CODE"].ToString() + "'";
            dataConn dc = new dataConn(chSql);
            if (dc.GetState() == true)
            {
                e.RowError = "已存在相同的班组代码！";
            }
        }
    }
    // 删除
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        //判断当前记录是否可以删除
        string delStr = e.Values["TEAM_CODE"].ToString();
        dataConn theDataConn = new dataConn("select func_check_delete_data('CODE_TEAM','" + theCompanyCode + "','MES','MES','MES','" + delStr + "') from dual");

        theDataConn.OpenConn();
        string theRet = theDataConn.GetValue();
        if (theRet != "Y")
        {
            ASPxGridView1.JSProperties.Add("cpCallbackName", "Delete");
            ASPxGridView1.JSProperties.Add("cpCompanyName", theRet);
            theDataConn.CloseConn();
        }
        else
        {
            //删除操作
            string dSql = "DELETE FROM CODE_TEAM WHERE COMPANY_CODE='" + theCompanyCode + "' AND TEAM_CODE='" + e.Values["TEAM_CODE"].ToString() + "'";
            theDc.ExeSql(dSql);
        }
        e.Cancel = true;
        queryFunction();
    }
    
}
