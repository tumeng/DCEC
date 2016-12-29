using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DevExpress.Web.ASPxGridLookup;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxEditors;

using Rmes.DA.Base;
using Rmes.DA.Procedures;
using Rmes.Pub.Data;
using Rmes.Web.Base;

namespace Rmes.WebApp.Rmes.Qms.qms2300
{
    public partial class qms2301bak : System.Web.UI.Page
    {
        private dataConn dc = new dataConn();
        private PubCs pc = new PubCs();
        public string theCompanyCode, theProgramCode, theUserId;

        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theProgramCode = "qms2300";
            theUserId = theUserManager.getUserId();

            string sql = "select a.pline_code,b.rmes_id,b.pline_name from VW_USER_ROLE_PROGRAM a "
                   + "left join code_product_line b on a.pline_code=b.pline_code "
                   + "where a.COMPANY_CODE = '" + theCompanyCode + "' and a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' order by b.PLINE_NAME";
            comboPlineCode.DataSource = dc.GetTable(sql);
            comboPlineCode.DataBind();
            if (!IsPostBack)
            {
                string sqlD = "DELETE from REL_STATION_DETECT_TEMP";
                dc.ExeSql(sqlD);
            }
        }
        private void setCondition()
        {
            //初始化GRIDVIEW
            string sql = "SELECT A.RMES_ID,A.COMPANY_CODE,A.PLINE_CODE,D.PLINE_NAME,a.PRODUCT_SERIES,"
                + "A.STATION_CODE,C.STATION_NAME,C.STATION_CODE AS STATION_CODE1,"
                + "A.DETECT_CODE,A.DETECT_NAME,A.DETECT_TYPE,A.DETECT_STANDARD,A.DETECT_MAX,A.DETECT_MIN,A.DETECT_UNIT,A.DETECT_SEQ "
                + "FROM REL_STATION_DETECT_TEMP A "
                + " LEFT JOIN CODE_STATION C ON A.STATION_CODE =C.RMES_ID "
                + " LEFT JOIN CODE_PRODUCT_LINE D ON A.PLINE_CODE =D.RMES_ID "
                + " WHERE A.COMPANY_CODE = '" + theCompanyCode + "'"
                + "and a.pline_code in(select w.rmes_id from VW_USER_ROLE_PROGRAM t left join code_product_line w on t.pline_code=w.pline_code "
                + "where t.user_id='" + theUserId + "' and t.program_code='" + theProgramCode + "')";
            DataTable dt = dc.GetTable(sql);

            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
        }
        protected void comboStationCode_Callback(object sender, CallbackEventArgsBase e)
        {
            string pline = e.Parameter;

            string sql = "select t.rmes_id,t.STATION_NAME from CODE_STATION t left join code_product_line a on t.pline_code=a.pline_code where t.pline_code='" + pline + "' order by t.STATION_NAME";
            DataTable dt = dc.GetTable(sql);

            ASPxComboBox station = sender as ASPxComboBox;
            station.DataSource = dt;
            station.ValueField = "RMES_ID";
            station.TextField = "STATION_NAME";
            station.DataBind();
        }

        protected void comboPSeries_Callback(object sender, CallbackEventArgsBase e)
        {
            string pline = e.Parameter;

            string sql = "select t.RMES_ID,t.ROUNTING_REMARK from DATA_ROUNTING_REMARK t left join code_product_line a on t.pline_code=a.pline_code where a.rmes_id='" + pline + "' order by t.ROUNTING_REMARK";
            DataTable dt = dc.GetTable(sql);

            ASPxComboBox pSeries = sender as ASPxComboBox;
            pSeries.DataSource = dt;
            pSeries.ValueField = "RMES_ID";
            pSeries.TextField = "ROUNTING_REMARK";
            pSeries.DataBind();
        }

        protected void initDetect(string plineCode)
        {
            string sql = "SELECT A.RMES_ID,A.DETECT_CODE,A.DETECT_NAME  FROM CODE_DETECT A WHERE A.PLINE_CODE='"
                        + plineCode + "' AND A.DETECT_CODE NOT IN(SELECT C.DETECT_CODE FROM REL_STATION_DETECT C WHERE C.DETECT_CODE='" + plineCode + "' )  ORDER BY A.DETECT_CODE";//
            DataTable dt = dc.GetTable(sql);

            ASPxListBox1.DataSource = dt;
            ASPxListBox1.DataBind();
        }

        protected void lbAvailable_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string pline = e.Parameter;
            ASPxListBox detectData = sender as ASPxListBox;

            initDetect(pline);

        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            int count = 0;
            string pline, station, pSeries, detectCode;

            pline = comboPlineCode.SelectedItem.Value.ToString();
            station = comboStationCode.SelectedItem.Value.ToString();
            pSeries = comboPSeries.SelectedItem.Text.ToString();

