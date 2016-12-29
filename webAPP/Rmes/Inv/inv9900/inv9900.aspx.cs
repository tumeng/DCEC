using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rmes.Pub.Data;
using PetaPoco;
using Rmes.DA.Base;
using DevExpress.Web.ASPxGridView;
using DevExpress.Utils;
using System.Data;
using Rmes.Web.Base;
using DevExpress.Web.ASPxClasses;
using Rmes.DA.Procedures;
using DevExpress.Web.ASPxEditors;
/**
 * 功能概述：库存综合查询
 * 作者：游晓航
 * 创建时间：2016-09-08
 */
public partial class Rmes_Inv_inv9900_inv9900 : Rmes.Web.Base.BasePage
{


    private dataConn dc = new dataConn();
    private PubCs thePubCs = new PubCs();
    private string theProgramCode;
    private string theCompanyCode, theUserId, theUserCode, theUserName;

    public Database db = DB.GetInstance();
    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theUserCode = theUserManager.getUserCode();
        theUserId = theUserManager.getUserId();
        theUserName = theUserManager.getUserName();
        theProgramCode = "inv9900";
        if (!IsPostBack)
        {
            ASPxDateEdit1.Date = DateTime.Now;
            ASPxDateEdit2.Date = DateTime.Now.AddDays(1);
           
        }
        initCode();
        initYG();
        initFrom();
        setCondition();
        //if (Request["opFlag"] == "getBatch")
        //{
        //    string str1 = "", str2 = "", str = "", rc = "", str3 = "";
        //    string DateEdit1 = Request["DATEEDIT1"].ToString().Trim();
        //    string DateEdit2 = Request["DATEEDIT2"].ToString().Trim();
        //    string Chose = Request["CHOSE"].ToString().Trim();
        //    string PCode = Request["PCODE"].ToString().Trim();
        //    //Server.UrlDecode（request.form["a"].ToString()）
        //    DateTime dt1 = Convert.ToDateTime(DateEdit1);
        //    DateTime dt2 = Convert.ToDateTime(DateEdit2);

        //    dt1.AddDays(30);
        //    if (Chose == "RK") { rc = "入库"; }
        //    else { rc = "出库"; }

        //    if (dt1.AddDays(30) < dt2)
        //    {
        //        str1 = "Overtime";

        //    }
        //    else
        //    {
        //        str1 = "ok";
        //        string sql = "select distinct gzrq from dp_rckwcb where  to_date(gzrq,'yyyy-mm-dd hh24:mi:ss')>= to_date('" + DateEdit1 + "', 'yyyy-mm-dd hh24:mi:ss')  and  to_date(gzrq,'yyyy-mm-dd hh24:mi:ss')<= to_date('" + DateEdit2 + "', 'yyyy-mm-dd hh24:mi:ss') AND GZDD='" + PCode + "' and Rc='" + rc + "' order by Gzrq";
        //        DataTable dt = dc.GetTable(sql);
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            string batch = dt.Rows[i][0].ToString();

        //            str2 = str2 + "@" + batch;
        //        }

        //    }

