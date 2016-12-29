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
using System.Web.UI.WebControls.WebParts;
using Rmes.DA.Base;
using Rmes.DA.Procedures;
using DevExpress.Web.ASPxGridView;
using Rmes.DA.Factory;
using Rmes.DA.Entity;
using System.Web.UI.HtmlControls;
using System.Web.Security;

/**
 * 功能概述：线边库存维护
 * 作者：杨少霞
 * 创建时间：2016-11-25
 */
namespace Rmes.WebApp.Rmes.Part.part2900
{
    public partial class part2900 : BasePage
    {
        private dataConn dc = new dataConn();
        public string theProgramCode;
        public string theCompanyCode;
        private string theUserId,theUserCode;
        private string plineCode;
        private string modifyFlag;

        protected void Page_Load(object sender, EventArgs e)
        {
            theProgramCode = "part2900";
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();
            theUserCode = theUserManager.getUserCode();
            modifyFlag = "0";

            string sql = "SELECT DISTINCT PLINE_CODE, PLINE_NAME FROM VW_USER_ROLE_PROGRAM WHERE USER_ID = '" + theUserId
                + "' AND PROGRAM_CODE = '" + theProgramCode + "' AND COMPANY_CODE = '" + theCompanyCode + "' ORDER BY PLINE_CODE";
            ComboGzdd.DataSource = dc.GetTable(sql);
            ComboGzdd.DataBind();

            setCondition();
            if (Request["opFlag"] == "getEditM")
            {
                string str1 = "";
                string MCode = Request["MCODE"].ToString().Trim();

                string sql88 = "select nvl(pt_desc2,' ') from copy_pt_mstr where UPPER(pt_part)='" + MCode + "'";
                dc.setTheSql(sql88);
                DataTable dt = dc.GetTable(sql);
                if (dt.Rows.Count == 0)
                {
                    str1 = "";
                    this.Response.Write(str1);
                    this.Response.End();
                    return;
                }

                string strMCode = dc.GetTable().Rows[0][0].ToString();
                if (strMCode == "")
                {
                    str1 = "";
                    this.Response.Write(str1);
                    this.Response.End();
                    return;
                }
                else { str1 = strMCode; }

                this.Response.Write(str1);
                this.Response.End();
            }
            if (Request["opFlag"] == "getEditGYS")
            {
                string str1 = "";
                string GCode = Request["GCODE"].ToString().Trim();

                string sql99 = "select nvl(ad_name,' ') from copy_ad_mstr where UPPER(ad_addr)='" + GCode + "'";
                dc.setTheSql(sql99);
                DataTable dt = dc.GetTable(sql);
                if (dt.Rows.Count == 0)
                {
                    str1 = "";
                    this.Response.Write(str1);
                    this.Response.End();
                    return;
                }

                string strGCode = dc.GetTable().Rows[0][0].ToString();
                if (strGCode == "")
                {
                    str1 = "";
                    this.Response.Write(str1);
                    this.Response.End();
                    return;
                }
                else { str1 = strGCode; }

                this.Response.Write(str1);
                this.Response.End();
            }

            if (!IsPostBack)
            {
                ComboGzdd.SelectedIndex = ComboGzdd.Items.Count >= 0 ? 0 : -1;

                //初始化工位
                string sql2 = " select location_code from ms_location_time where gzdd='" + ComboGzdd.Value.ToString() + "' order by location_seq ";
                DataTable dt2 = dc.GetTable(sql2);

                cmbLocation.DataSource = dt2;
                cmbLocation.ValueField = "LOCATION_CODE";
                cmbLocation.TextField = "LOCATION_CODE";
                cmbLocation.DataBind();

               
            }

        }

        ////初始化生产线下拉列表
        //private void initGzdd()
        //{
        //    string sql = "SELECT DISTINCT PLINE_CODE, PLINE_NAME FROM VW_USER_ROLE_PROGRAM WHERE USER_ID = '" + theUserId
        //        + "' AND PROGRAM_CODE = '" + theProgramCode + "' AND COMPANY_CODE = '" + theCompanyCode + "' ORDER BY PLINE_CODE";
        //    ComboGzdd.DataSource = dc.GetTable(sql);
        //    ComboGzdd.DataBind();
        //    ComboGzdd.SelectedIndex = ComboGzdd.Items.Count >= 0 ? 0 : -1;
        //}

        //初始化工位
        protected void cmbLocation_Callback(object sender, CallbackEventArgsBase e)
        {
            string pline = e.Parameter;

            string sql = " select location_code from ms_location_time where gzdd='" + pline + "' order by location_seq ";
            DataTable dt = dc.GetTable(sql);

            ASPxComboBox location = sender as ASPxComboBox;
            location.DataSource = dt;
            location.ValueField = "LOCATION_CODE";
            location.TextField = "LOCATION_CODE";
            location.DataBind();
        }