            for (count = 0; count < ASPxListBox1.SelectedItems.Count; count++)
            {
                detectCode = ASPxListBox1.SelectedItems[count].ToString();
                string[] sArray = detectCode.Split(';');

                string detectName = "";
                string detectType = "";
                string detectStandard = "";
                string detectMax = "";
                string detectMin = "";
                string detectUnit = "";
                string chSql = "select detect_name,detect_type,detect_standard,detect_max,detect_min,detect_unit from code_detect where pline_code='" + pline + "' and detect_code='" + sArray[0] + "'";
                dataConn chDc = new dataConn(chSql);
                DataTable chDt = chDc.GetTable();
                if (chDt.Rows.Count > 0)
                {
                    detectName = chDt.Rows[0]["detect_name"].ToString();
                    detectType = chDt.Rows[0]["detect_type"].ToString();
                    detectStandard = chDt.Rows[0]["detect_standard"].ToString();
                    detectMax = chDt.Rows[0]["detect_max"].ToString();
                    detectMin = chDt.Rows[0]["detect_min"].ToString();
                    detectUnit = chDt.Rows[0]["detect_unit"].ToString();
                }

                string sql = "insert into REL_STATION_DETECT_TEMP(rmes_id,company_code,pline_code,station_code,product_series,detect_code,detect_name,detect_type,detect_standard,"
                           + "detect_max,detect_min,detect_unit)"
                           + "values(SEQ_RMES_ID.NEXTVAL,'" + theCompanyCode + "','" + pline + "','" + station + "','" + pSeries + "','" + sArray[0] + "','" + detectName + "',"
                           + "'" + detectType + "','" + detectStandard + "','" + detectMax + "','" + detectMin + "','" + detectUnit + "')";
                dc.ExeSql(sql);
            }

            setCondition();

            string sql1 = "SELECT A.RMES_ID,A.DETECT_CODE,A.DETECT_NAME FROM CODE_DETECT A WHERE A.PLINE_CODE='"
                    + pline + "' AND A.DETECT_CODE NOT IN (SELECT DETECT_CODE FROM REL_STATION_DETECT_TEMP C WHERE C.PLINE_CODE='" + pline + "' )"
                    +" AND A.DETECT_CODE NOT IN(SELECT C.DETECT_CODE FROM REL_STATION_DETECT C WHERE C.DETECT_CODE='" + pline + "' ) "
                    +"  ORDER BY A.DETECT_CODE";
            DataTable dt = dc.GetTable(sql1);

            ASPxListBox1.DataSource = dt;
            ASPxListBox1.DataBind();

            //Response.Write("<script type='text/javascript'>alert('选择成功！');window.opener.location.reload();location.href='qms2301.aspx';</script>");

            //comboPlineCode.Text = "";
            //comboStationCode.Text = "";
            //comboPSeries.Text = "";
            //ASPxListBox1.Items.Clear();
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            string pline = comboPlineCode.SelectedItem.Value.ToString();
            if (ASPxGridView1.Selection.Count == 0)
            {
                //ASPxCallbackPanel5.JSProperties.Add("cpCallbackName", "Fail");
                //ASPxCallbackPanel5.JSProperties.Add("cpCallbackRet", "请选择计划！");
                //return;
            }
            string detectCode; ;
            for (int count = 0; count < ASPxGridView1.Selection.Count; count++)
            {
                detectCode = ASPxGridView1.GetSelectedFieldValues("DETECT_CODE")[count].ToString();

                string delSql = "delete from REL_STATION_DETECT_TEMP where detect_code='" + detectCode + "'";
                dc.ExeSql(delSql);
            }

            setCondition();

            string sql1 = "SELECT A.RMES_ID,A.DETECT_CODE,A.DETECT_NAME FROM CODE_DETECT A WHERE A.PLINE_CODE='"
                    + pline + "' AND A.DETECT_CODE NOT IN (SELECT DETECT_CODE FROM REL_STATION_DETECT_TEMP C WHERE C.PLINE_CODE='" + pline + "' )"
                    + " AND A.DETECT_CODE NOT IN(SELECT C.DETECT_CODE FROM REL_STATION_DETECT C WHERE C.DETECT_CODE='" + pline + "' ) "
                    +"  ORDER BY A.DETECT_CODE";
            DataTable dt = dc.GetTable(sql1);

