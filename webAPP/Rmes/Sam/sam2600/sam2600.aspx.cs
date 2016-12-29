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
 * 功能概述：用户标签定义
 * 作者：李蒙蒙
 * 创建时间：2011-07-27
 */

public partial class Rmes_Sam_sam2600_sam2600 : Rmes.Web.Base.BasePage
{
    public string theCompanyCode;
    public dataConn theDC = new dataConn();

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        this.TranslateASPxControl(ASPxGridView1);
        queryFunction();

    }
    //初始化GRIDVIEW
    private void queryFunction()
    {
        string sql = "SELECT a.Company_Code, a.User_ID,c.User_Name,b.Tag_Code_Father,b.Tag_Name_Father,a.Tag_Code,b.Tag_Name,a.Tag_Flag FROM REL_USER_TAG a,CODE_TAG b,CODE_USER c WHERE "+
                    "a.Tag_Code=b.Tag_Code AND a.User_ID=c.User_ID AND a.COMPANY_CODE = '" + theCompanyCode + "' ORDER BY User_ID,Tag_Code";
        DataTable dt = theDC.GetTable(sql);

        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
    }

    // 修改
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {

        ASPxComboBox pUserID = ASPxGridView1.FindEditFormTemplateControl("drpUID") as ASPxComboBox;
        ASPxComboBox pTagCode = ASPxGridView1.FindEditFormTemplateControl("drpTagCode") as ASPxComboBox;
        ASPxCheckBox chkTagFlag = ASPxGridView1.FindEditFormTemplateControl("chkTagFlag") as ASPxCheckBox;

        //string tagCodeFather = theDC.GetTable("SELECT * FROM CODE_TAG WHERE TAG_CODE='" + pTagCode + "'").Rows[0]["TAG_CODE_FATHER"].ToString();
        string tagFlag = "";
        if (chkTagFlag.Checked == true)
        {
            tagFlag = "Y";
        }
        else
        {
            tagFlag = "N";
        }

        string upSql = "UPDATE REL_USER_TAG SET TAG_FLAG='" + tagFlag + "' WHERE  COMPANY_CODE = '" +
                       theCompanyCode + "' AND USER_ID='" + pUserID.Value.ToString().Trim() + "' AND TAG_CODE='" + pTagCode.Value.ToString().Trim() + "'";
        theDC.ExeSql(upSql);

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        queryFunction();
    }
    // 新增
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxComboBox pUserID = ASPxGridView1.FindEditFormTemplateControl("drpUID") as ASPxComboBox;
        ASPxComboBox pTagCode = ASPxGridView1.FindEditFormTemplateControl("drpTagCode") as ASPxComboBox;
        ASPxCheckBox chkTagFlag = ASPxGridView1.FindEditFormTemplateControl("chkTagFlag") as ASPxCheckBox;

        //string tagCodeFather = theDC.GetTable("SELECT * FROM CODE_TAG WHERE TAG_CODE='" + pTagCode + "'").Rows[0]["TAG_CODE_FATHER"].ToString();

        
        string tagFlag = "";
        if (chkTagFlag.Checked == true)
        {
            tagFlag = "Y";
        }
        else
        {
            tagFlag = "N";
        }
        string inSql = "INSERT INTO REL_USER_TAG (COMPANY_CODE, USER_ID,TAG_CODE,TAG_FLAG) "
                        + "VALUES('" + theCompanyCode + "','" + pUserID.Value.ToString().Trim() + "','" + pTagCode.Value.ToString().Trim() + "','" + tagFlag + "')";
        theDC.ExeSql(inSql);
 

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        queryFunction();
    }
    // 进入修改界面时的处理
    protected void ASPxGridView1_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        
        ASPxComboBox drpUserID = ASPxGridView1.FindEditFormTemplateControl("drpUID") as ASPxComboBox;
        ASPxComboBox drpTagCode = ASPxGridView1.FindEditFormTemplateControl("drpTagCode") as ASPxComboBox;
        string Sql = "SELECT USER_ID,USER_NAME AS SHOWTEXT FROM CODE_USER";
        DataTable dt1 =theDC.GetTable(Sql);

        drpUserID.DataSource = dt1;
        drpUserID.TextField = dt1.Columns[1].ToString();
        drpUserID.ValueField = dt1.Columns[0].ToString();

        Sql = "SELECT TAG_CODE,TAG_NAME AS SHOWTEXT FROM CODE_TAG";
        DataTable dt2 = theDC.GetTable(Sql);

        drpTagCode.DataSource = dt2;
        drpTagCode.TextField = dt2.Columns[1].ToString();
        drpTagCode.ValueField = dt2.Columns[0].ToString();

        
        if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
        {
            //主键不可以修改
            (ASPxGridView1.FindEditFormTemplateControl("drpUID") as ASPxComboBox).Enabled = false;
            (ASPxGridView1.FindEditFormTemplateControl("drpTagCode") as ASPxComboBox).Enabled = false;
            ///处理ASPxCheckBox
            if (ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "TAG_FLAG").ToString() == "Y")
            {
                (ASPxGridView1.FindEditFormTemplateControl("chkTagFlag") as ASPxCheckBox).Checked = true;
            }
            else
            {
                (ASPxGridView1.FindEditFormTemplateControl("chkTagFlag") as ASPxCheckBox).Checked = false;
            }
        }
    }
    // 数据校验
    protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        
        //判断为空
        if (e.NewValues["USER_ID"] == null || e.NewValues["USER_ID"].ToString() == ""  )
        {
            e.RowError = "用户信息不能为空！";
            return;
        }
        if(e.NewValues["TAG_CODE"]==null || e.NewValues["TAG_CODE"].ToString() == "")
        {
            e.RowError = "标签信息不能为空！";
            return;
        }
        
        
        //新增判断键值重复
        if (ASPxGridView1.IsNewRowEditing)
        {
            string SQL = "SELECT * FROM REL_USER_TAG WHERE USER_ID='" + e.NewValues["USER_ID"].ToString() + "' AND TAG_CODE='" + e.NewValues["TAG_CODE"].ToString() + "'";
            if (theDC.GetTable(SQL).Rows.Count > 0)
            {
                e.RowError = "已存在相同的用户标签定义！";
            }
            
        }
    }
    // 删除
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        //判断当前记录是否可以删除
        //string delStr = e.Values["USER_ID"].ToString();
        //dataConn theDataConn = new dataConn("select func_check_delete_data('REL_USER_TAG','" + theCompanyCode + "','MES','MES','MES','" + delStr + "') from dual");

        //theDataConn.OpenConn();
        //string theRet = theDataConn.GetValue();
        //if (theRet != "Y")
        //{
        //    ASPxGridView1.JSProperties.Add("cpCallbackName", "Delete");
        //    ASPxGridView1.JSProperties.Add("cpCompanyName", theRet);
        //    theDataConn.CloseConn();
        //}
       
        //删除操作
        string dSql = "DELETE FROM REL_USER_TAG WHERE COMPANY_CODE='" + theCompanyCode + "' AND USER_ID='" + e.Values["USER_ID"].ToString() + "' AND TAG_CODE='" +
                      e.Values["TAG_CODE"].ToString()+"'";
        theDC.ExeSql(dSql);
      
        e.Cancel = true;
        queryFunction();
    }
    
}
