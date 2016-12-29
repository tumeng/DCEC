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
using Rmes.DA.Procedures;
using System.Net;
/**
 * 2016-09-09
 * 游晓航
 * 八大件查询
 * */

namespace Rmes.WebApp.Rmes.Inv.inv8800
{
    public partial class inv8800 : BasePage
    {
        private dataConn dc = new dataConn();
        private PubCs thePubCs = new PubCs();

        private static string theCompanyCode, theUserId, theUserName;
        public string theProgramCode, MachineName;
        public Database db = DB.GetInstance();
        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();
            theUserName = theUserManager.getUserName();
            //获取MachineName
            string hostIPAddress = Page.Request.UserHostAddress;
            //IPHostEntry hostInfo = System.Net.Dns.GetHostByAddress(hostIPAddress);
            MachineName = hostIPAddress;
            //MachineName = System.Net.Dns.GetHostName();
            theProgramCode = "inv8800";
             
            if (!IsPostBack)
            {
                ASPxDateEdit1.Date = DateTime.Now;
                ASPxDateEdit2.Date = DateTime.Now;
                //ASPxDateEdit2.Date = DateTime.Now.AddDays(1);
                //Session["inv8800table"] = null;

            }
            
            initCode();
            setCondition();
            ASPxListBoxUnused_Init();
            //ASPxListBoxUnused.ClientSideEvents.ItemDoubleClick = "function(s,e) {var index = ListBoxUnused.GetSelectedIndex();if(index!=-1) ListBoxUnused.RemoveItem(index);}";
            //双击删除
            ASPxListBoxUsed.ClientSideEvents.ItemDoubleClick = "function(s,e) {var index = ListBoxUsed.GetSelectedIndex();if(index!=-1){ ListBoxUsed.RemoveItem(index);}}";
            
        }
       
        private void initCode()
        {
            //初始化下拉列表
            string sql = "select distinct a.pline_code,b.pline_name from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'";
            SqlCode.SelectCommand = sql;
            SqlCode.DataBind();
            //txtPCode.SelectedIndex = txtPCode.Items.Count >= 0 ? 0 : -1;


        }
         

