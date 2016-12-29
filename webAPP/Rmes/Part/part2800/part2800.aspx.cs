using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DevExpress.Web.ASPxGridLookup;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxEditors;

using Rmes.DA.Base;
using Rmes.DA.Procedures;
using Rmes.Pub.Data;
using Rmes.Web.Base;
using DevExpress.Web.ASPxGridView;
using DevExpress.Utils;
using System.Net;


public partial class Rmes_part2800 : Rmes.Web.Base.BasePage
{
        private dataConn dc = new dataConn();
        private PubCs pc = new PubCs();
        public string theCompanyCode, theProgramCode, theUserId, theUserCode, GV_MachineName;

        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();
            theUserCode = theUserManager.getUserCode();

            theProgramCode = "part2800";
            //GV_MachineName = GV_MachineName = System.Net.Dns.GetHostName();
            string hostIPAddress = Page.Request.UserHostAddress;
            //IPHostEntry hostInfo = System.Net.Dns.GetHostByAddress(hostIPAddress); 
            //MachineName = System.Web.HttpContext.Current.Request.UserHostAddress;
            GV_MachineName = hostIPAddress;

            string sql = "select a.pline_code,b.rmes_id,b.pline_name from VW_USER_ROLE_PROGRAM a "
                   + "left join code_product_line b on a.pline_code=b.pline_code "
                   + "where a.COMPANY_CODE = '" + theCompanyCode + "' and a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' order by b.PLINE_NAME";
            comboPlineCode.DataSource = dc.GetTable(sql);
            comboPlineCode.DataBind();

            if (!IsPostBack)
            {
                //ASPxGridView4.Visible = false;
                //ASPxGridView5.Visible = false;

                string dsql = "delete from DATA_SN_TEMP";
                dc.ExeSql(dsql);
            }

            //if (!IsPostBack)
            //{
            //    comboPlineCode.SelectedIndex = comboPlineCode.Items.Count >= 0 ? 0 : -1;

            //    string sql2 = "select PLAN_CODE from DATA_PLAN where PLINE_CODE='" + comboPlineCode.Value.ToString() + "' and BEGIN_DATE>sysdate-100 order by BEGIN_DATE,PLAN_CODE";
            //    comboPlanCode.DataSource = dc.GetTable(sql2);
            //    comboPlanCode.DataBind();
                
