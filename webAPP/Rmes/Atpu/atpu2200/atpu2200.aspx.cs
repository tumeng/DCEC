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
 * 功能概述：客户条码打印维护
 * 作者：游晓航
 * 创建时间：2016-07-12
 * 修改时间：
 */



public partial class Rmes_atpu2200 : Rmes.Web.Base.BasePage
{
    private dataConn dc = new dataConn();
    private PubCs pc = new PubCs();
    public string theCompanyCode, theUserCode,theUserId;

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
        string sql = "SELECT * FROM ATPUKHTM order by INPUT_TIME desc nulls last";
        DataTable dt = dc.GetTable(sql);

        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
    }

    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {

        //判断当前记录是否可以删除
        string strDelCode = e.Values["SO"].ToString();
        
        string strTableName = "ATPUKHTM";

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
                string Sql2 = "INSERT INTO ATPUKHTM_LOG (khmc,khh,wlh,gysdm,sn,so,bbh,user_code,flag,rqsj)"
                    + " SELECT khmc,khh,wlh,gysdm,sn,so,bbh,'" + theUserCode + "' , 'DEL', SYSDATE FROM ATPUKHTM WHERE SO = '" + strDelCode + "' AND KHMC = '" + e.Values["KHMC"] + "' ";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }
            //确认删除
            string Sql = "delete from ATPUKHTM WHERE   SO = '" + strDelCode + "' and KHMC = '" + e.Values["KHMC"] + "'";
            dc.ExeSql(Sql);
        }


        setCondition();
        e.Cancel = true;
    }


    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxTextBox uKHMC = ASPxGridView1.FindEditFormTemplateControl("txtKHMC") as ASPxTextBox;
        ASPxTextBox uKHH = ASPxGridView1.FindEditFormTemplateControl("txtKHH") as ASPxTextBox;
        ASPxTextBox uWLH = ASPxGridView1.FindEditFormTemplateControl("txtWLH") as ASPxTextBox;

        ASPxTextBox uGYSDM = ASPxGridView1.FindEditFormTemplateControl("txtGYSDM") as ASPxTextBox;
        ASPxTextBox uSO = ASPxGridView1.FindEditFormTemplateControl("txtSO") as ASPxTextBox;
        ASPxTextBox uBBH = ASPxGridView1.FindEditFormTemplateControl("txtBBH") as ASPxTextBox;



        string Sql = "INSERT INTO ATPUKHTM (KHMC,KHH,WLH,GYSDM,SO,BBH,INPUT_PERSON,INPUT_TIME) "
             + "VALUES( '" + uKHMC.Text.Trim() + "', '" + uKHH.Text.Trim() + "','" + uWLH.Text.Trim() + "','" + uGYSDM.Text.Trim() + "','" + uSO.Text.Trim() + "','" + uBBH.Text.Trim() + "','"+theUserId+"',sysdate)";
        dc.ExeSql(Sql);
        //插入到日志表
        try
        {
            string Sql2 = "INSERT INTO ATPUKHTM_LOG (khmc,khh,wlh,gysdm,sn,so,bbh,user_code,flag,rqsj)"
                + " SELECT khmc,khh,wlh,gysdm,sn,so,bbh,'" + theUserCode + "' , 'ADD', SYSDATE FROM ATPUKHTM WHERE SO = '" + uSO.Text.Trim() + "' AND KHMC = '" + uKHMC.Text.Trim() + "' ";
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
        ASPxTextBox uKHMC = ASPxGridView1.FindEditFormTemplateControl("txtKHMC") as ASPxTextBox;
        ASPxTextBox uKHH = ASPxGridView1.FindEditFormTemplateControl("txtKHH") as ASPxTextBox;
        ASPxTextBox uWLH = ASPxGridView1.FindEditFormTemplateControl("txtWLH") as ASPxTextBox;

        ASPxTextBox uGYSDM = ASPxGridView1.FindEditFormTemplateControl("txtGYSDM") as ASPxTextBox;
        ASPxTextBox uSO = ASPxGridView1.FindEditFormTemplateControl("txtSO") as ASPxTextBox;
        ASPxTextBox uBBH = ASPxGridView1.FindEditFormTemplateControl("txtBBH") as ASPxTextBox;

        
        string Sql = "UPDATE ATPUKHTM SET KHH='" + uKHH.Text.Trim() + "',WLH='" + uWLH.Text.Trim() + "',GYSDM='" + uGYSDM.Text.Trim() + "',BBH='" + uBBH.Text.Trim() + "',input_person='"+theUserId+"',input_time=sysdate "
             + " WHERE   KHMC = '" + uKHMC.Text.Trim() + "' and  SO = '" + uSO.Text.Trim() + "'";
        dc.ExeSql(Sql);
        //插入到日志表
        try
        {
            string KHHOld = ""; 
            string WLHOld = ""; 
            string SNOld = ""; 
            string BBHOld = ""; 
            if (e.OldValues["KHH"] != null)
            {
                KHHOld = e.OldValues["KHH"].ToString(); 
            }
            if (e.OldValues["WLH"] != null)
            {
                WLHOld = e.OldValues["WLH"].ToString();
            }
            if (e.OldValues["SN"] != null)
            {
                SNOld = e.OldValues["SN"].ToString();
            }
            if (e.OldValues["BBH"] != null)
            {
                BBHOld = e.OldValues["BBH"].ToString();
            }
            
            string Sql2 = " INSERT INTO ATPUKHTM_LOG (khmc,khh,wlh,gysdm,sn,so,bbh,user_code,flag,rqsj)"
                           + " VALUES( '"
                           + e.OldValues["KHMC"].ToString() + "','"
                           + KHHOld + "','"
                           + WLHOld + "','"
                           + e.OldValues["GYSDM"].ToString() + "','"
                           + SNOld + "','"
                           + e.OldValues["SO"].ToString() + "','"
                           + BBHOld + "','"
                           + theUserCode + "','BEFOREEDIT',SYSDATE) ";
            dc.ExeSql(Sql2);
            string KHHNew = "";
            string WLHNew = "";
            string SNNew = "";
            string BBHNew = "";
            if (e.OldValues["KHH"] != null)
            {
                KHHNew = e.NewValues["KHH"].ToString();
            }
            if (e.OldValues["WLH"] != null)
            {
                WLHNew = e.NewValues["WLH"].ToString();
            }
            if (e.OldValues["SN"] != null)
            {
                SNNew = e.NewValues["SN"].ToString();
            }
            if (e.OldValues["BBH"] != null)
            {
                BBHNew = e.NewValues["BBH"].ToString();
            }
            string Sql3 = "INSERT INTO ATPUKHTM_LOG (khmc,khh,wlh,gysdm,sn,so,bbh,user_code,flag,rqsj)"
                           + " VALUES( '"
                           + e.NewValues["KHMC"].ToString() + "','"
                           + KHHNew + "','"
                           + WLHNew + "','"
                           + e.NewValues["GYSDM"].ToString() + "','"
                           + SNNew + "','"
                           + e.NewValues["SO"].ToString() + "','"
                           + BBHNew + "','"
                           + theUserCode + "','AFTEREDIT',SYSDATE) ";
            dc.ExeSql(Sql3);
            
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
            (ASPxGridView1.FindEditFormTemplateControl("txtSO") as ASPxTextBox).Enabled = false;
            (ASPxGridView1.FindEditFormTemplateControl("txtKHMC") as ASPxTextBox).Enabled = false;
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
            string chSql = "SELECT SO,KHMC FROM ATPUKHTM"
                + " WHERE SO='" + e.NewValues["SO"].ToString() + "' and KHMC='" + e.NewValues["KHMC"].ToString() + "'";
            DataTable dt = dc.GetTable(chSql);
            if (dt.Rows.Count > 0)
            {
                e.RowError = "该客户这个SO的供应商代码已经维护!";
            }
        }
        if (e.NewValues["SO"].ToString().Length > 20)
        {
            e.RowError = "SO号字节长度不能超过20！";
        }

        if (e.NewValues["KHMC"].ToString().Length > 30)
        {
            e.RowError = "客户名称字节长度不能超过30！";
        }


        if (e.NewValues["SO"].ToString() == "" || e.NewValues["SO"].ToString() == null)
        {
            e.RowError = "SO号 不能为空！";
        }

    }



}
