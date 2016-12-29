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
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
/**
 * 功能概述：按计划单独要料-三方要料
 * 作者：游晓航
 * 创建时间：2016-08-03
 */
namespace Rmes.WebApp.Rmes.Part.part1400
{
    public partial class part1400 : BasePage
    {
        private dataConn dc = new dataConn();
        PubCs thePubCs = new PubCs();
        public string theCompanyCode;
        private string theUserId, theUserCode;
        public string theProgramCode;
        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();
            theUserCode = theUserManager.getUserCode();
            theProgramCode = "part1400";
            initPlanCode();
            setCondition();
            if (!IsPostBack)
            {
                ASPxDateEdit1.Date = DateTime.Now;
                ASPxDateEdit3.Date = DateTime.Now.AddDays(1);


            }
            if (Request["opFlag"] == "getEditSeries")
            {
                string str1 = "", str2 = "";
                string pcode = Request["PCode"].ToString().Trim();
                string plancode = Request["PlanCode"].ToString().Trim();
                string sql = "select PLAN_SO,PLAN_QTY from data_plan where pline_code='" + pcode.ToUpper() + "' and plan_code='" + plancode.ToUpper() + "'";
                dc.setTheSql(sql);
                if (dc.GetTable().Rows.Count == 0)
                {
                    str1 = "";
                    this.Response.Write(str1);
                    this.Response.End();
                    return;
                }
                string Pqty = dc.GetTable().Rows[0][1].ToString();
                string Pso = dc.GetTable().Rows[0][0].ToString();
                if (Pso == "")
                {
                    str1 = "";
                    this.Response.Write(str1);
                    this.Response.End();
                    return;
                }
                str1 = Pso;
                str2 = Pqty;       
                str1 = str1 + "," + str2;
                this.Response.Write(str1);
                this.Response.End();
            }


        }
        private void initPlanCode()
        {
            //初始化计划下拉列表（物料确认过的计划,且未参与过物流计算的计划）
            string sql = "select distinct plan_code from ms_single_plan where gzdd in (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') order by plan_code";
            sqlPlanCode.SelectCommand = sql;
            sqlPlanCode.DataBind();
        }
        private void setCondition()
        {
            string sql = "";
            sql = "select * from ms_single_plan WHERE GZDD IN(select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "')  "
                + "AND ONLINE_TIME>=to_date('" + ASPxDateEdit1.Text.Trim() + "','YYYY-MM-DD  hh24:mi:ss') AND ONLINE_TIME<=to_date('" + ASPxDateEdit3.Text.Trim() + "','YYYY-MM-DD  hh24:mi:ss')";
            if (ComboPlanCode.Text.Trim() != "")
            {
                sql = sql + "and plan_code='" + ComboPlanCode.Text.Trim() + "' ";               
            }
             
            sql = sql + " order by add_time";

            DataTable dt = dc.GetTable(sql);
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();

        }
        protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            setCondition();
            ASPxGridView1.Selection.UnselectAll();

        }
          
        protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {

            ASPxTextBox uPSo = ASPxGridView1.FindEditFormTemplateControl("txtPSo") as ASPxTextBox;
            ASPxComboBox uPCode = ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;
            ASPxTextBox uPNum = ASPxGridView1.FindEditFormTemplateControl("txtPNum") as ASPxTextBox;
            ASPxComboBox uPlanCode = ASPxGridView1.FindEditFormTemplateControl("txtPlanCode") as ASPxComboBox;
            ASPxComboBox uOnLocation = ASPxGridView1.FindEditFormTemplateControl("txtOnLocation") as ASPxComboBox;
            ASPxDateEdit uOnlinetime = ASPxGridView1.FindEditFormTemplateControl("DateOnlinetime") as ASPxDateEdit;
            string strPSo = uPSo.Text.Trim();
            string strPCode = uPCode.Value.ToString();
            string strPNum = uPNum.Text.Trim();
            string strPlanCode = uPlanCode.Value.ToString();
            string strOnLocation = uOnLocation.Value.ToString();
            string strOnlinetime = uOnlinetime.Text.Trim();
  
            string Sql = "INSERT INTO MS_SINGLE_PLAN(PLAN_CODE,PLAN_SO,PLAN_NUM,ONLINE_LOCATION,ONLINE_TIME,FLAG,ADD_TIME,GZDD)"
            + "VALUES('" + strPlanCode + "','" + strPSo + "','" + strPNum + "','" + strOnLocation + "',to_date('" + uOnlinetime.Value + "','yyyy-mm-dd hh24:mi:ss'),'N',sysdate,'" + strPCode + "')";
            dc.ExeSql(Sql);

            e.Cancel = true;
            ASPxGridView1.CancelEdit();
            setCondition();
        }

        //删除
        protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            //判断当前记录是否可以删除
            string strPlanCode = e.Values["PLAN_CODE"].ToString();
            string strPCode = e.Values["GZDD"].ToString();
            string strTableName = "MS_SINGLE_PLAN";
            dataConn theDataConn = new dataConn("select func_check_delete_data('" + strTableName + "','" + theCompanyCode + "','MES','MES','MES','" + strPlanCode + "') from dual");
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
                string Sql = "delete from MS_SINGLE_PLAN WHERE  plan_code='" + strPlanCode + "' and flag='N' and gzdd='" + strPCode + "'";
                dc.ExeSql(Sql);      
            }
            setCondition();
            e.Cancel = true;
        }

        //修改
        protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {

            ASPxTextBox uPSo = ASPxGridView1.FindEditFormTemplateControl("txtPSo") as ASPxTextBox;
            ASPxComboBox uPCode = ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;
            ASPxTextBox uPNum = ASPxGridView1.FindEditFormTemplateControl("txtPNum") as ASPxTextBox;
            ASPxComboBox uPlanCode = ASPxGridView1.FindEditFormTemplateControl("txtPlanCode") as ASPxComboBox;
            ASPxComboBox uOnLocation = ASPxGridView1.FindEditFormTemplateControl("txtOnLocation") as ASPxComboBox;
            ASPxDateEdit uOnlinetime = ASPxGridView1.FindEditFormTemplateControl("DateOnlinetime") as ASPxDateEdit;
            string strPSo = uPSo.Text.Trim();
            string strPCode = uPCode.Value.ToString();
            string strPNum = uPNum.Text.Trim();
            string strPlanCode = uPlanCode.Value.ToString();
            string strOnLocation = uOnLocation.Value.ToString();
            string strOnlinetime = uOnlinetime.Text.Trim();
            string Sql = "update ms_single_plan set PLAN_NUM='" + strPNum + "',online_location='" + strOnLocation + "',online_time=to_date('" + uOnlinetime.Value + "','yyyy-mm-dd hh24:mi:ss')"
                     +"where PLAN_CODE='" + strPlanCode+ "' and FLAG='N' and gzdd='" + strPCode + "'";
               
            dc.ExeSql(Sql);

            e.Cancel = true;
            ASPxGridView1.CancelEdit();
            setCondition();
        }

        //创建EDITFORM前
        protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
        {
            string Sql = " select distinct a.pline_code,b.pline_name from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "' ";
            DataTable dt = dc.GetTable(Sql);
            ASPxComboBox uPCode = ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;
            uPCode.DataSource = dt;
            uPCode.TextField = dt.Columns[1].ToString();
            uPCode.ValueField = dt.Columns[0].ToString();
            //物料确认过的计划,且未参与过物流计算的计划
            string Sql2 = "select distinct plan_code from DATA_PLAN where pline_code in(select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') and ITEM_FLAG='Y' and "
                        + "ONLINE_QTY=0 and plan_code not in(select plan_code from ms_sfjit_plan_log where GZDD in (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "'))order by PLAN_CODE desc ";
            DataTable dt2 = dc.GetTable(Sql2);
            ASPxComboBox uLCode = ASPxGridView1.FindEditFormTemplateControl("txtPlanCode") as ASPxComboBox;
            uLCode.DataSource = dt2;
            uLCode.TextField = dt2.Columns[0].ToString();

            string Sql3 = "select location_code from ms_location_time where "
                       + "GZDD in (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "')order by location_seq";
            DataTable dt3 = dc.GetTable(Sql3);
            ASPxComboBox uOnLocation = ASPxGridView1.FindEditFormTemplateControl("txtOnLocation") as ASPxComboBox;
            uOnLocation.DataSource = dt3;
            uOnLocation.TextField = dt3.Columns[0].ToString();

            if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
            {
                ///主键不可以修改
                (ASPxGridView1.FindEditFormTemplateControl("txtPlanCode") as ASPxComboBox).Enabled = false;
                (ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox).Enabled = false;
                (ASPxGridView1.FindEditFormTemplateControl("txtPSo") as ASPxTextBox).Enabled = false;

            } 
        }

        //修改数据校验
        protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {

            string strPlanCode = e.NewValues["PLAN_CODE"].ToString().Trim();
            string strPCode = e.NewValues["GZDD"].ToString().Trim();
            //判断是否重复

            if (ASPxGridView1.IsNewRowEditing && !ASPxGridView1.IsEditing)
            {
                string chSql = "select * from ms_single_plan where plan_code='" + strPlanCode + "' and flag='N' and gzdd='" + strPCode + "'";
                DataTable dt = dc.GetTable(chSql);
                if (dt.Rows.Count > 0)
                {
                    e.RowError = "已存在该计划且尚未要料！";
                }
                //已参与过计算的计划不能再次要料
                string chSql2 = "select * from ms_sfjit_plan_log where plan_code='" + strPlanCode + "' and gzdd='" + strPCode + "'";
                DataTable dt2 = dc.GetTable(chSql2);
                if (dt2.Rows.Count > 0)
                {
                    e.RowError = "已存在该计划且尚未要料！";
                }

            }

        }
        protected void btnXlsExport_Click(object sender, EventArgs e)
        {
            ASPxGridViewExporter1.WriteXlsToResponse("按计划单独要料信息导出--三方");
        }
        protected void ASPxGridView1_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            ASPxGridView grid = (ASPxGridView)sender;
            string flag = grid.GetRowValues(e.VisibleIndex, "FLAG") as string;

            if (flag == "Y")//已计算的不能修改删除
                e.Enabled = false;
            else
                e.Enabled = true;
             
            if (e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
                e.Enabled = true;

        }
    }
}