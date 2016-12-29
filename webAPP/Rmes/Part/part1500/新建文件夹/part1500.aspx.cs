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
 * 功能概述：按物料单独要料-三方要料
 * 
 */
namespace Rmes.WebApp.Rmes.Part.part1500
{
    public partial class part1500 : BasePage
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
            theProgramCode = "part1500";
            initTName();
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
        private void initTName()
        {
            // 要料类型
            string sql = "select INTERNAL_NAME TYPE_NAME from CODE_INTERNAL WHERE INTERNAL_TYPE_CODE ='009' order by INTERNAL_CODE";
            sqlTName.SelectCommand = sql;
            sqlTName.DataBind();
        }
        private void initCode()
        {
            //初始化用户下拉列表
            string sql = "select distinct a.pline_code,b.pline_name from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'";
            SqlCode.SelectCommand = sql;
            SqlCode.DataBind();
        }
        private void setCondition()
        {
            string sql = "select a.*,nvl(b.pt_desc2,' ') MATERIAL_NAME,nvl(c.ad_name,' ') GYS_NAME  from ms_single_mat a left join copy_pt_mstr b on a.material_code=b.pt_part left join COPY_AD_MSTR c on Upper(a.gys_code)=Upper(c.ad_addr)  "
                       + " where online_time>=to_date('" + ASPxDateEdit1.Text.Trim() + "','YYYY-MM-DD')  AND online_time<=to_date('" + ASPxDateEdit3.Text.Trim() + "','YYYY-MM-DD') AND material_code like '%" + TextMCode.Text.Trim() + "%' ";
            if (txtPlineCode.Text.Trim() != "")
            {
                sql = sql + "and gzdd = '" + txtPlineCode.Value.ToString() + "'";
            }            
            if (TextGCode.Text.Trim() != "")
            {
                sql = sql + " AND gys_code like '%" + TextGCode.Text.Trim() + "%'";
            }
            if (TextTName.Text.Trim() != "")
            {
                sql = sql + " AND type_name like '%" + TextTName.Text.Trim() + "%'";
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
            (ASPxGridView1.FindEditFormTemplateControl("txtHcFLAG") as ASPxComboBox).SelectedIndex = 1;
            (ASPxGridView1.FindEditFormTemplateControl("txtLqFlag") as ASPxComboBox).SelectedIndex = 1;
            (ASPxGridView1.FindEditFormTemplateControl("txtPackFlag") as ASPxComboBox).SelectedIndex = 1;
            (ASPxGridView1.FindEditFormTemplateControl("txtTName") as ASPxComboBox).SelectedIndex = 0;

        }
        protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            setCondition();
            ASPxGridView1.Selection.UnselectAll();
        }

        protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {

           // ASPxTextBox uPSo = ASPxGridView1.FindEditFormTemplateControl("txtPSo") as ASPxTextBox;
            ASPxComboBox uPCode = ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;
            ASPxTextBox uMCode = ASPxGridView1.FindEditFormTemplateControl("txtMCode") as ASPxTextBox;
            ASPxTextBox uMName = ASPxGridView1.FindEditFormTemplateControl("txtMName") as ASPxTextBox;
            ASPxTextBox uGCode = ASPxGridView1.FindEditFormTemplateControl("txtGCode") as ASPxTextBox;
            ASPxTextBox uGName = ASPxGridView1.FindEditFormTemplateControl("txtGName") as ASPxTextBox;
            ASPxComboBox uHcFLAG = ASPxGridView1.FindEditFormTemplateControl("txtHcFLAG") as ASPxComboBox;
            ASPxComboBox uQSite = ASPxGridView1.FindEditFormTemplateControl("txtQSite") as ASPxComboBox;
            ASPxTextBox uMNum = ASPxGridView1.FindEditFormTemplateControl("txtMNum") as ASPxTextBox;
            ASPxTextBox uRemark = ASPxGridView1.FindEditFormTemplateControl("txtRemark") as ASPxTextBox;
            ASPxComboBox uPackFlag = ASPxGridView1.FindEditFormTemplateControl("txtPackFlag") as ASPxComboBox;
            ASPxComboBox uLqFlag = ASPxGridView1.FindEditFormTemplateControl("txtLqFlag") as ASPxComboBox;
            ASPxComboBox uTName = ASPxGridView1.FindEditFormTemplateControl("txtTName") as ASPxComboBox;
            ASPxDateEdit uOnTime = ASPxGridView1.FindEditFormTemplateControl("txtOnTime") as ASPxDateEdit;
            string strHcFLAG = uHcFLAG.Value.ToString();
            string strPCode = uPCode.Value.ToString();
            string strPackFlag = uPackFlag.Value.ToString();
            string strQSite = uQSite.Value.ToString();
            string strLqFlag = uLqFlag.Value.ToString();
            string strTName = uTName.Value.ToString();
            string strMCode = uMCode.Text.Trim();
            string strMName = uMName.Text.Trim();
            string strGName = uGName.Text.Trim();
            string strGCode = uGCode.Text.Trim();
            string strMNum = uMNum.Text.Trim();
            string strRemark = uRemark.Text.Trim();
            string strLCode="";
            if(strPCode=="E")
            {strLCode = "ZS5";}
            if(strPCode=="W")
            {strLCode = "F010";}
            if(strPCode=="Z")
            {strLCode = "XA100";}
            if(strLqFlag=="Y")
            {strLCode = "LQ8010";}
            
            //if (strHcFLAG == "Y")
            //{
            //    Response.Write("<script>alert('该物料将冲抵回冲池！！是否继续？')</script>");

 
            //}
            //if (strHcFLAG == "N")
            //{
            //    Response.Write("<script>alert('不进行冲抵，是否继续？')</script>");

            //}
            string Sql = "insert into ms_single_mat(material_code,material_num,online_time,add_time,flag,gzdd,gys_code,add_yhdm,type_name,qadsite,location_code,HC_FLAG,BILL_REMARK)"
            + "VALUES('" + strMCode + "','" + strMNum + "',to_date('" + uOnTime.Value + "','yyyy-mm-dd hh24:mi:ss'),sysdate ,'N','" + strPCode + "','" + strGCode + "','" + theUserCode + "',"
            + "'" + strTName + "','" + strQSite + "','" + strLCode + "','" + strHcFLAG + "','" + strRemark + "')";
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
            string strGCode = e.Values["GYS_CODE"].ToString();
            string strPCode = e.Values["GZDD"].ToString();
            string strTName = e.Values["TYPE_NAME"].ToString();
            string strQadsite = e.Values["QADSITE"].ToString();
            string strTableName = "MS_SINGLE_MAT";
            string flag = e.Values["FLAG"].ToString();
            dataConn theDataConn = new dataConn("select func_check_delete_data('" + strTableName + "','" + theCompanyCode + "','MES','MES','MES','" + strMCode + "') from dual");
            theDataConn.OpenConn();
            string theRet = theDataConn.GetValue();
            //弹框提示已经计算的不能删除
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
                string Sql = "delete from ms_single_mat where material_code='" + strMCode + "' and gys_code='" + strGCode + "' and flag='N' and gzdd='" + strPCode + "' and type_name='" + strTName + "' and qadsite='" + strQadsite + "'";
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
            ASPxComboBox uHcFLAG = ASPxGridView1.FindEditFormTemplateControl("txtHcFLAG") as ASPxComboBox;
            ASPxComboBox uQSite = ASPxGridView1.FindEditFormTemplateControl("txtQSite") as ASPxComboBox;
            ASPxTextBox uMNum = ASPxGridView1.FindEditFormTemplateControl("txtMNum") as ASPxTextBox;
            ASPxTextBox uRemark = ASPxGridView1.FindEditFormTemplateControl("txtRemark") as ASPxTextBox;
            ASPxComboBox uPackFlag = ASPxGridView1.FindEditFormTemplateControl("txtPackFlag") as ASPxComboBox;
            ASPxComboBox uLqFlag = ASPxGridView1.FindEditFormTemplateControl("txtLqFlag") as ASPxComboBox;
            ASPxComboBox uTName = ASPxGridView1.FindEditFormTemplateControl("txtTName") as ASPxComboBox;
            ASPxDateEdit uOnTime = ASPxGridView1.FindEditFormTemplateControl("txtOnTime") as ASPxDateEdit;
            string strHcFLAG = uHcFLAG.Value.ToString();
            string strPCode = uPCode.Value.ToString();
            string strPackFlag = uPackFlag.Value.ToString();
            string strQSite = uQSite.Value.ToString();
            string strLqFlag = uLqFlag.Value.ToString();
            string strTName = uTName.Value.ToString();
            string strMCode = uMCode.Text.Trim();
            string strMName = uMName.Text.Trim();
            string strGName = uGName.Text.Trim();
            string strGCode = uGCode.Text.Trim();
            string strMNum = uMNum.Text.Trim();
            string strRemark = uRemark.Text.Trim();
            string Sql = "update ms_single_mat set material_num='" + strMNum + "',online_time=to_date('" + uOnTime.Value + "','yyyy-mm-dd hh24:mi:ss') ,add_yhdm='" + theUserCode + "',bill_remark='" + strRemark + "',hc_flag='" + strHcFLAG + "'"
            + " where material_code='" + strMCode + "' and gzdd='" + strPCode + "' and gys_code='" + strGCode + "' and type_name='" + strTName + "' and qadsite='" + strQSite + "' and flag='N'";

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
                //string strFlag = ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "FLAG").ToString();
                //if (strFlag == "Y")
                //{
                    //Show(this, "已经计算的不能修改!");
                    //ASPxGridView1.JSProperties.Add("cpCallbackName", "Fail");
                    //ASPxGridView1.JSProperties.Add("cpCallbackRet", "已经计算的不能修改！");
                //    return;
                //}
                
                (ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox).Enabled = false;
                (ASPxGridView1.FindEditFormTemplateControl("txtMCode") as ASPxTextBox).Enabled = false;
                (ASPxGridView1.FindEditFormTemplateControl("txtTName") as ASPxComboBox).Enabled = false;

            }
            string Sql = " select distinct a.pline_code,b.pline_name as showtext from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "' ";
            DataTable dt = dc.GetTable(Sql);
            ASPxComboBox uPCode = ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;
            uPCode.DataSource = dt;
            uPCode.TextField = dt.Columns[1].ToString();
            uPCode.ValueField = dt.Columns[0].ToString();
         
