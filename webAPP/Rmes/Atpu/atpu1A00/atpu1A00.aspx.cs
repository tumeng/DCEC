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
using Rmes.Web.Base;

/**
 * 功能概述：重要零件表维护
 * 作者：杨少霞
 * 创建时间：2016-08-08
 * 修改时间：
 */

namespace Rmes.WebApp.Rmes.Atpu.atpu1A00
{
    public partial class atpu1A00 : BasePage
    {
        private dataConn dc = new dataConn();
        private PubCs pc = new PubCs();
        public string theCompanyCode, theUserId, theUserCode;
        public string theProgramCode;
        public static string  theZYLJZL;

        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();
            theUserCode = theUserManager.getUserCode();
            theProgramCode = "atpu1A00";

            //初始化零件名称begin
            if (Request["opFlag"] == "getEditLJDM")
            {
                string str1 = "";
                string ljdm = Request["ljdmC"].ToString();
                string sql = "select PT_DESC2 from COPY_PT_MSTR where PT_PART='" + ljdm + "'";
                dc.setTheSql(sql);
                if (dc.GetTable().Rows.Count == 0)
                {
                    str1 = "";
                    this.Response.Write(str1);
                    this.Response.End();
                    return;
                }
                string ljmc = dc.GetTable().Rows[0][0].ToString();
                if (ljdm == "")
                {
                    str1 = "";
                    this.Response.Write(str1);
                    this.Response.End();
                    return;
                }
                str1 = ljmc;
                this.Response.Write(str1);
                this.Response.End();
            }
            //初始化零件名称end
            

