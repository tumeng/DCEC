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
using DevExpress.Web.ASPxClasses;

namespace Rmes.WebApp.Rmes.Exp.Exp2000
{
    public partial class Exp2000 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ASPxDateEdit1.Date = DateTime.Now;
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
            string _dept = com_DEPT.SelectedItem.Value.ToString();
            List<ProductLineEntity> plines = DB.GetInstance().Fetch<ProductLineEntity>("where pline_code in (select pline_code from REL_DEPT_PLINE where dept_code=@0)",_dept);
            ASPxComboBox1.DataSource = plines;
            ASPxComboBox1.TextField = "PLINE_NAME";
            ASPxComboBox1.ValueField = "PLINE_CODE";
            ASPxComboBox1.DataBind();
            if (ASPxComboBox1.SelectedItem == null) return;
            Report_Exp2000_1 report = new Report_Exp2000_1(ASPxComboBox1.SelectedItem.Value.ToString(), com_DEPT.SelectedItem.Value.ToString(), ASPxDateEdit1.Date);
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