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
using Rmes.Pub.Data;
using Rmes.DA.Base;
/**
 * 功能概述：经济版标定维护,INSITE标定维护（增删改）
 * 作者：游晓航
 * 创建时间：2016-07-11
 */

public partial class Rmes_atpu2100 : Rmes.Web.Base.BasePage
{
    private dataConn dc = new dataConn();
    private PubCs pc = new PubCs();
    public string theCompanyCode,theUserId;
    public DateTime theTime;

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        
        theTime =  DateTime.Now;
        theUserId = theUserManager.getUserName();
       
        setCondition();
    }
    private void setCondition()
    {
        string sql = "SELECT * FROM ATPUFR order by INPUT_TIME desc nulls last ";  //WHERE YHMC='" + theUserId + "'
        DataTable dt = dc.GetTable(sql);
        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
        string sql2 = "SELECT * FROM ATPUFRLOG order by RQSJ desc   ";  //WHERE YHMC='" + theUserId + "'
        DataTable dt2 = dc.GetTable(sql2);
        ASPxGridView2.DataSource = dt2;
        ASPxGridView2.DataBind();

    }
    public static bool IsInt(string str)
    {
        if (str == string.Empty)
            return false;
        try
        {
            Convert.ToInt32(str);
            return true;
        }
        catch
        {
            return false;
        }
    }

    //新增
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxComboBox txtFR = ASPxGridView1.FindEditFormTemplateControl("txtFR") as ASPxComboBox;  
        ASPxComboBox txtSC = ASPxGridView1.FindEditFormTemplateControl("txtSC") as ASPxComboBox;
        ASPxComboBox txtDO = ASPxGridView1.FindEditFormTemplateControl("txtDO") as ASPxComboBox;
        ASPxTextBox txtSZTS = ASPxGridView1.FindEditFormTemplateControl("txtSZTS") as ASPxTextBox;
        //ASPxTextBox txtYZTS = ASPxGridView1.FindEditFormTemplateControl("txtYZTS") as ASPxTextBox;
        ASPxComboBox txtSFPDCK = ASPxGridView1.FindEditFormTemplateControl("txtSFPDCK") as ASPxComboBox;

        string strFR = txtFR.Value.ToString();
        string strSC = txtSC.Value.ToString();
        string strDO = txtDO.Value.ToString();
        string strSZTS = txtSZTS.Text.Trim();
        string strYZTS = "0";// txtYZTS.Text.Trim();
         //strSFPDCK = txtSFPDCK.Text.Trim();

        //判断是否以zz结尾，如果是，去掉ZZ，再插入数据库
        string mFR = strFR.Remove(0, strFR.Length - 2);
        string newFR = " ";
        if (mFR == "ZZ")
        {

            newFR = strFR.Substring(0, strFR.Length - 2);
            strFR = newFR;
        }

        string Sql = "INSERT INTO ATPUFR(FR,SC,DO,YHMC,RQSJ,SFPDCK,SZTS,INPUT_PERSON,INPUT_TIME) "
                   + "VALUES('" + strFR + "','" + strSC + "', '" + strDO + "','" + theUserId + "',to_date('" + theTime + "','yyyy-mm-dd hh24:mi:ss'),'" + txtSFPDCK.Text.Trim() + "','" + strSZTS + "','"+theUserId+"',sysdate)";
        dc.ExeSql(Sql);
        //插入日志表
        string Sql2 = "INSERT INTO ATPUFRLOG(FR,SC,DO,YHMC,CZSM,RQSJ,SFPDCK,SZTS,YZTS) "
                   + "VALUES('" + strFR + "','" + strSC + "', '" + strDO + "','" + theUserId + "','ADD ',to_date('" + theTime + "','yyyy-mm-dd hh24:mi:ss'),'" + txtSFPDCK.Text.Trim() + "','" + strSZTS + "','" + strYZTS + "')";
        dc.ExeSql(Sql2);
        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        setCondition();
        ASPxGridView1.JSProperties.Add("cpCallbackName", "refresh");
        
    }

    //删除
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        //判断当前记录是否可以删除
        string strFR = e.Values["FR"].ToString();
        string strSC = e.Values["SC"].ToString();
        string strDO = e.Values["DO"].ToString();
        string strSZTS = e.Values["SZTS"].ToString().Trim();
        string strYZTS = e.Values["YZTS"].ToString().Trim();
        int YZTS = Convert.ToInt32(strYZTS);
        
        string chSql = "SELECT * FROM ATPUFR WHERE FR='" + strFR + "' AND SC='" + strSC + "' AND DO='" + strDO + "'";
        DataTable dt = dc.GetTable(chSql);
        
        int theRet = dt.Rows.Count;
        if (theRet==0)
        {
           
                ASPxGridView1.JSProperties.Add("cpCallbackName", "Delete");
                ASPxGridView1.JSProperties.Add("cpCallbackRet", theRet);
                //theDataConn.CloseConn();
        }
        else 
        {
            if (YZTS > 0)
            {
                ASPxGridView1.JSProperties.Add("cpCallbackName", "noDelete");

            }
            else 
            {
                //插入日志表
                try
                {
                    string Sql2 = "INSERT INTO ATPUFRLOG(FR,SC,DO,YHMC,CZSM,RQSJ,SFPDCK,SZTS,YZTS) "
                                + "VALUES('" + strFR + "','" + strSC + "', '" + strDO + "','" + theUserId + "','DEL ',to_date('" + theTime + "','yyyy-mm-dd hh24:mi:ss'),'" + e.Values["SFPDCK"] + "','" + strSZTS + "','" + strYZTS + "')";
                    dc.ExeSql(Sql2);
                }
                catch
                {
                    return;
                }

                string Sql = "delete from ATPUFR WHERE FR = '" + strFR + "' and SC = '" + strSC + "' and DO = '" + strDO + "'";
                dc.ExeSql(Sql);
            }
            //确认删除
           
        }
        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        setCondition();
        ASPxGridView1.JSProperties.Add("cpCallbackName", "refresh");
    }

    //修改
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        string oldstrFR = e.OldValues["FR"].ToString();
        string oldstrSC = e.OldValues["SC"].ToString();
        string oldstrDO = e.OldValues["DO"].ToString();
        ASPxComboBox txtFR = ASPxGridView1.FindEditFormTemplateControl("txtFR") as ASPxComboBox;
        ASPxComboBox txtSC = ASPxGridView1.FindEditFormTemplateControl("txtSC") as ASPxComboBox;
        ASPxComboBox txtDO = ASPxGridView1.FindEditFormTemplateControl("txtDO") as ASPxComboBox;
        ASPxTextBox txtSZTS = ASPxGridView1.FindEditFormTemplateControl("txtSZTS") as ASPxTextBox;
        ASPxTextBox txtYZTS = ASPxGridView1.FindEditFormTemplateControl("txtYZTS") as ASPxTextBox;
        ASPxComboBox txtSFPDCK = ASPxGridView1.FindEditFormTemplateControl("txtSFPDCK") as ASPxComboBox;

        string strFR = txtFR.Value.ToString();
        string strSC = txtSC.Value.ToString();
        string strDO = txtDO.Value.ToString();
        string strSZTS = txtSZTS.Text.Trim();
        string strYZTS = txtYZTS.Text.Trim();
       // string strSFPDCK = txtSFPDCK.Value.ToString();

        string Sql = "UPDATE ATPUFR SET FR='" + strFR + "',SC='" + strSC + "',DO='" + strDO + "',YHMC='" + theUserId + "',RQSJ=to_date('" + theTime + "','yyyy-mm-dd hh24:mi:ss'),SZTS='" + strSZTS + "',YZTS='" + strYZTS + "',SFPDCK='" + txtSFPDCK.Text.Trim() + "',"
                   +"input_person='"+theUserId+"',input_time=sysdate "
                   + " WHERE FR='" + oldstrFR + "'and SC='" + oldstrSC + "'and DO='" + oldstrDO + "'";
        dc.ExeSql(Sql);
        //插入到日志表20161102
        try
        {
            string aaa = e.OldValues["SFPDCK"].ToString();
            string bbb = e.OldValues["SZTS"].ToString();
            string ccc = e.OldValues["YZTS"].ToString();
            string Sql2 = " INSERT INTO ATPUFRLOG(FR,SC,DO,YHMC,CZSM,RQSJ,SFPDCK,SZTS,YZTS)"
                           + " VALUES( '"
                           + e.OldValues["FR"].ToString() + "','"
                           + e.OldValues["SC"].ToString() + "','"
                           + e.OldValues["DO"].ToString() + "','"
                           + theUserId + "','"
                           + "BEFOREEDIT',sysdate,'"
                           + e.OldValues["SFPDCK"].ToString() + "','"
                           + e.OldValues["SZTS"].ToString() + "','"
                           + e.OldValues["YZTS"].ToString() + "') ";
            dc.ExeSql(Sql2);
            string Sql3 = " INSERT INTO ATPUFRLOG(FR,SC,DO,YHMC,CZSM,RQSJ,SFPDCK,SZTS,YZTS)"
                           + " VALUES( '"
                           + e.NewValues["FR"].ToString() + "','"
                           + e.NewValues["SC"].ToString() + "','"
                           + e.NewValues["DO"].ToString() + "','"
                           + theUserId + "','"
                           + "AFTEREDIT',sysdate,'"
                           + e.NewValues["SFPDCK"].ToString() + "','"
                           + e.NewValues["SZTS"].ToString() + "','"
                           + e.NewValues["YZTS"].ToString() + "') ";
            dc.ExeSql(Sql3);
        }
        catch
        {
            return;
        }

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        setCondition();
        ASPxGridView1.JSProperties.Add("cpCallbackName", "refresh");
    }

    //创建EDITFORM前
    protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
    {

        string Sql = "select distinct pt_part,pt_part as showtext from copy_pt_mstr where pt_part like 'FR%' and pt_part not like '%ZZ'";
        DataTable dt = dc.GetTable(Sql);
        ASPxComboBox uFR = ASPxGridView1.FindEditFormTemplateControl("txtFR") as ASPxComboBox;
        uFR.DataSource = dt;
        uFR.TextField = dt.Columns[1].ToString();
        uFR.ValueField = dt.Columns[0].ToString();

        string Sql1 = "select distinct pt_part,pt_part as showtext from copy_pt_mstr where pt_part like 'SC%' and pt_part not like '%ZZ'";
        DataTable dt1 = dc.GetTable(Sql1);

        ASPxComboBox txtSC = ASPxGridView1.FindEditFormTemplateControl("txtSC") as ASPxComboBox;
        txtSC.DataSource = dt1;
        txtSC.TextField = dt1.Columns[1].ToString();
        txtSC.ValueField = dt1.Columns[0].ToString();


        string Sql2 = "select distinct pt_part,pt_part as showtext from copy_pt_mstr where pt_part like 'DO%' and pt_part not like '%ZZ'";
        DataTable dt2 = dc.GetTable(Sql2);

        ASPxComboBox txtDO = ASPxGridView1.FindEditFormTemplateControl("txtDO") as ASPxComboBox;

        txtDO.DataSource = dt2;
        txtDO.TextField = dt2.Columns[1].ToString();
        txtDO.ValueField = dt2.Columns[0].ToString();
        if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
        {
            ///主键不可以修改
            (ASPxGridView1.FindEditFormTemplateControl("txtFR") as ASPxComboBox).Enabled = false;
            (ASPxGridView1.FindEditFormTemplateControl("txtSC") as ASPxComboBox).Enabled = false;
            (ASPxGridView1.FindEditFormTemplateControl("txtDO") as ASPxComboBox).Enabled = false;
        }
        //else
        //{
        //    //uTcode.SelectedIndex = 0;
        //}

    }

    //修改数据校验
    protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        //判断为空
        if (e.NewValues["FR"].ToString() == "" || e.NewValues["FR"].ToString() == null)
        {
            e.RowError = "FR不能为空！";
        }
        if (e.NewValues["SC"].ToString() == "" || e.NewValues["SC"].ToString() == null)
        {
            e.RowError = "SC不能为空！";
        }
        if (e.NewValues["DO"].ToString() == "" || e.NewValues["DO"].ToString() == null)
        {
            e.RowError = "DO不能为空！";
        }

        string strFR = e.NewValues["FR"].ToString().Trim();
        string strSC = e.NewValues["SC"].ToString().Trim();
        string strDO = e.NewValues["DO"].ToString().Trim();
        string strSZTS = e.NewValues["SZTS"].ToString().Trim();
    
        //判断是否合法
        string chSqlFR = "select pt_part from copy_pt_mstr"
                + " WHERE pt_part='" + e.NewValues["FR"].ToString() + "'";
        DataTable dtFR = dc.GetTable(chSqlFR);
        if (dtFR.Rows.Count == 0)
        {
            e.RowError = "FR数据非法！请重新维护";
        }
        string chSqlSC = "select pt_part from copy_pt_mstr"
                + " WHERE pt_part='" + e.NewValues["SC"].ToString() + "'";
        DataTable dtSC = dc.GetTable(chSqlSC);
        if (dtSC.Rows.Count == 0)
        {
            e.RowError = "SC数据非法！请重新维护";
        }
        string chSqlDO = "select pt_part from copy_pt_mstr"
                + " WHERE pt_part='" + e.NewValues["DO"].ToString() + "'";
        DataTable dtDO = dc.GetTable(chSqlDO);
        if (dtDO.Rows.Count == 0)
        {
            e.RowError = "DO数据非法！请重新维护";
        }

        //判断超长
        if (strFR.Length > 30)
        {
            e.RowError = "FR字节长度不能超过30！";
        }
        if (strSC.Length > 30)
        {
            e.RowError = "SC字节长度不能超过30！";
        }
        if (strDO.Length > 30)
        {
            e.RowError = "DO字节长度不能超过30！";
        }
        //判断试装台数是否为整数
        if (!IsInt(strSZTS))
        {
            e.RowError = "试装台数必须为整数!";
        }

        //判断是否重复
        if (ASPxGridView1.IsNewRowEditing)
        {
            
            string chSql = "SELECT * FROM ATPUFR WHERE FR='" + strFR + "' AND SC='" + strSC + "' AND DO='" + strDO + "'";
            DataTable dt = dc.GetTable(chSql);
            if (dt.Rows.Count > 0)
            {
                e.RowError = "该FR/SC/DO对应的记录已经存在,请重新维护!";
            }
            
        }
        if (ASPxGridView1.IsEditing) 
        {
            //判断FR状态是否非法
            //string chSql2 = "select upper(pt_status) from copy_pt_mstr where pt_part=upper('" + e.NewValues["FR"].ToString() + "')";
            //string dt2 = dc.GetValue(chSql2);
            //if (dt2 != "P" && dt2 != "L")
            //{
            //    e.RowError = "状态非法，请重新维护！";
            //}

            //判断重复
            //string strOldFR = e.OldValues["FR"].ToString().Trim();
            //string strOldSC = e.OldValues["SC"].ToString().Trim();
            //string strOldDO = e.OldValues["DO"].ToString().Trim();
            //string chSql = "SELECT FR,SC,DO FROM ATPUFR WHERE FR='" + strOldFR + "' AND SC='" + strOldSC + "' AND DO='" + strOldDO + "'";
            //DataTable dt = dc.GetTable(chSql);
            //if (dt.Rows.Count > 0)
            //{
            //    e.RowError = "该FR/SC/DO对应的记录已经存在,请重新维护!";
            //}
        }

    }

    protected void ASPxGridView2_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        string sql2 = "SELECT * FROM ATPUFRLOG order by RQSJ desc   ";  //WHERE YHMC='" + theUserId + "'
        DataTable dt2 = dc.GetTable(sql2);
        ASPxGridView2.DataSource = dt2;
        ASPxGridView2.DataBind();
    }
    


}
