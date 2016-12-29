using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxClasses;
using Rmes.Pub.Data;
using Rmes.DA.Base;
/**
 * 功能概述：重要零件装配查询
 * 作者：游晓航
 * 创建时间：2016-09-11
 */

public partial class Rmes_rept1500 : Rmes.Web.Base.BasePage
{
    private dataConn dc = new dataConn();
    private PubCs pc = new PubCs();
    public string theCompanyCode, theUserId;
    public string theProgramCode;
    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theUserId = theUserManager.getUserId();
        theProgramCode = "rept1500";
        initCode();
        setCondition();
        

        if (!IsPostBack)
        {
            //ASPxDateEdit1.Date = DateTime.Now;
            //ASPxDateEdit2.Date = DateTime.Now.AddDays(1);
            ASPxDateEdit3.Date = DateTime.Now;
            ASPxDateEdit4.Date = DateTime.Now;//.AddDays(1);
            //ASPxDateEdit5.Date = DateTime.Now;
            //ASPxDateEdit6.Date = DateTime.Now.AddDays(1);
            
        }
        if (Request["opFlag"] == "getEditSeries")
        {
            string str1 = "", Sql3 = "";

            //string chose = Request["CHOSE"].ToString().Trim();
            string chose = System.Web.HttpUtility.UrlDecode(Request["CHOSE"]);
            if (chose != "null")
            {
                Sql3 = "select distinct DETECT_NAME from code_detect where  DETECT_NAME like '%" + chose + "%' ";
            }
            else
            {
                Sql3 = "select distinct DETECT_NAME from code_detect  where  DETECT_NAME like '%%'";

            }
            if (txtPCode2.Text.Trim() != "")
            {
                Sql3 = Sql3 + " and pline_code='" + txtPCode2.Value.ToString() + "'";
            }
            else
            {
                Sql3 = Sql3 + " and pline_code in ( SELECT PLINE_ID FROM VW_USER_ROLE_PROGRAM WHERE USER_ID='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "')  ";
            }

            Sql3 = Sql3 + " order by DETECT_NAME";
            Session["rept1500"] = Sql3;
            DataTable dt3 = dc.GetTable(Sql3);
            ASPxListBoxUnused.DataSource = dt3;
            ASPxListBoxUnused.DataBind();

            this.Response.Write(chose);
            this.Response.End();
        }
    }
    private void initCode()
    {
        //初始化下拉列表
        string sql = "select distinct a.pline_code,b.pline_name from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'";
        //SqlCode.SelectCommand = sql;
        //SqlCode.DataBind();
        //SqlCode1.SelectCommand = sql;
        //SqlCode1.DataBind();
        SqlCode2.SelectCommand = sql;
        SqlCode2.DataBind();
        //txtPCode.SelectedIndex = txtPCode.Items.Count >= 0 ? 0 : -1;
        //txtPCode1.SelectedIndex = txtPCode.Items.Count >= 0 ? 0 : -1;
        //txtPCode2.SelectedIndex = txtPCode.Items.Count >= 0 ? 0 : -1;
         
        string Sql2 = "  select pt_part from copy_pt_mstr order by pt_part";
        SqlICode.SelectCommand = Sql2;
        SqlICode.DataBind();

        string Sql3 = "select distinct DETECT_NAME from code_detect where   ";
        if (txtPCode2.Text.Trim() != "")
        {
            Sql3 = Sql3 + "  pline_code='" + txtPCode2.Value.ToString() + "'";


        }
        else
        {
            Sql3 = Sql3 + "  pline_code in ( SELECT PLINE_ID FROM VW_USER_ROLE_PROGRAM WHERE USER_ID='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "')  ";
        }
        Sql3 = Sql3 + " order by DETECT_NAME";
        DataTable dt3 = dc.GetTable(Sql3);
        ASPxListBoxUnused.DataSource = dt3;
        ASPxListBoxUnused.DataBind();
        
         
    }
   
    protected void setCondition()
    {
        //if (txtICode.Text.Trim() != "")
        //{
        //    string sql = "select distinct A.SN ,B.PLAN_SO ,A.DETECT_VALUE   from data_sn_detect_data A,data_product B where A.SN=B.SN AND A.PLINE_CODE='" + txtPCode.Value.ToString() + "' AND A.DETECT_NAME like '%" + txtIKind.Text.Trim() + "%' and A.DETECT_VALUE like '%" + txtICode.Text.Trim() + "%'  ";
        //    DataTable dt = dc.GetTable(sql);

        //    ASPxGridView1.DataSource = dt;
        //    ASPxGridView1.DataBind();
        //}
        //if (txtPartCode.Text.Trim() != "")
        //{
        //    string chsql2 = "select * from  copy_pt_mstr where pt_part='" + txtPartCode.Text.Trim() + "'";
        //    DataTable ch2 = dc.GetTable(chsql2);
        //    if (ch2.Rows.Count <= 0)
        //    {
        //        return;
        //    }
        //    string sql2 = " select a.*,b.user_name from VW_DATA_SN_BOM a left join code_user b on a.create_userid=b.user_code where CREATE_TIME>=to_date('" + ASPxDateEdit1.Text.Trim() + "','yyyy-mm-dd hh24:mi:ss')"
        //                 +"   and CREATE_TIME<=to_date('" + ASPxDateEdit2.Text.Trim() + "','yyyy-mm-dd hh24:mi:ss') and pline_code='" + txtPCode1.Value.ToString() + "' and  item_code='" + txtPartCode.Text.Trim() + "' order by sn";
        //    DataTable dt2 = dc.GetTable(sql2);
        //    ASPxGridView2.DataSource = dt2;
        //    ASPxGridView2.DataBind();
        //}
        if (txtPCode2.Text.Trim() != "")
        {
            DataTable Table1 = new DataTable();
            
            Table1.Columns.Add("SO");
            Table1.Columns.Add("流水号");
            Table1.Columns.Add("时间");
            Table1.Columns.Add("站点");
            string planso = "", sn = "", wtime = "", scode = "", flag = "";
            //获取站点代码
            string chsql2 = "select station_code from code_station where station_name='" + txtStation.Text.Trim().ToUpper() + "'";
            scode = dc.GetValue(chsql2);
            //
            string ChSql = "select DISTINCT a.PLAN_SO,a.SN,a.WORK_TIME,c.complete_flag from data_product a ,data_plan b,data_complete c  where a.plan_code=b.plan_code and a.sn=c.sn and c.station_code like '%"+scode+"%' and a.WORK_DATE>=to_date('" + ASPxDateEdit3.Text.Trim() + "', 'yyyy-mm-dd hh24:mi:ss') "
                         +" and a.WORK_DATE<=to_date('" + ASPxDateEdit4.Text.Trim() + "', 'yyyy-mm-dd hh24:mi:ss') and b.plan_type<>'C' and b.plan_type<>'D'  and a.PLINE_CODE='" + txtPCode2.Value.ToString() + "' order by a.sn";
            DataTable Chdt = dc.GetTable(ChSql);
            for (int i = 0; i < Chdt.Rows.Count; i++)
            {
                planso = Chdt.Rows[i][0].ToString();
                sn = Chdt.Rows[i][1].ToString();
                wtime = Chdt.Rows[i][2].ToString();
                flag = Chdt.Rows[i][3].ToString();
                DataRow dr1 = Table1.NewRow();
                dr1[0] = planso;
                dr1[1] = sn;
                dr1[2] = wtime;
                
                for (int j = 0; j < ASPxListBoxUsed.Items.Count; j++)
                {
                    string sj1 = "", ZDMC = "", ChSql2 = "";
                    string part2 = ASPxListBoxUsed.Items[j].ToString();
                    if (i == 0)
                    {
                        Table1.Columns.Add(part2);
                    }
                    if (flag == "0")
                    {
                        ChSql2 = "Select a.DETECT_VALUE,a.STATION_NAME from data_sn_detect_data_TEMP a  where  a.sn='" + sn + "' and a.DETECT_NAME='" + part2 + "' and a.station_name like '%" + txtStation.Text.Trim().ToUpper() + "%'";
                         
                    }
                    else
                    {
                        ChSql2 = "Select a.DETECT_VALUE,a.STATION_NAME from data_sn_detect_data a  where  a.sn='" + sn + "' and a.DETECT_NAME='" + part2 + "' and a.station_name like '%" + txtStation.Text.Trim().ToUpper() + "%'";
                        
 
                    }
                    DataTable Chdt2 = dc.GetTable(ChSql2);
                    if (Chdt2.Rows.Count > 0)
                    {
                       sj1 = Chdt2.Rows[0][0].ToString();
                       ZDMC = Chdt2.Rows[0][1].ToString(); 
                    }
                    
                    dr1[3] = ZDMC;
                    dr1[j + 4] = sj1;
                    
                }
                Table1.Rows.Add(dr1);
            }
            
            ASPxGridView3.DataSource = Table1;
            ASPxGridView3.DataBind();
        }
        //if (txtStation.Text.Trim() != "")
        //{
        //    DataTable Table2 = new DataTable();
        //    DataRow dr2 = Table2.NewRow();
        //    Table2.Columns.Add("SO");
        //    Table2.Columns.Add("流水号");
        //    Table2.Columns.Add("时间");
        //    Table2.Columns.Add("员工");
        //    Table2.Columns.Add("项目");
        //    Table2.Columns.Add("数值");
        //    string Sname = "", so = "", sn2 = "", time = "", Uname = "", barcode = "", pt = "", icode = "", isn = "", vcode = "";
        //    string sql4 = "Select station_name from code_station where station_name='" + txtStation.Text.Trim() + "'";
        //    DataTable dt4 = dc.GetTable(sql4);
        //    if (dt4.Rows.Count <= 0)
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        Sname = dt4.Rows[0][0].ToString();
        //    }
        //    string sql5 = " select a.plan_so,a.sn,a.start_time,b.user_name from data_complete a,code_user b,code_station c  where a.user_id=b.user_code and a.station_code=c.station_code "
        //              +" and a.start_time>=to_date('" + ASPxDateEdit5.Text.Trim() + "','yyyy-mm-dd hh24:mi:ss') and a.start_time<=to_date('" + ASPxDateEdit6.Text.Trim() + "','yyyy-mm-dd hh24:mi:ss') and c.station_name='" + txtStation.Text.Trim().ToUpper() + "' order by a.sn ";
        //    DataTable dt5 = dc.GetTable(sql5);
        //    for (int i = 0; i < dt5.Rows.Count; i++)
        //    {
        //        so = dt5.Rows[i][0].ToString();
        //        sn2 = dt5.Rows[i][1].ToString();
        //        time = dt5.Rows[i][2].ToString();
        //        Uname = dt5.Rows[i][3].ToString();
        //        dr2[0] = so;
        //        dr2[1] = sn2;
        //        dr2[2] = time;
        //        dr2[3] = Uname;
        //        string sql6 = " Select a.item_code,a.vendor_code,a.item_sn,b.pt_desc2 from vw_data_sn_bom a,copy_pt_mstr b where a.sn='" + sn2 + "' and a.station_name='" + txtStation.Text.Trim() + "' and a.item_code=b.pt_part";
        //        DataTable dt6 = dc.GetTable(sql6);
        //        if (dt6.Rows.Count > 0)
        //        {
        //            icode = dt6.Rows[0][0].ToString();
        //            vcode = dt6.Rows[0][1].ToString();
        //            isn = dt6.Rows[0][2].ToString();
        //            pt = dt6.Rows[0][3].ToString();
        //            barcode = icode + '^' + vcode + '^' + isn;
        //        }
        //        dr2[4] = barcode;
        //        dr2[5] = pt;
                 
        //    }
        //    Table2.Rows.Add(dr2);
        //    ASPxGridView4.DataSource = Table2;
        //    ASPxGridView4.DataBind();
        //}
        


    }
    protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        //if (txtICode.Text.Trim() == "")
        //{
        //    ASPxGridView1.JSProperties.Add("cpCallbackName", "Fail");
        //    ASPxGridView1.JSProperties.Add("cpCallbackRet", "请输入要查询的零件号！");
        //    return;

        //}
        setCondition();
    }
    protected void ASPxGridView2_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        //if (txtPartCode.Text.Trim() == "")
        //{
        //    ASPxGridView2.JSProperties.Add("cpCallbackName", "Fail");
        //    ASPxGridView2.JSProperties.Add("cpCallbackRet", "请输入要查询的零件号！");
        //    return;
        //}
        //string chsql2 = "select * from  copy_pt_mstr where pt_part='" + txtPartCode.Text.Trim() + "'";
        //DataTable ch2 = dc.GetTable(chsql2);
        //if (ch2.Rows.Count <= 0)
        //{
        //    ASPxGridView2.JSProperties.Add("cpCallbackName", "Fail");
        //    ASPxGridView2.JSProperties.Add("cpCallbackRet", "输入的零件号不存在！");
        //    return;
        //}
        setCondition();
    }
    protected void ASPxGridView3_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        DataTable Table1 = new DataTable();
        ASPxGridView3.DataSource = Table1;
        ASPxGridView3.DataBind();
        setCondition();
    }
    protected void ASPxGridView4_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        //if (txtStation.Text.Trim() == "")
        //{
        //    ASPxGridView4.JSProperties.Add("cpCallbackName", "Fail");
        //    ASPxGridView4.JSProperties.Add("cpCallbackRet", "请输入站点代码！");
        //    return;
        //}
        //string sql4 = "Select station_name from code_station where station_name='" + txtStation.Text.Trim() + "'";
        //DataTable dt4 = dc.GetTable(sql4);
        //if (dt4.Rows.Count <= 0)
        //{
        //    ASPxGridView4.JSProperties.Add("cpCallbackName", "Fail");
        //    ASPxGridView4.JSProperties.Add("cpCallbackRet", "该站点不存在，请确认是否输入正确！");
        //    return;
        //}
        setCondition();
    }
 
    protected void ASPxCallbackPanel4_Callback(object sender, CallbackEventArgsBase e)
    {
        try
        {
            if (Session["rept1500"].ToString() != "")
            {
                string Sql3 = Session["rept1500"] as string;
                DataTable dt3 = dc.GetTable(Sql3);
                ASPxListBoxUnused.DataSource = dt3;
                ASPxListBoxUnused.DataBind();
            }
            else
            {
                string Sql3 = "select  distinct DETECT_NAME from code_detect where   ";
                if (txtPCode2.Text.Trim() != "")
                {
                    Sql3 = Sql3 + "  pline_code='" + txtPCode2.Value.ToString() + "'";
                }
                else
                {
                    Sql3 = Sql3 + "  pline_code in ( SELECT PLINE_ID FROM VW_USER_ROLE_PROGRAM WHERE USER_ID='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "')  ";
                }
                DataTable dt3 = dc.GetTable(Sql3);
                ASPxListBoxUnused.DataSource = dt3;
                ASPxListBoxUnused.DataBind();
            }
        }
        catch
        { }

    }
    
   



}
