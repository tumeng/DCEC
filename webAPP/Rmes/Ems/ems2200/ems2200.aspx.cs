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
 * 功能概述：设备维护管理
 * 版本：1.0
 * 作者：李蒙蒙
 * 创建时间：2013-6-17
 * 

 */

public partial class Rmes_ems2200 : Rmes.Web.Base.BasePage
{
    public static string theCompanyCode, theUserCode, thePrice;
    public dataConn theDc = new dataConn();
    public PubCs thePubPc = new PubCs();
    public DateTime theMntStartDate, theMntEndDate;

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


    }

    protected void ASPxCbSubmit_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        e.Result = "OK";

        string srlno = e.Parameter;

        string strSql = "SELECT * FROM data_asset_detail a,code_asset b WHERE a.asset_code=b.asset_code and a.serial_number= '" + srlno + "'";
        DataTable dt = theDc.GetTable(strSql);


        if (dt.Rows.Count == 0)
        {
            e.Result = "此类设备没有定义！";
            return;
        }
        e.Result = dt.Rows[0]["ASSET_CODE"].ToString();
        e.Result += "," + dt.Rows[0]["ASSET_NAME"].ToString();
        e.Result += "," + dt.Rows[0]["ASSET_MODEL"].ToString();
        e.Result += "," + dt.Rows[0]["ASSET_SPEC"].ToString();
        return;

    }



    //初始化主表
    private void queryFunction()
    {
        string sql = "SELECT a.company_code,a.SERIAL_NUMBER,b.asset_code,c.asset_name,c.asset_spec,c.asset_model,b.response_dept_code,f.dept_name,"
                    + "b.response_person_code,d.user_name,a.maint_type,a.maint_item,e.MAINT_ITEM_NAME,a.maint_service_unit,a.maint_service_person,a.maint_result,a.maint_cost,"
                    + "to_char(a.maint_start_date,'yyyy-mm-dd') maint_start_date,to_char(a.maint_end_date,'yyyy-mm-dd') maint_end_date,a.maint_remark,a.fault_script "

                   + "FROM DATA_ASSET_MAINTAIN a "
                   + "left join data_asset_detail b on a.SERIAL_NUMBER=b.serial_number "
                   + "left join code_asset c on b.company_code=c.company_code and b.asset_code=c.asset_code "
                   + "left join code_user d on b.company_code=c.company_code and b.response_person_code=d.user_id "
                   + "left join rel_asset_mntitem e on e.maint_item_code= a.maint_item "
                   + "left join code_dept f on b.company_code=f.company_code and b.response_dept_code=f.dept_code "

                   + " WHERE A.COMPANY_CODE = '" + theCompanyCode + "' ORDER BY a.maint_start_date DESC";
        DataTable dt = theDc.GetTable(sql);

        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
    }

    // 主表修改
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxTextBox srlno = ASPxGridView1.FindEditFormTemplateControl("txtSrlNo") as ASPxTextBox;

        ASPxGridLookup lkpMntType = ASPxGridView1.FindEditFormTemplateControl("lkpMaintType") as ASPxGridLookup;
        ASPxComboBox txtMntItem = ASPxGridView1.FindEditFormTemplateControl("cmbMntItem") as ASPxComboBox;
        ASPxTextBox txtSrvUnit = ASPxGridView1.FindEditFormTemplateControl("txtSrvUnit") as ASPxTextBox;
        ASPxTextBox txtSrvPerson = ASPxGridView1.FindEditFormTemplateControl("txtSrvPerson") as ASPxTextBox;
        ASPxTextBox txtMntResult = ASPxGridView1.FindEditFormTemplateControl("txtMntResult") as ASPxTextBox;
        ASPxTextBox txtMntCost = ASPxGridView1.FindEditFormTemplateControl("txtMntCost") as ASPxTextBox;


        ASPxDateEdit calMntStartDate = ASPxGridView1.FindEditFormTemplateControl("calMntStartDate") as ASPxDateEdit;
        ASPxDateEdit calMntEndDate = ASPxGridView1.FindEditFormTemplateControl("calMntEndDate") as ASPxDateEdit;

        ASPxMemo mntRemark = ASPxGridView1.FindEditFormTemplateControl("txtRemark") as ASPxMemo;
        ASPxMemo faultScript = ASPxGridView1.FindEditFormTemplateControl("txtFault") as ASPxMemo;


        string mntType = "";
        string mntItem = e.NewValues["MAINT_ITEM"].ToString();


        if (lkpMntType.Value != null) mntType = lkpMntType.Value.ToString();



        string upSql = "UPDATE DATA_ASSET_MAINTAIN SET maint_type='" + mntType + "',maint_item='" + mntItem + "',maint_service_unit='" + txtSrvUnit.Text + "'," +
                       "maint_service_person='" + txtSrvPerson.Text+ "',maint_result='" + txtMntResult.Text + "',maint_cost=" + txtMntCost.Text + "," +
                       "maint_start_date=to_date('" + calMntStartDate.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss'),maint_end_date=to_date('" + 
                       calMntEndDate.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss'),maint_remark='" + mntRemark.Text + "',FAULT_SCRIPT='"+faultScript.Text +
                       "' WHERE  COMPANY_CODE = '" + theCompanyCode + "' AND SERIAL_NUMBER='" + srlno.Text + "'";
        theDc.ExeSql(upSql);



        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        queryFunction();

    }

    // 主表新增
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxTextBox txtSrlNo = ASPxGridView1.FindEditFormTemplateControl("txtSrlNo") as ASPxTextBox;

        ASPxGridLookup lkpMntType = ASPxGridView1.FindEditFormTemplateControl("lkpMaintType") as ASPxGridLookup;
        ASPxComboBox txtMntItem = ASPxGridView1.FindEditFormTemplateControl("cmbMntItem") as ASPxComboBox;
        ASPxTextBox txtSrvUnit = ASPxGridView1.FindEditFormTemplateControl("txtSrvUnit") as ASPxTextBox;
        ASPxTextBox txtSrvPerson = ASPxGridView1.FindEditFormTemplateControl("txtSrvPerson") as ASPxTextBox;
        ASPxTextBox txtMntResult = ASPxGridView1.FindEditFormTemplateControl("txtMntResult") as ASPxTextBox;
        ASPxTextBox txtMntCost = ASPxGridView1.FindEditFormTemplateControl("txtMntCost") as ASPxTextBox;


        ASPxDateEdit calMntStartDate = ASPxGridView1.FindEditFormTemplateControl("calMntStartDate") as ASPxDateEdit;
        ASPxDateEdit calMntEndDate = ASPxGridView1.FindEditFormTemplateControl("calMntEndDate") as ASPxDateEdit;

        ASPxMemo mntRemark = ASPxGridView1.FindEditFormTemplateControl("txtRemark") as ASPxMemo;
        ASPxMemo faultScript = ASPxGridView1.FindEditFormTemplateControl("txtFault") as ASPxMemo;

        string mntType = "";

        string mntItem = e.NewValues["MAINT_ITEM"].ToString();
        //string mntItem = txtMntItem == null ? "" : txtMntItem.SelectedItem.Value.ToString();
        string mntCost = txtMntCost == null ? "0" : txtMntCost.Text;


        if (lkpMntType.Value != null) mntType = lkpMntType.Value.ToString();



        string inSql = "insert into DATA_ASSET_MAINTAIN(company_code,SERIAL_NUMBER,maint_type,maint_item,maint_service_unit,"+
                       "maint_service_person,maint_result,maint_cost,maint_start_date,maint_end_date,maint_remark,fault_script) " +
                       "values('" + theCompanyCode + "','" + txtSrlNo.Text + "','" + mntType + "','" + mntItem + "','" +
                       txtSrvUnit.Text + "','" + txtSrvPerson.Text + "','" + txtMntResult.Text + "'," + mntCost + "," +
                       "to_date('" + calMntStartDate.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss'),to_date('" + 
                       calMntEndDate.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss'),'" + mntRemark.Text + "','"+faultScript.Text+"')";


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
        (ASPxGridView1.FindEditFormTemplateControl("txtAssetCode") as ASPxTextBox).Enabled = false;

        // 进入修改界面时的处理
        if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
        {
            ///不可以修改的字段
            (ASPxGridView1.FindEditFormTemplateControl("txtSrlNo") as ASPxTextBox).Enabled = false;

            ////处理ASPxCheckBox执行
            //if (ASPxGridView1.GetRowValues(ASPxGridView1.EditingRowVisibleIndex, "ACTIVE_FLAG").ToString() == "Y")
            //{
            //    (ASPxGridView1.FindEditFormTemplateControl("chkActiveFlag") as ASPxCheckBox).Checked = true;
            //}
            //else
            //{
            //    (ASPxGridView1.FindEditFormTemplateControl("chkActiveFlag") as ASPxCheckBox).Checked = false;
            //}
            string stime = ASPxGridView1.GetRowValues(ASPxGridView1.EditingRowVisibleIndex, "MAINT_START_DATE").ToString();
            theMntStartDate = Convert.ToDateTime(stime);
            stime = ASPxGridView1.GetRowValues(ASPxGridView1.EditingRowVisibleIndex, "MAINT_END_DATE").ToString();
            theMntEndDate = Convert.ToDateTime(stime);


        }
        //新增
        if (ASPxGridView1.IsNewRowEditing)
        {
            theMntStartDate = DateTime.Today;
            (ASPxGridView1.FindEditFormTemplateControl("txtSrlNo") as ASPxTextBox).Enabled = true;
            (ASPxGridView1.FindEditFormTemplateControl("txtMntCost") as ASPxTextBox).Text = "0";
        }

        SqlMntType.SelectCommand = "SELECT internal_code,internal_name from code_internal where internal_code_father='022' order by internal_code";


    }
    // 数据校验
    protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        string SQL = "";
        DataTable dc = new DataTable();

        if (String.IsNullOrEmpty(e.NewValues["SERIAL_NUMBER"].ToString()))
        {
            e.RowError = "设备代码不能为空";
            return;
        }

        if (ASPxGridView1.IsNewRowEditing)
        {
            SQL = "select * from DATA_ASSET_MAINTAIN where SERIAL_NUMBER='" + e.NewValues["SERIAL_NUMBER"].ToString() + "' and maint_end_date is null";
            if (theDc.GetTable(SQL).Rows.Count > 0)
            {
                e.RowError = "此项编号的设备有未完成的维护计划！";
                return;
            }
        }

    }

    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        //判断当前记录是否可以删除

        string srlno = e.Values["SERIAL_NUMBER"].ToString();
        string SQL = "delete from DATA_ASSET_MAINTAIN where serial_number='" + srlno + "'";
        theDc.ExeSql(SQL);


        e.Cancel = true;
        queryFunction();
    }

    protected void ASPxGridView1_BeforeColumnSortingGrouping(object sender, ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
    {
        queryFunction();
    }


    protected void cmbMntItem_Callback(object source, CallbackEventArgsBase e)
    {
        string[] param = e.Parameter.Split(',');
        string sss1 = param[0];
        string sss2 = param[1];
        SqlMntItem.SelectParameters[0].DefaultValue = param[0];


        ASPxComboBox cmb = (ASPxComboBox)ASPxGridView1.FindEditFormTemplateControl("cmbMntItem");

        cmb.DataBind();

    }


}