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
using DevExpress.Web.ASPxGridLookup;

/**
 * 功能概述：设备台帐维护
 * 版本：1.0
 * 作者：李蒙蒙
 * 创建时间：2013-6-4
 * 

 */

public partial class Rmes_ems2100 : Rmes.Web.Base.BasePage
{
    public static string theCompanyCode, theUserCode, thePrice;
    public dataConn theDc = new dataConn();
    public PubCs thePubPc = new PubCs();
    public DateTime thePurchaseDate, WrtStartDate, WrtEndDate;

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theUserCode = theUserManager.getUserId();

        if (!IsPostBack)
        {
            theCompanyCode = theUserManager.getCompanyCode();

            Session["company"] = theCompanyCode;

            ASPxGridView1.SettingsDetail.AllowOnlyOneMasterRowExpanded = true;

            queryFunction();

            //Session.Remove("billCode");
        }

        theCompanyCode = Session["company"].ToString();

        
        SqlUser.SelectCommand = "SELECT USER_ID,USER_NAME FROM CODE_USER WHERE COMPANY_CODE='" + theCompanyCode + "'";
        SqlDept.SelectCommand = "select dept_code,dept_name from code_dept where company_code='" + theCompanyCode + "'";


   
        
    }

    protected void ASPxCbSubmit_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        e.Result = "OK";

        string assetcode = e.Parameter;
        
        string strSql = "SELECT * FROM code_asset WHERE asset_code= '" + assetcode + "'";
        DataTable dt=theDc.GetTable(strSql);


        if (dt.Rows.Count== 0)
        {
            e.Result = "此类设备没有定义！";
            return;
        }
        e.Result = dt.Rows[0]["ASSET_NAME"].ToString();
        e.Result += "," + dt.Rows[0]["ASSET_MODEL"].ToString();
        e.Result += "," + dt.Rows[0]["ASSET_SPEC"].ToString();
        return;


    }
    


    //初始化主表
    private void queryFunction()
    {
        string sql = "SELECT a.company_code,a.asset_code,b.asset_name,b.asset_spec,b.asset_model,a.serial_number,a.bar_code,a.asset_color,a.vendor_code,d.vendor_name,"
                    + "a.manufacturer_code,e.vendor_name manufacturer_name,to_char(a.purchase_date,'yyyy-mm-dd') purchase_date,a.purchase_cost,"
                    + "to_char(a.warranty_start_date,'yyyy-mm-dd') warranty_start_date,to_char(a.warranty_end_date,'yyyy-mm-dd') warranty_end_date,a.response_dept_code,f.dept_name,"
                    +"a.response_person_code,c.user_name,a.active_flag,decode(a.active_flag,'Y','是','N','否') active_flag_name,a.asset_remark "
                    
                   + "FROM data_asset_detail a "
                   + "left join code_asset b on a.company_code=b.company_code and a.asset_code=b.asset_code "
                   + "left join code_user c on a.company_code=c.company_code and a.response_person_code=c.user_id "
                   + "left join code_vendor d on a.company_code=d.company_code and a.vendor_code=d.vendor_code "
                   + "left join code_vendor e on a.company_code=e.company_code and a.manufacturer_code=e.vendor_code "
                   + "left join code_dept f on a.company_code=f.company_code and a.response_dept_code=f.dept_code "
                   
                   + " WHERE A.COMPANY_CODE = '" + theCompanyCode + "' ORDER BY a.purchase_date DESC";
        DataTable dt = theDc.GetTable(sql);

        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
    }

    // 主表修改
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //ASPxLabel billId = ASPxGridView1.FindEditFormTemplateControl("LabBillId") as ASPxLabel;
        ASPxTextBox assetcode = ASPxGridView1.FindEditFormTemplateControl("txtAssetCode") as ASPxTextBox;
        ASPxTextBox srlno = ASPxGridView1.FindEditFormTemplateControl("txtSrlNo") as ASPxTextBox;

        ASPxGridLookup lkpVendor = ASPxGridView1.FindEditFormTemplateControl("GridLookupVendor") as ASPxGridLookup;
        ASPxGridLookup lkpManuf = ASPxGridView1.FindEditFormTemplateControl("GridLookupManuf") as ASPxGridLookup;
        ASPxGridLookup lkpDept = ASPxGridView1.FindEditFormTemplateControl("GridLookupDept") as ASPxGridLookup;
        ASPxDateEdit dtePurchDate = ASPxGridView1.FindEditFormTemplateControl("CalPCDate") as ASPxDateEdit;
        ASPxTextBox txtPurchCost = ASPxGridView1.FindEditFormTemplateControl("txtPrice") as ASPxTextBox;
        ASPxDateEdit wrtStartDate = ASPxGridView1.FindEditFormTemplateControl("CalWRTStartDate") as ASPxDateEdit;
        ASPxDateEdit wrtEndDate = ASPxGridView1.FindEditFormTemplateControl("CalWRTEndDate") as ASPxDateEdit;
        ASPxTextBox txtAssetColor = ASPxGridView1.FindEditFormTemplateControl("txtColor") as ASPxTextBox;
        ASPxTextBox txtBarCode = ASPxGridView1.FindEditFormTemplateControl("txtBarCode") as ASPxTextBox;
        ASPxGridLookup lkpRspPerson = ASPxGridView1.FindEditFormTemplateControl("lkpResponsePerson") as ASPxGridLookup;
        ASPxCheckBox chkActiveFlag = ASPxGridView1.FindEditFormTemplateControl("chkActiveFlag") as ASPxCheckBox;
        ASPxMemo assetRemark = ASPxGridView1.FindEditFormTemplateControl("txtRemark") as ASPxMemo;
        

        string deptCode = "",vendorCode="",manufCode="",rspPerson="";
     


        ASPxGridLookup gridLookupVendor = ASPxGridView1.FindEditFormTemplateControl("GridLookupVendor") as ASPxGridLookup;
        ASPxTextBox txtSVendor = ASPxGridView1.FindEditFormTemplateControl("txtSVendor") as ASPxTextBox;
        if (lkpDept.Value != null) deptCode = lkpDept.Value.ToString();
        if (lkpManuf.Value != null) manufCode = lkpManuf.Value.ToString();
        if (lkpVendor.Value != null) vendorCode = lkpVendor.Value.ToString();
        if (lkpRspPerson.Value != null) rspPerson = lkpRspPerson.Value.ToString();
        string activeflag=chkActiveFlag.Checked ? "Y":"N";


        string upSql = "UPDATE data_asset_detail SET vendor_code='" + vendorCode + "',manufacturer_code='" + lkpManuf.Value + "'," +
                       "response_dept_code='" + deptCode + "',purchase_date=to_date('" + dtePurchDate.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss'),purchase_cost=" + txtPurchCost.Text + "," +
                       "warranty_start_date=to_date('"+wrtStartDate.Value.ToString()+"','yyyy-mm-dd hh24:mi:ss'),warranty_end_date=to_date('"+wrtEndDate.Value.ToString()+"','yyyy-mm-dd hh24:mi:ss'),"+
                       "response_person_code='" + lkpRspPerson.Value.ToString() + "',asset_color='" + txtAssetColor.Text+"',asset_remark='"+assetRemark.Text+"',active_flag='"+activeflag+"' "+
                       " WHERE  COMPANY_CODE = '" + theCompanyCode + "' AND serial_number='" + srlno.Text + "'";
        theDc.ExeSql(upSql);

        
        
        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        queryFunction();

    }

    // 主表新增
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        //ASPxLabel billId = ASPxGridView1.FindEditFormTemplateControl("LabBillId") as ASPxLabel;
        ASPxTextBox txtAssetCode = ASPxGridView1.FindEditFormTemplateControl("txtAssetCode") as ASPxTextBox;
        ASPxTextBox srlno = ASPxGridView1.FindEditFormTemplateControl("txtSrlNo") as ASPxTextBox;

        ASPxGridLookup lkpVendor = ASPxGridView1.FindEditFormTemplateControl("GridLookupVendor") as ASPxGridLookup;
        ASPxGridLookup lkpManuf = ASPxGridView1.FindEditFormTemplateControl("GridLookupManuf") as ASPxGridLookup;
        ASPxGridLookup lkpDept = ASPxGridView1.FindEditFormTemplateControl("GridLookupDept") as ASPxGridLookup;
        ASPxDateEdit dtePurchDate = ASPxGridView1.FindEditFormTemplateControl("CalPCDate") as ASPxDateEdit;
        ASPxTextBox txtPurchCost = ASPxGridView1.FindEditFormTemplateControl("txtPrice") as ASPxTextBox;
        ASPxDateEdit wrtStartDate = ASPxGridView1.FindEditFormTemplateControl("CalWRTStartDate") as ASPxDateEdit;
        ASPxDateEdit wrtEndDate = ASPxGridView1.FindEditFormTemplateControl("CalWRTEndDate") as ASPxDateEdit;
        ASPxTextBox txtAssetColor = ASPxGridView1.FindEditFormTemplateControl("txtColor") as ASPxTextBox;
        ASPxTextBox txtBarCode = ASPxGridView1.FindEditFormTemplateControl("txtBarCode") as ASPxTextBox;
        ASPxGridLookup lkpRspPerson = ASPxGridView1.FindEditFormTemplateControl("lkpResponsePerson") as ASPxGridLookup;
        ASPxCheckBox chkActiveFlag = ASPxGridView1.FindEditFormTemplateControl("chkActiveFlag") as ASPxCheckBox;
        ASPxMemo assetRemark = ASPxGridView1.FindEditFormTemplateControl("txtRemark") as ASPxMemo;


        string deptCode = "", vendorCode = "", manufCode = "", rspPerson = "";

        ASPxGridLookup gridLookupVendor = ASPxGridView1.FindEditFormTemplateControl("GridLookupVendor") as ASPxGridLookup;
        ASPxTextBox txtSVendor = ASPxGridView1.FindEditFormTemplateControl("txtSVendor") as ASPxTextBox;
        if (lkpDept.Value != null) deptCode = lkpDept.Value.ToString();
        if (lkpManuf.Value != null) manufCode = lkpManuf.Value.ToString();
        if (lkpVendor.Value != null) vendorCode = lkpVendor.Value.ToString();
        if (lkpRspPerson.Value != null) rspPerson = lkpRspPerson.Value.ToString();

        string activeflag=chkActiveFlag.Checked ? "Y" :"N";


        string inSql = "insert into data_asset_detail(company_code,asset_code,serial_number,vendor_code,manufacturer_code,response_dept_code,response_person_code,purchase_date," +
                       "purchase_cost,warranty_start_date,warranty_end_date,asset_color,bar_code,active_flag,asset_remark) " +
                       "values('" + theCompanyCode + "','" + txtAssetCode.Text + "','" + srlno.Text + "','" + vendorCode + "','" + manufCode + "','" + deptCode + "','" + rspPerson + "',to_date('" +
                       dtePurchDate.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss'),'" + txtPurchCost.Text + "',to_date('" + wrtStartDate.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss')," +
                       "to_date('" + wrtEndDate.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss'),'" + txtAssetColor.Text + "','" + txtBarCode.Text + "','" + activeflag + "','" + assetRemark.Text + "')";


        dataConn inDc = new dataConn();
        inDc.ExeSql(inSql);


        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        queryFunction();
    }

    protected void ASPxGridView1_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {


        //(ASPxGridView1.FindEditFormTemplateControl("txtCorBillId") as ASPxTextBox).Attributes.Add("onkeypress", "checkEnterKey(event);");
        (ASPxGridView1.FindEditFormTemplateControl("txtAssetName") as ASPxTextBox).Enabled = false;
        (ASPxGridView1.FindEditFormTemplateControl("txtAssetSpec") as ASPxTextBox).Enabled = false;
        (ASPxGridView1.FindEditFormTemplateControl("txtAssetModel") as ASPxTextBox).Enabled = false;

        // 进入修改界面时的处理
        if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
        {
            ///不可以修改的字段
            (ASPxGridView1.FindEditFormTemplateControl("txtAssetCode") as ASPxTextBox).Enabled = false;
            
            //处理ASPxCheckBox执行
            if (ASPxGridView1.GetRowValues(ASPxGridView1.EditingRowVisibleIndex, "ACTIVE_FLAG").ToString() == "Y")
            {
                (ASPxGridView1.FindEditFormTemplateControl("chkActiveFlag") as ASPxCheckBox).Checked = true;
            }
            else
            {
                (ASPxGridView1.FindEditFormTemplateControl("chkActiveFlag") as ASPxCheckBox).Checked = false;
            }
            string stime=ASPxGridView1.GetRowValues(ASPxGridView1.EditingRowVisibleIndex, "PURCHASE_DATE").ToString();
            thePurchaseDate = Convert.ToDateTime(stime);
            stime = ASPxGridView1.GetRowValues(ASPxGridView1.EditingRowVisibleIndex, "WARRANTY_START_DATE").ToString();
            WrtStartDate = Convert.ToDateTime(stime);
            stime = ASPxGridView1.GetRowValues(ASPxGridView1.EditingRowVisibleIndex, "WARRANTY_END_DATE").ToString();
            WrtEndDate = Convert.ToDateTime(stime);


        }
        //新增
        if (ASPxGridView1.IsNewRowEditing)
        {
            thePurchaseDate = DateTime.Today;
            WrtStartDate = DateTime.Today;
            WrtEndDate = DateTime.Today; 

        }

        SqlVendor.SelectCommand = "SELECT vendor_code,vendor_name from code_vendor where type='A' order by vendor_code";
        SqlManuf.SelectCommand = "SELECT vendor_code,vendor_name from code_vendor where type='C' order by vendor_code";
        SqlDept.SelectCommand="select dept_code,dept_name from code_dept where company_code='"+theCompanyCode+"'";
        SqlUser.SelectCommand="select user_id,user_name from code_user where company_code='"+theCompanyCode+"'";

        

    }
    // 数据校验
    protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        string SQL = "";
        DataTable dc = new DataTable();

        if (String.IsNullOrEmpty(e.NewValues["ASSET_CODE"].ToString()))
        {
            e.RowError = "设备代码不能为空";
            return;
        }

        if (ASPxGridView1.IsNewRowEditing)
        {
            SQL = "select * from data_asset_detail where asset_code='" + e.NewValues["ASSET_CODE"].ToString() + "' and serial_number='"+e.NewValues["SERIAL_NUMBER"].ToString()+"'";
            if (theDc.GetTable(SQL).Rows.Count > 0)
            {
                e.RowError = "此项编号的设备台账已经存在！";
                return;
            }
        }

        
    }
    // 主表删除
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        //判断当前记录是否可以删除

        string srlno = e.Values["SERIAL_NUMBER"].ToString();
        string SQL = "delete from data_asset_detail where serial_number='"+srlno+"'";
        theDc.ExeSql(SQL);

        
        e.Cancel = true;
        queryFunction();
    }

    protected void ASPxGridView1_BeforeColumnSortingGrouping(object sender, ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
    {
        queryFunction();
    }
    


    protected void cmbAccountType_Init(object sender, EventArgs e)
    {
        //(sender as ASPxComboBox).Value = "A";
        //(sender as ASPxComboBox).Items[0].Selected = true;
    }
}
