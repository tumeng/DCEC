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
 * 功能概述：流水号号段定义
 * 作者：游晓航
 * 创建时间：2016-06-15
 */

public partial class Rmes_atpu1200 : Rmes.Web.Base.BasePage
{
    private dataConn dc = new dataConn();
    private PubCs pc = new PubCs();
    public string theCompanyCode, theUserId, theUserCode;
    public string theProgramCode;
    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theUserId = theUserManager.getUserId();
        theUserCode = theUserManager.getUserCode();
        theProgramCode = "atpu1200";
        setCondition();

    }

    private void setCondition()
    {
         
        string sql = "SELECT T.RMES_ID,T.COMPANY_CODE,T.PLINE_CODE,B.PLINE_NAME,T.INITIAL_VALUE,T.CURRENT_VALUE,T.MAX_VALUE,T.INCREASE_FLAG,A.INTERNAL_NAME, "
            + "decode(T.ENABLE_FLAG,'Y','是','N','否','C','已用完') ENABLE_FLAG1,T.ENABLE_FLAG,T.WARNING_VALUE,T.VENDER_CODE FROM CODE_SN T left join CODE_INTERNAL A ON A.INTERNAL_CODE=T.INCREASE_FLAG "
            + " LEFT JOIN  CODE_PRODUCT_LINE B ON T.PLINE_CODE=B.PLINE_CODE WHERE T.COMPANY_CODE ='" + theCompanyCode + "' AND A.INTERNAL_TYPE_CODE='003' "
            + "and T.PLINE_CODE IN (SELECT PLINE_CODE FROM VW_USER_ROLE_PROGRAM WHERE USER_ID='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "')  order by t.INPUT_TIME desc nulls last";
        DataTable dt = dc.GetTable(sql);

        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();

        string sql2 = "SELECT A.RMES_ID,A.COMPANY_CODE,A.PLINE_CODE,B.PLINE_NAME,A.SN,C.INTERNAL_NAME,A.SN_FLAG FROM CODE_SN_RESERVE A LEFT JOIN CODE_PRODUCT_LINE B "
            + "ON A.PLINE_CODE=B.PLINE_CODE LEFT JOIN CODE_INTERNAL C ON C.INTERNAL_CODE=A.SN_FLAG WHERE A.COMPANY_CODE='" + theCompanyCode + "' AND C.INTERNAL_TYPE_CODE='004'"
            + " and A.PLINE_CODE IN (SELECT PLINE_CODE FROM VW_USER_ROLE_PROGRAM WHERE USER_ID='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') order by a.input_time desc nulls last,  A.SN  ";
       

        DataTable dt2 = dc.GetTable(sql2);

        ASPxGridView2.DataSource = dt2;
        ASPxGridView2.DataBind();
    }

    //新增
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxComboBox uPCode = ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;
        ASPxTextBox uSName = ASPxGridView1.FindEditFormTemplateControl("txtSName") as ASPxTextBox;
        ASPxTextBox uSinitial = ASPxGridView1.FindEditFormTemplateControl("txtSinitial") as ASPxTextBox;
      
        ASPxTextBox uSmax = ASPxGridView1.FindEditFormTemplateControl("txtSmax") as ASPxTextBox;
        ASPxTextBox uSwarning = ASPxGridView1.FindEditFormTemplateControl("txtSwarning") as ASPxTextBox;
        ASPxComboBox uIflag = ASPxGridView1.FindEditFormTemplateControl("ASPxComboBox1") as ASPxComboBox;
        ASPxComboBox uEflag = ASPxGridView1.FindEditFormTemplateControl("ASPxComboBox2") as ASPxComboBox;

        string strPCode = uPCode.Value.ToString();
        string strSinitial = uSinitial.Text.Trim();
        string strSName = uSName.Text.Trim();
        
        string strSmax = uSmax.Text.Trim();
        string strSwarning = uSwarning.Text.Trim();

        string strIflag = uIflag.Value.ToString();
        string strEflag = uEflag.Value.ToString();

        char chr = Convert.ToChar(strIflag);
        //取RMES_ID的值
        string sql_rmes_id = "SELECT SEQ_RMES_ID.NEXTVAL FROM DUAL ";
        dc.setTheSql(sql_rmes_id);
        string rmes_id = dc.GetTable().Rows[0][0].ToString();

        if (chr== 'A')
        {
            
            string Sql = "INSERT INTO CODE_SN (RMES_ID,COMPANY_CODE,PLINE_CODE,INITIAL_VALUE,CURRENT_VALUE,MAX_VALUE,INCREASE_FLAG,ENABLE_FLAG,WARNING_VALUE,VENDER_CODE,INPUT_PERSON,INPUT_TIME) "
                 + "VALUES(SEQ_RMES_ID.NextVal, '" + theCompanyCode + "','" + strPCode + "','" + strSinitial + "','" + strSinitial + "','" + strSmax + "','" + strIflag + "','" + strEflag + "','" + strSwarning + "','" + strSName + "','" + theUserId + "',sysdate)";
            dc.ExeSql(Sql);

            //插入到日志表20161101
            try
            {
                string Sql2 = "INSERT INTO CODE_SN_LOG (RMES_ID,COMPANY_CODE,PLINE_CODE,INITIAL_VALUE,CURRENT_VALUE,MAX_VALUE,INCREASE_FLAG,ENABLE_FLAG,WARNING_VALUE,VENDER_CODE,user_code,flag,rqsj)"
                     + "VALUES('" + rmes_id + "','" + theCompanyCode + "','" + strPCode + "','" + strSinitial + "','" + strSinitial + "','" + strSmax + "','" + strIflag + "','" + strEflag + "','" + strSwarning + "','" + strSName + "','" + theUserCode + "','ADD',SYSDATE)";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }
        }
        else 
        {
           
            string Sql = "INSERT INTO CODE_SN (RMES_ID,COMPANY_CODE,PLINE_CODE,INITIAL_VALUE,CURRENT_VALUE,MAX_VALUE,INCREASE_FLAG,ENABLE_FLAG,WARNING_VALUE,VENDER_CODE,INPUT_PERSON,INPUT_TIME) "
                 + "VALUES(SEQ_RMES_ID.NextVal, '" + theCompanyCode + "','" + strPCode + "','" + strSinitial + "','" + strSmax + "','" + strSmax + "','" + strIflag + "','" + strEflag + "','" + strSwarning + "','" + strSName + "','" + theUserId + "',sysdate)";
            dc.ExeSql(Sql);

            //插入到日志表20161101
            try
            {
                string Sql2 = "INSERT INTO CODE_SN_LOG (RMES_ID,COMPANY_CODE,PLINE_CODE,INITIAL_VALUE,CURRENT_VALUE,MAX_VALUE,INCREASE_FLAG,ENABLE_FLAG,WARNING_VALUE,VENDER_CODE,user_code,flag,rqsj)"
                         + "VALUES('" + rmes_id + "','" + theCompanyCode + "','" + strPCode + "','" + strSinitial + "','" + strSmax + "','" + strSmax + "','" + strIflag + "','" + strEflag + "','" + strSwarning + "','" + strSName + "','" + theUserCode + "','ADD',SYSDATE)";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }
        }
  
        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        setCondition();
    }

    //删除
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        //判断当前记录是否可以删除
        string strCode = e.Values["RMES_ID"].ToString();
        string strTableName = "CODE_SN";

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
            //确认删除
            //插入到日志表20161101
            try
            {
                string Sql1 = " SELECT * FROM CODE_SN WHERE rmes_id='" + strCode + "'";
                dc.setTheSql(Sql1);
                string rmes_id = dc.GetTable().Rows[0]["RMES_ID"].ToString();
                string company_code = dc.GetTable().Rows[0]["COMPANY_CODE"].ToString();
                string pline_code = dc.GetTable().Rows[0]["PLINE_CODE"].ToString();
                string INITIAL_VALUE = dc.GetTable().Rows[0]["INITIAL_VALUE"].ToString();
                string CURRENT_VALUE = dc.GetTable().Rows[0]["CURRENT_VALUE"].ToString();
                string MAX_VALUE = dc.GetTable().Rows[0]["MAX_VALUE"].ToString();
                string INCREASE_FLAG = dc.GetTable().Rows[0]["INCREASE_FLAG"].ToString();
                string ENABLE_FLAG = dc.GetTable().Rows[0]["ENABLE_FLAG"].ToString();
                string WARNING_VALUE = dc.GetTable().Rows[0]["WARNING_VALUE"].ToString();
                string VENDER_CODE = dc.GetTable().Rows[0]["VENDER_CODE"].ToString();

                string Sql2 = "INSERT INTO CODE_SN_LOG (RMES_ID,COMPANY_CODE,PLINE_CODE,INITIAL_VALUE,CURRENT_VALUE,MAX_VALUE,INCREASE_FLAG,ENABLE_FLAG,WARNING_VALUE,VENDER_CODE,user_code,flag,rqsj)"
                         + "VALUES('" + rmes_id + "','" + theCompanyCode + "','" + pline_code + "','" + INITIAL_VALUE + "','" + CURRENT_VALUE + "','" + MAX_VALUE + "','" + INCREASE_FLAG + "','" + ENABLE_FLAG + "','" + WARNING_VALUE + "','" + VENDER_CODE + "','" + theUserCode + "','DEL',SYSDATE)";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }

            string Sql = "delete from CODE_SN WHERE  COMPANY_CODE = '" + theCompanyCode + "' and RMES_ID = '" + strCode + "'";
            dc.ExeSql(Sql);
        }


        setCondition();
        e.Cancel = true;
    }

    //修改
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        
        ASPxComboBox uPCode = ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;
        ASPxTextBox uSName = ASPxGridView1.FindEditFormTemplateControl("txtSName") as ASPxTextBox;
        ASPxTextBox uSinitial = ASPxGridView1.FindEditFormTemplateControl("txtSinitial") as ASPxTextBox;
        
        ASPxTextBox uSmax = ASPxGridView1.FindEditFormTemplateControl("txtSmax") as ASPxTextBox;
        ASPxTextBox uSwarning = ASPxGridView1.FindEditFormTemplateControl("txtSwarning") as ASPxTextBox;
        ASPxComboBox uIflag = ASPxGridView1.FindEditFormTemplateControl("ASPxComboBox1") as ASPxComboBox;
        ASPxComboBox uEflag = ASPxGridView1.FindEditFormTemplateControl("ASPxComboBox2") as ASPxComboBox;

        string strPCode = uPCode.Value.ToString();
        string strSinitial = uSinitial.Text.Trim();
        string strSName = uSName.Text.Trim();
      
        string strSmax = uSmax.Text.Trim();
        string strSwarning = uSwarning.Text.Trim();

        string strIflag = uIflag.Value.ToString();
        string strEflag = uEflag.Value.ToString();
       
        string strID = e.OldValues["RMES_ID"].ToString().Trim();

        //插入到日志表
        try
        {
            string Sql2 = " INSERT INTO CODE_SN_LOG (RMES_ID,COMPANY_CODE,PLINE_CODE,INITIAL_VALUE,CURRENT_VALUE,MAX_VALUE,INCREASE_FLAG,ENABLE_FLAG,WARNING_VALUE,VENDER_CODE,user_code,flag,rqsj)"
                           + " VALUES( '" + strID + "','" + theCompanyCode + "','" + e.OldValues["PLINE_CODE"].ToString() + "','" + e.OldValues["INITIAL_VALUE"].ToString() + "','"
                           + e.OldValues["CURRENT_VALUE"].ToString() + "','"
                           + e.OldValues["MAX_VALUE"].ToString() + "','"
                           + e.OldValues["INCREASE_FLAG"].ToString() + "','"
                           + e.OldValues["ENABLE_FLAG"].ToString() + "','"
                           + e.OldValues["WARNING_VALUE"].ToString() + "','"
                           + e.OldValues["VENDER_CODE"].ToString() + "','"
                           + theUserCode + "','BEFOREEDIT',SYSDATE) ";
            dc.ExeSql(Sql2);
            string Sql3 = " INSERT INTO CODE_SN_LOG (RMES_ID,COMPANY_CODE,PLINE_CODE,INITIAL_VALUE,CURRENT_VALUE,MAX_VALUE,INCREASE_FLAG,ENABLE_FLAG,WARNING_VALUE,VENDER_CODE,user_code,flag,rqsj)"
                           + " VALUES( '" + strID + "','" + theCompanyCode + "','" + e.NewValues["PLINE_CODE"].ToString() + "','" + e.NewValues["INITIAL_VALUE"].ToString() + "','"
                           + e.NewValues["CURRENT_VALUE"].ToString() + "','"
                           + e.NewValues["MAX_VALUE"].ToString() + "','"
                           + e.NewValues["INCREASE_FLAG"].ToString() + "','"
                           + e.NewValues["ENABLE_FLAG"].ToString() + "','"
                           + e.NewValues["WARNING_VALUE"].ToString() + "','"
                           + e.NewValues["VENDER_CODE"].ToString() + "','"
                           + theUserCode + "','AFTEREDIT',SYSDATE) ";
            dc.ExeSql(Sql3);
        }
        catch
        {
            return;
        }
       
        string Sql = "UPDATE CODE_SN SET PLINE_CODE='" + strPCode + "',INITIAL_VALUE='" + strSinitial + "',MAX_VALUE='" + strSmax + "',INCREASE_FLAG='" + strIflag + "',ENABLE_FLAG='" + strEflag + "',WARNING_VALUE='" + strSwarning + "',VENDER_CODE='" + strSName + "',INPUT_PERSON='"+theUserId+"',INPUT_TIME=SYSDATE "
             + " WHERE COMPANY_CODE = '" + theCompanyCode + "' and RMES_ID= '" + strID + "'";
        dc.ExeSql(Sql);

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        setCondition();
    }

    //创建EDITFORM前
    protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
    {
       // txtPCode.value = 'E';
        string Sql = " select distinct a.pline_code,b.pline_name from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "' ";
        DataTable dt = dc.GetTable(Sql);

        ASPxComboBox uPCode = ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;
        
        uPCode.DataSource = dt;
        uPCode.TextField = dt.Columns[1].ToString();
        uPCode.ValueField = dt.Columns[0].ToString();
        //uPCode.SelectedIndex = uPCode.Items.Count >= 0 ? 0 : -1;
        if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
        {
            ///主键不可以修改
            string strIflag = ASPxGridView1.GetRowValues(ASPxGridView1.EditingRowVisibleIndex, new string[] { "INCREASE_FLAG" }).ToString();

            char chr = Convert.ToChar(strIflag);
            (ASPxGridView1.FindEditFormTemplateControl("ASPxComboBox1") as ASPxComboBox).Enabled = false;
            if (chr == 'A')
            {
                (ASPxGridView1.FindEditFormTemplateControl("txtSinitial") as ASPxTextBox).Enabled = false;
            }
            else
            { 
                (ASPxGridView1.FindEditFormTemplateControl("txtSmax") as ASPxTextBox).Enabled = false;
            }

            
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
        if (e.NewValues["INITIAL_VALUE"].ToString() == "" || e.NewValues["INITIAL_VALUE"].ToString() == null)
        {
            e.RowError = "初始值不能为空！";
            return;
        }
        if (e.NewValues["MAX_VALUE"].ToString() == "" || e.NewValues["MAX_VALUE"].ToString() == null)
        {
            e.RowError = "最大值不能为空！";
            return;
        }

        string strMin = e.NewValues["INITIAL_VALUE"].ToString().Trim();
        string strMax = e.NewValues["MAX_VALUE"].ToString().Trim();
        string strWar = e.NewValues["WARNING_VALUE"].ToString().Trim();
        int sMin = Convert.ToInt32(strMin);
        int sMax = Convert.ToInt32(strMax);
        int sWar = Convert.ToInt32(strWar);
          //判断字符串长度
            if (strMin.Length != 8)
            {
                e.RowError = "初始值字节长度只能是8位！";
                return;
            }
            if (strMax.Length != 8)
            {
                e.RowError = "最大值字节长度只能是8位！";
                return;
            }
            if (strWar.Length != 8)
            {
                e.RowError = "提醒值字节长度只能是8位！";
                return;
            }
            if (sMin > sMax)
            {
                e.RowError = "数据不合法，最大值应该大于最小值！";
                return;
            }
            if (sWar<sMin ||sWar> sMax)
            {
                e.RowError = "数据不合法，提醒值应介于最大值和最小值之间！";
                return;
            }
        //修改时判断是否重复
        if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
        {
            string strOldMin = e.OldValues["INITIAL_VALUE"].ToString().Trim();
            string strOldMax = e.OldValues["MAX_VALUE"].ToString().Trim();
            string strIflag = ASPxGridView1.GetRowValues(ASPxGridView1.EditingRowVisibleIndex, new string[] { "INCREASE_FLAG" }).ToString();

            char chr = Convert.ToChar(strIflag);
            string strID = e.OldValues["RMES_ID"].ToString().Trim();
            string strCValue = e.OldValues["CURRENT_VALUE"].ToString().Trim();
            int sCValue = Convert.ToInt32(strCValue);

          
             if (chr == 'A')
            {
                if (sMax < sCValue) { e.RowError = "最大值不能小于当前值！"; }
            }
            else if (chr == 'D')
            {
                if (sMin > sCValue) { e.RowError = "最小值不能大于当前值！"; }
            }

            
                string Sql = "SELECT INITIAL_VALUE, MAX_VALUE,PLINE_CODE  FROM CODE_SN WHERE COMPANY_CODE = '" + theCompanyCode + "' and RMES_ID<>'" + strID + "'  ";
                DataTable dt1 = dc.GetTable(Sql);
                for (int i = 0; i < dt1.Rows.Count; i++)
                {


                    string min = dt1.Rows[i][0].ToString();
                    int mi = Convert.ToInt32(min);
                    string max = dt1.Rows[i][1].ToString();
                    int ma = Convert.ToInt32(max);
                    if ((sMin < mi && sMax < mi) || (sMin > ma && sMax > ma))
                    {
                        continue;
                    }
                    else
                    {
                        string strCode = dt1.Rows[i][2].ToString();
                        e.RowError = "该流水号段已经被'" + strCode + "'生产线使用！";
                        break;
                    }
                }

            
        }
        
       
       

        //新增时判断是否重复
        if (ASPxGridView1.IsNewRowEditing)
        {
                string Sql = "SELECT INITIAL_VALUE, MAX_VALUE,PLINE_CODE  FROM CODE_SN WHERE COMPANY_CODE = '" + theCompanyCode + "'  ";
                DataTable dt1 = dc.GetTable(Sql);
                for (int i = 0; i < dt1.Rows.Count; i++)
                {

                    
                    string min = dt1.Rows[i][0].ToString();
                    int mi = Convert.ToInt32(min);
                    string max = dt1.Rows[i][1].ToString();
                    int ma = Convert.ToInt32(max);
                    if ((sMin < mi && sMax < mi) || (sMin > ma && sMax > ma))
                    {
                        continue;
                    }
                    else
                    {
                        string strCode = dt1.Rows[i][2].ToString();
                        e.RowError = "该流水号段已经被'" + strCode + "'生产线使用！";
                        break;
                    }
                }
        }
      
    }
    protected void ASPxGridView1_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.VisibleIndex < 0) return;
        string status = e.GetValue("ENABLE_FLAG").ToString();
        switch (status)
        {
            case "Y":
                e.Row.BackColor = System.Drawing.Color.LimeGreen;
                break;
            case "N":
                e.Row.BackColor = System.Drawing.Color.Red;
                break;
            case "C":
                e.Row.BackColor = System.Drawing.Color.Yellow;
                break;
        }
    }
    protected void ASPxGridView1_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
        string sn_flag = grid.GetRowValues(e.VisibleIndex, "ENABLE_FLAG") as string;

        if (sn_flag == "C")
            e.Enabled = false;
        else
            e.Enabled = true;
    }

    protected void ASPxGridView2_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxComboBox uPCode = ASPxGridView2.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;
        ASPxTextBox uSN = ASPxGridView2.FindEditFormTemplateControl("txtSN") as ASPxTextBox;
        ASPxComboBox uSNflag = ASPxGridView2.FindEditFormTemplateControl("ASPxComboBox3") as ASPxComboBox;
        

        string strPCode = uPCode.Value.ToString();
        string strSN = uSN.Text.Trim();
        string strSNflag = uSNflag.Value.ToString();

        //取RMES_ID的值
        string sql_rmes_id = "SELECT SEQ_RMES_ID.NEXTVAL FROM DUAL ";
        dc.setTheSql(sql_rmes_id);
        string rmes_id = dc.GetTable().Rows[0][0].ToString();

        string Sql = "INSERT INTO CODE_SN_RESERVE (RMES_ID,COMPANY_CODE,PLINE_CODE,SN,SN_FLAG,input_person,input_time) "
             + "VALUES('" + rmes_id + "', '" + theCompanyCode + "','" + strPCode + "','" + strSN + "','" + strSNflag + "','"+theUserId+"',sysdate)";
        dc.ExeSql(Sql);

        //插入到日志表
        try
        {
            string Sql2 = "INSERT INTO CODE_SN_RESERVE_LOG (RMES_ID,COMPANY_CODE,PLINE_CODE,SN,SN_FLAG,user_code,flag,rqsj)"
                         + "select RMES_ID,COMPANY_CODE,PLINE_CODE,SN,SN_FLAG,'" + theUserCode + "','ADD',SYSDATE from CODE_SN_RESERVE where rmes_id='"+rmes_id+"')";
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
        string strCode = e.Values["RMES_ID"].ToString();
        string strTableName = "CODE_SN_RESERVE";

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
                string Sql2 = "INSERT INTO CODE_SN_RESERVE_LOG (RMES_ID,COMPANY_CODE,PLINE_CODE,SN,SN_FLAG,user_code,flag,rqsj)"
                             + "select RMES_ID,COMPANY_CODE,PLINE_CODE,SN,SN_FLAG,'" + theUserCode + "','DEL',SYSDATE from CODE_SN_RESERVE where rmes_id='" + strCode + "')";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }

            //确认删除
            string Sql = "delete from CODE_SN_RESERVE WHERE  COMPANY_CODE = '" + theCompanyCode + "' and RMES_ID = '" + strCode + "'";
            dc.ExeSql(Sql);
        }


        setCondition();
        e.Cancel = true;
    }

    //修改
    protected void ASPxGridView2_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxComboBox uPCode = ASPxGridView2.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;
        ASPxTextBox uSN = ASPxGridView2.FindEditFormTemplateControl("txtSN") as ASPxTextBox;
        ASPxComboBox uSNflag = ASPxGridView2.FindEditFormTemplateControl("ASPxComboBox3") as ASPxComboBox;


        string strPCode = uPCode.Value.ToString();
        string strSN = uSN.Text.Trim();
        string strSNflag = uSNflag.Value.ToString();

        //插入到日志表
        try
        {
            string Sql2 = "INSERT INTO CODE_SN_RESERVE_LOG (RMES_ID,COMPANY_CODE,PLINE_CODE,SN,SN_FLAG,user_code,flag,rqsj)"
                         + "select RMES_ID,COMPANY_CODE,PLINE_CODE,SN,SN_FLAG,'" + theUserCode + "','DEL',SYSDATE from CODE_SN_RESERVE WHERE COMPANY_CODE = '" + theCompanyCode + "' and SN = '" + strSN + "')";
            dc.ExeSql(Sql2);
        }
        catch
        {
            return;
        }

        string Sql = "UPDATE CODE_SN_RESERVE SET PLINE_CODE='" + strPCode + "',SN='" + strSN + "',SN_FLAG='" + strSNflag + "',input_person='"+theUserId+"',input_time=sysdate "
             + " WHERE COMPANY_CODE = '" + theCompanyCode + "' and SN = '" + strSN + "'";
        dc.ExeSql(Sql);

        //插入到日志表
        try
        {
            string Sql2 = "INSERT INTO CODE_SN_RESERVE_LOG (RMES_ID,COMPANY_CODE,PLINE_CODE,SN,SN_FLAG,user_code,flag,rqsj)"
                         + "select RMES_ID,COMPANY_CODE,PLINE_CODE,SN,SN_FLAG,'" + theUserCode + "','DEL',SYSDATE from CODE_SN_RESERVE WHERE COMPANY_CODE = '" + theCompanyCode + "' and SN = '" + strSN + "')";
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

    //创建EDITFORM前
    protected void ASPxGridView2_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
    {
        string Sql = "select distinct a.pline_code,b.pline_name from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "' ";
        DataTable dt = dc.GetTable(Sql);

        ASPxComboBox uPCode = ASPxGridView2.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;

        uPCode.DataSource = dt;
        uPCode.TextField = dt.Columns[1].ToString();
        uPCode.ValueField = dt.Columns[0].ToString();
        //uPCode.SelectedIndex = uPCode.Items.Count >= 0 ? 0 : -1;
        if (!ASPxGridView2.IsNewRowEditing && ASPxGridView2.IsEditing)
        {
            ///主键不可以修改
            (ASPxGridView2.FindEditFormTemplateControl("txtSN") as ASPxTextBox).Enabled = false;
        }
        else
        {
            //uTcode.SelectedIndex = 0;
        }

    }

    //修改数据校验
    protected void ASPxGridView2_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        //判断为空
        if (e.NewValues["SN"].ToString() == "" || e.NewValues["SN"].ToString() == null)
        {
            e.RowError = "预留号不能为空！";
        }
       

        string strSN = e.NewValues["SN"].ToString().Trim();
        //string strCode = e.NewValues["PLIEN_CODE"].ToString().Trim();
       

        //判断字符串长度
        if (strSN.Length != 8)
        {
            e.RowError = "预留号字节只能是8位！";
        }
        

        //判断是否重复
        if (ASPxGridView2.IsNewRowEditing)
        {
           
            string chSql = "SELECT SN, SN_FLAG,PLINE_CODE  FROM CODE_SN_RESERVE WHERE COMPANY_CODE = '" + theCompanyCode + "' AND SN='" + strSN + "' ";
            DataTable dt = dc.GetTable(chSql);
            if (dt.Rows.Count > 0)
            {
                string strCode = dt.Rows[0][2].ToString();
                e.RowError = "'" + strCode + "' 生产线已存在相同预留号！";
            }
            else
            {
                string Sql = "SELECT INITIAL_VALUE, MAX_VALUE,PLINE_CODE  FROM CODE_SN WHERE COMPANY_CODE = '" + theCompanyCode + "' ";
                DataTable dt1 = dc.GetTable(Sql);
                for (int i = 0,j=0; i < dt1.Rows.Count; i++)
                {

                    int sn = Convert.ToInt32(strSN);
                  
                    string min = dt1.Rows[i][0].ToString();
                    int mi = Convert.ToInt32(min);
                    string max = dt1.Rows[i][1].ToString();
                    int ma = Convert.ToInt32(max);
                    if(sn>=mi&&sn<=ma)
                    {
                        j++;
                    }
                    if (j > 0)
                    {
                        string strCode = dt1.Rows[i][2].ToString();
                        e.RowError = "该号码已经在'" + strCode + "' 生产线被使用！";
                        break;
                    }
                }

            }

        }
     
    }

}
