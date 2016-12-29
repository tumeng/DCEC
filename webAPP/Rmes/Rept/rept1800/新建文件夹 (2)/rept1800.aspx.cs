using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Procedures;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.Pub.Data;
using Rmes.Web.Base;
using System.Data;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using System.Drawing;
using System.Drawing.Text;
/**
 * 功能概述：装机清单预览
 * 作者：游晓航
 * 创建时间：2016-09-12
 */

public partial class Rmes_Rept_rept1800_rept1800 : Rmes.Web.Base.BasePage
{
    private dataConn dc = new dataConn();
    PubCs thePubCs = new PubCs();
    //public DateTime theBeginDate, theEndDate;
    public string theCompanyCode;
    private string theUserId, theUserCode, MachineName;
    public string theProgramCode;
    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theUserId = theUserManager.getUserId();
        theProgramCode = "rept1800";
        MachineName = System.Net.Dns.GetHostName();
        //MachineName = "EATPU-Z670";  
        //测试数据：SO=SO11223 ,站点代码：z670
        if (!IsPostBack)
        {
            initCode();
            //for (int i = 0; i < ASPxGridView2.Rows.Count; i++)
            //{
            //    if (ASPxGridView2.Rows[i].Cells[5].Text == "批准")
            //    {
            //        //将当行设置为红色

            //        ASPxGridView2.GetRow[0][].Rows[i].Cells[5].BackColor = System.Drawing.Color.Red;
            //    }
            //}

        }

        string Site = "", Flag = "", So = "";

