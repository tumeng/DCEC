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
 * 功能概述：辅料工具周期更换提醒表维护
 * 作者：游晓航
 * 创建时间：2016-06-12
 * 修改时间：
 */



public partial class Rmes_atpu1500 : Rmes.Web.Base.BasePage
{
    private dataConn dc = new dataConn();
    private PubCs pc = new PubCs();
    public string theCompanyCode, theUserId,theUserCode;
    public string theProgramCode;
    public DateTime OracleEntityTime, OracleSQLTime;

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theProgramCode = "atpu1500";
        theUserId = theUserManager.getUserId();
        theUserCode = theUserManager.getUserCode();
        //初始化新增模版里生产线下拉列表
        initCode();

        
        setCondition();
    }

    private void initCode()
    {
        //初始化工位下拉列表
        string sql = "SELECT LOCATION_CODE,LOCATION_NAME FROM CODE_LOCATION WHERE COMPANY_CODE = '" + theCompanyCode + "' and pline_code in ((select pline_id from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "')) ORDER BY LOCATION_CODE";
        SqlCode.SelectCommand = sql;
        SqlCode.DataBind();
    }
    private void setCondition()
    {
        //初始化GRIDVIEW
        string sql = "SELECT * FROM ATPUFLGJTHZQB order by input_time desc nulls last ";
        DataTable dt = dc.GetTable(sql);



        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
    }

    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {


        //判断当前记录是否可以删除
        string strDelCode = e.Values["XH"].ToString();
        string strTableName = "ATPUFLGJTHZQB";

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
                string Sql2 = "INSERT INTO ATPUFLGJTHZQB_LOG (XH,LOCATION_CODE,LOCATION_NAME,LJJ,CYCLE,BZ,user_code,flag,rqsj)"
                    + " SELECT XH,LOCATION_CODE,LOCATION_NAME,LJJ,CYCLE,BZ,'" + theUserCode + "' , 'DEL', SYSDATE FROM ATPUFLGJTHZQB WHERE XH = '" + strDelCode + "' ";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }
            
            //确认删除
            string Sql = "delete from ATPUFLGJTHZQB WHERE   XH = '" + strDelCode + "'";
            dc.ExeSql(Sql);
        }


        setCondition();
        e.Cancel = true;
    }


    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxTextBox uLJJ = ASPxGridView1.FindEditFormTemplateControl("txtLJJ") as ASPxTextBox;
        ASPxTextBox uCYCLE = ASPxGridView1.FindEditFormTemplateControl("txtCYCLE") as ASPxTextBox;
        ASPxTextBox uXH = ASPxGridView1.FindEditFormTemplateControl("txtXH") as ASPxTextBox;
        ASPxGridLookup GridLookupCode= ASPxGridView1.FindEditFormTemplateControl("GridLookupCode") as ASPxGridLookup;

        ASPxTextBox uBZ = ASPxGridView1.FindEditFormTemplateControl("txtBZ") as ASPxTextBox;
       
        string code=GridLookupCode.Text.Trim();
        string sqlxh = "SELECT nvl(max(to_number(XH))+1,0) FROM ATPUFLGJTHZQB";

        string xh=dc.GetValue(sqlxh);
        //uXH.Text = xh;
       // DataTable dt = dc.GetTable(xh);
        //int xh = Convert.ToInt32(dc.GetValue(sqlxh));
        

        string Sql = "INSERT INTO ATPUFLGJTHZQB (XH,LOCATION_CODE,LOCATION_NAME,LJJ,CYCLE,BZ,INPUT_PERSON,INPUT_TIME) "
             + "VALUES( '" + xh + "', '" + code + "','" + code + "','" + uLJJ.Text.Trim() + "','" + uCYCLE.Text.Trim() + "','" + uBZ.Text.Trim() + "','"+theUserId+"',SYSDATE)";
        dc.ExeSql(Sql);

        //插入到日志表
        try
        {
            string Sql2 = "INSERT INTO ATPUFLGJTHZQB_LOG (XH,LOCATION_CODE,LOCATION_NAME,LJJ,CYCLE,BZ,user_code,flag,rqsj)"
                + " SELECT XH,LOCATION_CODE,LOCATION_NAME,LJJ,CYCLE,BZ,'" + theUserCode + "' , 'ADD', SYSDATE FROM ATPUFLGJTHZQB WHERE XH = '" + xh + "' ";
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

        ASPxTextBox uLJJ = ASPxGridView1.FindEditFormTemplateControl("txtLJJ") as ASPxTextBox;
        ASPxTextBox uCYCLE = ASPxGridView1.FindEditFormTemplateControl("txtCYCLE") as ASPxTextBox;
        ASPxTextBox uXH = ASPxGridView1.FindEditFormTemplateControl("txtXH") as ASPxTextBox;
        ASPxGridLookup GridLookupCode= ASPxGridView1.FindEditFormTemplateControl("GridLookupCode") as ASPxGridLookup;

        ASPxTextBox uBZ = ASPxGridView1.FindEditFormTemplateControl("txtBZ") as ASPxTextBox;
       
        string code=GridLookupCode.Text.Trim();

        //插入到日志表
        try
        {
            string Sql2 = "INSERT INTO ATPUFLGJTHZQB_LOG (XH,LOCATION_CODE,LOCATION_NAME,LJJ,CYCLE,BZ,user_code,flag,rqsj)"
                + " SELECT XH,LOCATION_CODE,LOCATION_NAME,LJJ,CYCLE,BZ,'" + theUserCode + "' , 'BEFOREEDIT', SYSDATE FROM ATPUFLGJTHZQB WHERE XH = '" + uXH.Text.Trim() + "' ";
            dc.ExeSql(Sql2);
        }
        catch
        {
            return;
        }

        string Sql = "UPDATE ATPUFLGJTHZQB SET LOCATION_CODE='" + code + "', LOCATION_NAME='" +code + "',LJJ='" + uLJJ.Text.Trim() + "',CYCLE='" + uCYCLE.Text.Trim() + "',BZ='" + uBZ.Text.Trim() + "',INPUT_PERSON='"+theUserId+"',INPUT_TIME=SYSDATE "
             + " WHERE   XH = '" + uXH.Text.Trim() + "'";
        dc.ExeSql(Sql);

        //插入到日志表
        try
        {
            string Sql2 = "INSERT INTO ATPUFLGJTHZQB_LOG (XH,LOCATION_CODE,LOCATION_NAME,LJJ,CYCLE,BZ,user_code,flag,rqsj)"
                + " SELECT XH,LOCATION_CODE,LOCATION_NAME,LJJ,CYCLE,BZ,'" + theUserCode + "' , 'AFTEREDIT', SYSDATE FROM ATPUFLGJTHZQB WHERE XH = '" + uXH.Text.Trim() + "' ";
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
            (ASPxGridView1.FindEditFormTemplateControl("txtXH") as ASPxTextBox).Enabled = false;
        }
        else
        {
            //uTcode.SelectedIndex = 0;
        }
    }

    protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        
        if (e.NewValues["LJJ"].ToString().Length > 100)
        {
            e.RowError = "量检辅工具字节长度不能超过100！";
        }

        if (e.NewValues["CYCLE"].ToString().Length > 50)
        {
            e.RowError = "周期字节长度不能超过50！";
        }

        
        if (e.NewValues["LJJ"].ToString() == "" || e.NewValues["LJJ"].ToString() == null)
        {
            e.RowError = "量检辅工具 不能为空！";
        }

    }
       
 protected void ASPxGridView1_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        //feichu
        if (e.ButtonID == "Camount")
        {
            string xh = ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "XH").ToString();
            string chSql = "select SL from ATPUFLGJTHZQB where XH='" + xh + "'";
           
            DataTable dt = dc.GetTable(chSql);
            
            if(dt.Rows.Count>0)
            {
                 ASPxGridView1.JSProperties.Add("cpCallbackName", "sure");
       
        string Sql = "UPDATE ATPUFLGJTHZQB SET SL=0 where XH ='" + xh + "'";
        dc.ExeSql(Sql);
        setCondition();


            }
            else{
                 ASPxGridView1.JSProperties.Add("cpCallbackName", "no");;
            }    
            }
            
        
    }
 protected void ASPxCbSubmit_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
 {
     string collection = e.Parameter;
     try
     {
         if (collection == "Commit")
         {
             string xh = ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "XH").ToString();
             string chSql = "select SL from ATPUFLGJTHZQB where XH='" + xh + "'";

             DataTable dt = dc.GetTable(chSql);

             if (dt.Rows.Count > 0)
             {
                 //插入到日志表
                 try
                 {
                     string Sql2 = "INSERT INTO ATPUFLGJTHZQB_LOG (XH,LOCATION_CODE,LOCATION_NAME,LJJ,CYCLE,BZ,user_code,flag,rqsj)"
                         + " SELECT XH,LOCATION_CODE,LOCATION_NAME,LJJ,CYCLE,BZ,'" + theUserCode + "' , 'BEFOREEDIT', SYSDATE FROM ATPUFLGJTHZQB WHERE XH = '" + xh + "' ";
                     dc.ExeSql(Sql2);
                 }
                 catch
                 {
                     return;
                 }
               
                 string Sql = "UPDATE ATPUFLGJTHZQB SET SL=0 where XH ='" + xh + "'";
                 dc.ExeSql(Sql);

                 //插入到日志表
                 try
                 {
                     string Sql2 = "INSERT INTO ATPUFLGJTHZQB_LOG (XH,LOCATION_CODE,LOCATION_NAME,LJJ,CYCLE,BZ,user_code,flag,rqsj)"
                         + " SELECT XH,LOCATION_CODE,LOCATION_NAME,LJJ,CYCLE,BZ,'" + theUserCode + "' , 'AFTEREDIT', SYSDATE FROM ATPUFLGJTHZQB WHERE XH = '" + xh + "' ";
                     dc.ExeSql(Sql2);
                 }
                 catch
                 {
                     return;
                 }
                 
             }

         }
     }
     catch (Exception e1)
     {
         e.Result = "Fail,提交失败" + e1.Message + "！";
         return;
     }
     setCondition();
 }
 protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
 {
     setCondition();
 }
}
