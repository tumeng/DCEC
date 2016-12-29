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
 * 功能概述：发料工位维护
 * 作者：游晓航
 * 创建时间：2016-08-13
 */

public partial class Rmes_part1600 : Rmes.Web.Base.BasePage
{
    private dataConn dc = new dataConn();
    private PubCs pc = new PubCs();
    public string theCompanyCode;
    private string theUserId,theUserCode;
    public string theProgramCode;

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theUserId = theUserManager.getUserId();
        theUserCode = theUserManager.getUserCode();
        theProgramCode = "part1600";
        //initRLocation();
        initCode();
        setCondition();
        if (Request["opFlag"] == "getEditSeries")
        {
            string str1 = "", str2 = "";
            string RLOCATION = Request["RLOCATION"].ToString().Trim();
            string pcode = Request["Pcode"].ToString().Trim();
            if (pcode != "R" &&pcode != "L")
            {
                str2 = RLOCATION;
            }
            else
            {
                str2 = "";
            }
            if (pcode == "L")
            {

                string sql = "select PLINE_CODE from code_product_line where pline_code='E' OR PLINE_CODE='W'";
                DataTable dt = dc.GetTable(sql);
                string Rgzdd1 = dt.Rows[0][0].ToString();
                string Rgzdd2 = dt.Rows[1][0].ToString();
                str1 = str1 + Rgzdd1 + "@" + Rgzdd2 ;
                
            }
            else if (pcode != "R")
            {
                string sql = "select PLINE_CODE  from code_product_line where pline_code ='" + pcode + "'";
                DataTable dt = dc.GetTable(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string code1 = dt.Rows[i][0].ToString();
                    //string code2 = dt.Rows[i][1].ToString();
                    str1 = str1 + "@" + code1;
                }
                //str1 = str1 + "@" + pcode;
               
               
            }
            else 
            {
                string sql = "select PLINE_CODE  from code_product_line where pline_code='E'OR PLINE_CODE='W' OR PLINE_CODE='R'";
                DataTable dt = dc.GetTable(sql);
                string Rgzdd1 = dt.Rows[0][0].ToString();
                string Rgzdd2 = dt.Rows[1][0].ToString();
                string Rgzdd3 = dt.Rows[2][0].ToString();
                str1 = Rgzdd1 + "@" + Rgzdd2 + "@" + Rgzdd3;
            }
            str1 = str1 + "," + str2;
            this.Response.Write(str1);
            this.Response.End();
        }


    }
    private void initRLocation()
    {
        //初始化工位下拉列表
        string sql = "select distinct location_code from code_location where pline_code in (select pline_id from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') ORDER BY LOCATION_CODE";
        SqlRLocation.SelectCommand = sql;
        SqlRLocation.DataBind();
    }
    private void initCode()
    {
        //初始化生产线下拉列表
        string sql = "select distinct a.pline_code,b.pline_name,a.pline_id from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'";

        SqlCode.SelectCommand = sql;
        SqlCode.DataBind();
        DataTable dt = dc.GetTable(sql);
        string sql2 = "";
        if (dt.Rows.Count > 0)
        {
            sql2 = "select distinct location_code from code_location where pline_code ='" + dt.Rows[0][2].ToString() + "' ORDER BY LOCATION_CODE";

        }
        else
        {
            sql2 = "select distinct location_code from code_location where pline_code in (select pline_id from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') ORDER BY LOCATION_CODE";

        }
        SqlRLocation.SelectCommand = sql2;
        SqlRLocation.DataBind();
    }

    private void setCondition()
    {
        string strPCode = "";
        if (txtPCode.Text.Trim() == "")
        {
            return;

        }
        else
        {
            strPCode = txtPCode.Value.ToString();
            string strRLoction = CombRLocation.Text.Trim().ToUpper();
            string sql = "select * from ms_issue_location  where ";
            if (txtPCode.Text.Trim() != "" && strRLoction != "")
            {
                sql = sql + "   real_location='" + strRLoction + "'";
                if (strPCode == "L")
                {
                    sql = sql + " and issue_gzdd='" + strPCode + "'";
                }
                else
                {
                    sql = sql + "and real_gzdd='" + strPCode + "'";
                }
            }
            else if (strRLoction != "" && strPCode == "")
            {
                sql = sql + "  real_location='" + strRLoction + "'";

            }
            else if (strRLoction == "" && strPCode != "")
            {
                if (strPCode == "L")
                {
                    sql = sql + "  issue_gzdd='" + strPCode + "'";
                }
                else
                {
                    sql = sql + "  real_gzdd='" + strPCode + "'";
                }

            }
            sql = sql + "order by input_time desc nulls last,real_gzdd,real_location";

            DataTable dt = dc.GetTable(sql);

            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
        }
    }
    //查询
    protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        setCondition();
        ASPxGridView1.Selection.UnselectAll();

    }
    //新增
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {

        ASPxComboBox uRGzdd = ASPxGridView1.FindEditFormTemplateControl("txtRGzdd") as ASPxComboBox;
        ASPxComboBox uRLocation = ASPxGridView1.FindEditFormTemplateControl("txtRLocation") as ASPxComboBox;
        ASPxComboBox uIGzdd = ASPxGridView1.FindEditFormTemplateControl("txtIGzdd") as ASPxComboBox;
        ASPxComboBox uILocation = ASPxGridView1.FindEditFormTemplateControl("txtILocation") as ASPxComboBox;
        string strRGzdd = uRGzdd.Value.ToString();
        string strRLocation = uRLocation.Value.ToString();
        string strIGzdd = uIGzdd.Value.ToString();
        string strILocation = uILocation.Value.ToString();
        strRGzdd = strRGzdd.Substring(0, 1);

        string Sql = "INSERT INTO MS_ISSUE_LOCATION (REAL_GZDD,REAL_LOCATION,ISSUE_GZDD,ISSUE_LOCATION,input_person,input_time)VALUES('" + strRGzdd + "','" + strRLocation + "','" + strIGzdd + "','" + strILocation + "','"+theUserId+"',sysdate)";
        dc.ExeSql(Sql);
        //插入到日志表
        try
        {
            string Sql2 = "INSERT INTO MS_ISSUE_LOCATION_LOG (REAL_GZDD,REAL_LOCATION,ISSUE_GZDD,ISSUE_LOCATION,user_code,flag,rqsj)"
                + " SELECT REAL_GZDD,REAL_LOCATION,ISSUE_GZDD,ISSUE_LOCATION,'"
                + theUserCode + "' , 'ADD', SYSDATE FROM MS_ISSUE_LOCATION WHERE real_gzdd='"
                + strRGzdd + "' and real_location='" + strRLocation + "' and issue_gzdd='" + strIGzdd + "' and issue_location='" + strILocation + "' ";
            dc.ExeSql(Sql2);
        }
        catch
        {
        
        }

        e.Cancel = true;
        ASPxGridView1.CancelEdit();
        setCondition();
    }

    //删除
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        //判断当前记录是否可以删除
        string strRGzdd = e.Values["REAL_GZDD"].ToString();
        string strRLocation = e.Values["REAL_LOCATION"].ToString();
        string strIGzdd = e.Values["ISSUE_GZDD"].ToString();
        string strILocation = e.Values["ISSUE_LOCATION"].ToString();

        //插入到日志表
        try
        {
            string Sql2 = "INSERT INTO MS_ISSUE_LOCATION_LOG (REAL_GZDD,REAL_LOCATION,ISSUE_GZDD,ISSUE_LOCATION,user_code,flag,rqsj)"
                + " SELECT REAL_GZDD,REAL_LOCATION,ISSUE_GZDD,ISSUE_LOCATION,'"
                + theUserCode + "' , 'DEL', SYSDATE FROM MS_ISSUE_LOCATION WHERE real_gzdd='"
                + strRGzdd + "' and real_location='" + strRLocation + "' and issue_gzdd='" + strIGzdd + "' and issue_location='" + strILocation + "' ";
            dc.ExeSql(Sql2);
        }
        catch
        {
            return;
        }
       
        //确认删除
        string Sql = "delete from  ms_issue_location where real_gzdd='" + strRGzdd + "' and real_location='" + strRLocation + "' and issue_gzdd='" + strIGzdd + "' and issue_location='" + strILocation + "' ";
        dc.ExeSql(Sql);
       
        setCondition();
        e.Cancel = true;
    }

    //创建EDITFORM前
    protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
    {
        string Sql = " select distinct a.pline_code,a.pline_code||' '||b.pline_name as showtext from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "' ";
        DataTable dt = dc.GetTable(Sql);
        ASPxComboBox uRGzdd = ASPxGridView1.FindEditFormTemplateControl("txtRGzdd") as ASPxComboBox;
        ASPxComboBox uIGzdd = ASPxGridView1.FindEditFormTemplateControl("txtIGzdd") as ASPxComboBox;
        //ASPxComboBox upcode = ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;
        //uRGzdd.DataSource = dt;
        //uRGzdd.TextField = dt.Columns[1].ToString();
        //uRGzdd.ValueField = dt.Columns[0].ToString();

        uIGzdd.DataSource = dt;
        uIGzdd.TextField = dt.Columns[1].ToString();
        uIGzdd.ValueField = dt.Columns[0].ToString();

        //upcode.DataSource = dt;
        //upcode.TextField = dt.Columns[1].ToString();
        //upcode.ValueField = dt.Columns[0].ToString();
        string Sql2 = "",Sql3="";
        if (txtPCode.Text.Trim() != "")
        {
            string pline_id = dc.GetValue("select rmes_id from code_product_line where pline_code='" + txtPCode.Value.ToString() + "'");
            if (txtPCode.Value.ToString().ToUpper() == "L")
            {
                Sql2 = "select distinct LOCATION_CODE,LOCATION_CODE||' '||LOCATION_NAME from CODE_LOCATION where pline_code ='不存在' order by LOCATION_CODE";
                Sql3 = "select distinct LOCATION_CODE,LOCATION_CODE||' '||LOCATION_NAME from CODE_LOCATION where pline_code ='" + pline_id + "' order by LOCATION_CODE";
            
            }
            else if (txtPCode.Value.ToString().ToUpper() != "R")
            {

                Sql3 = "select distinct LOCATION_CODE,LOCATION_CODE||' '||LOCATION_NAME from CODE_LOCATION where pline_code ='不存在' order by LOCATION_CODE";
                Sql2 = "select distinct LOCATION_CODE,LOCATION_CODE||' '||LOCATION_NAME from CODE_LOCATION where pline_code ='" + pline_id + "' order by LOCATION_CODE";
            }
            else
            {
                Sql2 = "select distinct LOCATION_CODE,LOCATION_CODE||' '||LOCATION_NAME from CODE_LOCATION where location_code not in "
                    +"(select issue_location from ms_issue_location where issue_gzdd ='" + txtPCode.Value.ToString() + "')order by LOCATION_CODE";
                Sql3 = "select distinct LOCATION_CODE,LOCATION_CODE||' '||LOCATION_NAME from CODE_LOCATION where pline_code ='不存在' order by LOCATION_CODE";
            }
        }
        else
        {
            Sql2 = "select distinct LOCATION_CODE,LOCATION_CODE||' '||LOCATION_NAME from CODE_LOCATION where"
           + " pline_code in (select pline_id from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "')"
           + "and location_code not in (select issue_location from ms_issue_location where issue_gzdd in (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "'))order by LOCATION_CODE";
          Sql3 = "select distinct LOCATION_CODE,LOCATION_CODE||' '||LOCATION_NAME from CODE_LOCATION where"
          + " pline_code in (select pline_id from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "')"
          + "and location_code not in (select real_location from ms_issue_location where real_gzdd in (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "'))order by LOCATION_CODE";
        
        }
        DataTable dt2 = dc.GetTable(Sql2);
        ASPxComboBox uRLocation = ASPxGridView1.FindEditFormTemplateControl("txtRLocation") as ASPxComboBox;
        ASPxComboBox uILocation = ASPxGridView1.FindEditFormTemplateControl("txtILocation") as ASPxComboBox;
        uRLocation.DataSource = dt2;
        uRLocation.TextField = dt2.Columns[1].ToString();
        uRLocation.ValueField = dt2.Columns[0].ToString();
        DataTable dt3 = dc.GetTable(Sql3);
        uILocation.DataSource = dt3;
        uILocation.TextField = dt3.Columns[1].ToString();
        uILocation.ValueField = dt3.Columns[0].ToString();
       // string Sql3 = "select distinct LOCATION_CODE,LOCATION_CODE||' '||LOCATION_NAME from CODE_LOCATION where"
       //+ " pline_code in (select pline_id from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "')"
       //+ "and location_code not in (select real_location from ms_issue_location where real_gzdd in (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "'))order by LOCATION_CODE";
        


        
    }

    //修改数据校验
    protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        string strRGzdd = e.NewValues["REAL_GZDD"].ToString();
        string strRLocation = e.NewValues["REAL_LOCATION"].ToString();
        string strIGzdd = e.NewValues["ISSUE_GZDD"].ToString();
        string strILocation = e.NewValues["ISSUE_LOCATION"].ToString();
        strRGzdd = strRGzdd.Substring(0, 1);

        //判断是否重复
        if (ASPxGridView1.IsNewRowEditing)
        {
            string chSql = "select * from ms_issue_location where real_gzdd='" + strRGzdd + "' and real_location='" + strRLocation + "' and issue_gzdd='" + strIGzdd + "' and issue_location='" + strILocation + "' ";
            DataTable dt = dc.GetTable(chSql);
            if (dt.Rows.Count > 0)
            {
                e.RowError = "已存在相同记录！";
            }

        }

    }
    protected void btnXlsExport_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter1.WriteXlsToResponse("发料工位信息");
    }

    protected void txtRLoction_Callback(object sender, CallbackEventArgsBase e)
    {
        string pline_id = dc.GetValue("select rmes_id from code_product_line where pline_code='" + e.Parameter + "'");
        string sql = "select distinct location_code from code_location where pline_code ='" + pline_id + "'  ORDER BY LOCATION_CODE";

        SqlRLocation.SelectCommand = sql;
        SqlRLocation.DataBind();
        CombRLocation.DataBind();//两次绑定才行
    }

   


}
