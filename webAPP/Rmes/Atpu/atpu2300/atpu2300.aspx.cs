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
 * 功能概述：维护FR组对应的程序号
 * 作者：游晓航
 * 创建时间：2016-07-12
 * 修改时间：
 */



public partial class Rmes_atpu2300 : Rmes.Web.Base.BasePage
{
    private dataConn dc = new dataConn();
    private PubCs pc = new PubCs();
    public string theCompanyCode, theUserCode, theUserId;

    public DateTime OracleEntityTime, OracleSQLTime;

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theUserId = theUserManager.getUserId();
        theUserCode = theUserManager.getUserCode();

        if (!IsPostBack)
        {
            //OracleEntityTime = DB.GetServerTime();

            OracleSQLTime = DB.GetInstance().ExecuteScalar<DateTime>("Select sysdate from dual");
        }
        setCondition();
    }

    private void setCondition()
    {
        //初始化GRIDVIEW
        string sql = "SELECT * FROM ATPUFRCXH order by INPUT_TIME desc nulls last";
        DataTable dt = dc.GetTable(sql);

        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
    }

    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        //判断当前记录是否可以删除
        string strDelCode = e.Values["FR"].ToString();

        string strTableName = "ATPUFRCXH";

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
            //插入到日志表
            try
            {
                string Sql2 = "INSERT INTO ATPUFRCXH_LOG (fr,cxh,user_code,flag,rqsj,sat)"
                    + " SELECT fr,cxh,'" + theUserCode + "' , 'DEL', SYSDATE,sat FROM ATPUFRCXH WHERE FR = '" + strDelCode + "' ";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }
            //确认删除
            string Sql = "delete from ATPUFRCXH WHERE   FR = '" + strDelCode + "'";
            dc.ExeSql(Sql);
        }


        setCondition();
        e.Cancel = true;
    }


    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        
        ASPxTextBox uCXH = ASPxGridView1.FindEditFormTemplateControl("txtCXH") as ASPxTextBox;
        ASPxTextBox txtFR = ASPxGridView1.FindEditFormTemplateControl("txtFR") as ASPxTextBox;
        ASPxTextBox txtsat = ASPxGridView1.FindEditFormTemplateControl("txtSAT") as ASPxTextBox;
        string strFR = txtFR.Text.Trim();

        string Sql = "INSERT INTO ATPUFRCXH (FR,CXH,INPUT_PERSON,INPUT_TIME,sat) "
             + "VALUES( '" + strFR.ToUpper() + "', '" + uCXH.Text.Trim() + "','" + theUserId + "',sysdate,'"+txtsat.Text.Trim()+"')";
        dc.ExeSql(Sql);
        //插入到日志表
        try
        {
            string Sql2 = "INSERT INTO ATPUFRCXH_LOG (fr,cxh,user_code,flag,rqsj,sat)"
                + " SELECT fr,cxh,'" + theUserCode + "' , 'ADD', SYSDATE,sat FROM ATPUFRCXH WHERE FR = '" + strFR.ToUpper() + "' ";
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

    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxTextBox uCXH = ASPxGridView1.FindEditFormTemplateControl("txtCXH") as ASPxTextBox;
        ASPxTextBox txtFR = ASPxGridView1.FindEditFormTemplateControl("txtFR") as ASPxTextBox;
        ASPxTextBox txtsat = ASPxGridView1.FindEditFormTemplateControl("txtSAT") as ASPxTextBox;
        string strFR = txtFR.Text.Trim();

        string Sql = "UPDATE ATPUFRCXH SET CXH='" + uCXH.Text.Trim() + "',sat='" + txtsat.Text.Trim() + "'"
             + " WHERE   FR = '" + strFR + "'";
        dc.ExeSql(Sql);
        
        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        setCondition();

    }
    protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
    {
        if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
        {
            ///主键不可以修改
            (ASPxGridView1.FindEditFormTemplateControl("txtFR") as ASPxComboBox).Enabled = false;
        }
        else
        {
            //uTcode.SelectedIndex = 0;
        }
    }

    protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        //判断是否重复
        if (ASPxGridView1.IsNewRowEditing)
        {
            string chSql = "SELECT FR FROM ATPUFRCXH"
                + " WHERE FR='" + e.NewValues["FR"].ToString() + "'";
            DataTable dt = dc.GetTable(chSql);
            if (dt.Rows.Count > 0)
            {
                e.RowError = "该FR对应的记录已经存在！请重新维护";
            }
            string chSql2 = "select pt_part from copy_pt_mstr"
                + " WHERE pt_part='" + e.NewValues["FR"].ToString() + "'";
            DataTable dt2 = dc.GetTable(chSql2);
            if (dt2.Rows.Count == 0)
            {
                e.RowError = "FR数据非法！请重新维护";
            }

        }
        if (e.NewValues["FR"].ToString().Length > 50)
        {
            e.RowError = "FR字节长度不能超过50！";
        }

        if (e.NewValues["CXH"].ToString().Length > 50)
        {
            e.RowError = "程序号字节长度不能超过50！";
        }
    }
}
