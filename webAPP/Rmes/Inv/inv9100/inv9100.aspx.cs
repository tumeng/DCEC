using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.Pub.Data;
using Rmes.Web.Base;
using System.Data;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridLookup;
/**
 * 功能概述：不回冲零件设定
 * 作者：游晓航
 * 创建时间：2016-08-23
 */
namespace Rmes.WebApp.Rmes.Inv.inv9100
{
    public partial class inv9100 : BasePage
    {
        private dataConn dc = new dataConn();
        PubCs thePubCs = new PubCs();
        public string theCompanyCode;
        private string theUserId, theUserCode, theUserName;
        public string theProgramCode;
        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();
            theUserCode = theUserManager.getUserCode();
            theUserName = theUserManager.getUserName();
            theProgramCode = "inv9100";
            if (!IsPostBack)
            {
                //Session["9100PART"] = "";
                //Session["9100USER"] = "";
                dataConn theDataConn1 = new dataConn(" select FUNC_GET_PLANSITE（'E','D'）from dual");
                theDataConn1.OpenConn();
                string gQadSite1 = theDataConn1.GetValue();
                if (gQadSite1 != "")
                {
                    string sql1 = "select distinct in_user1 bgy from copy_in_mstr  where upper(in_site)='" + gQadSite1 + "'";
                    Session["9100USER"] = sql1;
                    SqlBgy.SelectCommand = sql1;
                    SqlBgy.DataBind();

                    sql1 = "select distinct pt_part part from copy_pt_mstr where pt_phantom=0";
                    Session["9100PART"] = sql1;
                    SqlPart.SelectCommand = sql1;
                    SqlPart.DataBind();
                }
                ASPxDateEdit1.Date = DateTime.Now;
                ASPxDateEdit3.Date = DateTime.Now.AddDays(1);
            }
            initCode();

