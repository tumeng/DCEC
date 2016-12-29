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
 * 2016-06-12
 * 涂猛 唐海林
 * 计划维护
 * */

namespace Rmes.WebApp.Rmes.Ssd
{
    public partial class ssd1000 : BasePage
    {
        private dataConn dc = new dataConn();
        private PubCs thePubCs = new PubCs();

        private static string theCompanyCode, theUserId;
        public string theProgramCode;

        public Database db = DB.GetInstance();

        public string strLocation;

        protected void Page_Load(object sender, EventArgs e)
        {

            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();
            theProgramCode = "ssd1000";
            if (!IsPostBack)
            {


                List<ProductLineEntity> user_plines = ProductLineFactory.GetByUserID(theUserManager.getUserId(), theProgramCode);
                ASPxComboBoxPline.DataSource = user_plines;
                ASPxComboBoxPline.TextField = "PLINE_NAME";
                ASPxComboBoxPline.ValueField = "PLINE_CODE";
                ASPxComboBoxPline.DataBind();
                ASPxComboBoxPline.SelectedIndex = 0;

                List<ProductLineEntity> plines = ProductLineFactory.GetAll();
                ASPxComboBoxProcess.DataSource = plines;
                ASPxComboBoxProcess.TextField = "PLINE_NAME";
                ASPxComboBoxProcess.ValueField = "PLINE_CODE";
                ASPxComboBoxProcess.DataBind();
                ASPxComboBoxProcess.Value = ASPxComboBoxPline.Value;

                string sql = "select * from (select '' ALINE_CODE,'' ALINE_NAME from dual union select ALINE_CODE,ALINE_NAME from atpu_acrossline)  order by ALINE_CODE desc ";
                dc.setTheSql(sql);
                ASPxComboBoxAcross.DataSource = dc.GetTable();
                ASPxComboBoxAcross.TextField = "ALINE_NAME";
                ASPxComboBoxAcross.ValueField = "ALINE_CODE";
                ASPxComboBoxAcross.DataBind();
                ASPxComboBoxAcross.SelectedIndex = 0;

                ASPxDateEdit1.Date = DateTime.Now;
                ASPxDateEdit2.Date = DateTime.Now;//.AddDays(5)
                ASPxBeginDate.Date = DateTime.Now;
                ASPxEndDate.Date = DateTime.Now;//.AddDays(5)


                string plineSql1 = "select * from (select '' ALINE_CODE,'' ALINE_NAME from dual union select ALINE_CODE,ALINE_NAME from atpu_acrossline) order by ALINE_CODE desc  ";
                SqlDataSource2.SelectCommand = plineSql1;
                SqlDataSource2.DataBind();

                try
                {
                    ASPxTextBoxSeq.Text = dc.GetValue("select nvl(max(plan_seq),'0')+1 from data_plan where begin_date=to_date('" + ASPxDateEdit1.Date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') and pline_code='" + ASPxComboBoxPline.Value.ToString() + "' and plan_type='A'  ");
                    //ASPxTextBoxSeq.Text = ASPxTextBoxSeq.Text.PadLeft(2, '0');
                    if (ASPxComboBoxPline.Value.ToString() == "E")
                    {
                        ASPxTextPlanCode.Text = "E" + ASPxDateEdit1.Date.ToString("yyyyMMdd") + "-" + ASPxTextBoxSeq.Text.PadLeft(2, '0');
                    }
                    else if (ASPxComboBoxPline.Value.ToString() == "W")
                    {
                        ASPxTextPlanCode.Text = ASPxDateEdit1.Date.ToString("yyyy") + "P" + ASPxDateEdit1.Date.ToString("MMdd") + "-" + ASPxTextBoxSeq.Text.PadLeft(2, '0');
                    }
                    else if (ASPxComboBoxPline.Value.ToString() == "Z")
                    {
                        ASPxTextPlanCode.Text = "Z" + ASPxDateEdit1.Date.ToString("yyyyMMdd") + "-" + ASPxTextBoxSeq.Text.PadLeft(2, '0');
                    }
                    else if (ASPxComboBoxPline.Value.ToString() == "R")
                    {
                        if (ASPxComboBoxProcess.Value.ToString() == "E")
                            ASPxTextPlanCode.Text = "RB" + ASPxDateEdit1.Date.ToString("yyyyMMdd") + "-" + ASPxTextBoxSeq.Text.PadLeft(2, '0');
                        else if (ASPxComboBoxProcess.Value.ToString() == "W")
                            ASPxTextPlanCode.Text = "RC" + ASPxDateEdit1.Date.ToString("yyyyMMdd") + "-" + ASPxTextBoxSeq.Text.PadLeft(2, '0');
                        else if (ASPxComboBoxProcess.Value.ToString() == "R")
                            ASPxTextPlanCode.Text = "RM" + ASPxDateEdit1.Date.ToString("yyyyMMdd") + "-" + ASPxTextBoxSeq.Text.PadLeft(2, '0');
                    }

                }
                catch
                {
                    ASPxTextBoxSeq.Text = "";
                    ASPxTextPlanCode.Text = "";
                }
            }


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
                if(jx1=="")
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
                sql = "select GET_CSKD('"+so+"') from dual";
                dc.setTheSql(sql);
                string bz1=dc.GetValue().ToString();
                str1 = str1 + "," + bz1;
                string plineSql11 = "select * from (select '' ALINE_CODE,'' ALINE_NAME from dual union select ALINE_CODE,ALINE_NAME from atpu_acrossline) where aline_name like '%" + so.ToString().ToUpper() + "%' order by ALINE_CODE desc  ";
                SqlDataSource2.SelectCommand = plineSql11;
                SqlDataSource2.DataBind();
                this.Response.Write(str1);
                this.Response.End();
            }
            string plineSql = "SELECT a.PLINE_CODE,a.PLINE_NAME FROM CODE_PRODUCT_LINE a where pline_code in (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') "
                   + " order by a.pline_code";
            SqlDataSource1.SelectCommand = plineSql;
            SqlDataSource1.DataBind();


            setCondition();
            setCondition1();
        }

        protected void ASPxListBoxUnused_Callback(object sender, CallbackEventArgsBase e)
        {
            //string pline = ASPxGridView1.GetSelectedFieldValues("PLINE_CODE")[0].ToString();

            //string unusedsql = "select t.* from CODE_SN_RESERVE t where t.pline_code='" + pline + "'  order by sn";
            //DataTable unuseddt = dc.GetTable(unusedsql);
            //ASPxListBoxUnused.DataSource = unuseddt;
            //ASPxListBoxUnused.DataBind();
        }

        protected void ASPxListBoxUsed_Callback(object sender, CallbackEventArgsBase e)
        {
            //string pline = ASPxGridView1.GetSelectedFieldValues("PLINE_CODE")[0].ToString();
            //string planCode = ASPxGridView1.GetSelectedFieldValues("PLAN_CODE")[0].ToString();
            //string usedsql = "select t.* from DATA_PLAN_SN t where t.pline_code='" + pline + "' and t.plan_code='" + planCode + "' order by sn";
            //DataTable useddt = dc.GetTable(usedsql);
            //ASPxListBoxUsed.DataSource = useddt;
            //ASPxListBoxUsed.DataBind();
        }


        protected void ASPxCallbackPanel5_Callback(object sender, CallbackEventArgsBase e)
        {
            string[] s = e.Parameter.Split('|');
            string flag = s[0];
            string value = s[1];
            ASPxCallbackPanel5.JSProperties.Clear();
            if (ASPxGridView1.Selection.Count == 0)
            {
                ASPxCallbackPanel5.JSProperties.Add("cpCallbackName", "Fail");
                ASPxCallbackPanel5.JSProperties.Add("cpCallbackRet", "请选择计划！");
                return;
            }
            string pline = ASPxGridView1.GetSelectedFieldValues("PLINE_CODE")[0].ToString();
            string planCode = ASPxGridView1.GetSelectedFieldValues("PLAN_CODE")[0].ToString();
            string planQty = ASPxGridView1.GetSelectedFieldValues("PLAN_QTY")[0].ToString();
            string remark = ASPxGridView1.GetSelectedFieldValues("REMARK")[0].ToString();

            switch (flag)
            {
                case "add":
                    if (string.IsNullOrEmpty(value)) return;
                    string[] _ids = value.Split(',');
                    string ids = "'";
                    for (int i = 0; i < _ids.Length; i++)
                    {
                        ids += _ids[i] + "','";
                    }
                    if (ids.EndsWith(",'"))
                        ids = ids.Substring(0, ids.Length - 2);
                    //判断流水号是否重复
                    ASPxCallbackPanel5.JSProperties.Clear();
                    string sql12 = "select count(1) from data_plan_sn where sn in( select sn from CODE_SN_RESERVE t where t.RMES_ID in ({0}) ) ";
                    sql12 = string.Format(sql12, ids);
                    dc.setTheSql(sql12);
                    if (dc.GetValue()!="0")
                    {
                        //提示重复
                        ASPxCallbackPanel5.JSProperties.Add("cpCallbackName", "Fail");
                        ASPxCallbackPanel5.JSProperties.Add("cpCallbackRet", "流水号重复！");
                        break;
                    }

                    string insert_sql = "insert into DATA_PLAN_SN(rmes_id,company_code,pline_code,plan_code,sn,sn_flag,create_time) select SEQ_RMES_ID.Nextval,'{0}','{1}','{2}',SN,'N',sysdate from CODE_SN_RESERVE t where t.RMES_ID in ({3})";
                    insert_sql = string.Format(insert_sql, theCompanyCode, pline, planCode, ids);
                    dc.ExeSql(insert_sql);

                    insert_sql = "select count(1) from DATA_PLAN_SN where plan_code='" + planCode + "'";
                    dc.setTheSql(insert_sql);
                    if (dc.GetValue().ToString() == planQty)
                    {
                        string sql1 = "update data_plan set sn_flag='Y' where plan_code='" + planCode + "'";
                        dc.ExeSql(sql1);
                    }

                    string sql = "delete from CODE_SN_RESERVE where RMES_ID in (" + ids + ")";
                    dc.ExeSql(sql);

                    break;
                case "del":
                    if (string.IsNullOrEmpty(value)) return;
                    ASPxCallbackPanel5.JSProperties.Clear();
                    string del_pline = ASPxGridView1.GetSelectedFieldValues("PLINE_CODE")[0].ToString();
                    string del_planCode = ASPxGridView1.GetSelectedFieldValues("PLAN_CODE")[0].ToString();

                    string[] del__ids = value.Split(',');
                    string del_ids = "'";
                    for (int i = 0; i < del__ids.Length; i++)
                    {
                        del_ids += del__ids[i] + "','";
                    }
                    if (del_ids.EndsWith(",'"))
                        del_ids = del_ids.Substring(0, del_ids.Length - 2);
                    string update_sql = "insert into CODE_SN_RESERVE(rmes_id,company_code,pline_code,sn,sn_flag) (select seq_rmes_id.nextval,company_code,pline_code,sn,'A' from DATA_PLAN_SN where RMES_ID in (" + del_ids + "))";
                    dc.ExeSql(update_sql);

                    string del_sql = "delete from DATA_PLAN_SN where RMES_ID in (" + del_ids + ")";
                    dc.ExeSql(del_sql);

                    update_sql = "select count(1) from DATA_PLAN_SN where plan_code='" + planCode + "'";
                    dc.setTheSql(update_sql);
                    if (dc.GetValue().ToString() != planQty)
                    {
                        string sql1 = "update data_plan set sn_flag='N' where plan_code='" + planCode + "'";
                        dc.ExeSql(sql1);
                    }

                    break;
                case "YLH":
                    ASPxCallbackPanel5.JSProperties.Clear();
                    string ylh = s[1];
                    int ylhsl = Convert.ToInt32(s[2]);
                    if(!remark.Contains("预留号"))
                    {
                        ASPxCallbackPanel5.JSProperties.Add("cpCallbackName", "Fail");
                        ASPxCallbackPanel5.JSProperties.Add("cpCallbackRet", "非预留号计划！");
                        break;
                    }
                    for (int i = 0; i < ylhsl; i++)
                    {
                        //判断流水号是否重复
                        sql12 = "select count(1) from data_plan_sn where sn='" + ylh + "'  ";
                        dc.setTheSql(sql12);
                        if (dc.GetValue() != "0")
                        {
                            //提示重复
                            ASPxCallbackPanel5.JSProperties.Add("cpCallbackName", "Fail");
                            ASPxCallbackPanel5.JSProperties.Add("cpCallbackRet", "流水号重复！");
                            break;
                        }
                        sql12 = "select count(1) from data_product where sn='" + ylh + "'  ";
                        dc.setTheSql(sql12);
                        if (dc.GetValue() != "0")
                        {
                            //提示重复
                            ASPxCallbackPanel5.JSProperties.Add("cpCallbackName", "Fail");
                            ASPxCallbackPanel5.JSProperties.Add("cpCallbackRet", "流水号重复！");
                            break;
                        }

                        string ylhsql = "insert into DATA_PLAN_SN(rmes_id,company_code,pline_code,plan_code,sn,sn_flag,create_time) values( SEQ_RMES_ID.Nextval,'{0}','{1}','{2}','{3}','N',sysdate )";
                        ylhsql = string.Format(ylhsql, theCompanyCode, pline, planCode,ylh);
                        dc.ExeSql(ylhsql);
                        ylh = (Convert.ToInt32(ylh) + 1).ToString();
                        ylh = ylh.PadLeft(8,'0');
                    }

                    string sql11 = "select count(1) from DATA_PLAN_SN where plan_code='" + planCode + "'";
                    dc.setTheSql(sql11);
                    if (dc.GetValue().ToString() == planQty)
                    {
                        string sql1 = "update data_plan set sn_flag='Y' where plan_code='" + planCode + "'";
                        dc.ExeSql(sql1);
                    }
                    break;
                default:
                    ASPxCallbackPanel5.JSProperties.Clear();
                    break;
            }
            string usedsql = "select t.* from DATA_PLAN_SN t where t.pline_code='" + pline + "' and t.plan_code='" + planCode + "' order by sn";
            DataTable useddt = dc.GetTable(usedsql);
            ASPxListBoxUsed.DataSource = useddt;
            ASPxListBoxUsed.DataBind();
            //ASPxListBoxUsed.SelectAll();
            string unusedsql = "select t.* from CODE_SN_RESERVE t where t.pline_code='" + pline + "' order by sn ";
            DataTable unuseddt = dc.GetTable(unusedsql);
            ASPxListBoxUnused.DataSource = unuseddt;
            ASPxListBoxUnused.DataBind();

            lblYqty.Text = useddt.Rows.Count.ToString();
            YLH.Text = "";
            YLHsl.Text = "1";

        }

