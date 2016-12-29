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
using System.Windows.Forms;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxClasses;
using Rmes.Pub.Data;
using Rmes.DA.Base;
using DevExpress.Web.ASPxGridLookup;
/**
 * 功能概述：喷漆类型维护
 * 作者：游晓航
 * 创建时间：2016-07-07
 */

public partial class Rmes_atpu2000 : Rmes.Web.Base.BasePage
{
    private dataConn dc = new dataConn();
    private PubCs pc = new PubCs();
    public string theCompanyCode, theUserId,theUserCode;
    public string theProgramCode;

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theUserId = theUserManager.getUserId();
        theUserCode = theUserManager.getUserCode();
        theProgramCode = "atpu2000";
            
            setCondition();
            initSO();
 
        if (Request["opFlag"] == "getEditSeries")
        {
            string str1 = "";
            string so = Request["SO"].ToString();
            string sql = "select jx,config from copy_engine_property where SO='" + so.ToUpper() + "'";
            dc.setTheSql(sql);
            if (dc.GetTable().Rows.Count == 0)
            {
                str1 = "";
                this.Response.Write(str1);
                this.Response.End();
                return;
            }
            string config1 = dc.GetTable().Rows[0][1].ToString();
            string jx1 = dc.GetTable().Rows[0][0].ToString();
            if (jx1 == "")
            {
                str1 = "";
                this.Response.Write(str1);
                this.Response.End();
                return;
            }
            if (jx1.EndsWith("ZZ"))
            {
                jx1 = jx1.Substring(0, jx1.Length - 2);
            }
            str1 = jx1;
            sql = "select GET_CSKD('" + so + "') from dual";
            dc.setTheSql(sql);
            string bz1 = dc.GetValue().ToString();
            str1 = str1 + "," + bz1;
            this.Response.Write(str1);
            this.Response.End();
        }

       
    }
    private void initSO()
    {
        //初始化SO下拉列表
        string plineSql = "SELECT SO FROM copy_engine_property order by so ";
        SqlSO.SelectCommand = plineSql;
        SqlSO.DataBind();
        SqlSO2.SelectCommand = plineSql;
        SqlSO2.DataBind();
    }

    private void setCondition()
    {

        string sql = "SELECT * FROM PAINT_SO order by input_time desc nulls last";
      
        DataTable dt = dc.GetTable(sql);

       
        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();

        string sql2 = "SELECT * FROM PAINT_SO WHERE TYPE_PQBAK='Z' ORDER BY input_time desc nulls last,SO ";
        

        DataTable dt2 = dc.GetTable(sql2);
        //if (dt2.Rows.Count > 0)
        //{ MessageBox.Show("出现新机型，请注意增加喷漆类型!", "提示"); }
        ASPxGridView2.DataSource = dt2;
        ASPxGridView2.DataBind();
       
    }

    //新增
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridLookup uSO = ASPxGridView1.FindEditFormTemplateControl("txtSO") as ASPxGridLookup;
        ASPxTextBox uJX = ASPxGridView1.FindEditFormTemplateControl("txtJX") as ASPxTextBox;
        ASPxTextBox uPQBAK = ASPxGridView1.FindEditFormTemplateControl("txtPQBAK") as ASPxTextBox;
        ASPxTextBox uREMARK = ASPxGridView1.FindEditFormTemplateControl("txtREMARK") as ASPxTextBox;

        string strSO = uSO.Value.ToString();
        string strJX = uJX.Text.Trim();
        string strPQBAK = uPQBAK.Text.Trim();
        string strREMARK = uREMARK.Text.Trim();
       


            string Sql = "INSERT INTO PAINT_SO (SO,TYPE_PQ,TYPE_PQBAK,REMARK,JX,input_person,input_time) "
                   + "VALUES( '" + strSO + "','" + strPQBAK + "','" + strPQBAK + "','" + strREMARK + "','" + strJX + "','"+theUserId+"',sysdate)";
            dc.ExeSql(Sql);

            //插入到日志表
            try
            {
                string Sql2 = "INSERT INTO PAINT_SO_LOG (SO,TYPE_PQ,TYPE_PQBAK,REMARK,JX,user_code,flag,rqsj)"
                    + " VALUES( '" + strSO + "','" + strPQBAK + "','" + strPQBAK + "','" + strREMARK + "','" + strJX + "','" + theUserCode + "' , 'ADD', SYSDATE) ";
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

    //删除
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        //判断当前记录是否可以删除
        string strCode = e.Values["SO"].ToString();
        string strTableName = "PAINT_SO";

        dataConn theDataConn = new dataConn("select func_check_delete_data('" + strTableName + "','" + theCompanyCode + "','MES','MES','MES','" + strCode + "') from dual");

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
                string Sql2 = "INSERT INTO PAINT_SO_LOG (SO,TYPE_PQ,TYPE_PQBAK,REMARK,JX,user_code,flag,rqsj)"
                    + " SELECT SO,TYPE_PQ,TYPE_PQBAK,REMARK,JX,'" + theUserCode + "' , 'DEL', SYSDATE FROM PAINT_SO WHERE SO = '" + strCode + "'";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }

            //确认删除
            string Sql = "delete from PAINT_SO WHERE  SO =  '" + strCode + "'";
            dc.ExeSql(Sql);
        }


        setCondition();
        e.Cancel = true;
    }

    //修改
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {

        //ASPxTextBox uCode = ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxTextBox;
        //ASPxTextBox uName = ASPxGridView1.FindEditFormTemplateControl("txtPName") as ASPxTextBox;

        ASPxGridLookup uSO = ASPxGridView1.FindEditFormTemplateControl("txtSO") as ASPxGridLookup;
        ASPxTextBox uJX = ASPxGridView1.FindEditFormTemplateControl("txtJX") as ASPxTextBox;
        ASPxTextBox uPQBAK = ASPxGridView1.FindEditFormTemplateControl("txtPQBAK") as ASPxTextBox;
        ASPxTextBox uREMARK = ASPxGridView1.FindEditFormTemplateControl("txtREMARK") as ASPxTextBox;

        string strSO = uSO.Value.ToString();
        string strJX = uJX.Text.Trim();
        string strPQBAK = uPQBAK.Text.Trim();
        string strREMARK = uREMARK.Text.Trim();


        //插入到日志表
        try
        {
            string Sql2 = "INSERT INTO PAINT_SO_LOG (SO,TYPE_PQ,TYPE_PQBAK,REMARK,JX,user_code,flag,rqsj)"
                + " SELECT SO,TYPE_PQ,TYPE_PQBAK,REMARK,JX,'" + theUserCode + "' , 'BEFOREEDIT', SYSDATE FROM PAINT_SO WHERE SO = '" + strSO + "'";
            dc.ExeSql(Sql2);
        }
        catch
        {
            return;
        }

        string Sql = "UPDATE PAINT_SO SET TYPE_PQ='" + strPQBAK + "',TYPE_PQBAK='" + strPQBAK + "',REMARK='" + strREMARK + "',input_person='" + theUserId + "',input_time=sysdate"
             + " WHERE SO = '" + strSO + "' ";
        dc.ExeSql(Sql);

        //插入到日志表
        try
        {
            string Sql2 = "INSERT INTO PAINT_SO_LOG (SO,TYPE_PQ,TYPE_PQBAK,REMARK,JX,user_code,flag,rqsj)"
                + " SELECT SO,TYPE_PQ,TYPE_PQBAK,REMARK,JX,'" + theUserCode + "' , 'AFTEREDIT', SYSDATE FROM PAINT_SO WHERE SO = '" + strSO + "'";
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
    //创建EDITFORM前
    protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
    {

        
        if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
        {
            ///主键不可以修改
            (ASPxGridView1.FindEditFormTemplateControl("txtSO") as ASPxGridLookup).ClientEnabled = false;
            (ASPxGridView1.FindEditFormTemplateControl("txtJX") as ASPxTextBox).Enabled = false;
        }
        

    }
   

    //修改数据校验
    protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        //判断为空
        if (e.NewValues["SO"].ToString() == "" || e.NewValues["SO"].ToString() == null)
        {
            e.RowError = "SO不能为空！";
        }
        

        string strSO = e.NewValues["SO"].ToString().Trim();
     

        //判断超长
        if (strSO.Length > 30)
        {
            e.RowError = "初始值字节长度不能超过30！";
        }
        

        //判断是否重复
        if (ASPxGridView1.IsNewRowEditing)
        {
            string chSql = "SELECT SO  FROM PAINT_SO WHERE SO = '" + strSO + "'";
            DataTable dt = dc.GetTable(chSql);
            if (dt.Rows.Count > 0)
            {
                e.RowError = "已存在相同的SO号！";
            }

        }

    }
    protected void ASPxGridView2_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridLookup uSO = ASPxGridView2.FindEditFormTemplateControl("txtSO2") as ASPxGridLookup;
        ASPxTextBox uJX = ASPxGridView2.FindEditFormTemplateControl("txtJX") as ASPxTextBox;
        ASPxTextBox uPQBAK = ASPxGridView2.FindEditFormTemplateControl("txtPQBAK") as ASPxTextBox;
        ASPxTextBox uREMARK = ASPxGridView2.FindEditFormTemplateControl("txtREMARK") as ASPxTextBox;

        string strSO = uSO.Value.ToString();
        string strJX = uJX.Text.Trim();
        string strPQBAK = uPQBAK.Text.Trim();
        string strREMARK = uREMARK.Text.Trim();



        string Sql = "INSERT INTO PAINT_SO (SO,TYPE_PQ,TYPE_PQBAK,REMARK,JX,input_person,input_time) "
               + "VALUES( '" + strSO + "','" + strPQBAK + "','" + strPQBAK + "','" + strREMARK + "','" + strJX + "','"+theUserId+"',sysdate)";
        dc.ExeSql(Sql);

        //插入到日志表
        try
        {
            string Sql2 = "INSERT INTO PAINT_SO_LOG (SO,TYPE_PQ,TYPE_PQBAK,REMARK,JX,user_code,flag,rqsj)"
                + " VALUES( '" + strSO + "','" + strPQBAK + "','" + strPQBAK + "','" + strREMARK + "','" + strJX + "','" + theUserCode + "' , 'ADD', SYSDATE) ";
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
        //判断当前记录是否可以删除
        string strCode = e.Values["SO"].ToString();
        string strTableName = "PAINT_SO";

        dataConn theDataConn = new dataConn("select func_check_delete_data('" + strTableName + "','" + theCompanyCode + "','MES','MES','MES','" + strCode + "') from dual");

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
                string Sql2 = "INSERT INTO PAINT_SO_LOG (SO,TYPE_PQ,TYPE_PQBAK,REMARK,JX,user_code,flag,rqsj)"
                    + " SELECT SO,TYPE_PQ,TYPE_PQBAK,REMARK,JX,'" + theUserCode + "' , 'DEL', SYSDATE FROM PAINT_SO WHERE SO = '" + strCode + "'";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }
            //确认删除
            string Sql = "delete from PAINT_SO WHERE  SO =  '" + strCode + "'";
            dc.ExeSql(Sql);
        }


        setCondition();
        e.Cancel = true;
    }

    //修改
    protected void ASPxGridView2_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridLookup uSO = ASPxGridView2.FindEditFormTemplateControl("txtSO2") as ASPxGridLookup;
        ASPxTextBox uJX = ASPxGridView2.FindEditFormTemplateControl("txtJX") as ASPxTextBox;
        ASPxTextBox uPQBAK = ASPxGridView2.FindEditFormTemplateControl("txtPQBAK") as ASPxTextBox;
        ASPxTextBox uREMARK = ASPxGridView2.FindEditFormTemplateControl("txtREMARK") as ASPxTextBox;

        string strSO = uSO.Value.ToString();
        string strJX = uJX.Text.Trim();
        string strPQBAK = uPQBAK.Text.Trim();
        string strREMARK = uREMARK.Text.Trim();


        //插入到日志表
        try
        {
            string Sql2 = "INSERT INTO PAINT_SO_LOG (SO,TYPE_PQ,TYPE_PQBAK,REMARK,JX,user_code,flag,rqsj)"
                + " SELECT SO,TYPE_PQ,TYPE_PQBAK,REMARK,JX,'" + theUserCode + "' , 'BEFOREEDIT', SYSDATE FROM PAINT_SO WHERE SO = '" + strSO + "' and JX= '" + strJX + "'";
            dc.ExeSql(Sql2);
        }
        catch
        {
            return;
        }

        string Sql = "UPDATE PAINT_SO SET TYPE_PQ='" + strPQBAK + "',TYPE_PQBAK='" + strPQBAK + "',REMARK='" + strREMARK + "',input_person='"+theUserId+"',input_time=sysdate"
             + " WHERE SO = '" + strSO + "'and JX= '" + strJX + "' ";
        dc.ExeSql(Sql);

        //插入到日志表
        try
        {
            string Sql2 = "INSERT INTO PAINT_SO_LOG (SO,TYPE_PQ,TYPE_PQBAK,REMARK,JX,user_code,flag,rqsj)"
                + " SELECT SO,TYPE_PQ,TYPE_PQBAK,REMARK,JX,'" + theUserCode + "' , 'AFTEREDIT', SYSDATE FROM PAINT_SO WHERE SO = '" + strSO + "' and JX= '" + strJX + "'";
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
    protected void ASPxGridView2_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
    {


        if (!ASPxGridView2.IsNewRowEditing && ASPxGridView2.IsEditing)
        {
            ///主键不可以修改
            (ASPxGridView2.FindEditFormTemplateControl("txtSO2") as ASPxGridLookup).ClientEnabled  = false;
            (ASPxGridView2.FindEditFormTemplateControl("txtJX") as ASPxTextBox).Enabled = false;
        }


    }
    //protected void ASPxGridView2_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    //{
    //    //进入修改界面时的处理

    //    if (!ASPxGridView2.IsNewRowEditing && ASPxGridView2.IsEditing)
    //    {
    //         = ASPxGridView2.GetRowValues(ASPxGridView2.FocusedRowIndex, "JX").ToString();
    //    }
    //}

    //修改数据校验
    protected void ASPxGridView2_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        //判断为空
        if (e.NewValues["SO"].ToString() == "" || e.NewValues["SO"].ToString() == null)
        {
            e.RowError = "SO不能为空！";
        }


        string strSO = e.NewValues["SO"].ToString().Trim();


        //判断超长
        if (strSO.Length > 30)
        {
            e.RowError = "初始值字节长度不能超过30！";
        }


        //判断是否重复
        if (ASPxGridView2.IsNewRowEditing)
        {
            string chSql = "SELECT SO  FROM PAINT_SO WHERE SO = '" + strSO + "'";
            DataTable dt = dc.GetTable(chSql);
            if (dt.Rows.Count > 0)
            {
                e.RowError = "已存在相同的SO号！";
            }

        }

    }
    protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        setCondition();
        ASPxGridView1.Selection.UnselectAll();

    }
    protected void ASPxGridView2_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        setCondition();
        ASPxGridView1.Selection.UnselectAll();

    }

    protected void txtSO_Init(object sender, EventArgs e)
    {
        initSO();
    }

    protected void txtSO2_Init(object sender, EventArgs e)
    {
        initSO();
    }
    

}
