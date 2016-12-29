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
 * 功能概述：程序定义
 * 作者：杨少霞
 * 创建时间：2011-07-20
 */

public partial class Rmes_Sam_sam1400_sam1400 : Rmes.Web.Base.BasePage
{
    public string theCompanyCode;
    public dataConn theDc = new dataConn();

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
 
        queryFunction();

    }
    //初始化GRIDVIEW
    private void queryFunction()
    {
        string sql = "SELECT COMPANY_CODE, PROGRAM_CODE, PROGRAM_NAME, PROGRAM_NAME_EN, PROGRAM_VALUE,RIGHT_FLAG,DECODE(RIGHT_FLAG,'Y','是','N','否') RIGHT_FLAG1 FROM CODE_PROGRAM WHERE COMPANY_CODE = '" + theCompanyCode + "' ORDER BY PROGRAM_CODE";
        DataTable dt = theDc.GetTable(sql);

        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
    }

    // 修改
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxTextBox pCode = ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxTextBox;
        ASPxTextBox pName = ASPxGridView1.FindEditFormTemplateControl("txtPName") as ASPxTextBox;
        ASPxTextBox pNameEn = ASPxGridView1.FindEditFormTemplateControl("txtPNameEn") as ASPxTextBox;
        ASPxTextBox pValue = ASPxGridView1.FindEditFormTemplateControl("txtPValue") as ASPxTextBox;
        ASPxCheckBox chRFlag = ASPxGridView1.FindEditFormTemplateControl("chRFlag") as ASPxCheckBox;

        string rFlag = "";
        if (chRFlag.Checked == true)
        {
            rFlag = "Y";
        }
        else
        {
            rFlag = "N";
        }

        string upSql = "UPDATE CODE_PROGRAM SET PROGRAM_NAME='" + pName.Text.Trim() + "',PROGRAM_NAME_EN='" + pNameEn.Text.Trim() + "',PROGRAM_VALUE='" + pValue.Text.Trim() + "',"
                     + "RIGHT_FLAG='" + rFlag + "' WHERE  COMPANY_CODE = '" + theCompanyCode + "' AND PROGRAM_CODE='" + pCode.Text.Trim() + "'";
        theDc.ExeSql(upSql);

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        queryFunction();
    }
    // 新增
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxTextBox pCode = ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxTextBox;
        ASPxTextBox pName = ASPxGridView1.FindEditFormTemplateControl("txtPName") as ASPxTextBox;
        ASPxTextBox pNameEn = ASPxGridView1.FindEditFormTemplateControl("txtPNameEn") as ASPxTextBox;
        ASPxTextBox pValue = ASPxGridView1.FindEditFormTemplateControl("txtPValue") as ASPxTextBox;
        ASPxCheckBox chRFlag = ASPxGridView1.FindEditFormTemplateControl("chRFlag") as ASPxCheckBox;

        //string sql = "select 'PROG'||TRIM(TO_CHAR(SEQ_USER_ID.NEXTVAL,'000000')) from dual";
        //theDc.setTheSql(sql);
        //string dece_code1 = theDc.GetValue();
        string dece_code1 = pCode.Text.Trim();
        string rFlag = "";
        if (chRFlag.Checked == true)
        {
            rFlag = "Y";
        }
        else
        {
            rFlag = "N";
        }

        string inSql = "INSERT INTO CODE_PROGRAM (COMPANY_CODE, PROGRAM_CODE, PROGRAM_NAME, PROGRAM_NAME_EN, PROGRAM_VALUE, RIGHT_FLAG) "
                 + "VALUES('" + theCompanyCode + "','" + dece_code1 + "','" + pName.Text.Trim() + "','" + pNameEn.Text.Trim() + "','" + pValue.Text.Trim() + "','" + rFlag + "')";
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
        if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
        {
            ///主键不可以修改
            (ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxTextBox).Enabled = false;
            
            ///处理ASPxCheckBox
            if (ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "RIGHT_FLAG").ToString() == "Y")
            {
                (ASPxGridView1.FindEditFormTemplateControl("chRFlag") as ASPxCheckBox).Checked = true;
            }
            else
            {
                (ASPxGridView1.FindEditFormTemplateControl("chRFlag") as ASPxCheckBox).Checked = false;
            }
        }
        if (ASPxGridView1.IsNewRowEditing)
        {
            //(ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxTextBox).Enabled = false;
        }
    }
    // 数据校验
    protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        //判断超长
        if (e.NewValues["PROGRAM_CODE"].ToString().Length > 50)
        {
            e.RowError = "程序代码字节长度不能超过50！";
        }
        if (e.NewValues["PROGRAM_NAME"].ToString().Length > 50)
        {
            e.RowError = "程序名称字节长度不能超过50！";
        }
        if (e.NewValues["PROGRAM_NAME_EN"].ToString().Length > 50)
        {
            e.RowError = "英文缩写字节长度不能超过50！";
        }
        if (e.NewValues["PROGRAM_VALUE"].ToString().Length > 500)
        {
            e.RowError = "链接地址字节长度不能超过500！";
        }
        //判断为空
        //if (e.NewValues["PROGRAM_CODE"].ToString()=="" || e.NewValues["PROGRAM_CODE"].ToString()==null )
        //{
        //    e.RowError = "程序代码不能为空！";
        //}
        if (e.NewValues["PROGRAM_NAME"].ToString() == "" || e.NewValues["PROGRAM_NAME"].ToString() == null)
        {
            e.RowError = "程序名称不能为空！";
        }
        if (e.NewValues["PROGRAM_NAME_EN"].ToString() == "" || e.NewValues["PROGRAM_NAME_EN"].ToString() == null)
        {
            e.RowError = "英文缩写不能为空！";
        }
        if (e.NewValues["PROGRAM_VALUE"].ToString() == "" || e.NewValues["PROGRAM_VALUE"].ToString() == null)
        {
            e.RowError = "链接地址不能为空！";
        }
        //新增判断键值重复
        if (ASPxGridView1.IsNewRowEditing)
        {
            string chSql = "SELECT COMPANY_CODE, PROGRAM_CODE, PROGRAM_NAME, PROGRAM_NAME_EN, PROGRAM_VALUE, RIGHT_FLAG FROM CODE_PROGRAM"
                + " WHERE COMPANY_CODE = '" + theCompanyCode + "' AND PROGRAM_CODE='" + e.NewValues["PROGRAM_CODE"].ToString() + "'";
            dataConn dc = new dataConn(chSql);
            if (dc.GetState() == true)
            {
                e.RowError = "已存在相同的程序代码！";
            }
        }
        //判断program_value不能重复
        string chSqlV = "SELECT COMPANY_CODE, PROGRAM_CODE, PROGRAM_NAME, PROGRAM_NAME_EN, PROGRAM_VALUE, RIGHT_FLAG FROM CODE_PROGRAM"
                + " WHERE COMPANY_CODE = '" + theCompanyCode + "' AND PROGRAM_VALUE='" + e.NewValues["PROGRAM_VALUE"].ToString() + "'";
        dataConn dcV = new dataConn(chSqlV);
        if (!ASPxGridView1.IsNewRowEditing)
        {
            if (dcV.GetState() == true && e.NewValues["PROGRAM_VALUE"].ToString() != e.OldValues["PROGRAM_VALUE"].ToString())
            {
                e.RowError = "链接地址输入重复，请重新输入！";
            }
        }
        if (ASPxGridView1.IsNewRowEditing)
        {
            if (dcV.GetState() == true )
            {
                e.RowError = "链接地址输入重复，请重新输入！";
            }
        }
    }
    // 删除
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        //判断当前记录是否可以删除
        string delStr = e.Values["PROGRAM_CODE"].ToString();
        dataConn theDataConn = new dataConn("select func_check_delete_data('CODE_PROGRAM','" + theCompanyCode + "','MES','MES','MES','" + delStr + "') from dual");

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
            string dSql = "DELETE FROM CODE_PROGRAM WHERE COMPANY_CODE='" + theCompanyCode + "' AND PROGRAM_CODE='" + e.Values["PROGRAM_CODE"].ToString() + "'";
            theDc.ExeSql(dSql);
        }
        e.Cancel = true;
        queryFunction();
    }

    protected void ASPxGridView1_BeforeColumnSortingGrouping(object sender, ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
    {
        queryFunction();
    }
}