        protected void ASPxCallbackPanel4_Callback(object sender, CallbackEventArgsBase e)
        {
            for (int i = 0; i < ASPxComboBoxProcess.Items.Count; i++)
            {
                if (ASPxComboBoxProcess.Items[i].Value.ToString() == ASPxComboBoxPline.SelectedItem.Value.ToString())
                    ASPxComboBoxProcess.Items[i].Selected = true;
            }
        }
        protected void ASPxCallbackPanel8_Callback(object sender, CallbackEventArgsBase e)
        {
            string plineSql1 = "select * from (select '' ALINE_CODE,'' ALINE_NAME from dual union select ALINE_CODE,ALINE_NAME from atpu_acrossline) where aline_name like '%" + ASPxTextBoxSO.Text + "%' order by ALINE_CODE desc  ";
            DataTable dt = dc.GetTable(plineSql1);
            ASPxComboBoxAcross.DataSource = dt;
            ASPxComboBoxAcross.DataBind();
        }

        //计划调序
        public void ASPxGridView1_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
        {
            string s = e.Parameters;
            string[] s1 = s.Split('|');
            string type1 = s1[0];
            int rowIndex = int.Parse(s1[1]);
            string sql = "";
            string rmesid, planseq, begindate, plinecode1;

            ASPxGridView atl1 = (ASPxGridView)sender;
            int count1 = atl1.Selection.Count;
            List<object> aa = atl1.GetSelectedFieldValues("RMES_ID");

            try
            {
                rmesid = ASPxGridView1.GetRowValues(rowIndex, "RMES_ID").ToString();
                planseq = ASPxGridView1.GetRowValues(rowIndex, "PLAN_SEQ").ToString();
                begindate = Convert.ToString(ASPxGridView1.GetRowValues(rowIndex, "BEGIN_DATE")).Substring(0, Convert.ToString(ASPxGridView1.GetRowValues(rowIndex, "BEGIN_DATE")).IndexOf(" "));
                plinecode1 = ASPxGridView1.GetRowValues(rowIndex, "PLINE_CODE") as string;
            }
            catch
            {
                e.Result = "Fail,缺少关键值！";
                return;
            }
            switch (type1)
            {
                case "Up":
                    sql = "select max(plan_seq) from data_plan where begin_date=to_date('" + begindate + "','yyyy-mm-dd') and plan_seq<'" + planseq + "' and pline_code='" + plinecode1 + "'  and plan_type='A'";
                    dc.setTheSql(sql);
                    string planseq1=dc.GetTable().Rows[0][0].ToString();
                    if (dc.GetTable().Rows.Count == 0 || planseq1=="")
                    {
                        e.Result = "Fail,当前已最小序！";
                        break;
                    }

                    sql = "update data_plan set plan_seq='" + planseq + "' where plan_seq='" + planseq1 + "' and  begin_date=to_date('" + begindate + "','yyyy-mm-dd') and pline_code='" + plinecode1 + "' and plan_type='A' ";
                    dc.ExeSql(sql);
                    sql = "update data_plan set plan_seq='"+planseq1+"' where rmes_id='"+rmesid+"' ";
                    dc.ExeSql(sql);
                    //e.Result = "OK,上调成功！";
                    break;
                case "Down":
                    sql = "select min(plan_seq) from data_plan where begin_date=to_date('" + begindate + "','yyyy-mm-dd') and plan_seq>'" + planseq + "' and pline_code='" + plinecode1 + "' and plan_type='A' ";
                    dc.setTheSql(sql);
                    planseq1 = dc.GetTable().Rows[0][0].ToString();
                    if (dc.GetTable().Rows.Count == 0 || planseq1 == "")
                    {
                        e.Result = "Fail,当前已最大序！";
                        break;
                    }
                    sql = "update data_plan set plan_seq='" + planseq + "' where plan_seq='" + planseq1 + "' and  begin_date=to_date('" + begindate + "','yyyy-mm-dd') and pline_code='" + plinecode1 + "' and plan_type='A' ";
                    dc.ExeSql(sql);
                    sql = "update data_plan set plan_seq='" + planseq1 + "' where rmes_id='" + rmesid + "' ";
                    dc.ExeSql(sql);
                    //e.Result = "OK,下调成功！";
                    break;
                case "Confirm":
                    for (int i = 0; i < count1;i++ )
                    {
                        sql = "update data_plan set confirm_flag='Y' where rmes_id='" + aa[i] + "' and item_flag='N' and run_flag='N' and third_flag='N'";
                        dc.ExeSql(sql);
                    }
                    //e.Result = "OK,已确认！";
                    break;
                case "Cancel":
                    for (int i = 0; i < count1; i++)
                    {
                        sql = "update data_plan set confirm_flag='N' where rmes_id='" + aa[i] + "' and item_flag='N' and run_flag='N' and third_flag='N' ";
                        dc.ExeSql(sql);
                    }
                    //e.Result = "OK,取消确认！";
                    break;
                default:

                    break;
            }
        }
        //根据so获取机型
        protected void ASPxCallbackPanel1_Callback(object sender, CallbackEventArgsBase e)
        {
            string so = e.Parameter;
            string sql = "select jx,config from copy_engine_property where SO='" + so.ToUpper() + "'";
            dc.ExeSql(sql);
            if (dc.GetTable().Rows.Count == 0)
            {
                ;
            }
            string config1 = dc.GetTable().Rows[0][1].ToString();
            string jx1 = dc.GetTable().Rows[0][0].ToString();
            ASPxTextSeries.Text = jx1;
        }


        protected void ASPxCallbackPanel2_Callback(object sender, CallbackEventArgsBase e)
        {
            ASPxBeginDate.Date = ASPxDateEdit1.Date;
        }
        protected void ASPxCallbackPanel9_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                ASPxTextBoxSeq.Text = dc.GetValue("select nvl(max(plan_seq),'0')+1 from data_plan where begin_date=to_date('" + ASPxDateEdit1.Date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') and pline_code='" + ASPxComboBoxPline.Value.ToString() + "' and plan_type='A' ");
                //ASPxTextBoxSeq.Text = ASPxTextBoxSeq.Text.PadLeft(2, '0');
                if (ASPxComboBoxPline.Value.ToString() == "E")
                {
                    ASPxTextPlanCode.Text = "E" + ASPxDateEdit1.Date.ToString("yyyyMMdd") + "-" + ASPxTextBoxSeq.Text.PadLeft(2, '0');
                }
                else if (ASPxComboBoxPline.Value.ToString() == "W")
                {
                    ASPxTextPlanCode.Text = ASPxDateEdit1.Date.ToString("yyyy") + "P" + ASPxDateEdit1.Date.ToString("MMdd") + "-" + ASPxTextBoxSeq.Text.PadLeft(2, '0');
                }
                else if (ASPxComboBoxPline.Value.ToString() == "Z")
                {
                    ASPxTextPlanCode.Text = "Z" + ASPxDateEdit1.Date.ToString("yyyyMMdd") + "-" + ASPxTextBoxSeq.Text.PadLeft(2, '0');
                }
                else if (ASPxComboBoxPline.Value.ToString() == "R")
                {
                    if (ASPxComboBoxProcess.Value.ToString() == "E")
                        ASPxTextPlanCode.Text = "RB" + ASPxDateEdit1.Date.ToString("yyyyMMdd") + "-" + ASPxTextBoxSeq.Text.PadLeft(2, '0');
                    else if (ASPxComboBoxProcess.Value.ToString() == "W")
                        ASPxTextPlanCode.Text = "RC" + ASPxDateEdit1.Date.ToString("yyyyMMdd") + "-" + ASPxTextBoxSeq.Text.PadLeft(2, '0');
                    else if (ASPxComboBoxProcess.Value.ToString() == "R")
                        ASPxTextPlanCode.Text = "RM" + ASPxDateEdit1.Date.ToString("yyyyMMdd") + "-" + ASPxTextBoxSeq.Text.PadLeft(2, '0');
                }

            }
            catch
            {
                ASPxTextBoxSeq.Text = "";
                ASPxTextPlanCode.Text = "";
            }
        }

