using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.Pub.Data;
using Rmes.Web.Base;
using System.Data;
using System.Data.OleDb;
using System.IO;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxClasses;
/**
 * 功能概述：按物料单独要料-kufang要料
 * 
 */
namespace Rmes.WebApp.Rmes.Part.part2B00
{
    public partial class part2B00 : BasePage
    {
        private dataConn dc = new dataConn();
        PubCs thePubCs = new PubCs();
        public string theCompanyCode;
        private string theUserId, theUserCode;
        public string theProgramCode;
        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();
            theUserCode = theUserManager.getUserCode();
            theProgramCode = "part2B00";
            //initTName();
            initCode();
            setCondition();
            if (!IsPostBack)
            {

                ASPxDateEdit1.Date = DateTime.Now;
                ASPxDateEdit3.Date = DateTime.Now.AddDays(1);
            }
            if (Request["opFlag"] == "getEditSeries")
            {
                string str1 = "", str2 = "";
                string pcode = Request["Pcode"].ToString().Trim();
                if (pcode == "E")
                {
                    str2 = "DCEC-B";

                }
                else if (pcode == "W")
                {
                    str2 = "DCEC-C";
                }
                else { str2 = ""; }

                str1 = str1 + "," + str2;
                this.Response.Write(str1);
                this.Response.End();
            }
            if (Request["opFlag"] == "getEditSeries2")
            {
                string str1 = "", str2 = "";
                string pcode = Request["Pcode"].ToString().Trim();
                if (pcode == "E")
                {
                    str2 = "DCEC-B";

                }
                else if (pcode == "W")
                {
                    str2 = "DCEC-C";
                }
                else { str2 = ""; }

                str1 = str1 + "," + str2;

                dataConn theDataConn = new dataConn(" select FUNC_GET_PLANSITE（'" + pcode + "','D'）from dual");
                theDataConn.OpenConn();
                string gQadSite = theDataConn.GetValue();
                if (gQadSite != "")
                {
                    string sql = "select location_code from ms_location_time where gzdd='" + pcode + "' order by location_seq";
                    Session["9100USER"] = sql;
                    sqlLocation.SelectCommand = sql;
                    sqlLocation.DataBind();
                }
                


                this.Response.Write(str1);
                this.Response.End();
            }
            if (Request["opFlag"] == "getMATERIAL")
            {
                string str1 = "";
                string mcode = Request["MCODE"].ToString().Trim();
                string sql = "select nvl(pt_desc2,' ') from copy_pt_mstr where pt_part='" + mcode + "'";
                dc.setTheSql(sql);
                if (dc.GetTable().Rows.Count <= 0)
                {
                    str1 = "no";
                    this.Response.Write(str1);
                    this.Response.End();
                    return;
                }
                str1 = dc.GetTable().Rows[0][0].ToString();
                this.Response.Write(str1);
                this.Response.End();
            }
            if (Request["opFlag"] == "getGYS")
            {
                string str1 = "";
                string gcode = Request["GCODE"].ToString().Trim();
                string sql = "select nvl(ad_name,' ') from copy_ad_mstr where upper(ad_addr)='" + gcode + "'";
                dc.setTheSql(sql);
                if (dc.GetTable().Rows.Count <= 0)
                {
                    str1 = "no";
                    this.Response.Write(str1);
                    this.Response.End();
                    return;
                }
                str1 = dc.GetTable().Rows[0][0].ToString();
                this.Response.Write(str1);
                this.Response.End();
            }
        }
        //private void initTName()
        //{
        //    // 要料类型
        //    string sql = "select INTERNAL_NAME TYPE_NAME from CODE_INTERNAL WHERE INTERNAL_TYPE_CODE ='009' order by INTERNAL_CODE";
        //    sqlTName.SelectCommand = sql;
        //    sqlTName.DataBind();
        //}
        private void initCode()
        {
            //初始化用户下拉列表
            string sql = "select distinct a.pline_code,b.pline_name from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'";
            SqlCode.SelectCommand = sql;
            SqlCode.DataBind();
        }
        private void setCondition()
        {
            string sql = "select a.*,nvl(b.pt_desc2,' ') MATERIAL_NAME,nvl(c.ad_name,' ') GYS_NAME  from ms_single_mat_inner a left join copy_pt_mstr b on a.material_code=b.pt_part left join COPY_AD_MSTR c on Upper(a.gys_code)=Upper(c.ad_addr)  "
                       + " where online_time>=to_date('" + ASPxDateEdit1.Text.Trim() + "','YYYY-MM-DD')  AND online_time<=to_date('" + ASPxDateEdit3.Text.Trim() + "','YYYY-MM-DD') AND material_code like '%" + TextMCode.Text.Trim() + "%' ";
            if (txtPlineCode.Text.Trim() != "")
            {
                sql = sql + "and gzdd = '" + txtPlineCode.Value.ToString() + "'";
            }
            if (TextGCode.Text.Trim() != "")
            {
                sql = sql + " AND gys_code like '%" + TextGCode.Text.Trim() + "%'";
            }
            if (TextQadSite.Text.Trim() != "")
            {
                sql = sql + " AND qadsite like '%" + TextQadSite.Text.Trim() + "%'";
            }

            sql = sql + " order by add_time";

            DataTable dt = dc.GetTable(sql);
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();

        }
        protected void ASPxGridView1_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            //e.NewValues["HC_FLAG"];
            //(ASPxGridView1.FindEditFormTemplateControl("txtHcFLAG") as ASPxComboBox).SelectedIndex = '1';
            //(ASPxGridView1.FindEditFormTemplateControl("txtLqFlag") as ASPxComboBox).SelectedIndex = '1';
            //(ASPxGridView1.FindEditFormTemplateControl("txtPackFlag") as ASPxComboBox).SelectedIndex = '0';

        }
        protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            try
            {

                if (Session["9100USER"].ToString() != "")
                {
                    string sql = Session["9100USER"] as string;
                    sqlLocation.SelectCommand = sql;
                    sqlLocation.DataBind();
                }
            }
            catch
            { }
            
            setCondition();
            ASPxGridView1.Selection.UnselectAll();
        }

        protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxComboBox uPCode = ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;
            ASPxTextBox uMCode = ASPxGridView1.FindEditFormTemplateControl("txtMCode") as ASPxTextBox;
            ASPxTextBox uMName = ASPxGridView1.FindEditFormTemplateControl("txtMName") as ASPxTextBox;
            ASPxTextBox uGCode = ASPxGridView1.FindEditFormTemplateControl("txtGCode") as ASPxTextBox;
            ASPxTextBox uGName = ASPxGridView1.FindEditFormTemplateControl("txtGName") as ASPxTextBox;
            ASPxComboBox uLocation = ASPxGridView1.FindEditFormTemplateControl("txtLocation") as ASPxComboBox;
            ASPxComboBox uQSite = ASPxGridView1.FindEditFormTemplateControl("txtQSite") as ASPxComboBox;
            ASPxTextBox uMNum = ASPxGridView1.FindEditFormTemplateControl("txtMNum") as ASPxTextBox;
            ASPxDateEdit uOnTime = ASPxGridView1.FindEditFormTemplateControl("txtOnTime") as ASPxDateEdit;
            
            string strPCode = uPCode.Value.ToString();
            string strQSite = uQSite.Value.ToString();
            string strMCode = uMCode.Text.Trim();
            string strMName = uMName.Text.Trim();
            string strGName = uGName.Text.Trim();
            string strGCode = uGCode.Text.Trim();
            string strMNum = uMNum.Text.Trim();
            string locationCode = uLocation.Text.Trim();
            
            string Sql = "insert into ms_single_mat_inner(material_code,material_num,online_time,online_location,add_time,flag,gzdd,gys_code,qadsite)"
                       + "VALUES('" + strMCode + "','" + strMNum + "',to_date('" + uOnTime.Value + "','yyyy-mm-dd hh24:mi:ss'),'"+locationCode+"',"
                       + "sysdate ,'N','" + strPCode + "','" + strGCode + "','" + strQSite + "')";
            dc.ExeSql(Sql);

            e.Cancel = true;
            ASPxGridView1.CancelEdit();
            setCondition();
        }

        //删除
        protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            //判断当前记录是否可以删除
            string strMCode = e.Values["MATERIAL_CODE"].ToString();
            string strPCode = e.Values["GZDD"].ToString();
            string strOLocation = e.Values["ONLINE_LOCATION"].ToString();
            string strQadsite = e.Values["QADSITE"].ToString();
            string strTableName = "ms_single_mat_inner";
            string flag = e.Values["FLAG"].ToString();
            dataConn theDataConn = new dataConn("select func_check_delete_data('" + strTableName + "','" + theCompanyCode + "','MES','MES','MES','" + strMCode + "') from dual");
            theDataConn.OpenConn();
            string theRet = theDataConn.GetValue();
            //弹框提示已经计算的不能删除
            if (flag == "Y")
            {
                ASPxGridView1.JSProperties.Add("cpCallbackName", "Fail");
                ASPxGridView1.JSProperties.Add("cpCallbackRet", "已经计算的不能删除！");

            }
            else if (theRet != "Y")
            {
                ASPxGridView1.JSProperties.Add("cpCallbackName", "Delete");
                ASPxGridView1.JSProperties.Add("cpCallbackRet", theRet);
                theDataConn.CloseConn();
            }
            else
            {
                //确认删除
                string Sql = "delete from ms_single_mat_inner where material_code='" + strMCode + "' and ONLINE_LOCATION='" + strOLocation + "' and flag='N' and gzdd='" + strPCode + "' and qadsite='" + strQadsite + "'";
                dc.ExeSql(Sql);
            }
            setCondition();
            e.Cancel = true;
        }

        //修改
        protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxComboBox uPCode = ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;
            ASPxTextBox uMCode = ASPxGridView1.FindEditFormTemplateControl("txtMCode") as ASPxTextBox;
            ASPxTextBox uMName = ASPxGridView1.FindEditFormTemplateControl("txtMName") as ASPxTextBox;
            ASPxTextBox uGCode = ASPxGridView1.FindEditFormTemplateControl("txtGCode") as ASPxTextBox;
            ASPxTextBox uGName = ASPxGridView1.FindEditFormTemplateControl("txtGName") as ASPxTextBox;
            ASPxComboBox uQSite = ASPxGridView1.FindEditFormTemplateControl("txtQSite") as ASPxComboBox;
            ASPxTextBox uMNum = ASPxGridView1.FindEditFormTemplateControl("txtMNum") as ASPxTextBox;
            ASPxDateEdit uOnTime = ASPxGridView1.FindEditFormTemplateControl("txtOnTime") as ASPxDateEdit;
            ASPxComboBox uLocation = ASPxGridView1.FindEditFormTemplateControl("txtLocation") as ASPxComboBox;

            string strPCode = uPCode.Value.ToString();
            string strQSite = uQSite.Value.ToString();
            string strMCode = uMCode.Text.Trim();
            string strMName = uMName.Text.Trim();
            string strGName = uGName.Text.Trim();
            string strGCode = uGCode.Text.Trim();
            string strMNum = uMNum.Text.Trim();
            string locationCode = uLocation.Text.Trim();

            string Sql = "update ms_single_mat_inner set material_num='" + strMNum + "',online_time=to_date('" + uOnTime.Value + "','yyyy-mm-dd hh24:mi:ss') "
            + " where material_code='" + strMCode + "' and gzdd='" + strPCode + "' and gys_code='" + strGCode + "' and online_location='" + locationCode + "' and qadsite='" + strQSite + "' and flag='N'";
            
            dc.ExeSql(Sql);

            e.Cancel = true;
            ASPxGridView1.CancelEdit();
            setCondition();
        }

        //创建EDITFORM前
        protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
        {
            if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
            {
                (ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox).Enabled = false;
                (ASPxGridView1.FindEditFormTemplateControl("txtMCode") as ASPxTextBox).Enabled = false;
                (ASPxGridView1.FindEditFormTemplateControl("txtGCode") as ASPxTextBox).Enabled = false;
                (ASPxGridView1.FindEditFormTemplateControl("txtLocation") as ASPxComboBox).Enabled = false;

                (ASPxGridView1.FindEditFormTemplateControl("txtGName") as ASPxTextBox).Enabled = false;
                (ASPxGridView1.FindEditFormTemplateControl("txtMName") as ASPxTextBox).Enabled = false;
                
            }
            if (ASPxGridView1.IsNewRowEditing)
            {
                (ASPxGridView1.FindEditFormTemplateControl("txtGName") as ASPxTextBox).Enabled = false;
                (ASPxGridView1.FindEditFormTemplateControl("txtMName") as ASPxTextBox).Enabled = false;
            }
            string Sql = " select distinct a.pline_code,b.pline_name as showtext from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "' ";
            DataTable dt = dc.GetTable(Sql);
            ASPxComboBox uPCode = ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;
            uPCode.DataSource = dt;
            uPCode.TextField = dt.Columns[1].ToString();
            uPCode.ValueField = dt.Columns[0].ToString();

            if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
            {
                (ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox).Enabled = false;
                (ASPxGridView1.FindEditFormTemplateControl("txtMCode") as ASPxTextBox).Enabled = false;
            }
        }

        //修改数据校验
        protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            //判断为空
            if (e.NewValues["GZDD"].ToString() == "" || e.NewValues["GZDD"].ToString() == null)
            {
                e.RowError = "生产线不能为空！";
            }
            if (e.NewValues["MATERIAL_CODE"].ToString() == "" || e.NewValues["MATERIAL_CODE"].ToString() == null)
            {
                e.RowError = "物料代码不能为空！";
            }
            if (e.NewValues["GYS_CODE"].ToString() == "" || e.NewValues["GYS_CODE"].ToString() == null)
            {
                e.RowError = "供应商代码不能为空！";
            }
            if (e.NewValues["MATERIAL_NUM"].ToString() == "" || e.NewValues["MATERIAL_NUM"].ToString() == null)
            {
                e.RowError = "数量不能为空！";
            }
            if (e.NewValues["ONLINE_LOCATION"].ToString() == "" || e.NewValues["ONLINE_LOCATION"].ToString() == null)
            {
                e.RowError = "工位不能为空！";
            }
            if (e.NewValues["ONLINE_TIME"].ToString() == "" || e.NewValues["ONLINE_TIME"].ToString() == null)
            {
                e.RowError = "上线时间不能为空！";
            }

            //判断数量是否为整数
            if (!IsInt(e.NewValues["MATERIAL_NUM"].ToString()))
            {
                e.RowError = "数量必须为正整数!";
            }

            //判断是否重复
            if (ASPxGridView1.IsNewRowEditing)
            {
                string chSql = "select * from ms_single_mat_inner where material_code='" + e.NewValues["MATERIAL_CODE"].ToString() + "' "
                             + "and gys_code='" + e.NewValues["GYS_CODE"].ToString() + "' and online_location='" + e.NewValues["ONLINE_LOCATION"].ToString() + "' "
                             + "and flag='N' and gzdd='" + e.NewValues["GZDD"].ToString() + "' ";
                DataTable dt = dc.GetTable(chSql);
                if (dt.Rows.Count > 0)
                {
                    e.RowError = "该物料已添加且尚未要料!";
                }

            }
            
        }
        protected void btnXlsExport_Click(object sender, EventArgs e)
        {
            ASPxGridViewExporter1.WriteXlsToResponse("按物料单独要料信息导出--库房");
        }
        
        public static void Show(System.Web.UI.Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + msg.ToString() + "');</script>");
        }

        protected void TextQadSite_Callback(object sender, CallbackEventArgsBase e)
        {
            string pline = e.Parameter;

            string sql = "select INTERNAL_NAME from CODE_INTERNAL where INTERNAL_CODE='" + pline + "' AND INTERNAL_TYPE_CODE='012'";
            DataTable dt = dc.GetTable(sql);

            ASPxComboBox yldd = sender as ASPxComboBox;
            yldd.DataSource = dt;
            yldd.ValueField = "INTERNAL_NAME";
            yldd.TextField = "INTERNAL_NAME";
            yldd.DataBind();
            yldd.SelectedIndex = -1;
        }
        protected void txtLocation_Init(object sender, EventArgs e)
        {
            try
            {

                if (Session["9100USER"].ToString() != "")
                {
                    string sql = Session["9100USER"] as string;
                    sqlLocation.SelectCommand = sql;
                    sqlLocation.DataBind();
                }
            }
            catch
            { }
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
    }
}