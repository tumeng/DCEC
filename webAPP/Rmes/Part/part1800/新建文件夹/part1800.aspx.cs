using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PetaPoco;
using Oracle.DataAccess.Client;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.Pub.Data;
using Rmes.Web.Base;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using Rmes.DA.Procedures;
using System.Collections;
using DevExpress.Web.ASPxGridLookup;

/**
 * 功能概述：最小包装剩余维护
 * 作者：游晓航
 * 创建时间：2016-08-03
 */
public partial class Rmes_Part_part1800_part1800 : Rmes.Web.Base.BasePage  
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
            theProgramCode = "part1800";
            initPlanCode();
            initCode();
            initMcode();
            setCondition();
            if (Request["opFlag"] == "getEditSeries")
            {
                string str1 = "", str2 = "";
                string MCode = Request["MCODE"].ToString().Trim();
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
                string sql = "select pt_desc2 from copy_pt_mstr where pt_part='" + MCode + "'";
                dc.setTheSql(sql);
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

                str1 = str1 + "," + str2;
                this.Response.Write(str1);
                this.Response.End();
            }


        }
        private void initPlanCode()
        {
            //初始化计划下拉列表（物料确认过的计划,且未参与过物流计算的计划）
            string sql = "select distinct plan_code from ms_single_plan where gzdd in (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') order by plan_code";
            sqlPlanCode.SelectCommand = sql;
            sqlPlanCode.DataBind();
        }
        private void initCode()
        {
            //初始化工位下拉列表
            string sql = "select distinct a.pline_code,b.pline_name from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "' ";
            sqlPCode.SelectCommand = sql;
            sqlPCode.DataBind();
        }
        private void initMcode()
        {
            //初始化零件代码下拉列表
            string sql = "select distinct pt_part MATERIAL_CODE  from copy_pt_mstr ";
            SqlMcode.SelectCommand = sql;
            SqlMcode.DataBind();
        }

        private void setCondition()
        {
            string sql = "";
            sql = "select A.*,B.PT_DESC2 from MS_OVER_MAT A left join copy_pt_mstr B ON A.MATERIAL_CODE=B.Pt_Part order by a.input_time desc nulls last ";

            DataTable dt = dc.GetTable(sql);
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();

        }
        //protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        //{
        //    setCondition();
        //    ASPxGridView1.Selection.UnselectAll();

        //}

        protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {

            
            //调用存储过程进行处理

            MS_MODIFY_OVER_MAT sp = new MS_MODIFY_OVER_MAT()
            {
                FUNC1 = "UPDATE",
                MATERIALCODE1 = e.NewValues["MATERIAL_CODE"].ToString(),
                LINESIDENUM1 = e.NewValues["MATERIAL_NUM"].ToString(),
                GZDD1 = e.NewValues["GZDD"].ToString(),
                YHDM1 = theUserCode,
                QADSITE1 = e.NewValues["QADSITE"].ToString()
            };
            Rmes.DA.Base.Procedure.run(sp);
            
            setCondition();
            e.Cancel = true;
            ASPxGridView1.CancelEdit();
        }

        //删除
        protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            MS_MODIFY_OVER_MAT sp = new MS_MODIFY_OVER_MAT()
            {
                FUNC1 = "DELETE",
                MATERIALCODE1 = e.Values["MATERIAL_CODE"].ToString(),
                LINESIDENUM1 = e.Values["MATERIAL_NUM"].ToString(),
                GZDD1 = e.Values["GZDD"].ToString(),
                YHDM1 = theUserCode,
                QADSITE1 = e.Values["QADSITE"].ToString()
            };
            Rmes.DA.Base.Procedure.run(sp);

            setCondition();
            e.Cancel = true;
        }

        //修改
        protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {

            MS_MODIFY_OVER_MAT sp = new MS_MODIFY_OVER_MAT()
            {
                FUNC1 = "UPDATE",
                MATERIALCODE1 = e.NewValues["MATERIAL_CODE"].ToString(),
                LINESIDENUM1 = e.NewValues["MATERIAL_NUM"].ToString(),
                GZDD1 = e.NewValues["GZDD"].ToString(),
                YHDM1 = theUserCode,
                QADSITE1 = e.NewValues["QADSITE"].ToString()
            };
            Rmes.DA.Base.Procedure.run(sp);

            setCondition();
            e.Cancel = true;
            ASPxGridView1.CancelEdit();
        }

        //创建EDITFORM前
        protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
        {
            string Sql = " select distinct a.pline_code,b.pline_name as showtext from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "' ";
            DataTable dt = dc.GetTable(Sql);
            ASPxComboBox uPCode = ASPxGridView1.FindEditFormTemplateControl("txtPCode") as ASPxComboBox;
            uPCode.DataSource = dt;
            uPCode.TextField = dt.Columns[1].ToString();
            uPCode.ValueField = dt.Columns[0].ToString();

            if (ASPxGridView1.IsNewRowEditing)
            {
                uPCode.SelectedIndex = 0;
                //(ASPxGridView1.FindEditFormTemplateControl("combPcode") as ASPxComboBox).SelectedIndex = 0;
            }
            if (!ASPxGridView1.IsNewRowEditing && ASPxGridView1.IsEditing)
            {
                ///主键不可以修改
                (ASPxGridView1.FindEditFormTemplateControl("txtMCode") as ASPxGridLookup).ClientEnabled= false;
               

            }
        }

        //修改数据校验
        protected void ASPxGridView1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {

          

        }
        protected void btnXlsExport_Click(object sender, EventArgs e)
        {
            ASPxGridViewExporter1.WriteXlsToResponse("三方物流包装量多余物料信息导出");
        }
        protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            setCondition();
            ASPxGridView1.Selection.UnselectAll();

        }
        protected void ASPxCbSubmit_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
        {
            string collection = e.Parameter;
            try
            {
                if (collection == "Clear")
                {
                    string strPCode = combPcode.Text.Trim().ToUpper();
                  
                    if (strPCode == "")
                    {
                        e.Result = "Fail,请选择要清零的生产线！";
                        return;

                    }
                    else{
                        string chSql = "update ms_over_mat set material_num=0 where gzdd='" + strPCode + "'";

                        DataTable dt = dc.GetTable(chSql);

                        string Sql = "insert into ms_over_mat_log(material_code,material_num,log_time,gzdd,gys_code,bill_code,log_user,qadsite) values('SYSTEM','0',sysdate,'" + strPCode + "','SYSTEM','置所有零件数量为0','" + theUserCode + "','SYSTEM')";
                        DataTable dt1 = dc.GetTable(Sql);
                    }
                    e.Result = "OK,已将" + strPCode + "生产线的数量清零";
                }
            }
            catch (Exception e1)
            {
                e.Result = "Fail,提交失败" + e1.Message + "！";
                return;
            }
            setCondition();
        }
    }
