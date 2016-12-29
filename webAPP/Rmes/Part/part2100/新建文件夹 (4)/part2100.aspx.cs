using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxGridLookup;

using Rmes.Pub.Data;
using Rmes.Web.Base;
using DevExpress.Web.ASPxEditors;

/**
 * 功能概述：物料清单查询
 * 作者：任海
 * 创建时间：2016-08-24
 */
namespace Rmes.WebApp.Rmes.Part.part2100
{
    public partial class part2100 : BasePage
    {
        private dataConn dc = new dataConn();
        public string theProgramCode;
        public string theCompanyCode;
        private string theUserId;
        //private string BillCode = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            theProgramCode = "part2100";
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();

            if (!IsPostBack)
            {
                //初始化plineCode
                ComboGzdd.Value = "E";
                DateStart.Date = DateTime.Now;
                DateEnd.Date = DateTime.Now;

                initGzdd();
            }

            //if (ComboGzdd.Value != null)
            //{
            //往初始化combo的函数里加gzdd参数时使用
            //string gzdd = ComboGzdd.Value.ToString();
            //initBillCode();

            //initLocationCode();
            //initBgyCode();

            //}
            setCondition();
            queryPlan1();
        }

        //初始化GridView
        private void setCondition()
        {
             
            if (DateStart.Date.AddDays(62) < DateEnd.Date)
            {
                 return;

            }
            string sql = "";
            //if (ComboTable.Text.Trim() == "三方物流")
            //{
            sql = "SELECT R.ROWID,R.GZDD,R.BILL_CODE,R.MATERIAL_CODE,P.PT_DESC2,R.BGY_CODE,SUM(R.MATERIAL_NUM) AS SUM_MATERIAL_NUM,"
             + "R.LOCATION_CODE,R.GYS_CODE,V.VD_SORT,R.INV_CODE,R.PO_NBR,TO_CHAR(R.REQUIRE_TIME,'YYYY-MM-DD HH24:MI:SS') AS REQUIRE_TIME,R.BILL_REMARK "
             + " FROM MS_SFJIT_RESULT R "
             + " LEFT JOIN COPY_PT_MSTR P ON R.MATERIAL_CODE = P.PT_PART "
             + " LEFT JOIN COPY_VD_MSTR V ON R.GYS_CODE = V.VD_ADDR "
             + " WHERE 1=1 ";
            if (ComboGzdd.Text.Trim() != "")
            {
                sql += " AND R.GZDD = '" + ComboGzdd.Value.ToString() + "' ";
            }
            //若单据号为空则不显示数据
            //if (ComboBillCode.Text.Trim() == "")
            //{
            //    sql += " AND R.BILL_CODE = '' ";
            //}
            if (ComboBillCode.Text.Trim() != "")
            {
                sql += " AND R.BILL_CODE = '" + ComboBillCode.Text.Trim().ToUpper() + "' ";
            }
            if (ComboLocationCode.Text.Trim() != "")
            {
                sql += " AND R.LOCATION_CODE = '" + ComboLocationCode.Text.ToUpper() + "' ";
            }
            if (ComboBgyCode.Text.Trim() != "")
            {
                sql += " AND R.BGY_CODE = '" + ComboBgyCode.Text.ToUpper() + "' ";
            }
            if (TextMaterialCode.Text.Trim() != "")
            {
                sql += " AND R.MATERIAL_CODE like '%" + TextMaterialCode.Text + "%' ";
            }
            if (TextBillRemark.Text.Trim() != "")
            {
                sql += " AND R.BILL_REMARK like '%" + TextBillRemark.Text + "%' ";
            }
            if (DateStart.Text.Trim() != "")
            {
                sql += " AND REQUIRE_TIME >= TO_DATE('" + DateStart.Text + "','YYYY-MM-DD HH24:MI:SS') ";
            }
            if (DateEnd.Text.Trim() != "")
            {
                sql += " AND R.REQUIRE_TIME <= TO_DATE('" + DateEnd.Text + "','YYYY-MM-DD HH24:MI:SS') ";
            }
            sql += " GROUP BY R.ROWID,R.GZDD,R.BILL_CODE,R.MATERIAL_CODE,R.GYS_CODE,P.PT_DESC2,R.BGY_CODE,R.LOCATION_CODE,V.VD_SORT,R.INV_CODE,R.PO_NBR,R.REQUIRE_TIME,R.BILL_REMARK "
            + " ORDER BY R.BILL_CODE DESC";
            //}

            DataTable dt = dc.GetTable(sql);

            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();

            //给BillCode赋值
            //if (dt.Rows.Count > 0)
            //{
            //    string BillCode = dt.Rows[0]["BILL_CODE"] as string;
            //    queryPlan(BillCode);
            //}
            //else
            //{
            //    ASPxGridView2.DataSource = null;
            //    ASPxGridView2.DataBind();
            //}
        }

