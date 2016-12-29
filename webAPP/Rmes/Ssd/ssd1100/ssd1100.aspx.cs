using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rmes.Pub.Data;
using PetaPoco;
using Rmes.DA.Base;
using DevExpress.Web.ASPxGridView;
using DevExpress.Utils;
using System.Data;
using Rmes.Web.Base;
using DevExpress.Web.ASPxClasses;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
/**
 * 2016-06-24
 * 涂猛
 * 库房确认
 * */

namespace Rmes.WebApp.Rmes.Ssd.ssd1100
{
    public partial class ssd1100 : BasePage
    {

        private dataConn dc = new dataConn();
        private PubCs thePubCs = new PubCs();
        private string theProgramCode;
        private static string theCompanyCode, theUserId;

        public Database db = DB.GetInstance();
        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();
            theProgramCode = "ssd1100";
            if (!IsPostBack)
            {
                ASPxDateEdit1.Date = DateTime.Now;
                ASPxDateEdit2.Date = DateTime.Now;
                List<ProductLineEntity> user_plines = ProductLineFactory.GetByUserID(theUserManager.getUserId(), theProgramCode);
                ASPxComboBoxPline.DataSource = user_plines;
                ASPxComboBoxPline.TextField = "PLINE_NAME";
                ASPxComboBoxPline.ValueField = "PLINE_CODE";
                ASPxComboBoxPline.DataBind();
            }

            setCondition();
        }

        private void setCondition()
        {
            //string sql = "select * from VW_DATA_PLAN where pline_code='" + ASPxComboBoxPline.Value.ToString() + "' and confirm_flag='Y' and run_flag<>'Y' and bom_flag='Y' and run_flag<>'C' and run_flag<>'F' and begin_date>=to_date('" + ASPxDateEdit1.Date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') and begin_date<=to_date('" + ASPxDateEdit2.Date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') and  pline_code in(select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') order by begin_date,plan_seq ";
            
            string sql = "select * from VW_DATA_PLAN where pline_code='" + ASPxComboBoxPline.Value.ToString()+ "' and begin_date>=to_date('" + ASPxDateEdit1.Date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') and begin_date<=to_date('" + ASPxDateEdit2.Date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') and  pline_code in(select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') order by begin_date,plan_seq ";
            DataTable dt = dc.GetTable(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string remark1 = dt.Rows[i]["REMARK"].ToString();
                sql = "select FUNC_GET_REMARK('" + dt.Rows[i]["PLAN_CODE"].ToString() + "','" + remark1 + "') from dual ";
                dc.setTheSql(sql);
                dt.Rows[i]["REMARK"] = dc.GetValue(); ;
            }
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
        }

        protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            setCondition();
            ASPxGridView1.Selection.UnselectAll();
        }

        //计划调序
        public void ASPxGridView1_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
        {
            string s = e.Parameters;
            string[] s1 = s.Split('|');
            string type1 = s1[0];
            int rowIndex = int.Parse(s1[1]);
            string sql = "";
            string rmesid, planseq, begindate, plinecode1;

            ASPxGridView atl1 = (ASPxGridView)sender;
            int count1 = atl1.Selection.Count;
            List<object> aa = atl1.GetSelectedFieldValues("RMES_ID");
            List<object> flags = atl1.GetSelectedFieldValues("ITEM_FLAG");
            List<object> flags1 = atl1.GetSelectedFieldValues("CONFIRM_FLAG");
            List<object> flags2 = atl1.GetSelectedFieldValues("BOM_FLAG");
            List<object> flags3 = atl1.GetSelectedFieldValues("RUN_FLAG");
            List<object> flags4 = atl1.GetSelectedFieldValues("THIRD_FLAG");
            //try
            //{
            //    rmesid = ASPxGridView1.GetRowValues(rowIndex, "RMES_ID").ToString();
            //    planseq = ASPxGridView1.GetRowValues(rowIndex, "PLAN_SEQ").ToString();
            //    begindate = Convert.ToString(ASPxGridView1.GetRowValues(rowIndex, "BEGIN_DATE")).Substring(0, Convert.ToString(ASPxGridView1.GetRowValues(rowIndex, "BEGIN_DATE")).IndexOf(" "));
            //    plinecode1 = ASPxGridView1.GetRowValues(rowIndex, "PLINE_CODE") as string;
            //}
            //catch
            //{
            //    e.Result = "Fail,缺少关键值！";
            //    return;
            //}
            switch (type1)
            {
                case "Cbt1": //接收
                    if (flags1.Contains("N") || flags2.Contains("N") || flags3.Contains("Y") || flags3.Contains("C") || flags3.Contains("F"))
                    {
                            e.Result = "Fail,包含不可操作项！";
                            //e.Result = "OK,上调成功！";
                            break;
                    }
                    else
                    {
                        if (flags.Contains("Y") || flags.Contains("R"))
                        {
                            e.Result = "Fail,包含不可操作项！";
                            //e.Result = "OK,上调成功！";
                            break;
                        }
                        for (int i = 0; i < count1; i++)
                        {
                            sql = "update data_plan set Item_Flag='R' where rmes_id='" + aa[i] + "'";
                            dc.ExeSql(sql);
                        }
                        e.Result = "OK,已接收！";
                    }
                    //e.Result = "OK,上调成功！";
                    break;
                case "Cbt2": //取消接收
                    if (flags.Contains("Y") || flags.Contains("N"))
                    {
                        e.Result = "Fail,包含不可操作项！";
                        //e.Result = "OK,上调成功！";
                        break;
                    }
                    for (int i = 0; i < count1; i++)
                    {
                        sql = "update data_plan set Item_Flag='N' where rmes_id='" + aa[i] + "'";
                        dc.ExeSql(sql);
                    }
                    e.Result = "OK,取消接收成功！";
                    break;
                case "Confirm":
                    if (flags.Contains("Y") || flags.Contains("N"))
                    {
                        e.Result = "Fail,包含不可操作项！";
                        //e.Result = "OK,上调成功！";
                        break;
                    }
                    bool isth = false;
                    for (int i = 0; i < count1; i++)
                    {
                        string plancode1 = dc.GetValue("select plan_code from data_plan where rmes_id='" + aa[i] + "'");
                        if (dc.GetValue("select count(1) from SJBOMSOTH where jhdm='" + plancode1 + "' and istrue=0 ") != "0")
                        {
                            isth = true;
                            e.Result = "Fail," + plancode1 + "一对一替换未确认！";
                            break;
                        }
                        if (dc.GetValue("select count(1) from sjbomsothmuti where jhdm='" + plancode1 + "' and istrue=0 ") != "0")
                        {
                            isth = true;
                            e.Result = "Fail," + plancode1 + "多对多替换未确认！";
                            break;
                        }
                    }
                    if (!isth)
                    {
                        for (int i = 0; i < count1; i++)
                        {
                            sql = "update data_plan set Item_Flag='Y' where rmes_id='" + aa[i] + "'";
                            dc.ExeSql(sql);
                        }
                        e.Result = "OK,已确认！";
                    }
                    break;
                case "Cancel":
                    if (flags.Contains("N") || flags.Contains("R") || flags4.Contains("Y"))
                    {
                        e.Result = "Fail,包含不可操作项！";
                        //e.Result = "OK,上调成功！";
                        break;
                    }
                    for (int i = 0; i < count1; i++)
                    {
                        sql = "update data_plan set Item_Flag='R' where rmes_id='" + aa[i] + "'";
                        dc.ExeSql(sql);
                    }
                    e.Result = "OK,取消确认！";
                    break;
                default:

                    break;
            }
        }

        public void ASPxGridView1_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            ASPxGridView grid = (ASPxGridView)sender;
            string run_flag = grid.GetRowValues(e.VisibleIndex, "RUN_FLAG") as string;
            string item_flag = grid.GetRowValues(e.VisibleIndex, "ITEM_FLAG") as string;
            string third_flag = grid.GetRowValues(e.VisibleIndex, "THIRD_FLAG") as string;
            string confirm_flag = grid.GetRowValues(e.VisibleIndex, "CONFIRM_FLAG") as string;
            string bom_flag = grid.GetRowValues(e.VisibleIndex, "BOM_FLAG") as string;
            switch (e.ButtonID)
            {
                case "Cbt1":
                    if (e.CellType == GridViewTableCommandCellType.Filter) break;
                    if (item_flag != "N")
                        e.Visible = DefaultBoolean.False;
                    if (item_flag == "N")
                        e.Visible = DefaultBoolean.True;
                    if (confirm_flag=="N")
                        e.Visible = DefaultBoolean.False;
                    if (bom_flag == "N")
                        e.Visible = DefaultBoolean.False;
                    if (run_flag != "N")
                        e.Visible = DefaultBoolean.False;
                    break;
                case "Cbt2":
                    if (e.CellType == GridViewTableCommandCellType.Filter) break;
                    if (item_flag == "R")
                        e.Visible = DefaultBoolean.True;
                    if (item_flag != "R")
                        e.Visible = DefaultBoolean.False;
                    if (confirm_flag == "N")
                        e.Visible = DefaultBoolean.False;
                    if (bom_flag == "N")
                        e.Visible = DefaultBoolean.False;
                    if (run_flag != "N")
                        e.Visible = DefaultBoolean.False;
                    break;
                case "Confirm":
                    if (e.CellType == GridViewTableCommandCellType.Filter) break;
                    if (item_flag == "R")
                        e.Visible = DefaultBoolean.True;
                    if (item_flag != "R")
                        e.Visible = DefaultBoolean.False;
                    if (confirm_flag == "N")
                        e.Visible = DefaultBoolean.False;
                    if (bom_flag == "N")
                        e.Visible = DefaultBoolean.False;
                    if (run_flag != "N")
                        e.Visible = DefaultBoolean.False;
                    break;

                case "Cancel":
                    //if (item_flag == "Y" && confirm_flag == "Y" && run_flag == "Y")
                    if (e.CellType == GridViewTableCommandCellType.Filter) break;
                    if (item_flag == "Y")
                        e.Visible = DefaultBoolean.True;
                    if (item_flag != "Y")
                        e.Visible = DefaultBoolean.False;
                    if (third_flag == "P")//正参与三方物料计算的不能取消确认
                        e.Visible = DefaultBoolean.False;
                    if (confirm_flag == "N")
                        e.Visible = DefaultBoolean.False;
                    if (bom_flag == "N")
                        e.Visible = DefaultBoolean.False;
                    if (run_flag != "N")
                        e.Visible = DefaultBoolean.False;
                    break;
                
            }
        }


        protected void ASPxGridView1_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.VisibleIndex < 0) return;

            string status = e.GetValue("ITEM_FLAG").ToString(); 
            switch (status)
            {
                case "Y":
                    e.Row.BackColor = System.Drawing.Color.LimeGreen;
                    break;
                case "R":
                    e.Row.BackColor = System.Drawing.Color.Yellow;
                    break;
            }

        }

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

        protected void btnXlsExport_Click(object sender, EventArgs e)
        {
            ASPxGridView1.Columns.RemoveAt(26);
            ASPxGridView1.Columns.RemoveAt(25);
            ASPxGridView1.Columns.RemoveAt(24);
            ASPxGridView1.Columns.RemoveAt(23);
            ASPxGridView1.Columns.RemoveAt(22);
            ASPxGridView1.Columns.RemoveAt(21);
            ASPxGridView1.Columns.RemoveAt(20);
            ASPxGridView1.Columns.RemoveAt(19);
            ASPxGridView1.Columns.RemoveAt(18);
            ASPxGridView1.Columns.RemoveAt(17);
            ASPxGridView1.Columns.RemoveAt(16);
            ASPxGridView1.Columns.RemoveAt(15);
            ASPxGridView1.Columns.RemoveAt(14);
            ASPxGridView1.Columns.RemoveAt(13);
            ASPxGridView1.Columns.RemoveAt(12);
            ASPxGridView1.Columns.RemoveAt(11);
            GridViewDataTextColumn d3 = new GridViewDataTextColumn();
            d3.Caption = "客户";
            d3.VisibleIndex = 7;
            d3.FieldName = "CUSTOMER_NAME";
            ASPxGridView1.Columns.Add(d3);

            GridViewDataTextColumn d1 = new GridViewDataTextColumn();
            d1.Caption = "备注";
            d1.VisibleIndex = 17;
            d1.FieldName = "REMARK";
            ASPxGridView1.Columns.Add(d1);

            ASPxGridViewExporter1.WriteXlsToResponse("导出计划");
        }

        protected void callbackPanel_Callback(object sender, CallbackEventArgsBase e)
        {
            litText.Text = GetNotes(e.Parameter).ToString();

        }
        object GetNotes(string id)
        {
            string sql = "SELECT PLAN_CODE,REMARK FROM VW_DATA_PLAN WHERE  RMES_ID='" + id + "'";
            dataConn dc = new dataConn(sql);
            DataTable dt = dc.GetTable();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string remark1 = dt.Rows[i]["REMARK"].ToString();
                sql = "select FUNC_GET_REMARK('" + dt.Rows[i]["PLAN_CODE"].ToString() + "','" + remark1 + "') from dual ";
                dc.setTheSql(sql);
                dt.Rows[i]["REMARK"] = dc.GetValue(); ;
            }
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["REMARK"];
            }
            return null;
        }
    }
}