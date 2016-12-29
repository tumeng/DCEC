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
/**
 * 功能概述：库存待冲抵物料查询
 * 作者：杨少霞
 * 创建时间：2016-11-11
 */

public partial class Rmes_part2500 : Rmes.Web.Base.BasePage
{
    private dataConn dc = new dataConn();
    private PubCs pc = new PubCs();
    public string theCompanyCode, theUserId, theUserCode;
    public string theProgramCode;

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();

        theProgramCode = "part2500";
        theUserId = theUserManager.getUserId();
        theUserCode = theUserManager.getUserCode();

        //初始查询条件生产线
        string Sql = " select distinct a.pline_code,b.pline_name as showtext from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "' ";
        comPLQ.DataSource = dc.GetTable(Sql);
        comPLQ.DataBind();

        setCondition();
        
        if (!IsPostBack)
        {
            //comPLQ.SelectedIndex = comPLQ.Items.Count >= 0 ? 0 : -1;//
            StartDateQ.Date = DateTime.Now.AddDays(-10);
            EndDateQ.Date = DateTime.Now.AddDays(1);
        }
    }
    
    private void setCondition()
    {
        string selSql = "select a.material_code,a.gys_code,a.material_num,a.add_yh,to_char(a.ADD_TIME,'yyyy-mm-dd hh24:mi:ss') ADD_TIME,a.ready_flag,"
                       + "to_char(a.last_handle_time,'yyyy-mm-dd hh24:mi:ss') last_handle_time,a.last_handle_num,a.bill_code,a.gzdd,b.PT_DESC2,c.ad_name "
                       + " from ms_inv_mat a "
                       + " left join copy_pt_mstr b on a.material_code=b.pt_part "
                       + " left join copy_ad_mstr c on a.gys_code=c.ad_addr "
                       + "where 1=1 ";
        //生产线必选，否则没数据
        if (comPLQ.Value == null)
        {
            selSql += " AND a.GZDD = '' ";
        }
        else
        {
            selSql += " and a.gzdd='" + comPLQ.Value.ToString() + "' ";
        }
        if (TxtMatCodeQ.Text != "")
        {
            selSql = selSql + " and a.material_code like '" + TxtMatCodeQ.Text.Trim() + "%'";
        }
        if (txtGysCodeQ.Text != "")
        {
            selSql = selSql + " and a.gys_code like '" + txtGysCodeQ.Text.Trim() + "%'";
        }
        
        selSql = selSql + " order by 1,2";
        DataTable dt = dc.GetTable(selSql);
        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();

        string selSql2 = "select material_code,gys_code,cur_handle_num,to_char(CUR_HANDLE_TIME,'yyyy-mm-dd hh24:mi:ss') CUR_HANDLE_TIME,bill_code,gzdd "
                       + "from ms_inv_mat_log "
                       + "where 1=1 ";
        //生产线必选，否则没数据
        if (comPLQ.Value == null)
        {
            selSql2 += " AND GZDD = '' ";
        }
        else
        {
            selSql2 += " and gzdd='" + comPLQ.Value.ToString() + "' ";
        }
        if (TxtMatCodeQ.Text != "")
        {
            selSql2 = selSql2 + " and material_code like UPPER('%" + TxtMatCodeQ.Text.Trim() + "%') ";
        }
        if (txtGysCodeQ.Text != "")
        {
            selSql2 = selSql2 + " and gys_code like UPPER('%" + txtGysCodeQ.Text.Trim() + "%') ";
        }
        if (txtBillCodeQ.Text != "")
        {
            selSql2 = selSql2 + " and bill_code like UPPER('%" + txtBillCodeQ.Text.Trim() + "%') ";
        }
        if (StartDateQ.Value != null)
        {
            selSql2 = selSql2 + " and CUR_HANDLE_TIME >= to_date('" + StartDateQ.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') ";
        }
        if (EndDateQ.Value != null)
        {
            selSql2 = selSql2 + " and CUR_HANDLE_TIME <= to_date('" + EndDateQ.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') ";
        }
        selSql2 = selSql2 + " order by 1,2";
        DataTable dt2 = dc.GetTable(selSql2);
        ASPxGridView2.DataSource = dt2;
        ASPxGridView2.DataBind();

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
    
   

    protected void btnXlsExportPlan_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter1.WriteXlsToResponse("库存待冲抵物料导出");
    }
    

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        setCondition();
    }

    

}
