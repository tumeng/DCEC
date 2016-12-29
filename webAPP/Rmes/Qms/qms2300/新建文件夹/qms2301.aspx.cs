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
using DevExpress.Web.ASPxGridView;
using DevExpress.Utils;
using DevExpress.Web.ASPxEditors;

namespace Rmes.WebApp.Rmes.Qms.qms2300
{
    public partial class qms2301 : System.Web.UI.Page
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
        }
        private void setCondition()
        {
            //初始化GRIDVIEW
            string sql = "SELECT A.RMES_ID,A.COMPANY_CODE,A.PLINE_CODE,D.PLINE_NAME,a.PRODUCT_SERIES,"
                       + "A.STATION_CODE,C.STATION_NAME,C.STATION_CODE AS STATION_CODE1,"
                       + "A.DETECT_CODE,A.DETECT_NAME,A.DETECT_TYPE,A.DETECT_STANDARD,A.DETECT_MAX,A.DETECT_MIN,A.DETECT_UNIT,A.DETECT_SEQ "
                       + "FROM REL_STATION_DETECT A "
                       + " LEFT JOIN CODE_STATION C ON A.STATION_CODE =C.RMES_ID "
                       + " LEFT JOIN CODE_PRODUCT_LINE D ON A.PLINE_CODE =D.RMES_ID "
                       + " WHERE A.COMPANY_CODE = '" + theCompanyCode + "' and a.pline_code ='" + comboPlineCode.Value.ToString() + "'"
                       + " and a.PRODUCT_SERIES='" + comboPSeries.SelectedItem.Text.ToString() + "' and a.STATION_CODE='" + comboStationCode.Value.ToString() +"'"
                       + " order by A.DETECT_SEQ";
            DataTable dt = dc.GetTable(sql);

            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
        }
        protected void comboStationCode_Callback(object sender, CallbackEventArgsBase e)
        {
            string pline = e.Parameter;

            string sql = "select '' rmes_id,'' station_name from dual union select t.rmes_id,t.STATION_NAME from CODE_STATION t left join code_product_line a on t.pline_code=a.pline_code where t.pline_code='" + pline + "' order by STATION_NAME desc";
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

            string sql = "select '' rmes_id,'' ROUNTING_REMARK from dual union select t.RMES_ID,t.ROUNTING_REMARK from DATA_ROUNTING_REMARK t left join code_product_line a on t.pline_code=a.pline_code where a.rmes_id='" + pline + "' order by ROUNTING_REMARK desc";
            DataTable dt = dc.GetTable(sql);

            ASPxComboBox pSeries = sender as ASPxComboBox;
            pSeries.DataSource = dt;
            pSeries.ValueField = "RMES_ID";
            pSeries.TextField = "ROUNTING_REMARK";
            pSeries.DataBind();
        }

        protected void lbAvailable_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string pline = e.Parameter;
            ASPxListBox detectData = sender as ASPxListBox;

            string sql = "SELECT A.RMES_ID,A.DETECT_CODE,A.DETECT_CODE||';'||A.DETECT_NAME DETECT_NAME FROM CODE_DETECT A WHERE A.PLINE_CODE='"
                    + pline + "' AND NOT EXISTS(SELECT * FROM REL_STATION_DETECT C WHERE C.DETECT_CODE=A.DETECT_CODE )  ORDER BY A.DETECT_CODE";//
            DataTable dt = dc.GetTable(sql);

            detectData.DataSource = dt;
            detectData.DataBind();

        }
        
        
        protected void ASPxCbSubmit_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
        {
            int count = 0;

            string pline, station, pSeries, detectCode;
            //e.Result = "fail,";
            char[] charSeparators = new char[] { ',' };
            string[] collection = e.Parameter.Split(charSeparators);
            int cnt = collection.Length;

            pline = collection[0].ToString();
            station = collection[1].ToString();
            pSeries = collection[cnt - 1].ToString();

            for (count = 0; count < listChosedDetect.Items.Count; count++)
            {
                detectCode = listChosedDetect.Items[count].ToString();
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
                //顺序号start
                string detectCode1 = "";
                dataConn dc8 = new dataConn("select DETECT_SEQ from REL_STATION_DETECT where pline_code ='" + pline + "' and PRODUCT_SERIES ='" + pSeries + "' and STATION_CODE ='" + station + "'");
                if (dc8.GetState() == true)
                {
                    string cSql = "select max(DETECT_SEQ) DETECT_SEQ_MAX from REL_STATION_DETECT where pline_code ='" + pline + "' and PRODUCT_SERIES ='" + pSeries + "' and STATION_CODE ='" + station + "'";
                    dataConn dc1 = new dataConn(cSql);
                    DataTable dt = dc1.GetTable();
                    string aa = dt.Rows[0]["DETECT_SEQ_MAX"].ToString();
                    detectCode1 = Convert.ToString(Convert.ToInt32(aa) + 1);
                }
                else
                {
                    detectCode1 = "1";
                }
                //顺序号end

                string sql = "insert into REL_STATION_DETECT(rmes_id,company_code,pline_code,station_code,product_series,detect_code,detect_name,detect_type,detect_standard,"
                           + "detect_max,detect_min,detect_unit,DETECT_SEQ)"
                           + "values(SEQ_RMES_ID.NEXTVAL,'" + theCompanyCode + "','" + pline + "','" + station + "','" + pSeries + "','" + sArray[0] + "','" + detectName + "',"
                           + "'" + detectType + "','" + detectStandard + "','" + detectMax + "','" + detectMin + "','" + detectUnit + "','" + detectCode1 + "')";
                dc.ExeSql(sql);
            }
            
        }
        protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] param = e.Parameters.Split(',');
            string sss1 = param[0];//生产线
            string sss2 = param[1];//站点
            string sss3 = param[2];//系列
            //初始化GRIDVIEW
            string sql = "SELECT A.RMES_ID,A.COMPANY_CODE,A.PLINE_CODE,D.PLINE_NAME,a.PRODUCT_SERIES,"
                       + "A.STATION_CODE,C.STATION_NAME,C.STATION_CODE AS STATION_CODE1,"
                       + "A.DETECT_CODE,A.DETECT_NAME,A.DETECT_TYPE,A.DETECT_STANDARD,A.DETECT_MAX,A.DETECT_MIN,A.DETECT_UNIT,A.DETECT_SEQ "
                       + "FROM REL_STATION_DETECT A "
                       + " LEFT JOIN CODE_STATION C ON A.STATION_CODE =C.RMES_ID "
                       + " LEFT JOIN CODE_PRODUCT_LINE D ON A.PLINE_CODE =D.RMES_ID "
                       + " WHERE A.COMPANY_CODE = '" + theCompanyCode + "' and a.pline_code ='" + sss1 + "'"
                       + " and a.PRODUCT_SERIES='" + sss3 + "' and a.STATION_CODE='" + sss2 + "'"
                       + " order by A.DETECT_SEQ";
            DataTable dt = dc.GetTable(sql);
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();

        }
        
        //检测数据调序
        public void ASPxGridView1_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
        {
            string s = e.Parameters;
            string[] s1 = s.Split('|');
            string type1 = s1[0];
            int rowIndex = int.Parse(s1[1]);
            string sql = "";
            string rmesid, detectseq, plineCode,stationCode,productSeries;

            try
            {
                rmesid = ASPxGridView1.GetRowValues(rowIndex, "RMES_ID").ToString();
                detectseq = ASPxGridView1.GetRowValues(rowIndex, "DETECT_SEQ").ToString();
                plineCode = ASPxGridView1.GetRowValues(rowIndex, "PLINE_CODE") as string;
                stationCode = ASPxGridView1.GetRowValues(rowIndex, "STATION_CODE") as string;
                productSeries = ASPxGridView1.GetRowValues(rowIndex, "PRODUCT_SERIES") as string;
            }
            catch
            {
                e.Result = "Fail,缺少关键值！";
                return;
            }
            switch (type1)
            {
                case "Up":
                    sql = "select max(detect_seq) from REL_STATION_DETECT where detect_seq<'" + detectseq + "'"
                        + " and PLINE_CODE='" + plineCode + "' and STATION_CODE='" + stationCode + "' and PRODUCT_SERIES='"+productSeries+"' ";
                    dc.setTheSql(sql);
                    string detectseq1 = dc.GetTable().Rows[0][0].ToString();
                    if (dc.GetTable().Rows.Count == 0 || detectseq1 == "")
                    {
                        e.Result = "Fail,当前已最小序！";
                        break;
                    }

                    sql = "update REL_STATION_DETECT set detect_seq='" + detectseq + "' where detect_seq='" + detectseq1 + "'  ";
                    dc.ExeSql(sql);
                    sql = "update REL_STATION_DETECT set detect_seq='" + detectseq1 + "' where rmes_id='" + rmesid + "' ";
                    dc.ExeSql(sql);
                    //e.Result = "OK,上调成功！";
                    break;
                case "Down":
                    sql = "select min(detect_seq) from REL_STATION_DETECT where detect_seq>'" + detectseq + "' "
                        + " and PLINE_CODE='" + plineCode + "' and STATION_CODE='" + stationCode + "' and PRODUCT_SERIES='"+productSeries+"' ";
                    dc.setTheSql(sql);
                    detectseq1 = dc.GetTable().Rows[0][0].ToString();
                    if (dc.GetTable().Rows.Count == 0 || detectseq1 == "")
                    {
                        e.Result = "Fail,当前已最大序！";
                        break;
                    }
                    sql = "update REL_STATION_DETECT set detect_seq='" + detectseq + "' where detect_seq='" + detectseq1 + "' ";
                    dc.ExeSql(sql);
                    sql = "update REL_STATION_DETECT set detect_seq='" + detectseq1 + "' where rmes_id='" + rmesid + "' ";
                    dc.ExeSql(sql);
                    //e.Result = "OK,下调成功！";
                    break;
                
                default:

                    break;
            }
        }
        protected void ASPxButton6_Click(object sender, EventArgs e)
        {
            Response.Write("<script type='text/javascript'>window.opener.location.reload();window.close();</script>");
        }

    }
}