            if (Request["opFlag"] == "getEditSeries")
            {
                string str1 = "", str2 = "", str = "";
                string pcode = Request["PCode"].ToString().Trim();
                string qad = Request["QAD"].ToString().Trim();

                dataConn theDataConn = new dataConn(" select FUNC_GET_PLANSITE（'" + pcode + "','D'）from dual");
                theDataConn.OpenConn();
                string gQadSite = theDataConn.GetValue();
                if (gQadSite != "")
                {
                    string sql = "select distinct in_user1 bgy from copy_in_mstr  where upper(in_site)='" + gQadSite + "'";
                    Session["9100USER"] = sql;
                    SqlBgy.SelectCommand = sql;
                    SqlBgy.DataBind();
                }
                if (qad == "false" || qad == "null")//若未勾选，显示所有实零件//|| qad == "null"
                {
                    string sql = "select distinct pt_part part from copy_pt_mstr where pt_phantom=0";
                    Session["9100PART"] = sql;
                    SqlPart.SelectCommand = sql;
                    SqlPart.DataBind();

                }
                else if (qad == "true")//若勾选，显示非QAD零件
                {
                    string sql = "select distinct part from atpubkflpart where part_type='1' and gzdd='" + pcode + "' ";
                    Session["9100PART"] = sql;
                    SqlPart.SelectCommand = sql;
                    SqlPart.DataBind();
                }

                this.Response.Write(str);
                this.Response.End();
            }
            if (Request["opFlag"] == "getEditSeries2")
            {
                string str2 = "";
               
                string part = Request["PART"].ToString().Trim();
                string qad = Request["QAD"].ToString().Trim();
                if (qad == "false"||qad=="null")//若未勾选
                {
                    string sql = "select pt_desc2 from copy_pt_mstr where upper(pt_part)='" + part.ToUpper() + "'";
                    DataTable dt = dc.GetTable(sql);
                    if (dt.Rows.Count == 0)
                    {
                        str2 = " ";
                        this.Response.Write(str2);
                        this.Response.End();
                        return;
                    }
                    str2 = dt.Rows[0][0].ToString();
                }
                else if (qad == "true")//若勾选
                {
                    string sql = "select part_desc from atpubkflpart where upper(pt_part)='" + part.ToUpper() + "' ";
                    DataTable dt = dc.GetTable(sql);
                    if (dt.Rows.Count == 0)
                    {
                        str2 = " ";
                        this.Response.Write(str2);
                        this.Response.End();
                        return;
                    }
                    str2 = dt.Rows[0][0].ToString();
                }

                this.Response.Write(str2);
                this.Response.End();
                
            }
            setCondition();
            Query();

        }
        private void initCode()
        {
            //初始化生产线下拉列表
            string sql = "select distinct a.pline_code,b.pline_name from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'";
            SqlCode.SelectCommand = sql;
            SqlCode.DataBind();
        }
        private void setCondition()
        {
            string sql = "";
            sql = "SELECT PART ,PART_DESC ,DECODE(PART_TYPE,'1','非QAD零件','0','QAD零件') PART_TYPE,EDIT_NAME,EDIT_DATE ,BGY,GZDD  FROM ATPUBKFLPART WHERE "
               + "GZDD IN (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "')  order by part";
             
            DataTable dt = dc.GetTable(sql);
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
        }
        private void Query()
        {
            string sql = "SELECT PART,PART_DESC,DECODE(PART_TYPE,'1','非QAD零件','0','QAD零件') PART_TYPE,EDIT_NAME,EDIT_DATE,DEL_DATE,BGY,GZDD FROM ATPUBKFLPART_LOG WHERE  ";
            if (txtPlineCode.Text.Trim() == "")
            {
                sql = sql + " GZDD IN (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "')";
            }
            else
            {
                sql = sql + " GZDD='" + txtPlineCode.Value.ToString() + "'";
            }
            if (ASPxDateEdit1.Text.Trim() != "")
            {
                sql = sql + " AND DEL_DATE>=to_date('" + ASPxDateEdit1.Text.Trim() + "','YYYY-MM-DD  hh24:mi:ss') ";
            }
            if (ASPxDateEdit3.Text.Trim() != "")
            {
                sql = sql + " AND DEL_DATE<=to_date('" + ASPxDateEdit3.Text.Trim() + "','YYYY-MM-DD  hh24:mi:ss') ";
            }
            if (TextPart.Text.Trim() != "")
            {
                sql = sql + " AND PART LIKE '%" + TextPart.Text.Trim() + "%'";
            }

            sql = sql + "  ORDER BY PART";

            DataTable dt = dc.GetTable(sql);
            ASPxGridView2.DataSource = dt;
            ASPxGridView2.DataBind();
        }
        protected void ASPxGridView2_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            
            Query();
            ASPxGridView2.Selection.UnselectAll();
        }

        protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            
            ASPxCheckBox chkQAD = ASPxGridView1.FindEditFormTemplateControl("chkQAD") as ASPxCheckBox;
            ASPxComboBox PCode = ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;
            //ASPxComboBox uPNum = ASPxGridView1.FindEditFormTemplateControl("txtPNum") as ASPxComboBox;
            ASPxGridLookup Bgy = ASPxGridView1.FindEditFormTemplateControl("txtBgy") as ASPxGridLookup;
            ASPxGridLookup Part = ASPxGridView1.FindEditFormTemplateControl("txtPart") as ASPxGridLookup;
            ASPxTextBox Pname = ASPxGridView1.FindEditFormTemplateControl("txtPname") as ASPxTextBox;
            //ASPxDateEdit uOnlinetime = ASPxGridView1.FindEditFormTemplateControl("DateOnlinetime") as ASPxDateEdit;
            string vFlag = "";
            if (chkQAD.Checked == true)
            {
                vFlag = "Y";
            }
            else
            {
                vFlag = "N";
            }

            string strPCode = PCode.Value.ToString();
            string strPname = "";
            string strBgy = Bgy.Value.ToString();
            string strPart = Part.Value.ToString();
            string Gsql = "select pt_desc2 from copy_pt_mstr where upper(pt_part)='" + strPart.ToUpper() + "'";
            DataTable Gdt = dc.GetTable(Gsql);
            if (Gdt.Rows.Count > 0)
            {
                strPname = Gdt.Rows[0][0].ToString();

            }
            
           // string strOnlinetime = uOnlinetime.Text.Trim();
            if (vFlag == "N")
            {
                string Sql = "insert into atpubkflpart(part,gzdd,part_type,part_desc,edit_name,edit_date) "
                            + "VALUES('" + strPart + "','" + strPCode + "','0','" + strPname + "','" + theUserName + "',sysdate)";
                dc.ExeSql(Sql);
            }
            else
            {
                string Sql = "insert into atpubkflpart(part,gzdd,part_type,part_desc,edit_name,edit_date,bgy) "
                             + "VALUES('" + strPart + "','" + strPCode + "','1','" + strPname + "','" + theUserName + "',sysdate,'" + strBgy + "')";
                dc.ExeSql(Sql);
            }
            e.Cancel = true;
            ASPxGridView1.CancelEdit();
            setCondition();
        }

        //删除
        protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            //判断当前记录是否可以删除
            string strPART = e.Values["PART"].ToString();
            string strPCode = e.Values["GZDD"].ToString();
            string strTableName = "atpubkflpart";
            dataConn theDataConn = new dataConn("select func_check_delete_data('" + strTableName + "','" + theCompanyCode + "','MES','MES','MES','" + strPART + "') from dual");
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
                string Sql = "delete from atpubkflpart where part='" + strPART + "' and gzdd='" + strPCode + "' ";
                dc.ExeSql(Sql);
            }
            setCondition();
            e.Cancel = true;
        }

        

        //创建EDITFORM前
        protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
        {
            
            string Sql = " select distinct a.pline_code,b.pline_name as from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "' ";
            DataTable dt = dc.GetTable(Sql);
            ASPxComboBox uPCode = ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;
            uPCode.DataSource = dt;
            uPCode.TextField = dt.Columns[1].ToString();
            uPCode.ValueField = dt.Columns[0].ToString();
            //try
            //{

            //    if (Session["9100USER"].ToString() != "")
            //    {
            //        string sql = Session["9100USER"] as string;
            //        SqlBgy.SelectCommand = sql;
            //        SqlBgy.DataBind();
            //    }
            //}
            //catch
            //{ }
            //try
            //{
            //    if (Session["9100PART"].ToString() != "")
            //    {
            //        string sql = Session["9100PART"] as string;
            //        SqlPart.SelectCommand = sql;
            //        SqlPart.DataBind();
            //    }
            //}
            //catch
            //{ }


        }

        //修改数据校验
        protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {

            string strPart = e.NewValues["PART"].ToString().Trim();
            
            ASPxCheckBox chkQAD = ASPxGridView1.FindEditFormTemplateControl("chkQAD") as ASPxCheckBox;
          
            if (ASPxGridView1.IsNewRowEditing )
            {
                if (chkQAD.Checked == true)
                {
                    // 判断是否存在这个零件，存在就会提示不能选择非qad零件选项
                    string chSql = "select pt_part from copy_pt_mstr where upper(pt_part)='" + strPart + "'";
                    DataTable dt = dc.GetTable(chSql);
                    if (dt.Rows.Count >0)
                    {
                        e.RowError = "该零件是qad中存在的零件，请重选选项！";
                    }
                }
                else {
                    string chSql1 = "select pt_part from copy_pt_mstr where upper(pt_part)='" + strPart + "'";
                    DataTable dt1 = dc.GetTable(chSql1);
                    if (dt1.Rows.Count <= 0)
                    {
                        e.RowError = "该零件不存在!";
                    }
                }
                //判断是否重复
                string chSql2 = "select part from atpubkflpart where upper(part)='" + strPart + "'";
                DataTable dt2 = dc.GetTable(chSql2);
                if (dt2.Rows.Count > 0)
                {
                    e.RowError = "零件已经存在！";
                }

            }

        }

        //protected void ASPxCallbackPanel4_Callback(object sender, CallbackEventArgsBase e)
        //{
              
                
        //        string[] param = e.Parameter.Split(',');
        //        string pcode = param[0];
        //        string qad = param[1];
  
        //    dataConn theDataConn = new dataConn(" select FUNC_GET_PLANSITE（'" + pcode + "','D'）from dual");
        //        theDataConn.OpenConn();
        //        string gQadSite = theDataConn.GetValue();
                

        //        if (qad == "false" || qad == "null")//若未勾选，显示所有实零件//|| qad == "null"
        //        {
        //            string sql = "select distinct pt_part part from copy_pt_mstr where pt_phantom=0";
        //            Session["9100PART"] = sql;
        //            SqlPart.SelectCommand = sql;
        //            SqlPart.DataBind();

        //        }
        //        else if (qad == "true")//若勾选，显示非QAD零件
        //        {
        //            string sql = "select distinct pt_part part from atpubkflpart where part_type='1' and gzdd='" + pcode + "' ";
        //            Session["9100PART"] = sql;
        //            //DataTable dt = dc.GetTable(sql);
        //            SqlPart.SelectCommand = sql;
        //            SqlPart.DataBind();

        //        }

                 
           
        //}
        //protected void ASPxCallbackPanel3_Callback(object sender, CallbackEventArgsBase e)
        //{


        //    string[] param = e.Parameter.Split(',');
        //    string pcode = param[0];
        //    string qad = param[1];

        //    //    string strPmCode = ASPxGridView1.GetRowValues(ASPxGridView1.EditingRowVisibleIndex, new string[] { "PARAMETER_CODE" }).ToString();

        //    dataConn theDataConn = new dataConn(" select FUNC_GET_PLANSITE（'" + pcode + "','D'）from dual");
        //    theDataConn.OpenConn();
        //    string gQadSite = theDataConn.GetValue();
        //    if (gQadSite != "")
        //    {
        //        string sql = "select distinct in_user1 bgy from copy_in_mstr  where upper(in_site)='" + gQadSite + "'";
        //        Session["9100USER"] = sql;
        //        SqlBgy.SelectCommand = sql;
        //        SqlBgy.DataBind();

        //    }

        //}
        protected void txtPart_Init(object sender, EventArgs e)
        {
            try
            {
                if (Session["9100PART"].ToString() != "")
                {
                    string sql = Session["9100PART"] as string;
                    SqlPart.SelectCommand = sql;
                    SqlPart.DataBind();
                }
            }
            catch
            { }
        }
        protected void txtBgy_Init(object sender, EventArgs e)
        {
            try
            {

                if (Session["9100USER"].ToString() != "")
                {
                    string sql = Session["9100USER"] as string;
                    SqlBgy.SelectCommand = sql;
                    SqlBgy.DataBind();
                }
            }
            catch
            { }
        }

        protected void ASPxGridView1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            try
            {

                if (Session["9100USER"].ToString() != "")
                {
                    string sql = Session["9100USER"] as string;
                    SqlBgy.SelectCommand = sql;
                    SqlBgy.DataBind();
                }
            }
            catch
            { }
            try
            {
                if (Session["9100PART"].ToString() != "")
                {
                    string sql = Session["9100PART"] as string;
                    SqlPart.SelectCommand = sql;
                    SqlPart.DataBind();
                }
            }
            catch
            { }
        }   
    }
}