using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rmes.Web.Base;
using Rmes.Pub.Data;
using PetaPoco;
using Rmes.DA.Base;
using System.Data;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxGridView;
using Rmes.DA.Entity;
using Rmes.DA.Factory;
/**
 * 2016-06-24
 * 涂猛
 * 生产确认
 * 修改时间：2016-09-27
 * 修改人：游晓航
 * 修改功能：新增一个解锁功能
 * */

namespace Rmes.WebApp.Rmes.Ssd.ssd1200
{
    public partial class ssd1200 : BasePage
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
            theProgramCode = "ssd1200";
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
            string sql = "select * from VW_DATA_PLAN where pline_code='" + ASPxComboBoxPline.Value.ToString() + "' and confirm_flag='Y' and item_flag='Y' and plan_qty>0 and run_flag<>'C' and run_flag<>'F'  and begin_date>=to_date('" + ASPxDateEdit1.Date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') and begin_date<=to_date('" + ASPxDateEdit2.Date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') and pline_code in(select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "' ) order by begin_date,plan_seq ";
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
        protected void callbackPanel1_Callback(object sender, CallbackEventArgsBase e)
        {
            ASPxMemo1.Text = "";
            string sql = "select plan_code from data_plan where rmes_id='" + e.Parameter + "'";
            string plancode = dc.GetValue(sql);
            sql = "select sn from data_plan_sn where plan_code='" + plancode + "'   ";//and is_valid='Y'
            DataTable dt = dc.GetTable(sql);
            string txt1 = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                txt1 = txt1 + dt.Rows[i][0].ToString() + (char)13 + (char)10;
            }
            ASPxMemo1.Text = txt1;
        }
        //计划调序
        public void ASPxGridView1_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
        {
            string s = e.Parameters;
            string[] s1 = s.Split('|');
            string type1 = s1[0];
            
            string sql = "";

            ASPxGridView atl1 = (ASPxGridView)sender;
            int count1 = atl1.Selection.Count;
            List<object> aa = atl1.GetSelectedFieldValues("RMES_ID");
            List<object> flags = atl1.GetSelectedFieldValues("RUN_FLAG");
            List<object> plancode = atl1.GetSelectedFieldValues("PLAN_CODE");
            List<object> plinecode = atl1.GetSelectedFieldValues("PLINE_CODE");

         
            switch (type1)
            {
                case "Confirm":
                    if (flags.Contains("Y"))
                    {
                        e.Result = "Fail,包含不可操作项！";
                        //e.Result = "OK,上调成功！";
                        break;
                    }
                    for (int i = 0; i < count1; i++)
                    {
                        sql = "update data_plan set RUN_FLAG='Y' where rmes_id='" + aa[i] + "' and run_flag='N'";
                        dc.ExeSql(sql);
                    }
                    e.Result = "OK,已执行！";
                    break;
                case "Cancel":
                    if (flags.Contains("N"))
                    {
                        e.Result = "Fail,包含不可操作项！";
                        //e.Result = "OK,上调成功！";
                        break;
                    }
                    for (int i = 0; i < count1; i++)
                    {
                        sql = "update data_plan set RUN_FLAG='N' where rmes_id='" + aa[i] + "' and run_flag='Y'";
                        dc.ExeSql(sql);
                    }
                    e.Result = "OK,已取消！";
                    break;
                case "Unlock":
                    string password = s1[1];
                   
                    for (int i = 0; i < count1; i++)
                    {
                        sql = "select yhmm from ms_skip_password where gzdd='" + plinecode[i] + "' and yhmm='" + password + "'";
                        dataConn dc = new dataConn(sql);
                        DataTable dt = dc.GetTable();
                        if (dt.Rows.Count <=0)
                        {
                            e.Result = "Fail,密码错误！";
                        }
                        else
                        {
                            sql = "update atpujhb_tmp set flag='Y' where jhmc='" + plancode[i] + "'";
                            dc.ExeSql(sql);
                            e.Result = "Password,解锁成功！";
                        }
                    }
                   
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

            string plancode = grid.GetRowValues(e.VisibleIndex, "PLAN_CODE") as string;
            string sql1 = "select nvl(online_qty,0) from data_plan where plan_code='" + plancode + "' and rownum=1";
            DataTable dt1 = dc.GetTable(sql1);
            string isonline = "0";
            if (dt1.Rows.Count > 0)
                isonline = dt1.Rows[0][0].ToString();

            switch (e.ButtonID)
            {
                case "Confirm":
                    if (e.CellType == GridViewTableCommandCellType.Filter) break;
                    
                    if (run_flag == "Y")
                        e.Enabled = false;
                    if (run_flag == "N")
                        e.Enabled = true;
                    else
                        e.Enabled = false;
                    break;

                case "Cancel":
                    //if (item_flag == "Y" && confirm_flag == "Y" && run_flag == "Y")
                    if (e.CellType == GridViewTableCommandCellType.Filter) break;
                    if (run_flag == "Y" && isonline=="0")
                        e.Enabled = true;
                    if (isonline != "0")
                        e.Enabled = false;
                    if (run_flag == "N")
                        e.Enabled = false;
                    break;
                case "Unlock":
                    string sql = "select nvl(flag,'Y') from atpujhb_tmp where jhmc='" + plancode + "'";
                    if(dc.GetValue(sql)=="Y" || dc.GetTable(sql).Rows.Count==0)
                        e.Visible= DevExpress.Utils.DefaultBoolean.False;
                    break;

            }
        }


        protected void ASPxGridView1_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.VisibleIndex < 0) return;

            string status = e.GetValue("RUN_FLAG").ToString(); 
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