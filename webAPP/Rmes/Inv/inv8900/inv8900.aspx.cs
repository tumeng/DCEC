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
using Rmes.DA.Base;
using Rmes.DA.Procedures;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using System.Net;
/**
 * 功能概述：报表
 * 作者：游晓航
 * 创建时间：2016-08-09
 */

public partial class Rmes_Inv_inv8900_inv8900 : Rmes.Web.Base.BasePage
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
        theProgramCode = "inv8900";
        string hostIPAddress = Page.Request.UserHostAddress;
        //IPHostEntry hostInfo = System.Net.Dns.GetHostByAddress(hostIPAddress);
        MachineName = hostIPAddress;
        //MachineName = System.Net.Dns.GetHostName();
        if (!IsPostBack)
        {
            ASPxDateEdit1.Date = DateTime.Now;
            ASPxDateEdit2.Date = DateTime.Now.AddDays(1);
           
            
        }
        initCode();


    }
    private void initCode()
    {
        //初始化下拉列表
        string sql = "select distinct a.pline_code,b.pline_name from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'";
        SqlCode.SelectCommand = sql;
        SqlCode.DataBind();
        //txtPCode.SelectedIndex = txtPCode.Items.Count >= 0 ? 0 : -1;
        

    }
    private void setCondition()
    {
        
        string m_YearStr, m_MonthStr, m_FromrqStr, m_TorqStr, m_MonthDateStr, m_SO;
        m_YearStr = ASPxDateEdit1.Date.Year.ToString();
        m_MonthStr = ASPxDateEdit2.Date.Month.ToString();
   
        m_FromrqStr =  ASPxDateEdit1.Text.Trim() ;
        m_TorqStr =  ASPxDateEdit2.Text.Trim() ;

        if (ASPxDateEdit1.Date.AddDays(31) < ASPxDateEdit2.Date)
         {
             ASPxGridView1.JSProperties.Add("cpCallbackName", "Fail");
             ASPxGridView1.JSProperties.Add("cpCallbackRet", "选择日期范围不能超过31天，请重新选择！");
             return;
         }

         string sql = "DELETE FROM DP_REPORTSOTEMP WHERE MACHINENAME='" + MachineName + "'";
         dc.ExeSql(sql);
         string Insql1 = " insert into dp_reportsotemp(so,Machinename) select distinct so,'" + MachineName + "' from dp_monthkcb where year='" + m_YearStr + "' and month='" + m_MonthStr + "' and periodline='Begin' and gzdd='" + txtPCode.Value.ToString() + "'";
         dc.ExeSql(Insql1);
         string ChSql = "select  monthdate from dp_monthkcb where year='" + m_YearStr + "' and month='" + m_MonthStr + "' and periodline='Begin' and gzdd='" + txtPCode.Value.ToString() + "' order by monthdate desc";
         DataTable dt = dc.GetTable(ChSql);
         if (dt.Rows.Count <= 0)
         {
             ASPxGridView1.JSProperties.Add("cpCallbackName", "Fail");
             ASPxGridView1.JSProperties.Add("cpCallbackRet", "没有期初库存数据！");
             return;
         }
         else
         {
             string strMonD = dt.Rows[0][0].ToString();
              m_MonthDateStr = strMonD;
         }
         string Insql2 = " insert into dp_reportsotemp(so,Machinename) select distinct so,'" + MachineName + "' from dp_rckwcb where gzrq>='" + m_MonthDateStr + "' and gzrq<='" + m_TorqStr + "' and gzdd='" + txtPCode.Value.ToString() + "' and so not in (select so from dp_reportsotemp where MachineName='" + MachineName + "')";
         dc.ExeSql(Insql2);
         string ChSql2 = "select SO from dp_reportsotemp where machinename='" + MachineName + "' order by So";
         DataTable dt2 = dc.GetTable(ChSql2);
         DataTable Table = new DataTable();
         Table.Columns.Add("SO");
         Table.Columns.Add("QCKC");
         Table.Columns.Add("RK1");
         Table.Columns.Add("CK1");
         Table.Columns.Add("RK0");
         Table.Columns.Add("RK2");
         Table.Columns.Add("RK3");
         Table.Columns.Add("RK4");
         Table.Columns.Add("RK5");
         Table.Columns.Add("RK6");
         Table.Columns.Add("RK7");
         Table.Columns.Add("RK8");
         Table.Columns.Add("RK9");
         Table.Columns.Add("RK10");
         Table.Columns.Add("RKHJ");
         Table.Columns.Add("WXK");
         Table.Columns.Add("SYK");
         Table.Columns.Add("QT");
         Table.Columns.Add("CKHJ");
         Table.Columns.Add("KC");
 
         Table.TableName = "报表";
         for (int i = 0; i < dt2.Rows.Count; i++)
         {
             m_SO = dt2.Rows[i][0].ToString();
             string outstr;
             PL_CREATE_REPORT sp = new PL_CREATE_REPORT()
             {
                 SO1 = m_SO,
                 YEAR1 = m_YearStr,
                 MONTH1 = m_MonthStr,
                 GZDD1=txtPCode.Value.ToString(),
                 FROMRQ1=m_FromrqStr,
                 TORQ1=m_TorqStr,
                 MONTHDATE1 = m_MonthDateStr,
                 OUTSTR1=""
             };
             Procedure.run(sp);
             //ProductDataFactory.PL_CREATE_REPORT(m_SO, m_YearStr, m_MonthStr, txtPCode.Value.ToString(), m_FromrqStr, m_TorqStr, m_MonthDateStr, out outstr);
             outstr = sp.OUTSTR1;
             string[] param = outstr.Split('#');
             if (param.Length < 19)
             {
                 ASPxGridView1.JSProperties.Add("cpCallbackName", "Fail");
                 ASPxGridView1.JSProperties.Add("cpCallbackRet", "查询失败！");
                 return;

             }
             else
             {
                 Table.Rows.Add(m_SO, param[0], param[1], param[2], param[3], param[4], param[5], param[6], param[7], param[8], param[9], param[10], param[11], param[12], param[13], param[14], param[15], param[16], param[17], param[18]);
             }
              
 
         }

         ASPxGridView1.DataSource = Table;
         ASPxGridView1.DataBind();


    }
    protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        setCondition();
        ASPxGridView1.Selection.UnselectAll();

    }

}
