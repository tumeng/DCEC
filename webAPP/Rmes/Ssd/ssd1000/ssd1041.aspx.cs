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

namespace Rmes.WebApp.Rmes.Ssd
{
    public partial class ssd1041 : System.Web.UI.Page
    {
        private dataConn dc = new dataConn();
        private PubCs thePubCs = new PubCs();

        private static string theCompanyCode, theUserId;
        private string programcode;
        public Database db = DB.GetInstance();

        public string strLocation;

        protected void Page_Load(object sender, EventArgs e)
        {

            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();
            programcode = "ssd1041";
            if (!IsPostBack)
            {


                List<ProductLineEntity> user_plines = ProductLineFactory.GetByUserID(theUserManager.getUserId(), programcode);
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

                //string sql = "select ALINE_CODE,ALINE_NAME from atpu_acrossline";
                //dc.setTheSql(sql);
                //ASPxComboBoxAcross.DataSource = dc.GetTable();
                //ASPxComboBoxAcross.TextField = "ALINE_NAME";
                //ASPxComboBoxAcross.ValueField = "ALINE_CODE";
                //ASPxComboBoxAcross.DataBind();
                //ASPxComboBoxAcross.SelectedIndex = 0;

                string sql = "select * from code_station t left join code_station_type a on t.station_type=a.station_type_code where a.station_type_name like '%拆解%' and t.pline_code in (select pline_code from rel_user_pline where user_id='" + theUserManager.getUserId() + "')  ";
                dc.setTheSql(sql);
                CJZD.DataSource = dc.GetTable();
                CJZD.TextField = "STATION_NAME";
                CJZD.ValueField = "STATION_TYPE";
                CJZD.DataBind();
                CJZD.SelectedIndex = 0;

                ASPxDateEdit1.Date = DateTime.Now;
                ASPxDateEdit2.Date = DateTime.Now.AddDays(5);
                ASPxBeginDate.Date = DateTime.Now;
                ASPxEndDate.Date = DateTime.Now.AddDays(5);


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
            string plineSql = "SELECT a.PLINE_CODE,a.PLINE_NAME FROM CODE_PRODUCT_LINE a left join rel_user_pline b on a.rmes_id=b.PLINE_CODE "
                   + " WHERE a.COMPANY_CODE='" + theCompanyCode + "' AND b.USER_ID='" + theUserId + "' order by a.pline_code";
            SqlDataSource1.SelectCommand = plineSql;
            SqlDataSource1.DataBind();

            string plineSql1 = "SELECT ALINE_CODE,ALINE_NAME FROM atpu_acrossline ";
            SqlDataSource2.SelectCommand = plineSql1;
            SqlDataSource2.DataBind();

            string plineSql2 = "select * from code_station t left join code_station_type a on t.station_type=a.station_type_code where a.station_type_name like '%拆解%' and t.pline_code in (select pline_code from rel_user_pline where user_id='" + theUserManager.getUserId() + "')  ";
            SqlDataSource3.SelectCommand = plineSql2;
            SqlDataSource3.DataBind();

            setCondition();
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
            string planSo = ASPxGridView1.GetSelectedFieldValues("PLAN_SO")[0].ToString();
            string planType = ASPxGridView1.GetSelectedFieldValues("PLAN_TYPE")[0].ToString();
            string ISBOM1 = ASPxGridView1.GetSelectedFieldValues("IS_BOM")[0].ToString();

            switch (flag)
            {
                case "add":
                    if (string.IsNullOrEmpty(value)) return;
                    //判断是否存在上线记录
                    string sql = "select * from data_product where sn='" + value + "' ";
                    dc.setTheSql(sql);
                    if (dc.GetTable().Rows.Count == 0)
                    {
                        ASPxCallbackPanel5.JSProperties.Add("cpCallbackName", "Fail");
                        ASPxCallbackPanel5.JSProperties.Add("cpCallbackRet", "该发动机未在DCEC上线，不能改制/返修！");
                        break;
                    }
                    //判断是否已回冲 生成回冲清单
                    sql = "select * from qad_bkfl where lsh='" + value + "'";
                    dc.setTheSql(sql);
                    if (dc.GetTable().Rows.Count == 0)
                    {
                        sql = "select * from qad_bkflrecord where lsh1='" + value + "'";
                        dc.setTheSql(sql);
                        if (dc.GetTable().Rows.Count == 0)
                        {
                            ASPxCallbackPanel5.JSProperties.Add("cpCallbackName", "Fail");
                            ASPxCallbackPanel5.JSProperties.Add("cpCallbackRet", "该发动机没有生成回冲清单，不能改制/返修！");
                            break;
                        }
                    }

                    //改制计划判断是否改制出库
                    if (planType == "C")
                    {
                        sql = "select * from dp_rckwcb where  rc='出库' and rklx='改制出库' and  ghtm='" + value + "' and gzrq=(select max(gzrq) from dp_rckwcb where ghtm='" + value + "')";
                        dc.setTheSql(sql);
                        if (dc.GetTable().Rows.Count == 0)
                        {
                            ASPxCallbackPanel5.JSProperties.Add("cpCallbackName", "Fail");
                            ASPxCallbackPanel5.JSProperties.Add("cpCallbackRet", "该发动机没有改制出库到ATPU,无法改制！");
                            break;
                        }
                    }
                    //返修计划判断返修SO是否一致
                    if (planType == "D")
                    {
                        sql = "select plan_so from data_product where sn='" + value + "' and rownum=1";
                        dc.setTheSql(sql);
                        if (dc.GetValue() != planSo)
                        {
                            ASPxCallbackPanel5.JSProperties.Add("cpCallbackName", "Fail");
                            ASPxCallbackPanel5.JSProperties.Add("cpCallbackRet", "该发动机原SO与当前SO不一致！");
                            break;
                        }
                    }

                    string insert_sql = "insert into DATA_PLAN_SN select SEQ_RMES_ID.Nextval,'{0}','{1}','{2}','{3}','N',sysdate from dual";
                    insert_sql = string.Format(insert_sql, theCompanyCode, pline, planCode, value);
                    dc.ExeSql(insert_sql);

                    insert_sql = "select count(1) from DATA_PLAN_SN where plan_code='" + planCode + "'";
                    dc.setTheSql(insert_sql);
                    if (dc.GetValue().ToString() == planQty)
                    {
                        string sql1 = "update data_plan set sn_flag='Y' where plan_code='" + planCode + "'";
                        dc.ExeSql(sql1);
                    }

                    break;
                case "del":
                    if (string.IsNullOrEmpty(value)) return;
                    ASPxCallbackPanel5.JSProperties.Clear();
                    string del_pline = ASPxGridView1.GetSelectedFieldValues("PLINE_CODE")[0].ToString();
                    string del_planCode = ASPxGridView1.GetSelectedFieldValues("PLAN_CODE")[0].ToString();

                    //判断是否存在上线记录
                    sql = "select * from DATA_PRODUCT where sn='" + value + "' and plan_code='" + del_planCode + "'  ";
                    dc.setTheSql(sql);
                    if (dc.GetTable().Rows.Count > 0)
                    {
                        ASPxCallbackPanel5.JSProperties.Add("cpCallbackName", "Fail");
                        ASPxCallbackPanel5.JSProperties.Add("cpCallbackRet", "该发动机已上线，不能删除！");
                        break;
                    }

                    string del_sql = "delete from DATA_PLAN_SN where sn = '" + value + "'";
                    dc.ExeSql(del_sql);

                    string update_sql = "select count(1) from DATA_PLAN_SN where plan_code='" + planCode + "'";
                    dc.setTheSql(update_sql);
                    if (dc.GetValue().ToString() != planQty)
                    {
                        string sql1 = "update data_plan set sn_flag='N' where plan_code='" + planCode + "'";
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

            lblYqty.Text = useddt.Rows.Count.ToString();
            YLH.Text = "";
            //YLHsl.Text = "1";

        }

        protected void ASPxCallbackPanel4_Callback(object sender, CallbackEventArgsBase e)
        {
            for (int i = 0; i < ASPxComboBoxProcess.Items.Count; i++)
            {
                if (ASPxComboBoxProcess.Items[i].Value.ToString() == ASPxComboBoxPline.SelectedItem.Value.ToString())
                    ASPxComboBoxProcess.Items[i].Selected = true;
            }
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
                    sql = "select max(plan_seq) from data_plan where begin_date=to_date('" + begindate + "','yyyy-mm-dd') and plan_seq<'" + planseq + "' and pline_code='" + plinecode1 + "'  ";
                    dc.setTheSql(sql);
                    string planseq1 = dc.GetTable().Rows[0][0].ToString();
                    if (dc.GetTable().Rows.Count == 0 || planseq1 == "")
                    {
                        e.Result = "Fail,当前已最小序！";
                        break;
                    }

                    sql = "update data_plan set plan_seq='" + planseq + "' where plan_seq='" + planseq1 + "' and  begin_date=to_date('" + begindate + "','yyyy-mm-dd') and pline_code='" + plinecode1 + "' ";
                    dc.ExeSql(sql);
                    sql = "update data_plan set plan_seq='" + planseq1 + "' where rmes_id='" + rmesid + "' ";
                    dc.ExeSql(sql);
                    //e.Result = "OK,上调成功！";
                    break;
                case "Down":
                    sql = "select min(plan_seq) from data_plan where begin_date=to_date('" + begindate + "','yyyy-mm-dd') and plan_seq>'" + planseq + "' and pline_code='" + plinecode1 + "'  ";
                    dc.setTheSql(sql);
                    planseq1 = dc.GetTable().Rows[0][0].ToString();
                    if (dc.GetTable().Rows.Count == 0 || planseq1 == "")
                    {
                        e.Result = "Fail,当前已最大序！";
                        break;
                    }
                    sql = "update data_plan set plan_seq='" + planseq + "' where plan_seq='" + planseq1 + "' and  begin_date=to_date('" + begindate + "','yyyy-mm-dd') and pline_code='" + plinecode1 + "'  ";
                    dc.ExeSql(sql);
                    sql = "update data_plan set plan_seq='" + planseq1 + "' where rmes_id='" + rmesid + "' ";
                    dc.ExeSql(sql);
                    //e.Result = "OK,下调成功！";
                    break;
                case "Confirm":
                    for (int i = 0; i < count1; i++)
                    {
                        sql = "update data_plan set confirm_flag='Y' where rmes_id='" + aa[i] + "'";
                        dc.ExeSql(sql);
                    }
                    //e.Result = "OK,已确认！";
                    break;
                case "Cancel":
                    for (int i = 0; i < count1; i++)
                    {
                        sql = "update data_plan set confirm_flag='N' where rmes_id='" + aa[i] + "'";
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
        private void setCondition()
        {
            string sql = "select t.*,a.cjzd from VW_DATA_PLAN t left join ATPUPLAN_CJSTATION a on t.plan_code=a.jhdm where plan_type='E' and begin_date>=to_date('" + ASPxDateEdit1.Date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') and begin_date<=to_date('" + ASPxDateEdit2.Date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') and pline_code in(select pline_code from code_product_line t where rmes_id in (select pline_code from rel_user_pline where user_id='" + theUserId + "') ) order by begin_date,plan_seq ";
            DataTable dt = dc.GetTable(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string remark1 = dt.Rows[i]["REMARK"].ToString();
                sql = "select FUNC_GET_REMARK('" + remark1 + "') from dual ";
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
            string plantype1 = "E";  //E拆解
            string isbom1 = "Y" ;
            int qty11 = Convert.ToInt32(qty1);
            int qty21 = 0;
            if (planso == "" || productseries == "" || qty2 == "" || begindate1 == "" || enddate1 == "" || planseq == "" || customer == "" || plantype1 == "")
            {
                e.RowError = "Fail,计划参数缺少！";
                return;
            }
            try
            {
                qty21 = Convert.ToInt32(qty2);
            }
            catch
            {
                e.RowError = "Fail,计划数量不合法！";
                return;
            }
            if (plantype1 == "D")
            {
                if (qty21 != 1)
                {
                    e.RowError = "Fail,返修计划数量限定为1！";
                    return;
                }
            }
            if (plantype1 == "C")
            {
                if (isbom1 == "N")
                {
                    e.RowError = "Fail,改制计划必须转BOM！";
                    return;
                }
            }
            if (qty11 < qty21)
            {
                e.RowError = "Fail,计划数量不能大于原数量！";
                return;
            }
            if (begindate1.CompareTo(enddate1) > 0)
            {
                e.RowError = "Fail,结束日期不能小于开始日期！";
                return;
            }

            //调用函数判断计划代码是否符合规范
            string sqlPlan = "select func_check_plan('E1','" + plancode + "','" + PlineCode.Value.ToString() + "','" + Rsite.Value.ToString() + "','" + planso + "') from dual";
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

            string isbom1 =  "Y";
            string plantype = "E";
            DateTime beginDate = begindate1.Date;
            DateTime endDate = enddate1.Date;

            userManager theUserManager = (userManager)Session["theUserManager"];
            string gylx1;
            string accountdate1;
            string ROUNTING_CODE1 = "A01"; //工艺路线代码，默认为A01
            //调用函数判断计划代码是否符合规范
            string sqlPlan = "select func_check_plan('E1','" + plancode.Text.Trim() + "','" + PlineCode.SelectedItem.Value + "','" + RSite.SelectedItem.Value + "','" + planso.Text.Trim() + "') from dual";
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

            PlanFactory.PL_CREATE_PLAN("MODIFY", theCompanyCode, PlineCode.SelectedItem.Value.ToString(), PlineCode.SelectedItem.Text, planseq.Text.Trim(), plancode.Text.Trim(), planso.Text.Trim(), planseries.Text.Trim(), plantype, theUserId, theUserManager.getUserName(), beginDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"), accountdate1, qty.Text.Trim(), "0", "0", RSite.SelectedItem.Value.ToString(), gylx1, customer.Text.Trim(), remark.Text.Trim(), ROUNTING_CODE1, isbom1);

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
            string sql = "SELECT REMARK FROM VW_DATA_PLAN WHERE  RMES_ID='" + id + "'";
            dataConn dc = new dataConn(sql);
            DataTable dt = dc.GetTable();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string remark1 = dt.Rows[i]["REMARK"].ToString();
                sql = "select FUNC_GET_REMARK('" + remark1 + "') from dual ";
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
            {
                DateTime beginDate = ASPxBeginDate.Date;
                DateTime endDate = ASPxEndDate.Date;
                string pline = ASPxComboBoxPline.SelectedItem.Value.ToString();
                string rountingsite = ASPxComboBoxProcess.SelectedItem.Value.ToString();
                string plineName = ASPxComboBoxPline.SelectedItem.Text;
                string process = ASPxComboBoxProcess.SelectedItem.Value.ToString();
                string cjzd = CJZD.SelectedItem.Value.ToString();
                string series = ASPxTextSeries.Text;
                string so = ASPxTextBoxSO.Text;
                string planCode = ASPxTextPlanCode.Text;
                string seq = ASPxTextBoxSeq.Text;
                string remark = ASPxTextBoxRemark.Text;
                string customerName = ASPxTextBoxCustomerName.Text;
                string qty = ASPxTextBoxQty.Text;
                string gylx1;
                string accountdate1;
                string ROUNTING_CODE1 = "A01"; //工艺路线代码，默认为A01
                //string plantype = ASPxComboBoxPlanType.SelectedItem.Value.ToString();
                string plantype = "E";
                string IsBOm = "";
                IsBOm = "Y";
                //调用函数判断计划代码是否符合规范
                string sqlPlan = "select func_check_plan('E','" + planCode + "','" + pline + "','" + rountingsite + "','" + so + "') from dual";
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
                    PlanFactory.PL_CREATE_PLAN("ADD", theCompanyCode, pline, plineName, seq, planCode, so, series, plantype, theUserId, theUserManager.getUserName(), beginDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"), accountdate1, qty, "0", "0", rountingsite, gylx1, customerName, remark, ROUNTING_CODE1, IsBOm);
                    //dc.ExeSql("call PL_CREATE_PLAN('ADD','" + theCompanyCode + "','" + pline + "','" + plineName + "','" + seq + "','" + planCode + "','" + so + "','" + series + "','A','" + theUserId + "','" + theUserManager.getUserName() + "',to_date('" + beginDate.ToString("yyyy-MM-dd") + "','yyyy-MM-dd'),to_date('" + endDate.ToString("yyyy-MM-dd") + "','yyyy-MM-dd')),to_date('" + accountdate1 + "','yyyy-MM-dd')),'" + qty + "','0','0','" + rountingsite + "','" + gylx1 + "','" + customerName + "','" + remark + "','" + ROUNTING_CODE1 + "'");
                    string sql = "delete from ATPUPLAN_CJSTATION where jhdm='"+planCode+"'";
                    dc.ExeSql(sql);
                    sql = "insert into ATPUPLAN_CJSTATION(jhdm,cjzd) values('"+planCode+"','"+CJZD+"')";
                    dc.ExeSql(sql);
                    e.Result = "OK,计划提交成功！";
                    return;
                }
                catch (Exception ex)
                {
                    e.Result = "Fail,计划生成失败！";
                    return;
                }
            }
        }

        protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string plancode = e.Values["PLAN_CODE"].ToString();
            string planso = e.Values["PLAN_SO"].ToString();
            string PlineCode = e.Values["PLINE_CODE"].ToString();
            string Planseq = e.Values["PLAN_SEQ"].ToString();
            string Rsite = e.Values["ROUNTING_SITE"].ToString();
            string qty = e.Values["PLAN_QTY"].ToString();
            userManager theUserManager = (userManager)Session["theUserManager"];

            string sql = "select count(1) from data_plan_sn where plan_code='" + plancode + "'";
            dc.setTheSql(sql);
            if (dc.GetValue().ToString() != "0")
            {
                ASPxGridView1.JSProperties.Add("cpCallbackName", "Delete");
                ASPxGridView1.JSProperties.Add("cpCallbackRet", "该计划还有未删除的条码!");
            }
            else
            {
                PlanFactory.PL_CREATE_PLAN("DELETE", theCompanyCode, PlineCode, "", Planseq, plancode, planso, "", "E", theUserId, theUserManager.getUserName(), "", "", "", qty, "0", "0", Rsite, "", "", "", "", "Y");
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
                        //if (sn_flag == "N")
                        //{
                        //    e.Enabled = false;
                        //}
                    }
                    break;

                case "Cancel":
                    //if (item_flag == "Y" && confirm_flag == "Y" && run_flag == "Y")
                    if (e.CellType == GridViewTableCommandCellType.Filter) break;
                    if (confirm_flag == "Y")
                    {
                        e.Enabled = true;
                        e.Visible = DefaultBoolean.True;
                        if (item_flag == "Y")
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
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "G.xls";


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
                    string sqlPlan = "select func_check_plan('E','" + planCode + "','" + pline + "','" + rountingsite + "','" + planSo + "') from dual";
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
                        string planQty = t[2].ToString().Trim();
                        string planCus = t[3].ToString().Trim();
                        string pline = t[4].ToString().Trim();
                        string rountingsite = t[5].ToString().Trim();
                        string remark = t[6].ToString().Trim();
                        string accountdate1;
                        string gylx1;
                        string ROUNTING_CODE1 = "A01";
                        string plineName = "", seq = "", series = "";
                        string plantype = "E";
                        string IsBom = "Y";
                        //调用函数判断计划代码是否符合规范
                        string sqlPlan = "select func_check_plan('E','" + planCode + "','" + pline + "','" + rountingsite + "','" + planSo + "') from dual";
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

                        sql = "select nvl(max(plan_seq+1),'1') from data_plan where pline_code='" + pline + "' and plan_type='" + plantype + "' and begin_date=to_date('" + ASPxDateEdit1.Date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') ";
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
                            PlanFactory.PL_CREATE_PLAN("ADD", theCompanyCode, pline, plineName, seq, planCode, planSo, series, plantype, theUserId, theUserManager.getUserName(), ASPxDateEdit1.Date.ToString("yyyy-MM-dd"), ASPxDateEdit1.Date.ToString("yyyy-MM-dd"), accountdate1, planQty, "0", "0", rountingsite, gylx1, planCus, remark, ROUNTING_CODE1, IsBom);
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
            ASPxGridViewExporter1.WriteXlsToResponse("导出计划");
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
                    e.Row.BackColor = System.Drawing.Color.Green;
                    break;
            }
            status = e.GetValue("BOM_FLAG").ToString();//已生成BOM蓝色
            switch (status)
            {
                case "Y":
                    e.Row.BackColor = System.Drawing.Color.Blue;
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
            
        }
    }
}