        protected void ASPxListBoxUnused_Init()
        {
            string sql = "select distinct part from atpumainpart where part is not null order by part";
            DataTable dt = dc.GetTable(sql);
            ASPxListBoxUnused.DataSource = dt;
            ASPxListBoxUnused.DataBind();
             
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtIteam.Text.Trim() == "")
            {
                return;
            }
            string sql = "insert into atpumainpart(part)values('" + txtIteam.Text.Trim() + "') ";
            dc.ExeSql(sql);
            ASPxListBoxUnused_Init(); 
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string sql = "delete from atpumainpart where part='" + txtIteam.Text.Trim() + "'" ;
            dc.ExeSql(sql);
            ASPxListBoxUnused_Init();
        }
        public void ASPxGridView1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            DateTime DT1 = Convert.ToDateTime(ASPxDateEdit1.Text.Trim());
            DateTime DT2 = Convert.ToDateTime(ASPxDateEdit2.Text.Trim());
            string plinecode = "",scode="";
            try
            {
                if (DT1.AddDays(31) < DT2)
                {
                    ASPxGridView1.JSProperties.Add("cpCallbackName", "Fail");
                    ASPxGridView1.JSProperties.Add("cpCallbackRet", "选择日期范围不能超过31天，请重新选择！");
                    return;
                }
                else if (ASPxListBoxUsed.Items.Count <= 0)
                {
                    ASPxGridView1.JSProperties.Add("cpCallbackName", "Fail");
                    ASPxGridView1.JSProperties.Add("cpCallbackRet", " 请选择要查询的零件！");
                    return;
                }
                plinecode = txtPCode.Value.ToString();
                if (plinecode == "E")
                {
                    scode = "ZDE068";
                }
                if (plinecode == "W")
                {
                    scode = "ZD910";
                }
                if (plinecode == "R")
                {
                    scode = "RONE001";
                }
                DataTable Table1 = new DataTable();
                Table1.Columns.Add("日期");
                Table1.Columns.Add("SO");
                Table1.Columns.Add("计划");
                for (int a = 0; a < ASPxListBoxUsed.Items.Count; a++)
                {
                    string part = ASPxListBoxUsed.Items[a].ToString();
                    Table1.Columns.Add(part);
                    Table1.Columns.Add(part + "数量");  //数量=零件数量*计划数量
                    Table1.Columns.Add(part + "供应商");
                    PL_QUERY_ITEM8 sp = new PL_QUERY_ITEM8()
                    {
                        PLINECODE1 = plinecode,
                        SCODE1 = scode,
                        BEGINDATE1 = ASPxDateEdit1.Date,
                        ENDDATE1 = ASPxDateEdit2.Date,
                        PART1 = part,
                        MACHINENAME1=MachineName
                    };
                    Procedure.run(sp);
                }


                string sql1 = "select distinct to_char(begin_date,'yyyy-mm-dd') begin_date,plan_code ,plan_so,plan_qty,plan_seq from data_plan where pline_code='" + txtPCode.Value.ToString() + "'"
                              +" and begin_date>=to_date('" + ASPxDateEdit1.Text.Trim() + "', 'yyyy-mm-dd hh24:mi:ss') and  end_date<=to_date('" + ASPxDateEdit2.Text.Trim() + "', 'yyyy-mm-dd hh24:mi:ss') "
                              + "and plan_type<>'C' and plan_type<>'D' order by begin_date,plan_seq";
                DataTable dt1 = dc.GetTable(sql1);
                for (int j = 0; j < dt1.Rows.Count; j++)
                {
                    string PlanQty = dt1.Rows[j][3].ToString();
                    string PlanSo = dt1.Rows[j][2].ToString();
                    string PlanCode = dt1.Rows[j][1].ToString();
                    string RQbegin = dt1.Rows[j][0].ToString();
                    int PlanSl = Convert.ToInt32(PlanQty);
                    DataRow dr = Table1.NewRow();
                    dr[0] = RQbegin;
                    dr[1] = PlanSo;
                    dr[2] = PlanCode;
                    for (int i = 0, m = 0; i < ASPxListBoxUsed.Items.Count; i++)
                    {
                        string code = "", qty = "", gys = "";
                         
                        string part2 = ASPxListBoxUsed.Items[i].ToString();
                        string ChSql1 = "select replace ( a.comp,'#','') comp ,a.qty,a.gysmc  from RSTBOMQD_ITEM8 a   where a.udesc='" + part2 + "' and a.zddm='" + scode + "' and a.plan_Code='" + PlanCode + "' and a.plan_so='" + PlanSo + "' ";
                        DataTable dt2 = dc.GetTable(ChSql1);
                        if (dt2.Rows.Count > 0)
                        {
                            code = dt2.Rows[0][0].ToString();
                            qty = dt2.Rows[0][1].ToString();
                            gys = dt2.Rows[0][2].ToString();
                            int sl = Convert.ToInt32(qty);
                            dr[m + 3] = code;
                            dr[m + 4] = sl * PlanSl;
                            dr[m + 5] = gys;
                            m = m + 3;

                        }
                        else
                        {
                            dr[m + 3] = code;
                            dr[m + 4] = "";
                            dr[m + 5] = gys;
                            m = m + 3;
                        }
                    }
                    Table1.Rows.Add(dr);
                }

                Session["inv8800table"] = Table1;
                ASPxGridView1.DataSource = Table1;
                ASPxGridView1.DataBind();
               
            }
            catch  { }
                
        }
        private void setCondition()
        {
            string QtySql = "", part = "";

            QtySql = "select a.udesc,replace (a.comp,'#','') comp, sum(b.plan_qty*a.qty) sl  from RSTBOMQD_ITEM8 a left join data_plan b on a.plan_code=b.plan_code   where machinename='"+MachineName+"' and (udesc='&*北自所信息部' ";
            
                //QtySql = "select udesc,comp,count(*) sl from RSTBOMQD_ITEM8 where ";
                for (int a = 0; a < ASPxListBoxUsed.Items.Count; a++)
                {
                    QtySql = QtySql + "or ";
                    part = ASPxListBoxUsed.Items[a].ToString();
                    QtySql = QtySql + " udesc='" + part + "' ";
                }
               
            
            QtySql = QtySql + "  )group by comp,udesc order by udesc,comp ";
            DataTable QtyDt = dc.GetTable(QtySql);
            ASPxGridView2.DataSource = QtyDt;
            ASPxGridView2.DataBind();
        }
        public void ASPxGridView2_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            setCondition();
        }
        protected void btnXlsExport_Click(object sender, EventArgs e)
        {
            DataTable table1 = Session["inv8800table"] as DataTable;
            ASPxGridView1.DataSource = table1;
            ASPxGridView1.DataBind();
            ASPxGridViewExporter1.WriteXlsToResponse("八大件查询信息导出");
        }

     
    }
}