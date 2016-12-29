using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rmes.Web.Base;
using System.Data;
using Rmes.Pub.Data;
using PetaPoco;
using Rmes.DA.Base;
using DevExpress.Web.ASPxClasses;
using Rmes.DA.Factory;
using Rmes.DA.Entity;
using System.Data.OleDb;
using System.IO;
using DevExpress.Web.ASPxGridView;
using DevExpress.Utils;
using DevExpress.Web.ASPxEditors;

/**
 * 2016-07-12
 * 唐海林
 * BOM转换提交改制返修
 * */

namespace Rmes.WebApp.Rmes.Ssd.ssd2100
{
    public partial class ssd2101 : BasePage
    {
        private dataConn dc = new dataConn();
        private PubCs thePubCs = new PubCs();

        private static string theCompanyCode, theUserId, theUserName;
        public string programcode;
        public Database db = DB.GetInstance();
        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();
            theUserName = theUserManager.getUserName();
            programcode = "ssd2101";
            if (!IsPostBack)
            {
                ASPxDateEdit1.Date = DateTime.Now;
                List<ProductLineEntity> user_plines = ProductLineFactory.GetByUserID(theUserManager.getUserId(), programcode);
                ASPxComboBoxPline.DataSource = user_plines;
                ASPxComboBoxPline.TextField = "PLINE_NAME";
                ASPxComboBoxPline.ValueField = "PLINE_CODE";
                ASPxComboBoxPline.DataBind();
                ASPxComboBoxPline.SelectedIndex = 0;

            }
            //string sql1 = "select rmes_id,plan_code,plan_so,plan_qty,pline_code from data_plan where pline_code='" + ASPxComboBoxPline.SelectedItem.Value.ToString() + "' and begin_date=to_date('" + ASPxDateEdit1.Date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') and  confirm_flag='Y' and bom_flag='N' and item_flag='N' and third_flag='N' and run_flag='N' order by plan_code";

