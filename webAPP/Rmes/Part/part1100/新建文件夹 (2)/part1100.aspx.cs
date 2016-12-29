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
 * 功能概述：JIT计算监控
 * 作者：游晓航
 * 创建时间：2016-08-03
 */
namespace Rmes.WebApp.Rmes.Part.part1100
{
    public partial class part1100 : BasePage
    {
        private dataConn dc = new dataConn();
        PubCs thePubCs = new PubCs();
        //public DateTime theBeginDate, theEndDate;
        public string theCompanyCode;
        private string theUserId,theUserCode;
        public string theProgramCode;
        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();
            theUserCode = theUserManager.getUserCode();
           // thePlineCode = theUserManager.getPlineCode();
            theProgramCode = "part1100";
            initCode();
            setCondition();
            if (!IsPostBack)
            {
                ASPxDateEdit1.Date = DateTime.Now;
                ASPxDateEdit3.Date = DateTime.Now.AddDays(1);
                DateOnlinetime.Date = DateTime.Now;
            }

            //DateOnlinetime.TimeSectionProperties.Visible = true;
            //DateOnlinetime.UseMaskBehavior = true;
            //DateOnlinetime.DisplayFormatString = "None";
            if (Request["opFlag"] == "submit")
            {
                string str1 = "", tableName = "";
                string tabname = Request["TABNAME"].ToString().Trim();
                string pcode = Request["PCODE"].ToString().Trim();
                string onltime = Request["ONLTIME"].ToString().Trim();

                DateTime DT1 = Convert.ToDateTime(onltime);
                DateTime DT2 = DateTime.Now;
               
                if (ComboTabN2.Text.Trim() == "三方物流要料")
                {
                    tableName = "SF";
                }
                else
                {
                    tableName = "KF";
                }
                if (DT1 < DT2)
                {
                    str1 = "Fail,到货时间不能小于当前时间!";

                }
                else
                {
                    string sql2 = "select manualflag from ms_jit_manualflag where  jitflag='" + tableName + "' and manualflag='0' AND GZDD ='" + pcode + "'";
                                

                    DataTable dt2 = dc.GetTable(sql2);

                    if (dt2.Rows.Count > 0)
                    {
                        str1 = "Fail,当前有未计算的手工提交，不能连续提交！!";
                       
                    }
                    else
                    {
                        string insertSql = "INSERT INTO MS_JIT_MANUALFLAG(JITFLAG,MANUALFLAG,COMMITTIME,JITUSER,GZDD,ONLINETIME) "
                                + "values('" + tableName + "','0',SYSDATE,'" + theUserCode + "','" + pcode + "', to_date('" + DT1 + "','yyyy-mm-dd hh24:mi:ss'))";

                        dc.ExeSql(insertSql);
                        str1 = "OK,提交完毕!";

                        //sql2 = "SELECT  NVL(JITUSER,'SYSTEM') JITUSER,DECODE(MANUALFLAG,'0','还未计算','1','已经计算') MANUALFLAG,TO_CHAR(COMMITTIME,'YYYY-MM-DD HH24:MI:SS') COMMITTIME,NVL(TO_CHAR(ONLINETIME,'YYYY-MM-DD HH24:MI:SS'),'') ONLINETIME,NVL(TO_CHAR(JITTIME,'YYYY-MM-DD HH24:MI:SS'),'') JITTIME FROM MS_JIT_MANUALFLAG WHERE "
                        //       + "GZDD IN ( select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') and jitflag='SF' and committime>sysdate-3 order by committime";

                        //dt2 = dc.GetTable(sql2);

                        //ASPxGridView2.DataSource = dt2;
                        //ASPxGridView2.DataBind();


                    }
                }
                this.Response.Write(str1);
                this.Response.End();
            }

        }
        private void initCode()
        {
            //初始化用户下拉列表
            string sql = "select distinct a.pline_code,b.pline_name from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'";
            SqlCode.SelectCommand = sql;
            SqlCode.DataBind();
        }
        private void setCondition()
        {
            string sql = "";
            sql = "select * from MS_JIT_LOG WHERE JITTIME>=to_date('" + ASPxDateEdit1.Text.Trim() + "','YYYY-MM-DD  hh24:mi:ss') AND JITTIME<=to_date('" + ASPxDateEdit3.Text.Trim() + "','YYYY-MM-DD  hh24:mi:ss') ";
            if (ComboTabN.Text.Trim() == "三方物流要料")
            {                                 
                    sql = sql + " and  JITFLAG='SF' ";
            }
            else 
            {
                   sql = sql + " and JITFLAG='KF' ";
            }
            sql = sql + " order by JITTIME desc"; 

            DataTable dt = dc.GetTable(sql);
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();

            string sql2 = "SELECT  NVL(JITUSER,'SYSTEM') JITUSER,DECODE(MANUALFLAG,'0','还未计算','1','已经计算') MANUALFLAG,TO_CHAR(COMMITTIME,'YYYY-MM-DD HH24:MI:SS') COMMITTIME,NVL(TO_CHAR(ONLINETIME,'YYYY-MM-DD HH24:MI:SS'),'') ONLINETIME,NVL(TO_CHAR(JITTIME,'YYYY-MM-DD HH24:MI:SS'),'') JITTIME FROM MS_JIT_MANUALFLAG WHERE "
                         + "GZDD IN ( select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') and jitflag='SF' and committime>sysdate-3 order by committime";

            DataTable dt2 = dc.GetTable(sql2);
           
            ASPxGridView2.DataSource = dt2;
            ASPxGridView2.DataBind();
            
            
        }
        protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            setCondition();
            ASPxGridView1.Selection.UnselectAll();
            
        }
        protected void ASPxGridView2_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            setCondition();
             

        }
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
           
                string a = txtPCode2.Text.Trim();
                if (a == null || a == "")
                {
                    Response.Write("<script>alert('请先选择生产线，再点刷新！')</script>");
                }
                else
                {
                    string sfyh = "", sfzt = "", sfsj = "", ltime = "", count="";
                    string sql3 = "SELECT  NVL(JITUSER,'SYSTEM'),DECODE(RUNFLAG,'0','未计算','1','正在计算'),TO_CHAR(NVL(JITTIME,SYSDATE),'YYYY-MM-DD HH24:MI:SS') FROM MS_JIT_STATE WHERE  "
                                + "GZDD ='" + txtPCode2.Value.ToString() + "' and jitflag='SF' ";

                    DataTable dt3 = dc.GetTable(sql3);
                    if (dt3.Rows.Count > 0)
                    {
                          sfyh = dt3.Rows[0][0].ToString();
                          sfzt = dt3.Rows[0][1].ToString();
                          sfsj = dt3.Rows[0][2].ToString();
                    }
                    textSfsj.Text = sfsj;
                    textSfyh.Text = sfyh;
                    textSfzt.Text = sfzt;
                    string sql4 = "select  to_char(nvl(last_calculate_time,sysdate),'YYYY-MM-DD HH24:MI:SS') from ms_last_calculate_time where flag='SF' and "
                               + "GZDD ='" + txtPCode2.Value.ToString() + "' ";

                    DataTable dt4 = dc.GetTable(sql4);
                    if (dt4.Rows.Count > 0)
                    {
                        ltime = dt4.Rows[0][0].ToString();
 
                    }
                      
                    TextLtime.Text = ltime;

                    string sql5 = "select a.parameter_value-b.sxsl  from ms_jit_parameter a ,(select ONLINE_QTY sxsl from data_plan where pline_code = '" + txtPCode2.Value.ToString() + "' "
                               + "and BEGIN_DATE>(select jittime from ms_jit_state where gzdd='" + txtPCode2.Value.ToString() + "' and jitflag='SF')) b where  a.parameter_code='$THIRD_JIT_ONLINE_NUM' and "
                               + "A.GZDD IN ( select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') ";

                    DataTable dt5 = dc.GetTable(sql5);
                     
                    if (dt5.Rows.Count > 0)
                    {
                          count = dt5.Rows[0][0].ToString();
                    }
                    TextCount.Text = count;
                }
            
            
 
        }
        
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            string tableName = "";
            DateTime DT1 = DateOnlinetime.Date;
            DateTime DT2 = DateTime.Now;
            if (DT1 < DT2)
            {
                Response.Write("<script>alert('到货时间不能小于当前时间！')</script>");
                return;
            }
            if (ComboTabN2.Text.Trim() == "三方物流要料")
            {
                 tableName = "SF";
            }
            else
            {
                 tableName = "KF";
            }
            string sql2 = "select manualflag from ms_jit_manualflag where  jitflag='" + tableName + "' and manualflag='0' AND "
                        + "GZDD IN ( select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') ";

            DataTable dt2 = dc.GetTable(sql2);

            if (dt2.Rows.Count > 0)
            {
                Response.Write("<script>alert('当前有未计算的手工提交，不能连续提交！')</script>");
            }
            else 

            {

                string insertSql = "INSERT INTO MS_JIT_MANUALFLAG(JITFLAG,MANUALFLAG,COMMITTIME,JITUSER,GZDD,ONLINETIME) "
                        + "values('" + tableName + "','0',SYSDATE,'" + theUserCode + "','" + txtPCode.Value.ToString() + "',to_date('" + DateOnlinetime.Value + "','yyyy-mm-dd hh24:mi:ss'))";

                dc.ExeSql(insertSql);

                  sql2 = "SELECT  NVL(JITUSER,'SYSTEM') JITUSER,DECODE(MANUALFLAG,'0','还未计算','1','已经计算') MANUALFLAG,TO_CHAR(COMMITTIME,'YYYY-MM-DD HH24:MI:SS') COMMITTIME,NVL(TO_CHAR(ONLINETIME,'YYYY-MM-DD HH24:MI:SS'),'') ONLINETIME,NVL(TO_CHAR(JITTIME,'YYYY-MM-DD HH24:MI:SS'),'') JITTIME FROM MS_JIT_MANUALFLAG WHERE "
                         + "GZDD IN ( select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') and jitflag='SF' and committime>sysdate-3 order by committime";

                  dt2 = dc.GetTable(sql2);

                ASPxGridView2.DataSource = dt2;
                ASPxGridView2.DataBind();


            }
 
        }
        
      
    }
}