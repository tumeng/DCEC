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
 * 功能概述：工位工时维护
 * 作者：游晓航
 * 创建时间：2016-08-09
 */

public partial class Rmes_part1300 : Rmes.Web.Base.BasePage
{
    private dataConn dc = new dataConn();
    private PubCs pc = new PubCs();
    public string theCompanyCode;
    private string theUserId,theUserCode;
    public string theProgramCode;

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theUserId = theUserManager.getUserId();
        theUserCode = theUserManager.getUserCode();
        theProgramCode = "part1300";
        
        
        if (!IsPostBack)
        {

            //initLocation();
            initPlineCode();
        }
        setCondition();

    }
    private void initLocation()
    {
        //初始化多余工位下拉列表
        string sql = "select distinct location_code from ms_location_time where gzdd in (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "')"
                    +"and location_code not in (select location_code from code_location where pline_code in (select pline_id from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "' )) order by location_code";
        SqlLocation.SelectCommand = sql;
        SqlLocation.DataBind();
    }
    private void initPlineCode()
    {
        //初始化生产线下拉列表

        string sql = "select distinct a.pline_code,b.pline_name from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'";
        SqlCode.SelectCommand = sql;
        SqlCode.DataBind();
    }
    private void setCondition()
    {
        string pline = "";
        if (txtPCode.Text.Trim() != "")     
        {
            pline = txtPCode.Value.ToString();
        }
        string sql = "select a.*,b.pline_name from MS_LOCATION_TIME a left join code_product_line b on a.gzdd=b.pline_code where gzdd ='" + pline + "' order by input_time desc nulls last ";
        DataTable dt = dc.GetTable(sql);
        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
        //string sql = "select a.*,b.pline_name from MS_LOCATION_TIME a left join code_product_line b on a.gzdd=b.pline_code where gzdd in "
        //     + "( select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') order by input_time desc nulls last ";
        //DataTable dt = dc.GetTable(sql);
        //ASPxGridView1.DataSource = dt;
        //ASPxGridView1.DataBind();
    }

    //新增
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (txtPCode.Text.Trim() == "")
        {

            return;

        }
        else
        {
            ASPxTextBox uLSeq = ASPxGridView1.FindEditFormTemplateControl("txtLSeq") as ASPxTextBox;
            //ASPxComboBox uPCode = ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;
            ASPxTextBox uLTime = ASPxGridView1.FindEditFormTemplateControl("txtLTime") as ASPxTextBox;
            ASPxComboBox uLCode = ASPxGridView1.FindEditFormTemplateControl("txtLCode") as ASPxComboBox;
            string strLSeq = uLSeq.Text.Trim();
            string strPCode = txtPCode.Value.ToString();
            string strLTime = uLTime.Text.Trim();
            string strLCode = uLCode.Value.ToString();

            string chSql2 = "SELECT LOCATION_SEQ FROM MS_LOCATION_TIME WHERE  LOCATION_SEQ='" + strLSeq + "'  AND GZDD='" + strPCode + "'  ";
            DataTable dt2 = dc.GetTable(chSql2);
            if (dt2.Rows.Count > 0)
            {
                //插入到日志表
                try
                {
                    string Sql2 = "INSERT INTO MS_LOCATION_TIME_LOG (LOCATION_SEQ,LOCATION_CODE,LOCATION_TIME,GZDD,user_code,flag,rqsj)"
                        + " SELECT LOCATION_SEQ,LOCATION_CODE,LOCATION_TIME,GZDD,'"
                        + theUserCode + "' , 'BEFOREEDIT', SYSDATE FROM MS_LOCATION_TIME WHERE LOCATION_SEQ >= '" + strLSeq + "' AND GZDD='" + strPCode + "' ";
                    dc.ExeSql(Sql2);
                }
                catch
                {
                    return;
                }

                string UpSql = "UPDATE MS_LOCATION_TIME SET LOCATION_SEQ = LOCATION_SEQ+1 WHERE LOCATION_SEQ >= '" + strLSeq + "' AND GZDD='" + strPCode + "' ";
                dc.ExeSql(UpSql);

                //插入到日志表
                try
                {
                    string Sql2 = "INSERT INTO MS_LOCATION_TIME_LOG (LOCATION_SEQ,LOCATION_CODE,LOCATION_TIME,GZDD,user_code,flag,rqsj)"
                        + " SELECT LOCATION_SEQ,LOCATION_CODE,LOCATION_TIME,GZDD,'"
                        + theUserCode + "' , 'AFTEREDIT', SYSDATE FROM MS_LOCATION_TIME WHERE LOCATION_SEQ >= '" + strLSeq + "' AND GZDD='" + strPCode + "' ";
                    dc.ExeSql(Sql2);
                }
                catch
                {
                    return;
                }

                e.Cancel = true;

            }
            string Sql = "INSERT INTO MS_LOCATION_TIME (LOCATION_SEQ,LOCATION_CODE,LOCATION_TIME,GZDD,input_person,input_time)VALUES('" + strLSeq + "','" + strLCode + "'," + strLTime + ",'" + strPCode + "','" + theUserId + "',sysdate)";
            dc.ExeSql(Sql);

            //插入到日志表
            try
            {
                string Sql2 = "INSERT INTO MS_LOCATION_TIME_LOG (LOCATION_SEQ,LOCATION_CODE,LOCATION_TIME,GZDD,user_code,flag,rqsj)"
                    + " SELECT LOCATION_SEQ,LOCATION_CODE,LOCATION_TIME,GZDD,'"
                    + theUserCode + "' , 'ADD', SYSDATE FROM MS_LOCATION_TIME WHERE LOCATION_CODE = '" + strLCode + "' AND GZDD='" + strPCode + "' ";
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
    }

    //删除
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        //判断当前记录是否可以删除
        string strLSeq = e.Values["LOCATION_SEQ"].ToString();
        string strLCode = e.Values["LOCATION_CODE"].ToString();
        string strPCode = e.Values["GZDD"].ToString();
        string strTableName = "MS_LOCATION_TIME";

        dataConn theDataConn = new dataConn("select func_check_delete_data('" + strTableName + "','" + theCompanyCode + "','MES','MES','MES','" + strLCode + "') from dual");

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
            //插入到日志表delete
            try
            {
                string Sql2 = "INSERT INTO MS_LOCATION_TIME_LOG (LOCATION_SEQ,LOCATION_CODE,LOCATION_TIME,GZDD,user_code,flag,rqsj)"
                    + " SELECT LOCATION_SEQ,LOCATION_CODE,LOCATION_TIME,GZDD,'"
                    + theUserCode + "' , 'DEL', SYSDATE FROM MS_LOCATION_TIME WHERE LOCATION_CODE = '" + strLCode + "' AND GZDD='" + strPCode + "' ";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }
            //确认删除
            string Sql = "delete from MS_LOCATION_TIME WHERE  LOCATION_CODE = '" + strLCode + "' AND GZDD='" + strPCode + "' ";
            dc.ExeSql(Sql);

            //插入到日志表update
            try
            {
                string Sql2 = "INSERT INTO MS_LOCATION_TIME_LOG (LOCATION_SEQ,LOCATION_CODE,LOCATION_TIME,GZDD,user_code,flag,rqsj)"
                    + " SELECT LOCATION_SEQ,LOCATION_CODE,LOCATION_TIME,GZDD,'"
                    + theUserCode + "' , 'BEFOREEDIT', SYSDATE FROM MS_LOCATION_TIME WHERE LOCATION_SEQ >= '" + strLSeq + "' AND GZDD='" + strPCode + "' ";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }

            string UpSql = "UPDATE MS_LOCATION_TIME SET LOCATION_SEQ = LOCATION_SEQ-1 WHERE  LOCATION_SEQ >= '" + strLSeq + "' AND GZDD = '" + strPCode + "' ";
            dc.ExeSql(UpSql);

            //插入到日志表update
            try
            {
                string Sql2 = "INSERT INTO MS_LOCATION_TIME_LOG (LOCATION_SEQ,LOCATION_CODE,LOCATION_TIME,GZDD,user_code,flag,rqsj)"
                    + " SELECT LOCATION_SEQ,LOCATION_CODE,LOCATION_TIME,GZDD,'"
                    + theUserCode + "' , 'AFTEREDIT', SYSDATE FROM MS_LOCATION_TIME WHERE LOCATION_SEQ >= '" + strLSeq + "' AND GZDD='" + strPCode + "' ";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }

        }


        setCondition();
        e.Cancel = true;
    }

    //修改
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        if (txtPCode.Text.Trim() == "")
        {
            return;
        }
        else
        {
            ASPxTextBox uLSeq = ASPxGridView1.FindEditFormTemplateControl("txtLSeq") as ASPxTextBox;
            //ASPxComboBox uPCode = ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;
            ASPxTextBox uLTime = ASPxGridView1.FindEditFormTemplateControl("txtLTime") as ASPxTextBox;
            ASPxComboBox uLCode = ASPxGridView1.FindEditFormTemplateControl("txtLCode") as ASPxComboBox;
            string strLSeq = uLSeq.Text.Trim();
            string strPCode = txtPCode.Value.ToString();
            string strLTime = uLTime.Text.Trim();
            string strLCode = uLCode.Value.ToString();


            string NstrSEQ = e.NewValues["LOCATION_SEQ"].ToString().Trim();
            string OstrSEQ = e.OldValues["LOCATION_SEQ"].ToString().Trim();

            int Nseq = Convert.ToInt32(NstrSEQ);
            int Oseq = Convert.ToInt32(OstrSEQ);

            //插入到日志表update
            try
            {
                string Sql2 = "INSERT INTO MS_LOCATION_TIME_LOG (LOCATION_SEQ,LOCATION_CODE,LOCATION_TIME,GZDD,user_code,flag,rqsj)"
                    + " SELECT LOCATION_SEQ,LOCATION_CODE,LOCATION_TIME,GZDD,'"
                    + theUserCode + "' , 'BEFOREEDIT', SYSDATE FROM MS_LOCATION_TIME WHERE LOCATION_SEQ <= '" + NstrSEQ + "' and LOCATION_SEQ >'" + OstrSEQ + "'AND GZDD = '" + strPCode + "' ";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }

            if (Nseq > Oseq)
            {
                string UpSql = "UPDATE MS_LOCATION_TIME SET LOCATION_SEQ = LOCATION_SEQ-1 WHERE  LOCATION_SEQ <= '" + NstrSEQ + "' and LOCATION_SEQ >'" + OstrSEQ + "'AND GZDD = '" + strPCode + "'  ";
                dc.ExeSql(UpSql);

                e.Cancel = true;

            }
            else if (Nseq < Oseq)
            {
                string UpSql = "UPDATE MS_LOCATION_TIME SET LOCATION_SEQ = LOCATION_SEQ+1 WHERE  LOCATION_SEQ >= '" + NstrSEQ + "' and LOCATION_SEQ <'" + OstrSEQ + "'AND GZDD = '" + strPCode + "' ";
                dc.ExeSql(UpSql);

                e.Cancel = true;

            }
            //插入到日志表update
            try
            {
                string Sql2 = "INSERT INTO MS_LOCATION_TIME_LOG (LOCATION_SEQ,LOCATION_CODE,LOCATION_TIME,GZDD,user_code,flag,rqsj)"
                    + " SELECT LOCATION_SEQ,LOCATION_CODE,LOCATION_TIME,GZDD,'"
                    + theUserCode + "' , 'AFTEREDIT', SYSDATE FROM MS_LOCATION_TIME WHERE LOCATION_SEQ <= '" + NstrSEQ + "' and LOCATION_SEQ >'" + OstrSEQ + "'AND GZDD = '" + strPCode + "'";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }

            //插入到日志表update again
            try
            {
                string Sql2 = "INSERT INTO MS_LOCATION_TIME_LOG (LOCATION_SEQ,LOCATION_CODE,LOCATION_TIME,GZDD,user_code,flag,rqsj)"
                    + " SELECT LOCATION_SEQ,LOCATION_CODE,LOCATION_TIME,GZDD,'"
                    + theUserCode + "' , 'BEFOREEDIT', SYSDATE FROM MS_LOCATION_TIME WHERE GZDD = '" + strPCode + "' and LOCATION_CODE = '" + strLCode + "' ";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }
            string Sql = "UPDATE MS_LOCATION_TIME SET LOCATION_TIME='" + strLTime + "',LOCATION_SEQ='" + strLSeq + "' WHERE GZDD = '" + strPCode + "' and LOCATION_CODE = '" + strLCode + "'";
            dc.ExeSql(Sql);
            //插入到日志表update again
            try
            {
                string Sql2 = "INSERT INTO MS_LOCATION_TIME_LOG (LOCATION_SEQ,LOCATION_CODE,LOCATION_TIME,GZDD,user_code,flag,rqsj)"
                    + " SELECT LOCATION_SEQ,LOCATION_CODE,LOCATION_TIME,GZDD,'"
                    + theUserCode + "' , 'AFTEREDIT', SYSDATE FROM MS_LOCATION_TIME WHERE GZDD = '" + strPCode + "' and LOCATION_CODE = '" + strLCode + "' ";
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
    }

    //创建EDITFORM前
    protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
    {
        //string Sql = " select distinct a.pline_code,b.pline_name as showtext from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "' ";
        //DataTable dt = dc.GetTable(Sql);
        //ASPxComboBox uPCode = ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;
        //uPCode.DataSource = dt;
        //uPCode.TextField = dt.Columns[1].ToString();
        //uPCode.ValueField = dt.Columns[0].ToString();

        string Sql2 = "", pline_id = "";
        
        if (txtPCode.Text.Trim() != "")
        {
            
             pline_id = dc.GetValue("select rmes_id from code_product_line where pline_code='" + txtPCode.Value.ToString() + "'");
            Sql2 = "select distinct LOCATION_CODE,LOCATION_CODE||' '||LOCATION_NAME from CODE_LOCATION where LOCATION_CODE not like 'OP%' and LOCATION_CODE not in (select location_code from ms_location_time where gzdd ='" + txtPCode.Value.ToString() + "')"
              + "and  pline_code = '" + pline_id + "' order by LOCATION_CODE";
        }
        else
        {
            Sql2 = "select distinct LOCATION_CODE,LOCATION_CODE||' '||LOCATION_NAME from CODE_LOCATION where LOCATION_CODE not like 'OP%' and LOCATION_CODE not in (select location_code from ms_location_time where gzdd in (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "'))"
           + " and  pline_code in (select pline_id from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') order by LOCATION_CODE";
        }
            
        DataTable dt2 = dc.GetTable(Sql2);
        ASPxComboBox uLCode = ASPxGridView1.FindEditFormTemplateControl("txtLCode") as ASPxComboBox;
        uLCode.DataSource = dt2;
        uLCode.TextField = dt2.Columns[1].ToString();
        uLCode.ValueField = dt2.Columns[0].ToString();
     

        if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
        {
            ///主键不可以修改
            (ASPxGridView1.FindEditFormTemplateControl("txtLCode") as ASPxComboBox).Enabled = false;
            //(ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox).Enabled = false;
            
        }
        else
        {
            //uTcode.SelectedIndex = 0;
        }

    }

    //修改数据校验
    protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        string strLCode = e.NewValues["LOCATION_CODE"].ToString().Trim();
        string strPCode = "";
        if (txtPCode.Text.Trim() != "")
        {
            strPCode = txtPCode.Value.ToString();
        }
        else
        {
            e.RowError = "请先选择要维护的生产线！";
            return;
        }

        //判断是否重复
        if (ASPxGridView1.IsNewRowEditing)
        {
            string chSql = "select * from ms_location_time where location_code='" + strLCode + "' and gzdd='" + strPCode + "'";
            DataTable dt = dc.GetTable(chSql);
            if (dt.Rows.Count > 0)
            {
                e.RowError = "已存在相同记录！";
            }
            
        }

    }
    protected void btnXlsExport_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter1.WriteXlsToResponse("工位工时信息");
    }

    protected void ASPxListBoxLocation_Callback(object sender, CallbackEventArgsBase e)
    {
        string pline = e.Parameter;
        string sql = "select distinct location_code from ms_location_time where gzdd ='"+pline+"'"
                   + "and location_code not in (select location_code from code_location ) order by location_code";
        SqlLocation.SelectCommand = sql;
        SqlLocation.DataBind();
        ASPxListBoxLocation.DataBind();

    }

    protected void ASPxGridView1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        setCondition();
    }


}
