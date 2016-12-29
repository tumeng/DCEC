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
        MachineName = System.Net.Dns.GetHostName();
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
        for (int a = 0; a < ASPxListBoxUsed.Items.Count; a++)
        {
            string part = ASPxListBoxUsed.Items[a].ToString();
            Table1.Columns.Add(part + "----数量");
            //Table1.Columns.Add(part+'▍↔'+"数量");

        }
        string plancode = "", PlanSo = "", PlanCode = "", RQbegin = "", plinecode = "", xl = "", scode = "";
        string snSql = "select lsh,ptime from ATPUDQYQDYB where PTIME>=to_date('" + ASPxDateEdit1.Text.Trim() + "', 'yyyy-mm-dd hh24:mi:ss') and  PTIME<=to_date('" + ASPxDateEdit2.Text.Trim() + "', 'yyyy-mm-dd hh24:mi:ss')  ";
        DataTable sndt = dc.GetTable(snSql);
        for (int j = 0; j < sndt.Rows.Count; j++)
        {
            RQbegin = sndt.Rows[j][1].ToString();
            string soSql = "select plan_code from data_plan_sn where sn='" + sndt.Rows[j][0].ToString() + "'";
            DataTable sodt = dc.GetTable(soSql);
            if (sodt.Rows.Count > 0) { plancode = sodt.Rows[0][0].ToString(); }
            string sql1 = "select distinct begin_date,plan_so from data_plan where pline_code='" + txtPCode.Value.ToString() + "' and plan_code='" + plancode + "' ";
            DataTable dt1 = dc.GetTable(sql1);
            if (dt1.Rows.Count > 0)
            {
                PlanSo = dt1.Rows[0][1].ToString();
                //RQbegin = dt1.Rows[0][0].ToString();
            }
            plinecode = txtPCode.Value.ToString();
            PlanCode = plancode;
            DataRow dr = Table1.NewRow();
            dr[0] = RQbegin;
            dr[1] = PlanSo;
            dr[2] = PlanCode;
            for (int i = 0; i < ASPxListBoxUsed.Items.Count; i++)
            {
                string part2 = ASPxListBoxUsed.Items[i].ToString();
                //string sql2 = "(select a.item_code,a.item_qty from data_sn_bom a left join data_plan b on a.plan_code=b.plan_code  where a.plan_code='" + PlanCode + "' and b.plan_so='" + PlanSo + "' and a.item_name='" + part2 + "') union "
                //             + "select a.item_code,a.item_qty from data_sn_bom_temp a left join data_plan b on a.plan_code=b.plan_code  where a.plan_code='" + PlanCode + "' and b.plan_so='" + PlanSo + "' and a.item_name='" + part2 + "'";

                //DataTable dt2 = dc.GetTable(sql2);


                string sql3 = "select xl from copy_engine_property where so='" + PlanSo.ToUpper() + "' and rownum=1 ";
                DataTable dt3 = dc.GetTable(sql3);
                if (dt3.Rows.Count > 0)
                {
                    xl = dt3.Rows[0][0].ToString();
                }
                if (plinecode == "E")
                {
                    scode = "ZF5";
                }
                if (plinecode == "W")
                {
                    scode = "ATPU-T560";
                }
                //if (plinecode == "R")
                //{
                //    scode = "RONE001";
                //}


                PL_QUERY_BOMZJTS3 sp = new PL_QUERY_BOMZJTS3()
                {
                    SO1 = PlanSo,
                    ZDDM1 = scode,
                    GZDD1 = plinecode,
                    FDJXL1 = xl,
                    JHDM1 = PlanCode
                };
                Procedure.run(sp);
                PL_UPDATE_BOMZJTS_CRM3 sp2 = new PL_UPDATE_BOMZJTS_CRM3()
                {
                    SO1 = PlanSo,
                    ZDDM1 = scode,
                    JHDM1 = PlanCode,
                    GZDD1 = plinecode
                };
                Procedure.run(sp2);
                //PL_UPDATE_BOMLSHTS3 sp3 = new PL_UPDATE_BOMLSHTS3()
                //{
                //    LSH1 = txtSN.Text.Trim(),
                //    ZDDM1 = scode

                //};
                //Procedure.run(sp3);
                PL_UPDATE_BOMSOTHTS3 sp4 = new PL_UPDATE_BOMSOTHTS3()
                {
                    SO1 = PlanSo,
                    JHDM1 = PlanCode,
                    ZDDM1 = scode

                };
                Procedure.run(sp4);

                string ChSql1 = "select comp ,qty  from RSTBOMQD_CRM where udesc='" + part2 + "' and zddm='" + scode + "' ";
                DataTable dt2 = dc.GetTable(ChSql1);

                if (dt2.Rows.Count > 0)
                {
                    string code = dt2.Rows[0][0].ToString();
                    string qty = dt2.Rows[0][1].ToString();
                    //iteam_code = iteam_code + '#' + code;
                    dr[i + 3] = code + "----" + qty;
                    //dr[i + 4] =  qty;

                }


            }

            Table1.Rows.Add(dr);
        }
        Session["rept3400table"] = Table1;
        ASPxGridView1.DataSource = Table1;
        ASPxGridView1.DataBind();

    }
    protected void btnXlsExport_Click(object sender, EventArgs e)
    {
        DataTable table1 = Session["rept3400table"] as DataTable;
        ASPxGridView1.DataSource = table1;
        ASPxGridView1.DataBind();
        ASPxGridViewExporter1.WriteXlsToResponse("乱序附装领料单导出");
    }
}
