using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rmes.DA.Procedures;
using System.Data;

using Rmes.Web.Base;
using Rmes.Pub.Data;

using DevExpress.Utils;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxClasses;
using Rmes.DA.Base;

/**
 * 功能概述：在制品物料清单
 * 作者：任海
 * 创建时间：2016-09-18
 */
namespace Rmes.WebApp.Rmes.Rept.rept3200
{
    public partial class rept3200 : BasePage
    {
        private dataConn dc = new dataConn();
        public string theProgramCode;
        public string theCompanyCode;
        private string theUserId;

        protected void Page_Load(object sender, EventArgs e)
        {
            theProgramCode = "rept3200";
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();

            string sql = "select distinct a.pline_code,b.pline_name from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'";
            SqlCode.SelectCommand = sql;
            SqlCode.DataBind();
            SqlCode1.SelectCommand = sql;
            SqlCode1.DataBind();

            if (!IsPostBack)
            {
                //第一次进入页面默认取东区
                txtPCode.SelectedIndex = 0;

                //第一次进入页面取不到生产线，用这种方式给ASPxGridView1增加数据
                //string plineSql = "select distinct a.pline_code,b.pline_name from vw_user_role_program a "
                //+ " left join code_product_line b on a.pline_code=b.pline_code where a.user_id='"
                //+ theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'";
                //DataTable dt1 = dc.GetTable(plineSql);
                ////注意此处用rows才能取到，用columns取不到
                //string pline = dt1.Rows[0]["PLINE_CODE"].ToString();
                //string inSqlTemp = "";
                //inSqlTemp = " SELECT DISTINCT SN FROM DATA_STORE WHERE PLINE_CODE = '" + pline + "' ";

                //DataTable dt = dc.GetTable(inSqlTemp);
                //ASPxGridView1.DataSource = dt;
                //ASPxGridView1.DataBind();
            }
            //initPlineCode();
            //放在pageload里，模糊查询，分页等功能才能正常使用，因为这些事件都会来后台处理数据，所以要查询的相关数据必须要放在pageload里，否则
            //无法实现正常功能
            setCondition1();
            setCondition();
        }

        //初始化生产线
        private void initPlineCode()
        {
            string sql = "select distinct a.pline_code,b.pline_name from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'";
            SqlCode.SelectCommand = sql;
            SqlCode.DataBind();
            SqlCode1.SelectCommand = sql;
            SqlCode1.DataBind();
            //页面其他事件访问后台时会每次都将生产线都刷为E，这是不对的
            txtPCode.SelectedIndex = txtPCode.Items.Count >= 0 ? 0 : -1;
        }

        //初始化物料清单
        private void setCondition()
        {
            string sql = "";
            sql = " SELECT ITEM_CODE,SUM(ITEM_QTY) AS SUM FROM DATA_SN_BOM_TEMP WHERE 1=1 ";
            //若生产线为空，则取空值
            if (txtPCode.Text.Trim() == "")
            {
                sql += " AND PLINE_CODE = '' ";
            }
            if (txtPCode.Text.Trim() != "")
            {
                sql += " AND PLINE_CODE = '" + txtPCode.Value.ToString() + "' ";
            }
            //如果列表中没有条件，则查询的数据为空
            //后端取控件用ID，前端用ClientInstanceName
            if (ASPxGridView3.VisibleRowCount < 1)
            {
                sql += " AND SN = '' ";
            }
            if (ASPxGridView3.VisibleRowCount >= 1 && txtPCode.Text.Trim() != "")
            {
                sql += " AND SN IN (SELECT SN FROM DATA_SN_BOM_TEMP_SNTEMP WHERE PLINE_CODE = '" + txtPCode.Value.ToString() + "' AND FLAG = 'ADD') ";
            }
            sql += " GROUP BY ITEM_CODE ORDER BY ITEM_CODE ";

            DataTable dt = dc.GetTable(sql);
            ASPxGridView2.DataSource = dt;
            ASPxGridView2.DataBind();
        }

