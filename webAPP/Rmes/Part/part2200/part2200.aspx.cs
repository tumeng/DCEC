using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

using Rmes.Web.Base;
using Rmes.Pub.Data;

/**
 * 功能概述：MES需求发出查询
 * 作者：任海
 * 创建时间：2016-08-26
 */
namespace Rmes.WebApp.Rmes.Part.part2200
{
    public partial class part2200 : BasePage
    {
        private dataConn dc = new dataConn();
        public string theProgramCode;
        public string theCompanyCode;
        private string theUserId;
        //生产线全局变量
        private string plineCode;

        protected void Page_Load(object sender, EventArgs e)
        {
            theProgramCode = "part2200";
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();

            if (!IsPostBack)
            {
                //第一次访问将生产线设为E
                //plineCode = "E";
                //test
                ComboGzdd.Value = "E";
                DateStart.Date = DateTime.Now;
                DateEnd.Date = DateTime.Now;
                initGzdd();
            }
            //将生产线设为combobox的值
            if (ComboGzdd.Value != null)
            {
                plineCode = ComboGzdd.Value.ToString();
            }

            setCondition();
        }

        //初始化gridview
        private void setCondition()
        {
            string sql = "";
            sql = "SELECT T.ROWID,T.PO_VEND,T.PART_NBR,A.PT_DESC2，T.RCT_AMOUNT, T.IMP_DATETIME "
                + " FROM COPY_RCT_PO_R_LOG T " 
                + " LEFT JOIN COPY_PT_MSTR A ON T.PART_NBR = A.PT_PART "
                //CODE_PRODUCT_LINE里的PLINE_CODE只可能对应一个SAP_CODE，所以直接等于就可以
                //combobox的值第一次打开页面获取不到，所有通过plineCode全局变量获取
                + " WHERE T.PO_NBER = (SELECT DISTINCT SAP_CODE FROM CODE_PRODUCT_LINE WHERE PLINE_CODE = '" + ComboGzdd.Value.ToString() + "' )  ";
            if (TextPartNbr.Text.Trim() != "")
            {
                sql += " AND T.PART_NBR like '%" + TextPartNbr.Text.Trim() + "%' ";
            }
            if (TextPoVend.Text.Trim() != "")
            {
                sql += " AND T.PO_VEND like '%" + TextPoVend.Text.Trim() + "%' ";
            }
            //因为日期是直接赋给的值，并不需要先去前台获取
            if (DateStart.Text.Trim() != "")
            {
                sql += " AND TO_DATE(T.IMP_DATETIME, 'YYYY-MM-DD HH24:MI:SS') >= TO_DATE('" + DateStart.Text + "','YYYY-MM-DD HH24:MI:SS') ";
            }
            if (DateEnd.Text.Trim() != "")
            {
                sql += " AND TO_DATE(T.IMP_DATETIME, 'YYYY-MM-DD HH24:MI:SS') <= TO_DATE('" + DateEnd.Text + "','YYYY-MM-DD HH24:MI:SS') ";
            }
            sql += " ORDER BY T.IMP_DATETIME DESC ";

            DataTable dt = dc.GetTable(sql);
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
        }

        //初始化生产线下拉列表
        private void initGzdd()
        {
            String sql = "SELECT DISTINCT PLINE_CODE, PLINE_NAME FROM VW_USER_ROLE_PROGRAM WHERE USER_ID = '" + theUserId
                + "' AND PROGRAM_CODE = '" + theProgramCode + "' AND COMPANY_CODE = '" + theCompanyCode + "' ORDER BY PLINE_CODE";
            sqlGzdd.SelectCommand = sql;
            sqlGzdd.DataBind();
            ComboGzdd.SelectedIndex = ComboGzdd.Items.Count >= 0 ? 0 : -1;
        }

        protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            setCondition();
        }
    }
}