        if (Request["opFlag"] == "getEditSeries2")
        {
            string str1 = "";
            //string so = Request["SO"].ToString().Trim().ToUpper();
            string plancode = Request["PLANCODE"].ToString().Trim();
            string sql = "SELECT CUSTOMER_NAME,PLAN_QTY,ROUNTING_SITE,LQ_FLAG,PLAN_SO FROM DATA_PLAN WHERE PLAN_CODE='" + plancode + "' ";
            DataTable dt = dc.GetTable(sql);
            str1 = dt.Rows[0][0].ToString();
            Site = dt.Rows[0][2].ToString();
            Flag = dt.Rows[0][3].ToString();
            So = dt.Rows[0][4].ToString();

            str1 = str1 + "@" + Site + "@" + Flag + "@" + So;
            this.Response.Write(str1);
            this.Response.End();
        }
        if (Request["opFlag"] == "openPic")
        {//装机图片用$隔开的
            string str1 = "", sql3 = "";
            string sname = Request["SNAME"].ToString().Trim().ToUpper();
            string pcode = Request["PCODE"].ToString().Trim();
            string flag = Request["FLAG"].ToString().Trim();
            string getScode = "SELECT STATION_CODE FROM CODE_STATION WHERE STATION_NAME='" + sname + "'";
            string Scode = dc.GetValue(getScode);
            if (pcode != "L" && flag != "L")
            {
                sql3 = "select  a.wjpath from rstbomts_MWS a   where a.zdDM='" + Scode + "' AND a.MACHINENAME='" + MachineName + "' AND a.wjpath is not null AND a.GWdm NOT IN (SELECT REAL_LOCATION FROM MS_ISSUE_LOCATION WHERE ISSUE_GZDD='L') order by a.gwdm";
            }
            else
            {
                sql3 = "select  a.wjpath from rstbomts_MWS a    where a.zdDM='" + Scode + "' AND a.MACHINENAME='" + MachineName + "' AND a.wjpath is not null order by a.gwdm";

            }
            //string sql = "select  wjpath from rstbomts_MWS a left join rstbomts b on a.gwdm=b.gwdm and a.czts=b.czts and a.zddm=b.zddm where a.zdDM='" + Scode + "' AND a.MACHINENAME='" + MachineName + "' AND a.GWdm NOT IN (SELECT REAL_LOCATION FROM MS_ISSUE_LOCATION WHERE ISSUE_GZDD='L') order by a.gwdm";
            //string path = dc.GetValue(sql);
            DataTable dt = dc.GetTable(sql3);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string path = dt.Rows[i][0].ToString();
                str1 += path + "$";
            }
            this.Response.Write(str1);
            this.Response.End();
        }
        setCondition();

    }
    private void initCode()
    {
        //初始化下拉列表
        string sql = "select distinct a.pline_code,b.pline_name from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'";
        SqlCode.SelectCommand = sql;
        SqlCode.DataBind();
        
    }
    private void setCondition()
    {
        string SCode = "";
        string sql = "SELECT STATION_CODE FROM CODE_STATION WHERE STATION_NAME='" + txtSName.Text.Trim().ToUpper() + "'";
        DataTable dt = dc.GetTable(sql);
        if (dt.Rows.Count > 0)
        {
            SCode = dt.Rows[0][0].ToString();
        }
       
        string sql3 = "",sql4="" ;
        if (txtPCode.Text.Trim() != "")
        {
            if (txtPCode.Value.ToString() != "L" && txtFlag.Text.Trim() != "L")
            {
                sql3 = "select '★'|| a.CZTS as CZTS,b.note_color,b.note_font,a.gwdm,a.zddm from rstbomts_MWS a left join rstbomts b on a.gwdm=b.gwdm and a.czts=b.czts and a.zddm=b.zddm where a.zdDM='" + SCode + "' AND a.MACHINENAME='" + MachineName + "' AND a.GWdm NOT IN (SELECT REAL_LOCATION FROM MS_ISSUE_LOCATION WHERE ISSUE_GZDD='L') order by a.gwdm";
            }
            else
            {
                sql3 = "select '★'|| a.CZTS as CZTS,b.note_color,b.note_font,a.gwdm,a.zddm from rstbomts_MWS a left join rstbomts b on a.gwdm=b.gwdm and a.czts=b.czts and a.zddm=b.zddm   where a.zdDM='" + SCode + "' AND a.MACHINENAME='" + MachineName + "' order by a.gwdm";

            }
            DataTable dt3 = dc.GetTable(sql3);
           
            ASPxGridView2.DataSource = dt3;
            ASPxGridView2.DataBind();

            if (txtPCode.Value.ToString() != "L" && txtFlag.Text.Trim() != "L")
            {
                //AND a.MACHINENAME='" + MachineName + "'
                sql4 = "select a.*, FUNC_GET_ITEMTYPE('" + txtPCode.Value.ToString() + "','" + SCode + "',a.gwmc,replace(a.comp,'#','')) item_type ,func_get_itemclass('" + txtPCode.Value.ToString() + "','','',a.comp) item_class"
                + " from rstbomqd_MWS a   where a.zdDM='" + SCode + "' AND a.MACHINENAME='" + MachineName + "' AND  a.GWMC NOT IN (SELECT REAL_LOCATION FROM MS_ISSUE_LOCATION WHERE ISSUE_GZDD='L') order by a.gwmc,a.gxmc";
            }
            else
            {
                sql4 = "select a.*, FUNC_GET_ITEMTYPE('" + txtPCode.Value.ToString() + "','" + SCode + "',a.gwmc,replace(a.comp,'#','')) item_type ,func_get_itemclass('" + txtPCode.Value.ToString() + "','','',a.comp) item_class"
                +" from rstbomqd_MWS a   where zdDM='" + SCode + "' AND MACHINENAME='" + MachineName + "' order by a.gwmc,b.gxmc";

            }
            DataTable dt4 = dc.GetTable(sql4);
            ASPxGridView1.DataSource = dt4;
            ASPxGridView1.DataBind();
        }
    }
    protected void ASPxGridView1_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {    
        if (e.VisibleIndex < 0) return;
        string item_class_code = e.GetValue("ITEM_CLASS").ToString();
        string replace_flag = e.GetValue("REPLACE_FLAG").ToString();
        string vendercode = e.GetValue("GYSMC").ToString();

        switch (item_class_code)//重要件显示绿色
        {
            case "1":
                e.Row.BackColor = Color.LimeGreen;
                break;
            case "2":
                e.Row.BackColor = Color.LimeGreen;
                break;
            case "3":
                e.Row.BackColor = Color.LimeGreen;
                break;
            case "4":
                e.Row.BackColor = Color.LimeGreen;
                break;
            case "5":
                e.Row.BackColor = Color.LimeGreen;
                break;
            default:
                break;
        }
        if (replace_flag == "A")
        {
            e.Row.BackColor = Color.Yellow;//替换零件显示黄色
        }
        if (replace_flag == "B" || replace_flag == "Y")
        {
            e.Row.BackColor = Color.Orange;//临时措施替换零件显示橘黄色
        }
        if (vendercode != "")
        {
            e.Row.BackColor = Color.Red;//指定供应商零件显红色
        }
        //if (item_type == "A" || item_type == "B")
        //{
        //    e.Row.Cells[0].BackColor = System.Drawing.Color.Magenta  ;
        //}
        
    }
    protected void ASPxGridView2_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.VisibleIndex < 0) return;
        if (e.GetValue("NOTE_COLOR").ToString() != "")
        {
            e.Row.ForeColor = Color.FromName(e.GetValue("NOTE_COLOR").ToString());
        }
        if (e.GetValue("NOTE_FONT").ToString() != "")
        {
            e.Row.Font.Size = FontUnit.Point(Convert.ToInt32(e.GetValue("NOTE_FONT").ToString()));
        }
       
        
    }
    protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        string SCode = "";
        string sql = "SELECT STATION_CODE FROM CODE_STATION WHERE STATION_NAME='" + txtSName.Text.Trim().ToUpper() + "'";
        DataTable dt = dc.GetTable(sql);
        if (dt.Rows.Count > 0)
        {
            SCode = dt.Rows[0][0].ToString();
        }
        string xlh = dc.GetValue("select GET_FDJXL('" + txtSO.Text.ToUpper() + "') from dual");
        string Oldplan = txtPlanCode.Text.Trim();
        string sql2 = "select jhmcold from atpujhsn where jhmc='" + txtPlanCode.Text.Trim() + "'";
        DataTable dt2 = dc.GetTable(sql2);
        if (dt2.Rows.Count > 0)
        {
            Oldplan = dt2.Rows[0][0].ToString();
        }
        if (txtPCode.Text.Trim() != "")
        {
            PL_QUERY_BOMZJTS_MWS sp = new PL_QUERY_BOMZJTS_MWS()
            {
                SO1 = txtSO.Text.Trim().ToUpper(),
                ZDDM1 = SCode,
                GZDD11 = txtPCode.Value.ToString(),
                FDJXL1 = xlh,
                JHDM1 = Oldplan,
                MACHINENAME1 = MachineName
            };
            Procedure.run(sp);
            PL_UPDATE_BOMZJTS_MWS sp2 = new PL_UPDATE_BOMZJTS_MWS()
            {
                SO1 = txtSO.Text.Trim().ToUpper(),
                ZDDM1 = SCode,
                JHDM1 = Oldplan,
                GZDD11 = txtPCode.Value.ToString(),
                MACHINENAME1 = MachineName
            };
            Procedure.run(sp2);
            PL_UPDATE_BOMSOTHTS_MWS sp3 = new PL_UPDATE_BOMSOTHTS_MWS()
            {
                SO1 = txtSO.Text.Trim().ToUpper(),
                ZDDM1 = SCode,
                JHDM1 = Oldplan,
                MACHINENAME1 = MachineName
            };
            Procedure.run(sp3);
        }
        setCondition();
        ASPxGridView1.Selection.UnselectAll();

    }
    protected void ASPxCallbackPanel4_Callback(object sender, CallbackEventArgsBase e)
    {
        if (txtPCode.Text.Trim() != "" && txtSO.Text.Trim()!="")
        {
            string sql = "SELECT PLAN_CODE FROM DATA_PLAN WHERE PLAN_SO='" + txtSO.Text.Trim().ToUpper() + "' and PLINE_CODE='" + txtPCode.Value.ToString() + "' order by  BEGIN_DATE DESC";
            SqlPCode.SelectCommand = sql;
            SqlPCode.DataBind();
        }
        else if (txtPCode.Text.Trim() != "")
        {
            string sql = "SELECT PLAN_CODE FROM DATA_PLAN WHERE begin_date>=sysdate-30  and begin_Date<=sysdate and PLINE_CODE='" + txtPCode.Value.ToString() + "' order by BEGIN_DATE DESC  ";
            SqlPCode.SelectCommand = sql;
            SqlPCode.DataBind();

        }

    }
    protected void ASPxListBoxUsed_Callback(object sender, CallbackEventArgsBase e)
    {

        setCondition();

    }

    protected void ASPxGridView1_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.VisibleIndex < 0) return;
        string item_type = e.GetValue("ITEM_TYPE").ToString();
        //需要扫描的零件在零件号这一列显示洋红色
        if (e.DataColumn.FieldName.ToString() == "COMP")
        {
            if (item_type == "A" || item_type == "B")
            {
                e.Cell.BackColor = System.Drawing.Color.Magenta;
            }
            return;
        }
    }

    
   

}