            //}
        }
        //初始化计划下拉列表
        protected void comboPlanCode_Callback(object sender, CallbackEventArgsBase e)
        {
            string pline = e.Parameter;

            string sql = "select PLAN_CODE from DATA_PLAN where PLINE_CODE='" + pline + "' and BEGIN_DATE>sysdate-120 order by BEGIN_DATE,PLAN_CODE";
                        //select jhmc from atpujhb where GZQY='" + gSite + "' and rqbegin>sysdate-100 order by rqbegin,jhdm----------VB
            DataTable dt = dc.GetTable(sql);

            ASPxComboBox comboPlan = sender as ASPxComboBox;
            comboPlan.DataSource = dt;
            comboPlan.ValueField = "PLAN_CODE";
            comboPlan.TextField = "PLAN_CODE";
            comboPlan.DataBind();
        }
        //初始化流水号列表
        protected void ASPxGridView1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] param = e.Parameters.Split(',');
            string plineCode = param[0];//生产线
            string planCode = param[1];//计划代码

            string sql = "select a.SN from DATA_PRODUCT a where a.PLINE_CODE='" + plineCode + "' and a.PLAN_CODE='" + planCode + "' "
                       +" and NOT EXISTS(SELECT * FROM data_sn_temp C WHERE C.sn=A.sn  )  order by a.SN";
                         //select ghtm from sjsxb where gzdd='" + gSite + "' and jhdm='" + thisJh + "' order by ghtm-------------VB
            DataTable dt = dc.GetTable(sql);

            ASPxGridView gridview1 = sender as ASPxGridView;
            gridview1.DataSource = dt;
            gridview1.DataBind();
            gridview1.Selection.UnselectAll();
        }
        protected void ASPxGridView2_CustomCallback1(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] param = e.Parameters.Split(',');
            string plineCode = param[0];//生产线
            string planCode = param[1];//计划代码

            string sql = "select SN from DATA_SN_TEMP order by SN";
            DataTable dt = dc.GetTable(sql);
            ASPxGridView gridview2 = sender as ASPxGridView;
            gridview2.DataSource = dt;
            gridview2.DataBind();
            gridview2.Selection.UnselectAll();
        }
    protected void ASPxCbSubmit1_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        int count = 0;

        string oFlag, pline, planCode, strSN;

        char[] charSeparators = new char[] { ',' };
        string[] collection = e.Parameter.Split(charSeparators);
        int cnt = collection.Length;

        oFlag = collection[0].ToString();
        pline = collection[1].ToString();
        planCode = collection[2].ToString();
        
        if (oFlag == "ADD")
        {
            //获取SN
            List<string> s = new List<string>();
            for (int i = 3; i < cnt; i++)
            {
                s.Add(collection[i].ToString());
            }
            string[] s1 = s.ToArray();

            if (ASPxGridView1.Selection.Count == 0)
            {
                e.Result = "Fail,请选择要增加的SN！";
                return;
            }
            for (int i = 0; i < s1.Length; i++)
            {
                strSN = s1[i].ToString();

                string inSqlTemp = "insert into DATA_SN_TEMP(PLINE_CODE,PLAN_CODE,SN,MACHINENAME,input_time) "
                                 + "values('" + pline + "','" + planCode + "','" + strSN + "','" + GV_MachineName + "',sysdate)";
                dc.ExeSql(inSqlTemp);
            }
            e.Result = "OK,增加成功！";
            return;
        }
        if (oFlag == "DELETE")
        {
            //获取工位
            List<string> s = new List<string>();
            for (int i = 3; i < cnt; i++)
            {
                s.Add(collection[i].ToString());
            }
            string[] s1 = s.ToArray();

            if (ASPxGridView2.Selection.Count == 0)
            {
                e.Result = "Fail,请选择要删除的SN！";
                return;
            }
            for (int i = 0; i < s1.Length; i++)
            {
                strSN = s1[i].ToString();

                string deSqlTemp = "DELETE FROM  DATA_SN_TEMP WHERE PLINE_CODE='" + pline + "' and PLAN_CODE ='" + planCode + "' "
                                 + " AND SN='" + strSN + "' AND MACHINENAME='" + GV_MachineName + "' ";
                dc.ExeSql(deSqlTemp);
            }
            e.Result = "OK,删除成功！";
            return;
        }
        if (oFlag == "CMDQRY")
        {
            //获取SN
            List<string> s = new List<string>();
            for (int i = 3; i < cnt; i++)
            {
                s.Add(collection[i].ToString());
            }
            string[] s1 = s.ToArray();

            if (ASPxGridView2.Selection.Count == 0)
            {
                e.Result = "Fail,请选择要计算的SN！";
                return;
            }
            string GhtmStr = "(";
            for (int i = 0; i < s1.Length; i++)
            {
                strSN = s1[i].ToString();
                GhtmStr = GhtmStr + "'" + strSN + "',";
                
            }
            GhtmStr = GhtmStr.Substring(0, GhtmStr.Length - 1).ToString();
            GhtmStr = GhtmStr + ")";

            string deSqlTemp = "delete from ms_lineside_mt_tmp where gzdd='" + pline + "' ";
            dc.ExeSql(deSqlTemp);
            string dSql = "delete from ms_online_mt_tmp where gzdd='" + pline + "'";
            dc.ExeSql(dSql);

            string inSql = "insert into ms_online_mt_tmp(material_code,gys_code,online_num,gzdd) "
                         + "select ljdm,nvl(gysdm,'TEMP'),sum(ljsl),gzdd from ms_lineside_mt_ghtm_bom_log "
                         + "where gzdd='" + pline + "' and hjbs='Y' and ghtm in " + GhtmStr + "  "
                         + "group by ljdm,gysdm,gzdd order by ljdm,gysdm";
            dc.ExeSql(inSql);

            string inSql2 = "insert into ms_lineside_mt_tmp(material_code,gys_code,lineside_num,gzdd) "
                         + "select material_code,nvl(gys_code,'TEMP'),sum(lineside_num),gzdd from ms_lineside_mt "
                         + "where gzdd='" + pline + "' "
                         + "group by material_code,gys_code,gzdd order by material_code,gys_code";
            dc.ExeSql(inSql2);

            e.Result = "OK,计算完成！";
            return;
        }
    }

    protected void CmdQry_Click(object sender, EventArgs e)
    {

    }

    protected void cmdOnline_Click(object sender, EventArgs e)
    {
        
    }

    protected void ASPxGridView3_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        string[] param = e.Parameters.Split(',');
        string plineCode = param[0];//生产线
        string planCode = param[1];//计划代码

        string gQadSite = "";
        string sSql = "select internal_name from code_internal where internal_code='" + plineCode + "' and internal_type_code='012'";
        dataConn dc1 = new dataConn(sSql);
        DataTable dt1 = dc1.GetTable();
        if (dt1.Rows.Count > 0)
        {
            gQadSite = dt1.Rows[0][0].ToString();
        }
        string sql = "select a.material_code,a.gys_code,b.in_qadc01,a.online_num from ms_online_mt_tmp a "
                  + " left join copy_in_mstr b on a.material_code=upper(b.in_part) and upper(b.in_site)='" + gQadSite + "' where a.gzdd='" + plineCode + "' "//upper(d.in_site)????????
                  + " order by a.material_code,a.gys_code ";
        Session["2800ONLINE"] = sql;
        
        DataTable dt = dc.GetTable(sql);

        ASPxGridView gridview3 = sender as ASPxGridView;
        gridview3.DataSource = dt;
        gridview3.DataBind();
        

    }

    protected void ASPxGridView4_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        ASPxGridView3.Visible = false;
        ASPxGridView5.Visible = false;
        
        string[] param = e.Parameters.Split(',');
        string plineCode = param[0];//生产线
        string planCode = param[1];//计划代码

        string gQadSite = "";
        string sSql = "select internal_name from code_internal where internal_code='" + plineCode + "' and internal_type_code='012'";
        dataConn dc1 = new dataConn(sSql);
        DataTable dt1 = dc1.GetTable();
        if (dt1.Rows.Count > 0)
        {
            gQadSite = dt1.Rows[0][0].ToString();
        }
        string sql = "select a.material_code,a.gys_code,b.in_qadc01,a.lineside_num from ms_lineside_mt_tmp a "
                  + "left join copy_in_mstr b on a.material_code=upper(b.in_part)  and upper(b.in_site)='" + gQadSite + "' where a.gzdd='" + plineCode + "'  "//upper(d.in_site)????????
                  + " order by a.material_code,a.gys_code ";
        Session["2800LINESIDE"] = sql;
        DataTable dt = dc.GetTable(sql);

        ASPxGridView gridview4 = sender as ASPxGridView;
        gridview4.DataSource = dt;
        gridview4.DataBind();
        
    }
    protected void ASPxGridView5_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        ASPxGridView4.Visible = false;
        ASPxGridView3.Visible = false;
        
        string[] param = e.Parameters.Split(',');
        string plineCode = param[0];//生产线
        string planCode = param[1];//计划代码

        string gQadSite = "";
        string sSql = "select internal_name from code_internal where internal_code='" + plineCode + "' and internal_type_code='012'";
        dataConn dc1 = new dataConn(sSql);
        DataTable dt1 = dc1.GetTable();
        if (dt1.Rows.Count > 0)
        {
            gQadSite = dt1.Rows[0][0].ToString();
        }
        string sql = "select a.material_code,a.gys_code,d.in_qadc01,b.online_num,c.lineside_num from  "
                  + " (select material_code,gys_code,gzdd from ms_online_mt_tmp where gzdd='" + plineCode + "'  union "
                  + "  select material_code,gys_code,gzdd from ms_lineside_mt_tmp where gzdd='" + plineCode + "') a "
                  + " left join ms_online_mt_tmp b on a.material_code=b.material_code and a.gys_code=b.gys_code and a.gzdd=b.gzdd "
                  + " left join ms_lineside_mt_tmp c on a.material_code=c.material_code and a.gys_code=c.gys_code and a.gzdd=c.gzdd "
                  + " left join copy_in_mstr d on a.material_code=upper(d.in_part)   and upper(d.in_site)='" + gQadSite + "' where a.gzdd='" + plineCode + "'"
                  + " order by a.material_code,a.gys_code ";
        Session["2800COMPARE"] = sql;
        DataTable dt = dc.GetTable(sql);

        ASPxGridView gridview5 = sender as ASPxGridView;
        gridview5.DataSource = dt;
        gridview5.DataBind();
        
    }

    protected void btnXlsExport1_Click(object sender, EventArgs e)
    {
        string sql=Session["2800ONLINE"].ToString() ;

        DataTable dt = dc.GetTable(sql);
        ASPxGridView3.DataSource = dt;
        ASPxGridView3.DataBind();

        ASPxGridViewExporter1.WriteXlsToResponse("在制品物料清单导出" + DateTime.Now.ToString() + ".xls");
    }
    protected void btnXlsExport2_Click(object sender, EventArgs e)
    {
        string sql = Session["2800LINESIDE"].ToString();

        DataTable dt = dc.GetTable(sql);
        ASPxGridView4.DataSource = dt;
        ASPxGridView4.DataBind();
        
        ASPxGridViewExporter2.WriteXlsToResponse("线边物料清单" + DateTime.Now.ToString() + ".xls");
    }
    protected void btnXlsExport3_Click(object sender, EventArgs e)
    {
        string sql = Session["2800COMPARE"].ToString();

        DataTable dt = dc.GetTable(sql);
        ASPxGridView5.DataSource = dt;
        ASPxGridView5.DataBind();
        
        ASPxGridViewExporter3.WriteXlsToResponse("在制品物料清单导出" + DateTime.Now.ToString() + ".xls");
    }
        
}