            //ASPxListBoxLocation.DataSource = dc.GetTable(sql1);
            //ASPxListBoxLocation.DataBind();
            setCondition();
        }
        private void setCondition()
        {
            string sql = "select * from atpu_convert_bom where tjsj>sysdate-1 and gzdd in (select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + programcode + "' and company_code='" + theCompanyCode + "') order by tjsj desc ";
            DataTable dt = dc.GetTable(sql);
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
        }
        protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            setCondition();
        }
        protected void listBoxLSH1_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] param = e.Parameter.Split(',');
            //string pline1 = param[0];
            string pline1 = ASPxComboBoxPline.SelectedItem.Value.ToString();
            //string begindate1 = param[1] ;
            string begindate1 = ASPxDateEdit1.Date.ToString("yyyy-MM-dd");
            string sql = "select rmes_id,plan_code,plan_so,plan_qty,pline_code from data_plan where pline_code='" + pline1 + "' and begin_date=to_date('" + begindate1 + "','yyyy-mm-dd') and (plan_type='C' or plan_type='D') and confirm_flag='Y' and bom_flag='N' and item_flag='N' and third_flag='N' and run_flag='N' order by plan_code";
            ASPxListBox location = sender as ASPxListBox;
            DataTable dt = dc.GetTable(sql);

            location.DataSource = dt;
            location.DataBind();
        }

        protected void ASPxListBox1_Callback(object sender, CallbackEventArgsBase e)
        {
            string[] param = e.Parameter.Split(',');
            string type = param[0];
            string pline1 = ASPxComboBoxPline.SelectedItem.Value.ToString();
            //string pline1 = param[1];
            //string begindate1 = param[1] ;
            string begindate1 = ASPxDateEdit1.Date.ToString("yyyy-MM-dd");
            ASPxListBox location = sender as ASPxListBox;
            if (type == "ALL")
            {
                //大线和改制返修如何区分，根据计划制定人员？
                string sql = "select plan_code||'; '||plan_so||'; '||plan_qty||'; '||pline_code ID1 from data_plan where pline_code='" + pline1 + "' and (plan_type='C' or plan_type='D') and begin_date=to_date('" + begindate1 + "','yyyy-mm-dd') and  confirm_flag='Y' and bom_flag='N' and item_flag='N' and third_flag='N' and run_flag='N' order by plan_code";

                DataTable dt = dc.GetTable(sql);
                location.DataSource = dt;
                location.DataBind();
            }
            if (type == "ALLW")
            {
                string sql = "select plan_code||'; '||plan_so||'; '||plan_qty||'; '||pline_code ID1 from data_plan where pline_code='" + pline1 + "' and (plan_type='C' or plan_type='D') and confirm_flag='Y' and bom_flag='N' and item_flag='N' and third_flag='N' and run_flag='N' order by plan_code";

                DataTable dt = dc.GetTable(sql);
                location.DataSource = dt;
                location.DataBind();
            }
            string sql1 = "select rmes_id,plan_code,plan_so,plan_qty,pline_code from data_plan where pline_code='" + pline1 + "' and (plan_type='C' or plan_type='D') and begin_date=to_date('" + begindate1 + "','yyyy-mm-dd') and  confirm_flag='Y' and bom_flag='N' and item_flag='N' and third_flag='N' and run_flag='N' order by plan_code";

            ASPxListBoxLocation.DataSource = dc.GetTable(sql1);
            ASPxListBoxLocation.DataBind();

        }

        protected void ASPxCallbackPanel5_Callback(object sender, CallbackEventArgsBase e)
        {
            string[] s = e.Parameter.Split(',');
            string flag = s[0];
            string value = s[1];

            switch (flag)
            {
                case "ADD":
                    if (string.IsNullOrEmpty(value)) return;
                    string value3 = s[2];
                    if (value3.EndsWith("|"))
                    {
                        value3 = value3.Substring(0, value3.Length - 1);
                    }
                    string[] _ids = value3.Split('|');
                    DataTable dt1 = new DataTable();
                    DataColumn dc1 = new DataColumn("ID1");
                    dt1.Columns.Add(dc1);
                    for (int i = 0; i < _ids.Length; i++)
                    {
                        if (_ids[i] != "")
                            dt1.Rows.Add(_ids[i]);
                    }

                    string sql12 = "select count(1) from data_plan where plan_code ='" + value + "' and (plan_type='C' or plan_type='D') ";
                    dc.setTheSql(sql12);
                    if (dc.GetValue() == "0")
                    {
                        //提示重复
                        ASPxListBox1.DataSource = dt1;
                        ASPxListBox1.DataBind();
                        ASPxCallbackPanel5.JSProperties.Add("cpCallbackName", "Fail");
                        ASPxCallbackPanel5.JSProperties.Add("cpCallbackRet", "计划号非法！");
                        break;
                    }
                    string sql = "select plan_code,plan_so,plan_qty,pline_code,item_flag from data_plan where plan_code='" + value.ToUpper() + "' and (plan_type='C' or plan_type='D') ";
                    dc.setTheSql(sql);
                    DataTable dt = dc.GetTable();
                    string plancode1 = dt.Rows[0][0].ToString();
                    string planso1 = dt.Rows[0][1].ToString();
                    string planqty1 = dt.Rows[0][2].ToString();
                    string plinecode1 = dt.Rows[0][3].ToString();
                    string itemflag1 = dt.Rows[0][4].ToString();
                    if (itemflag1 != "N")
                    {
                        ASPxListBox1.DataSource = dt1;
                        ASPxListBox1.DataBind();
                        ASPxCallbackPanel5.JSProperties.Add("cpCallbackName", "Fail");
                        ASPxCallbackPanel5.JSProperties.Add("cpCallbackRet", "计划已库房接收或确认！");
                        break;
                    }
                    string txt1 = plancode1 + "; " + planso1 + "; " + planqty1 + "; " + plinecode1;



                    for (int i = 0; i < _ids.Length; i++)
                    {
                        if (txt1 == _ids[i])
                        {
                            ASPxListBox1.DataSource = dt1;
                            ASPxListBox1.DataBind();
                            return;
                        }
                    }
                    //dt1.Columns.Add("ID1",typeof(string));
                    //DataRow dd = dt1.NewRow();
                    //dd["ID1"] = txt1;
                    dt1.Rows.Add(txt1);
                    ASPxListBox1.DataSource = dt1;
                    ASPxListBox1.DataBind();
                    //ASPxListBox1.Items.Add(txt1,dd);
                    break;
                default:
                    ASPxCallbackPanel5.JSProperties.Clear();
                    break;
            }
        }
        protected void ASPxCbSubmit_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
        {
            try
            {
                string[] collection = e.Parameter.Split('@');
                int cnt = collection.Length;
                if (collection[0] == "Commit")
                {
                    //自动分配流水号
                    //获取计划，判断计划类型
                    string value = collection[1];
                    if (value.EndsWith("|"))
                    {
                        value = value.Substring(0, value.Length - 1);
                    }
                    string[] _ids = value.Split('|');
                    for (int k = 0; k < _ids.Length; k++)
                    {
                        //获取每条记录的计划信息
                        string[] _ids1 = _ids[k].Split(';');
                        string plancode1 = _ids1[0].Trim().ToUpper();
                        string planso1 = _ids1[1].Trim().ToUpper();
                        string planqty1 = _ids1[2].Trim().ToUpper();
                        string plinecode1 = _ids1[3].Trim().ToUpper();
                        //判断该计划是否存在未转换记录
                        string sql = "select count(1) from atpu_convert_bom where jhdm='" + plancode1 + "' and ifzh='N' ";
                        dc.setTheSql(sql);
                        if (Convert.ToInt32(dc.GetValue()) > 0)
                        {
                            continue;
                        }
                        sql = "insert into atpu_convert_bom(rmes_id,company_code,jhdm,jhso,gzdd,yhmc,tjsj,IFZH,IFZC)values(seq_rmes_id.nextval,'" + theCompanyCode + "','" + plancode1 + "','" + planso1 + "','" + plinecode1 + "','" + theUserName + "',sysdate,'N','N') ";
                        dc.ExeSql(sql);
                    }
                    e.Result = "OK,BOM转换提交成功！";
                    return;
                }
                //else if (collection[0] == "ADD")
                //{
                //    string value = collection[1];
                //    if (string.IsNullOrEmpty(value))
                //    {
                //        e.Result = "Fail,计划为空！";
                //        return;
                //    }
                //    string sql12 = "select count(1) from data_plan where plan_code ='" + value + "' ";
                //    dc.setTheSql(sql12);
                //    if (dc.GetValue() == "0")
                //    {
                //        //提示计划不存在
                //        e.Result = "Fail,计划号不存在！";
                //        return;
                //    }
                //    string sql = "select plan_code,plan_so,plan_qty,pline_code from data_plan where plan_code='" + value.ToUpper() + "'";
                //    dc.setTheSql(sql);
                //    DataTable dt = dc.GetTable();
                //    string plancode1 = dt.Rows[0][0].ToString();
                //    string planso1 = dt.Rows[0][1].ToString();
                //    string planqty1 = dt.Rows[0][2].ToString();
                //    string plinecode1 = dt.Rows[0][3].ToString();
                //    string txt1 = plancode1 + "; " + planso1 + "; " + planqty1 + "; " + plinecode1;
                //    for (int i = 0; i < ASPxListBox1.Items.Count; i++)
                //    {
                //        if (txt1 == ASPxListBox1.Items[i].Text)
                //        {
                //            break;
                //        }
                //    }
                //    ASPxListBox1.Items.Add(txt1, txt1);
                //    e.Result = "OK,计划提交成功！";
                //    return;
                //}

            }
            catch (Exception e1)
            {
                e.Result = "Fail,BOM转换提交失败" + e1.Message + "！";
                return;
            }
        }
        protected void btnXlsExport_Click2(object sender, EventArgs e)
        {
            ASPxGridViewExporter2.WriteXlsToResponse("FAIL_BOM");
        }
        protected void ASPxGridView2_DataSelect(object sender, EventArgs e)
        {
            ASPxGridView grid = (ASPxGridView)sender;
            string planCode = grid.GetMasterRowFieldValues("JHDM").ToString();
            string plineCode = grid.GetMasterRowFieldValues("GZDD").ToString();

            string sql = "SELECT * FROM vw_data_plan_standard_bom_log WHERE PLAN_CODE = '" + planCode + "' AND PLINE_CODE = '" + plineCode + "' "
                + " and (process_code is null or item_code is null or item_name is null or  process_code is null or station_code is null) ORDER BY location_code,process_code,item_code  ";

            DataTable planBom = dc.GetTable(sql);
            grid.DataSource = planBom;
        }
    }
}