        //初始化生产线下拉列表
        private void initGzdd()
        {
            String sql = "SELECT DISTINCT PLINE_CODE, PLINE_NAME FROM VW_USER_ROLE_PROGRAM WHERE USER_ID = '" + theUserId
                + "' AND PROGRAM_CODE = '" + theProgramCode + "' AND COMPANY_CODE = '" + theCompanyCode + "' ORDER BY PLINE_CODE";
            sqlGzdd.SelectCommand = sql;
            sqlGzdd.DataBind();
            //ComboGzdd.SelectedIndex = ComboGzdd.Items.Count >= 0 ? 0 : -1;
        }

        //根据日期查询单据号
        protected void queryBillCode_Click(object sender, EventArgs e)
        {
            //查询单据号前清空一下单据号
            ComboBillCode.Items.Clear();

            string sql = "";
            sql = "SELECT DISTINCT BILL_CODE FROM MS_SFJIT_RESULT WHERE 1=1 ";
            if (ComboGzdd.Value != null)
            {
                sql += " AND GZDD = '" + ComboGzdd.Value.ToString() + "' ";
            }
            if (DateStart.Text.Trim() != "")
            {
                sql += " AND REQUIRE_TIME >= TO_DATE('" + DateStart.Text + "','YYYY-MM-DD HH24:MI:SS') ";
            }
            if (DateEnd.Text.Trim() != "")
            {
                sql += " AND REQUIRE_TIME <= TO_DATE('" + DateEnd.Text + "','YYYY-MM-DD HH24:MI:SS') ";
            }
            sql += " ORDER BY SUBSTR(BILL_CODE,-14,14) DESC ";

            sqlBillCode.SelectCommand = sql;
            sqlBillCode.DataBind();
        }

        //初始化单据号下拉列表
        //private void initBillCode()
        //{
        //    string sql = "";
        //    //if (ComboTable.Text.Trim() == "三方物流")
        //    //{
        //    sql = "SELECT DISTINCT BILL_CODE FROM MS_SFJIT_RESULT WHERE 1=1 ";
        //    if (ComboGzdd.Value != null)
        //    {
        //        sql += " AND GZDD = '" + ComboGzdd.Value.ToString() + "' ";
        //    }
        //    sql +=  " ORDER BY SUBSTR(BILL_CODE,-14,14)";
        //    //}
        //    //if (ComboTable.Text.Trim() == "内装")
        //    //{
        //    //    sql = "SELECT DISTINCT BILL_CODE FROM MS_KFJIT_RESULT WHERE GZDD IN "
        //    //    + "(SELECT PLINE_CODE FROM VW_USER_ROLE_PROGRAM WHERE USER_ID = '" + theUserId
        //    //    + "' AND PROGRAM_CODE = '" + theProgramCode + "' AND COMPANY_CODE = '" + theCompanyCode + "') "
        //    //    + " ORDER BY SUBSTR(BILL_CODE,-14,14)";
        //    //}
        //    sqlBillCode.SelectCommand = sql;
        //    sqlBillCode.DataBind();
        //}

        //初始化工位下拉列表
        private void initLocationCode()
        {
            string sql = "";
            //if (ComboTable.Text.Trim() == "三方物流")
            //{
            sql = "SELECT DISTINCT LOCATION_CODE FROM MS_SFJIT_RESULT WHERE 1=1 ";
            if (ComboGzdd.Value != null)
            {
                sql += " AND GZDD = '" + ComboGzdd.Value.ToString() + "' ";
            }
            sql += " ORDER BY LOCATION_CODE ";
            //}
            //if (ComboTable.Text.Trim() == "内装")
            //{
            //    sql = "SELECT DISTINCT LOCATION_CODE FROM MS_KFJIT_RESULT WHERE GZDD IN "
            //    + "(SELECT PLINE_CODE FROM VW_USER_ROLE_PROGRAM WHERE USER_ID = '" + theUserId
            //    + "' AND PROGRAM_CODE = '" + theProgramCode + "' AND COMPANY_CODE = '" + theCompanyCode + "')"
            //    + " AND BILL_CODE = '" + ComboBillCode.Text.Trim() + "' ORDER BY LOCATION_CODE";
            //}
            sqlLocationCode.SelectCommand = sql;
            sqlLocationCode.DataBind();
        }

        //初始化保管员下拉列表
        private void initBgyCode()
        {
            string sql = "SELECT DISTINCT BGY_CODE FROM MS_SFJIT_RESULT WHERE 1=1 ";
            if (ComboGzdd.Value != null)
            {
                sql += " AND GZDD = '" + ComboGzdd.Value.ToString() + "' ";
            }
            sql += " ORDER BY BGY_CODE";
            sqlBgyCode.SelectCommand = sql;
            sqlBgyCode.DataBind();
        }

        //导出EXCEL
        protected void btnXlsExport_Click(object sender, EventArgs e)
        {
            ASPxGridViewExporter1.WriteXlsToResponse("物料清单查询");
        }

