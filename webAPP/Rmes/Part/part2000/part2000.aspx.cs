using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DevExpress.Web.ASPxGridView;

using System.Data;
using Rmes.Pub.Data;
using DevExpress.Web.ASPxClasses;
using Rmes.Web.Base;
using DevExpress.Web.ASPxGridLookup;

/**
 * 功能概述：JIT特殊物料查询
 * 作者：任海
 * 创建时间：2016-08-23
 * 修改时间：2016-12-02 YXH 单据号下拉列表需要根据生产线的变化而变化
 */
namespace Rmes.WebApp.Rmes.Part.part2000
{
    public partial class part2000 : BasePage
    {
        private dataConn dc = new dataConn();
        public string theCompanyCode;
        private string theUserId;
        public string theProgramCode;
        //生产线全局变量
        private string plineCode;
        private string createTime;

        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();
            theProgramCode = "part2000";

            if (!IsPostBack)
            {
                //plineCode = "E";
                createTime = DateTime.Now.ToString();
                initGzdd();
                Session["part2000sql"] = "";
            }
            else
            {
                createTime = "";
            }
            if (Session["part2000sql"] as string != "")
            {
                string sql2 = Session["part2000sql"] as string;
                sqlBillCode.SelectCommand = sql2;
                sqlBillCode.DataBind();
            }
            else { initGzdd(); }
            
            setCondition();
            //if (ComboGzdd.Text.Trim() != "")
            //{
            //    plineCode = ComboGzdd.Value.ToString();
            //}

            //initBillCode();
            

            //if (ComboBillCode.Text.Trim() != "" && ComboGzdd.Text.Trim() != "")
            //{
            //    String sql = "SELECT T.*,ROWID FROM MS_SFJIT_RESULT_PO T ";
            //    sql += " WHERE 1 = 1 ";
            //    //测试“算术运算导致溢出”的错误是否是因为数据量太大而引起
            //    //sql += " AND ROWNUM <= 100 ";
            //    //if (ComboGzdd.Text.Trim() != "")
            //    //{
            //        sql += " AND GZDD = '" + plineCode + "' ";
            //    //}
            //    if (ComboBillCode.Text.Trim() != "")
            //    {
            //        sql += " AND BILL_CODE = '" + ComboBillCode.Text.Trim() + "' ";
            //    }
            //    if (createTime != "")
            //    {
            //        //防止数据量太大而报错
            //        sql += " AND CREATE_TIME >= TO_DATE('" + createTime + "', 'YYYY-MM-DD HH24:MI:SS') ";
            //    }
            //    sql += " ORDER BY LOCATION_CODE,MATERIAL_CODE ";

            //    DataTable dt = dc.GetTable(sql);
            //    ASPxGridView1.DataSource = dt;
            //    ASPxGridView1.DataBind();
            //}

        }

        //初始化GridView
        private void setCondition()
        {
            String sql = "SELECT T.*,ROWID FROM MS_SFJIT_RESULT_PO T ";
            sql += " WHERE 1 =1 ";
            //TEST
            //sql += " AND ROWNUM <= 100 ";
            if (ComboGzdd.Text.Trim() != "")
            {
                sql += " AND GZDD = '" + ComboGzdd.Value.ToString() + "' ";
            }
            //不选择BILL_CODE则设为空，即不查询数据
            if (ComboBillCode.Text == "" || ComboBillCode.Text == null)
            {
                sql += " AND BILL_CODE = '' ";
            }
            if (ComboBillCode.Text.Trim() != "")
            {
                sql += " AND BILL_CODE = '" + ComboBillCode.Text + "' ";
            }
            sql += " ORDER BY LOCATION_CODE,MATERIAL_CODE ";

            DataTable dt = dc.GetTable(sql);
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
        }

        //初始化生产线下拉框（PLINE_NAME其实不需要，也就不需要LFFT JOIN CODE_PRODUCT_LINE，因为MS_SFJIT_RESULT_PO表中输入的生产线就是E/W）
        private void initGzdd()
        {
            String sql = "SELECT DISTINCT A.PLINE_CODE,B.PLINE_NAME FROM VW_USER_ROLE_PROGRAM A LEFT JOIN CODE_PRODUCT_LINE B "
                + " ON A.PLINE_CODE = B.PLINE_CODE WHERE A.USER_ID = '" + theUserId + "' AND A.PROGRAM_CODE = '" + theProgramCode
                + "' AND A.COMPANY_CODE = '" + theCompanyCode + "' ORDER BY A.PLINE_CODE";
            sqlGzdd.SelectCommand = sql;
            sqlGzdd.DataBind();
            //YXH 2016.12.07
            DataTable dt = dc.GetTable(sql);
            string sql2 = "";
            if (dt.Rows.Count > 0)
            {
                sql2 = " SELECT DISTINCT BILL_CODE FROM MS_SFJIT_RESULT_PO WHERE GZDD= '" + dt.Rows[0][0].ToString() + "'  ";
            }
            else
            {
                sql2 = " SELECT DISTINCT BILL_CODE FROM MS_SFJIT_RESULT_PO WHERE GZDD in(SELECT DISTINCT  A.PLINE_CODE  FROM VW_USER_ROLE_PROGRAM A WHERE A.USER_ID = '" + theUserId + "' AND A.PROGRAM_CODE = '" + theProgramCode + "' AND A.COMPANY_CODE = '" + theCompanyCode + "' )";
            }
            sql2 += " ORDER BY SUBSTR(BILL_CODE,-14,14) DESC ";
            
            sqlBillCode.SelectCommand = sql2;
            sqlBillCode.DataBind();
        }

        //初始化查询的单据号下拉框
        private void initBillCode()
        {
            String sql = "SELECT DISTINCT BILL_CODE FROM MS_SFJIT_RESULT_PO WHERE GZDD = '" + plineCode + "' ";
            sql += " ORDER BY SUBSTR(BILL_CODE,-14,14) DESC ";
            //if (ComboGzdd.Text.Trim() != "")
            //{
            //    sql += " WHERE GZDD = '" + ComboGzdd.Value.ToString() + "' ";
            //}

            sqlBillCode.SelectCommand = sql;
            sqlBillCode.DataBind();
        }

        //查询  不需要这个函数
        //private void ASPxGridView1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        //{
        //    setCondition();
        //    ASPxGridView1.Selection.UnselectAll();
        //}

        //导出EXCEL
        protected void btnXlsExport_Click(object sender, EventArgs e)
        {
            ASPxGridViewExporter1.WriteXlsToResponse("JIT特殊物料查询");
        }

        protected void ASPxCallbackPanel4_Callback(object sender, CallbackEventArgsBase e)
        {
            string sql2 = "";
            if (e.Parameter != "")
            {
                sql2 = " SELECT DISTINCT BILL_CODE FROM MS_SFJIT_RESULT_PO WHERE GZDD= '" + e.Parameter + "'  ";
            }
            else
            {
                sql2 = " SELECT DISTINCT BILL_CODE FROM MS_SFJIT_RESULT_PO WHERE GZDD in(SELECT DISTINCT  A.PLINE_CODE  FROM VW_USER_ROLE_PROGRAM A WHERE A.USER_ID = '" + theUserId + "' AND A.PROGRAM_CODE = '" + theProgramCode + "' AND A.COMPANY_CODE = '" + theCompanyCode + "' )";
            }
            sql2 += " ORDER BY SUBSTR(BILL_CODE,-14,14) DESC ";

            Session["part2000sql"] = sql2;
            sqlBillCode.SelectCommand = sql2;
            sqlBillCode.DataBind();
        }


    }
}
