using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.Pub.Data;
using Rmes.Web.Base;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
/**
 * 功能概述：线边核减控制密码维护
 * 作者：游晓航
 * 创建时间：2016-08-15
 */
namespace Rmes.WebApp.Rmes.Part.part1900
{
    public partial class part1900 : BasePage
    {
        private dataConn dc = new dataConn();
        PubCs thePubCs = new PubCs();
        //public DateTime theBeginDate, theEndDate;
        public string theCompanyCode;
        private string theUserId, theUserCode;
        public string theProgramCode;
        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();
            theUserCode = theUserManager.getUserCode();
            theProgramCode = "part1900";
            initCode();
            if (Request["opFlag"] == "getEditSeries")
            {
                string str1 = "", whyh = "", whsj = "";
               
                string pcode = Request["Pcode"].ToString().Trim();
                if (pcode != "")
                {
                    string sql3 = "select nvl(whyh,' '),nvl(to_char(whsj,'yyyy-mm-dd hh24:mi:ss'),' ') from ms_skip_password where gzdd='" + pcode + "' ";
                    DataTable dt3 = dc.GetTable(sql3);
                    if (dt3.Rows.Count > 0)
                    {
                         whyh = dt3.Rows[0][0].ToString();
                         whsj = dt3.Rows[0][1].ToString();
                        //txtWHYH.Text = whyh;
                        //txtWHSJ.Text = whsj;
                    }
                    
                }
                str1 = whyh + "," + whsj;
                this.Response.Write(str1);
                this.Response.End();
            }            
        }
        private void initCode()
        {
            //初始化用户下拉列表
            string sql = "select distinct a.pline_code,b.pline_name from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'";
            SqlCode.SelectCommand = sql;
            SqlCode.DataBind();
        }
       
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if (txtPCode.Text.Trim() == "")
            {
                Response.Write("<script>alert('请选择要维护的生产线！')</script>");
                return;
            }
            string sql = " select yhmm from ms_skip_password where gzdd='" + txtPCode.Value + "' and yhmm='" + txtYMM.Text.Trim() + "'";
            DataTable dt = dc.GetTable(sql);
            if (dt.Rows.Count <= 0)
            {
                Response.Write("<script>alert('原密码输入错误！')</script>");
                return;
            }
            if (txtXMM.Text.Trim() == "")
            {
                Response.Write("<script>alert('新密码不能为空！')</script>");
                return;
            }
            if (txtXMM.Text.Trim() != txtQRMM.Text.Trim())
            {
                Response.Write("<script>alert('新密码与确认密码不一致！')</script>");
                return;
            }           
                string Sql = " update ms_skip_password set yhmm='" + txtXMM.Text.Trim() + "',whyh='" + theUserId + "',whsj=sysdate where gzdd='" + txtPCode.Value + "'  ";

                dc.ExeSql(Sql);
                Response.Write("<script>alert('密码修改成功！')</script>"); 
        }
    }
}
          