        private void setCondition()
        {
            string sql = "select a.gzdd,a.location_code,a.material_code,a.gys_code,a.lineside_num,c.location_name,d.pt_desc2,e.ad_name from ms_lineside_mt a"
                       +" left join code_product_line b on a.gzdd=b.pline_code "
                       +" left join code_location c on a.location_code=c.location_code "
                       + " left join copy_pt_mstr d on a.material_code=d.pt_part "
                       + " left join copy_ad_mstr e on a.gys_code=e.ad_addr "
                       +" where 1=1 ";
            //生产线必选，否则没数据
            if (ComboGzdd.Value == null)
            {
                sql += " AND a.GZDD = '' ";
            }
            else
            {
                sql += " AND a.GZDD = '" + ComboGzdd.Value.ToString() + "' ";
                
            }
            if (cmbLocation.Text.Trim().ToString() != "")
            {
                sql += " AND a.LOCATION_CODE = '" + cmbLocation.Text.Trim().ToString() + "' ";
            }
            if (txtMatCode.Text.Trim().ToString() != "")
            {
                sql += " AND a.MATERIAL_CODE LIKE '%" + txtMatCode.Text.Trim().ToString() + "%' ";
            }
            if (txtGysCode.Text.Trim().ToString() != "")
            {
                sql += " and a.gys_code like '%" + txtGysCode.Text.Trim().ToString() + "%' ";
            }
            else
            {
                sql += " ORDER BY 1,2,3 ";
            }
            DataTable dt = dc.GetTable(sql);
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
        }

        protected void btnXlsExportPlan_Click(object sender, EventArgs e)
        {
            ASPxGridViewExporter1.WriteXlsToResponse("线边库存导出");
        }

        protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string plineC = e.Values["GZDD"].ToString();
            string mCode = e.Values["MATERIAL_CODE"].ToString();
            string gysCode = e.Values["GYS_CODE"].ToString();
            string lCode = e.Values["LOCATION_CODE"].ToString();
            string linesideNum = e.Values["LINESIDE_NUM"].ToString();

            LineSideMatFactory.MS_MODIFY_LINESIDE_MT("DELETE", mCode, gysCode, lCode, Convert.ToInt32(linesideNum), plineC, theUserCode);
            e.Cancel = true;
            ASPxGridView1.CancelEdit();
            setCondition();
            ASPxGridView1.DataBind();
        }

        protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxComboBox plineC = ASPxGridView1.FindEditFormTemplateControl("comboPCode") as ASPxComboBox;
            ASPxComboBox mCode = ASPxGridView1.FindEditFormTemplateControl("txtMCode") as ASPxComboBox;
            ASPxComboBox gysCode = ASPxGridView1.FindEditFormTemplateControl("txtGYSCodeA") as ASPxComboBox;
            ASPxComboBox lCode = ASPxGridView1.FindEditFormTemplateControl("comboLocation") as ASPxComboBox;
            ASPxTextBox linesideNum = ASPxGridView1.FindEditFormTemplateControl("txtMNum") as ASPxTextBox;

