using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Rmes.Pub.Data;
using DevExpress.Web.ASPxEditors;

namespace Rmes.WebApp.Rmes.EpdDCEC.epdQualityQA
{
    public partial class epdQualityQA : System.Web.UI.Page
    {
        private dataConn dc = new dataConn();
        public string theCompanyCode;
        private string theUserId,theUserCode;
        public string theProgramCode;
        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();
            theUserCode = theUserManager.getUserCode();
            theProgramCode = "epdQualityQA";
            setCondition();
        }
        private void setCondition()
        {
            //初始化GRIDVIEW
            userManager theUserManager = (userManager)Session["theUserManager"];
            string companyCode = theUserManager.getCompanyCode();

            string sql = "SELECT a.*,b.pline_name,"
                +"case QA_TYPE when 'A' then '工艺路线+SO+工序' when 'B' then '工艺路线+工序' when 'C' then 'SO+工序' when 'D' then '组件+工序' when 'E' then 'SO+组件+工序' end noteType "
                +"from DATA_QUALITY_QA a "
                + "left join code_product_line b on a.pline_code=b.rmes_id "
                + "where a.company_code='" + companyCode + "' and a.pline_code in (select pline_id  from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "')  order by a.input_time desc nulls last,a.QA_TYPE,a.rmes_id";
            DataTable dt = dc.GetTable(sql);

            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
            
        }
        protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string rmesId=e.Keys["RMES_ID"].ToString();

            //插入到日志表
            try
            {
                string Sql2 = "INSERT INTO DATA_QUALITY_QA_LOG (rmes_id,company_code,pline_code,rounting_remark,plan_so,process_code,"
                    + "qa_question,qa_question_color,qa_question_font,qa_type,component_code,from_date,to_date,QA_ANSWER,user_code,flag,rqsj)"
                    + " SELECT rmes_id,company_code,pline_code,rounting_remark,plan_so,process_code,"
                    + "qa_question,qa_question_color,qa_question_font,qa_type,component_code,from_date,to_date,QA_ANSWER,'" + theUserCode + "' , 'DEL', SYSDATE"
                    + " FROM DATA_QUALITY_QA WHERE RMES_ID = '" + rmesId + "'";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }

            string sql = "delete from DATA_QUALITY_QA where rmes_id='" + rmesId + "'";
            dc.ExeSql(sql);

            setCondition();
            e.Cancel = true;
        }

        protected void ASPxGridView1_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            //edie
            if (e.ButtonID == "Edit") {
                string rmesId = ASPxGridView1.GetRowValues(e.VisibleIndex, "RMES_ID").ToString();
                ASPxGridView1.JSProperties.Add("cpCallbackName", "Edit");
                ASPxGridView1.JSProperties.Add("cpRmesId", rmesId);
            }
        }
    }
}