        protected void ASPxCallbackPanel10_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                ASPxTextBoxSeq.Text = dc.GetValue("select nvl(max(plan_seq),'0')+1 from data_plan where begin_date=to_date('" + ASPxDateEdit1.Date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') and pline_code='" + ASPxComboBoxPline.Value.ToString() + "' and plan_type='A'  ");
                //ASPxTextBoxSeq.Text = ASPxTextBoxSeq.Text.PadLeft(2, '0');
                if (ASPxComboBoxPline.Value.ToString() == "E")
                {
                    ASPxTextPlanCode.Text = "E" + ASPxDateEdit1.Date.ToString("yyyyMMdd") + "-" + ASPxTextBoxSeq.Text.PadLeft(2, '0');
                }
                else if (ASPxComboBoxPline.Value.ToString() == "W")
                {
                    ASPxTextPlanCode.Text = ASPxDateEdit1.Date.ToString("yyyy") + "P" + ASPxDateEdit1.Date.ToString("MMdd") + "-" + ASPxTextBoxSeq.Text.PadLeft(2, '0');
                }
                else if (ASPxComboBoxPline.Value.ToString() == "Z")
                {
                    ASPxTextPlanCode.Text = "Z" + ASPxDateEdit1.Date.ToString("yyyyMMdd") + "-" + ASPxTextBoxSeq.Text.PadLeft(2, '0');
                }
                else if (ASPxComboBoxPline.Value.ToString() == "R")
                {
                    if (ASPxComboBoxProcess.Value.ToString() == "E")
                        ASPxTextPlanCode.Text = "RB" + ASPxDateEdit1.Date.ToString("yyyyMMdd") + "-" + ASPxTextBoxSeq.Text.PadLeft(2, '0');
                    else if (ASPxComboBoxProcess.Value.ToString() == "W")
                        ASPxTextPlanCode.Text = "RC" + ASPxDateEdit1.Date.ToString("yyyyMMdd") + "-" + ASPxTextBoxSeq.Text.PadLeft(2, '0');
                    else if (ASPxComboBoxProcess.Value.ToString() == "R")
                        ASPxTextPlanCode.Text = "RM" + ASPxDateEdit1.Date.ToString("yyyyMMdd") + "-" + ASPxTextBoxSeq.Text.PadLeft(2, '0');
                }

            }
            catch
            {
                ASPxTextBoxSeq.Text = "";
                ASPxTextPlanCode.Text = "";
            }
        }
        protected void ASPxCallbackPanel3_Callback(object sender, CallbackEventArgsBase e)
        {
            ASPxEndDate.Date = ASPxDateEdit1.Date;
        }

        void ReloadGridviewData()
        {
            //List<PlanEntity> listPlan = db.Fetch<PlanEntity>("");

            //ASPxGridView1.DataSource = listPlan;

            string sql = "select t.*,to_char(t.begin_date,'yyyy-mm-dd') begin_date1,e.team_name,a.pline_name,b.user_name,c.shift_name,d.product_name from vw_data_plan t left join code_product_line a on a.rmes_id=t.pline_code "
                + " left join code_user b on b.user_id=t.create_user_id left join code_shift c on c.rmes_id=t.shift_code left join code_product d on t.product_code=d.product_code "
                + " left join code_team e on t.team_code=e.team_code and t.pline_code=e.pline_code where online_qty=0 and item_flag='N' order by t.CREATE_TIME desc";

            DataTable dt = dc.GetTable(sql);
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
        }

        protected void ASPxGridView1_Init(object sender, EventArgs e)
        {
            string sql = "select * from VW_DATA_PLAN ";
            DataTable dt = dc.GetTable(sql);
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
        }
        private void setCondition1()
        {
            string sql = "select rmes_id,plan_code,plan_seq from VW_DATA_PLAN where plan_type='A'  and begin_date>=to_date('" + ASPxDateEdit1.Date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') and begin_date<=to_date('" + ASPxDateEdit2.Date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') and pline_code in(select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') order by begin_date,plan_seq ";
            DataTable dt = dc.GetTable(sql);
            ASPxGridView2.DataSource = dt;
            ASPxGridView2.DataBind();
        }
        private void setCondition()
        {
            string sql = "select * from VW_DATA_PLAN where plan_type='A' and begin_date>=to_date('" + ASPxDateEdit1.Date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') and begin_date<=to_date('" + ASPxDateEdit2.Date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') and pline_code in(select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') order by begin_date,plan_seq ";
            DataTable dt = dc.GetTable(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string remark1 = dt.Rows[i]["REMARK"].ToString();
                sql = "select FUNC_GET_REMARK('" + dt.Rows[i]["PLAN_CODE"].ToString() + "','" + remark1 + "') from dual ";
                dc.setTheSql(sql);
                dt.Rows[i]["REMARK"] = dc.GetValue(); ;
            }
                ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();

            
        }
        protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            setCondition();
            ASPxGridView1.Selection.UnselectAll();
        }
        protected void ASPxGridView2_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            setCondition1();
        }

        protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            ASPxComboBox PlineCode = ASPxGridView1.FindEditFormTemplateControl("PlineCombo") as ASPxComboBox;
            ASPxComboBox Rsite = ASPxGridView1.FindEditFormTemplateControl("RountingsiteCombo") as ASPxComboBox;
            string plancode = e.NewValues["PLAN_CODE"].ToString();
            string planseq = e.NewValues["PLAN_SEQ"].ToString();
            string planso = e.NewValues["PLAN_SO"].ToString();
            string qty1 = e.OldValues["PLAN_QTY"].ToString();
            string qty2 = e.NewValues["PLAN_QTY"].ToString();
            string customer = e.NewValues["CUSTOMER_NAME"].ToString();
            string productseries = e.NewValues["PRODUCT_MODEL"].ToString();
            string begindate1 = e.NewValues["BEGIN_DATE"].ToString();
            string enddate1 = e.NewValues["END_DATE"].ToString();
            //string snflag1 = e.OldValues["SN_FLAG"].ToString();
            int qty11 = Convert.ToInt32(qty1);
            int qty21=0;
            if (planso == "" || productseries == "" || qty2 == "" || begindate1 == "" || enddate1 == ""||planseq=="" ||customer=="")
            {
                e.RowError = "Fail,计划参数缺少！";
                return;
            }
            try
            {
                qty21=Convert.ToInt32(qty2);
            }
            catch
            {
                e.RowError = "Fail,计划数量不合法！";
                return;
            }
            string snflag1 = dc.GetValue("select nvl(upper(sn_flag),'N') from data_plan where plan_code='" + plancode + "' and rownum=1 ");
            if (snflag1=="Y" && qty11 < qty21)
            {
                e.RowError = "Fail,计划数量不能大于原数量！";
                return;
            }
            string isSeq = dc.GetValue("select count(1) from data_plan where plan_seq=" + planseq + " and pline_code='" + PlineCode.Value.ToString() + "' and begin_date=to_date('" + begindate1.Substring(0, begindate1.IndexOf(' ')) + "','yyyy-mm-dd') and plan_type='A' and plan_code!='" + plancode + "'");
            if (isSeq != "0")
            {
                e.RowError = "Fail,计划序重复！";
                return;
            }

            if (begindate1.CompareTo(enddate1)>0)
            {
                e.RowError = "Fail,结束日期不能小于开始日期！";
                return;
            }

            //调用函数判断计划代码是否符合规范
            string sqlPlan = "select func_check_plan('B','" + plancode + "','" + PlineCode.Value.ToString() + "','" + Rsite.Value.ToString() + "','" + planso + "') from dual";
            dc.setTheSql(sqlPlan);
            string getvalue1 = dc.GetValue();
            char[] charSeparators1 = new char[] { '#' };
            string[] collection1 = getvalue1.Split(charSeparators1);

            if (collection1[0] != "OK")
            {
                e.RowError = "Fail," + collection1[1];
                return;
            }

        }


        protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxTextBox rmesid = ASPxGridView1.FindEditFormTemplateControl("txtRmesid") as ASPxTextBox;
            ASPxTextBox plancode = ASPxGridView1.FindEditFormTemplateControl("txtPlanCode") as ASPxTextBox;
            ASPxTextBox planseq = ASPxGridView1.FindEditFormTemplateControl("txtPlanSeq") as ASPxTextBox;
            ASPxTextBox planso = ASPxGridView1.FindEditFormTemplateControl("txtPlanSO") as ASPxTextBox;
            ASPxTextBox planseries = ASPxGridView1.FindEditFormTemplateControl("productSeries") as ASPxTextBox;
            ASPxTextBox qty = ASPxGridView1.FindEditFormTemplateControl("ASPxplanqty") as ASPxTextBox;
            ASPxTextBox customer = ASPxGridView1.FindEditFormTemplateControl("ASPxCustomer") as ASPxTextBox;
            ASPxComboBox across = ASPxGridView1.FindEditFormTemplateControl("ASPxComboBoxAcross1") as ASPxComboBox;
            ASPxMemo remark = ASPxGridView1.FindEditFormTemplateControl("txtRemark") as ASPxMemo;
            ASPxComboBox PlineCode = ASPxGridView1.FindEditFormTemplateControl("PlineCombo") as ASPxComboBox;
            ASPxComboBox RSite = ASPxGridView1.FindEditFormTemplateControl("RountingsiteCombo") as ASPxComboBox;
            ASPxDateEdit begindate1 = ASPxGridView1.FindEditFormTemplateControl("ASPxDateBegin") as ASPxDateEdit;
            ASPxDateEdit enddate1 = ASPxGridView1.FindEditFormTemplateControl("ASPxDateEnd") as ASPxDateEdit;

            DateTime beginDate = begindate1.Date;
            DateTime endDate = enddate1.Date;

            userManager theUserManager = (userManager)Session["theUserManager"];

            string gylx1;
            string accountdate1;
            string ROUNTING_CODE1 = "";
            try
            {
                ROUNTING_CODE1 = across.Value.ToString(); //工艺路线代码，默认为
            }
            catch
            {
            }
            //调用函数判断计划代码是否符合规范
            string sqlPlan = "select func_check_plan('B','" + plancode.Text.Trim() + "','" + PlineCode.SelectedItem.Value + "','" + RSite.SelectedItem.Value + "','" + planso.Text.Trim() + "') from dual";
            dc.setTheSql(sqlPlan);
            string getvalue1 = dc.GetValue();
            char[] charSeparators1 = new char[] { '#' };
            string[] collection1 = getvalue1.Split(charSeparators1);

            if (collection1[0] != "OK")
            {
                return;
            }
            else
            {
                accountdate1 = collection1[1];
                gylx1 = collection1[2];
            }
            string oldso="",oldqty="";
            oldso = dc.GetValue("select nvl(plan_so,'') from data_plan where rmes_id='"+rmesid.Text+"' and rownum=1");
            oldqty = dc.GetValue("select nvl(plan_qty,'0') from data_plan where rmes_id='" + rmesid.Text + "' and rownum=1");

            PlanFactory.PL_CREATE_PLAN("MODIFY", theCompanyCode, PlineCode.SelectedItem.Value.ToString(), PlineCode.SelectedItem.Text, planseq.Text.Trim(), plancode.Text.Trim(), planso.Text.Trim(), planseries.Text.Trim(), "A", theUserId, theUserManager.getUserName(), beginDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"), accountdate1, qty.Text.Trim(), "0", "0", RSite.SelectedItem.Value.ToString(), gylx1, customer.Text.Trim(), remark.Text.Trim(), ROUNTING_CODE1,"Y");
            if (oldso != planso.Text.Trim())
            {
                //SO不相等更改BOM状态，删除替换关系
                dc.ExeSql("update data_plan set bom_flag='N' where rmes_id='" + rmesid.Text + "' ");
                dc.ExeSql("delete from sjbomsoth where jhdm='" + plancode.Text.Trim() + "' ");
                dc.ExeSql("delete from sjbomsothmuti where jhdm='" + plancode.Text.Trim() + "' ");
                dc.ExeSql("delete from data_plan_standard_bom where plan_code='" + plancode.Text.Trim() + "' ");
            }
            //判断是否存在分装计划
            string sql = "select count(1) from data_plan where plan_code like 'F%" + plancode.Text.Trim() + "' and plan_type='F'";
            if (dc.GetValue(sql) != "0")
            {
                string sql1 = "select count(1) from data_product where plan_code like 'F%" + plancode.Text.Trim() + "'";
                if (dc.GetValue(sql1) == "0")
                {
                    string Rsite = RSite.SelectedItem.Value.ToString();
                    //未上线删除分装计划

                    if (oldqty != qty.Text.Trim())  //数量发生变化 ，更改分装计划数量
                    {
                        //PlanFactory.PL_CREATE_PLAN("MODIFY", theCompanyCode, PlineCode.SelectedItem.Value.ToString(), PlineCode.SelectedItem.Text, planseq.Text.Trim(), plancode.Text.Trim(), planso.Text.Trim(), planseries.Text.Trim(), "F", theUserId, theUserManager.getUserName(), beginDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"), accountdate1, qty.Text.Trim(), "0", "0", RSite.SelectedItem.Value.ToString(), gylx1, customer.Text.Trim(), remark.Text.Trim(), ROUNTING_CODE1, "Y");
                        sql1 = "INSERT INTO data_plan_LOG(RMES_ID,COMPANY_CODE,PLINE_CODE,PLINE_NAME,PLAN_SEQ,PLAN_CODE,PLAN_SO,PRODUCT_MODEL,PLAN_TYPE,CREATE_USERID,CREATE_USERNAME,CREATE_TIME,BEGIN_DATE,END_DATE,ACCOUNT_DATE,PLAN_QTY,ONLINE_QTY,OFFLINE_QTY,ROUNTING_SITE,ROUNTING_REMARK,CUSTOMER_CODE,CUSTOMER_NAME,ORDER_CODE,REMARK,ROUNTING_CODE,LQ_FLAG,CONFIRM_FLAG,RUN_FLAG,BOM_FLAG,ITEM_FLAG,SN_FLAG,THIRD_FLAG,STOCK_FLAG,THIRD_RECEIVE_FLAG,STOCK_RECEIVE_FLAG,modify_user,modify_time,modify_type,is_bom) "
                              + " SELECT RMES_ID,COMPANY_CODE,PLINE_CODE,PLINE_NAME,PLAN_SEQ,PLAN_CODE,PLAN_SO,PRODUCT_MODEL,PLAN_TYPE,CREATE_USERID,CREATE_USERNAME,CREATE_TIME,BEGIN_DATE,END_DATE,ACCOUNT_DATE,PLAN_QTY,ONLINE_QTY,OFFLINE_QTY,ROUNTING_SITE,ROUNTING_REMARK,CUSTOMER_CODE,CUSTOMER_NAME,ORDER_CODE,REMARK,ROUNTING_CODE,LQ_FLAG,CONFIRM_FLAG,RUN_FLAG,BOM_FLAG,ITEM_FLAG,SN_FLAG,THIRD_FLAG,STOCK_FLAG,THIRD_RECEIVE_FLAG,STOCK_RECEIVE_FLAG,'" + theUserManager.getUserName() + "',SYSDATE,'修改前记录','Y' "
                              + " FROM data_plan WHERE  TRIM(plan_code)like 'F%" + plancode.Text.Trim() + "'  and plan_type='F'";
                        dc.ExeSql(sql1);
                        sql1 = "update data_plan set plan_qty=to_number('"+qty.Text.Trim()+"') where plan_code like 'F%" + plancode.Text.Trim() + "' and plan_type='F'  ";
                        dc.ExeSql(sql1);
                        sql1 = "  INSERT INTO DATA_PLANLOG(plan_so,plan_code,create_date,remark,modify_type,plan_seq,pline_code,rounting_site) "
                               + " select plan_so,plan_code,sysdate, '修改计划','修改',to_number(plan_seq),pline_code,rounting_site from data_plan "
                               + " where plan_code like 'F%" + plancode.Text.Trim() + "' and plan_type='F'  ";
                        dc.ExeSql(sql1);
                        sql1 = "INSERT INTO data_plan_LOG(RMES_ID,COMPANY_CODE,PLINE_CODE,PLINE_NAME,PLAN_SEQ,PLAN_CODE,PLAN_SO,PRODUCT_MODEL,PLAN_TYPE,CREATE_USERID,CREATE_USERNAME,CREATE_TIME,BEGIN_DATE,END_DATE,ACCOUNT_DATE,PLAN_QTY,ONLINE_QTY,OFFLINE_QTY,ROUNTING_SITE,ROUNTING_REMARK,CUSTOMER_CODE,CUSTOMER_NAME,ORDER_CODE,REMARK,ROUNTING_CODE,LQ_FLAG,CONFIRM_FLAG,RUN_FLAG,BOM_FLAG,ITEM_FLAG,SN_FLAG,THIRD_FLAG,STOCK_FLAG,THIRD_RECEIVE_FLAG,STOCK_RECEIVE_FLAG,modify_user,modify_time,modify_type,is_bom) "
                              + " SELECT RMES_ID,COMPANY_CODE,PLINE_CODE,PLINE_NAME,PLAN_SEQ,PLAN_CODE,PLAN_SO,PRODUCT_MODEL,PLAN_TYPE,CREATE_USERID,CREATE_USERNAME,CREATE_TIME,BEGIN_DATE,END_DATE,ACCOUNT_DATE,PLAN_QTY,ONLINE_QTY,OFFLINE_QTY,ROUNTING_SITE,ROUNTING_REMARK,CUSTOMER_CODE,CUSTOMER_NAME,ORDER_CODE,REMARK,ROUNTING_CODE,LQ_FLAG,CONFIRM_FLAG,RUN_FLAG,BOM_FLAG,ITEM_FLAG,SN_FLAG,THIRD_FLAG,STOCK_FLAG,THIRD_RECEIVE_FLAG,STOCK_RECEIVE_FLAG,'" + theUserManager.getUserName() + "',SYSDATE,'修改后记录','Y' "
                              + " FROM data_plan WHERE  TRIM(plan_code)like 'F%" + plancode.Text.Trim() + "'  and plan_type='F'";
                        dc.ExeSql(sql1);
                    }
                    if (oldso != planso.Text.Trim()) //SO发生变化 删除分装计划 删除BOM
                    {
                         //PlanFactory.PL_CREATE_PLAN("DELETE", theCompanyCode, PlineCode.SelectedItem.Value.ToString(), "", planseq.Text.Trim(), plancode.Text.Trim(), planso.Text.Trim(), "", "F", theUserId, theUserManager.getUserName(), "", "", "", qty.Text.Trim(), "0", "0", Rsite, "", "", "", "", "Y");
                        sql1 = "INSERT INTO data_plan_LOG(RMES_ID,COMPANY_CODE,PLINE_CODE,PLINE_NAME,PLAN_SEQ,PLAN_CODE,PLAN_SO,PRODUCT_MODEL,PLAN_TYPE,CREATE_USERID,CREATE_USERNAME,CREATE_TIME,BEGIN_DATE,END_DATE,ACCOUNT_DATE,PLAN_QTY,ONLINE_QTY,OFFLINE_QTY,ROUNTING_SITE,ROUNTING_REMARK,CUSTOMER_CODE,CUSTOMER_NAME,ORDER_CODE,REMARK,ROUNTING_CODE,LQ_FLAG,CONFIRM_FLAG,RUN_FLAG,BOM_FLAG,ITEM_FLAG,SN_FLAG,THIRD_FLAG,STOCK_FLAG,THIRD_RECEIVE_FLAG,STOCK_RECEIVE_FLAG,modify_user,modify_time,modify_type,is_bom) "
                             + " SELECT RMES_ID,COMPANY_CODE,PLINE_CODE,PLINE_NAME,PLAN_SEQ,PLAN_CODE,PLAN_SO,PRODUCT_MODEL,PLAN_TYPE,CREATE_USERID,CREATE_USERNAME,CREATE_TIME,BEGIN_DATE,END_DATE,ACCOUNT_DATE,PLAN_QTY,ONLINE_QTY,OFFLINE_QTY,ROUNTING_SITE,ROUNTING_REMARK,CUSTOMER_CODE,CUSTOMER_NAME,ORDER_CODE,REMARK,ROUNTING_CODE,LQ_FLAG,CONFIRM_FLAG,RUN_FLAG,BOM_FLAG,ITEM_FLAG,SN_FLAG,THIRD_FLAG,STOCK_FLAG,THIRD_RECEIVE_FLAG,STOCK_RECEIVE_FLAG,'" + theUserManager.getUserName() + "',SYSDATE,'删除记录','Y' "
                             + " FROM data_plan WHERE  TRIM(plan_code)like 'F%" + plancode.Text.Trim() + "'  and plan_type='F'";
                        dc.ExeSql(sql1);
                        sql1 = "  INSERT INTO DATA_PLANLOG(plan_so,plan_code,create_date,remark,modify_type,plan_seq,pline_code,rounting_site) "
                           + " select plan_so,plan_code,sysdate, '删除计划','删除',to_number(plan_seq),pline_code,rounting_site from data_plan "
                           + " where plan_code like 'F%" + plancode.Text.Trim() + "' and plan_type='F'  ";
                        dc.ExeSql(sql1);
                        sql1 = "delete from data_plan_standard_bom where plan_code like 'F%" + plancode.Text.Trim() + "' ";
                        dc.ExeSql(sql1);
                        sql1 = "delete from data_plan where plan_code like 'F%" + plancode.Text.Trim() + "' and plan_type='F' ";
                        dc.ExeSql(sql1);
                        dc.ExeSql("delete from ATPUJHB_FROMMAIN where JHMC='" + plancode.Text.Trim() + "'");
                    }

                }
            }
            e.Cancel = true;
            ASPxGridView1.CancelEdit();
            setCondition();
        }
        public string ConvertFormat(string str)
        {//内容为我们需要转换成的格式

            string result = "";
            if (str.Length <= 10)
            {
                result = str;
            }
            else
            {
                result = str.Substring(0, 10) + "......";
            }
            return result;

        }
        protected void callbackPanel_Callback(object sender, CallbackEventArgsBase e)
        {
            litText.Text = GetNotes(e.Parameter).ToString();

        }
        object GetNotes(string id)
        {
            string sql = "SELECT PLAN_CODE,REMARK FROM VW_DATA_PLAN WHERE  RMES_ID='" + id + "'";
            dataConn dc = new dataConn(sql);
            DataTable dt = dc.GetTable();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string remark1 = dt.Rows[i]["REMARK"].ToString();
                sql = "select FUNC_GET_REMARK('" + dt.Rows[i]["PLAN_CODE"].ToString() + "','" + remark1 + "') from dual ";
                dc.setTheSql(sql);
                dt.Rows[i]["REMARK"] = dc.GetValue(); ;
            }
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["REMARK"];
            }
            return null;
        }

        //最终提交生成计划及计划关联BOM清单
        protected void ASPxCbSubmit_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
        {
            char[] charSeparators = new char[] { ',' };
            string[] collection = e.Parameter.Split(charSeparators);
            int cnt = collection.Length;
            if (collection[0] == "LS")
            {
                //自动分配流水号
                //获取计划，判断计划类型
                List<string> s = new List<string>();
                for (int i = 1; i < cnt; i++)
                {
                    s.Add(collection[i].ToString());
                }
                string[] s1 = s.ToArray();

                if (ASPxGridView1.Selection.Count == 0)
                {
                    e.Result = "Fail,请选择计划！";
                    return;
                }
                int count2 = 0;
                for (int i = 0; i < s1.Length; i++)
                {
                    string rmesid = s1[i].ToString();
                    string sqlls = "select FUNC_CHECK_PLANSN('"+rmesid+"') from dual";
                    dc.setTheSql(sqlls);
                    if (dc.GetValue()=="OK")
                    {
                        //自动分配流水号 计划数量为1的优先从留号表中选，特定客户的选取客户号段
                        try
                        {
                            PlanFactory.PL_CREATE_PLANSN(rmesid);
                        }
                        catch (Exception ex)
                        {
                            count2++;
                            //e.Result = "Fail,流水号生成失败！";
                            //return;
                            break;
                        }
                    }
                }
                if (count2 > 0)
                {
                    e.Result = "Fail,部分计划未正常生成流水号！";
                    return;
                }
                e.Result = "OK,流水号生成成功！";
                return;
            }
            else if (collection[0] == "MoveUp")
            {
                string rmesid = collection[1];
                string begindate = dc.GetValue("select to_char(begin_date,'yyyy-mm-dd') from data_plan where rmes_id='" + rmesid + "'");
                string planseq = dc.GetValue("select plan_seq from data_plan where rmes_id='" + rmesid + "'");
                string itemflag1 = dc.GetValue("select item_flag from data_plan where rmes_id='" + rmesid + "'");
                string plinecode1 = dc.GetValue("select pline_code from data_plan where rmes_id='" + rmesid + "'");
                string sql = "select max(plan_seq) from data_plan where begin_date=to_date('" + begindate + "','yyyy-mm-dd') and plan_seq<'" + planseq + "' and item_flag='N' and pline_code='" + plinecode1 + "' and run_flag='N' and third_flag='N'  and plan_type='A'";
                dc.setTheSql(sql);
                string planseq1=dc.GetTable().Rows[0][0].ToString();
                if (dc.GetTable().Rows.Count == 0 || planseq1=="")
                {
                    e.Result = "Fail1,当前已最小序！";
                    return;
                }
                if (itemflag1 == "R")
                {
                    e.Result = "Fail1,该计划已库房接收！";
                    return;
                }
                sql = "update data_plan set plan_seq='" + planseq + "' where plan_seq='" + planseq1 + "' and  begin_date=to_date('" + begindate + "','yyyy-mm-dd') and item_flag='N'  and pline_code='" + plinecode1 + "' and plan_type='A' ";
                dc.ExeSql(sql);
                sql = "update data_plan set plan_seq='"+planseq1+"' where rmes_id='"+rmesid+"' ";
                dc.ExeSql(sql);
                e.Result = "OK1,";
                return;
            }
            else if (collection[0] == "MoveDown")
            {
                string rmesid = collection[1];
                string begindate = dc.GetValue("select to_char(begin_date,'yyyy-mm-dd') from data_plan where rmes_id='" + rmesid + "'");
                string planseq = dc.GetValue("select plan_seq from data_plan where rmes_id='" + rmesid + "'");
                string itemflag1 = dc.GetValue("select item_flag from data_plan where rmes_id='" + rmesid + "'");
                string plinecode1 = dc.GetValue("select pline_code from data_plan where rmes_id='" + rmesid + "'");
                string sql = "select min(plan_seq) from data_plan where begin_date=to_date('" + begindate + "','yyyy-mm-dd') and plan_seq>'" + planseq + "' and pline_code='" + plinecode1 + "' and item_flag='N'  and run_flag='N' and third_flag='N' and plan_type='A' ";
                dc.setTheSql(sql);
                string planseq1 = dc.GetTable().Rows[0][0].ToString();
                if (dc.GetTable().Rows.Count == 0 || planseq1 == "")
                {
                    e.Result = "Fail1,当前已最大序！";
                    return;
                }
                if (itemflag1 == "R")
                {
                    e.Result = "Fail1,该计划已库房接收！";
                    return;
                }
                sql = "update data_plan set plan_seq='" + planseq + "' where plan_seq='" + planseq1 + "' and  begin_date=to_date('" + begindate + "','yyyy-mm-dd') and item_flag='N'  and pline_code='" + plinecode1 + "' and plan_type='A' ";
                dc.ExeSql(sql);
                sql = "update data_plan set plan_seq='" + planseq1 + "' where rmes_id='" + rmesid + "' ";
                dc.ExeSql(sql);
                e.Result = "OK1,";
                return;
            }
            else
            {
                DateTime beginDate = ASPxBeginDate.Date;
                DateTime endDate = ASPxEndDate.Date;
                string pline = ASPxComboBoxPline.SelectedItem.Value.ToString();
                string rountingsite = ASPxComboBoxProcess.SelectedItem.Value.ToString();
                string plineName = ASPxComboBoxPline.SelectedItem.Text;
                string process = ASPxComboBoxProcess.SelectedItem.Value.ToString();
                string series = ASPxTextSeries.Text;
                string so = ASPxTextBoxSO.Text;
                string planCode = ASPxTextPlanCode.Text;
                string seq = ASPxTextBoxSeq.Text;
                string remark = aspxmemoRemark.Text;
                string customerName = ASPxTextBoxCustomerName.Text;
                string qty = ASPxTextBoxQty.Text;
                string gylx1;
                string accountdate1;
                string ROUNTING_CODE1 = "";
                try
                {
                    ROUNTING_CODE1 = ASPxComboBoxAcross.Value.ToString(); //工艺路线代码，默认为
                }
                catch
                { }
                string isSeq = dc.GetValue("select count(1) from data_plan where plan_seq=" + seq + " and pline_code='" + pline + "' and begin_date=to_date('" + beginDate.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') and plan_type='A' ");
                if (isSeq != "0")
                {
                    e.Result = "Fail,计划序重复！";
                    return;
                }
                //调用函数判断计划代码是否符合规范
                string sqlPlan = "select func_check_plan('A','" + planCode + "','" + pline + "','" + rountingsite + "','" + so + "') from dual";
                dc.setTheSql(sqlPlan);
                string getvalue1 = dc.GetValue();
                char[] charSeparators1 = new char[] { '#' };
                string[] collection1 = getvalue1.Split(charSeparators1);

                if (collection1[0] != "OK")
                {
                    e.Result = "Fail," + collection1[1];
                    return;
                }
                else
                {
                    accountdate1 = collection1[1];
                    gylx1 = collection1[2];
                }
                userManager theUserManager = (userManager)Session["theUserManager"];
                try
                {
                    PlanFactory.PL_CREATE_PLAN("ADD", theCompanyCode, pline, plineName, seq, planCode, so, series, "A", theUserId, theUserManager.getUserName(), beginDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"), accountdate1, qty, "0", "0", rountingsite, gylx1, customerName, remark, ROUNTING_CODE1, "Y");
                    //dc.ExeSql("call PL_CREATE_PLAN('ADD','" + theCompanyCode + "','" + pline + "','" + plineName + "','" + seq + "','" + planCode + "','" + so + "','" + series + "','A','" + theUserId + "','" + theUserManager.getUserName() + "',to_date('" + beginDate.ToString("yyyy-MM-dd") + "','yyyy-MM-dd'),to_date('" + endDate.ToString("yyyy-MM-dd") + "','yyyy-MM-dd')),to_date('" + accountdate1 + "','yyyy-MM-dd')),'" + qty + "','0','0','" + rountingsite + "','" + gylx1 + "','" + customerName + "','" + remark + "','" + ROUNTING_CODE1 + "'");
                    e.Result = "OK,计划提交成功！";
                    return;
                }
                catch (Exception ex)
                {
                    e.Result = "Fail,计划生成失败！";
                    return;
                }
            }


            //string[] column = new string[]
            //{
            //    "RMES_ID",
            //    "COMPANY_CODE",
            //    "PLINE_CODE",
            //    "PLINE_NAME",
            //    "PLAN_SEQ",
            //    "PLAN_CODE",
            //    "PLAN_SO",
            //    "PRODUCT_MODEL",
            //    "CREATE_USERID",
            //    "CREATE_USERNAME",
            //    "BEGIN_DATE",
            //    "END_DATE",
            //    "PLAN_QTY",
            //    "CUSTOMER_NAME"
            //};

            //string[] value = new string[]
            //{
            //    "seq_rmes_id.nextval",
            //    "'" + theCompanyCode + "'",
            //    "'" + pline + "'",
            //    "'" + plineName + "'",
            //    "'" + seq + "'",
            //    "'" + planCode + "'",
            //    "'" + so + "'",
            //    "'" + series + "'",
            //    "'" + theUserId + "'",
            //    "'" + theUserManager.getUserName() + "'",
            //    "to_date('" + beginDate.ToString("yyyy-MM-dd") +"','yyyy-MM-dd')",
            //     "to_date('" + endDate.ToString("yyyy-MM-dd") +"','yyyy-MM-dd')",
            //    "'" + qty + "'",
            //    "'" + customerName + "'",
            //};

            //string _Column = string.Join(",", column);
            //string _Values = string.Join(",", value);

            //string sql = string.Format("insert into data_plan({0}) values({1})", _Column, _Values);
            //dc.ExeSql(sql);

            //if (pline == "E")
            //{
            //    if (!planCode.StartsWith("E"))
            //    {
            //        e.Result = "Fail,计划代码格式错误，格式：E20160101-1！";
            //    }
            //}
            //else
            //    if (pline == "W")
            //    { 
            //        if(planCode.Substring(5,1)!="P")
            //        {
            //            e.Result = "Fail,计划代码格式错误，格式：2016P0101-1！";
            //        }
            //    }
            //    else if (pline == "R")
            //        {
            //            if (rountingsite == "E")
            //            {
            //                if (!planCode.StartsWith("RB"))
            //                { }
            //            }
            //            else
            //                if (rountingsite == "W")
            //                {
            //                    if (!planCode.StartsWith("RC"))
            //                    { }
            //                }
            //                else
            //                    if (rountingsite == "R")
            //                    {
            //                        if (!planCode.StartsWith("RM"))
            //                        { }
            //                    }
            //    }
            //        else if (pline == "Z")
            //        {
            //            if (!planCode.StartsWith("Z"))
            //            {
            //                e.Result = "Fail,计划代码格式错误，格式：Z20160101-1！";
            //            }
            //        }

            //string[] column = new string[]
            //{
            //    "RMES_ID",
            //    "COMPANY_CODE",
            //    "PLINE_CODE",
            //    "PLINE_NAME",
            //    "PLAN_SEQ",
            //    "PLAN_CODE",
            //    "PLAN_SO",
            //    "PRODUCT_MODEL",
            //    "CREATE_USERID",
            //    "CREATE_USERNAME",
            //    "BEGIN_DATE",
            //    "END_DATE",
            //    "PLAN_QTY",
            //    "CUSTOMER_NAME"
            //};

            //string[] value = new string[]
            //{
            //    "seq_rmes_id.nextval",
            //    "'" + theCompanyCode + "'",
            //    "'" + pline + "'",
            //    "'" + plineName + "'",
            //    "'" + seq + "'",
            //    "'" + planCode + "'",
            //    "'" + so + "'",
            //    "'" + series + "'",
            //    "'" + theUserId + "'",
            //    "'" + theUserManager.getUserName() + "'",
            //    "to_date('" + beginDate.ToString("yyyy-MM-dd") +"','yyyy-MM-dd')",
            //     "to_date('" + endDate.ToString("yyyy-MM-dd") +"','yyyy-MM-dd')",
            //    "'" + qty + "'",
            //    "'" + customerName + "'",
            //};

            //string _Column = string.Join(",", column);
            //string _Values = string.Join(",", value);

            //string sql = string.Format("insert into data_plan({0}) values({1})", _Column, _Values);
            //try
            //{
            //    dc.ExeSql(sql);
            //    e.Result = "OK,计划提交成功！";
            //}
            //catch (Exception)
            //{
            //    e.Result = "Fail,计划生成失败！";
            //    return;
            //}

            //    string alertFlag = (flag == "true") ? "Y" : "N";

            //    string sql;

            //    DataTable dt = dc.GetTable("select pline_plan_code,PLINE_TYPE_CODE from code_product_line where rmes_id='" + pline + "'");

            //    string subTitle = dt.Rows[0][0].ToString();
            //    string plineType = dt.Rows[0][1].ToString();
            //    if (ASPxComboBoxTeam.Text == "")
            //    {
            //        e.Result = "请选择班组！";
            //        return;
            //    }
            //    dt = dc.GetTable("select team_code from code_team where team_name='" + ASPxComboBoxTeam.Text + "'");
            //    string teamCode = dt.Rows[0][0].ToString();
            //    string limFlag = "N";
            //    if (ASPxCheckBox1.Checked == true)
            //    {
            //        limFlag = "Y";
            //    }
            //    //如果总装，不保留填写信息
            //    if (plineType == "B")
            //    {
            //        if (item == "")
            //        {
            //            e.Result = "Fail,分装线零件必填！";
            //            return;
            //        }
            //        else
            //        {
            //            sql = "select * from data_zd_bom t where t.item_code='" + item + "'";
            //            if (dc.GetTable(sql).Rows.Count == 0)
            //            {
            //                e.Result = "Fail,分装线零件代号不存在！";
            //                return;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        item = "";
            //    }

            //    //string rmesID = dc.GetValue("select seq_rmes_id.nextval from dual");
            //    string planID = dc.GetValue("select SEQ_PLAN_CODE.nextval from dual");

            //    //计划需要写的列
            //    string[] planColumn = new string[] 
            //{
            //    "RMES_ID",
            //    "CREATE_USER_ID",
            //    "COMPANY_CODE",
            //    "PLINE_CODE",
            //    "SHIFT_CODE",
            //    "PLAN_CODE",
            //    "PLAN_SEQUENCE",
            //    "PRODUCT_SERIES",
            //    "PROJECT_CODE",
            //    "ITEM_CODE",
            //    "BEGIN_DATE",
            //    "PLAN_QTY",
            //    "BOM_INDEX",
            //    "PRODUCT_CODE",
            //    "REMARK",
            //    "STOCK_ALERT_FLAG",
            //    "TEAM_CODE",
            //    "LIM_FLAG"
            //};

            //    string planCode = subTitle + string.Format("{0:yyyyMMdd}", DateTime.Today) + planID;

            //    string[] planValues = new string[]
            //{
            //    "seq_rmes_id.nextval",
            //    "'" + theUserId + "'",
            //    "'" + theCompanyCode + "'",   
            //    "'" + pline + "'",
            //    "'" + shift + "'",
            //    "'" + planCode + "'",
            //    seq,
            //    "'" + series + "'",
            //    "'" + project + "'",
            //    "'" + item + "'",
            //    "to_date('" + beginDate.ToShortDateString() +"','yyyy-MM-dd')",
            //    qty,
            //    "'" + bom + "'",
            //    "'" + tcm + "'",
            //    "'" + remark + "'",
            //    "'" + alertFlag + "'",
            //    "'" + teamCode + "'",
            //    "'" + limFlag + "'"
            //};

            //    string _planColumn = string.Join(",", planColumn);
            //    string _planValues = string.Join(",", planValues);

            //    sql = string.Format("insert into data_plan({0}) values({1})", _planColumn, _planValues);

            //    try
            //    {
            //        int result = dc.ExeSql(sql);

            //        dt = dc.GetTable("select a.station_code,t.* from data_zd_bom t left join rel_station_location a on a.location_code=t.location_code where t.bom_index='" + bom + "'");
            //        if (dt.Rows.Count > 0)
            //        {
            //            for (int i = 0; i < dt.Rows.Count; i++)
            //            {
            //                string itemCode = dt.Rows[i]["ITEM_CODE"].ToString();
            //                string itemName = dt.Rows[i]["ITEM_NAME"].ToString();
            //                string itemQty = dt.Rows[i]["ITEM_QTY"].ToString();
            //                string itemRank = dt.Rows[i]["ITEM_RANK"].ToString();
            //                //string locationCode = dt.Rows[i]["LOCATION_CODE"].ToString();
            //                string stationCode = dt.Rows[i]["STATION_CODE"].ToString();

            //                string[] bomColumn = new string[] 
            //            {
            //                "RMES_ID",
            //                "PLAN_CODE",
            //                "ITEM_CODE",
            //                "ITEM_NAME",
            //                "ITEM_QTY",
            //                "ITEM_RANK",
            //                "REQUEST_DATE",
            //                "LOCATION_CODE"
            //            };

            //                string[] bomValues = new string[]
            //            {
            //                "seq_rmes_id.nextval",
            //                "'" + planCode + "'",
            //                "'" + itemCode + "'",   
            //                "'" + itemName + "'",
            //                itemQty,
            //                "'" + itemRank + "'",
            //                "to_date('" + beginDate.ToShortDateString() +"','yyyy-MM-dd')",
            //                "'" + stationCode + "'"  //"'" + locationCode + "'"
            //            };

            //                string _bomColumn = string.Join(",", bomColumn);
            //                string _bomValues = string.Join(",", bomValues);

            //                sql = string.Format("insert into data_plan_bom({0}) values({1})", _bomColumn, _bomValues);
            //                dc.ExeSql(sql);

            //            }


            //        }

            //        e.Result = "OK,计划提交成功！";
            //    }
            //    catch (Exception)
            //    {
            //        e.Result = "Fail,计划生成失败！";
            //    }
        }

        protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            
            //多选删除，判断是否可以删除
            ASPxGridView atl1 = (ASPxGridView)sender;
            int count1 = atl1.Selection.Count;
            List<object> aa = atl1.GetSelectedFieldValues("RMES_ID");
            List<object> flag1 = atl1.GetSelectedFieldValues("CONFIRM_FLAG");
            List<object> flag2 = atl1.GetSelectedFieldValues("RUN_FLAG");
            List<object> flag3 = atl1.GetSelectedFieldValues("ITEM_FLAG");
            List<object> flag4 = atl1.GetSelectedFieldValues("THIRD_FLAG");

            List<object> plancode1 = atl1.GetSelectedFieldValues("PLAN_CODE");
            List<object> planso1 = atl1.GetSelectedFieldValues("PLAN_SO");
            List<object> plinecode1 = atl1.GetSelectedFieldValues("PLINE_CODE");
            List<object> planseq1 = atl1.GetSelectedFieldValues("PLAN_SEQ");
            List<object> rountingsite1 = atl1.GetSelectedFieldValues("ROUNTING_SITE");
            List<object> planqty1 = atl1.GetSelectedFieldValues("PLAN_QTY");
            for (int i = 0; i < aa.Count; i++)
            {
                try
                {
                    if (flag1[i].ToString() == "N" && flag2[i].ToString() == "N" && flag3[i].ToString() == "N" && flag4[i].ToString() == "N")
                    {
                        string plancode = plancode1[i].ToString();
                        string planso = planso1[i].ToString();
                        string PlineCode = plinecode1[i].ToString();
                        string Planseq = planseq1[i].ToString();
                        string Rsite = rountingsite1[i].ToString();
                        string qty = planqty1[i].ToString();

                        //string plancode = e.Values["PLAN_CODE"].ToString();
                        //string planso = e.Values["PLAN_SO"].ToString();
                        //string PlineCode = e.Values["PLINE_CODE"].ToString();
                        //string Planseq = e.Values["PLAN_SEQ"].ToString();
                        //string Rsite = e.Values["ROUNTING_SITE"].ToString();
                        //string qty = e.Values["PLAN_QTY"].ToString();
                        userManager theUserManager = (userManager)Session["theUserManager"];

                        //PlanFactory.PL_CREATE_PLAN("DELETE", theCompanyCode, PlineCode, "", "", plancode, planso, "", "A", "", "", "", "", "", "", "0", "0", "", "", "", "", "", "Y");
                        PlanFactory.PL_CREATE_PLAN("DELETE", theCompanyCode, PlineCode, "", Planseq, plancode, planso, "", "A", theUserId, theUserManager.getUserName(), "", "", "", qty, "0", "0", Rsite, "", "", "", "", "Y");
                        //判断是否存在分装计划
                        string sql = "select count(1) from data_plan where plan_code like 'F%" + plancode + "' and plan_type='F'";
                        if (dc.GetValue(sql) != "0")
                        {
                            string sql1 = "select count(1) from data_product where plan_code like 'F%" + plancode + "'";
                            if (dc.GetValue(sql1) == "0")
                            {
                                //未上线删除分装计划
                                //PlanFactory.PL_CREATE_PLAN("DELETE", theCompanyCode, PlineCode, "", Planseq, plancode, planso, "", "F", theUserId, theUserManager.getUserName(), "", "", "", qty, "0", "0", Rsite, "", "", "", "", "Y");
                                sql1 = "INSERT INTO data_plan_LOG(RMES_ID,COMPANY_CODE,PLINE_CODE,PLINE_NAME,PLAN_SEQ,PLAN_CODE,PLAN_SO,PRODUCT_MODEL,PLAN_TYPE,CREATE_USERID,CREATE_USERNAME,CREATE_TIME,BEGIN_DATE,END_DATE,ACCOUNT_DATE,PLAN_QTY,ONLINE_QTY,OFFLINE_QTY,ROUNTING_SITE,ROUNTING_REMARK,CUSTOMER_CODE,CUSTOMER_NAME,ORDER_CODE,REMARK,ROUNTING_CODE,LQ_FLAG,CONFIRM_FLAG,RUN_FLAG,BOM_FLAG,ITEM_FLAG,SN_FLAG,THIRD_FLAG,STOCK_FLAG,THIRD_RECEIVE_FLAG,STOCK_RECEIVE_FLAG,modify_user,modify_time,modify_type,is_bom) "
                                   + " SELECT RMES_ID,COMPANY_CODE,PLINE_CODE,PLINE_NAME,PLAN_SEQ,PLAN_CODE,PLAN_SO,PRODUCT_MODEL,PLAN_TYPE,CREATE_USERID,CREATE_USERNAME,CREATE_TIME,BEGIN_DATE,END_DATE,ACCOUNT_DATE,PLAN_QTY,ONLINE_QTY,OFFLINE_QTY,ROUNTING_SITE,ROUNTING_REMARK,CUSTOMER_CODE,CUSTOMER_NAME,ORDER_CODE,REMARK,ROUNTING_CODE,LQ_FLAG,CONFIRM_FLAG,RUN_FLAG,BOM_FLAG,ITEM_FLAG,SN_FLAG,THIRD_FLAG,STOCK_FLAG,THIRD_RECEIVE_FLAG,STOCK_RECEIVE_FLAG,'" + theUserManager.getUserName() + "',SYSDATE,'删除记录','Y' "
                                   + " FROM data_plan WHERE  TRIM(plan_code)like 'F%" + plancode + "' and plan_type='F'";
                                dc.ExeSql(sql1);
                                sql1 = "  INSERT INTO DATA_PLANLOG(plan_so,plan_code,create_date,remark,modify_type,plan_seq,pline_code,rounting_site) "
                                   + " select plan_so,plan_code,sysdate, '删除计划','删除',to_number(plan_seq),pline_code,rounting_site from data_plan "
                                   + " where plan_code like 'F%" + plancode + "' and plan_type='F'  ";
                                dc.ExeSql(sql1);
                                sql1 = "delete from data_plan_standard_bom where plan_code like 'F%" + plancode + "' ";
                                dc.ExeSql(sql1);
                                sql1 = "delete from data_plan where plan_code like 'F%" + plancode + "' and plan_type='F' ";
                                dc.ExeSql(sql1);
                                dc.ExeSql("delete from ATPUJHB_FROMMAIN where JHMC='" + plancode + "'");
                            }
                        }
                    }
                }
                catch
                { }
            }
            e.Cancel = true;
            setCondition();
        }

        public void ASPxGridView1_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            ASPxGridView grid = (ASPxGridView)sender;
            string run_flag = grid.GetRowValues(e.VisibleIndex, "RUN_FLAG") as string;
            string item_flag = grid.GetRowValues(e.VisibleIndex, "ITEM_FLAG") as string;
            string third_flag = grid.GetRowValues(e.VisibleIndex, "THIRD_FLAG") as string;
            string confirm_flag = grid.GetRowValues(e.VisibleIndex, "CONFIRM_FLAG") as string;
            string sn_flag = grid.GetRowValues(e.VisibleIndex, "SN_FLAG") as string;
            switch (e.ButtonID)
            {
                case "Confirm":
                    if (e.CellType == GridViewTableCommandCellType.Filter) break;
                    if (confirm_flag == "Y")
                    {
                        e.Enabled = false;
                        e.Visible = DefaultBoolean.False;
                    }
                    else
                    {
                        e.Enabled = true;
                        e.Visible = DefaultBoolean.True;
                        if (sn_flag == "N" || item_flag != "N")
                        {
                            e.Enabled = false;
                        }
                    }
                    break;

                case "Cancel":
                    //if (item_flag == "Y" && confirm_flag == "Y" && run_flag == "Y")
                    if (e.CellType == GridViewTableCommandCellType.Filter) break;
                    if (confirm_flag == "Y")
                    {
                        e.Enabled = true;
                        e.Visible = DefaultBoolean.True;
                        if (item_flag != "N")
                        {
                            e.Enabled = false;
                        }
                    }
                    else
                    {
                        e.Enabled = false;
                        e.Visible = DefaultBoolean.False;
                    }
                    if (run_flag != "N" || item_flag != "N" || third_flag != "N") //已库房确认的不能修改删除 已参与三方要料的不能修改删除 计划确认或取消或暂停的不能修改
                    {
                        e.Enabled = false;
                    }
                    break;
            }
        }

        protected void ASPxGridView1_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            ASPxGridView grid = (ASPxGridView)sender;
            string run_flag = grid.GetRowValues(e.VisibleIndex, "RUN_FLAG") as string;
            string ITEM_flag = grid.GetRowValues(e.VisibleIndex, "ITEM_FLAG") as string;
            string third_flag = grid.GetRowValues(e.VisibleIndex, "THIRD_FLAG") as string;
            string confirm_flag = grid.GetRowValues(e.VisibleIndex, "CONFIRM_FLAG") as string;

            if (confirm_flag == "N")
                e.Enabled = true;
            else
                e.Enabled = false;
            if (run_flag != "N")//已生产确认的不能修改删除
                e.Enabled = false;
            if (ITEM_flag != "N") //已库房确认的不能修改删除
                e.Enabled = false;
            if (third_flag != "N")//已参与三方要料的不能修改删除
                e.Enabled = false;

            if (e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
                e.Enabled = true;

        }


        protected void ASPxButton_Import_Click(object sender, EventArgs e)
        {
            try
            {
                ASPxButton_Import.Enabled = false;

                string path = File1.Value;//上传文件路径

                if (path == "")
                {
                    ASPxButton_Import.Enabled = true;
                    return;
                }

                string uploadPath = Server.MapPath(Request.ApplicationPath); //上传目录及文件名
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

                if (!Directory.Exists("C:\\inetpub\\wwwroot\\webapp\\Rmes\\File\\excel"))
                {
                    Directory.CreateDirectory("C:\\inetpub\\wwwroot\\webapp\\Rmes\\File\\excel");
                }
                uploadPath = uploadPath + "\\Rmes\\File\\excel\\" + fileName; //得到上传目录及文件名称  
                File1.PostedFile.SaveAs(uploadPath);       //上传文件到服务器  

                DataTable dt = GetExcelDataTable(uploadPath);

                //ASPxGridView1.DataSource = dt;
                //ASPxGridView1.DataBind();

                //string[] orderColumn = new string[] 
                //{
                //    "RMES_ID"         ,
                //    "COMPANY_CODE"    ,
                //    "ORDER_CODE"      ,
                //    "CUSTOMER_CODE"   ,
                //    "CUSTOMER_NAME"   ,
                //    "PRODUCT_CODE"    ,
                //    "PRODUCT_NAME"    ,
                //    "PRODUCT_MODEL"   ,
                //    "PRODUCT_SERIES"  ,
                //    "ORDER_QTY"       ,
                //    "DELIVERY_DATE"   ,
                //    "CREATE_USERID"   ,
                //    "CREATE_USERNAME" ,
                //    "CREATE_TIME"     ,
                //    "REMARK"          
                //};

                int count = 0;
                foreach (DataRow t in dt.Rows)
                {
                    if (t.IsNull(0)) continue;
                    string planCode = t[0].ToString().Trim();
                    string planSo = t[1].ToString().Trim();
                    string planCus = t[2].ToString().Trim();
                    string planQty = t[3].ToString().Trim();
                    
                    string pline = t[4].ToString().Trim();
                    string rountingsite = t[5].ToString().Trim();
                    string remark = t[6].ToString().Trim();
                    if (planCode == "" || planSo == "" || planQty == "" || planCus == "" || pline == "" || rountingsite=="")
                    {
                        Show(this, planCode + ":" + "缺少计划参数");
                        ASPxButton_Import.Enabled = true;
                        return;
                    }
                    try
                    {
                        int qty = Convert.ToInt32(planQty);
                    }
                    catch
                    {
                        Show(this, planCode + ":" + "计划数量不合法");
                        ASPxButton_Import.Enabled = true;
                        return;
                    }
                    string sqlpp = "select count(1) from vw_user_role_program where pline_code='" + pline + "' and program_code='" + theProgramCode + "' and user_id='" + theUserId + "' and company_code='" + theCompanyCode + "'";
                    if (dc.GetValue(sqlpp) == "0")
                    {
                        Show(this, planCode + ":" + "无该生产线权限");
                        ASPxButton_Import.Enabled = true;
                        return;
                    }
                    //调用函数判断计划代码是否符合规范
                    string sqlPlan = "select func_check_plan('A','" + planCode + "','" + pline + "','" + rountingsite + "','" + planSo + "') from dual";
                    dc.setTheSql(sqlPlan);
                    string getvalue1 = dc.GetValue();
                    char[] charSeparators1 = new char[] { '#' };
                    string[] collection1 = getvalue1.Split(charSeparators1);

                    if (collection1[0] != "OK")
                    {
                        Show(this, planCode + ":" + collection1[1]);
                        //ASPxCallbackPanel6.JSProperties.Add("cpCallbackName", "Fail");
                        //ASPxCallbackPanel6.JSProperties.Add("cpCallbackRet", collection1[1]);
                        ASPxButton_Import.Enabled = true;
                        return;
                    }
                }
                foreach (DataRow t in dt.Rows)
                {
                    try
                    {
                        string planCode = t[0].ToString().Trim();
                        string planSo = t[1].ToString().Trim();
                        string planQty = t[3].ToString().Trim();
                        string planCus = t[2].ToString().Trim();
                        string pline = t[4].ToString().Trim();
                        string rountingsite = t[5].ToString().Trim();
                        string remark = t[6].ToString().Trim();
                        string accountdate1;
                        string gylx1;
                        string ROUNTING_CODE1 = "";
                        string plineName = "", seq = "", series = "";
                        //调用函数判断计划代码是否符合规范
                        string sqlPlan = "select func_check_plan('A','" + planCode + "','" + pline + "','" + rountingsite + "','" + planSo + "') from dual";
                        dc.setTheSql(sqlPlan);
                        string getvalue1 = dc.GetValue();
                        char[] charSeparators1 = new char[] { '#' };
                        string[] collection1 = getvalue1.Split(charSeparators1);

                        if (collection1[0] != "OK")
                        {
                            Show(this, planCode + ":" + collection1[1]);
                            //ASPxCallbackPanel6.JSProperties.Add("cpCallbackName", "Fail");
                            //ASPxCallbackPanel6.JSProperties.Add("cpCallbackRet", collection1[1]);
                            ASPxButton_Import.Enabled = true;
                            return;
                        }
                        else
                        {
                            accountdate1 = collection1[1];
                            gylx1 = collection1[2];
                        }
                        string sql = "select pline_name from code_product_line where pline_code='" + pline + "'";
                        dc.setTheSql(sql);
                        plineName = dc.GetValue();

                        sql = "select nvl(max(plan_seq+1),'1') from data_plan where pline_code='" + pline + "' and plan_type='A' and begin_date=to_date('" + ASPxDateEdit1.Date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') ";
                        dc.setTheSql(sql);
                        seq = dc.GetValue();

                        sql = "select jx from copy_engine_property where SO='" + planSo + "' and rownum=1";
                        dc.setTheSql(sql);
                        series = dc.GetTable().Rows[0][0].ToString();
                        if (series.EndsWith("ZZ"))
                        {
                            series = series.Substring(0, series.Length - 2);
                        }

                        userManager theUserManager = (userManager)Session["theUserManager"];
                        try
                        {
                            PlanFactory.PL_CREATE_PLAN("ADD", theCompanyCode, pline, plineName, seq, planCode, planSo, series, "A", theUserId, theUserManager.getUserName(), ASPxDateEdit1.Date.ToString("yyyy-MM-dd"), ASPxDateEdit1.Date.ToString("yyyy-MM-dd"), accountdate1, planQty, "0", "0", rountingsite, gylx1, planCus, remark, ROUNTING_CODE1, "Y");
                            //ASPxButton_Import.Enabled = true;
                            //return;
                        }
                        catch (Exception ex)
                        {
                            Show(this, ex.Message);
                            //ASPxCallbackPanel6.JSProperties.Add("cpCallbackName", "Fail");
                            //ASPxCallbackPanel6.JSProperties.Add("cpCallbackRet", ex.Message);
                            ASPxButton_Import.Enabled = true;
                            return;
                        }
                    }
                    catch (Exception ex1)
                    {
                        Show(this, ex1.Message);
                        //ASPxCallbackPanel6.JSProperties.Add("cpCallbackName", "Fail");
                        //ASPxCallbackPanel6.JSProperties.Add("cpCallbackRet", ex1.Message);
                        ASPxButton_Import.Enabled = true;
                        return;
                    }
                    //count += 1;
                }

                ASPxButton_Import.Enabled = true;
                setCondition();
                return;
            }
            catch (Exception ex2)
            {
                ASPxCallbackPanel6.JSProperties.Add("cpCallbackName", "Fail");
                ASPxCallbackPanel6.JSProperties.Add("cpCallbackRet", ex2.Message);
                ASPxButton_Import.Enabled = true;
            }
        }

        public static void Show(System.Web.UI.Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + msg.ToString() + "');</script>");
        }


        ///<summary>
        ///根据excel路径和sheet名称，返回excel的DataTable
        ///</summary>
        public static DataTable GetExcelDataTable(string path)
        {
            /*Office 2007*/
            string ace = "Microsoft.ACE.OLEDB.12.0";
            /*Office 97 - 2003*/
            string jet = "Microsoft.Jet.OLEDB.4.0";
            string xl2007 = "Excel 12.0 Xml";
            string xl2003 = "Excel 8.0";
            string imex = "IMEX=1";
            /* csv */
            string text = "text";
            string fmt = "FMT=Delimited";
            string hdr = "Yes";
            string conn = "Provider={0};Data Source={1};Extended Properties=\"{2};HDR={3};{4}\";";

            //string select = sql;
            string ext = Path.GetExtension(path);
            OleDbDataAdapter oda;
            DataTable dt = new DataTable("data");
            switch (ext.ToLower())
            {
                case ".xlsx":
                    conn = String.Format(conn, ace, Path.GetFullPath(path), xl2007, hdr, imex);
                    break;
                case ".xls":
                    conn = String.Format(conn, jet, Path.GetFullPath(path), xl2003, hdr, imex);
                    break;
                case ".csv":
                    conn = String.Format(conn, jet, Path.GetDirectoryName(path), text, hdr, fmt);
                    //sheet = Path.GetFileName(path);
                    break;
                default:
                    throw new Exception("File Not Supported!");
            }
            OleDbConnection con = new OleDbConnection(conn);
            con.Open();
            //select = string.Format(select, sql);

            DataTable dtbl1 = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            //dataGridView2.DataSource = dtbl1;   
            string strSheetName = dtbl1.Rows[0][2].ToString().Trim();

            string select = string.Format("SELECT * FROM [{0}]", strSheetName);

            oda = new OleDbDataAdapter(select, con);
            oda.Fill(dt);
            con.Close();
            return dt;
        }

        protected void btnXlsExport_Click(object sender, EventArgs e)
        {
            //DataTable dt = (DataTable)ASPxGridViewExporter1.GridView.DataSource;
            //SpreadsheetClass xls = new SpreadsheetClass();
            ////读取datatable的列名
            //for (int i = 0; i < dt.Columns.Count; i++)
            //{
            //    //设置第一行的每一列为datatable中的每个列名
            //    xls.ActiveSheet.Cells[1, i + 1] = dt.Columns[i].Caption.ToString();
            //}
            ////将datatable中每个单元格的数据放到excel表单中
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    for (int j = 0; j < dt.Columns.Count; j++)
            //    {
            //        xls.ActiveSheet.Cells[i + 2, j + 1] = dt.Rows[i][j].ToString();
            //    }
            //}
            ////导出路径设置
            //string path = Server.MapPath("~") + "/" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls";
            ////导出到excel
            //xls.Export(path, SheetExportActionEnum.ssExportActionNone, SheetExportFormat.ssExportXMLSpreadsheet);
            
            
            //ASPxGridView1.Columns.RemoveAt(27);
            //ASPxGridView1.Columns.RemoveAt(28);
            //ASPxGridView1.Columns.RemoveAt(29);
            //ASPxGridView1.Columns.RemoveAt(30);
            ASPxGridView1.Columns.RemoveAt(26);
            ASPxGridView1.Columns.RemoveAt(25);
            ASPxGridView1.Columns.RemoveAt(24);
            ASPxGridView1.Columns.RemoveAt(23);
            ASPxGridView1.Columns.RemoveAt(22);
            ASPxGridView1.Columns.RemoveAt(21);
            ASPxGridView1.Columns.RemoveAt(20);
            ASPxGridView1.Columns.RemoveAt(19);
            ASPxGridView1.Columns.RemoveAt(18);
            ASPxGridView1.Columns.RemoveAt(17);
            ASPxGridView1.Columns.RemoveAt(16);
            ASPxGridView1.Columns.RemoveAt(15);
            ASPxGridView1.Columns.RemoveAt(14);
            ASPxGridView1.Columns.RemoveAt(13);
            ASPxGridView1.Columns.RemoveAt(12);

            GridViewDataTextColumn d3 = new GridViewDataTextColumn();
            d3.Caption = "客户";
            d3.VisibleIndex = 7;
            d3.FieldName = "CUSTOMER_NAME";
            ASPxGridView1.Columns.Add(d3);

            GridViewDataTextColumn d2 = new GridViewDataTextColumn();
            d2.Caption = "入库日期";
            d2.VisibleIndex = 16;
            d2.FieldName = "ACCOUNT_DATE";
            ASPxGridView1.Columns.Add(d2);
            GridViewDataTextColumn d1 = new GridViewDataTextColumn();
            d1.Caption = "备注";
            d1.VisibleIndex = 17;
            d1.FieldName = "REMARK";
            ASPxGridView1.Columns.Add(d1);

            ASPxGridViewExporter1.WriteXlsToResponse("导出计划");

            //SaveFile(actionToCall => ASPxGridViewExporter1.WriteXls(actionToCall), "xls");

        }
        protected string DocumentsFolder = "~/Rmes/File";
        protected string DocumentFileName = DateTime.Now.ToString("yyyyMMddHH:mm:ss");
        protected void SaveFile(Action<Stream> action, string format)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                action(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);
                using (FileStream fileStream = new FileStream(MapPath(
                        string.Format("{0}/{1}.{2}",
                        DocumentsFolder,
                        DocumentFileName,
                        format)
                    ),
                    FileMode.Create,
                    FileAccess.Write))
                {
                    memoryStream.WriteTo(fileStream);
                }
            }
        }
        protected void ASPxGridView1_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.VisibleIndex < 0) return;
            string status = e.GetValue("REMARK").ToString(); //已确认绿色
            if (status.Contains("预留号"))
            {
                e.Row.BackColor = System.Drawing.Color.LightPink;
            }
            status = e.GetValue("SN_FLAG").ToString(); //已生成流水号黄色
            switch (status)
            {
                case "Y":
                    e.Row.BackColor = System.Drawing.Color.Yellow;
                    break;
            }
            status = e.GetValue("CONFIRM_FLAG").ToString(); //已确认绿色
            switch (status)
            {
                case "Y":
                    e.Row.BackColor = System.Drawing.Color.LimeGreen;
                    break;
            }
            status = e.GetValue("BOM_FLAG").ToString();//已生成BOM蓝色
            switch (status)
            {
                case "Y":
                    e.Row.BackColor = System.Drawing.Color.LightSkyBlue;
                    break;
            }
            status = e.GetValue("ITEM_FLAG").ToString(); //已库房确认红色
            switch (status)
            {
                case "Y":
                    e.Row.BackColor = System.Drawing.Color.Red;
                    break;
            }
            status = e.GetValue("RUN_FLAG").ToString(); //已库房确认红色
            switch (status)
            {
                case "C":
                    e.Row.BackColor = System.Drawing.Color.Gray;  //已取消的灰色
                    break;
                case "P":
                    e.Row.BackColor = System.Drawing.Color.GreenYellow;//已暂停的黄绿色
                    break;
            }

        }

        protected void ASPxGridView1_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            //string Payment_Type_Temp = (string)e.GetValue("CONFIRM_FLAG");
            //if (Payment_Type_Temp == "Y")
            //{
            //    e.Row.Cells[12].Style.Add("BackgroundColor", "Green");
            //}
        }

        /// <summary>
        /// 根据控件id得到控件
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private Control GetControl(string name)
        {
            object o = this.GetType().GetField(name, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(this);
            return ((Control)o);
        }

        protected void ASPxCallbackPanel6_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                ASPxButton_Import.Enabled = false;
                //string[] aa = Request.Files.AllKeys;
                //string aa = GetControl("File1").GetType().ToString();
                string[] s = e.Parameter.Split('|');
                string flag = s[0];
                string path = flag;//上传文件路径
                //string path = File1.Value;//上传文件路径
                
                if (path == "")
                {
                    ASPxButton_Import.Enabled = true;
                    return;
                }

                string uploadPath = Server.MapPath(Request.ApplicationPath); //上传目录及文件名
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";


                uploadPath = uploadPath + "\\Rmes\\File\\excel\\" + fileName; //得到上传目录及文件名称  
                File1.PostedFile.SaveAs(uploadPath);       //上传文件到服务器  

                DataTable dt = GetExcelDataTable(uploadPath);

                int count = 0;
                foreach (DataRow t in dt.Rows)
                {
                    if (t.IsNull(0)) continue;
                    string planCode = t[0].ToString().Trim();
                    string planSo = t[1].ToString().Trim();
                    string planQty = t[2].ToString().Trim();
                    string planCus = t[3].ToString().Trim();
                    string pline = t[4].ToString().Trim();
                    string rountingsite = t[5].ToString().Trim();
                    string remark = t[6].ToString().Trim();

                    //调用函数判断计划代码是否符合规范
                    string sqlPlan = "select func_check_plan('A','" + planCode + "','" + pline + "','" + rountingsite + "','" + planSo + "') from dual";
                    dc.setTheSql(sqlPlan);
                    string getvalue1 = dc.GetValue();
                    char[] charSeparators1 = new char[] { '#' };
                    string[] collection1 = getvalue1.Split(charSeparators1);

                    if (collection1[0] != "OK")
                    {
                        //Show(this, planCode + ":" + collection1[1]);
                        ASPxCallbackPanel6.JSProperties.Add("cpCallbackName", "Fail");
                        ASPxCallbackPanel6.JSProperties.Add("cpCallbackRet", collection1[1]);
                        ASPxButton_Import.Enabled = true;
                        return;
                    }
                }
                foreach (DataRow t in dt.Rows)
                {
                    try
                    {
                        string planCode = t[0].ToString().Trim();
                        string planSo = t[1].ToString().Trim();
                        string planQty = t[2].ToString().Trim();
                        string planCus = t[3].ToString().Trim();
                        string pline = t[4].ToString().Trim();
                        string rountingsite = t[5].ToString().Trim();
                        string remark = t[6].ToString().Trim();
                        string accountdate1;
                        string gylx1;
                        string ROUNTING_CODE1 = "";
                        string plineName = "", seq = "", series = "";
                        //调用函数判断计划代码是否符合规范
                        string sqlPlan = "select func_check_plan('A','" + planCode + "','" + pline + "','" + rountingsite + "','" + planSo + "') from dual";
                        dc.setTheSql(sqlPlan);
                        string getvalue1 = dc.GetValue();
                        char[] charSeparators1 = new char[] { '#' };
                        string[] collection1 = getvalue1.Split(charSeparators1);

                        if (collection1[0] != "OK")
                        {
                            //Show(this, planCode + ":" + collection1[1]);
                            ASPxCallbackPanel6.JSProperties.Add("cpCallbackName", "Fail");
                            ASPxCallbackPanel6.JSProperties.Add("cpCallbackRet", collection1[1]);
                            ASPxButton_Import.Enabled = true;
                            return;
                        }
                        else
                        {
                            accountdate1 = collection1[1];
                            gylx1 = collection1[2];
                        }
                        string sql = "select pline_name from code_product_line where pline_code='" + pline + "'";
                        dc.setTheSql(sql);
                        plineName = dc.GetValue();

                        sql = "select nvl(max(plan_seq+1),'1') from data_plan where pline_code='" + pline + "' and begin_date=to_date('" + ASPxDateEdit1.Date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') ";
                        dc.setTheSql(sql);
                        seq = dc.GetValue();

                        sql = "select jx from copy_engine_property where SO='" + planSo + "' and rownum=1";
                        dc.setTheSql(sql);
                        series = dc.GetTable().Rows[0][0].ToString();
                        if (series.EndsWith("ZZ"))
                        {
                            series = series.Substring(0, series.Length - 2);
                        }

                        userManager theUserManager = (userManager)Session["theUserManager"];
                        try
                        {
                            PlanFactory.PL_CREATE_PLAN("ADD", theCompanyCode, pline, plineName, seq, planCode, planSo, series, "A", theUserId, theUserManager.getUserName(), ASPxDateEdit1.Date.ToString("yyyy-MM-dd"), ASPxDateEdit1.Date.ToString("yyyy-MM-dd"), accountdate1, planQty, "0", "0", rountingsite, gylx1, planCus, remark, ROUNTING_CODE1, "Y");
                        }
                        catch (Exception ex)
                        {
                            ASPxCallbackPanel6.JSProperties.Add("cpCallbackName", "Fail");
                            ASPxCallbackPanel6.JSProperties.Add("cpCallbackRet", ex.Message);
                            ASPxButton_Import.Enabled = true;
                            return;
                        }
                    }
                    catch (Exception ex1)
                    {
                        ASPxCallbackPanel6.JSProperties.Add("cpCallbackName", "Fail");
                        ASPxCallbackPanel6.JSProperties.Add("cpCallbackRet", ex1.Message);
                        ASPxButton_Import.Enabled = true;
                        return;
                    }
                }

                ASPxButton_Import.Enabled = true;
                return;
            }
            catch (Exception ex2)
            {
                ASPxCallbackPanel6.JSProperties.Add("cpCallbackName", "Fail");
                ASPxCallbackPanel6.JSProperties.Add("cpCallbackRet", ex2.Message);
                ASPxButton_Import.Enabled = true;
            }
        }

        protected void callbackPanel1_Callback(object sender, CallbackEventArgsBase e)
        {
            ASPxMemo1.Text = "";
            string sql = "select plan_code from data_plan where rmes_id='" + e.Parameter + "'";
            string plancode = dc.GetValue(sql);
            sql = "select sn from data_plan_sn where plan_code='"+plancode+"'  ";//and is_valid='Y' 
            DataTable dt = dc.GetTable(sql);
            string txt1 = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                txt1 = txt1 + dt.Rows[i][0].ToString() + (char)13 + (char)10;
            }
            ASPxMemo1.Text = txt1;
        }

        protected void ASPxComboBoxAcross1_Callback(object sender, CallbackEventArgsBase e)
        {
            //string plineSql1 = "select * from (select '' ALINE_CODE,'' ALINE_NAME from dual union select ALINE_CODE,ALINE_NAME from atpu_acrossline) where aline_name like '%" + e.Parameter.ToString().ToUpper() + "%' order by ALINE_CODE desc  ";
            //SqlDataSource2.SelectCommand = plineSql1;
            //SqlDataSource2.DataBind();
        }
    }
}