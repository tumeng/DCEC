using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//以下引用，如果采用实体类，请全部复制到新页面
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.Pub.Data;
using Rmes.Web.Base;
using System.Data;
using DevExpress.Web.ASPxGridView;

namespace Rmes.WebApp.Rmes.Exp.Exp1600
{
    public partial class Exp1600 : BasePage
    {
        private dataConn dc = new dataConn();
        PubCs thePubCs = new PubCs();
        public Database db = DB.GetInstance();
        protected void Page_Load(object sender, EventArgs e)
        {
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
            List<ProductLineEntity> plines = DB.GetInstance().Fetch<ProductLineEntity>("where pline_code in (select pline_code from REL_DEPT_PLINE where dept_code=@0)", _dept);
            ASPxComboBox1.DataSource = plines;
            ASPxComboBox1.TextField = "PLINE_NAME";
            ASPxComboBox1.ValueField = "PLINE_CODE";
            ASPxComboBox1.DataBind();
            if (ASPxComboBox1.SelectedItem == null) return;
            string _pline = ASPxComboBox1.SelectedItem.Value.ToString();
            List<ItemReturnEntity> returns = db.Fetch<ItemReturnEntity>("where order_code in (select order_code from data_plan where pline_code=@0)", _pline);
            //List<UserEntity> allusers = UserFactory.GetAll();
            //GridViewDataComboBoxColumn create_user = ASPxGridView1.Columns["CREATE_USER"] as GridViewDataComboBoxColumn;
            //create_user.PropertiesComboBox.DataSource = allusers;
            //create_user.PropertiesComboBox.ValueField = "USER_ID";
            //create_user.PropertiesComboBox.TextField = "USER_NAME";
            DataTable dt = new DataTable();
            dt.Columns.Add("ITEM_CODE");
            dt.Columns.Add("ITEM_NAME");
            dt.Columns.Add("ITEM_QTY");
            dt.Columns.Add("CREATE_USER");
            dt.Columns.Add("ORDER_CODE");
            dt.Columns.Add("STORE_CODE_1");
            dt.Columns.Add("STORE_CODE_2");
            dt.Columns.Add("WORK_TIME");
            dt.Columns.Add("PLAN_SO");
            dt.Columns.Add("PROJECT_CODE");
            for (int i = 0; i < returns.Count; i++)
            {
                string _order = returns[i].ORDER_CODE.ToString();
                PlanEntity entity = PlanFactory.GetByOrder(_order);
                //dt.Rows.Add(returns[i].ITEM_CODE.ToString(),returns[i].ITEM_NAME.ToString(),returns[i].ITEM_QTY.ToString(),returns[i].CREATE_USER.ToString(),returns[i].ORDER_CODE.ToString(),returns[i].STORE_CODE_1.ToString(),returns[i].STORE_CODE_2.ToString(),returns[i].WORK_TIME.ToString(),entity.PLAN_SO.ToString(),entity.PROJECT_CODE.ToString());
            }
            Report_Exp1600 re = new Report_Exp1600(dt);
            ReportViewer1.Report = re;
            //    ASPxGridView1.DataSource = returns;
            //ASPxGridView1.DataBind();
        }
        public void ASPxButton1_Click(object sender, EventArgs e)
        {

            //BindData();

        }

        //protected void btnXlsExport_Click(object sender, EventArgs e)
        //{
        //    ASPxGridViewExporter1.WriteXlsToResponse("退料信息导出");
        //}
        protected void com_DEPT_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}