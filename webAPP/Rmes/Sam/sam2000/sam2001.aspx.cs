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


public partial class Rmes_sam2001 : BasePage
{
    private dataConn dc = new dataConn();
    private PubCs pc = new PubCs();
    public string theCompanyCode , theUserId;
    private void initCode()
    {
        //初始化用户下拉列表
        string sql = "SELECT PLINE_CODE,PLINE_NAME FROM CODE_PRODUCT_LINE WHERE COMPANY_CODE = '" + theCompanyCode + "' ORDER BY PLINE_CODE";
        SqlCode.SelectCommand = sql;
        SqlCode.DataBind();
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theUserId = theUserManager.getUserId();

        string sql = "select USER_ID,USER_NAME from CODE_USER where COMPANY_CODE = '" + theCompanyCode + "' and user_type='A' order by USER_ID";
        comboUser.DataSource = dc.GetTable(sql);
        comboUser.DataBind();
        initCode();
        


    }

    protected void listBoxProgram_Callback(object sender, CallbackEventArgsBase e)
    {
        string user = e.Parameter;

        string sql = "select a.program_code,b.program_name from vw_user_role_program a left join code_program b on a.program_code=b.program_code where user_id='" + user + "' ORDER BY PROGRAM_CODE ";
        DataTable dt = dc.GetTable(sql);

        ASPxListBox program = sender as ASPxListBox;
        program.DataSource = dt;
        program.ValueField = "PROGRAM_CODE";
        program.TextField = "PROGRAM_NAME";
        program.DataBind();
    }

    //protected void comboPline_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    //{
    //    string user = e.Parameter;

    //    ASPxComboBox pline = sender as ASPxComboBox;

    //    string sql = "SELECT PLINE_CODE,PLINE_NAME FROM CODE_PRODUCT_LINE  WHERE COMPANY_CODE = '" + theCompanyCode + "' ORDER BY PLINE_CODE ";
    //    DataTable dt = dc.GetTable(sql);

    //    pline.DataSource = dt;
    //    pline.ValueField = "PLINE_CODE";
    //    pline.TextField = "PLINE_NAME";
    //    pline.DataBind();

    //}

    protected void butConfirm_Click(object sender, EventArgs e)
    {
        int count = 0;

        string pline, user, program;
       
        user = comboUser.SelectedItem.Value.ToString();
        
          List<object> plines = GridLookupCode.GridView.GetSelectedFieldValues("PLINE_CODE");

        for (count = 0; count < ASPxListBoxProgram.SelectedValues.Count; count++)
        {
            program = ASPxListBoxProgram.SelectedValues[count].ToString();
            for (int i = 0; i < plines.Count; i++)
            {
                 pline = plines[i].ToString();

                 string sql1 = "delete from REL_USER_PLINE_PROGRAM where company_code='" + theCompanyCode + "' and user_id='" + user + "' and program_code='" + program + "' and pline_code='" + pline + "'";
                 dc.ExeSql(sql1);
                 string sql = "insert into REL_USER_PLINE_PROGRAM(company_code,user_id,program_code,pline_code)"
                      + "values('" + theCompanyCode + "','" + user + "','" + program + "','" + pline + "')";
                 dc.ExeSql(sql);

            }

         }

        Response.Write("<script type='text/javascript'>alert('新增关系成功！');window.opener.location.reload();location.href='sam2001.aspx';</script>");
      
       
    }

    protected void butCloseWindow_Click(object sender, EventArgs e)
    {
        Response.Write("<script type='text/javascript'>window.opener.location.reload();window.close();</script>");
    }

}