using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rmes.DA.Entity;
using Rmes.DA.Base;
using DevExpress.Web.ASPxEditors;
using Rmes.Pub.Data;
using System.Data;

namespace Rmes.WebApp.Rmes.Exp.Exp2100
{
    public partial class Exp2100 : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int year = DateTime.Now.Year;
                int month = DateTime.Now.Month;
                for (int i = 0; i < 10; i++)
                {
                    ListEditItem item = ASPxComboBox1.Items.Add();
                    item.Text = (year + i - 5).ToString();
                    item.Value = (year + i - 5).ToString();
                    if (item.Value.Equals(year.ToString())) item.Selected = true;
                }
                for (int i = 1; i <= 12; i++)
                {
                    ListEditItem item = ASPxComboBox2.Items.Add();
                    item.Text = i.ToString();
                    item.Value = i.ToString();
                    if (item.Value.Equals(month.ToString())) item.Selected = true;
                }
            }
            BindData();
        }
        public void BindData()
        {
            List<DepartmentEntity> dept = DB.GetInstance().Fetch<DepartmentEntity>("");
            com_DEPT.DataSource = dept;
            com_DEPT.ValueField = "DEPT_CODE";
            com_DEPT.TextField = "DEPT_NAME";
            com_DEPT.DataBind();
            if (com_DEPT.SelectedItem == null) return;
            if (ASPxComboBox1.SelectedItem == null || ASPxComboBox2.SelectedItem == null) return;
            //DataTable dt = (DataTable)Session["data"];
            //if (dt == null || dt.Rows.Count == 0) return;
            int year = Convert.ToInt32(ASPxComboBox1.SelectedItem.Value);

            int month = Convert.ToInt32(ASPxComboBox2.SelectedItem.Value);
            DateTime time = new DateTime(year, month, 1);
            Report_Exp2100 report = new Report_Exp2100( time, com_DEPT.SelectedItem.Value.ToString());
            ReportViewer1.Report = report;
            Session.Remove("data");
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            //string dept = com_DEPT.SelectedItem.Value.ToString();
            //int year = Convert.ToInt32(ASPxComboBox1.SelectedItem.Value);
            //int month = Convert.ToInt32(ASPxComboBox2.SelectedItem.Value);
            //DateTime time = new DateTime(year, month, 1);
            //dataConn theconn = new dataConn();

            //DataTable dt = new DataTable();
            ////部门对应产线
            //DataTable plines = theconn.GetTable("select pline_code from REL_DEPT_PLINE where dept_code='" + dept + "'");

            ////DataTable dt = new DataTable();
            //dt.Columns.Add("PLINE_CODE");
            //dt.Columns.Add("PROJECT_CODE");
            //dt.Columns.Add("PRODUCT_SERIES");
            //dt.Columns.Add("ORDER_NUM");
            //dt.Columns.Add("COMPLETED_NUM");
            //dt.Columns.Add("PERIOD_COMPLETED_NUM");


            //string _b = time.AddDays(-time.Day + 1).ToShortDateString() + " 00:00:00";
            //string _e = time.AddMonths(1).AddDays(-time.Day + 1).ToShortDateString() + " 00:00:00";

            //for (int j = 0; j < plines.Rows.Count; j++)
            //{
            //    string pline_name = theconn.GetValue("select pline_name from CODE_PRODUCT_LINE where pline_code='" + plines.Rows[j][0] + "'");
            //    string sql = string.Format("select dp.product_series,dp.project_code,dp.pline_code from DATA_SN_COMPLETE_INSTORE t left join data_plan dp on t.plan_code=dp.plan_code  where t.work_date between to_date('{0}','yyyy-mm-dd hh24:mi:ss') and to_date('{1}','yyyy-mm-dd hh24:mi:ss') group by product_series,pline_code,project_code ORDER BY pline_code,project_code,product_series", _b, _e);
            //    DataTable complete = theconn.GetTable(sql);
            //    //List<SNCompleteInstoreEntity> complete = DB.GetInstance().Fetch<SNCompleteInstoreEntity>("where WORK_DATE between to_date(@0,'yyyy-mm-dd hh24:mi:ss') and to_date(@1,'yyyy-mm-dd hh24:mi:ss')",_b,_e);

            //    for (int i = 0; i < complete.Rows.Count; i++)
            //    {
            //        string plinecode = complete.Rows[i]["PLINE_CODE"].ToString();
            //        string projectCode = complete.Rows[i]["PROJECT_CODE"].ToString();
            //        string productSeries = complete.Rows[i]["PRODUCT_SERIES"].ToString();
            //        int orderNum, comNum, periodNum;

            //        if (string.IsNullOrEmpty(projectCode)) continue;

            //        if (!string.IsNullOrEmpty(productSeries))
            //        {
            //            string _orderNum = theconn.GetValue(string.Format("select sum(GAMNG) from ISAP_DATA_PLAN where text1='{0}'", projectCode));
            //            orderNum = string.IsNullOrEmpty(_orderNum) ? 0 : Convert.ToInt32(_orderNum);
            //            string _comNum = theconn.GetValue(string.Format("select sum(instore_qty) from DATA_SN_COMPLETE_INSTORE t left join data_plan dp on t.plan_code=dp.plan_code where dp.project_code='{0}' and dp.product_series='{1}'", projectCode, productSeries));
            //            comNum = string.IsNullOrEmpty(_comNum) ? 0 : Convert.ToInt32(_comNum);
            //            string _periodNum = theconn.GetValue(string.Format("select sum(instore_qty) from DATA_SN_COMPLETE_INSTORE t left join data_plan dp on t.plan_code=dp.plan_code where dp.project_code='{0}' and dp.product_series='{1}' and t.work_date between to_date('{2}','yyyy-mm-dd hh24:mi:ss') and to_date('{3}','yyyy-mm-dd hh24:mi:ss')", projectCode, productSeries, _b, _e));
            //            periodNum = string.IsNullOrEmpty(_periodNum) ? 0 : Convert.ToInt32(_periodNum);
            //        }
            //        else
            //        {
            //            string _orderNum = theconn.GetValue(string.Format("select sum(GAMNG) from ISAP_DATA_PLAN where text1='{0}'", projectCode));
            //            orderNum = string.IsNullOrEmpty(_orderNum) ? 0 : Convert.ToInt32(_orderNum);
            //            string _comNum = theconn.GetValue(string.Format("select sum(instore_qty) from DATA_SN_COMPLETE_INSTORE t left join data_plan dp on t.plan_code=dp.plan_code where dp.project_code='{0}' and dp.product_series is null", projectCode));
            //            comNum = string.IsNullOrEmpty(_comNum) ? 0 : Convert.ToInt32(_comNum);
            //            string _periodNum = theconn.GetValue(string.Format("select sum(instore_qty) from DATA_SN_COMPLETE_INSTORE t left join data_plan dp on t.plan_code=dp.plan_code where dp.project_code='{0}' and dp.product_series is null and t.work_date between to_date('{1}','yyyy-mm-dd hh24:mi:ss') and to_date('{2}','yyyy-mm-dd hh24:mi:ss')", projectCode, _b, _e));
            //            periodNum = string.IsNullOrEmpty(_periodNum) ? 0 : Convert.ToInt32(_periodNum);
            //        }
            //        dt.Rows.Add(pline_name, projectCode, productSeries, orderNum, comNum, periodNum);
            //    }
            //}
            //Session["data"] = dt;

        }
    }
}