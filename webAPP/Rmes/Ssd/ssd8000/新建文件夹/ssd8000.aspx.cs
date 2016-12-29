using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DevExpress.Utils;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxClasses;

using Rmes.Web.Base;

using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.Pub.Data;

namespace Rmes.WebApp.Rmes.Ssd.ssd8000
{
    public partial class ssd8000 : BasePage
    {
        private dataConn dc = new dataConn();
        public DateTime OracleSQLTime;

        private string theUserId;
        private string theCompanyCode;
        public string theProgramCode;

        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theUserId = theUserManager.getUserId();
            theCompanyCode = theUserManager.getCompanyCode();
            theProgramCode = "ssd8000";

            if (!IsPostBack)
            {
                OracleSQLTime = DB.GetInstance().ExecuteScalar<DateTime>("Select sysdate from dual");

                ASPxDateEdit1.Date = DateTime.Now;
                ASPxDateEdit2.Date = DateTime.Now;
            }

            setCondition();
            //grid里面用不着combo显示，所以不需要
            //initPlineCode();
            //grid里面用不着combo显示，所以不需要
            //initPlanCode();
        }

        //查询计划
        private void setCondition()
        {
            string beginDate = ASPxDateEdit1.Date.ToString("yyyy/MM/dd");
            string endDate = ASPxDateEdit2.Date.ToString("yyyy/MM/dd");
            string sql = "SELECT A.*,B.USER_CODE FROM DATA_PLAN A LEFT JOIN CODE_USER B ON A.CREATE_USERID = B.USER_ID"
            + " WHERE BEGIN_DATE >= TO_DATE('" + beginDate + "','YYYY/MM/DD HH24:MI:SS')"
            + " AND END_DATE <= TO_DATE('" + endDate + "','YYYY/MM/DD HH24:MI:SS')"
            + " AND A.PLINE_CODE IN (SELECT PLINE_CODE FROM VW_USER_ROLE_PROGRAM WHERE USER_ID = '"
            + theUserId + "' AND PROGRAM_CODE = '" + theProgramCode + "' AND COMPANY_CODE = '" + theCompanyCode + "')"
            + " ORDER BY A.BEGIN_DATE DESC, A.PLAN_SEQ DESC ";
            DataTable dt = dc.GetTable(sql);

            //不让敏感字出现
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string remark1 = dt.Rows[i]["REMARK"].ToString();
                string planCode1 = dt.Rows[i]["PLAN_CODE"].ToString();
                sql = "select FUNC_GET_REMARK('" + planCode1 + "', '" + remark1 + "') from dual ";
                dc.setTheSql(sql);
                dt.Rows[i]["REMARK"] = dc.GetValue(); ;
            }

            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
        }

        //初始化生产线代码列表
        private void initPlineCode()
        {
            string sql = "SELECT DISTINCT PLINE_CODE FROM DATA_PLAN WHERE PLINE_CODE IN (SELECT PLINE_CODE FROM VW_USER_ROLE_PROGRAM WHERE USER_ID = '"
                + theUserId + "' AND PROGRAM_CODE = '" + theProgramCode + "' AND COMPANY_CODE = '" + theCompanyCode + "')";
            boxPline.SelectCommand = sql;
            boxPline.DataBind();
        }

        //初始化计划编号列表
        private void initPlanCode()
        {
            string sql = "SELECT DISTINCT PLAN_CODE FROM DATA_PLAN";
            boxPlan.SelectCommand = sql;
            boxPlan.DataBind();
        }

        //显示物料清单
        protected void ASPxGridView2_DataSelect(object sender, EventArgs e)
        {
            ASPxGridView grid = (ASPxGridView)sender;
            string planCode = grid.GetMasterRowFieldValues("PLAN_CODE").ToString();
            string plineCode = grid.GetMasterRowFieldValues("PLINE_CODE").ToString();

            string sql = "SELECT * FROM DATA_PLAN_STANDARD_BOM WHERE PLAN_CODE = '" + planCode + "' AND PLINE_CODE = '" + plineCode + "'";
            //string sql = "SELECT A.RMES_ID,A.COMPANY_CODE,A.PLAN_CODE,A.PLINE_CODE,"
            //    +"(SELECT B.LOCATION_NAME FROM CODE_LOCATION B WHERE B.LOCATION_CODE = (SELECT A.LOCATION_CODE FROM DATA_PLAN_STANDARD_BOM A"
            //    + " WHERE A.PLAN_CODE = '" + planCode + "' AND A.PLINE_CODE = '" + plineCode + "')) AS LOCATION_NAME,A.PROCESS_CODE,A.PROCESS_SEQ,A.ITEM_CODE,"
            //    + "A.ITEM_NAME,A.ITEM_QTY,A.ITEM_CLASS,A.ITEM_TYPE,A.VENDOR_CODE,A.CREATE_TIME,A.CREATE_USERID FROM DATA_PLAN_STANDARD_BOM A"
            //    + " WHERE A.PLAN_CODE = '" + planCode + "' AND A.PLINE_CODE = '" + plineCode + "'";
            DataTable planBom = dc.GetTable(sql);

            grid.DataSource = planBom;
            //grid.DataBind();
        }

        //显示分配流水
        protected void ASPxGridView3_DataSelect(object sender, EventArgs e)
        {
            ASPxGridView grid = (ASPxGridView)sender;
            string planCode = grid.GetMasterRowFieldValues("PLAN_CODE").ToString();
            string plineCode = grid.GetMasterRowFieldValues("PLINE_CODE").ToString();

            string sql = "SELECT * FROM DATA_PLAN_SN WHERE PLAN_CODE = '" + planCode + "' AND PLINE_CODE = '" + plineCode + "'";
            DataTable snDist = dc.GetTable(sql);

            grid.DataSource = snDist;
            //grid.DataBind();
        }

        //导出计划表 EXCEL
        protected void btnXlsExport_Click1(object sender, EventArgs e)
        {
            ////ASPxGridView subGrid = (ASPxGridView)this.masterGrid.Items[intRow].FindControl("subGrid");
            //string plineName = Convert.ToString(ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "PLINE_NAME"));
            //Console.WriteLine(plineName);

            //ASPxGridView grid = (ASPxGridView)this.ASPxGridView1;
            //int rowNum = Convert.ToInt16(ASPxGridView1.FocusedRowIndex);
            //grid.GetRow(rowNum);
            ////ASPxGridView1.Columns[3].Visible = false;

            //setCondition();
            ASPxGridViewExporter1.WriteXlsToResponse("Plan_List");
        }

        //导出物料清单 EXCEL
        protected void btnXlsExport_Click2(object sender, EventArgs e)
        {
            ASPxGridViewExporter2.WriteXlsToResponse("Plan_BOM_List");
        }

        //导出分配流水号 EXCEL
        protected void btnXlsExport_Click3(object sender, EventArgs e)
        {
            ASPxGridViewExporter3.WriteXlsToResponse("SN_List");
        }

        //查询计划
        //protected void queryPlan_Click(object sender, EventArgs e)
        //{
        //    setCondition();
        //}

        public string ConvertFormat(string str)
        {//内容为我们需要转换成的格式

            string result = "";
            if (str.Length <= 10)
            {
                result = str;
            }
            else
            {
                result = str.Substring(0, 10) + "......";
            }
            return result;

        }
        protected void callbackPanel_Callback(object sender, CallbackEventArgsBase e)
        {
            litText.Text = GetNotes(e.Parameter).ToString();

        }
        object GetNotes(string id)
        {
            string sql = "SELECT * FROM DATA_PLAN WHERE  RMES_ID='" + id + "'";
            dataConn dc = new dataConn(sql);
            DataTable dt = dc.GetTable();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string remark1 = dt.Rows[i]["REMARK"].ToString();
                string planCode1 = dt.Rows[i]["PLAN_CODE"].ToString();
                sql = "select FUNC_GET_REMARK('" + planCode1 + "', '" + remark1 + "') from dual ";
                dc.setTheSql(sql);
                dt.Rows[i]["REMARK"] = dc.GetValue(); ;
            }
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["REMARK"];
            }
            return null;
        }
    }
}