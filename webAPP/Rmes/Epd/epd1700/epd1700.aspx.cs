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
using Rmes.Pub.Data;
using Rmes.Pub.Function;
using DevExpress.Web.ASPxEditors;




/**
 * 功能概述：工位定义
 * 作者：李崇
 * 创建时间：2011-07-26
 */

public partial class Rmes_epd1700 : Rmes.Web.Base.BasePage
{

    public string companyCode;
    public string theUserCode;
    public dataConn theDc = new dataConn();
    public PubCs thePubCs = new PubCs();
    public string theProgramCode;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        companyCode = theUserManager.getCompanyCode();
        theUserCode = theUserManager.getUserId();
        theProgramCode = "epd1700";
        queryFunction();

        //string plineSql = "SELECT PLINE_CODE,PLINE_NAME FROM vw_rel_user_pline WHERE COMPANY_CODE='" + companyCode + "' AND USER_ID='" + theUserCode + "' order by pline_code";

        string plineSql = "SELECT DISTINCT PLINE_CODE,PLINE_NAME FROM vw_user_role_program WHERE COMPANY_CODE='" + companyCode + "' and user_id='" + theUserCode + "' and program_code='" + theProgramCode + "' order by pline_code";

        SqlDataSource1.SelectCommand = plineSql;
        SqlDataSource1.DataBind();

    }
    //初始化GRIDVIEW
    private void queryFunction()
    {
        //string sql = "SELECT COMPANY_CODE,PLINE_CODE,LOCATION_CODE,LOCATION_NAME,LOCATION_SEQ,LOCATION_MANHOUR,PLINE_NAME FROM VW_CODE_LOCATION where COMPANY_CODE='" + companyCode + "'"
        //     + " AND PLINE_CODE IN (SELECT PLINE_CODE FROM REL_USER_PLINE WHERE COMPANY_CODE='" + companyCode + "' AND USER_ID='" + theUserCode + "') ORDER BY PLINE_CODE,LOCATION_CODE";
        
        string sql = "SELECT COMPANY_CODE,PLINE_CODE,LOCATION_CODE,LOCATION_NAME,LOCATION_SEQ,LOCATION_MANHOUR,PLINE_NAME FROM VW_CODE_LOCATION "
             + "where COMPANY_CODE='" + companyCode + "' and pline_code in (select pline_CODE from vw_user_role_program where user_id='" + theUserCode + "' and program_code='" + theProgramCode + "' and company_code='" + companyCode + "') ORDER BY PLINE_CODE,LOCATION_CODE";
        
        DataTable dt = theDc.GetTable(sql);

        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
        
    }
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxTextBox LCode = ASPxGridView1.FindEditFormTemplateControl("txtLocationCode") as ASPxTextBox;
        ASPxTextBox LName = ASPxGridView1.FindEditFormTemplateControl("txtLocationName") as ASPxTextBox;
        ASPxTextBox LOrder = ASPxGridView1.FindEditFormTemplateControl("txtLocationOrder") as ASPxTextBox;
        ASPxTextBox LManhour = ASPxGridView1.FindEditFormTemplateControl("txtLocationManhour") as ASPxTextBox;


        ASPxComboBox PlineCode = ASPxGridView1.FindEditFormTemplateControl("ASPxComboBox1") as ASPxComboBox;



        string upSql = "UPDATE CODE_LOCATION SET LOCATION_NAME='" + LName.Text.Trim() + "',LOCATION_SEQ='" + LOrder.Text.Trim() + "'," +
                     "LOCATION_MANHOUR='" + LManhour.Text.Trim() + "' WHERE  COMPANY_CODE = '" + companyCode + "' and  LOCATION_CODE='"+LCode.Text.Trim()+"' and PLINE_CODE='"+PlineCode.SelectedItem.Value.ToString()+"'";
        theDc.ExeSql(upSql);

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        queryFunction();
    }
    protected void ASPxComboBox1_Load(object sender, EventArgs e)
    {
       
      
    }
    protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        //判断超长
        if (e.NewValues["LOCATION_CODE"].ToString().Length > 30)
        {
            e.RowError = "工位代码字节长度不能超过30！";
        }
        if (e.NewValues["LOCATION_NAME"].ToString().Length > 30)
        {
            e.RowError = "工位名称字节长度不能超过30！";
        }
        //判断为空
        if (e.NewValues["LOCATION_CODE"].ToString() == "" || e.NewValues["LOCATION_CODE"].ToString() == null)
        {
            e.RowError = "工位代码不能为空！";
        }
        if (e.NewValues["LOCATION_NAME"].ToString() == "" || e.NewValues["LOCATION_NAME"].ToString() == null)
        {
            e.RowError = "工位名称不能为空！";
        }
        if (e.NewValues["LOCATION_SEQ"].ToString() == "" || e.NewValues["LOCATION_SEQ"].ToString() == null)
        {
            e.RowError = "工位顺序不能为空！";
        }
        if (e.NewValues["LOCATION_MANHOUR"].ToString() == "" || e.NewValues["LOCATION_MANHOUR"].ToString() == null)
        {
            e.RowError = "工时不能为空！";
        }
        
        
        //判断是否为数字
        
        //工位顺序要求为整数
        if (thePubCs.IsNumeric(e.NewValues["LOCATION_SEQ"].ToString()) == false)
        {
            e.RowError = "工位顺序必须为整数！";
        }

        //工时要求为整数
        if (thePubCs.IsValidNumber(e.NewValues["LOCATION_MANHOUR"].ToString()) == false)
        {
            e.RowError = "工时必须为数字！";
        }

        //新增判断键值重复
        if (ASPxGridView1.IsNewRowEditing)
        {
            string chSql = "SELECT COMPANY_CODE, LOCATION_CODE, LOCATION_NAME, LOCATION_SEQ," +
            "LOCATION_MANHOUR,PLINE_CODE FROM CODE_LOCATION"
                + " WHERE COMPANY_CODE = '" + companyCode + "' and LOCATION_CODE='"+ e.NewValues["LOCATION_CODE"].ToString() + "'";
            dataConn dc = new dataConn(chSql);
            if (dc.GetState() == true)
            {
                e.RowError = "已存在相同的工位代码！";
            }
        }
       
    }
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxTextBox LCode = ASPxGridView1.FindEditFormTemplateControl("txtLocationCode") as ASPxTextBox;
        ASPxTextBox LName = ASPxGridView1.FindEditFormTemplateControl("txtLocationName") as ASPxTextBox;
        ASPxTextBox LOrder = ASPxGridView1.FindEditFormTemplateControl("txtLocationOrder") as ASPxTextBox;
        ASPxTextBox LManhour = ASPxGridView1.FindEditFormTemplateControl("txtLocationManhour") as ASPxTextBox;
        ASPxComboBox PlineCode = ASPxGridView1.FindEditFormTemplateControl("ASPxComboBox1") as ASPxComboBox;





        string inSql = "INSERT INTO CODE_LOCATION (RMES_ID, COMPANY_CODE, LOCATION_CODE, LOCATION_NAME, PLINE_CODE, LOCATION_SEQ, LOCATION_MANHOUR) "
                 + "VALUES(SEQ_RMES_ID.nextval, '" + companyCode + "','" + LCode.Text.Trim() + "','" + LName.Text.Trim() + "','" + PlineCode.SelectedItem.Value.ToString() + "','" + LOrder.Text.Trim() + "','" + LManhour.Text.Trim() + "')";
        dataConn inDc = new dataConn();
        inDc.ExeSql(inSql);
        inDc.CloseConn();

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        queryFunction();
    }
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        string dSql = "DELETE FROM CODE_LOCATION WHERE COMPANY_CODE='" + companyCode + "' and LOCATION_CODE='" + e.Values["LOCATION_CODE"].ToString() + "'";
        theDc.ExeSql(dSql);

        e.Cancel = true;
        queryFunction();
    }

    protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
    {
        if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
        {
            //主键不可以修改
            (ASPxGridView1.FindEditFormTemplateControl("txtLocationCode") as ASPxTextBox).Enabled = false;
            //按照之前开发的样子，生产线也不可以修改
            (ASPxGridView1.FindEditFormTemplateControl("ASPxComboBox1") as ASPxComboBox).Enabled = false;
        }
    }
    protected void ASPxComboBox1_Init(object sender, EventArgs e)
    {
        
    }
    protected void btnXlsExport_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter1.WriteXlsToResponse("工位信息导出");
    }
}