            ASPxListBox1.DataSource = dt;
            ASPxListBox1.DataBind();
        }

        protected void butConfirm_Click(object sender, EventArgs e)
        {
            int count = 0;

            //string pline, station, pSeries, detectCode;

            //pline = comboPlineCode.SelectedItem.Value.ToString();
            //station = comboStationCode.SelectedItem.Value.ToString();
            //pSeries = comboPSeries.SelectedItem.Text.ToString();

            //for (count = 0; count < listChosedDetect.Items.Count; count++)
            //{
            //    detectCode = listChosedDetect.Items[count].ToString();
            //    string[] sArray = detectCode.Split(';');

            //    string detectName = "";
            //    string detectType = "";
            //    string detectStandard = "";
            //    string detectMax = "";
            //    string detectMin = "";
            //    string detectUnit = "";
            //    string chSql = "select detect_name,detect_type,detect_standard,detect_max,detect_min,detect_unit from code_detect where pline_code='" + pline + "' and detect_code='" + sArray[0] + "'";
            //    dataConn chDc = new dataConn(chSql);
            //    DataTable chDt = chDc.GetTable();
            //    if (chDt.Rows.Count > 0)
            //    {
            //        detectName = chDt.Rows[0]["detect_name"].ToString();
            //        detectType = chDt.Rows[0]["detect_type"].ToString();
            //        detectStandard = chDt.Rows[0]["detect_standard"].ToString();
            //        detectMax = chDt.Rows[0]["detect_max"].ToString();
            //        detectMin = chDt.Rows[0]["detect_min"].ToString();
            //        detectUnit = chDt.Rows[0]["detect_unit"].ToString();
            //    }

            //    string sql = "insert into REL_STATION_DETECT(rmes_id,company_code,pline_code,station_code,product_series,detect_code,detect_name,detect_type,detect_standard,"
            //               + "detect_max,detect_min,detect_unit)"
            //               + "values(SEQ_RMES_ID.NEXTVAL,'" + theCompanyCode + "','" + pline + "','" + station + "','" + pSeries + "','" + sArray[0] + "','" + detectName + "',"
            //               + "'" + detectType + "','" + detectStandard + "','" + detectMax + "','" + detectMin + "','" + detectUnit + "')";
            //    dc.ExeSql(sql);
            //}

            //Response.Write("<script type='text/javascript'>alert('新增站点检测数据关系成功！');window.opener.location.reload();location.href='qms2301.aspx';</script>");

            //comboPlineCode.Text = "";
            //comboStationCode.Text = "";
            //comboPSeries.Text = "";
            //listChosedDetect.Items.Clear();

        }

        protected void butCloseWindow_Click(object sender, EventArgs e)
        {
            Response.Write("<script type='text/javascript'>window.opener.location.reload();window.close();</script>");
        }

        private void btnUP_Click(object sender, EventArgs e)
        {
            //int index = listChosedDetect.SelectedIndex;
            //if (index == -1) return;
            //if (index == 0) return;
            //string[] strs = listChosedDetect.Items[index].ToString().Split('\t');
            //string[] strs2 = listChosedDetect.Items[index - 1].ToString().Split('\t');
            //string temp = string.Empty;
            //temp = strs[1];
            //strs[1] = strs2[1];
            //strs2[1] = temp;
            ////listChosedDetect.Items[index] = strs[0] + "\t" + strs[1];
            ////listChosedDetect.Items[index - 1] = strs2[0] + "\t" + strs2[1];
            //listChosedDetect.SelectedIndex = index - 1;


            //int i = this.listChosedDetect.SelectedIndex;
            //if (i > 0)
            //{
            //    string aa = listChosedDetect.SelectedItem.ToString();
            //    string uptest = this.listChosedDetect.Items[i - 1].ToString();
            //    //把当前选择行的值与上一行互换 并将选择索引减1
            //    listChosedDetect.Items[i - 1] = aa;      
            //    listChosedDetect.Items[i] = uptest; 
            //    listChosedDetect.SelectedIndex = i - 1;
            //}
            //else
            //{
            //     //button2.Enabled = false;
            //}
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            //int i = this.listChosedDetect.SelectedIndex;
            //if (i < listChosedDetect.Items.Count - 1)
            //{
            //    string aa = listChosedDetect.SelectedItem.ToString();
            //    string uptest = this.listChosedDetect.Items[i + 1].ToString();
            //    //把当前选择行的值与下一行互换 并将选择索引加1
            //    //listChosedDetect.Items[i + 1] = aa;     
            //    //listChosedDetect.Items[i] = uptest;
            //    listChosedDetect.SelectedIndex = i + 1;
            //}
        }

        protected void ASPxButton5_Command(object sender, CommandEventArgs e)
        {
            //case "ToUp":
            //            // 上移
            //            if (this.listChosedDetect.SelectedIndices.Count > 0 &&
            //                this.listChosedDetect.SelectedIndices[0] > 0)
            //            {
            //                int[] newIndices =
            //                    this.listChosedDetect.SelectedIndices.Cast<int>()
            //                    .Select(index => index - 1).ToArray();

            //                this.listChosedDetect.SelectedItems.Clear();

            //                for (int i = 0; i < newIndices.Length; i++)
            //                {
            //                    object obj = this.listChosedDetect.Items[newIndices[i]];
            //                    this.listChosedDetect.Items[newIndices[i]] = this.listChosedDetect.Items[newIndices[i] + 1];
            //                    this.listChosedDetect.Items[newIndices[i] + 1] = obj;
            //                    this.listChosedDetect.SelectedItems.Add(this.listChosedDetect.Items[newIndices[i]]);
            //                }
            //            }

            //            break;
        }
    }
}