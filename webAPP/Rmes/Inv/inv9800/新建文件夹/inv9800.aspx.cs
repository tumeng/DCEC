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

/**
 * 功能概述：库存查询
 * 作者：游晓航
 * 创建时间：2016-09-03
 */
public partial class Rmes_Inv_inv9800_inv9800 : Rmes.Web.Base.BasePage
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
        theProgramCode = "inv9800";
        if (!IsPostBack)
        {
            ASPxDateEdit1.Date = DateTime.Now;
            ASPxDateEdit2.Date = DateTime.Now.AddDays(1);
            
        }
        initCode();
        setCondition();
        if (Request["opFlag"] == "getBatch")
        {
            string str1 = "", str2 = "", str = "", rc = "", str3 = "";
            string DateEdit1 = Request["DATEEDIT1"].ToString().Trim();
            string DateEdit2 = Request["DATEEDIT2"].ToString().Trim();
            
            string PCode = Request["PCODE"].ToString().Trim();
            //Server.UrlDecode（request.form["a"].ToString()）
            DateTime dt1 = Convert.ToDateTime(DateEdit1);
            DateTime dt2 = Convert.ToDateTime(DateEdit2);
            
            dt1.AddDays(30);
            
              
            if (dt1.AddDays(31) < dt2)
            {
                str1 = "Overtime";

            }
            else
            {
                str1 = "ok";
                string sql = "select distinct gzrq from dp_rckwcb where  to_date(gzrq,'yyyy-mm-dd hh24:mi:ss')>= to_date('" + DateEdit1 + "', 'yyyy-mm-dd hh24:mi:ss')  and  to_date(gzrq,'yyyy-mm-dd hh24:mi:ss')<= to_date('" + DateEdit2 + "', 'yyyy-mm-dd hh24:mi:ss') AND GZDD='" + PCode + "' and Rc='入库' order by Gzrq";
                DataTable dt = dc.GetTable(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string batch = dt.Rows[i][0].ToString();
                    
                    str2 = str2 + "@" + batch;
                }
                
            }

            str = str1 + "," + str2 + "," + str3;
            this.Response.Write(str);
            this.Response.End();
        }
    }
    private void initCode()
    {
        //初始化生产线下拉列表
        string sql = "select distinct a.pline_code,b.pline_name from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'";
        SqlCode.SelectCommand = sql;
        SqlCode.DataBind();
    }
    
    private void setCondition()
    {
        string sql = "select * from dp_kcb where  ";
            
        if (txtPCode.Text.Trim() != "")
        {
            sql = sql + " gzdd = '" + txtPCode.Value.ToString() + "'";
        }
        else { sql = sql + "  gzdd in  (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') " ;}
        if (ASPxDateEdit1.Text.Trim() != "")
        {
            sql = sql + " AND to_date(gzrq,'yyyy-mm-dd hh24:mi:ss')>= to_date('" + ASPxDateEdit1.Text.Trim() + "', 'yyyy-mm-dd hh24:mi:ss') ";
        }
        if (ASPxDateEdit2.Text.Trim() != "")
        {
            sql = sql + " AND to_date(gzrq,'yyyy-mm-dd hh24:mi:ss')<= to_date('" + ASPxDateEdit2.Text.Trim() + "', 'yyyy-mm-dd hh24:mi:ss')";
        }
         
        if (textSO.Text.Trim() != "")
        {
            sql = sql + " AND so like '%" + textSO.Text.Trim() + "%'";
        }
        if (txtPc.Text.Trim() != "")
        {
            sql = sql + " AND gzrq like '%" + txtPc.Text.Trim() + "%'";
        }
       
        sql = sql + " order by so,ghtm";
        DataTable dt = dc.GetTable(sql);
        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();

        string sql2 = "select SO,COUNT(*) SL from dp_kcb where ";
         
        if (txtPCode.Text.Trim() != "")
        {
            sql2 = sql2 + " gzdd = '" + txtPCode.Value.ToString() + "'";
        }
        else { sql2 = sql2 + "  gzdd in  (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') " ;}
        if (ASPxDateEdit1.Text.Trim() != "")
        {
            sql2 = sql2 + " AND to_date(gzrq,'yyyy-mm-dd hh24:mi:ss')>= to_date('" + ASPxDateEdit1.Text.Trim() + "', 'yyyy-mm-dd hh24:mi:ss')";
        }
        if (ASPxDateEdit2.Text.Trim() != "")
        {
            sql2 = sql2 + " AND to_date(gzrq,'yyyy-mm-dd hh24:mi:ss')<= to_date('" + ASPxDateEdit2.Text.Trim() + "', 'yyyy-mm-dd hh24:mi:ss')";
        }
        
        if (textSO.Text.Trim() != "")
        {
            sql2 = sql2 + " AND so like '%" + textSO.Text.Trim() + "%'";
        }
        if (txtPc.Text.Trim() != "")
        {
            sql2 = sql2 + " AND gzrq like '%" + txtPc.Text.Trim() + "%'";
        }
         
            sql2 = sql2 + "  GROUP BY SO order by So";
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
        //ASPxGridView2.Selection.UnselectAll();
    }
 

}