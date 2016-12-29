using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rmes.DA.Entity;
using Rmes.DA.Base;
using Rmes.DA.Factory;

namespace Rmes.WebApp.Rmes.Exp.Exp2200
{
    public partial class Exp2200 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ASPxDateEdit1.Date = DateTime.Now.AddDays(-7);
                ASPxDateEdit2.Date = DateTime.Now;
            }
            BindData();
            
        }

        public void ASPxComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        public void ASPxComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void BindData()
        {
            List<DepartmentEntity> dept = DB.GetInstance().Fetch<DepartmentEntity>("");
            com_DEPT.DataSource = dept;
            com_DEPT.ValueField = "DEPT_CODE";
            com_DEPT.TextField = "DEPT_NAME";
            com_DEPT.DataBind();
            if (com_DEPT.SelectedItem == null) return;
            string _dept = com_DEPT.SelectedItem.Value.ToString();
            List<ProductLineEntity> plines = DB.GetInstance().Fetch<ProductLineEntity>("where pline_code in (select pline_code from REL_DEPT_PLINE where dept_code=@0)", _dept);
            ASPxComboBox1.DataSource = plines;
            ASPxComboBox1.TextField = "PLINE_NAME";
            ASPxComboBox1.ValueField = "PLINE_CODE";
            ASPxComboBox1.DataBind();
            if (ASPxComboBox1.SelectedItem == null) return;
            string pline = ASPxComboBox1.SelectedItem.Value.ToString();
            List<StationEntity> stations = StationFactory.GetByProductLine(pline);
            ASPxComboBox2.DataSource = stations;
            ASPxComboBox2.TextField = "STATION_NAME";
            ASPxComboBox2.ValueField = "WORKUNIT_CODE";
            ASPxComboBox2.DataBind();
            string workunit = "";
            if(ASPxComboBox2.SelectedItem!=null)
                workunit = ASPxComboBox2.SelectedItem.Value.ToString();
            Report_Exp2200 report = new Report_Exp2200( ASPxComboBox1.SelectedItem.Value.ToString(),workunit, ASPxDateEdit1.Date, ASPxDateEdit2.Date);
            ReportViewer1.Report = report;

        }

        protected void submit_Click(object sender, EventArgs e)
        {
        }

        protected void com_DEPT_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}