            string Sql2 = "select INTERNAL_NAME TYPE_NAME,INTERNAL_CODE TYPE_CODE from CODE_INTERNAL WHERE INTERNAL_TYPE_CODE ='009' order by INTERNAL_CODE";
            DataTable dt2 = dc.GetTable(Sql2);
            ASPxComboBox uLCode = ASPxGridView1.FindEditFormTemplateControl("txtTName") as ASPxComboBox;
            uLCode.DataSource = dt2;
           
            uLCode.TextField = dt2.Columns[0].ToString();

            DataTable dt1 = new DataTable();
            dt1.Columns.Add("VALUE1", typeof(string));
            dt1.Columns.Add("TEXT1", typeof(string));
            DataRow dr1 = dt1.NewRow();
            dt1.Rows.Add("Y", "是");
            dt1.Rows.Add("N", "否");
            ASPxComboBox gcfalg = ASPxGridView1.FindEditFormTemplateControl("txtHcFLAG") as ASPxComboBox;
            gcfalg.DataSource = dt1;
            gcfalg.TextField = dt1.Columns[1].ToString();
            gcfalg.ValueField = dt1.Columns[0].ToString();

            DataTable dt3 = new DataTable();
            dt3.Columns.Add("VALUE1", typeof(string));
            dt3.Columns.Add("TEXT1", typeof(string));
            DataRow dr3 = dt3.NewRow();
            dt3.Rows.Add("Y", "是");
            dt3.Rows.Add("N", "否");
            ASPxComboBox lqflag = ASPxGridView1.FindEditFormTemplateControl("txtLqFlag") as ASPxComboBox;
            lqflag.DataSource = dt3;
            lqflag.TextField = dt3.Columns[1].ToString();
            lqflag.ValueField = dt3.Columns[0].ToString();

