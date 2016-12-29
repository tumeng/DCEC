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
using DevExpress.Web.ASPxGridLookup;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxClasses;
using Rmes.Pub.Data;
using Rmes.DA.Base;
using DevExpress.Utils;
/**
 * 功能概述：现场待处理物料接收
 * 作者：杨少霞
 * 创建时间：2016-11-11
 */

public partial class Rmes_part2700 : Rmes.Web.Base.BasePage
{
    private dataConn dc = new dataConn();
    private PubCs pc = new PubCs();
    public string theCompanyCode, theUserId, theUserCode, theUserName;
    public string theProgramCode;

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();

        theProgramCode = "part2700";
        theUserId = theUserManager.getUserId();
        theUserCode = theUserManager.getUserCode();
        theUserName = theUserManager.getUserName();

        //初始查询条件生产线
        string Sql = " select distinct a.pline_code,b.pline_name as showtext from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "' ";
        comPLQ.DataSource = dc.GetTable(Sql);
        comPLQ.DataBind();

        //初始查询条件状态
        string SqlStatus = " select internal_code,internal_name from code_internal where internal_type_code='013' order by internal_code";
        comboStatusQ.DataSource = dc.GetTable(SqlStatus);
        comboStatusQ.DataBind();

        setCondition();

        if (!IsPostBack)
        {
            //comPLQ.SelectedIndex = comPLQ.Items.Count >= 0 ? 0 : -1;//
            StartDateQ.Date = DateTime.Now.AddDays(-1);
            EndDateQ.Date = DateTime.Now.AddDays(1);
        }
    }

    private void setCondition()
    {
        string selSql = "select a.location_code,a.material_code,a.gys_code,a.material_num,"
                      + "to_char(a.add_time,'YYYY-MM-DD HH24:MI:SS') add_time,b.in_qadc01,a.ygmc,a.reject_reason,"
                      + "a.handle_flag,a.handle_user,to_char(handle_time,'YYYY-MM-DD HH24:MI:SS') handle_time,"
                      + "a.take_user,to_char(a.take_time,'YYYY-MM-DD HH24:MI:SS') take_time,a.gzdd, "
                      + " c.internal_name status_name "
                      + "from ms_discard_mt a "
                      + " left join copy_in_mstr b on upper(a.material_code)=upper(b.in_part)  "
                      + " left join (select internal_code,internal_name from code_internal where internal_type_code='013') c on a.handle_flag=c.internal_code "
                      + "where 1=1 ";
        //生产线必选，否则没数据
        if (comPLQ.Value == null)
        {
            selSql += " AND a.GZDD = '' ";
        }
        else
        {
            selSql += " and a.gzdd='" + comPLQ.Value.ToString() + "'  ";//and upper(b.in_site)='" + gQadSite + "'
        }
        if (StartDateQ.Value != null)
        {
            selSql = selSql + " and ADD_TIME >= to_date('" + StartDateQ.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') ";
        }
        if (EndDateQ.Value != null)
        {
            selSql = selSql + " and ADD_TIME <= to_date('" + EndDateQ.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') ";
        }
        //zhuangtai
        if (comboStatusQ.Value == null)
        {
            selSql += " AND 1=1 ";
        }
        else
        {
            selSql += " and a.HANDLE_FLAG='" + comboStatusQ.Value.ToString() + "'  ";
        }

        selSql = selSql + " order by a.add_time,a.location_code";
        DataTable dt = dc.GetTable(selSql);
        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
    }

    public static bool IsInt(string str)
    {
        if (str == string.Empty)
            return false;
        try
        {
            Convert.ToInt32(str);
            return true;
        }
        catch
        {
            return false;
        }
    }

    protected void btnXlsExport_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter1.WriteXlsToResponse("现场待处理物料维护导出");
    }


    protected void btnQuery_Click(object sender, EventArgs e)
    {
        setCondition();
    }

    protected void ASPxGridView1_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        //取消
        if (e.ButtonID == "cancelSure")
        {
            string RunValue = ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "ADD_TIME").ToString();
            string gSite = ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "GZDD").ToString().ToUpper();

            string sSql = "update ms_discard_mt  set handle_flag='3',take_time=sysdate,take_user='" + theUserName + "' where add_time=to_date('" + RunValue + "','YYYY-MM-DD HH24:MI:SS') and  gzdd='" + gSite + "'  and handle_flag='N'";
            dc.ExeSql(sSql);

            setCondition();

        }
        //接收
        if (e.ButtonID == "takeSure")
        {
            string RunValue = ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "ADD_TIME").ToString();
            string gSite = ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "GZDD").ToString().ToUpper();

            string sSql = "update ms_discard_mt  set handle_flag='2',take_time=sysdate,take_user='" + theUserName + "' where add_time=to_date('" + RunValue + "','YYYY-MM-DD HH24:MI:SS') and  gzdd='" + gSite + "' and handle_flag='1'";
            dc.ExeSql(sSql);

            setCondition();

        }
        
    }
    public static void Show(System.Web.UI.Page page, string msg)
    {
        page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + msg.ToString() + "');</script>");
    }

    protected void ASPxGridView1_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.VisibleIndex < 0) return;

        string status = e.GetValue("HANDLE_FLAG").ToString(); //状态不同背景色
        if (status.Contains("1"))
        {
            e.Row.BackColor = System.Drawing.Color.Red;
        }
        if (status.Contains("3"))
        {
            e.Row.BackColor = System.Drawing.Color.Yellow;
        }
        if (status.Contains("N"))
        {
            e.Row.BackColor = System.Drawing.Color.LightPink;
        }

    }

    protected void ASPxGridView1_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;
        string flag = grid.GetRowValues(e.VisibleIndex, "HANDLE_FLAG") as string;
        
        if (flag == "N")
        {
            if (e.ButtonID == "takeSure")
            {
                e.Visible = DefaultBoolean.False;
            }
            if (e.ButtonID == "SURE1")
            {
                e.Visible = DefaultBoolean.False;
            }
            if (e.ButtonID == "SURE2")
            {
                e.Visible = DefaultBoolean.False;
            }
        }
        if (flag == "1")
        {
            if (e.ButtonID == "cancelSure")
            {
                e.Visible = DefaultBoolean.False; 
            }
            if (e.ButtonID == "SURE1")
            {
                e.Visible = DefaultBoolean.False;
            }
            if (e.ButtonID == "SURE2")
            {
                e.Visible = DefaultBoolean.False;
            }
        }
        if (flag == "2")
        {
            if (e.ButtonID == "takeSure")
            {
                e.Visible = DefaultBoolean.False;
            }
            if (e.ButtonID == "cancelSure")
            {
                e.Visible = DefaultBoolean.False;
            }
            if (e.ButtonID == "SURE1")
            {
                e.Enabled = false;
            }
            if (e.ButtonID == "SURE2")
            {
                e.Visible = DefaultBoolean.False;
            }
            
            
        }
        if (flag == "3")
        {
            if (e.ButtonID == "takeSure")
            {
                e.Visible = DefaultBoolean.False;
            }
            if (e.ButtonID == "cancelSure")
            {
                e.Visible = DefaultBoolean.False;
            }
            if (e.ButtonID == "SURE1")
            {
                e.Visible = DefaultBoolean.False;
            }
            if (e.ButtonID == "SURE2")
            {
                e.Enabled = false;
            }
            

        }
        
    }

}
