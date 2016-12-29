using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Rmes.Pub.Data;
using DevExpress.Web.ASPxEditors;

namespace Rmes.WebApp.Rmes.EpdDCEC.epdProcessNote
{
    public partial class epdProcessNoteQry : System.Web.UI.Page
    {
        private dataConn dc = new dataConn();
        public string theCompanyCode;
        private string theUserId;
        public string theProgramCode;
        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();
            theProgramCode = "epdProcessNote";
            setCondition();
        }
        private void setCondition()
        {
            //初始化GRIDVIEW
            userManager theUserManager = (userManager)Session["theUserManager"];
            string companyCode = theUserManager.getCompanyCode();

            string sql = "SELECT a.*,b.pline_name,"
                +"case NOTE_TYPE when 'A' then '工艺路线+SO+工序' when 'B' then '工艺路线+工序' when 'C' then 'SO+工序' when 'D' then '组件+工序' when 'E' then 'SO+组件+工序' end noteType "
                +"from data_process_note a "
                + "left join code_product_line b on a.pline_code=b.rmes_id "
                + "where a.company_code='" + companyCode + "' and a.pline_code in (select pline_id from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') order by a.NOTE_TYPE,a.rmes_id";
            DataTable dt = dc.GetTable(sql);

            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
            
        }

        protected void btnXlsExport_Click(object sender, EventArgs e)
        {
            ASPxGridViewExporter1.WriteXlsToResponse("装机提示信息导出");
        }
    }
}