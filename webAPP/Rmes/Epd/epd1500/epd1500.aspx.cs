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

public partial class Rmes_epd1500 : Rmes.Web.Base.BasePage
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
        theProgramCode = "epd1500";
        queryFunction();

        string plineSql = "SELECT a.rmes_id,a.PLINE_NAME FROM CODE_PRODUCT_LINE a where pline_code in (select pline_code from vw_user_role_program where user_id='" + theUserCode + "' and program_code='" + theProgramCode + "' and company_code='" + companyCode + "') "
        
            + " order by a.pline_code";


        SqlDataSource1.SelectCommand = plineSql;
        SqlDataSource1.DataBind();
        
        
        
 
    }
    //初始化GRIDVIEW
    private void queryFunction()
    {

        string sql = "SELECT COMPANY_CODE,PLINE_CODE,STATION_SPECIAL_CODE,STATION_SPECIAL_NAME,STATION_CODE,PLINE_NAME,STATION_NAME FROM VW_CODE_STATION_SPECIAL where COMPANY_CODE='" + companyCode + "'"
             + " AND PLINE_CODE IN (SELECT a.rmes_id FROM code_product_line a left join vw_user_role_program b on b.pline_code= a.pline_code WHERE b.COMPANY_CODE='" + companyCode + "' AND b.USER_ID='" + theUserCode + "' and b.program_code='"+theProgramCode+"') ORDER BY PLINE_CODE,STATION_SPECIAL_CODE";
        
        DataTable dt = theDc.GetTable(sql);

        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
        
    }
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxTextBox SCode = ASPxGridView1.FindEditFormTemplateControl("txtSpecialCode") as ASPxTextBox;
        ASPxTextBox SName = ASPxGridView1.FindEditFormTemplateControl("txtSpecialName") as ASPxTextBox;
       

        ASPxComboBox PlineCode = ASPxGridView1.FindEditFormTemplateControl("PlineCombo") as ASPxComboBox;

        ASPxComboBox StationCode = ASPxGridView1.FindEditFormTemplateControl("StationCombo") as ASPxComboBox;

        string upSql = "UPDATE CODE_STATION_SPECIAL SET STATION_SPECIAL_NAME='" + SName.Text.Trim() + "'," +
                     "STATION_CODE='" + StationCode.SelectedItem.Value.ToString() + "' WHERE  COMPANY_CODE = '" + companyCode + "'  and PLINE_CODE='" + PlineCode.SelectedItem.Value.ToString() + "'"+
                     " and STATION_SPECIAL_CODE='"+SCode.Text.Trim()+"'";
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


        //string aa = e.NewValues["STATION_SPECIAL_CODE"].ToString();
        //string bb = e.NewValues["STATION_SPECIAL_NAME"].ToString();


        //string cc = e.NewValues["PLINE_CODE"].ToString();


        ASPxComboBox PlineCode = ASPxGridView1.FindEditFormTemplateControl("PlineCombo") as ASPxComboBox;

        if (e.NewValues["STATION_SPECIAL_CODE"].ToString().Length > 30)
        {
            e.RowError = "特殊站点代码字节长度不能超过30！";
        }
        if (e.NewValues["STATION_SPECIAL_NAME"].ToString().Length > 30)
        {
            e.RowError = "特殊站点名称字节长度不能超过30！";
        }
        //判断为空
        if (e.NewValues["STATION_SPECIAL_CODE"].ToString() == "" || e.NewValues["STATION_SPECIAL_CODE"].ToString() == null)
        {
            e.RowError = "特殊站点代码不能为空！";
        }
        if (e.NewValues["STATION_SPECIAL_NAME"].ToString() == "" || e.NewValues["STATION_SPECIAL_NAME"].ToString() == null)
        {
            e.RowError = "特殊站点名称不能为空！";
        }
       
        
     

        //新增判断键值重复
        if (ASPxGridView1.IsNewRowEditing)
        {
            string chSql = "SELECT COMPANY_CODE, STATION_SPECIAL_CODE, STATION_SPECIAL_NAME, STATION_CODE," +
            "PLINE_CODE FROM CODE_STATION_SPECIAL"
                + " WHERE COMPANY_CODE = '" + companyCode + "' and STATION_SPECIAL_CODE='" + e.NewValues["STATION_SPECIAL_CODE"].ToString() + "'"+
                " and PLINE_CODE='" + PlineCode.Value.ToString() + "'";
            dataConn dc = new dataConn(chSql);
            if (dc.GetState() == true)
            {
                e.RowError = "已存在相同的特殊站点代码，请确认后重新输入！";
            }
        }


      
        FillStationCodeCombo(PlineCode.Value.ToString());
       
    }
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxTextBox SCode = ASPxGridView1.FindEditFormTemplateControl("txtSpecialCode") as ASPxTextBox;
        ASPxTextBox SName = ASPxGridView1.FindEditFormTemplateControl("txtSpecialName") as ASPxTextBox;
        ASPxComboBox PlineCode = ASPxGridView1.FindEditFormTemplateControl("PlineCombo") as ASPxComboBox;
        ASPxComboBox StationCode = ASPxGridView1.FindEditFormTemplateControl("StationCombo") as ASPxComboBox;





        string inSql = "INSERT INTO CODE_STATION_SPECIAL (COMPANY_CODE, STATION_SPECIAL_CODE, STATION_SPECIAL_NAME, PLINE_CODE, STATION_CODE) "
                 + "VALUES('" + companyCode + "','" + SCode.Text.Trim() + "','" + SName.Text.Trim() + "','" + PlineCode.SelectedItem.Value.ToString() + "','" + StationCode.Value.ToString() + "')";
        dataConn inDc = new dataConn();
        inDc.ExeSql(inSql);
        inDc.CloseConn();

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        queryFunction();
    }
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        string dSql = "DELETE FROM CODE_STATION_SPECIAL WHERE COMPANY_CODE='" + companyCode + "' and STATION_SPECIAL_CODE='" + e.Values["STATION_SPECIAL_CODE"].ToString() + "' and PLINE_CODE='"+e.Values["PLINE_CODE"].ToString()+"'";
        theDc.ExeSql(dSql);

        e.Cancel = true;
        queryFunction();
    }

    protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
    {
        if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
        {
           

            ASPxComboBox PlineCode = ASPxGridView1.FindEditFormTemplateControl("PlineCombo") as ASPxComboBox;
            ASPxComboBox StationCode = ASPxGridView1.FindEditFormTemplateControl("StationCombo") as ASPxComboBox;
            
             //主键不可以修改
            (ASPxGridView1.FindEditFormTemplateControl("txtSpecialCode") as ASPxTextBox).Enabled = false;
            PlineCode.Enabled = false;

            
            
            string SelectPlineCode = this.ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "PLINE_CODE").ToString();
            string SelectStationCode = this.ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "STATION_CODE").ToString();
           
            PlineCode.Value = SelectPlineCode;
            StationCode.Value = SelectStationCode;


            FillStationCodeCombo(PlineCode.Value.ToString());


        }
    }
    protected void ASPxComboBox1_Init(object sender, EventArgs e)
    {
        
    }
    protected void StationCombo_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        FillStationCodeCombo(e.Parameter);
    }
    
    protected void FillStationCodeCombo(string Pline)
    {
        if (string.IsNullOrEmpty(Pline)) return;

        userManager theUserManager = (userManager)Session["theUserManager"];
        companyCode = theUserManager.getCompanyCode();



        ASPxComboBox StationCode = ASPxGridView1.FindEditFormTemplateControl("StationCombo") as ASPxComboBox;
      


        string sql = "select station_code,station_name from code_station where company_code='" + companyCode + "' and pline_code='" + Pline + "'";
        SqlDataSource2.SelectCommand = sql;
        SqlDataSource2.DataBind();

        StationCode.DataBind();



    }

}
