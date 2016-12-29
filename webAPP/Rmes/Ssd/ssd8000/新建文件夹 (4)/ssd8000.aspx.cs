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

/**
 * 功能概述：计划信息综合查询
 * 作者：任海
 * 创建时间：2016-08-24
 */

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
            queryPlanLog();
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
            string sql = "SELECT A.*,B.USER_CODE,'' FZQTY FROM DATA_PLAN A LEFT JOIN CODE_USER B ON A.CREATE_USERID = B.USER_ID"
            + " WHERE BEGIN_DATE >= TO_DATE('" + beginDate + "','YYYY/MM/DD HH24:MI:SS')"
            + " AND BEGIN_DATE <= TO_DATE('" + endDate + "','YYYY/MM/DD HH24:MI:SS')"
            + " AND A.PLINE_CODE IN (SELECT PLINE_CODE FROM VW_USER_ROLE_PROGRAM WHERE USER_ID = '"
            + theUserId + "' AND PROGRAM_CODE = '" + theProgramCode + "' AND COMPANY_CODE = '" + theCompanyCode + "')"
            + " AND A.CONFIRM_FLAG = 'Y' "
            + " ORDER BY A.BEGIN_DATE, A.PLAN_SEQ ";
            DataTable dt = dc.GetTable(sql);

            //不让敏感字出现
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string remark1 = dt.Rows[i]["REMARK"].ToString();
                string planCode1 = dt.Rows[i]["PLAN_CODE"].ToString();
                sql = "select FUNC_GET_REMARK('" + planCode1 + "', '" + remark1 + "') from dual ";
                dc.setTheSql(sql);
                dt.Rows[i]["REMARK"] = dc.GetValue(); 
                string FZqty=dc.GetValue("select FUNC_GET_FZQTY('" + planCode1 + "') from dual ");
                dt.Rows[i]["FZQTY"] = FZqty; 
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

        //刷新计划log数据展示
        protected void ASPxGridView4_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            queryPlanLog();
        }

        //查询计划log
        private void queryPlanLog()
        {
            //ASPxGridView grid = (ASPxGridView)sender;

            //string beginDate = ASPxDateEdit1.Date.ToString("yyyy/MM/dd");
            //日期格式大小写必须严格按yyyyMMdd
            string endDate = ASPxDateEdit2.Date.ToString("yyyyMMdd");
            string sql = "SELECT A.* FROM DATA_PLANLOG A "
            + " WHERE 1=1 "
                //+ " AND BEGIN_DATE >= TO_DATE('" + beginDate + "','YYYY/MM/DD HH24:MI:SS')"
                //+ " AND BEGIN_DATE <= TO_DATE('" + endDate + "','YYYY/MM/DD HH24:MI:SS')"
                //ROUNTING_SITE对应原来的GZDD
            + " AND A.ROUNTING_SITE IN (SELECT PLINE_CODE FROM VW_USER_ROLE_PROGRAM WHERE USER_ID = '"
            + theUserId + "' AND PROGRAM_CODE = '" + theProgramCode + "' AND COMPANY_CODE = '" + theCompanyCode + "')"
            + " AND PLAN_CODE LIKE 'E" + endDate + "%' "
            + " ORDER BY A.CREATE_DATE, A.PLAN_CODE ";
            DataTable dt = dc.GetTable(sql);

            //这种方式还是不能分页
            //grid.DataSource = dt;
            //grid.DataBind();

            ASPxGridView4.DataSource = dt;
            ASPxGridView4.DataBind();
        }

        //用这种方法改变列颜色，取非0列时会报错
        /*protected void ASPxGridView1_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.VisibleIndex < 0) return;

            string sql;
            DataTable dt;
            string status;

            status = e.GetValue("SN_FLAG").ToString();
            if (status == "N")
            {
                e.Row.BackColor = System.Drawing.Color.Brown;
            }

            status = e.GetValue("CONFIRM_FLAG").ToString();
            if (status == "N")
            {
                e.Row.BackColor = System.Drawing.Color.Yellow;
            }

            status = e.GetValue("BOM_FLAG").ToString();
            if (status == "N")
            {
                e.Row.BackColor = System.Drawing.Color.Green;
            }

            status = e.GetValue("ITEM_FLAG").ToString();
            switch (status)
            {
                case "N":
                    e.Row.BackColor = System.Drawing.Color.Chartreuse;
                    break;
                //case "R":
                //    e.Row.BackColor = System.Drawing.Color.BurlyWood;
                //    break;
                //case "Y":
                //    e.Row.BackColor = System.Drawing.Color.CadetBlue;
                //    break;
            }

            //与THIRD_FLAG意义一样
            sql = " select plan_code from ms_sfjit_plan_log where gzdd IN (SELECT PLINE_CODE FROM VW_USER_ROLE_PROGRAM WHERE USER_ID = '"
                + theUserId + "' AND PROGRAM_CODE = '" + theProgramCode + "' AND COMPANY_CODE = '" + theCompanyCode + "')"
                + " and plan_code='" + e.GetValue("PLAN_CODE").ToString() + "' ";
            dc.setTheSql(sql);
            dt = dc.GetTable();
            if (dt.Rows.Count > 0)
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(128, 128, 64);
            }

            status = e.GetValue("RUN_FLAG").ToString();
            if (status == "N")
            {
                e.Row.BackColor = System.Drawing.Color.Red;
            }

            //sql = "select plan_code from ms_kfjit_plan_log where gzdd IN (SELECT PLINE_CODE FROM VW_USER_ROLE_PROGRAM WHERE USER_ID = '"
            //    + theUserId + "' AND PROGRAM_CODE = '" + theProgramCode + "' AND COMPANY_CODE = '" + theCompanyCode + "')"
            //    + " and plan_code='" + e.GetValue("PLAN_CODE").ToString() + "' ";
            //dc.setTheSql(sql);
            //dt = dc.GetTable();
            //if (dt.Rows.Count > 0)
            //{
            //    e.Row.BackColor = System.Drawing.Color.FromArgb(255, 0, 255);
            //}

            sql = "select SO from atpusofjb where so='" + e.GetValue("PLAN_SO").ToString() + "'";
            dc.setTheSql(sql);
            dt = dc.GetTable();
            if (dt.Rows.Count < 1)
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(0, 123, 123);
            }

            string tJhso = e.GetValue("PLAN_SO").ToString();
            string tJhdm = e.GetValue("PLAN_CODE").ToString();
            sql = " SELECT PLAN_CODE,PLAN_SO,CREATE_DATE,REMARK FROM DATA_PLANLOG where PLAN_SO='" + tJhso + "' and PLAN_CODE='" + tJhdm
                + "' AND ROUNTING_SITE IN (SELECT PLINE_CODE FROM VW_USER_ROLE_PROGRAM WHERE USER_ID = ' "
                + theUserId + "' AND PROGRAM_CODE = '" + theProgramCode + "' AND COMPANY_CODE = '" + theCompanyCode + "') "
                + " ORDER BY CREATE_DATE,PLAN_CODE ";
            dc.setTheSql(sql);
            dt = dc.GetTable();
            if (dt.Rows.Count > 0)
            {
                e.Row.BackColor = System.Drawing.Color.Cyan;
            }

        }*/

        //给列设置颜色要用这种方式
        //但是页面加载会变得特别慢，因为要进行两重循环，每一行的所有列都要进行一次循环，即列表中的每一个单元格都要循环一次
        protected void ASPxGridView1_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            //若不是PLAN_CODE列，则不执行下面的代码
            if (e.DataColumn.FieldName.ToString() != "PLAN_CODE" && e.DataColumn.FieldName.ToString() != "PRODUCT_MODEL")
            {
                return;
            }
            string tJhso = e.GetValue("PLAN_SO").ToString();
            string tJhdm = e.GetValue("PLAN_CODE").ToString();
            string sql = " SELECT PLAN_CODE,PLAN_SO,CREATE_DATE,REMARK FROM DATA_PLANLOG where PLAN_SO='" + tJhso + "' and PLAN_CODE='" + tJhdm
                + "' AND ROUNTING_SITE IN (SELECT PLINE_CODE FROM VW_USER_ROLE_PROGRAM WHERE USER_ID = '"
                + theUserId + "' AND PROGRAM_CODE = '" + theProgramCode + "' AND COMPANY_CODE = '" + theCompanyCode + "') "
                + " ORDER BY CREATE_DATE,PLAN_CODE ";
            dc.setTheSql(sql);
            DataTable dt = dc.GetTable();
            //如果是机型列则只需判断计划是否有过变更
            if (e.DataColumn.FieldName.ToString() == "PRODUCT_MODEL")
            {
                if (dt.Rows.Count > 0)
                {
                    e.Cell.BackColor = System.Drawing.Color.Cyan;
                }
                return;
            }
            //下面的改变计划代码列的颜色
            string status = e.GetValue("SN_FLAG").ToString();
            if (status == "N")
            {
                //给需要设置颜色的列设置颜色
                //流水号分配
                e.Cell.BackColor = System.Drawing.Color.Brown;
            }
            status = e.GetValue("CONFIRM_FLAG").ToString();
            if (status == "N")
            {
                //计划确认
                e.Cell.BackColor = System.Drawing.Color.Yellow;
            }
            status = e.GetValue("BOM_FLAG").ToString();
            if (status == "N")
            {
                //BOM转换
                e.Cell.BackColor = System.Drawing.Color.Green;
            }
            status = e.GetValue("ITEM_FLAG").ToString();
            if (status == "N")
            {
                //库房确认
                e.Cell.BackColor = System.Drawing.Color.Chartreuse;
            }
            sql = " select plan_code from ms_sfjit_plan_log where gzdd IN (SELECT PLINE_CODE FROM VW_USER_ROLE_PROGRAM WHERE USER_ID = '"
                + theUserId + "' AND PROGRAM_CODE = '" + theProgramCode + "' AND COMPANY_CODE = '" + theCompanyCode + "') "
                + " and plan_code='" + tJhdm + "' ";
            dc.setTheSql(sql);
            dt = dc.GetTable();
            if (dt.Rows.Count > 0)
            {
                //三方物料计算
                e.Cell.BackColor = System.Drawing.Color.FromArgb(128, 128, 64);
            }
            status = e.GetValue("RUN_FLAG").ToString();
            if (status == "N")
            {
                //计划执行
                e.Cell.BackColor = System.Drawing.Color.Red;
				//若计划未执行，则后面的都不再判断
                return;
            }
            //sql = " select plan_code from ms_kfjit_plan_log where gzdd IN (SELECT PLINE_CODE FROM VW_USER_ROLE_PROGRAM WHERE USER_ID = '"
            //    + theUserId + "' AND PROGRAM_CODE = '" + theProgramCode + "' AND COMPANY_CODE = '" + theCompanyCode + "') "
            //    + " and plan_code='" + tJhdm + "' ";
            //dc.setTheSql(sql);
            //dt = dc.GetTable();
            //if (dt.Rows.Count > 0)
            //{
            //    e.Cell.BackColor = System.Drawing.Color.FromArgb(255, 0, 255);
            //}
            sql = " select SO from atpusofjb where so='" + tJhso + "' ";
            dc.setTheSql(sql);
            dt = dc.GetTable();
            if (dt.Rows.Count < 1)
            {
                //随机带走件
                e.Cell.BackColor = System.Drawing.Color.FromArgb(0, 123, 123);
            }

            /*string tJhso = e.GetValue("PLAN_SO").ToString();
            string tJhdm = e.GetValue("PLAN_CODE").ToString();
            string sql = " SELECT PLAN_CODE,PLAN_SO,CREATE_DATE,REMARK FROM DATA_PLANLOG where PLAN_SO='" + tJhso + "' and PLAN_CODE='" + tJhdm
                + "' AND ROUNTING_SITE IN (SELECT PLINE_CODE FROM VW_USER_ROLE_PROGRAM WHERE USER_ID = ' "
                + theUserId + "' AND PROGRAM_CODE = '" + theProgramCode + "' AND COMPANY_CODE = '" + theCompanyCode + "') "
                + " ORDER BY CREATE_DATE,PLAN_CODE ";
            dc.setTheSql(sql);
            DataTable dt = dc.GetTable();
            if (dt.Rows.Count > 0)
            {
                //一些不需设置颜色的列
                //if (e.DataColumn.Caption.ToString() == "计划确认标识") return;
                //if (e.DataColumn.Caption.ToString() == "BOM转换") return;
                //if (e.DataColumn.Caption.ToString() == "库房确认标识") return;
                //if (e.DataColumn.Caption.ToString() == "是否分配流水号") return;
                //if (e.DataColumn.Caption.ToString() == "三方要料") return;
                //if (e.DataColumn.Caption.ToString() == "库房要料") return;
                //if (e.DataColumn.Caption.ToString() == "三方物料接收状态") return;
                //if (e.DataColumn.Caption.ToString() == "库房发料接收状态") return;
                //if (e.DataColumn.Caption.ToString() == "生产确认标识") return;
                //if (e.DataColumn.Caption.ToString() == "柳汽标识") return;
                //if (e.DataColumn.Caption.ToString() == "改制返修是否转BOM") return;
                //也可以根据FieldName进行判断
                //if (e.DataColumn.FieldName.ToString() == "ORDER_CODE") return;

                //给需要设置颜色的列设置颜色
                if (e.DataColumn.FieldName.ToString() == "PLINE_CODE")
                {
                    e.Cell.BackColor = System.Drawing.Color.Cyan;
                    return;
                }
            }

            string status = e.GetValue("RUN_FLAG").ToString();
            if (status == "N")
            {
                //一些不需设置颜色的列
                //if (e.DataColumn.Caption.ToString() == "计划确认标识") return;
                //if (e.DataColumn.Caption.ToString() == "BOM转换") return;
                //if (e.DataColumn.Caption.ToString() == "库房确认标识") return;
                //if (e.DataColumn.Caption.ToString() == "是否分配流水号") return;
                //if (e.DataColumn.Caption.ToString() == "三方要料") return;
                //if (e.DataColumn.Caption.ToString() == "库房要料") return;
                //if (e.DataColumn.Caption.ToString() == "三方物料接收状态") return;
                //if (e.DataColumn.Caption.ToString() == "库房发料接收状态") return;
                //if (e.DataColumn.Caption.ToString() == "生产确认标识") return;
                //if (e.DataColumn.Caption.ToString() == "柳汽标识") return;
                //if (e.DataColumn.Caption.ToString() == "改制返修是否转BOM") return;

                //给需要设置颜色的列设置颜色
                if (e.DataColumn.FieldName.ToString() == "PLINE_NAME")
                {
                    e.Cell.BackColor = System.Drawing.Color.Red;
                    return;
                }
            }

            status = e.GetValue("CONFIRM_FLAG").ToString();
            if (status == "N")
            {
                //一些不需设置颜色的列
                //if (e.DataColumn.Caption.ToString() == "计划确认标识") return;
                //if (e.DataColumn.Caption.ToString() == "BOM转换") return;
                //if (e.DataColumn.Caption.ToString() == "库房确认标识") return;
                //if (e.DataColumn.Caption.ToString() == "是否分配流水号") return;
                //if (e.DataColumn.Caption.ToString() == "三方要料") return;
                //if (e.DataColumn.Caption.ToString() == "库房要料") return;
                //if (e.DataColumn.Caption.ToString() == "三方物料接收状态") return;
                //if (e.DataColumn.Caption.ToString() == "库房发料接收状态") return;
                //if (e.DataColumn.Caption.ToString() == "生产确认标识") return;
                //if (e.DataColumn.Caption.ToString() == "柳汽标识") return;
                //if (e.DataColumn.Caption.ToString() == "改制返修是否转BOM") return;

                //给需要设置颜色的列设置颜色
                if (e.DataColumn.FieldName.ToString() == "PLAN_BATCH")
                {
                    e.Cell.BackColor = System.Drawing.Color.Yellow;
                    return;
                }
            }

            status = e.GetValue("BOM_FLAG").ToString();
            if (status == "N")
            {
                //一些不需设置颜色的列
                //if (e.DataColumn.Caption.ToString() == "计划确认标识") return;
                //if (e.DataColumn.Caption.ToString() == "BOM转换") return;
                //if (e.DataColumn.Caption.ToString() == "库房确认标识") return;
                //if (e.DataColumn.Caption.ToString() == "是否分配流水号") return;
                //if (e.DataColumn.Caption.ToString() == "三方要料") return;
                //if (e.DataColumn.Caption.ToString() == "库房要料") return;
                //if (e.DataColumn.Caption.ToString() == "三方物料接收状态") return;
                //if (e.DataColumn.Caption.ToString() == "库房发料接收状态") return;
                //if (e.DataColumn.Caption.ToString() == "生产确认标识") return;
                //if (e.DataColumn.Caption.ToString() == "柳汽标识") return;
                //if (e.DataColumn.Caption.ToString() == "改制返修是否转BOM") return;

                //给需要设置颜色的列设置颜色
                if (e.DataColumn.FieldName.ToString() == "PLAN_SEQ")
                {
                    e.Cell.BackColor = System.Drawing.Color.Green;
                    return;
                }
            }

            sql = " select plan_code from ms_sfjit_plan_log where gzdd IN (SELECT PLINE_CODE FROM VW_USER_ROLE_PROGRAM WHERE USER_ID = ' "
                + theUserId + "' AND PROGRAM_CODE = '" + theProgramCode + "' AND COMPANY_CODE = '" + theCompanyCode + "') "
                + " and plan_code='" + tJhdm + "' ";
            dc.setTheSql(sql);
            dt = dc.GetTable();
            if (dt.Rows.Count > 0)
            {
                //给需要设置颜色的列设置颜色
                if (e.DataColumn.FieldName.ToString() == "PLAN_CODE")
                {
                    e.Cell.BackColor = System.Drawing.Color.FromArgb(128, 128, 64);
                }
            }

            sql = " select plan_code from ms_kfjit_plan_log where gzdd IN (SELECT PLINE_CODE FROM VW_USER_ROLE_PROGRAM WHERE USER_ID = ' "
                + theUserId + "' AND PROGRAM_CODE = '" + theProgramCode + "' AND COMPANY_CODE = '" + theCompanyCode + "') "
                + " and plan_code='" + tJhdm + "' ";
            dc.setTheSql(sql);
            dt = dc.GetTable();
            if (dt.Rows.Count > 0)
            {
                //给需要设置颜色的列设置颜色
                if (e.DataColumn.FieldName.ToString() == "PLAN_SO")
                {
                    e.Cell.BackColor = System.Drawing.Color.FromArgb(255, 0, 255);
                }
            }

            sql = " select SO from atpusofjb where so='" + tJhso + "' ";
            dc.setTheSql(sql);
            dt = dc.GetTable();
            if (dt.Rows.Count < 0)
            {
                //给需要设置颜色的列设置颜色
                if (e.DataColumn.FieldName.ToString() == "PRODUCT_MODEL")
                {
                    e.Cell.BackColor = System.Drawing.Color.FromArgb(0, 123, 123);
                }
            }*/
        }


    }
}