            LineSideMatFactory.MS_MODIFY_LINESIDE_MT("UPDATE", mCode.Value.ToString(), gysCode.Value.ToString(), lCode.Value.ToString(),Convert.ToInt32(linesideNum.Text.Trim()), plineC.Value.ToString(), theUserCode);
            e.Cancel = true;
            ASPxGridView1.CancelEdit();
            setCondition();
            ASPxGridView1.DataBind();

        }

        protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxComboBox plineC = ASPxGridView1.FindEditFormTemplateControl("comboPCode") as ASPxComboBox;
            ASPxComboBox mCode = ASPxGridView1.FindEditFormTemplateControl("txtMCode") as ASPxComboBox;
            ASPxComboBox gysCode = ASPxGridView1.FindEditFormTemplateControl("txtGYSCodeA") as ASPxComboBox;
            ASPxComboBox lCode = ASPxGridView1.FindEditFormTemplateControl("comboLocation") as ASPxComboBox;
            ASPxTextBox linesideNum = ASPxGridView1.FindEditFormTemplateControl("txtMNum") as ASPxTextBox;

            LineSideMatFactory.MS_MODIFY_LINESIDE_MT("UPDATE", mCode.Value.ToString(), gysCode.Value.ToString(), lCode.Value.ToString(), Convert.ToInt32(linesideNum.Text.Trim()), plineC.Value.ToString(), theUserCode);
            e.Cancel = true;
            ASPxGridView1.CancelEdit();
            setCondition();
            ASPxGridView1.DataBind();
        }

        protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            //判断为空
            if (e.NewValues["MATERIAL_CODE"].ToString() == "" || e.NewValues["MATERIAL_CODE"].ToString() == null)
            {
                e.RowError = "物料代码不能为空！";
            }
            if (e.NewValues["GYS_CODE"].ToString() == "" || e.NewValues["GYS_CODE"].ToString() == null)
            {
                e.RowError = "供应商代码不能为空！";
            }
            if (e.NewValues["LINESIDE_NUM"].ToString() == "" || e.NewValues["LINESIDE_NUM"].ToString() == null)
            {
                e.RowError = "数量不能为空！";
            }
            if (e.NewValues["LOCATION_CODE"].ToString() == "" || e.NewValues["LOCATION_CODE"].ToString() == null)
            {
                e.RowError = "工位不能为空！";
            }

            //判断数量是否为整数
            if (!IsInt(e.NewValues["LINESIDE_NUM"].ToString()))
            {
                e.RowError = "数量必须为正整数!";
            }

            //判断是否重复
            if (ASPxGridView1.IsNewRowEditing)
            {

                string chSql = "select * from ms_lineside_mt "
                             +"where material_code='" + e.NewValues["MATERIAL_CODE"].ToString().ToUpper() + "' and location_code='"+e.NewValues["LOCATION_CODE"]+"' "
                             + "and gys_code='" + e.NewValues["GYS_CODE"].ToString().ToUpper() + "' and gzdd='" + e.NewValues["GZDD"].ToString() + "'";
                DataTable dt = dc.GetTable(chSql);
                if (dt.Rows.Count > 0)
                {
                    e.RowError = "该线边数据已存在，请选择修改操作!";
                }

            }
        }

        protected void ASPxGridView1_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
        {
            string Sql = " select distinct a.pline_code,b.pline_name as showtext from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "' ";
            DataTable dt = dc.GetTable(Sql);
            ASPxComboBox uPCode = ASPxGridView1.FindEditFormTemplateControl("comboPCode") as ASPxComboBox;
            uPCode.DataSource = dt;
            uPCode.TextField = dt.Columns[1].ToString();
            uPCode.ValueField = dt.Columns[0].ToString();
            //uPCode.SelectedIndex = uPCode.Items.Count >= 0 ? 0 : -1;

            string Sql2 = " select ad_addr,ad_name from copy_ad_mstr order by ad_addr";
            DataTable dt2 = dc.GetTable(Sql2);
            ASPxComboBox gysCode = ASPxGridView1.FindEditFormTemplateControl("txtGYSCodeA") as ASPxComboBox;
            gysCode.DataSource = dt2;
            gysCode.TextField = dt2.Columns[0].ToString();
            gysCode.ValueField = dt2.Columns[0].ToString();

            if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
            {
                (ASPxGridView1.FindEditFormTemplateControl("comboPCode") as ASPxComboBox).Enabled = false;
                (ASPxGridView1.FindEditFormTemplateControl("txtMCode") as ASPxComboBox).Enabled = false;
                (ASPxGridView1.FindEditFormTemplateControl("txtGYSCodeA") as ASPxComboBox).Enabled = false;
                (ASPxGridView1.FindEditFormTemplateControl("comboLocation") as ASPxComboBox).Enabled = false;
            }
        }

        public static bool IsInt(string str)
        {
            if (str == string.Empty)
                return false;
            try
            {
                Convert.ToInt32(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            setCondition();
            ASPxGridView1.DataBind();
        }

        protected void cmdSet_Click(object sender, EventArgs e)
        {
            string sql = "update ms_lineside_mt set lineside_num=0 where gzdd='" + ComboGzdd.Value.ToString() + "'";
            dc.ExeSql(sql);

            setCondition();
            ASPxGridView1.DataBind();
        }
        protected void txtMCode_Callback(object sender, CallbackEventArgsBase e)
        {
            string pline = e.Parameter;

            string sql = "select INTERNAL_NAME from CODE_INTERNAL where INTERNAL_CODE='" + pline + "' AND INTERNAL_TYPE_CODE='012'";
            DataTable dt = dc.GetTable(sql);
            string qad_site = "";
            if (dt.Rows.Count > 0)
            {
                qad_site = dt.Rows[0][0].ToString();
            }
            string sql2 = "SELECT PTP_PART FROM COPY_PTP_DET WHERE UPPER(PTP_RUN_SEQ1)='JIS' AND UPPER(PTP_SITE)='" + qad_site + "'";
            DataTable dt2 = dc.GetTable(sql2);
            ASPxComboBox mCode = sender as ASPxComboBox;
            mCode.DataSource = dt2;
            mCode.ValueField = "PTP_PART";
            mCode.TextField = "PTP_PART";
            mCode.DataBind();
        }
        protected void comboLocation_Callback(object sender, CallbackEventArgsBase e)
        {
            string pline = e.Parameter;

            string Sql3 = " select location_code from ms_location_time where gzdd='" + pline + "'  order by location_seq";//
            DataTable dt3 = dc.GetTable(Sql3);
            ASPxComboBox lCode = ASPxGridView1.FindEditFormTemplateControl("comboLocation") as ASPxComboBox;
            lCode.DataSource = dt3;
            lCode.TextField = dt3.Columns[0].ToString();
            lCode.ValueField = dt3.Columns[0].ToString();
            lCode.DataBind();
            
        }
    }
}