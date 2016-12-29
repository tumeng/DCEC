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

namespace Rmes.WebApp.Rmes.Exp.Exp2400
{
    public partial class Exp2400 : System.Web.UI.Page
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
            Report_Exp2400 report = new Report_Exp2400(time, com_DEPT.SelectedItem.Value.ToString());
            ReportViewer1.Report = report;
            Session.Remove("data");
        }

        protected void submit_Click(object sender, EventArgs e)
        {

        }
    }
}