            setCondition();
           
        }
        //初始化零件代码
        private void initLJDM()
        {
            //string ljdmSql = "SELECT PT_PART FROM COPY_PT_MSTR order by PT_PART ";
            //SqlDataSource1.SelectCommand = ljdmSql;
            //SqlDataSource1.DataBind();
        }
        //初始化生产线
        private void initPline()
        {
            sqlPline.SelectCommand = "select a.PLINE_CODE,b.rmes_id,b.pline_name from VW_USER_ROLE_PROGRAM a "
                   + "left join code_product_line b on a.pline_code=b.pline_code "
                   + "where a.COMPANY_CODE = '" + theCompanyCode + "' and a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' order by b.PLINE_NAME";
        }
        //初始化零件类别
        private void initLJLB()
        {
            SqlLJZL.SelectCommand = "SELECT INTERNAL_CODE,INTERNAL_NAME FROM CODE_INTERNAL WHERE INTERNAL_TYPE_CODE='006' ORDER BY INTERNAL_CODE ";
        }
        //初始化零件级别
        private void initLJJB()
        {
            SqlLJJB.SelectCommand = "SELECT INTERNAL_CODE,INTERNAL_NAME FROM CODE_INTERNAL WHERE INTERNAL_TYPE_CODE='007' ORDER BY INTERNAL_CODE ";
        }
        private void setCondition()
        {
            //初始化GRIDVIEW

            //string sql = "SELECT '装配重要零件' as ZYLJLB, A.LJDM,A.LJMC,A.XH,A.GZDD,A.LJLB,B.PLINE_NAME,B.PLINE_CODE FROM DMZYLJB  A "
            //            + " LEFT JOIN CODE_PRODUCT_LINE B ON A.GZDD=B.PLINE_CODE "
            //            + " WHERE  A.GZDD in(SELECT PLINE_CODE FROM VW_USER_ROLE_PROGRAM WHERE USER_ID='" + theUserId + "' "
            //            + "and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "')"
            //            + " union SELECT '检查重要零件' as ZYLJLB, C.LJDM,C.LJMC,C.XH,C.GZDD,'' as LJLB,D.PLINE_NAME,D.PLINE_CODE FROM DMJCLJB C "
            //            + " LEFT JOIN CODE_PRODUCT_LINE D ON C.GZDD=D.PLINE_CODE "
            //            + " WHERE  C.GZDD in(SELECT PLINE_CODE FROM VW_USER_ROLE_PROGRAM WHERE USER_ID='" + theUserId + "' "
            //            +"and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "')"
            //            + " ORDER BY ZYLJLB,GZDD,LJDM";
            string sql = "SELECT * from vw_zyljb WHERE  GZDD in(SELECT PLINE_CODE FROM VW_USER_ROLE_PROGRAM WHERE USER_ID='" + theUserId + "' "
            + "and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') "
            + " ORDER BY INPUT_TIME DESC NULLS LAST ";
            DataTable dt = dc.GetTable(sql);

            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
            initLJDM();
            initPline();
            initLJLB();
            initLJJB();
            //新增的按零件名称维护标签页 YXH
            string sql2 = "SELECT * from DMZYLJB_NAME WHERE  PLINE_CODE in(SELECT PLINE_CODE FROM VW_USER_ROLE_PROGRAM WHERE USER_ID='" + theUserId + "' "
          + "and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') ORDER BY INPUT_TIME DESC NULLS LAST";
            DataTable dt2 = dc.GetTable(sql2);

            ASPxGridView2.DataSource = dt2;
            ASPxGridView2.DataBind();
        }
        protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            //判断当前记录是否可以删除
            string strDelCode = e.Values["XH"].ToString();
            string ljlb = e.Values["ZYLJLB"].ToString();
            string strTableName = "";
            if (ljlb == "装配重要零件")
            {
                strTableName = "DMZYLJB";
            }
            else
            {
                strTableName = "DMJCLJB";
            }
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
                    if (strTableName == "DMZYLJB")
                    {
                        string Sql2 = "INSERT INTO DMZYLJB_LOG (LJDM,LJMC,XH,GZDD,LJLB,user_code,flag,rqsj)"
                            + " SELECT LJDM,LJMC,XH,GZDD,LJLB,'" + theUserCode + "' , 'DEL', SYSDATE FROM DMZYLJB WHERE XH = '" + strDelCode + "'";
                        dc.ExeSql(Sql2);
                    }
                    if (strTableName == "DMJCLJB")
                    {
                        string Sql2 = "INSERT INTO DMJCLJB_LOG (LJDM,LJMC,XH,GZDD,user_code,flag,rqsj)"
                            + " SELECT LJDM,LJMC,XH,GZDD,LJLB,'" + theUserCode + "' , 'DEL', SYSDATE FROM DMJCLJB WHERE XH = '" + strDelCode + "'";
                        dc.ExeSql(Sql2);
                    }
                }
                catch
                {
                    return;
                }
                //确认删除
                string Sql = "delete from " + strTableName + " WHERE   XH = '" + strDelCode + "'";
                dc.ExeSql(Sql);
            }
            setCondition();
            e.Cancel = true;
        }
        protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxComboBox ljzl = ASPxGridView1.FindEditFormTemplateControl("comboLJZL") as ASPxComboBox;

            ASPxComboBox plineCode = ASPxGridView1.FindEditFormTemplateControl("comboPCode") as ASPxComboBox;
            //ASPxComboBox ljdm = ASPxGridView1.FindEditFormTemplateControl("comboLJDM") as ASPxComboBox;
            ASPxTextBox ljdm = ASPxGridView1.FindEditFormTemplateControl("ASPxTextBox1") as ASPxTextBox;
            ASPxTextBox ljmc = ASPxGridView1.FindEditFormTemplateControl("txtLJMC") as ASPxTextBox;
            ASPxComboBox ljjb = ASPxGridView1.FindEditFormTemplateControl("comboLJJB") as ASPxComboBox;

            string Sql = "";
            if (ljzl.Value.ToString() == "ZP")
            {
                Sql = "INSERT INTO DMZYLJB (LJDM,LJMC,XH,GZDD,LJLB,INPUT_PERSON,INPUT_TIME)"
                    + "VALUES( '" + ljdm.Text.Trim().ToUpper() + "', '" + ljmc.Text.Trim() + "',SEQ_XH.NEXTVAL,'" + plineCode.Value.ToString() + "','" + ljjb.Value.ToString() + "','" + theUserId + "',SYSDATE)";
            }
            else
            {
                Sql = "INSERT INTO DMJCLJB (LJDM,LJMC,XH,GZDD,INPUT_PERSON,INPUT_TIME)"
                        + "VALUES( '" + ljdm.Text.Trim().ToUpper() + "', '" + ljmc.Text.Trim() + "',SEQ_XH.NEXTVAL,'" + plineCode.Value.ToString() + "','"+theUserId+"',SYSDATE)";
            }
            dc.ExeSql(Sql);

            //插入到日志表
            try
            {
                if (ljzl.Value.ToString() == "ZP")
                {
                    string Sql2 = "INSERT INTO DMZYLJB_LOG (LJDM,LJMC,XH,GZDD,LJLB,user_code,flag,rqsj)"
                        + " SELECT LJDM,LJMC,XH,GZDD,LJLB,'" + theUserCode + "' , 'ADD', SYSDATE FROM DMZYLJB WHERE GZDD='"
                        + plineCode.Value.ToString() + "' and LJDM='" + ljdm.Text.Trim().ToUpper() + "'";
                    dc.ExeSql(Sql2);
                }
                else
                {
                    string Sql2 = "INSERT INTO DMJCLJB_LOG (LJDM,LJMC,XH,GZDD,user_code,flag,rqsj)"
                        + " SELECT LJDM,LJMC,XH,GZDD,LJLB,'" + theUserCode + "' , 'ADD', SYSDATE FROM DMJCLJB WHERE GZDD='"
                        + plineCode.Value.ToString() + "' and LJDM='" + ljdm.Text.Trim().ToUpper() + "'";
                    dc.ExeSql(Sql2);
                }
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

            ASPxComboBox ljzl = ASPxGridView1.FindEditFormTemplateControl("comboLJZL") as ASPxComboBox;

            ASPxComboBox plineCode = ASPxGridView1.FindEditFormTemplateControl("comboPCode") as ASPxComboBox;
            //ASPxComboBox ljdm = ASPxGridView1.FindEditFormTemplateControl("comboLJDM") as ASPxComboBox;
            ASPxTextBox ljdm = ASPxGridView1.FindEditFormTemplateControl("ASPxTextBox1") as ASPxTextBox;
            ASPxTextBox ljmc = ASPxGridView1.FindEditFormTemplateControl("txtLJMC") as ASPxTextBox;
            ASPxComboBox ljjb = ASPxGridView1.FindEditFormTemplateControl("comboLJJB") as ASPxComboBox;

            string Sql = "";
            if (ljzl.Value.ToString() == "装配重要零件")
            {
                //插入到日志表
                try
                {
                    string Sql2 = "INSERT INTO DMZYLJB_LOG (LJDM,LJMC,XH,GZDD,LJLB,user_code,flag,rqsj)"
                        + " SELECT LJDM,LJMC,XH,GZDD,LJLB,'" + theUserCode + "' , 'BEFOREEDIT', SYSDATE FROM DMZYLJB WHERE GZDD='"
                        + plineCode.Value.ToString() + "' and LJDM='" + ljdm.Text.Trim().ToUpper() + "'";
                    dc.ExeSql(Sql2);
                }
                catch
                {
                    return;
                }

                Sql = "update DMZYLJB set LJLB='" + ljjb.Value.ToString() + "'"
                    + "where GZDD='" + plineCode.Value.ToString() + "' and LJDM='" + ljdm.Text.Trim().ToUpper() + "'";
            }
            dc.ExeSql(Sql);
            //插入到日志表
            try
            {
                string Sql2 = "INSERT INTO DMZYLJB_LOG (LJDM,LJMC,XH,GZDD,LJLB,user_code,flag,rqsj)"
                    + " SELECT LJDM,LJMC,XH,GZDD,LJLB,'" + theUserCode + "' , 'AFTEREDIT', SYSDATE FROM DMZYLJB WHERE GZDD='"
                    + plineCode.Value.ToString() + "' and LJDM='" + ljdm.Text.Trim().ToUpper() + "'";
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
                (ASPxGridView1.FindEditFormTemplateControl("comboLJZL") as ASPxComboBox).Enabled = false;
                (ASPxGridView1.FindEditFormTemplateControl("comboPCode") as ASPxComboBox).Enabled = false;
                //(ASPxGridView1.FindEditFormTemplateControl("comboLJDM") as ASPxComboBox).Enabled = false;
                (ASPxGridView1.FindEditFormTemplateControl("ASPxTextBox1") as ASPxTextBox).Enabled = false;
                (ASPxGridView1.FindEditFormTemplateControl("txtLJMC") as ASPxTextBox).Enabled = false;

                if (theZYLJZL == "检查重要零件")
                {
                    (ASPxGridView1.FindEditFormTemplateControl("comboLJJB") as ASPxComboBox).Enabled = false;
                }
            }
            if (ASPxGridView1.IsNewRowEditing)
            {
                theZYLJZL = "";
                (ASPxGridView1.FindEditFormTemplateControl("comboLJZL") as ASPxComboBox).Text = "";
                if (theZYLJZL == "检查重要零件")
                {
                    (ASPxGridView1.FindEditFormTemplateControl("comboLJJB") as ASPxComboBox).Enabled = false;
                }
            }
            string PSql = "select PLINE_CODE,PLINE_NAME from CODE_PRODUCT_LINE where pline_code in (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "')";
            DataTable Pdt = dc.GetTable(PSql);
            ASPxComboBox uPcode = ASPxGridView1.FindEditFormTemplateControl("comboPCode") as ASPxComboBox;
            uPcode.DataSource = Pdt;
            uPcode.TextField = Pdt.Columns[1].ToString();
            uPcode.ValueField = Pdt.Columns[0].ToString();
            uPcode.SelectedIndex = uPcode.Items.Count >= 0 ? 0 : -1;
        }

        protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            //判断是否重复
            ASPxComboBox ljzl = ASPxGridView1.FindEditFormTemplateControl("comboLJZL") as ASPxComboBox;
            ASPxComboBox ljjb = ASPxGridView1.FindEditFormTemplateControl("comboLJJB") as ASPxComboBox;
            
            if (ASPxGridView1.IsNewRowEditing)
            {
                string chSql = "";
                if (ljzl.Value.ToString() == "ZP")
                {
                    chSql = "SELECT GZDD,LJDM  FROM DMZYLJB"
                          + " WHERE  GZDD='" + e.NewValues["PLINE_CODE"].ToString() + "' and LJDM='" + e.NewValues["LJDM"].ToString() + "'";
                }
                else
                {
                    chSql = "SELECT GZDD,LJDM  FROM DMJCLJB"
                              + " WHERE  GZDD='" + e.NewValues["PLINE_CODE"].ToString() + "' and LJDM='" + e.NewValues["LJDM"].ToString() + "'";
                }
                DataTable dt = dc.GetTable(chSql);
                if (dt.Rows.Count > 0)
                {
                    e.RowError = "已存在相同的生产线和零件代码！";
                }
            }
            //判断装配重要零件重要级别的录入
            if (ASPxGridView1.IsNewRowEditing)
            {
                if (ljzl.Value.ToString() == "ZP")
                {
                    if (ljjb.SelectedItem == null)
                    {
                        e.RowError = "装配零部件重要级别不能为空！";
                    }
                }
            }
           
        }

        protected void ASPxGridView1_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            //进入修改界面时的处理
            if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
            {
                theZYLJZL = ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "ZYLJLB").ToString();
            }
            if (ASPxGridView1.IsNewRowEditing)
            {
                theZYLJZL = "";
            }
        }

        protected void ASPxGridView1_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            //如果是检查重要零件则不允许修改
            try
            {
                ASPxGridView grid = (ASPxGridView)sender;
                string zyljlb = grid.GetRowValues(e.VisibleIndex, "ZYLJLB").ToString();
                if (zyljlb == "检查重要零件")
                {
                    switch (e.ButtonType)
                    {
                        case ColumnCommandButtonType.Edit:
                            e.Visible = false;
                            break;
                    }
                }
            }
            catch { }
        }
        protected void ASPxGridView2_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            //判断当前记录是否可以删除
            string strDelCode = e.Values["RMES_ID"].ToString();
            
            string strTableName = "DMZYLJB_NAME";
             
            dataConn theDataConn = new dataConn("select func_check_delete_data('" + strTableName + "','" + theCompanyCode + "','MES','MES','MES','" + strDelCode + "') from dual");

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
                    string Sql2 = "INSERT INTO DMZYLJB_NAME_LOG (RMES_ID,LJMC,PLINE_CODE,user_code,flag,rqsj)"
                        + " SELECT RMES_ID,LJMC,PLINE_CODE,'" + theUserCode + "' , 'DEL', SYSDATE FROM DMZYLJB_NAME WHERE RMES_ID = '" + strDelCode + "'";
                    dc.ExeSql(Sql2);
                    
                }
                catch
                {
                    return;
                }
                //确认删除
                string Sql = "delete from " + strTableName + " WHERE   RMES_ID = '" + strDelCode + "'";
                dc.ExeSql(Sql);
            }
            setCondition();
            e.Cancel = true;
        }
        protected void ASPxGridView2_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {


            ASPxComboBox plineCode = ASPxGridView2.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;
             
            ASPxTextBox ljmc = ASPxGridView2.FindEditFormTemplateControl("txtName") as ASPxTextBox;

            //取RMES_ID的值
            string sql_rmes_id = "SELECT SEQ_RMES_ID.NEXTVAL FROM DUAL ";
            dc.setTheSql(sql_rmes_id);
            string rmes_id = dc.GetTable().Rows[0][0].ToString();

            string Sql = "";
            Sql = "INSERT INTO DMZYLJB_NAME (RMES_ID,LJMC,PLINE_CODE,INPUT_PERSON,INPUT_TIME)"
                        + "VALUES('" + rmes_id + "', '" + ljmc.Text.Trim() + "','" + plineCode.Value.ToString() + "','"+theUserId+"',SYSDATE)";
            dc.ExeSql(Sql);

            //插入到日志表
            try
            {
                string Sql2 = "INSERT INTO DMZYLJB_NAME_LOG (RMES_ID,LJMC,PLINE_CODE,user_code,flag,rqsj)"
                    + " SELECT RMES_ID,LJMC,PLINE_CODE,'" + theUserCode + "' , 'ADD', SYSDATE FROM DMZYLJB_NAME WHERE RMES_ID = '" + rmes_id + "'";
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
             
            string PSql = "select PLINE_CODE,PLINE_NAME from CODE_PRODUCT_LINE where pline_code in (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "')";
            DataTable Pdt = dc.GetTable(PSql);
            ASPxComboBox uPcode = ASPxGridView2.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;
            uPcode.DataSource = Pdt;
            uPcode.TextField = Pdt.Columns[1].ToString();
            uPcode.ValueField = Pdt.Columns[0].ToString();
            
        }

        protected void ASPxGridView2_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            //判断是否重复
            
           
            if (ASPxGridView2.IsNewRowEditing)
            {
                string chSql = "";
                
                    chSql = "SELECT LJMC  FROM DMZYLJB_NAME"
                              + " WHERE  PLINE_CODE='" + e.NewValues["PLINE_CODE"].ToString() + "' and LJMC='" + e.NewValues["LJMC"].ToString() + "'";
                
                DataTable dt = dc.GetTable(chSql);
                if (dt.Rows.Count > 0)
                {
                    e.RowError = "已存在相同的生产线和零件名称！";
                }
            }
          

        }
        protected void btnXlsExport_Click(object sender, EventArgs e)
        {
            ASPxGridViewExporter1.WriteXlsToResponse("按零件号维护重要零件信息导出");
        }
        protected void btnXlsExport2_Click(object sender, EventArgs e)
        {
            ASPxGridViewExporter2.WriteXlsToResponse("按名称维护重要零件信息导出");
        }

    }
}