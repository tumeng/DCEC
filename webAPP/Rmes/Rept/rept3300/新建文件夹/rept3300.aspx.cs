using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

using Rmes.Web.Base;
using Rmes.Pub.Data;
using Rmes.DA.Base;
using Rmes.DA.Procedures;

using DevExpress.Web.ASPxEditors;

/**
 * 功能概述：改制BOM对比
 * 作者：任海
 * 创建时间：2016-10-14
 */
namespace Rmes.WebApp.Rmes.Rept.rept3300
{
    public partial class rept3300 : BasePage
    {
        private dataConn dc = new dataConn();
        public string theProgramCode;
        public string theCompanyCode;
        private string theUserId, theUserCode;

        protected void Page_Load(object sender, EventArgs e)
        {
            theProgramCode = "rept3300";
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();
            theUserCode = theUserManager.getUserCode();
            //test
            //theUserCode = "ZZ098";

            if (!IsPostBack)
            {
            }

            initPlineCode();
            setCondition();

            if (Request["opFlag"] == "getEditSeries")
            {
                string result = "";
                string sn = Request["SN"].ToString().Trim();
                string plineCode = Request["PLINE_CODE"].ToString().Trim();

                string sql = "select SN,PLAN_CODE,PLAN_SO,PLINE_CODE from VW_DATA_PRODUCT where SN = '" + sn + "' "
                    //+ " and ZDMC='管理' "
                    + " and PLAN_CODE in (select PLAN_CODE from DATA_PLAN where PLINE_CODE='" + plineCode + "')";
                DataTable dt = dc.GetTable(sql);
                if (dt.Rows.Count > 0)
                {
                    //??或者是取不到list里的数据，或者是已被清空，待测试 前端是用ClientInstanceName取，后台用ID?
                    for (int i = 0; i < listLsh.Items.Count; i++)
                    {
                        //根据“&”分割出SN
                        string[] str = listLsh.Items[i].ToString().Trim().Split("&".ToCharArray());
                        if (sn == str[0])
                        {
                            //为1时前端显示“流水号已录入！”
                            result = "1";
                        }
                    }
                    sn = dt.Rows[0][0].ToString();
                    string planCode = dt.Rows[0][1].ToString();
                    string planSO = dt.Rows[0][2].ToString();
                    string plineCode1 = dt.Rows[0][3].ToString();
                    result = sn + "&" + planCode + "&" + planSO + "&" + plineCode1;
                }
                else
                {
                    //为0时前端显示“流水号不存在！”
                    result = "0";
                }

                this.Response.Write(result);
                this.Response.End();
            }
        }

        //初始化gridview
        private void setCondition()
        {
            string sql = "";
            sql = " SELECT * FROM RST_GHTM_BOM_COMP WHERE YHDM = '" + theUserCode + "' ORDER BY COMP_FLAG DESC ";

            DataTable dt = dc.GetTable(sql);
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
        }

        //是点击按钮的时候才执行吗？
        //callback方式刷新gridview
        protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            setCondition();
        }

        //初始化生产线
        private void initPlineCode()
        {
            string sql = "select distinct a.pline_code,b.pline_name from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'";
            SqlCode.SelectCommand = sql;
            SqlCode.DataBind();
            txtPCode.SelectedIndex = txtPCode.Items.Count >= 0 ? 0 : -1;
        }

        protected void btnQryDetail_Click(object sender, EventArgs e)
        {
            string oldJhdm;
            string oldSO;
            string newJhdm;
            string newSO;
            string newGhtm;
            string newGzdd;

            if (listLsh.Items.Count >= 1)
            {
                string sql = " DELETE FROM RST_GHTM_BOM_COMP WHERE YHDM = '" + theUserCode + "' ";
                dc.ExeSql(sql);

                for (int i = 0; i < listLsh.Items.Count; i++)
                {
                    //sql = " SELECT GHTM,JHDM,SO FROM RST_GHTM_GZ_LOG WHERE JHDM != NEW_JHDM AND GZRQ IS NOT NULL ";
                    //string[] str = listLsh.Items[i].ToString().Trim().Split("&".ToCharArray());
                    //string ghtm = str[0];
                    //sql += " AND GHTM = '" + ghtm + "' ";
                    //sql += " ORDER BY GZRQ DESC ";
                    //不从原来的RST_GHTM_GZ_LOG里取旧JHDM，从DATA_RECORD里取旧JHDM
                    string[] str = listLsh.Items[i].ToString().Trim().Split("&".ToCharArray());
                    string ghtm = str[0];
                    sql = " SELECT SN,PLAN_CODE,PLAN_SO FROM DATA_RECORD WHERE SN = '" + ghtm + "' ORDER BY WORK_DATE DESC ";

                    DataTable dt = dc.GetTable(sql);
                    if (dt.Rows.Count > 0)
                    {
                        oldJhdm = dt.Rows[0][1].ToString();
                        oldSO = dt.Rows[0][2].ToString();
                    }
                    else
                    {
                        oldJhdm = "";
                        oldSO = "";
                    }
                    newGhtm = ghtm;
                    newJhdm = str[1];
                    newSO = str[2];
                    newGzdd = str[3];

                    MW_COMPARE_GHTMBOM sp = new MW_COMPARE_GHTMBOM()
                    {
                        GHTM1 = newGhtm,
                        GZDD1 = newGzdd, 
                        JHDM1 = oldJhdm, 
                        JHSO1 = oldSO, 
                        GZFLAG1 = "OLD", 
                        GZDD2 = newGzdd, 
                        JHDM2 = newJhdm, 
                        JHSO2 = newSO, 
                        GZFLAG2 = "NEW",
                        YHDM1 = theUserCode 
                    };
                    Procedure.run(sp);
                }
                setCondition();
            }

        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            ASPxGridViewExporter1.WriteXlsToResponse("改制差异清单明细信息导出");
        }

        protected void ASPxGridView1_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            if (e.VisibleIndex < 0) return;
            string compFlag = e.GetValue("COMP_FLAG").ToString();
            switch (compFlag)
            {
                case "0":
                    e.Row.BackColor = System.Drawing.Color.White;
                    break;
                case "1":
                    e.Row.BackColor = System.Drawing.Color.Red;
                    break;
                case "2":
                    e.Row.BackColor = System.Drawing.Color.Green;
                    break;
                default:
                    return;
            }
        }



    }
}