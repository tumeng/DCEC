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
 * 功能概述：用户定义
 * 作者：杨少霞
 * 创建时间：2011-07-20
 */

public partial class Rmes_Sam_sam1200_sam1200 : Rmes.Web.Base.BasePage
{
    public string theCompanyCode;
    public dataConn theDc = new dataConn();
    public PubCs thePubPc = new PubCs();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();

        queryFunction();
        string a = thePubPc.AESEncrypt("admin");
    }

    //初始化GRIDVIEW
    private void queryFunction()
    {
        string sql = "SELECT A.COMPANY_CODE, A.USER_ID, A.USER_CODE, A.USER_NAME, A.USER_DEPT_CODE, B.DEPT_NAME, DECODE(USER_SEX,'M','男','F','女')USER_SEX, A.USER_PASSWORD,A.USER_OLD_PASSWORD,A.USER_AUTHORIZED_IP,A.USER_MAXNUM,"
                   + "DECODE(VALID_FLAG,'Y','有效','N','无效') VALID_FLAG, DECODE(LOCK_FLAG,'0','未锁定','1','未锁定','2','未锁定','3','锁定') LOCK_FLAG,A.USER_EMAIL,A.USER_TEL,A.USER_QQ,A.USER_WECHAT FROM CODE_USER A LEFT JOIN CODE_DEPT B ON A.USER_DEPT_CODE=B.DEPT_CODE WHERE A.COMPANY_CODE = '" + theCompanyCode + "' and user_type='A'";
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
        ASPxRadioButton radM = ASPxGridView1.FindEditFormTemplateControl("RadMan") as ASPxRadioButton;
        ASPxRadioButton radW = ASPxGridView1.FindEditFormTemplateControl("RadWoman") as ASPxRadioButton;
        ASPxTextBox authIp = ASPxGridView1.FindEditFormTemplateControl("txtIp") as ASPxTextBox;
        ASPxTextBox uMax = ASPxGridView1.FindEditFormTemplateControl("TextBoxMax") as ASPxTextBox;
        ASPxTextBox uTel = ASPxGridView1.FindEditFormTemplateControl("txtTel") as ASPxTextBox;
        ASPxTextBox uEmail = ASPxGridView1.FindEditFormTemplateControl("txtEmail") as ASPxTextBox;
        ASPxTextBox uQQ = ASPxGridView1.FindEditFormTemplateControl("TextQQ") as ASPxTextBox;
        ASPxTextBox uWeChat = ASPxGridView1.FindEditFormTemplateControl("txtWeChat") as ASPxTextBox;
        ASPxCheckBox chVFlag = ASPxGridView1.FindEditFormTemplateControl("chValidFlag") as ASPxCheckBox;
        ASPxCheckBox chLFlag = ASPxGridView1.FindEditFormTemplateControl("chLockFlag") as ASPxCheckBox;
        ASPxComboBox uDept = ASPxGridView1.FindEditFormTemplateControl("txtDept") as ASPxComboBox;
        string vFlag = "";
        if (chVFlag.Checked == true)
        {
            vFlag = "Y";
        }
        else
        {
            vFlag = "N";
        }

        string uSex = "";
        if (radM.Checked == true)
        {
            uSex = "M";
        }
        if (radW.Checked == true)
        {
            uSex = "F";
        }

        int lFlag;
        if (chLFlag.Checked == true)
        {
            lFlag = 3;
        }
        else
        {
            lFlag = 0;
        }
        string email = uEmail.Text.Trim();
        email=email.Replace("@","@@");
        string upSql = "UPDATE CODE_USER SET USER_CODE='" + uCode.Text.Trim().ToUpper() + "',USER_NAME='" + uName.Text.Trim() + "',USER_SEX='" + uSex + "',"
                     + "USER_AUTHORIZED_IP='" + authIp.Text.Trim() + "',USER_MAXNUM='" + uMax.Text.Trim() + "',VALID_FLAG='" + vFlag + "',LOCK_FLAG='" + lFlag + "',"
                     + "USER_EMAIL='" + email + "',USER_TEL='" + uTel.Text.Trim() + "',USER_QQ='" + uQQ.Text.Trim() + "',USER_WECHAT='" + uWeChat.Text.Trim() + "',USER_DEPT_CODE='" + uDept.Value.ToString() + "' "
                     + "WHERE  COMPANY_CODE = '" + theCompanyCode + "' AND USER_ID='" + userId + "'";
        theDc.ExeSql(upSql);

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        queryFunction();
    }
    // 新增
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxTextBox uCode = ASPxGridView1.FindEditFormTemplateControl("txtUCode") as ASPxTextBox;
        ASPxTextBox uName = ASPxGridView1.FindEditFormTemplateControl("txtUName") as ASPxTextBox;
        ASPxRadioButton radM = ASPxGridView1.FindEditFormTemplateControl("RadMan") as ASPxRadioButton;
        ASPxRadioButton radW = ASPxGridView1.FindEditFormTemplateControl("RadWoman") as ASPxRadioButton;
        ASPxTextBox authIp = ASPxGridView1.FindEditFormTemplateControl("txtIp") as ASPxTextBox;
        ASPxTextBox uMax = ASPxGridView1.FindEditFormTemplateControl("TextBoxMax") as ASPxTextBox;
        ASPxTextBox uTel = ASPxGridView1.FindEditFormTemplateControl("txtTel") as ASPxTextBox;
        ASPxTextBox uEmail = ASPxGridView1.FindEditFormTemplateControl("txtEmail") as ASPxTextBox;
        ASPxTextBox uQQ = ASPxGridView1.FindEditFormTemplateControl("TextQQ") as ASPxTextBox;
        ASPxTextBox uWeChat = ASPxGridView1.FindEditFormTemplateControl("txtWeChat") as ASPxTextBox;
        ASPxCheckBox chVFlag = ASPxGridView1.FindEditFormTemplateControl("chValidFlag") as ASPxCheckBox;
        ASPxCheckBox chLFlag = ASPxGridView1.FindEditFormTemplateControl("chLockFlag") as ASPxCheckBox;
        ASPxComboBox uDept = ASPxGridView1.FindEditFormTemplateControl("txtDept") as ASPxComboBox;
        string vFlag = "";
        if (chVFlag.Checked == true)
        {
            vFlag = "Y";
        }
        else
        {
            vFlag = "N";
        }

        string uSex = "";
        if (radM.Checked == true)
        {
            uSex = "M";
        }
        if (radW.Checked == true)
        {
            uSex = "F";
        }
        string email = uEmail.Text.Trim();
        email = email.Replace("@", "@@");
        string inSql = "INSERT INTO CODE_USER (COMPANY_CODE, USER_ID, USER_CODE, USER_NAME, USER_SEX, USER_PASSWORD,USER_AUTHORIZED_IP,USER_MAXNUM,VALID_FLAG,LOCK_FLAG,USER_EMAIL,USER_TEL,USER_QQ,USER_WECHAT,USER_DEPT_CODE,user_type) "
                 + "VALUES('" + theCompanyCode + "',TRIM(TO_CHAR(SEQ_USER_ID.NEXTVAL,'000000')),'" + uCode.Text.Trim().ToUpper() + "','" + uName.Text.Trim() + "','" + uSex + "','" + thePubPc.AESEncrypt(uCode.Text.Trim().ToUpper()) + "',"
                 + "'" + authIp.Text.Trim() + "','" + uMax.Text.Trim() + "','" + vFlag + "','0','" + email + "','" + uTel.Text.Trim() + "','" + uQQ.Text.Trim() + "','" + uWeChat.Text.Trim() + "','" + uDept.Value.ToString() + "','A')";
        dataConn inDc = new dataConn();  
        inDc.ExeSql(inSql);
        inDc.CloseConn();

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        queryFunction();
    }
    // 进入编辑界面时的处理
    protected void ASPxGridView1_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {

        string Sql = "select DEPT_CODE,DEPT_CODE||' '||DEPT_NAME as showtext from CODE_DEPT";
        DataTable dt = theDc.GetTable(Sql);
        ASPxComboBox uSType = ASPxGridView1.FindEditFormTemplateControl("txtDept") as ASPxComboBox;
        uSType.DataSource = dt;
        uSType.TextField = dt.Columns[1].ToString();
        uSType.ValueField = dt.Columns[0].ToString();
        //修改
        if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
        {
            //处理ASPxCheckBox
            if (ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "VALID_FLAG").ToString() == "有效")
            {
                (ASPxGridView1.FindEditFormTemplateControl("chValidFlag") as ASPxCheckBox).Checked = true;
            }
            else
            {
                (ASPxGridView1.FindEditFormTemplateControl("chValidFlag") as ASPxCheckBox).Checked = false;
            }
            //处理ASPxCheckBox
            if (ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "LOCK_FLAG").ToString() == "锁定")
            {
                (ASPxGridView1.FindEditFormTemplateControl("chLockFlag") as ASPxCheckBox).Checked = true;
            }
            else
            {
                (ASPxGridView1.FindEditFormTemplateControl("chLockFlag") as ASPxCheckBox).Checked = false;
            }
            //处理性别
            if (ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "USER_SEX").ToString() == "男")
            {
                (ASPxGridView1.FindEditFormTemplateControl("RadMan") as ASPxRadioButton).Checked = true;
            }
            if (ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "USER_SEX").ToString() == "女")
            {
                (ASPxGridView1.FindEditFormTemplateControl("RadWoman") as ASPxRadioButton).Checked = true;
            }
        }
        //新增
        if (ASPxGridView1.IsNewRowEditing)
        {
            (ASPxGridView1.FindEditFormTemplateControl("chLockFlag") as ASPxCheckBox).Visible = false;
            (ASPxGridView1.FindEditFormTemplateControl("Label8") as Label).Visible = false;
        }
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
        if (e.NewValues["USER_AUTHORIZED_IP"].ToString().Length > 20)
        {
            e.RowError = "授权IP字节长度不能超过20！";
        }
        if (e.NewValues["USER_EMAIL"].ToString().Length > 30)
        {
            e.RowError = "EMAIL字节长度不能超过30！";
        }
        if (e.NewValues["USER_TEL"].ToString().Length > 30)
        {
            e.RowError = "电话字节长度不能超过30！";
        }
        if (e.NewValues["USER_QQ"].ToString().Length > 30)
        {
            e.RowError = "QQ字节长度不能超过30！";
        }
        if (e.NewValues["USER_WECHAT"].ToString().Length > 30)
        {
            e.RowError = "微信号字节长度不能超过30！";
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
        //if (e.NewValues["USER_MAXNUM"].ToString() == "" || e.NewValues["USER_MAXNUM"].ToString() == null)
        //{
        //    e.RowError = "最大登录数不能为空！";
        //}
        //判断输入为整形
        //if (thePubPc.IsNumeric(e.NewValues["USER_MAXNUM"].ToString()) == false)
        //{
        //    e.RowError = "最大用户数必须输入为整形！";
        //}
        //判断有效性为Y的同一个用户代码只能有一个
        if (ASPxGridView1.IsNewRowEditing)
        {
            //string chSql = "SELECT COMPANY_CODE, USER_CODE  FROM CODE_USER"
            //                    + " WHERE COMPANY_CODE = '" + theCompanyCode + "' AND USER_CODE='" + e.NewValues["USER_CODE"].ToString() + "' AND VALID_FLAG='Y'";
            //DataTable dt = theDc.GetTable(chSql);
            //if (dt.Rows.Count == 1)
            //{
            //    e.RowError = "用户代码重复！";
            //}
            checkUnique(e);
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
                                + " WHERE COMPANY_CODE = '" + theCompanyCode + "' AND USER_CODE='" + e.NewValues["USER_CODE"].ToString().ToUpper() + "' AND VALID_FLAG='Y'";
        DataTable dt = theDc.GetTable(chSql);
        if (dt.Rows.Count == 1)
        {
            e.RowError = "用户代码重复！";
        }
    }
    // 删除
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        //判断当前记录是否可以删除
        string delStr = e.Values["USER_ID"].ToString();
        dataConn theDataConn = new dataConn("select func_check_delete_data('CODE_USER','" + theCompanyCode + "','MES','MES','MES','" + delStr + "') from dual");

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
            string dSql = "DELETE FROM CODE_USER WHERE COMPANY_CODE='" + theCompanyCode + "' AND USER_ID='" + e.Values["USER_ID"].ToString() + "'";
            theDc.ExeSql(dSql);
        }
        e.Cancel = true;
        queryFunction();
    }
    

    //密码重置
    protected void ASPxGridView1_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        if (e.ButtonID == "passOri")
        {
            string uId = ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "USER_ID").ToString();
            string uCode = ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "USER_CODE").ToString().ToUpper();
            string a = thePubPc.AESEncrypt(uCode);
            string oriSql = "UPDATE CODE_USER SET USER_PASSWORD='" + a + "',lock_flag='0' WHERE COMPANY_CODE='" + theCompanyCode + "' AND USER_ID='" + uId + "'";
            theDc.ExeSql(oriSql);

            queryFunction();

            ASPxGridView1.JSProperties.Add("cpCallbackName", "Ori");
        }
    }
}
