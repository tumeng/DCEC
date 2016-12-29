using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Rmes.Pub.Data;
using Rmes.Pub.Function;
using DevExpress.Web.ASPxClasses.Internal;
using DevExpress.Web.ASPxEditors;

/**
 * 功能概述：密码修改
 * 作者：杨少霞
 * 创建时间：2011-07-26
 */

public partial class Rmes_Sam_sam2400_sam2400 : System.Web.UI.Page
{
    public String theCompanyCode;
    public String theUserCode;
    public String theUserName;
    public String theOldPassword;
    public PubJs thePubJs = new PubJs();
    public PubCs thePubCs = new PubCs();
    public String theSql;
    public string theUserId;
    private dataConn dc = new dataConn();

    private bool canUpdate = true;

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theUserCode = theUserManager.getUserCode();
        theUserName = theUserManager.getUserName();
        theUserId = theUserManager.getUserId();

        this.txtUserCode.Text = theUserCode;
        this.txtUserName.Text = theUserName;

        this.txtUserCode.Enabled = false;
        this.txtUserName.Enabled = false;

        TextBoxOldPass.Focus();

        getPassword();
        //string a = thePubCs.AESDecrypt("VxFs8B7jBINGy8nozGdmqJmxBmbIIiuekpQAT9FFlpY=");
    }

    protected void btnSure_Click(object sender, EventArgs e)
    {

        if (canUpdate == false)
        {
            return;
        }


        //从界面上得到输入的旧密码，新密码，确认密码
        string oldPassword = TextBoxOldPass.Text.Trim();
        string newPassword = TextBoxNewPass.Text.Trim();
        string newPasswordSure = TextBoxNewPassSure.Text.Trim();

        String updateSql = "update code_user set USER_PASSWORD='" +thePubCs.AESEncrypt(newPassword) + "',USER_OLD_PASSWORD='" + theOldPassword + "'"
                         + " where COMPANY_CODE='" + theCompanyCode + "' and USER_ID='" + theUserId + "'";

        dc.ExeSql(updateSql);
        getPassword();
        dc.CloseConn();
        TextBoxOldPass.Focus();
        Response.Redirect("/Rmes/Login/RmesIndex.aspx?progCode=rmesIndex&progName=系统登录");
    }

    private void getPassword()
    {
        //根据公司号和用户代码从code_user表中得到USER_OLD_PASSWORD,USER_PASSWORD
        theSql = "SELECT USER_PASSWORD,USER_OLD_PASSWORD FROM CODE_USER WHERE COMPANY_CODE='" + theCompanyCode + "' AND USER_ID='" + theUserId + "'";
        dataConn theDa = new dataConn(theSql);
        DataTable theDt = new DataTable();
        theDt = theDa.GetTable();
        theOldPassword = theDt.Rows[0]["USER_PASSWORD"].ToString();

    }
    protected void TextBoxOldPass_Validation(object sender, ValidationEventArgs e)
    {
        string inputOldPass = (string)e.Value;
        if (inputOldPass !=thePubCs.AESDecrypt(theOldPassword))
        {
            e.IsValid = false;
            canUpdate = false;
            //e.ErrorText = "chao";
        }
    }
    protected void TextBoxNewPassSure_Validation(object sender, ValidationEventArgs e)
    {
        string inputNewPass = (string)e.Value;

    }
    protected void TextBoxNewPass_Validation(object sender, ValidationEventArgs e)
    {
        if (!thePubCs.isUserPasswd((string)e.Value))
        {
            e.ErrorText = "密码格式不合法！必须包含字母，数字和特殊字符，不能包含空格";
            e.IsValid = false;
            canUpdate = false;
        }
    }
}