        //根据单据号查找对应的计划
        private void queryPlan1()
        {
            string sql = "";
            sql = "SELECT P.SF_JIT_ID,P.PLAN_CODE,D.PLAN_SO,D.PLAN_QTY,P.CURRENT_LOCATION,TO_CHAR(P.REQUIRE_TIME,'YYYY-MM-DD HH24:MI:SS') AS REQUIRE_TIME "
                + " FROM MS_SFJIT_PLAN_LOG P,DATA_PLAN D WHERE P.GZDD = '" + ComboGzdd.Value.ToString() + "' ";
            //若单据号为空则不显示数据
            if (ComboBillCode.Text.Trim() == "")
            {
                sql += " AND P.SF_JIT_ID = '' ";
            }
            if (ComboBillCode.Text.Trim() != "")
            {
                sql += " AND P.SF_JIT_ID = '" + ComboBillCode.Text.Trim().ToUpper() + "' ";
            }
            sql += " AND P.PLAN_CODE = D.PLAN_CODE AND P.PLAN_NUM = D.PLAN_QTY ORDER BY D.BEGIN_DATE,D.PLAN_SEQ";

            DataTable dt = dc.GetTable(sql);
            ASPxGridView2.DataSource = dt;
            ASPxGridView2.DataBind();
        }

        protected void ASPxGridView2_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            queryPlan1();

            //if (e.Parameters == "-1")
            //{
            //    ASPxGridView2.DataSource = null;
            //    ASPxGridView2.DataBind();
            //    return;
            //}

            //int rowIndex = int.Parse(e.Parameters);
            //string masterKeyValue = "";
            ////判断对象为空不能用"",只能和null进行比较
            //if (ASPxGridView1.GetRowValues(rowIndex, "BILL_CODE") != null)
            //{
            //    masterKeyValue = ASPxGridView1.GetRowValues(rowIndex, "BILL_CODE").ToString();
            //}
            //queryPlan(masterKeyValue);
            //queryPlan(BillCode);
        }

        //根据单据号查找对应的计划
        private void queryPlan(string billCode)
        {
            string sql = "";
            sql = "SELECT P.SF_JIT_ID,P.PLAN_CODE,D.PLAN_SO,D.PLAN_QTY,P.CURRENT_LOCATION,TO_CHAR(P.REQUIRE_TIME,'YYYY-MM-DD HH24:MI:SS') AS REQUIRE_TIME "
            + " FROM MS_SFJIT_PLAN_LOG P,DATA_PLAN D WHERE P.GZDD = '" + ComboGzdd.Value.ToString() + "' "
            + " AND P.PLAN_CODE = D.PLAN_CODE AND P.PLAN_NUM = D.PLAN_QTY AND P.SF_JIT_ID = '" + billCode + "' ORDER BY D.BEGIN_DATE,D.PLAN_SEQ";

            DataTable dt = dc.GetTable(sql);
            ASPxGridView2.DataSource = dt;
            ASPxGridView2.DataBind();
        }

        //使用customcallback刷新数据，防止整个页面刷新
        protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            
            if (DateStart.Date.AddDays(62) < DateEnd.Date)
            {
                ASPxGridView1.JSProperties.Add("cpCallbackName", "Fail");
                ASPxGridView1.JSProperties.Add("cpCallbackRet", "选择日期范围不能超过60天，请重新选择！！");
                return;
 
            }

            setCondition();
        }

        //根据日期查询单据号
        protected void ComboBillCode_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            if (DateStart.Date.AddDays(62) < DateEnd.Date)
            {
                 return;

            }
            ASPxComboBox asp = (ASPxComboBox)sender;
            string sql = "";
            sql = "SELECT DISTINCT BILL_CODE FROM MS_SFJIT_RESULT WHERE 1=1 ";
            if (ComboGzdd.Value != null)
            {
                sql += " AND GZDD = '" + ComboGzdd.Value.ToString() + "' ";
            }
            if (DateStart.Text.Trim() != "")
            {
                sql += " AND REQUIRE_TIME >= TO_DATE('" + DateStart.Text + "','YYYY-MM-DD HH24:MI:SS') ";
            }
            if (DateEnd.Text.Trim() != "")
            {
                sql += " AND REQUIRE_TIME <= TO_DATE('" + DateEnd.Text + "','YYYY-MM-DD HH24:MI:SS') ";
            }
            sql += " ORDER BY SUBSTR(BILL_CODE,-14,14) DESC ";

            sqlBillCode.SelectCommand = sql;
            sqlBillCode.DataBind();
            asp.DataBind();
            //默认选择第一项，不然不知道是否刷新数据
            ComboBillCode.SelectedIndex = ComboBillCode.Items.Count >= 0 ? 0 : -1;
            //ASPxGridView1.DataSource = null;
            //ASPxGridView1.DataBind();
            //ASPxGridView2.DataSource = null;
            //ASPxGridView2.DataBind();
        }

    }
}