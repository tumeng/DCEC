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
 * 功能概述：EMARK维护
 * 作者：游晓航
 * 创建时间：2016-06-24
 * 修改时间：
 */

public partial class Rmes_atpu2400 : Rmes.Web.Base.BasePage
{
    private dataConn dc = new dataConn();
    private PubCs pc = new PubCs();
    public string theCompanyCode, theUserId,theUserCode;

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
        string sql = "SELECT * FROM ATPUEMARK order by INPUT_TIME desc nulls last";
        DataTable dt = dc.GetTable(sql);

        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
    }

    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        //判断当前记录是否可以删除
        string strDelCode = e.Values["FDJJX"].ToString();
        string strDelCode2 = e.Values["SC"].ToString();
        string strTableName = "ATPUEMARK";

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
                string Sql2 = "INSERT INTO ATPUEMARK_LOG (FDJJX,SC,RZZS1,RZZS2,RZZS3,RZG,SFMRZ,WHRQ,RYDM,user_code,flag,rqsj)"
                    + " SELECT FDJJX,SC,RZZS1,RZZS2,RZZS3,RZG,SFMRZ,WHRQ,RYDM,'" + theUserCode + "' , 'DEL', SYSDATE FROM ATPUEMARK WHERE FDJJX = '" + strDelCode + "' and SC = '" + strDelCode2 + "'";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }
            //确认删除
            string Sql = "delete from ATPUEMARK WHERE   FDJJX = '" + strDelCode + "' and SC = '" + strDelCode2 + "'";
            dc.ExeSql(Sql);
        }


        setCondition();
        e.Cancel = true;
    }


    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxTextBox uJX = ASPxGridView1.FindEditFormTemplateControl("txtJX") as ASPxTextBox;
        ASPxTextBox uRZG = ASPxGridView1.FindEditFormTemplateControl("txtRZG") as ASPxTextBox;
        ASPxTextBox uSC = ASPxGridView1.FindEditFormTemplateControl("txtSC") as ASPxTextBox;
      
        ASPxTextBox uS1 = ASPxGridView1.FindEditFormTemplateControl("txtS1") as ASPxTextBox;
        ASPxTextBox uS2 = ASPxGridView1.FindEditFormTemplateControl("txtS2") as ASPxTextBox;
        ASPxTextBox uS3 = ASPxGridView1.FindEditFormTemplateControl("txtS3") as ASPxTextBox;
        ASPxComboBox uSFMRZ = ASPxGridView1.FindEditFormTemplateControl("txtSFMRZ") as ASPxComboBox;

        string strJX = uJX.Text.Trim();
        string strSFMRZ = uSFMRZ.Value.ToString();
        //判断是否以zz结尾，如果是，去掉ZZ，再插入数据库
        string mJX = strJX.Remove(0, strJX.Length - 2);
        string newJX = " ";
        if (mJX == "ZZ")
        {

            newJX = strJX.Substring(0, strJX.Length - 2);
            strJX = newJX;
        }


        string Sql = "INSERT INTO ATPUEMARK(FDJJX,SC,RZZS1,RZZS2,RZZS3,RZG,SFMRZ,WHRQ,RYDM,INPUT_PERSON,INPUT_TIME)  "
             + "VALUES( '" + strJX + "', '" + uSC.Text.Trim().ToUpper() + "','" + uS1.Text.Trim() + "','" + uS2.Text.Trim() + "','" + uS3.Text.Trim() + "','" + uRZG.Text.Trim() + "','" + uSFMRZ.Text.Trim() + "',to_date('" + theTime + "','yyyy-mm-dd hh24:mi:ss'),'" + theUserId + "','" + theUserId + "',sysdate)";
        dc.ExeSql(Sql);
        //插入到日志表
        try
        {
            string Sql2 = "INSERT INTO ATPUEMARK_LOG (FDJJX,SC,RZZS1,RZZS2,RZZS3,RZG,SFMRZ,WHRQ,RYDM,user_code,flag,rqsj)"
                + " SELECT FDJJX,SC,RZZS1,RZZS2,RZZS3,RZG,SFMRZ,WHRQ,RYDM,'" + theUserCode + "' , 'ADD', SYSDATE FROM ATPUEMARK WHERE FDJJX = '" + strJX + "' and SC = '" + uSC.Text.Trim().ToUpper() + "'";
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

        ASPxTextBox uJX = ASPxGridView1.FindEditFormTemplateControl("txtJX") as ASPxTextBox;
        ASPxTextBox uRZG = ASPxGridView1.FindEditFormTemplateControl("txtRZG") as ASPxTextBox;
        ASPxTextBox uSC = ASPxGridView1.FindEditFormTemplateControl("txtSC") as ASPxTextBox;

        ASPxTextBox uS1 = ASPxGridView1.FindEditFormTemplateControl("txtS1") as ASPxTextBox;
        ASPxTextBox uS2 = ASPxGridView1.FindEditFormTemplateControl("txtS2") as ASPxTextBox;
        ASPxTextBox uS3 = ASPxGridView1.FindEditFormTemplateControl("txtS3") as ASPxTextBox;
        ASPxComboBox uSFMRZ = ASPxGridView1.FindEditFormTemplateControl("txtSFMRZ") as ASPxComboBox;

        string strSFMRZ = uSFMRZ.Value.ToString();

        //插入到日志表
        try
        {
            string Sql2 = "INSERT INTO ATPUEMARK_LOG (FDJJX,SC,RZZS1,RZZS2,RZZS3,RZG,SFMRZ,WHRQ,RYDM,user_code,flag,rqsj)"
                + " SELECT FDJJX,SC,RZZS1,RZZS2,RZZS3,RZG,SFMRZ,WHRQ,RYDM,'" + theUserCode + "' , 'BEFOREEDIT', SYSDATE FROM ATPUEMARK WHERE FDJJX = '" + uJX.Text.Trim() + "'";
            dc.ExeSql(Sql2);
        }
        catch
        {
            return;
        }
        string Sql = "UPDATE ATPUEMARK SET SC='" + uSC.Text.Trim().ToUpper() + "',RZZS1='" + uS1.Text.Trim() + "',RZZS2='" + uS2.Text.Trim() + "',"
                   +"RZZS3='" + uS3.Text.Trim() + "',RZG='" + uRZG.Text.Trim() + "',SFMRZ='" + uSFMRZ.Text.Trim() + "',"
                   +"WHRQ=to_date('" + theTime + "','yyyy-mm-dd hh24:mi:ss'),INPUT_TIME=SYSDATE,INPUT_PERSON='"+theUserId+"' "
                   + " WHERE   FDJJX = '" + uJX.Text.Trim() + "'";
        dc.ExeSql(Sql);
        //插入到日志表
        try
        {
            string Sql2 = "INSERT INTO ATPUEMARK_LOG (FDJJX,SC,RZZS1,RZZS2,RZZS3,RZG,SFMRZ,WHRQ,RYDM,user_code,flag,rqsj)"
                + " SELECT FDJJX,SC,RZZS1,RZZS2,RZZS3,RZG,SFMRZ,WHRQ,RYDM,'" + theUserCode + "' , 'AFTEREDIT', SYSDATE FROM ATPUEMARK WHERE FDJJX = '" + uJX.Text.Trim() + "'";
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
            (ASPxGridView1.FindEditFormTemplateControl("txtJX") as ASPxTextBox).Enabled = false;
        }
        else
        {
            //uTcode.SelectedIndex = 0;
        }
    }

    protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        
        if (ASPxGridView1.IsEditing)
        {
            //判断是否非法
            string chSqlFR = "select ps_par from copy_ps_mstr"
               + " WHERE ps_par='" + e.NewValues["FDJJX"].ToString() + "'";
            DataTable dtFR = dc.GetTable(chSqlFR);
            
            string chSqlSC = "select pt_part from copy_pt_mstr"
                    + " WHERE pt_part='" + e.NewValues["SC"].ToString().ToUpper() + "'";
            DataTable dtSC = dc.GetTable(chSqlSC);
           
            //判断是否重复
            string chSql = "SELECT * FROM ATPUEMARK WHERE FDJJX='" + e.NewValues["FDJJX"].ToString() + "' and sc='" + e.NewValues["SC"].ToString().ToUpper() + "'";
            DataTable dt = dc.GetTable(chSql);
            if (dtFR.Rows.Count == 0)
            {
                e.RowError = "机型不存在！请重新维护";
            }
            else 
            if (dtSC.Rows.Count == 0)
            {
                e.RowError = "SC组不存在！请重新维护";
            }
            else if (dt.Rows.Count > 0)
            {
                e.RowError = "该机型、SC对应的记录已经存在！请重新维护！";
            }
        }
        //if (e.NewValues["SO"].ToString().Length > 30)
        //{
        //    e.RowError = "SO号字节长度不能超过30！";
        //}

        //if (e.NewValues["PF"].ToString().Length > 50)
        //{
        //    e.RowError = "排放参数字节长度不能超过50！";
        //}


        //if (e.NewValues["SO"].ToString() == "" || e.NewValues["SO"].ToString() == null)
        //{
        //    e.RowError = "SO号 不能为空！";
        //}

    }



}