        //在制品物料清单查询
        protected void ASPxGridView2_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            string flag = e.Parameters;
            if (flag == "QUERY")
            {
                setCondition();
            }
            if (flag == "CLEAR")
            {
                ASPxGridView2.DataSource = null;
                ASPxGridView2.DataBind();
            }
        }

        //未选流水号gridview展示数据
        protected void ASPxGridView1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string oFlag, pline;

            char[] charSeparators = new char[] { ',' };
            string[] collection = e.Parameters.Split(charSeparators);

            oFlag = collection[0].ToString();
            pline = collection[1].ToString();

            string sql = "";
            if (oFlag == "FIRST")
            { 
                //放在这里无效
                if (pline == "")
                {
                    string plineSql = "select distinct a.pline_code,b.pline_name from vw_user_role_program a "
                    + " left join code_product_line b on a.pline_code=b.pline_code where a.user_id='"
                    + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'";
                    DataTable dt1 = dc.GetTable(plineSql);
                    //注意此处用rows才能取到，用columns取不到
                    pline = dt1.Rows[0]["PLINE_CODE"].ToString();
                }
                if (pline != "")
                {
                    string inSqlTemp = "";
                    inSqlTemp = " TRUNCATE TABLE DATA_SN_BOM_TEMP_SNTEMP ";
                    dc.ExeSql(inSqlTemp);
                    inSqlTemp = " INSERT INTO DATA_SN_BOM_TEMP_SNTEMP(SN) SELECT DISTINCT SN FROM DATA_STORE WHERE PLINE_CODE = '" + pline + "' ";
                    dc.ExeSql(inSqlTemp);
                    inSqlTemp = " UPDATE DATA_SN_BOM_TEMP_SNTEMP SET PLINE_CODE = '" + pline + "' ";
                    dc.ExeSql(inSqlTemp);
                    inSqlTemp = " UPDATE DATA_SN_BOM_TEMP_SNTEMP SET FLAG = 'DEL' ";
                    dc.ExeSql(inSqlTemp);
                    sql += " SELECT SN FROM DATA_SN_BOM_TEMP_SNTEMP WHERE PLINE_CODE = '" + pline + "' AND FLAG = 'DEL' ORDER BY SN ";
                }
                DataTable dt = dc.GetTable(sql);

                ASPxGridView gridview1 = sender as ASPxGridView;
                gridview1.DataSource = dt;
                gridview1.DataBind();
                gridview1.Selection.UnselectAll();
            }
            if (oFlag == "NOTFIRST")
            {
                setCondition1();
                ASPxGridView1.Selection.UnselectAll();
            }
        }
        
        //初始化流水号列表
        private void setCondition1()
        {
            string pline="",sql = "";
            if (txtPCode.Text.Trim() != "")
            {
                pline = txtPCode.Value.ToString();
                sql += " SELECT SN FROM DATA_SN_BOM_TEMP_SNTEMP WHERE PLINE_CODE = '" + pline + "' AND FLAG = 'DEL' ORDER BY SN ";
            }
            //第一次访问页面
            if (txtPCode.Text.Trim() == "")
            {
                string plineSql = "select distinct a.pline_code,b.pline_name from vw_user_role_program a "
                + " left join code_product_line b on a.pline_code=b.pline_code where a.user_id='"
                + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'";
                DataTable dt1 = dc.GetTable(plineSql);
                //注意此处用rows才能取到，用columns取不到
                pline = dt1.Rows[0]["PLINE_CODE"].ToString();
                string inSqlTemp = "";
                inSqlTemp = " TRUNCATE TABLE DATA_SN_BOM_TEMP_SNTEMP ";
                dc.ExeSql(inSqlTemp);
                inSqlTemp = " INSERT INTO DATA_SN_BOM_TEMP_SNTEMP(SN) SELECT DISTINCT SN FROM DATA_STORE WHERE PLINE_CODE = '" + pline + "' ";
                dc.ExeSql(inSqlTemp);
                inSqlTemp = " UPDATE DATA_SN_BOM_TEMP_SNTEMP SET PLINE_CODE = '" + pline + "' ";
                dc.ExeSql(inSqlTemp);
                inSqlTemp = " UPDATE DATA_SN_BOM_TEMP_SNTEMP SET FLAG = 'DEL' ";
                dc.ExeSql(inSqlTemp);
                sql += " SELECT SN FROM DATA_SN_BOM_TEMP_SNTEMP WHERE PLINE_CODE = '" + pline + "' AND FLAG = 'DEL' ORDER BY SN ";
            }

            DataTable dt = dc.GetTable(sql);
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
        }

        //已选流水号gridview展示数据
        protected void ASPxGridView3_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string pline = e.Parameters;

            string sql = "";
            if (pline != "")
            {
                sql += " SELECT SN FROM DATA_SN_BOM_TEMP_SNTEMP WHERE PLINE_CODE = '" + pline + "' AND FLAG = 'ADD' ORDER BY SN ";
            }
            else
            {
                sql += " SELECT SN FROM DATA_SN_BOM_TEMP_SNTEMP WHERE PLINE_CODE = '' ";
            }
            //如果是查看工位显示所有工位
            DataTable dt = dc.GetTable(sql);

            ASPxGridView gridview1 = sender as ASPxGridView;
            gridview1.DataSource = dt;
            gridview1.DataBind();
            gridview1.Selection.UnselectAll();
        }

        protected void ASPxCbSubmit_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
        {
            string oFlag, pline, sn;

            char[] charSeparators = new char[] { ',' };
            string[] collection = e.Parameter.Split(charSeparators);
            int cnt = collection.Length;

            oFlag = collection[0].ToString();
            pline = collection[1].ToString();

            if (oFlag == "ADD")
            {
                //获取流水号
                List<string> s = new List<string>();
                for (int i = 2; i < cnt; i++)
                {
                    s.Add(collection[i].ToString());
                }
                string[] s1 = s.ToArray();
                for (int i = 0; i < s1.Length; i++)
                {
                    sn = s1[i].ToString();
                    string inSqlTemp = "UPDATE DATA_SN_BOM_TEMP_SNTEMP SET FLAG = 'ADD' WHERE SN = '" + sn + "' AND PLINE_CODE = '" + pline + "' ";
                    dc.ExeSql(inSqlTemp);
                }
                e.Result = "OK,增加成功！";
                return;
            }

            if (oFlag == "DEL")
            {
                //获取流水号
                List<string> s = new List<string>();
                for (int i = 2; i < cnt; i++)
                {
                    s.Add(collection[i].ToString());
                }
                string[] s1 = s.ToArray();

                //if (ASPxGridView3.Selection.Count == 0)
                //{
                //    e.Result = "Fail,请选择要增加的流水号！";
                //    return;
                //}
                for (int i = 0; i < s1.Length; i++)
                {
                    sn = s1[i].ToString();
                    string inSqlTemp = "UPDATE DATA_SN_BOM_TEMP_SNTEMP SET FLAG = 'DEL' WHERE SN = '" + sn + "' AND PLINE_CODE = '" + pline + "' ";
                    dc.ExeSql(inSqlTemp);
                }
                e.Result = "OK,增加成功！";
                return;
            }
        }

        //初始化流水号列表
        //protected void ASPxListBoxUnused_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        //{
        //    string sql = " SELECT DISTINCT SN FROM DATA_SN_BOM_TEMP WHERE 1=1 ";
        //    if (txtPCode.Text.Trim() != "")
        //    {
        //        sql += " AND PLINE_CODE = '" + txtPCode.Value.ToString() + "' ";
        //    }
        //    sql += " ORDER BY SN ";

        //    DataTable dt = dc.GetTable(sql);
        //    ASPxListBoxUnused.DataSource = dt;
        //    ASPxListBoxUnused.DataBind();
        //}

        //导出物料清单 EXCEL
        protected void btnXlsExport_Bom_List(object sender, EventArgs e)
        {
            ASPxGridViewExporter1.WriteXlsToResponse("导出物料清单");
        }


    }
}