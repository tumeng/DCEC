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

using System.Text.RegularExpressions;

using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxClasses;
using Rmes.Pub.Data;


/**
 * 功能概述：班次定义
 * 作者：曹路圆
 * 创建时间：2011-08-01
 **/



public partial class Rmes_epd2100 : Rmes.Web.Base.BasePage
{
    private dataConn dc = new dataConn();
    private PubCs pc = new PubCs();
    public string theCompanyCode;
    private string theUserId;
    public string theProgramCode;
    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theUserId = theUserManager.getUserId();
        theProgramCode = "epd2100";
        //this.TranslateASPxControl(ASPxGridView1);


        if (!IsPostBack)
        {

        }
        setCondition();
    }


    private void setCondition()
    {
        //初始化GRIDVIEW
        string sql = "SELECT A.SHIFT_CODE,A.SHIFT_NAME,A.BEGIN_TIME,A.END_TIME,A.PLINE_CODE,B.PLINE_NAME,A.SHIFT_MANHOUR,DECODE(A.IS_CROSS_DAY,'Y',1,'N',0) IS_CROSS_DAY FROM CODE_SHIFT A"
            + " LEFT JOIN CODE_PRODUCT_LINE B ON A.COMPANY_CODE = B.COMPANY_CODE AND A.PLINE_CODE = B.PLINE_CODE"
            + " WHERE A.COMPANY_CODE = '" + theCompanyCode + "' and a.pline_code in (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') ORDER BY PLINE_CODE,SHIFT_CODE";
        DataTable dt = dc.GetTable(sql);

        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
    }

    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        //判断当前记录是否可以删除
        string strDelCode = e.Values["SHIFT_CODE"].ToString();
        string strTableName = "CODE_SHIFT";

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
            string Sql = "DELETE FROM CODE_SHIFT WHERE  COMPANY_CODE = '" + theCompanyCode + "' AND SHIFT_CODE = '" + strDelCode + "'";
            dc.ExeSql(Sql);
        }

        setCondition();
        e.Cancel = true;
    }

    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxTextBox uCode = ASPxGridView1.FindEditFormTemplateControl("txtShiftCode") as ASPxTextBox;
        ASPxTextBox uName = ASPxGridView1.FindEditFormTemplateControl("txtShiftName") as ASPxTextBox;
        ASPxTextBox sTime = ASPxGridView1.FindEditFormTemplateControl("txtBeginTime") as ASPxTextBox;
        ASPxTextBox eTime = ASPxGridView1.FindEditFormTemplateControl("txtEndTime") as ASPxTextBox;        
        ASPxComboBox uPcode = ASPxGridView1.FindEditFormTemplateControl("dropPlineCode") as ASPxComboBox;
        ASPxCheckBox chkCrossDay = ASPxGridView1.FindEditFormTemplateControl("chkCrossDay") as ASPxCheckBox;
        ASPxTextBox mHour = ASPxGridView1.FindEditFormTemplateControl("txtManHour") as ASPxTextBox;

        string vFlag = "";
        if (chkCrossDay.Checked == true)
        {
            vFlag = "Y";
        }
        else
        {
            vFlag = "N";
        }

        string Sql = "INSERT INTO CODE_SHIFT (COMPANY_CODE,SHIFT_CODE,SHIFT_NAME,BEGIN_TIME,END_TIME,PLINE_CODE,IS_CROSS_DAY,SHIFT_MANHOUR) "
             + "VALUES('" + theCompanyCode + "','" + uCode.Text.Trim().ToUpper() + "','" + uName.Text.Trim() + "','" + sTime.Text.ToString() + "','" + eTime.Text.ToString() + "','" + uPcode.Value.ToString() + "','" + vFlag + "','"+mHour.Text.Trim()+"')";
        dc.ExeSql(Sql);

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        setCondition();
    }

    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxTextBox uCode = ASPxGridView1.FindEditFormTemplateControl("txtShiftCode") as ASPxTextBox;
        ASPxTextBox uName = ASPxGridView1.FindEditFormTemplateControl("txtShiftName") as ASPxTextBox;
        ASPxTextBox sTime = ASPxGridView1.FindEditFormTemplateControl("txtBeginTime") as ASPxTextBox;
        ASPxTextBox eTime = ASPxGridView1.FindEditFormTemplateControl("txtEndTime") as ASPxTextBox;
        ASPxComboBox uPcode = ASPxGridView1.FindEditFormTemplateControl("dropPlineCode") as ASPxComboBox;
        ASPxCheckBox chkCrossDay = ASPxGridView1.FindEditFormTemplateControl("chkCrossDay") as ASPxCheckBox;
        ASPxTextBox mHour = ASPxGridView1.FindEditFormTemplateControl("txtManHour") as ASPxTextBox;

        

        string vFlag = "";
        if (chkCrossDay.Checked == true)
        {
            vFlag = "Y";
        }
        else
        {
            vFlag = "N";
        }



        string Sql = "UPDATE CODE_SHIFT SET SHIFT_NAME='" + uName.Text.Trim() + "', BEGIN_TIME='" + sTime.Text.ToString() + "', END_TIME='" + eTime.Text.ToString() + "', PLINE_CODE='" + uPcode.Value.ToString() + "', IS_CROSS_DAY='" + vFlag + "',SHIFT_MANHOUR='"+mHour.Text.Trim()+"'"
             + " WHERE  COMPANY_CODE = '" + theCompanyCode + "' and SHIFT_CODE = '" + uCode.Text.Trim().ToUpper() + "'";
        dc.ExeSql(Sql);

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        setCondition();

    }

    protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        //判断超长
        if (e.NewValues["SHIFT_CODE"].ToString().Length > 30)
        {
            e.RowError = "班次代码字节长度不能超过30！";
        }
        if (e.NewValues["SHIFT_NAME"].ToString().Length > 30)
        {
            e.RowError = "班次名称字节长度不能超过30！";
        }

        string strBeginTime = e.NewValues["BEGIN_TIME"].ToString();
        string strEndTime = e.NewValues["END_TIME"].ToString();


        string strRegular = @"([01]?\d|2[0-3]):[0-5]?\d:[0-5]?\d";

        Regex rx = new Regex(strRegular);

        bool isMatch = rx.IsMatch(strBeginTime);

        if (!isMatch)
        {
            e.RowError = "时间请按格式“HH:mm:ss”输入，注意\":\"为半角符号！";
        }

        if (!rx.IsMatch(strEndTime))
        {
            e.RowError = "时间请按格式“HH:mm:ss”输入，注意\":\"为半角符号！";
        }


        //判断为空
        if (e.NewValues["SHIFT_CODE"].ToString() == "" || e.NewValues["SHIFT_CODE"].ToString() == null)
        {
            e.RowError = "班次代码不能为空！";
        }
        if (e.NewValues["SHIFT_NAME"].ToString() == "" || e.NewValues["SHIFT_NAME"].ToString() == null)
        {
            e.RowError = "班次名称不能为空！";
        }



        //判断是否重复
        if (ASPxGridView1.IsNewRowEditing)
        {
            string chSql = "SELECT SHIFT_CODE, SHIFT_NAME  FROM CODE_SHIFT"
                + " WHERE COMPANY_CODE = '" + theCompanyCode + "' AND SHIFT_CODE='" + e.NewValues["SHIFT_CODE"].ToString().ToUpper() + "' AND PLINE_CODE='" + e.NewValues["PLINE_CODE"].ToString() + "'";
            DataTable dt = dc.GetTable(chSql);
            if (dt.Rows.Count > 0)
            {
                e.RowError = "该生产线已存在相同的班次代码！";
            }
        }

    }



    protected void ASPxGridView1_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {

        ASPxComboBox uPcode = ASPxGridView1.FindEditFormTemplateControl("dropPlineCode") as ASPxComboBox;

        string Sql2 = "select PLINE_CODE,PLINE_CODE||' '||pline_name as showtext from CODE_PRODUCT_LINE where pline_code in (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "')";
        DataTable dt2 = dc.GetTable(Sql2);

        uPcode.DataSource = dt2;
        uPcode.TextField = dt2.Columns[1].ToString();
        uPcode.ValueField = dt2.Columns[0].ToString();


        if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
        {
            ///主键不可以修改
            (ASPxGridView1.FindEditFormTemplateControl("txtShiftCode") as ASPxTextBox).Enabled = false;

            if (ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "IS_CROSS_DAY").ToString() == "1")
            {
                (ASPxGridView1.FindEditFormTemplateControl("chkCrossDay") as ASPxCheckBox).Checked = true;
            }
            else
            {
                (ASPxGridView1.FindEditFormTemplateControl("chkCrossDay") as ASPxCheckBox).Checked = false;
            }
        }
        else
        {
            (ASPxGridView1.FindEditFormTemplateControl("chkCrossDay") as ASPxCheckBox).Checked = false;
            //uPcode.SelectedIndex = 0;
        }
        

    }



}
