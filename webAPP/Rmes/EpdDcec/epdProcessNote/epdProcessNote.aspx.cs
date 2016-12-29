using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Rmes.Pub.Data;
using DevExpress.Web.ASPxEditors;
using Oracle.DataAccess.Client;

namespace Rmes.WebApp.Rmes.EpdDCEC.epdProcessNote
{
    public partial class epdProcessNote : System.Web.UI.Page
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
                + "where a.company_code='" + companyCode + "' and a.pline_code in (select pline_id from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') order by a.input_time desc nulls last, a.NOTE_TYPE,a.rmes_id";
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
                string Sql2 = "INSERT INTO data_process_note_LOG (rmes_id,company_code,pline_code,rounting_remark,plan_so,process_code,"
                    + "process_note,process_note_color,process_note_font,note_type,component_code,from_date,to_date,process_pic,user_code,flag,rqsj)"
                    + " SELECT rmes_id,company_code,pline_code,rounting_remark,plan_so,process_code,"
                    + "process_note,process_note_color,process_note_font,note_type,component_code,from_date,to_date,process_pic,'" + theUserCode + "' , 'DEL', SYSDATE"
                    + " FROM data_process_note WHERE RMES_ID = '" + rmesId + "'";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }

            string sql = "delete from data_process_note where rmes_id='" + rmesId + "'";
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

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                dataConn theDataConn = new dataConn();
                theDataConn.theComd.CommandType = CommandType.StoredProcedure;
                theDataConn.theComd.CommandText = "MW_UPDATE_ZJTS";
                theDataConn.theComd.Parameters.Add("TYPE1", OracleDbType.Varchar2).Value = "ALL";
                theDataConn.theComd.Parameters.Add("RMESID1", OracleDbType.Varchar2).Value = "";
                theDataConn.OpenConn();
                theDataConn.theComd.ExecuteNonQuery();
                theDataConn.CloseConn();
                showAlert(this, "处理成功！");
            }
            catch (Exception e1)
            {
                showAlert(this, e1.Message.ToString());
            }
        }

        protected void BtnUpdate1_Click(object sender, EventArgs e)
        {
            try
            {
                int count1 = ASPxGridView1.Selection.Count;
                if (count1 != 1)
                {
                    showAlert(this, "请选择单条处理！");
                    return;
                }
                List<object> aa = ASPxGridView1.GetSelectedFieldValues("RMES_ID");
                string sql = "select count(1) from data_process_note where rmes_id='" + aa[0] + "' and (note_type='C' or note_type='D' or note_type='E')";
                if (dc.GetValue(sql) == "0")
                {
                    showAlert(this, "待处理数据类型错误，只能选择so、组件相关装机提示！");
                    return;
                }

                dataConn theDataConn = new dataConn();
                theDataConn.theComd.CommandType = CommandType.StoredProcedure;
                theDataConn.theComd.CommandText = "MW_UPDATE_ZJTS";
                theDataConn.theComd.Parameters.Add("TYPE1", OracleDbType.Varchar2).Value = "ONE";
                theDataConn.theComd.Parameters.Add("RMESID1", OracleDbType.Varchar2).Value = aa[0];
                theDataConn.OpenConn();
                theDataConn.theComd.ExecuteNonQuery();
                theDataConn.CloseConn();
                showAlert(this, "处理成功！");
            }
            catch (Exception e1)
            {
                showAlert(this, e1.Message.ToString());
            }
        }

        //前端弹出alert消息
        public static void showAlert(System.Web.UI.Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + msg.ToString() + "');</script>");
        }
    }
}