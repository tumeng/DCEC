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
 * 功能概述：SO状态修改
 * 作者：游晓航
 * 创建时间：2016-07-29
 * 修改时间：
 */



public partial class Rmes_atpu2800 : Rmes.Web.Base.BasePage
{
    private dataConn dc = new dataConn();
    private PubCs pc = new PubCs();
    public string theCompanyCode, theUserId, theUserCode;

    public DateTime OracleEntityTime, OracleSQLTime, theTime;

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theTime = DateTime.Now;
        theUserId = theUserManager.getUserName();
        theUserCode = theUserManager.getUserCode();

        setCondition();
    }

    private void setCondition()
    {
        //初始化GRIDVIEW
        string sql = "SELECT * FROM COPY_PT_MSTR where upper(pt_part)='" + txtPlan.Text.Trim().ToUpper() + "'";
        DataTable dt = dc.GetTable(sql);

        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
    }

    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {

        ASPxTextBox uPart = ASPxGridView1.FindEditFormTemplateControl("TextPart") as ASPxTextBox;
        ASPxComboBox uStatus = ASPxGridView1.FindEditFormTemplateControl("TextStatus") as ASPxComboBox;

        string strStatus = uStatus.Value.ToString();

        //插入到日志表
        try
        {
            string Sql2 = "INSERT INTO COPY_PT_MSTR_LOG (pt_part,pt_desc1,pt_desc2,pt_group,pt_part_type,pt_status,pt_phantom,pt_article,pt_prod_line,pt_config,user_code,flag,rqsj)"
                + " SELECT pt_part,pt_desc1,pt_desc2,pt_group,pt_part_type,pt_status,pt_phantom,pt_article,pt_prod_line,pt_config,'" + theUserCode + "' , 'BEFOREEDIT', SYSDATE FROM COPY_PT_MSTR WHERE PT_PART = '" + uPart.Text.Trim() + "'";
            dc.ExeSql(Sql2);
        }
        catch
        {
            return;
        }

        string Sql = "UPDATE COPY_PT_MSTR SET PT_STATUS='" + strStatus + "'  "
             + " WHERE   PT_PART = '" + uPart.Text.Trim() + "'";
        dc.ExeSql(Sql);

        //插入到日志表
        try
        {
            string Sql2 = "INSERT INTO COPY_PT_MSTR_LOG (pt_part,pt_desc1,pt_desc2,pt_group,pt_part_type,pt_status,pt_phantom,pt_article,pt_prod_line,pt_config,user_code,flag,rqsj)"
                + " SELECT pt_part,pt_desc1,pt_desc2,pt_group,pt_part_type,pt_status,pt_phantom,pt_article,pt_prod_line,pt_config,'" + theUserCode + "' , 'AFTEREDIT', SYSDATE FROM COPY_PT_MSTR WHERE PT_PART = '" + uPart.Text.Trim() + "'";
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
    protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
    {
        if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
        {
            ///主键不可以修改
            (ASPxGridView1.FindEditFormTemplateControl("TextPart") as ASPxTextBox).Enabled = false;
        }
        else
        {
            //uTcode.SelectedIndex = 0;
        }
    }

    protected void btnCx1_Click(object sender, EventArgs e)
    {

    }

    

}
