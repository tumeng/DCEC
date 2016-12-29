using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DevExpress.Web.ASPxEditors;

//以下引用，如果采用实体类，请全部复制到新页面
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.Pub.Data;
using Rmes.Pub.Function;
using Rmes.Web.Base;


/**
 * 功能概述：工序定义
 * 作者：李崇
 * 创建时间：2011-07-26
 */

public partial class Rmes_epd1800 : Rmes.Web.Base.BasePage
{

    public string companyCode;
    public string theUserCode;
    public dataConn theDc = new dataConn();
    public PubCs thePubCs = new PubCs();
    private string theProgramCode;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        companyCode = theUserManager.getCompanyCode();
        theUserCode = theUserManager.getUserId();
        theProgramCode = "epd1800";
        queryFunction();


        string plineSql = "SELECT PLINE_CODE,PLINE_NAME FROM vw_user_role_program WHERE COMPANY_CODE='" + companyCode + "' AND USER_ID='" + theUserCode + "' and program_code='" + theProgramCode + "' order by pline_code";


        SqlDataSource1.SelectCommand = plineSql;
        SqlDataSource1.DataBind();
        
    }
    //初始化GRIDVIEW
    private void queryFunction()
    {

        string sql = "SELECT COMPANY_CODE,PLINE_CODE,PROCESS_CODE,PROCESS_NAME,PLINE_NAME,PROCESS_MANHOUR FROM VW_CODE_PROCESS where COMPANY_CODE='" + companyCode + "'"
             + " AND PLINE_CODE IN (SELECT PLINE_CODE FROM vw_user_role_program WHERE COMPANY_CODE='" + companyCode + "' AND USER_ID='" + theUserCode + "' and program_code='"+theProgramCode+"') ORDER BY PLINE_CODE,PROCESS_CODE";

        DataTable dt = theDc.GetTable(sql);

        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();

    }
    protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        //判断超长
        if (e.NewValues["PROCESS_CODE"].ToString().Length > 30)
        {
            e.RowError = "工序代码字节长度不能超过30！";
        }
        if (e.NewValues["PROCESS_NAME"].ToString().Length > 30)
        {
            e.RowError = "工序名称字节长度不能超过30！";
        }
        //判断为空
        if (e.NewValues["PROCESS_CODE"].ToString() == "" || e.NewValues["PROCESS_CODE"].ToString() == null)
        {
            e.RowError = "工序代码不能为空！";
        }
        if (e.NewValues["PROCESS_NAME"].ToString() == "" || e.NewValues["PROCESS_NAME"].ToString() == null)
        {
            e.RowError = "工序名称不能为空！";
        }
      


        

        //新增判断键值重复
        if (ASPxGridView1.IsNewRowEditing)
        {
            string chSql = "SELECT COMPANY_CODE, PROCESS_CODE, PROCESS_NAME, PLINE_CODE FROM CODE_PROCESS"
                + " WHERE COMPANY_CODE = '" + companyCode + "' and PROCESS_CODE='" + e.NewValues["PROCESS_CODE"].ToString() + "'";
            dataConn dc = new dataConn(chSql);
            if (dc.GetState() == true)
            {
                e.RowError = "已存在相同的工序代码！";
            }
        }
    }

    //protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    //{
    //    ASPxTextBox OCode = ASPxGridView1.FindEditFormTemplateControl("txtOptionCode") as ASPxTextBox;
    //    ASPxTextBox OName = ASPxGridView1.FindEditFormTemplateControl("txtOptionName") as ASPxTextBox;
      
    //    ASPxComboBox PlineCode = ASPxGridView1.FindEditFormTemplateControl("ASPxComboBox1") as ASPxComboBox;





    //    string inSql = "INSERT INTO CODE_PROCESS (COMPANY_CODE, PROCESS_CODE, PROCESS_NAME, PLINE_CODE) "
    //             + "VALUES('" + companyCode + "','" + OCode.Text.Trim() + "','" + OName.Text.Trim() + "','" + PlineCode.SelectedItem.Value.ToString() + "')";
    //    dataConn inDc = new dataConn();
    //    inDc.ExeSql(inSql);
    //    inDc.CloseConn();

    //    e.Cancel = true;
    //    ASPxGridView1.CancelEdit();
    //    queryFunction();
    //}
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxTextBox OCode = ASPxGridView1.FindEditFormTemplateControl("txtOptionCode") as ASPxTextBox;
        ASPxTextBox OName = ASPxGridView1.FindEditFormTemplateControl("txtOptionName") as ASPxTextBox;



        ASPxComboBox PlineCode = ASPxGridView1.FindEditFormTemplateControl("ASPxComboBox1") as ASPxComboBox;



        string upSql = "UPDATE CODE_PROCESS SET PROCESS_NAME='" + OName.Text.Trim() + "'" +
                     "  WHERE  COMPANY_CODE = '" + companyCode + "' and  PROCESS_CODE='" + OCode.Text.Trim() + "' and PLINE_CODE='"+PlineCode.SelectedItem.Value.ToString()+"'";
        theDc.ExeSql(upSql);

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        queryFunction();
    }
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        string dSql = "DELETE FROM CODE_PROCESS WHERE COMPANY_CODE='" + companyCode + "' and PROCESS_CODE='" + e.Values["PROCESS_CODE"].ToString() + "'";
        theDc.ExeSql(dSql);

        e.Cancel = true;
        queryFunction();
    }
    protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
    {
        if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
        {
            //主键不可以修改
            (ASPxGridView1.FindEditFormTemplateControl("txtOptionCode") as ASPxTextBox).Enabled = false;
            //按照之前开发的样子，生产线也不可以修改
            (ASPxGridView1.FindEditFormTemplateControl("ASPxComboBox1") as ASPxComboBox).Enabled = false;
        }
    }
    protected void btnXlsExport_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter1.WriteXlsToResponse("工序信息导出");
    }
}