        //    str = str1 + "," + str2 + "," + str3;
        //    this.Response.Write(str);
        //    this.Response.End();
        //}
    }
    protected void ASPxCallbackPanel4_Callback(object sender, CallbackEventArgsBase e)
    {
        
        if (ASPxDateEdit1.Date.AddDays(31) < ASPxDateEdit2.Date)
        {
            ASPxCallbackPanel4.JSProperties.Add("cpCallbackName", "Fail");
            ASPxCallbackPanel4.JSProperties.Add("cpCallbackRet", "选择日期范围不能超过31天，请重新选择！");
            return;
 
        }
        else if (txtPCode.Text.Trim() != "")
        {

            string sql = " select '全部批次' as gzrq  from dp_rckwcb union select distinct gzrq from dp_rckwcb where  to_date(gzrq,'yyyy-mm-dd hh24:mi:ss')>= to_date('" + ASPxDateEdit1.Text.Trim() + "', 'yyyy-mm-dd hh24:mi:ss')  "
                       +"and  to_date(gzrq,'yyyy-mm-dd hh24:mi:ss')<= to_date('" + ASPxDateEdit2.Text.Trim() + "', 'yyyy-mm-dd hh24:mi:ss') AND GZDD='" + txtPCode.Value.ToString() + "' and Rc='" + txtChose.Text.Trim() + "' order by Gzrq desc";
            SqlPC.SelectCommand = sql;
            SqlPC.DataBind();
        }
    }
    private void initYG()
    {
        //初始化员工下拉列表
        string sql = "select user_name from code_user a left join rel_team_user c on a.user_id= c.user_id left join code_team b on c.team_code=b.team_code  where  b.team_name='库房用户组' order by user_name";
        SqlYG.SelectCommand = sql; 
        SqlYG.DataBind();
    }
    private void initFrom()
    {
        //初始库房下拉列表
        string sql = "select a.depotid,a.depotname from dp_dmdepot a left join REL_USER_DMDEPOT b on a.depotid=b.depotid where b.user_code='" + theUserCode + "' and b.PROGRAM_CODE='INV9900'  ";
            
        //string sql = "Select depotid,depotname from dp_dmdepot order by depotId";
        SqlFrom.SelectCommand = sql;
        SqlFrom.DataBind();
    }
    
    private void initCode()
    {
        //初始化生产线下拉列表
        string sql = "select a.depotid pline_code,a.depotname pline_name from dp_dmdepot a left join REL_USER_DMDEPOT b on a.depotid=b.depotid where b.user_code='" + theUserCode + "' and b.PROGRAM_CODE='INV9900'  ";
            
        //string sql = "select distinct a.pline_code,b.pline_name from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'";
        SqlCode.SelectCommand = sql;
        SqlCode.DataBind();
         
        //txtPCode.SelectedIndex = txtPCode.Items.Count >= 0 ? 0 : -1;
    }

    private void setCondition()
    {
        
        string sql = "select * from dp_rckwcb where ";

        if (txtPCode.Text.Trim() != "")
        {
            sql = sql + " gzdd = '" + txtPCode.Value.ToString() + "'";
        }
        else { sql = sql + "  gzdd in  (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') "; }
        if (ASPxDateEdit1.Text.Trim() != "")
        {
            sql = sql + " AND to_date(gzrq,'yyyy-mm-dd hh24:mi:ss')>= to_date('" + ASPxDateEdit1.Text.Trim() + "', 'yyyy-mm-dd hh24:mi:ss') ";
        }
        if (ASPxDateEdit2.Text.Trim() != "")
        {
            sql = sql + " AND to_date(gzrq,'yyyy-mm-dd hh24:mi:ss')<= to_date('" + ASPxDateEdit2.Text.Trim() + "', 'yyyy-mm-dd hh24:mi:ss')";
        }
        if (txtPc.Text.Trim() != "")
        {
            sql = sql + " AND gzrq like '%" + txtPc.Text.Trim() + "%'";
        }
        if (txtChose.Text.Trim() != "")
        {
            sql = sql + " AND rc like '%" + txtChose.Text.Trim() + "%'";
        }
        if (txtYG.Text.Trim() != "")
        {
            sql = sql + " AND ygmc like '%" + txtYG.Text.Trim() + "%'";
        }
        if (txtSO.Text.Trim() != "")
        {
            sql = sql + " AND so like '%" + txtSO.Text.Trim() + "%'";
        }
        if (txtCH.Text.Trim() != "")
        {
            sql = sql + " AND ch like '%" + txtCH.Text.Trim() + "%'";
        }
        if (txtKFfrom.Text.Trim() != "")
        {
            sql = sql + " AND sourceplace like '%" + txtKFfrom.Text.Trim() + "%'";
        }
        if (txtKFto.Text.Trim() != "")
        {
            sql = sql + " AND destination like '%" + txtKFto.Text.Trim() + "%'";
        }

        sql = sql + " order by so,ghtm";
        DataTable dt = dc.GetTable(sql);
        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
        string sql2 = "";
        if (txtChose.Text.Trim() == "入库")
        {
            sql2 = "select SourcePlace  SOURCE1,Gzrq BatchId,SO,count(*)  as SL from dp_rckwcb  where ";
        }
        else
        {
            sql2 = "select Destination SOURCE1,BatchId ,so,count(*) as SL from dp_rckwcb where ";
        }
        
        if (txtPCode.Text.Trim() != "")
        {
            sql2 = sql2 + " gzdd = '" + txtPCode.Value.ToString() + "'";
        }
        else { sql2 = sql2 + "  gzdd in  (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') "; }
        if (ASPxDateEdit1.Text.Trim() != "")
        {
            sql2 = sql2 + " AND to_date(gzrq,'yyyy-mm-dd hh24:mi:ss')>= to_date('" + ASPxDateEdit1.Text.Trim() + "', 'yyyy-mm-dd hh24:mi:ss')";
        }
        if (ASPxDateEdit2.Text.Trim() != "")
        {
            sql2 = sql2 + " AND to_date(gzrq,'yyyy-mm-dd hh24:mi:ss')<= to_date('" + ASPxDateEdit2.Text.Trim() + "', 'yyyy-mm-dd hh24:mi:ss')";
        }
        if (txtCH.Text.Trim() != "")
        {
            sql = sql + " AND ch like '%" + txtCH.Text.Trim() + "%'";
        }
        if (txtKFfrom.Text.Trim() != "")
        {
            sql = sql + " AND sourceplace like '%" + txtKFfrom.Text.Trim() + "%'";
        }
        if (txtKFto.Text.Trim() != "")
        {
            sql = sql + " AND destination like '%" + txtKFto.Text.Trim() + "%'";
        }
        if (txtYG.Text.Trim() != "")
        {
            sql = sql + " AND ygmc like '%" + txtYG.Text.Trim() + "%'";
        }
        if (txtSO.Text.Trim() != "")
        {
            sql2 = sql2 + " AND so like '%" + txtSO.Text.Trim() + "%'";
        }
        if (txtPc.Text.Trim() != "")
        {
            sql2 = sql2 + " AND gzrq like '%" + txtPc.Text.Trim() + "%'";
        }
        if (txtChose.Text.Trim() == "入库")
        {
            sql2 = sql2 + "  GROUP BY SourcePlace,GZRQ,SO order by So";
        }
        else
        {
            sql2 = sql2 + "  GROUP BY Destination,BatchId,SO order by So";
        }
         
        DataTable dt2 = dc.GetTable(sql2);
        ASPxGridView2.DataSource = dt2;
        ASPxGridView2.DataBind();
    }

    protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        setCondition();
        ASPxGridView1.Selection.UnselectAll();
    }
    protected void ASPxGridView2_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        setCondition();
         
    }


}