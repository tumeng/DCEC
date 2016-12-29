using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rmes.Web.Base;
using System.Data;
using Rmes.Pub.Data;
using PetaPoco;
using Rmes.DA.Base;
using DevExpress.Web.ASPxClasses;
using Rmes.DA.Factory;
using Rmes.DA.Entity;
using System.Data.OleDb;
using System.IO;
using DevExpress.Web.ASPxGridView;
using DevExpress.Utils;
using DevExpress.Web.ASPxEditors;

/**
 * 2016-07-29
 * 唐海林
 * 三方物流计划取消
 * */

namespace Rmes.WebApp.Rmes.Ssd.ssd2200
{
    public partial class ssd2200 : BasePage
    {
        private dataConn dc = new dataConn();
        private PubCs thePubCs = new PubCs();

        private static string theCompanyCode, theUserId, theUserName,theUserCode;
        public string programcode;
        public Database db = DB.GetInstance();
        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();
            theUserName = theUserManager.getUserName();
            theUserCode = theUserManager.getUserCode();
            programcode = "ssd2200";
            if (!IsPostBack)
            {

            }
            if (Request["opFlag"] == "getPlan")
            {
                string str1 = "";
                string plancode = Request["PLAN"].ToString();
                string sql = "select plan_so,plan_qty from data_plan where plan_code='" + plancode.ToUpper() + "'";
                dc.setTheSql(sql);
                if (dc.GetTable().Rows.Count == 0)
                {
                    str1 = "";
                    this.Response.Write(str1);
                    this.Response.End();
                    return;
                }
                string planso = dc.GetTable().Rows[0][0].ToString();
                string planqty = dc.GetTable().Rows[0][1].ToString();

                if (planso == "")
                {
                    str1 = "";
                    this.Response.Write(str1);
                    this.Response.End();
                    return;
                }
                str1 = planso + "," + planqty;
                this.Response.Write(str1);
                this.Response.End();
            }
            setCondition();
        }
        private void setCondition()
        {
            string plancode=ASPxTextPlanCode.Text.Trim().ToUpper();
            //if (plancode != "")
            //{
            //    string sql = "select t.*,t.rowid from ms_over_mat_log t where 1=1 ";
            //    if (plancode != "")
            //    {
            //        sql = sql + " and bill_code='DEL-" + plancode + "' ";
            //    }
            //    sql = sql + "";  //order by MATERIAL_CODE
            //    DataTable dt = dc.GetTable(sql);
            //    ASPxGridView1.DataSource = dt;
            //    ASPxGridView1.DataBind();
            //}
            //else
            //{
            //    string sql = "select t.*,t.rowid from ms_over_mat_log t where 1=2 ";
            //    sql = sql + "";  //order by MATERIAL_CODE
            //    DataTable dt = dc.GetTable(sql);
            //    ASPxGridView1.DataSource = dt;
            //    ASPxGridView1.DataBind();
            //}
            string sql1 = "select t.*,t.rowid from ms_plan_adjust_log t where 1=1  ";
            sql1 = sql1 + " and gzdd in (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + programcode + "' and company_code='" + theCompanyCode + "' )";
            sql1 = sql1 + " order by log_time desc ";
            DataTable dt1 = dc.GetTable(sql1);
            ASPxGridView2.DataSource = dt1;
            ASPxGridView2.DataBind();
        }
        protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            //string plancode = ASPxTextPlanCode.Text.Trim().ToUpper();
            //string sql = "select t.*,t.rowid  from ms_over_mat_log t where 1=1 ";
            //if (plancode != "")
            //{
            //    sql = sql + " and bill_code='DEL-" + plancode + "' ";
            //}
            //sql = sql + "order by MATERIAL_CODE";
            //DataTable dt = dc.GetTable(sql);
            //ASPxGridView1.DataSource = dt;
            //ASPxGridView1.DataBind();
        }

        protected void ASPxCallbackPanel5_Callback(object sender, CallbackEventArgsBase e)
        {
            string[] s = e.Parameter.Split(',');
            string flag = s[0];
            string value = s[1];

            switch (flag)
            {
                case "ADD":
                    if (string.IsNullOrEmpty(value)) return;
                    string value3 = s[2];
                    if (value3.EndsWith("|"))
                    {
                        value3 = value3.Substring(0, value3.Length - 1);
                    }
                    string[] _ids = value3.Split('|');
                    break;
                default:
                    ASPxCallbackPanel5.JSProperties.Clear();
                    break;
            }
        }
        protected void ASPxCbSubmit_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
        {
            try
            {
                string[] collection = e.Parameter.Split('@');
                int cnt = collection.Length;
                string value = collection[1]; //计划号

                if (collection[0] == "Commit")
                {
                    if (value.EndsWith(","))
                    {
                        value = value.Substring(0,value.Length-1);
                    }
                    string[] _ids = value.Split(',');
                    string plancode=_ids[0].ToUpper();
                    string planso=_ids[1];
                    string planqty=_ids[2];
                    string changeqty=_ids[3];
                    //根据计划号判断该用户是否有该生产线权限
                    string sql = "select run_flag,online_qty,third_flag from data_plan where plan_code='" + plancode + "' and plan_type='A' and pline_code in "
                          + " (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + programcode + "' and company_code='" + theCompanyCode + "' )";
                    DataTable dt = dc.GetTable(sql);
                    if (dt.Rows.Count == 0)
                    {
                        e.Result = "Fail,无该计划生产线权限！";
                        return;
                    }
                    string runflag = dt.Rows[0][0].ToString();
                    string onlineqty = dt.Rows[0][1].ToString();
                    string thirdflag = dt.Rows[0][2].ToString();
                    if (thirdflag == "N")
                    {
                        e.Result = "Fail,该计划未进行三方物料计算！";
                        return;
                    }
                    if (onlineqty != "0" || runflag == "Y")
                    {
                        e.Result = "Fail,该计划已上线！";
                        return;
                    }
                    int count2 = Convert.ToInt32(dc.GetValue("select count(1) from data_plan_sn where sn_flag='N' and plan_code='" + plancode + "'"));
                    if (count2 < Convert.ToInt32(changeqty))
                    {
                        e.Result = "Fail,该计划未上线流水号数量小于调整数量！";
                        return;
                    }
                    string plinecode="";
                    plinecode = dc.GetValue("select nvl(pline_code,'') from data_plan where plan_code='"+plancode+"'");
                    PlanFactory.MS_HANDLE_PLAN_ADJUST("DELETE", plinecode, plancode, planso, planqty, changeqty, theUserCode);

                    string sql1 = "INSERT INTO data_plan_LOG(RMES_ID,COMPANY_CODE,PLINE_CODE,PLINE_NAME,PLAN_SEQ,PLAN_CODE,PLAN_SO,PRODUCT_MODEL,PLAN_TYPE,CREATE_USERID,CREATE_USERNAME,CREATE_TIME,BEGIN_DATE,END_DATE,ACCOUNT_DATE,PLAN_QTY,ONLINE_QTY,OFFLINE_QTY,ROUNTING_SITE,ROUNTING_REMARK,CUSTOMER_CODE,CUSTOMER_NAME,ORDER_CODE,REMARK,ROUNTING_CODE,LQ_FLAG,CONFIRM_FLAG,RUN_FLAG,BOM_FLAG,ITEM_FLAG,SN_FLAG,THIRD_FLAG,STOCK_FLAG,THIRD_RECEIVE_FLAG,STOCK_RECEIVE_FLAG,modify_user,modify_time,modify_type,is_bom) "
                            + " SELECT RMES_ID,COMPANY_CODE,PLINE_CODE,PLINE_NAME,PLAN_SEQ,PLAN_CODE,PLAN_SO,PRODUCT_MODEL,PLAN_TYPE,CREATE_USERID,CREATE_USERNAME,CREATE_TIME,BEGIN_DATE,END_DATE,ACCOUNT_DATE,PLAN_QTY,ONLINE_QTY,OFFLINE_QTY,ROUNTING_SITE,ROUNTING_REMARK,CUSTOMER_CODE,CUSTOMER_NAME,ORDER_CODE,REMARK,ROUNTING_CODE,LQ_FLAG,CONFIRM_FLAG,RUN_FLAG,BOM_FLAG,ITEM_FLAG,SN_FLAG,THIRD_FLAG,STOCK_FLAG,THIRD_RECEIVE_FLAG,STOCK_RECEIVE_FLAG,'" + theUserName + "',SYSDATE,'修改前记录','Y' "
                            + " FROM data_plan WHERE  TRIM(plan_code) = '" + plancode + "' ";
                    dc.ExeSql(sql1);
                    int qty = Convert.ToInt32(planqty) - Convert.ToInt32(changeqty);
                    sql1 = "update data_plan set plan_qty=" + qty + " where plan_code = '" + plancode + "' ";
                    dc.ExeSql(sql1);
                    sql1 = "  INSERT INTO DATA_PLANLOG(plan_so,plan_code,create_date,remark,modify_type,plan_seq,pline_code,rounting_site) "
                           + " select plan_so,plan_code,sysdate, '修改计划','修改',to_number(plan_seq),pline_code,rounting_site from data_plan "
                           + " where plan_code = '" + plancode + "' ";
                    dc.ExeSql(sql1);
                    sql1 = "INSERT INTO data_plan_LOG(RMES_ID,COMPANY_CODE,PLINE_CODE,PLINE_NAME,PLAN_SEQ,PLAN_CODE,PLAN_SO,PRODUCT_MODEL,PLAN_TYPE,CREATE_USERID,CREATE_USERNAME,CREATE_TIME,BEGIN_DATE,END_DATE,ACCOUNT_DATE,PLAN_QTY,ONLINE_QTY,OFFLINE_QTY,ROUNTING_SITE,ROUNTING_REMARK,CUSTOMER_CODE,CUSTOMER_NAME,ORDER_CODE,REMARK,ROUNTING_CODE,LQ_FLAG,CONFIRM_FLAG,RUN_FLAG,BOM_FLAG,ITEM_FLAG,SN_FLAG,THIRD_FLAG,STOCK_FLAG,THIRD_RECEIVE_FLAG,STOCK_RECEIVE_FLAG,modify_user,modify_time,modify_type,is_bom) "
                          + " SELECT RMES_ID,COMPANY_CODE,PLINE_CODE,PLINE_NAME,PLAN_SEQ,PLAN_CODE,PLAN_SO,PRODUCT_MODEL,PLAN_TYPE,CREATE_USERID,CREATE_USERNAME,CREATE_TIME,BEGIN_DATE,END_DATE,ACCOUNT_DATE,PLAN_QTY,ONLINE_QTY,OFFLINE_QTY,ROUNTING_SITE,ROUNTING_REMARK,CUSTOMER_CODE,CUSTOMER_NAME,ORDER_CODE,REMARK,ROUNTING_CODE,LQ_FLAG,CONFIRM_FLAG,RUN_FLAG,BOM_FLAG,ITEM_FLAG,SN_FLAG,THIRD_FLAG,STOCK_FLAG,THIRD_RECEIVE_FLAG,STOCK_RECEIVE_FLAG,'" + theUserName + "',SYSDATE,'修改后记录','Y' "
                          + " FROM data_plan WHERE  TRIM(plan_code) = '" + plancode + "'";
                    dc.ExeSql(sql1);

                    //回收流水号
                    for (int i = 0; i < Convert.ToInt32(changeqty); i++)
                    {
                        int count1 = Convert.ToInt32(dc.GetValue("select count(1) from data_plan_sn where sn_flag='N' and plan_code='" + plancode + "'"));
                        if (count1 > 0)
                        {
                             string sn1=dc.GetValue("select sn from data_plan_sn where sn_flag='N' and plan_code='"+plancode+"' and rownum=1 ");
                             dc.ExeSql("insert into code_sn_reserve(rmes_id,company_code,pline_code,sn,sn_flag) values(seq_rmes_id.nextval,'"+theCompanyCode+"','"+plinecode+"','"+sn1+"','A') ");
                             dc.ExeSql("delete from data_plan_sn where sn='" + sn1 + "' ");
                        }
                    }
                    //判断分装计划是否上线
                    sql = "select count(1) from data_plan where plan_code like 'F%" + plancode + "' and plan_type='F'";
                    if (dc.GetValue(sql) != "0")
                    {
                        sql1 = "select count(1) from data_product where plan_code like 'F%" + plancode + "'";
                        if (dc.GetValue(sql1) == "0")
                        {
                            //string Rsite = RSite.SelectedItem.Value.ToString();
                            sql1 = "INSERT INTO data_plan_LOG(RMES_ID,COMPANY_CODE,PLINE_CODE,PLINE_NAME,PLAN_SEQ,PLAN_CODE,PLAN_SO,PRODUCT_MODEL,PLAN_TYPE,CREATE_USERID,CREATE_USERNAME,CREATE_TIME,BEGIN_DATE,END_DATE,ACCOUNT_DATE,PLAN_QTY,ONLINE_QTY,OFFLINE_QTY,ROUNTING_SITE,ROUNTING_REMARK,CUSTOMER_CODE,CUSTOMER_NAME,ORDER_CODE,REMARK,ROUNTING_CODE,LQ_FLAG,CONFIRM_FLAG,RUN_FLAG,BOM_FLAG,ITEM_FLAG,SN_FLAG,THIRD_FLAG,STOCK_FLAG,THIRD_RECEIVE_FLAG,STOCK_RECEIVE_FLAG,modify_user,modify_time,modify_type,is_bom) "
                                    + " SELECT RMES_ID,COMPANY_CODE,PLINE_CODE,PLINE_NAME,PLAN_SEQ,PLAN_CODE,PLAN_SO,PRODUCT_MODEL,PLAN_TYPE,CREATE_USERID,CREATE_USERNAME,CREATE_TIME,BEGIN_DATE,END_DATE,ACCOUNT_DATE,PLAN_QTY,ONLINE_QTY,OFFLINE_QTY,ROUNTING_SITE,ROUNTING_REMARK,CUSTOMER_CODE,CUSTOMER_NAME,ORDER_CODE,REMARK,ROUNTING_CODE,LQ_FLAG,CONFIRM_FLAG,RUN_FLAG,BOM_FLAG,ITEM_FLAG,SN_FLAG,THIRD_FLAG,STOCK_FLAG,THIRD_RECEIVE_FLAG,STOCK_RECEIVE_FLAG,'" + theUserName + "',SYSDATE,'修改前记录','Y' "
                                    + " FROM data_plan WHERE  TRIM(plan_code)like 'F%" + plancode + "'  and plan_type='F'";
                            dc.ExeSql(sql1);
                            sql1 = "update data_plan set plan_qty=to_number('" + qty  + "') where plan_code like 'F%" + plancode + "' and plan_type='F'  ";
                            dc.ExeSql(sql1);
                            sql1 = "  INSERT INTO DATA_PLANLOG(plan_so,plan_code,create_date,remark,modify_type,plan_seq,pline_code,rounting_site) "
                                    + " select plan_so,plan_code,sysdate, '修改计划','修改',to_number(plan_seq),pline_code,rounting_site from data_plan "
                                    + " where plan_code like 'F%" + plancode + "' and plan_type='F'  ";
                            dc.ExeSql(sql1);
                            sql1 = "INSERT INTO data_plan_LOG(RMES_ID,COMPANY_CODE,PLINE_CODE,PLINE_NAME,PLAN_SEQ,PLAN_CODE,PLAN_SO,PRODUCT_MODEL,PLAN_TYPE,CREATE_USERID,CREATE_USERNAME,CREATE_TIME,BEGIN_DATE,END_DATE,ACCOUNT_DATE,PLAN_QTY,ONLINE_QTY,OFFLINE_QTY,ROUNTING_SITE,ROUNTING_REMARK,CUSTOMER_CODE,CUSTOMER_NAME,ORDER_CODE,REMARK,ROUNTING_CODE,LQ_FLAG,CONFIRM_FLAG,RUN_FLAG,BOM_FLAG,ITEM_FLAG,SN_FLAG,THIRD_FLAG,STOCK_FLAG,THIRD_RECEIVE_FLAG,STOCK_RECEIVE_FLAG,modify_user,modify_time,modify_type,is_bom) "
                                    + " SELECT RMES_ID,COMPANY_CODE,PLINE_CODE,PLINE_NAME,PLAN_SEQ,PLAN_CODE,PLAN_SO,PRODUCT_MODEL,PLAN_TYPE,CREATE_USERID,CREATE_USERNAME,CREATE_TIME,BEGIN_DATE,END_DATE,ACCOUNT_DATE,PLAN_QTY,ONLINE_QTY,OFFLINE_QTY,ROUNTING_SITE,ROUNTING_REMARK,CUSTOMER_CODE,CUSTOMER_NAME,ORDER_CODE,REMARK,ROUNTING_CODE,LQ_FLAG,CONFIRM_FLAG,RUN_FLAG,BOM_FLAG,ITEM_FLAG,SN_FLAG,THIRD_FLAG,STOCK_FLAG,THIRD_RECEIVE_FLAG,STOCK_RECEIVE_FLAG,'" + theUserName + "',SYSDATE,'修改后记录','Y' "
                                    + " FROM data_plan WHERE  TRIM(plan_code)like 'F%" + plancode + "'  and plan_type='F'";
                            dc.ExeSql(sql1);
                        }
                    }

                    e.Result = "OK,三方物料计划取消提交成功！";
                    return;
                }
                else
                    if (collection[0] == "Check")
                    {
                        string plancode = collection[1]; //计划号
                        //根据计划号判断该用户是否有该生产线权限
                        string sql = "select run_flag,online_qty,third_flag from data_plan where plan_code='" + plancode + "' and plan_type='A' and pline_code in "
                              + " (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + programcode + "' and company_code='" + theCompanyCode + "' )";
                        DataTable dt = dc.GetTable(sql);
                        if (dt.Rows.Count==0)
                        {
                            e.Result = "Fail,无该计划生产线权限！";
                            return;
                        }
                        string runflag = dt.Rows[0][0].ToString();
                        string onlineqty = dt.Rows[0][1].ToString();
                        string thirdflag = dt.Rows[0][2].ToString();
                        if (thirdflag == "N")
                        {
                            e.Result = "Fail,该计划未进行三方物料计算！";
                            return;
                        }
                        if (onlineqty != "0" || runflag == "Y")
                        {
                            e.Result = "Fail,该计划已上线！";
                            return;
                        }
                        e.Result = "OK1,OK";
                    }
            }
            catch (Exception e1)
            {
                e.Result = "Fail,三方物料计划取消提交失败" + e1.Message + "！";
                return;
            }
        }

        protected void ASPxGridView2_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string plancode = ASPxTextPlanCode.Text.Trim().ToUpper();
            string sql = "select t.*,t.rowid from ms_plan_adjust_log t where 1=1  ";
            sql = sql + " and gzdd in (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + programcode + "' and company_code='" + theCompanyCode + "' )";
            sql = sql + " order by log_time desc ";
            DataTable dt = dc.GetTable(sql);
            ASPxGridView2.DataSource = dt;
            ASPxGridView2.DataBind();
        }
    }
}