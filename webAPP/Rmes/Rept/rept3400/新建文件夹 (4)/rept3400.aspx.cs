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
 * 2016-10-29
 * 游晓航
 *  附装乱序领料单查询
 * */

public partial class Rmes_Rept_rept3400_rept3400 : Rmes.Web.Base.BasePage
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
        //MachineName = System.Net.Dns.GetHostName();
        string hostIPAddress = Page.Request.UserHostAddress;
        //IPHostEntry hostInfo = System.Net.Dns.GetHostByAddress(hostIPAddress);
        MachineName = hostIPAddress;
        theProgramCode = "rept3400";
        //ASPxListBoxPart1.Attributes.Add("ondblclick", "ListBoxDblClick(this);");
        if (!IsPostBack)
        {
            ASPxDateEdit1.Date = DateTime.Now;
            ASPxDateEdit2.Date = DateTime.Now.AddDays(1);


        }
        //if (Session["rept3400table"] as DataTable != null)
        //{

        //    ASPxGridView1.DataSource = Session["rept3400table"];
        //    ASPxGridView1.DataBind();
        //}
        initCode();
        setCondition();
        ASPxListBoxUnused_Init();
        //ASPxListBoxUnused.ClientSideEvents.ItemDoubleClick = "function(s,e) {var index = ListBoxUnused.GetSelectedIndex();if(index!=-1) ListBoxUnused.RemoveItem(index);}";
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
    protected void ASPxListBoxUsed_Callback(object sender, CallbackEventArgsBase e)
    {


    }

    protected void ASPxListBoxUnused_Init()
    {
        string sql = "select distinct part from PART_FZLX where part is not null order by part";
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
        string sql = "insert into PART_FZLX(part)values('" + txtIteam.Text.Trim() + "') ";
        dc.ExeSql(sql);
        ASPxListBoxUnused_Init();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string sql = "delete from PART_FZLX where part='" + txtIteam.Text.Trim() + "'";
        dc.ExeSql(sql);
        ASPxListBoxUnused_Init();
    }

    public void ASPxGridView1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        DateTime DT1 = Convert.ToDateTime(ASPxDateEdit1.Text.Trim());
        DateTime DT2 = Convert.ToDateTime(ASPxDateEdit2.Text.Trim());

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

        DataTable Table1 = new DataTable();
        //string iteam_code = "";
        Table1.Columns.Add("日期");
        Table1.Columns.Add("SO");
        Table1.Columns.Add("计划");
        Table1.Columns.Add("流水号");
        for (int a = 0; a < ASPxListBoxUsed.Items.Count; a++)
        {
            string part = ASPxListBoxUsed.Items[a].ToString();
            string delsql = "delete from RSTBOMQD_ITEM8 where udesc='" + part + "'";
            dc.ExeSql(delsql);
            Table1.Columns.Add(part);
            Table1.Columns.Add(part + "数量");  
            //Table1.Columns.Add(part+'▍↔'+"数量");

        }

        string plancode = "", PlanSo = "", PlanCode = "", RQbegin = "", plinecode = "", xl = "", scode = "", PlanQty = "";
        int PlanSl = 0;
        string snSql = "select  lsh,max(ptime) ptime from ATPUDQYQDYB where PTIME>=to_date('" + ASPxDateEdit1.Text.Trim() + "', 'yyyy-mm-dd hh24:mi:ss') and  PTIME<=to_date('" + ASPxDateEdit2.Text.Trim() + "', 'yyyy-mm-dd hh24:mi:ss') group by lsh   order by lsh,ptime";
        DataTable sndt = dc.GetTable(snSql);
        for (int j = 0; j < sndt.Rows.Count; j++)
        {
            RQbegin = sndt.Rows[j][1].ToString();
            string soSql = "select plan_code from data_plan_sn where sn='" + sndt.Rows[j][0].ToString() + "' and pline_code='" + txtPCode.Value.ToString() + "' and plan_type<>'C' and plan_type<>'D' ";
            plancode = dc.GetValue(soSql);
            //DataTable sodt = dc.GetTable(soSql);
            //if (sodt.Rows.Count > 0) { plancode = sodt.Rows[0][0].ToString(); }
            
            string sql1 = "select distinct begin_date,plan_so,plan_qty from data_plan where pline_code='" + txtPCode.Value.ToString() + "' and plan_code='" + plancode + "' ";
            DataTable dt1 = dc.GetTable(sql1);
            if (dt1.Rows.Count <= 0)
            {
                continue;
            }
            PlanSo = dt1.Rows[0][1].ToString();
            PlanQty = dt1.Rows[0][2].ToString();
            PlanSl = Convert.ToInt32(PlanQty);
            plinecode = txtPCode.Value.ToString();
            PlanCode = plancode;
            DataRow dr = Table1.NewRow();
            dr[0] = RQbegin;
            dr[1] = PlanSo;
            dr[2] = PlanCode;
            dr[3] = sndt.Rows[j][0].ToString();
            for (int i = 0, m = 0; i < ASPxListBoxUsed.Items.Count; i++)
            {
                string part2 = ASPxListBoxUsed.Items[i].ToString();
                if (plinecode == "E")
                {
                    scode = "ZF5";
                }
                if (plinecode == "W")
                {
                    scode = "ATPU-T560";
                }
                //string delsql = "delete from RSTBOMQD_ITEM8 where udesc='" + part2 + "'";
                //dc.ExeSql(delsql);
                PL_BOMZJTS_ITEM8 sp = new PL_BOMZJTS_ITEM8()
                {
                    PLANSO1 = PlanSo,
                    SCODE1 = scode,
                    PLANCODE1 = PlanCode,
                    PART1 = part2,    
                };
                Procedure.run(sp);
                PL_UPDATE_BOMZJTS_ITEM8 sp2 = new PL_UPDATE_BOMZJTS_ITEM8()
                {
                    PLANSO1 = PlanSo,
                    SCODE1 = scode,
                    PLANCODE1 = PlanCode,
                    PLINECODE1 = plinecode,
                };
                Procedure.run(sp2);

                PL_UPDATE_ITEM8 sp4 = new PL_UPDATE_ITEM8()
                {
                    PLANSO1 = PlanSo,                 
                    PLANCODE1 = PlanCode,
                    SCODE1 = scode
                };
                Procedure.run(sp4);

                //string ChSql22 = "select comp ,qty  from RSTBOMQD_ITEM8 where udesc='" + part2 + "' and zddm='" + scode + "' ";
                string ChSql1 = "select replace ( a.comp,'#','') comp ,a.qty,a.gysmc  from RSTBOMQD_ITEM8 a   where a.udesc='" + part2 + "' and a.zddm='" + scode + "' and a.plan_Code='" + PlanCode + "' ";
                
                if (PlanSo != "")
                {
                    ChSql1 = ChSql1 + " and a.plan_so='" + PlanSo + "'";
 
                }
                DataTable dt2 = dc.GetTable(ChSql1);
                string code = "", qty = "";
                int sl = 0;
                if (dt2.Rows.Count > 0)
                {
                     code = dt2.Rows[0][0].ToString();
                     qty = dt2.Rows[0][1].ToString();
                     sl = Convert.ToInt32(qty);
                    //dr[m + 4] = code;
                    //dr[m + 5] = sl * PlanSl; 
                    //m = m + 2;
                }
                dr[m + 4] = code;
                dr[m + 5] = sl ;
                m = m + 2;
            }

            Table1.Rows.Add(dr);
        }
        Session["rept3400table"] = Table1;
        ASPxGridView1.DataSource = Table1;
        ASPxGridView1.DataBind();

    }
    private void setCondition()
    {
        string QtySql = "", part = "";

        //QtySql = "select c.sn ,a.udesc,replace (a.comp,'#','') comp, sum(b.plan_qty*a.qty) sl from RSTBOMQD_ITEM8 a left join data_plan b "
        //+"on a.plan_code=b.plan_code left join data_plan_sn c on a.plan_code=c.plan_code  where udesc='&*北自所信息部' ";
        QtySql = "select  a.udesc,replace (a.comp,'#','') comp, sum(b.plan_qty*a.qty) sl from RSTBOMQD_ITEM8 a left join data_plan b on a.plan_code=b.plan_code   where udesc='&*北自所信息部' ";
         for (int a = 0; a < ASPxListBoxUsed.Items.Count; a++)
        {
            QtySql = QtySql + "or ";
            part = ASPxListBoxUsed.Items[a].ToString();
            QtySql = QtySql + " a.udesc='" + part + "' ";
        }


        QtySql = QtySql + "  group by a.comp,a.udesc  order by a.udesc,a.comp ";
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
        DataTable table1 = Session["rept3400table"] as DataTable;
        ASPxGridView1.DataSource = table1;
        ASPxGridView1.DataBind();
        ASPxGridViewExporter1.WriteXlsToResponse("乱序附装领料单导出");
    }
}
