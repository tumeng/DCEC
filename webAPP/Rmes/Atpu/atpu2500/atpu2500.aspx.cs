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
 * 功能概述：随机带走件包装维护
 * 作者：游晓航
 * 创建时间：2016-06-15
 */

public partial class Rmes_atpu2500 : Rmes.Web.Base.BasePage
{
    private dataConn dc = new dataConn();
    private PubCs pc = new PubCs();
    public string theCompanyCode, theUserCode;

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theUserCode = theUserManager.getUserCode();

        setCondition();
    }

    private void setCondition()
    {

        string sql = "SELECT * FROM ATPUSOFJB ORDER BY INPUT_TIME desc nulls last, SO,DYXH ";
        
        DataTable dt = dc.GetTable(sql);
        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();

        string sql2 = " SELECT * FROM DMFJBZB ORDER BY INPUT_TIME desc nulls last, LJLB,LJDM";

        DataTable dt2 = dc.GetTable(sql2);

        ASPxGridView2.DataSource = dt2;
        ASPxGridView2.DataBind();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        //setCondition();
        string sql = "SELECT * FROM ATPUSOFJB WHERE ";
        if (txtSoQry.Text.Trim() != "")
        {
            sql = sql + "SO='" + txtSoQry.Text.Trim().ToUpper() + "' ORDER BY INPUT_TIME desc nulls last, SO,DYXH ";
        }
        else
        {
            sql = sql + "1=1 ORDER BY INPUT_TIME desc nulls last, SO,DYXH ";
        }
        DataTable dt = dc.GetTable(sql);
        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
    }
    //新增
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        
        ASPxTextBox uSO = ASPxGridView1.FindEditFormTemplateControl("TextSO") as ASPxTextBox;
        ASPxComboBox uDYXH = ASPxGridView1.FindEditFormTemplateControl("ComboDYXH") as ASPxComboBox;
        ASPxComboBox uFJB = ASPxGridView1.FindEditFormTemplateControl("ComboFJB") as ASPxComboBox;

        string strSO = uSO.Text.Trim();
        string strDYXH = uDYXH.Value.ToString();
        string strFJB = uFJB.Value.ToString();

        string Sql = "INSERT INTO ATPUSOFJB(SO,DYXH,BZDY,FJB,INPUT_PERSON,INPUT_TIME)  "
                   + "VALUES( '" + strSO.ToUpper() + "','" + strDYXH + "','包装单元" + strDYXH + "','" + strFJB + "','"+theUserCode+"',sysdate)";
        dc.ExeSql(Sql);
       
        //插入到日志表
        try
        {
            string Sql2 = "INSERT INTO ATPUSOFJB_LOG (SO,DYXH,BZDY,FJB,user_code,flag,rqsj)"
                + " SELECT SO,DYXH,BZDY,FJB,'" + theUserCode + "' , 'ADD', SYSDATE FROM ATPUSOFJB WHERE  SO = '" + strSO.ToUpper() + "' and DYXH = '" + strDYXH + "'";
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
        string strDYXH = e.Values["DYXH"].ToString();
        string strTableName = "ATPUSOFJB";

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
                string Sql2 = "INSERT INTO ATPUSOFJB_LOG (SO,DYXH,BZDY,FJB,user_code,flag,rqsj)"
                    + " SELECT SO,DYXH,BZDY,FJB,'" + theUserCode + "' , 'DEL', SYSDATE FROM ATPUSOFJB WHERE  SO = '" + strCode + "' and DYXH = '" + strDYXH + "'";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }
            //确认删除
            string Sql = "delete from ATPUSOFJB WHERE  SO = '" + strCode + "' and DYXH = '" + strDYXH + "'";
            dc.ExeSql(Sql);
        }


        setCondition();
        e.Cancel = true;
    }

    //修改
   
    //创建EDITFORM前
    protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
    {

        string Sql = "select ljlb||'--'||ljjc as showtext from dmfjbzb order by ljlb,ljjc ";
        DataTable dt = dc.GetTable(Sql);

        ASPxComboBox uFJB = ASPxGridView1.FindEditFormTemplateControl("ComboFJB") as ASPxComboBox;

        uFJB.DataSource = dt;
       // uFJB.TextField = dt.Columns[1].ToString();
        uFJB.TextField = dt.Columns[0].ToString();
        if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
        {
            ///主键不可以修改
            (ASPxGridView1.FindEditFormTemplateControl("TextSO") as ASPxTextBox).Enabled = false;
        }
        else
        {
            //uTcode.SelectedIndex = 0;
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
        if (e.NewValues["DYXH"].ToString() == "" || e.NewValues["DYXH"].ToString() == null)
        {
            e.RowError = "单元序号不能为空！";
        }

        string strSO = e.NewValues["SO"].ToString().Trim().ToUpper();
        string strFJB = e.NewValues["FJB"].ToString().Trim();
        string strDYXH = e.NewValues["DYXH"].ToString().Trim();


        //判断超长
        if (strSO.Length > 50)
        {
            e.RowError = "SO字节长度不能超过50！";
        }
        if (strFJB.Length > 30)
        {
            e.RowError = "附件包字节长度不能超过30！";
        }

        //判断SO是否存在
        string chSql2 = "SELECT PS_PAR FROM COPY_PS_MSTR WHERE PS_PAR='" + strSO.ToUpper() + "' or ps_par='" + strSO + "ZZ'";
        DataTable dt2 = dc.GetTable(chSql2);
        if (dt2.Rows.Count == 0)
        {
            e.RowError = "该SO号不存在！";
        }

        //判断是否重复
        if (ASPxGridView1.IsNewRowEditing)
        {
            string chSql = "SELECT SO FROM ATPUSOFJB WHERE SO='" + strSO + "' AND DYXH=" + strDYXH + "";
            DataTable dt = dc.GetTable(chSql);
            if (dt.Rows.Count > 0)
            {
                e.RowError = "该序号的记录已经存在，请先删除！";
            }
            

        }

    }
    protected void ASPxGridView2_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxComboBox uLJLB = ASPxGridView2.FindEditFormTemplateControl("ComboLJLB") as ASPxComboBox;
        ASPxTextBox uLJDM = ASPxGridView2.FindEditFormTemplateControl("TextLJDM") as ASPxTextBox;
        ASPxTextBox uLJMC = ASPxGridView2.FindEditFormTemplateControl("TextLJMC") as ASPxTextBox;
        ASPxTextBox uLJJC = ASPxGridView2.FindEditFormTemplateControl("TextLJJC") as ASPxTextBox;
        ASPxTextBox uLJGG = ASPxGridView2.FindEditFormTemplateControl("TextLJGG") as ASPxTextBox;
        ASPxTextBox uLJDW = ASPxGridView2.FindEditFormTemplateControl("TextLJDW") as ASPxTextBox;

        string strLJLB = uLJLB.Value.ToString();
        string strLJDM = uLJDM.Text.Trim();
        string strLJMC = uLJMC.Text.Trim();
        string strLJJC = uLJJC.Text.Trim();
        string strLJGG = uLJGG.Text.Trim();
        string strLJDW = uLJDW.Text.Trim();

        string Sql = "INSERT INTO DMFJBZB (LJDM,LJMC,LJJC,LJGG,LJDW,LJLB,INPUT_PERSON,INPUT_TIME) "
             + "VALUES('" + strLJDM + "', '" + strLJMC + "','" + strLJJC + "','" + strLJGG + "','" + strLJDW + "','" + strLJLB + "','" + theUserCode + "',sysdate)";
        dc.ExeSql(Sql);

        //插入到日志表
        try
        {
            string Sql2 = "INSERT INTO DMFJBZB_LOG (LJDM,LJMC,LJJC,LJGG,LJDW,LJLB,user_code,flag,rqsj)"
                + " SELECT LJDM,LJMC,LJJC,LJGG,LJDW,LJLB,'" + theUserCode + "' , 'ADD', SYSDATE FROM DMFJBZB WHERE LJDM = '" + strLJDM + "'";
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
        string strDM = e.Values["LJDM"].ToString();
        string strTableName = "DMFJBZB";

        dataConn theDataConn = new dataConn("select func_check_delete_data('" + strTableName + "','" + theCompanyCode + "','MES','MES','MES','" + strDM + "') from dual");

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
                string Sql2 = "INSERT INTO DMFJBZB_LOG (LJDM,LJMC,LJJC,LJGG,LJDW,LJLB,user_code,flag,rqsj)"
                    + " SELECT LJDM,LJMC,LJJC,LJGG,LJDW,LJLB,'" + theUserCode + "' , 'DEL', SYSDATE FROM DMFJBZB WHERE LJDM = '" + strDM + "'";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }
            //确认删除
            string Sql = "delete from DMFJBZB WHERE  LJDM = '" + strDM + "'";
            dc.ExeSql(Sql);
        }


        setCondition();
        e.Cancel = true;
    }

    //修改
   

    //创建EDITFORM前
    protected void ASPxGridView2_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
    {
        string Sql = "select  distinct ljlb  as showtext from dmfjbzb";
        DataTable dt = dc.GetTable(Sql);

        ASPxComboBox uPCode = ASPxGridView2.FindEditFormTemplateControl("ComboLJLB") as ASPxComboBox;

        uPCode.DataSource = dt;
        uPCode.TextField = dt.Columns[0].ToString();
    }

    //修改数据校验
    protected void ASPxGridView2_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        
        //判断为空
        if (e.NewValues["LJLB"].ToString() == "" || e.NewValues["LJLB"].ToString() == null)
        {
            e.RowError = "零件类别不能为空！";
        }
        if (e.NewValues["LJDM"].ToString() == "" || e.NewValues["LJDM"].ToString() == null)
        {
            e.RowError = "零件号不能为空！";
        }
        if (e.NewValues["LJMC"].ToString() == "" || e.NewValues["LJMC"].ToString() == null)
        {
            e.RowError = "零件名称不能为空！";
        }
        if (e.NewValues["LJJC"].ToString() == "" || e.NewValues["LJJC"].ToString() == null)
        {
            e.RowError = "零件简称不能为空！";
        }

        string strLJDM = e.NewValues["LJDM"].ToString().Trim();
        //string strFJB = e.NewValues["FJB"].ToString().Trim();
        //string strDYXH = e.NewValues["DYXH"].ToString().Trim();

        //判断SO是否存在
        string chSql2 = "SELECT LJDM FROM DMFJBZB WHERE LJDM='" + strLJDM + "'";
        DataTable dt2 = dc.GetTable(chSql2);
        if (dt2.Rows.Count == 0)
        {
            e.RowError = "该零件号不存在！";
        }

        //判断是否重复
        if (ASPxGridView1.IsNewRowEditing)
        {
            string chSql = "SELECT LJDM FROM DMFJBZB WHERE LJDM='" + strLJDM + "'";
            DataTable dt = dc.GetTable(chSql);
            if (dt.Rows.Count > 0)
            {
                e.RowError = "该零件号的记录已经存在，请先删除！";
            }
        }
    }

    protected void btnXlsExport_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter1.WriteXlsToResponse("随机带走件包装信息");
    }

}
