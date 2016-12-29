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
using Rmes.DA.Base;
/**
 * 功能概述：So对应config号维护以及NONPPIF维护
 * 作者：游晓航
 * 创建时间：2016-07-27
 */

public partial class Rmes_atpu2600 : Rmes.Web.Base.BasePage
{
    private dataConn dc = new dataConn();
    private PubCs pc = new PubCs();
    public string theCompanyCode, theUserCode, theUserId;

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theUserId = theUserManager.getUserId();
        theUserCode = theUserManager.getUserCode();

        initSO();
        initConfig();
        setCondition();

    }
    private void initSO()
    {
        //初始化用户下拉列表
        string sql = "SELECT  DISTINCT SO FROM PPIF_ATPU ";
        SqlSO.SelectCommand = sql;
        SqlSO.DataBind();
    }
    private void initConfig()
    {
        //初始化用户下拉列表
        string sql = "SELECT  DISTINCT CONFIG FROM PPIF_ATPU ";
        SqlConfig.SelectCommand = sql;
        SqlConfig.DataBind();
    }

    private void setCondition()
    {
        string sql = "SELECT * FROM PPIF_ATPU order by SO,CONFIG   ";
        
        DataTable dt = dc.GetTable(sql);

        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();

        string sql2 = "SELECT * FROM NONPPIF_ATPU  order by INPUT_TIME desc nulls last, SO ";
        DataTable dt2 = dc.GetTable(sql2);

        ASPxGridView2.DataSource = dt2;
        ASPxGridView2.DataBind();
    }
    protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        setCondition();
        
    }
    protected void ASPxGridView2_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        setCondition();
        
    }

    
    
    protected void ASPxGridView2_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
       
        ASPxTextBox uSO = ASPxGridView2.FindEditFormTemplateControl("TextSO") as ASPxTextBox;
       
        string strSO = uSO.Text.Trim();



        string Sql = "INSERT INTO NONPPIF_ATPU (SO,input_person,input_time)VALUES( '" + strSO + "','"+theUserId+"',sysdate) ";
        dc.ExeSql(Sql);
        //插入到日志表
        try
        {
            string Sql2 = "INSERT INTO NONPPIF_ATPU_LOG (SO,user_code,flag,rqsj)"
                + " SELECT SO,'" + theUserCode + "' , 'ADD', SYSDATE FROM NONPPIF_ATPU WHERE SO = '" + strSO + "' ";
            dc.ExeSql(Sql2);
        }
        catch
        {
            return;
        }

        e.Cancel = true;
        ASPxGridView2.CancelEdit();
        setCondition();
    }

    //删除
    protected void ASPxGridView2_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        //判断当前记录是否可以删除
        string strCode = e.Values["SO"].ToString();
        string strTableName = "NONPPIF_ATPU";

        dataConn theDataConn = new dataConn("select func_check_delete_data('" + strTableName + "','" + theCompanyCode + "','MES','MES','MES','" + strCode + "') from dual");

        theDataConn.OpenConn();
        string theRet = theDataConn.GetValue();
        if (theRet != "Y")
        {
            ASPxGridView2.JSProperties.Add("cpCallbackName", "Delete");
            ASPxGridView2.JSProperties.Add("cpCallbackRet", theRet);
            theDataConn.CloseConn();
        }
        else
        {
            //插入到日志表
            try
            {
                string Sql2 = "INSERT INTO NONPPIF_ATPU_LOG (SO,user_code,flag,rqsj)"
                    + " SELECT SO,'" + theUserCode + "' , 'DEL', SYSDATE FROM NONPPIF_ATPU WHERE SO = '" + strCode + "' ";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }
            //确认删除
            string Sql = "delete from NONPPIF_ATPU WHERE  SO = '" + strCode + "' ";
            dc.ExeSql(Sql);
        }


        setCondition();
        e.Cancel = true;
    }

    

  
   
    

}