            DataTable dt4 = new DataTable();
            dt4.Columns.Add("VALUE1", typeof(string));
            dt4.Columns.Add("TEXT1", typeof(string));
            DataRow dr4 = dt4.NewRow();
            dt4.Rows.Add("N", "否");
            dt4.Rows.Add("Y", "是");
            ASPxComboBox packflag = ASPxGridView1.FindEditFormTemplateControl("txtPackFlag") as ASPxComboBox;
            packflag.DataSource = dt4;
            packflag.TextField = dt4.Columns[1].ToString();
            packflag.ValueField = dt4.Columns[0].ToString();

            if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
            {
                (ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox).Enabled = false;
                (ASPxGridView1.FindEditFormTemplateControl("txtMCode") as ASPxTextBox).Enabled = false;
                (ASPxGridView1.FindEditFormTemplateControl("txtTName") as ASPxComboBox).Enabled = false;
            }
        }

        //修改数据校验
        protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            string strMCode = e.NewValues["MATERIAL_CODE"].ToString().Trim();
            string strQSite = e.NewValues["QADSITE"].ToString().Trim();
            string strGCode = e.NewValues["GYS_CODE"].ToString().Trim();
            string strPCode = e.NewValues["GZDD"].ToString().Trim();
            string strTName = e.NewValues["TYPE_NAME"].ToString().Trim();
            string strFlag = ASPxGridView1.GetRowValues(ASPxGridView1.FocusedRowIndex, "FLAG").ToString();
            
            //判断是否重复
            if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
            {
                if (strFlag == "Y")
                {
                    e.RowError = "已经计算的不能修改！";

                    return;
                }
                string chSql = "select gys_code from ms_mat_vender where material_code='" + strMCode + "' and qadsite='" + strQSite + "' and gys_code='" + strGCode + "'";
                DataTable dt = dc.GetTable(chSql);
                if (dt.Rows.Count <= 0)
                {
                    e.RowError = "零件供应商不存在对应关系！";
                    
                }
            }
            if (ASPxGridView1.IsNewRowEditing && !ASPxGridView1.IsEditing)
            {
                string chSql = "select ad_addr from copy_ad_mstr where Upper(ad_addr)='" + strGCode + "";
                DataTable dt = dc.GetTable(chSql);
                if (dt.Rows.Count <= 0)
                {
                    e.RowError = "供应商代码不合法！";
                    return;
                }
                string chSql2 = "select gys_code from ms_mat_vender where material_code='" + strMCode + "' and qadsite='" + strQSite + "' and gys_code='" + strGCode + "'";
                DataTable dt2 = dc.GetTable(chSql2);
                if (dt2.Rows.Count <= 0)
                {
                    e.RowError = "零件供应商不存在对应关系！";
                    return;
                }
                string chSql3 = "select * from ms_single_mat where material_code='" + strMCode + "' and gys_code='" + strGCode + "' and flag='N' and gzdd='" + strPCode + "' and type_name='" + strTName + "' and qadsite='" + strQSite + "'";
                DataTable dt3 = dc.GetTable(chSql3);
                if (dt3.Rows.Count != 0)
                {
                    e.RowError = "该物料该供应商数据已添加且尚未要料！";
                    return;
                }
            }

        }
        protected void btnXlsExport_Click(object sender, EventArgs e)
        {
            ASPxGridViewExporter1.WriteXlsToResponse("按物料单独要料信息导出--三方");
        }
        //关于批量导入
        protected void ASPxButton_Import_Click(object sender, EventArgs e)
        {
            try
            {
                ASPxButton_Import.Enabled = false;

                string path = File1.Value;//上传文件路径

                if (path == "")
                {
                    ASPxButton_Import.Enabled = true;
                    return;
                }

                string uploadPath = Server.MapPath(Request.ApplicationPath); //上传目录及文件名
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

                if (!Directory.Exists("C:\\inetpub\\wwwroot\\webapp\\Rmes\\File\\excel"))
                {
                    Directory.CreateDirectory("C:\\inetpub\\wwwroot\\webapp\\Rmes\\File\\excel");
                }
                uploadPath = uploadPath + "\\Rmes\\File\\excel\\" + fileName; //得到上传目录及文件名称  
                File1.PostedFile.SaveAs(uploadPath);       //上传文件到服务器  

                DataTable dt = GetExcelDataTable(uploadPath);

                int count = 0;
                foreach (DataRow t in dt.Rows)
                {
                    if (t.IsNull(0)) continue;
                    string Pcode = "", strTname = "";
                    string itemCode = t[0].ToString().Trim();
                    string strGCode = t[1].ToString().Trim();
                    string sl = t[2].ToString().Trim();
                    string Tname = t[3].ToString().Trim();
                    string strQSite = t[4].ToString().Trim();
                    string hcFlag = t[5].ToString().Trim();
                    string PackFlag = t[6].ToString().Trim();
                    string bz = t[7].ToString().Trim();
                    if (txtPlineCode.Text.Trim() != "")
                    {
                        Pcode = txtPlineCode.Value.ToString();
                    }
                    if (Tname.Substring(0, 2) == "备件")
                    {
                        strTname = "备件要料";

                    }
                    else if (Tname.Substring(0, 2) == "生产")
                    {
                        strTname = "生产要料";
                    }
                    else
                    {
                        strTname = Tname;
                    }
                    if (itemCode == "" || strGCode == "" || sl == "" || Tname == "" || strQSite == "" || hcFlag == "" || PackFlag == "")
                    {
                        Show(this, itemCode + ":" + "缺少要料参数");
                        ASPxButton_Import.Enabled = true;
                        return;
                    }
                    if (ASPxDateEdit2.Text.ToString() == "" || txtPlineCode.Value == null)
                    {
                        Show(this, itemCode + ":" + "请选择生产线和录入到货时间后再导入");
                        ASPxButton_Import.Enabled = true;
                        return;
                    }
                    try
                    {
                        int qty = Convert.ToInt32(sl);
                    }
                    catch
                    {
                        Show(this, itemCode + ":" + "数量输入不合法");
                        ASPxButton_Import.Enabled = true;
                        return;
                    }
                    //判断零件代码是否符合规范
                    string sqlItem = "select count(1) from copy_pt_mstr where pt_part='" + itemCode + "' ";
                    if (dc.GetValue(sqlItem) == "0")
                    {
                        Show(this, itemCode + ":" + "零件不存在！");
                        ASPxButton_Import.Enabled = true;
                        return;
                    }
                    string chSql2 = "select count(1) from copy_ad_mstr where Upper(ad_addr)='" + strGCode + "'";
                    if (dc.GetValue(chSql2) == "0")
                    {
                        Show(this, itemCode + ":" + "供应商代码不合法！");
                        ASPxButton_Import.Enabled = true;
                        return;
                    }

                    string chSql3 = "select count(1) from ms_mat_vender where material_code='" + itemCode + "' and qadsite='" + strQSite + "' and gys_code='" + strGCode + "'";
                    if (dc.GetValue(chSql3) == "0")
                    {
                        Show(this, itemCode + ":" + "零件供应商不存在对应关系！");
                        ASPxButton_Import.Enabled = true;
                        return;
                    }

                    string chSql4 = "select count(1) from ms_single_mat where material_code='" + itemCode + "' and gys_code='" + strGCode + "' and flag='N' and gzdd='" + Pcode + "' and type_name='" + strTname + "' and qadsite='" + strQSite + "'";
                    if (dc.GetValue(chSql4) != "0")
                    {
                        Show(this, itemCode + ":" + "该物料该供应商数据已添加且尚未要料！");
                        ASPxButton_Import.Enabled = true;
                        return;
                    }

                    if (TextQadSite.Value.ToString() != strQSite)
                    {
                        Show(this, itemCode + ":" + "选择的生产线和excel导入的要料地点不匹配！");
                        ASPxButton_Import.Enabled = true;
                        return;
                    }
                    string Sql = "insert into ms_single_mat(material_code,material_num,online_time,add_time,flag,gzdd,gys_code,add_yhdm,type_name,qadsite,HC_FLAG,BILL_REMARK,PACK_FLAG)"
                                + "VALUES('" + itemCode + "','" + sl + "',to_date('" + ASPxDateEdit2.Text.ToString() + "','YYYY-MM-DD'),sysdate ,'N','" + Pcode + "','" + strGCode + "','" + theUserCode + "',"
                                + "'" + strTname + "','" + strQSite + "','" + hcFlag + "','" + bz + "','" + PackFlag + "')";
                    dc.ExeSql(Sql);
               }
                ASPxButton_Import.Enabled = true;
                setCondition();
                return;
            }
            catch (Exception ex2)
            {
                ASPxCallbackPanel6.JSProperties.Add("cpCallbackName", "Fail");
                ASPxCallbackPanel6.JSProperties.Add("cpCallbackRet", ex2.Message);
                ASPxButton_Import.Enabled = true;
            }
        }
        protected void ASPxCallbackPanel6_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                ASPxButton_Import.Enabled = false;

                string[] s = e.Parameter.Split('|');
                string flag = s[0];
                string path = flag;//上传文件路径
                if (path == "")
                {
                    ASPxButton_Import.Enabled = true;
                    return;
                }

                string uploadPath = Server.MapPath(Request.ApplicationPath); //上传目录及文件名
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";


                uploadPath = uploadPath + "\\Rmes\\File\\excel\\" + fileName; //得到上传目录及文件名称  
                File1.PostedFile.SaveAs(uploadPath);       //上传文件到服务器  

                DataTable dt = GetExcelDataTable(uploadPath);

                foreach (DataRow t in dt.Rows)
                {
                    if (t.IsNull(0)) continue;
                    string itemCode = t[0].ToString().Trim();
                    string strGCode = t[1].ToString().Trim();
                    string sl = t[2].ToString().Trim();
                    string Tname = t[3].ToString().Trim();
                    string strQSite = t[4].ToString().Trim();
                    string hcFlag = t[5].ToString().Trim();
                    string bz = t[6].ToString().Trim();

                    //物料代码是否存在
                    string sqlItem = "select count(1) from copy_pt_mstr where pt_part='" + itemCode + "' ";
                    if (dc.GetValue(sqlItem) == "0")
                    {
                        Show(this, itemCode + ":" + "零件不存在！");
                        ASPxButton_Import.Enabled = true;
                        return;
                    }
                }
                foreach (DataRow t in dt.Rows)
                {
                    try
                    {
                        string itemCode = t[0].ToString().Trim();
                        string strGCode = t[1].ToString().Trim();
                        string sl = t[2].ToString().Trim();
                        string Tname = t[3].ToString().Trim();
                        string strQSite = t[4].ToString().Trim();
                        string hcFlag = t[5].ToString().Trim();
                        string bz = t[6].ToString().Trim();
                        string itemName = "", GysName = "";

                        //物料代码是否存在
                        string sqlItem = "select count(1) from copy_pt_mstr where pt_part='" + itemCode + "' ";
                        if (dc.GetValue(sqlItem) == "0")
                        {
                            Show(this, itemCode + ":" + "零件不存在！");
                            ASPxButton_Import.Enabled = true;
                            return;
                        }

                        else
                        {
                            string sql = "select nvl(pt_desc2,' ') from copy_pt_mstr where pt_part='" + itemCode + "'";
                            dc.setTheSql(sql);
                            itemName = dc.GetValue();
                        }
                        string sql2 = "select nvl(ad_name,' ') from copy_ad_mstr where upper(ad_addr)='" + strGCode + "'";
                        dc.setTheSql(sql2);
                        GysName = dc.GetValue();
                        userManager theUserManager = (userManager)Session["theUserManager"];


                        //try
                        //{
                        //    PlanFactory.PL_CREATE_PLAN("ADD", theCompanyCode, pline, plineName, seq, planCode, planSo, series, "F", theUserId, theUserManager.getUserName(), ASPxDateEdit1.Date.ToString("yyyy-MM-dd"), ASPxDateEdit1.Date.ToString("yyyy-MM-dd"), accountdate1, planQty, "0", "0", rountingsite, gylx1, planCus, remark, ROUNTING_CODE1, "Y");
                        //}
                        //catch (Exception ex)
                        //{
                        //    ASPxCallbackPanel6.JSProperties.Add("cpCallbackName", "Fail");
                        //    ASPxCallbackPanel6.JSProperties.Add("cpCallbackRet", ex.Message);
                        //    ASPxButton_Import.Enabled = true;
                        //    return;
                        //}
                    }
                    catch (Exception ex1)
                    {
                        ASPxCallbackPanel6.JSProperties.Add("cpCallbackName", "Fail");
                        ASPxCallbackPanel6.JSProperties.Add("cpCallbackRet", ex1.Message);
                        ASPxButton_Import.Enabled = true;
                        return;
                    }
                }
               ASPxButton_Import.Enabled = true;
                return;
            }
            catch (Exception ex2)
            {
                ASPxCallbackPanel6.JSProperties.Add("cpCallbackName", "Fail");
                ASPxCallbackPanel6.JSProperties.Add("cpCallbackRet", ex2.Message);
                ASPxButton_Import.Enabled = true;
            }
        }
        public static DataTable GetExcelDataTable(string path)
        {
            /*Office 2007*/
            string ace = "Microsoft.ACE.OLEDB.12.0";
            /*Office 97 - 2003*/
            string jet = "Microsoft.Jet.OLEDB.4.0";
            string xl2007 = "Excel 12.0 Xml";
            string xl2003 = "Excel 8.0";
            string imex = "IMEX=1";
            /* csv */
            string text = "text";
            string fmt = "FMT=Delimited";
            string hdr = "Yes";
            string conn = "Provider={0};Data Source={1};Extended Properties=\"{2};HDR={3};{4}\";";

            //string select = sql;
            string ext = Path.GetExtension(path);
            OleDbDataAdapter oda;
            DataTable dt = new DataTable("data");
            switch (ext.ToLower())
            {
                case ".xlsx":
                    conn = String.Format(conn, ace, Path.GetFullPath(path), xl2007, hdr, imex);
                    break;
                case ".xls":
                    conn = String.Format(conn, jet, Path.GetFullPath(path), xl2003, hdr, imex);
                    break;
                case ".csv":
                    conn = String.Format(conn, jet, Path.GetDirectoryName(path), text, hdr, fmt);
                    //sheet = Path.GetFileName(path);
                    break;
                default:
                    throw new Exception("File Not Supported!");
            }
            OleDbConnection con = new OleDbConnection(conn);
            con.Open();
            //select = string.Format(select, sql);

            DataTable dtbl1 = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            //dataGridView2.DataSource = dtbl1;   
            string strSheetName = dtbl1.Rows[0][2].ToString().Trim();

            string select = string.Format("SELECT * FROM [{0}]", strSheetName);

            oda = new OleDbDataAdapter(select, con);
            oda.Fill(dt);
            con.Close();
            return dt;
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
    }
}