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
using DevExpress.Utils;
/**
 * 功能概述：入库回冲确认
 * 作者：游晓航
 * 创建时间：2016-08-23
 */
namespace Rmes.WebApp.Rmes.Inv.inv9400
{
    public partial class inv9400 : BasePage
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
            theProgramCode = "inv9400";
            if (!IsPostBack)
            {
                ASPxDateEdit1.Date = DateTime.Now;
                ASPxDateEdit3.Date = DateTime.Now.AddDays(1);
                Session["inv9400sql"] = "";
            }
            initCode();
            if (Session["inv9400sql"] as string != "")
            {
                DataTable dt = dc.GetTable(Session["inv9400sql"] as string);
                ASPxGridView1.DataSource = dt;
                ASPxGridView1.DataBind();
            }
            if (Request["opFlag"] == "getEditSeries")
            {
                string str1 = "", str2 = "", str = "", str3 = "";
                string lsh = Request["LSH"].ToString().Trim();
                //string qad = Request["QAD"].ToString().Trim();

                string sql = "select PLAN_SO,PLAN_CODE from DATA_PRODUCT where SN='" + lsh + "'";
                DataTable dt = dc.GetTable(sql);
                if (dt.Rows.Count > 0)
                {
                    str1 = dt.Rows[0][0].ToString();
                    str2 = dt.Rows[0][1].ToString();
                }

                string sql2 = "select t.station_code from data_store t where t.SN='" + lsh + "'";

                DataTable dt2 = dc.GetTable(sql2);
                if (dt2.Rows.Count > 0)
                {
                    str3 = dt2.Rows[0][0].ToString();
                }

                str = str1 + "," + str2 + "," + str3;
                this.Response.Write(str);
                this.Response.End();
            }
            //if (Session["inv9400sql"] as string != "")
            //{
            //    DataTable dt = dc.GetTable(Session["inv99400sql"] as string);
            //    ASPxGridView1.DataSource = dt;
            //    ASPxGridView1.DataBind();
            //}
        }
        private void initCode()
        {
            //初始化生产线下拉列表
            string sql = "select distinct a.pline_code,b.pline_name from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'";
            SqlCode.SelectCommand = sql;
            SqlCode.DataBind();
        }
        private void setCondition(string sql)
        {

            DataTable dt = dc.GetTable(sql);

          
            Session["inv9400sql"] = sql;
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();

        }
        private void setCondition2()
        {
            if (TextSN.Text.Trim() != "")
            {
                string sql = "select SN GHTM ,PLAN_CODE JHDM,PLAN_SO SO,to_char(WORK_TIME,'yyyy-mm-dd hh24:mi:ss') GZRQ, PLINE_CODE GZDD,'' state1,''state2 ,'' state3 from DATA_PRODUCT where SN='" + TextSN.Text.Trim() + "'";

                DataTable dt = dc.GetTable(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string lsh = dt.Rows[i][0].ToString();
                    string sql2 = "select * from qad_bkfl where lsh='" + lsh + "'";
                    DataTable dt2 = dc.GetTable(sql2);
                    if (dt2.Rows.Count > 0)
                    {
                        //判断是否已回冲
                        dt.Rows[i][5] = "物料";
                        dt.Rows[i][6] = "已冲";


                    }
                    else
                    {
                        //判断是否下线
                        dt.Rows[i][4] = "未冲";
                        string sql3 = "select * from dp_rkwcb where ghtm='" + lsh + "'";
                        DataTable dt3 = dc.GetTable(sql3);
                        if (dt3.Rows.Count > 0)
                        {
                            dt.Rows[i][5] = "物料";
                            dt.Rows[i][6] = "在制";
                        }
                        else
                        {
                            //判断是否等待回冲
                            string sql4 = "select * from qad_bkflrecord where lsh1='" + lsh + "'";
                            DataTable dt4 = dc.GetTable(sql4);
                            if (dt4.Rows.Count > 0)
                            {
                                dt.Rows[0][5] = "物料";
                                dt.Columns[5].ReadOnly = false;
                                dt.Rows[i][6] = "未回冲";
                            }
                            else
                            {
                                dt.Rows[i][5] = "物料";
                                dt.Rows[i][6] = "等待回冲";
                            }
                        }
                    }
                }

                ASPxGridView1.DataSource = dt;
                ASPxGridView1.DataBind();
            }
            else
            {
                Response.Write("<script>alert('请输入要查询的流水号！')</script>");

            }
        }
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {


            if (TextSN.Text.Trim() != "")
            {
                string sql = "select SN GHTM ,PLAN_CODE JHDM,PLAN_SO SO,to_char(WORK_TIME,'yyyy-mm-dd hh24:mi:ss') GZRQ, PLINE_CODE GZDD,'' state1,''state2 ,'' state3 from DATA_PRODUCT where SN='" + TextSN.Text.Trim() + "'";
                //Session["inv9400sql"] = sql;
                DataTable dt = dc.GetTable(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string lsh = dt.Rows[i][0].ToString();
                    string sql2 = "select * from qad_bkfl where lsh='" + lsh + "'";
                    DataTable dt2 = dc.GetTable(sql2);
                    if (dt2.Rows.Count > 0)
                    {
                        //判断是否已回冲
                        dt.Rows[i][5] = "物料";
                        dt.Rows[i][6] = "已冲";


                    }
                    else
                    {
                        //判断是否下线
                        dt.Rows[i][4] = "未冲";
                        string sql3 = "select * from dp_rkwcb where ghtm='" + lsh + "'";
                        DataTable dt3 = dc.GetTable(sql3);
                        if (dt3.Rows.Count > 0)
                        {
                            dt.Rows[i][5] = "物料";
                            dt.Rows[i][6] = "在制";
                        }
                        else
                        {
                            //判断是否等待回冲
                            string sql4 = "select * from qad_bkflrecord where lsh1='" + lsh + "'";
                            DataTable dt4 = dc.GetTable(sql4);
                            if (dt4.Rows.Count > 0)
                            {
                                dt.Rows[0][5] = "物料";
                                dt.Columns[5].ReadOnly = false;
                                dt.Rows[i][6] = "未回冲";
                            }
                            else
                            {
                                dt.Rows[i][5] = "物料";
                                dt.Rows[i][6] = "等待回冲";
                            }
                        }
                    }
                }

                ASPxGridView1.DataSource = dt;
                ASPxGridView1.DataBind();
            }
            else
            {
                Response.Write("<script>alert('请输入要查询的流水号！')</script>");

            }

        }
        //全部确认
        protected void ASPxCbSubmit_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
        {
            // string collection = e.Parameter;
            string[] collection = e.Parameter.Split('@');
            try
            {
                if (collection[0] == "Commit")
                {
                    int count1 = ASPxGridView1.VisibleRowCount;
                    // string strPmCode = ASPxGridView1.GetRowValues(ASPxGridView1.EditingRowVisibleIndex, new string[] { "PARAMETER_CODE" }).ToString();
                    for (int i = 0; i < count1; i++)
                    {
                        string ThisLsh = ASPxGridView1.GetRowValues(i, "GHTM").ToString();
                        string gzdd = ASPxGridView1.GetRowValues(i, "GZDD").ToString();
                        string thisJhdm = ASPxGridView1.GetRowValues(i, "JHDM").ToString();
                        string ThisSo = ASPxGridView1.GetRowValues(i, "SO").ToString();
                        //判断是否改制，非改制发动机才可以进行确认
                        string ChSql = "select * from DATA_PRODUCT a left join data_plan b on a.plan_code=b.plan_code  where a.SN='" + ThisLsh + "' and (b.plan_type='C'OR b.plan_type='D')";
                        DataTable ch = dc.GetTable(ChSql);
                        if (ch.Rows.Count <= 0)
                        {
                            string Sql = "insert into qad_bkflrecord(lsh1,user1,rqsj1,gzdd1,jhdm1,jhso1) values('" + ThisLsh + "','" + theUserName + "',sysdate,'" + gzdd + "','" + thisJhdm + "','" + ThisSo + "') ";
                            string Sql2 = "insert into qad_bkfllog(lsh1,user1,rqsj1,gzdd1,jhdm1) values('" + ThisLsh + "','" + theUserName + "',sysdate,'" + gzdd + "','" + thisJhdm + "')";
                            dc.ExeSql(Sql2);
                            string chSql = "select * from qad_bkflrecord where lsh1='" + ThisLsh + "'";
                            DataTable dt = dc.GetTable(chSql);
                            if (dt.Rows.Count <= 0)
                            { dc.ExeSql(Sql); }
                        }

                    }
                    e.Result = "OK,已全部确认！";
                }

            }
            catch (Exception e1)
            {
                e.Result = "Fail,确认失败！";
                return;
            }
        }

        public void ASPxGridView1_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            ASPxGridView grid = (ASPxGridView)sender;
            if (grid.GetRowValues(e.VisibleIndex, "GHTM") == null)
                return;
            string lsh = grid.GetRowValues(e.VisibleIndex, "GHTM") as string;
            string sql = "select count(1) from qad_bkfl where lsh='" + lsh + "'";
            string sl1 = dc.GetValue(sql);
            string sql2 = "select count(1) from dp_rkwcb where ghtm='" + lsh + "'";
            string sl2 = dc.GetValue(sql2);
            string sql3 = "select count(1) from qad_bkflrecord where lsh1='" + lsh + "'";
            string sl3 = dc.GetValue(sql3);
            int isl1 = Convert.ToInt32(sl1);
            int isl2 = Convert.ToInt32(sl2);
            int isl3 = Convert.ToInt32(sl3);
            //int isl1 = 0;
            //int isl2 = 0;
            //int isl3 = 0;
            switch (e.ButtonID)
            {
                case "Offset1":
                    if (e.CellType == GridViewTableCommandCellType.Filter) break;
                    if (isl1 > 0)
                    {
                        e.Enabled = false;
                        e.Visible = DefaultBoolean.False;
                    }
                    else if (isl2 > 0)
                    {
                        if (isl3 == 0)
                        {
                            e.Enabled = true;
                            e.Visible = DefaultBoolean.True;

                        }
                        else { e.Visible = DefaultBoolean.False; }
                    }
                    break;
                case "Offset2":
                    if (e.CellType == GridViewTableCommandCellType.Filter) break;
                    if (isl1 > 0)
                    {
                        e.Enabled = false;
                        e.Visible = DefaultBoolean.True;
                    }
                    else { e.Visible = DefaultBoolean.False; }
                    break;
                case "Offset3":
                    if (e.CellType == GridViewTableCommandCellType.Filter) break;
                    if (isl1 > 0)
                    {
                        e.Enabled = false;
                        e.Visible = DefaultBoolean.False;

                    }
                    else if (isl2 > 0)
                    {
                        if (isl3== 0)
                        {
                            e.Enabled = false;
                            e.Visible = DefaultBoolean.False;
                        }
                        else
                        {
                            e.Enabled = true;
                            e.Visible = DefaultBoolean.True;
                        }
                    }
                    break;
            }

        }
        protected void ASPxGridView1_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.VisibleIndex < 0) return;
            string lsh = e.GetValue("GHTM").ToString();
            string sql = "select * from qad_bkfl where lsh='" + lsh + "'";
            DataTable dt = dc.GetTable(sql);
            if (dt.Rows.Count <= 0)//未冲的发动机设置为黄色
            { e.Row.BackColor = System.Drawing.Color.Yellow; }
            string sql2 = "select * from DATA_PRODUCT a left join data_plan b on a.plan_code=b.plan_code  where (b.plan_type='C' OR b.plan_type='D') and a.sn='" + lsh + "'";
            DataTable dt2 = dc.GetTable(sql2);
            if (dt2.Rows.Count > 0)//是改制发动机的话设置为红色
            { e.Row.BackColor = System.Drawing.Color.Red; }


        }
        //public void ASPxGridView2_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        //{
        //    if (e.Parameters == "-1")
        //    {
        //        ASPxGridView2.DataSource = null;
        //        ASPxGridView2.DataBind();
        //        return;
        //    }

        //    int rowIndex = int.Parse(e.Parameters);
        //    string Lsh = ASPxGridView1.GetRowValues(rowIndex, "GHTM").ToString();
        //    string sql3 = "select abom_comp ,abom_qty ,abom_op ,abom_wkctr ,abom_jhdm ,abom_so  ,abom_date  from rst_qad_bombkfl_bak where abom_lsh='" + Lsh + "' order by abom_wkctr";
        //    //string sss = "select abom_comp 零件,abom_qty 数量,abom_op 工序,abom_wkctr 工位,abom_jhdm 计划号,abom_so SO from rst_qad_bombkfl  order by abom_wkctr ";      
        //    DataTable dt3 = dc.GetTable(sql3);
        //    ASPxGridView2.DataSource = dt3;
        //    ASPxGridView2.DataBind();



        //}

        protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            string sql = "";
            if (e.Parameters == "BtnSubmit")
            {
                if (TextSN.Text.Trim() != "")
                {
                    sql = "select SN GHTM ,PLAN_CODE JHDM,PLAN_SO SO,to_char(WORK_TIME,'yyyy-mm-dd hh24:mi:ss') GZRQ, PLINE_CODE GZDD,'' state1,''state2 ,'' state3 from DATA_PRODUCT where SN='" + TextSN.Text.Trim() + "'";

                }
                else
                {
                    ASPxGridView1.JSProperties.Add("cpCallbackName", "Fail");
                    ASPxGridView1.JSProperties.Add("cpCallbackRet", "请输入要查询的流水号！");
                    return;
                }
            }
            else
            {
                sql = "select ghtm ,jhdm ,so ,to_char(GZRQ,'yyyy-mm-dd hh24:mi:ss') GZRQ ,GZDD,'' state1,''state2 ,'' state3 from dp_rkwcb where ghtm in(select a.SN from DATA_PRODUCT a left join data_plan b on a.plan_code=b.plan_code  where b.plan_type<>'C' and b.plan_type<>'D')";
                if (txtPlineCode.Text.Trim() != "")
                { sql = sql + " and gzdd='" + txtPlineCode.Value.ToString() + "'"; }
                else { sql = sql + " and gzdd IN (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "')"; }
                if (TextPlanCode2.Text.Trim() != "")
                {
                    sql = " and jhdm='" + TextPlanCode2.Text.Trim() + "'";
                }
                if (ASPxDateEdit1.Text.Trim() != "")
                {
                    sql = sql + " AND  GZRQ>=to_date('" + ASPxDateEdit1.Text.Trim() + "','YYYY-MM-DD ') ";
                }
                if (ASPxDateEdit3.Text.Trim() != "")
                {
                    sql = sql + " AND GZRQ<=to_date('" + ASPxDateEdit3.Text.Trim() + "','YYYY-MM-DD ') ";
                }
            }
            if (sql != "")
            {
                setCondition(sql);
            }
            ASPxGridView1.Selection.UnselectAll();
        }
        public void ASPxGridView1_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
        {
            string s = e.Parameters;
            string[] s1 = s.Split('|');
            string type1 = s1[0];
            int rowIndex = int.Parse(s1[1]);
            string sql = "";
            string ThisLsh = "", thisJhdm = "", ThisSo = "";

            //ASPxGridView atl1 = (ASPxGridView)sender;
            //int count1 = atl1.Selection.Count;
            //List<object> aa = atl1.GetSelectedFieldValues("RMES_ID");

            try
            {
                ThisLsh = ASPxGridView1.GetRowValues(rowIndex, "GHTM").ToString();
                thisJhdm = ASPxGridView1.GetRowValues(rowIndex, "JHDM").ToString();
                //ThisSo = Convert.ToString(ASPxGridView1.GetRowValues(rowIndex, "BEGIN_DATE")).Substring(0, Convert.ToString(ASPxGridView1.GetRowValues(rowIndex, "BEGIN_DATE")).IndexOf(" "));
                ThisSo = ASPxGridView1.GetRowValues(rowIndex, "SO").ToString();
            }
            catch
            {
                e.Result = "Fail,缺少关键值！";
                return;
            }
            sql = " select * from DATA_PRODUCT a left join data_plan b on a.plan_code=b.plan_code  where (b.plan_type='C' or b.plan_type='D')and sn='" + ThisLsh + "'  ";
            DataTable dt = dc.GetTable(sql);
        
            switch (type1)
            {
                case "Offset1":
                    if (dt.Rows.Count > 0)
                    {
                        e.Result = "Fail,改制发动机！";

                    }
                    else
                    {
                        string SQLSTR2 = "insert into qad_bkfllog(lsh1,user1,rqsj1,gzdd1,jhdm1) values('" + ThisLsh + "','" + theUserName + "',sysdate,'" + txtPlineCode.Value.ToString() + "','" + thisJhdm + "')";
                        dc.ExeSql(SQLSTR2);
                    }
                    break;
                case "Offset3":
                    
                    if (dt.Rows.Count > 0)
                    {
                        e.Result = "Fail,改制发动机！";

                    }
                    else
                    {
                        string  SQlstr = "delete from qad_bkflrecord where lsh1='" + ThisLsh + "'";
                        dc.ExeSql(SQlstr);
                    }
                     
                    //e.Result = "OK,下调成功！";
                    break;
                 
                
                default:

                    break;
            }
        }


    }
}