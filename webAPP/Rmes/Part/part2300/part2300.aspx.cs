using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Rmes.Web.Base;
using Rmes.Pub.Data;
using DevExpress.Web.ASPxGridLookup;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxEditors;
using Rmes.DA.Base;
using Rmes.DA.Procedures;
using DevExpress.Web.ASPxGridView;

/**
 * 功能概述：线边库存查询
 * 作者：任海
 * 创建时间：2016-10-18
 */
namespace Rmes.WebApp.Rmes.Part.part2300
{
    public partial class part2300 : BasePage
    {
        private dataConn dc = new dataConn();
        public string theProgramCode;
        public string theCompanyCode;
        private string theUserId;
        private string plineCode;

        protected void Page_Load(object sender, EventArgs e)
        {
            theProgramCode = "part2300";
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();

            string sql = "SELECT DISTINCT PLINE_CODE, PLINE_NAME FROM VW_USER_ROLE_PROGRAM WHERE USER_ID = '" + theUserId
                + "' AND PROGRAM_CODE = '" + theProgramCode + "' AND COMPANY_CODE = '" + theCompanyCode + "' ORDER BY PLINE_CODE";
            ComboGzdd.DataSource = dc.GetTable(sql);
            ComboGzdd.DataBind();

            setCondition();

            if (!IsPostBack)
            {
                ComboGzdd.SelectedIndex = ComboGzdd.Items.Count >= 0 ? 0 : -1;
                
                //初始化工位
                string sql2 = " SELECT DISTINCT LOCATION_CODE FROM CODE_LOCATION WHERE  PLINE_CODE=RH_GET_DATA('L','" + ComboGzdd.Value.ToString() + "','','','')  order by LOCATION_CODE";
                DataTable dt2 = dc.GetTable(sql2);

                cmbLocation.DataSource = dt2;
                cmbLocation.ValueField = "LOCATION_CODE";
                cmbLocation.TextField = "LOCATION_CODE";
                cmbLocation.DataBind();

                //初始化保管员
                string sql1 = " select distinct upper(in_qadc01) in_qadc01 from copy_in_mstr "
                    + " where upper(in_site)= (SELECT DISTINCT SAP_CODE FROM CODE_PRODUCT_LINE WHERE PLINE_CODE = '" + ComboGzdd.Value.ToString() + "' ) "
                    + " AND in_qadc01 is not null order by upper(in_qadc01) ";
                DataTable dt1 = dc.GetTable(sql1);

                cmbBgy.DataSource = dt1;
                cmbBgy.ValueField = "IN_QADC01";
                cmbBgy.TextField = "IN_QADC01";
                cmbBgy.DataBind();
            }
            
        }

        //初始化生产线下拉列表
        private void initGzdd()
        {
            string sql = "SELECT DISTINCT PLINE_CODE, PLINE_NAME FROM VW_USER_ROLE_PROGRAM WHERE USER_ID = '" + theUserId
                + "' AND PROGRAM_CODE = '" + theProgramCode + "' AND COMPANY_CODE = '" + theCompanyCode + "' ORDER BY PLINE_CODE";
            ComboGzdd.DataSource = dc.GetTable(sql);
            ComboGzdd.DataBind();
            ComboGzdd.SelectedIndex = ComboGzdd.Items.Count >= 0 ? 0 : -1;
        }

        //初始化工位
        protected void cmbLocation_Callback(object sender, CallbackEventArgsBase e)
        {
            string pline = e.Parameter;

            string sql = " SELECT DISTINCT LOCATION_CODE FROM CODE_LOCATION WHERE  PLINE_CODE=RH_GET_DATA('L','" + pline + "','','','')  order by LOCATION_CODE";
            DataTable dt = dc.GetTable(sql);

            ASPxComboBox location = sender as ASPxComboBox;
            location.DataSource = dt;
            location.ValueField = "LOCATION_CODE";
            location.TextField = "LOCATION_CODE";
            location.DataBind();
        }
        

        //初始化保管员
        protected void cmbBgy_Callback(object sender, CallbackEventArgsBase e)
        {
            string pline = e.Parameter;

            string sql = " select distinct upper(in_qadc01) in_qadc01 from copy_in_mstr "
                    + " where upper(in_site)= (SELECT DISTINCT SAP_CODE FROM CODE_PRODUCT_LINE WHERE PLINE_CODE = '" + pline + "' ) "
                    + " AND in_qadc01 is not null order by upper(in_qadc01) ";
            DataTable dt = dc.GetTable(sql);

            ASPxComboBox bgy = sender as ASPxComboBox;
            bgy.DataSource = dt;
            bgy.ValueField = "IN_QADC01";
            bgy.TextField = "IN_QADC01";
            bgy.DataBind();
        }
        private void setCondition()
        {
            string sql = "";
            if (cbGys.Checked)
            {
                sql = " SELECT LOCATION_CODE,MATERIAL_CODE,PT_DESC2,UPPER(IN_QADC01) IN_QADC01,SUM(LINESIDE_NUM) LINESIDE_NUM,'' as GYS_CODE,'' as VD_SORT  FROM MS_LINESIDE_MT M,COPY_PT_MSTR P,COPY_IN_MSTR "
                    + " WHERE UPPER(PT_PART) = UPPER(MATERIAL_CODE) AND UPPER(IN_PART(+))=MATERIAL_CODE ";
            }
            else
            {
                sql = " SELECT LOCATION_CODE,MATERIAL_CODE,PT_DESC2,UPPER(IN_QADC01) IN_QADC01,LINESIDE_NUM,GYS_CODE,VD_SORT FROM MS_LINESIDE_MT,COPY_PT_MSTR,COPY_VD_MSTR,COPY_IN_MSTR "
                    + " WHERE UPPER(PT_PART) = UPPER(MATERIAL_CODE) AND UPPER(IN_PART(+)) = MATERIAL_CODE AND UPPER(VD_ADDR(+)) = GYS_CODE ";
            }
            //生产线必选，否则没数据
            if (ComboGzdd.Value == null)
            {
                sql += " AND GZDD = '' ";
            }
            else
            {
                sql += " AND GZDD = '" + ComboGzdd.Value.ToString() + "' ";
                sql += " AND UPPER(IN_SITE) = (SELECT DISTINCT SAP_CODE FROM CODE_PRODUCT_LINE WHERE PLINE_CODE = '" + ComboGzdd.Value.ToString() + "' ) ";
            }
            if (cmbLocation.Text.Trim().ToString() != "")
            {
                sql += " AND LOCATION_CODE = '" + cmbLocation.Text.Trim().ToString() + "' ";
            }
            if (cmbBgy.Text.Trim().ToString() != "")
            {
                sql += " AND UPPER(IN_QADC01) = '" + cmbBgy.Text.Trim().ToString() + "' "; 
            }
            if (txtMatCode.Text.Trim().ToString() != "")
            {
                sql += " AND MATERIAL_CODE LIKE '%" + txtMatCode.Text.Trim().ToString() + "%' ";
            }
            if (cbGys.Checked == true)
            {
                sql += " GROUP BY LOCATION_CODE,MATERIAL_CODE,PT_DESC2,IN_QADC01 ";
            }
            else
            {
                sql += " ORDER BY LOCATION_CODE,MATERIAL_CODE ";
            }
            DataTable dt = dc.GetTable(sql);
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
        }

        protected void btnXlsExportPlan_Click(object sender, EventArgs e)
        {
            ASPxGridViewExporter1.WriteXlsToResponse("线边库存查询导出");
        }



    }
}