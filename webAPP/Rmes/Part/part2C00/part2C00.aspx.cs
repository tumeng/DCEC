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
using System.Data.OleDb;
using System.IO;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxClasses;
/**
 * 功能概述： 现场缺料查询
 * 
 */
namespace Rmes.WebApp.Rmes.Part.part2C00
{
    public partial class part2C00 : BasePage
    {
        private dataConn dc = new dataConn();
        PubCs thePubCs = new PubCs();
        public string theCompanyCode;
        private string theUserId, theUserCode,theUserName;
        public string theProgramCode;
        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();
            theUserCode = theUserManager.getUserCode();
            theUserName = theUserManager.getUserName();
            theProgramCode = "part2C00";

            initCode();
            setCondition();
            if (!IsPostBack)
            {
                ASPxDateEdit1.Date = DateTime.Now;
                ASPxDateEdit3.Date = DateTime.Now;
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
            string sql = "select a.*,A.ROWID,nvl(b.pt_desc2,' ') MATERIAL_NAME,decode(a.ISDELETE,'0','否','1','是') ISDELETE1 from atpustatus a left join copy_pt_mstr b on a.ljdm=b.pt_part "
                       + " where rqsj>=to_date('" + ASPxDateEdit1.Text.Trim() + "','YYYY-MM-DD')  AND rqsj<=to_date('" + ASPxDateEdit3.Text.Trim() + "','YYYY-MM-DD') ";
            if (txtPlineCode.Text.Trim() != "")
            {
                sql = sql + "and gzdd = '" + txtPlineCode.Value.ToString() + "'";
            }
            sql = sql + " order by ROWID";

            DataTable dt = dc.GetTable(sql);
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();

        }
        
        protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            setCondition();
            
        }
        
        public static void Show(System.Web.UI.Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + msg.ToString() + "');</script>");
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

        protected void ASPxGridView1_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (e.ButtonID == "delSure")
        {
            string zddm = ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "ZDDM").ToString();
            string zdmc = ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "ZDMC").ToString();
            string ThisRowId = ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "ROWID").ToString();
            
            LineSideMatFactory.PL_INSERT_ATPUSTATUS("DELETE",zddm,zdmc,theUserName,"","",ThisRowId,"");

            setCondition();

            ASPxGridView1.JSProperties.Add("cpCallbackName", "Ori");
        }
        }
    }
}