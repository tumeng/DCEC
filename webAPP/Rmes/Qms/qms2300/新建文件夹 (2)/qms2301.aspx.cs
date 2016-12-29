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
using System.Net;


namespace Rmes.WebApp.Rmes.Qms.qms2300
{
    public partial class qms2301 : BasePage
    {
        private dataConn dc = new dataConn();
        private PubCs pc = new PubCs();
        public string theCompanyCode, theProgramCode, theUserId, GV_MachineName, theUserCode;

        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theProgramCode = "qms2300";
            theUserId = theUserManager.getUserId();
            //GV_MachineName = System.Net.Dns.GetHostName();
            string hostIPAddress = Page.Request.UserHostAddress;
            //IPHostEntry hostInfo = System.Net.Dns.GetHostByAddress(hostIPAddress);
            GV_MachineName = hostIPAddress;

            theUserCode = theUserManager.getUserCode();

            string sql = "select a.pline_code,b.rmes_id,b.pline_name from VW_USER_ROLE_PROGRAM a "
                   + "left join code_product_line b on a.pline_code=b.pline_code "
                   + "where a.COMPANY_CODE = '" + theCompanyCode + "' and a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' order by b.PLINE_NAME";
            comboPlineCode.DataSource = dc.GetTable(sql);
            comboPlineCode.DataBind();

            setCondition();

            if (!IsPostBack)
            {
                string sqlD = "DELETE from rel_station_detect_temp";
                dc.ExeSql(sqlD);
            }
        }
        private void setCondition()
        {
            if (comboPlineCode.Value != null && comboStationCode.Value != null && comboPSeries.Value!=null)
            {
                string sql = "SELECT A.RMES_ID,A.DETECT_CODE,A.DETECT_NAME FROM CODE_DETECT A WHERE A.PLINE_CODE='" + comboPlineCode.Value.ToString() + "'"
                       + " AND NOT EXISTS(SELECT * FROM REL_STATION_DETECT C WHERE C.DETECT_CODE=A.DETECT_CODE and c.pline_code='" + comboPlineCode.Value.ToString() + "' "
                       + "and c.station_code='" + comboStationCode.Value.ToString() + "' and c.product_series='" + comboPSeries.Value.ToString() + "' ) "
                       + " AND NOT EXISTS(SELECT * FROM REL_STATION_DETECT_temp d WHERE d.DETECT_CODE=A.DETECT_CODE and d.pline_code='" + comboPlineCode.Value.ToString() + "' "
                       + "and d.station_code='" + comboStationCode.Value.ToString() + "' and d.product_series='" + comboPSeries.Value.ToString() + "' ) "
                       + " ORDER BY  A.DETECT_CODE";

                DataTable dt = dc.GetTable(sql);

                ASPxGridView3.DataSource = dt;
                ASPxGridView3.DataBind();
            }
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
        protected void ASPxGridView3_CustomCallback1(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] param = e.Parameters.Split(',');
            string sss1 = param[0];//生产线
            string sss2 = param[1];//站点
            string sss3 = param[2];//产品系列

            string sql = "SELECT A.RMES_ID,A.DETECT_CODE,A.DETECT_NAME FROM CODE_DETECT A WHERE A.PLINE_CODE='"+ sss1 + "'"
                       + " AND NOT EXISTS(SELECT * FROM REL_STATION_DETECT C WHERE C.DETECT_CODE=A.DETECT_CODE and c.pline_code='" + sss1 + "' "
                       + "and c.station_code='" + sss2 + "' and c.product_series='"+sss3+"' ) "
                       + " AND NOT EXISTS(SELECT * FROM REL_STATION_DETECT_temp d WHERE d.DETECT_CODE=A.DETECT_CODE and d.pline_code='" + sss1 + "' "
                       + "and d.station_code='" + sss2 + "' and d.product_series='" + sss3 + "' ) "
                       +" ORDER BY A.DETECT_CODE";

            DataTable dt = dc.GetTable(sql);

            ASPxGridView gridview3 = sender as ASPxGridView;
            gridview3.DataSource = dt;
            gridview3.DataBind();
            gridview3.Selection.UnselectAll();
        }
        protected void ASPxGridView2_CustomCallback1(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] param = e.Parameters.Split(',');
            string sss1 = param[0];//生产线
            string sss2 = param[1];//站点
            string sss3 = param[2];//标识
            string sss4 = param[3];//产品系列

            string sql = "";
            if (sss3 == "CHUFA")
            {
                string inSqlTemp = "insert into REL_STATION_DETECT_TEMP(RMES_ID,PLINE_CODE,STATION_CODE,PRODUCT_SERIES,DETECT_CODE,DETECT_NAME,DETECT_TYPE,"
                                 + "DETECT_STANDARD,DETECT_MAX,DETECT_MIN,DETECT_UNIT,DETECT_SEQ,MACHINENAME,INSERT_FLAG) "
                                 + "select RMES_ID,PLINE_CODE,STATION_CODE,PRODUCT_SERIES,DETECT_CODE,DETECT_NAME,DETECT_TYPE,"
                                 + "DETECT_STANDARD,DETECT_MAX,DETECT_MIN,DETECT_UNIT,DETECT_SEQ,'" + GV_MachineName + "','1' from REL_STATION_DETECT "
                                 + "where PLINE_CODE='" + sss1 + "' and station_code='" + sss2 + "' and PRODUCT_SERIES='" + sss4 + "'";
                dc.ExeSql(inSqlTemp);

                sql = "SELECT A.RMES_ID,A.DETECT_CODE,A.DETECT_NAME FROM CODE_DETECT A WHERE A.PLINE_CODE='"
                    + sss1 + "' AND EXISTS(SELECT * FROM REL_STATION_DETECT_TEMP C WHERE C.DETECT_CODE=A.DETECT_CODE  "
                    + " and PLINE_CODE='" + sss1 + "' and station_code='" + sss2 + "' and PRODUCT_SERIES='" + sss4 + "' )  ORDER BY A.DETECT_CODE";

            }

            if (sss3 == "INIT")
            {
                sql = "SELECT A.RMES_ID,A.DETECT_CODE,A.DETECT_NAME FROM CODE_DETECT A  WHERE A.PLINE_CODE='" + sss1 + "' "
                    + " AND EXISTS(SELECT * FROM REL_STATION_DETECT_TEMP C WHERE C.DETECT_CODE=A.DETECT_CODE  "
                    + " and PLINE_CODE='" + sss1 + "' and station_code='" + sss2 + "' and PRODUCT_SERIES='" + sss4 + "'  and c.insert_flag<>'2') "
                    + " ORDER BY A.DETECT_CODE";

            }

            DataTable dt = dc.GetTable(sql);

            ASPxGridView gridview2 = sender as ASPxGridView;
            gridview2.DataSource = dt;
            gridview2.DataBind();

            gridview2.Selection.UnselectAll();
        }
        protected void ASPxCbSubmit1_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
        {
            int count = 0;

            string oFlag, pline, station, pSeries,detectCode;

            char[] charSeparators = new char[] { ',' };
            string[] collection = e.Parameter.Split(charSeparators);
            int cnt = collection.Length;

            oFlag = collection[0].ToString();
            pline = collection[1].ToString();
            station = collection[2].ToString();
            pSeries = collection[3].ToString();

            if (oFlag == "ADD")
            {
                //获取检测数据
                List<string> s = new List<string>();
                for (int i = 4; i < cnt; i++)
                {
                    s.Add(collection[i].ToString());
                }
                string[] s1 = s.ToArray();

                if (ASPxGridView3.Selection.Count == 0)
                {
                    e.Result = "Fail,请选择要增加的检测数据！";
                    return;
                }
                for (int i = 0; i < s1.Length; i++)
                {
                    detectCode = s1[i].ToString();

                    string detectName = "";
                    string detectType = "";
                    string detectStandard = "";
                    string detectMax = "";
                    string detectMin = "";
                    string detectUnit = "";
                    
                    string chSql = "select detect_name,detect_type,detect_standard,detect_max,detect_min,detect_unit from code_detect"
                                 + " where pline_code='" + pline + "' and detect_code='" + detectCode + "'";
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
                    string dSeq1 = "";
                    dataConn dc8 = new dataConn("select DETECT_SEQ from REL_STATION_DETECT_TEMP where pline_code ='" + pline + "' and PRODUCT_SERIES ='" + pSeries + "' and STATION_CODE ='" + station + "'");
                    if (dc8.GetState() == true)
                    {
                        string cSql = "select max(DETECT_SEQ) DETECT_SEQ_MAX from REL_STATION_DETECT_TEMP where pline_code ='" + pline + "' and PRODUCT_SERIES ='" + pSeries + "' and STATION_CODE ='" + station + "'";
                        dataConn dc1 = new dataConn(cSql);
                        DataTable dt = dc1.GetTable();
                        string aa = dt.Rows[0]["DETECT_SEQ_MAX"].ToString();
                        dSeq1 = Convert.ToString(Convert.ToInt32(aa) + 1);
                    }
                    else
                    {
                        dSeq1 = "1";
                    }
                    //顺序号end

                    string inSqlTemp = "insert into REL_STATION_DETECT_TEMP(RMES_ID,PLINE_CODE,STATION_CODE,PRODUCT_SERIES,DETECT_CODE,DETECT_NAME,DETECT_TYPE,"
                                     + "DETECT_STANDARD,DETECT_MAX,DETECT_MIN,DETECT_UNIT,DETECT_SEQ,MACHINENAME,INSERT_FLAG) "
                                     + "values(SEQ_RMES_ID.NEXTVAL,'" + pline + "','" + station + "','" + pSeries + "','" + detectCode + "','" + detectName + "','" + detectType + "',"
                                     + "'" + detectStandard + "','" + detectMax + "','" + detectMin + "','" + detectUnit + "','" + dSeq1 + "',"
                                     +"'" + GV_MachineName + "','3')";
                    dc.ExeSql(inSqlTemp);
                }
                e.Result = "OK,增加成功！";
                return;
            }
            if (oFlag == "DELETE")
            {
                //获取工位
                List<string> s = new List<string>();
                for (int i = 4; i < cnt; i++)
                {
                    s.Add(collection[i].ToString());
                }
                string[] s1 = s.ToArray();

                if (ASPxGridView2.Selection.Count == 0)
                {
                    e.Result = "Fail,请选择要删除的检测数据！";
                    return;
                }
                for (int i = 0; i < s1.Length; i++)
                {
                    detectCode = s1[i].ToString();
                    string sql = "select insert_flag from REL_STATION_DETECT_TEMP where PLINE_CODE='" + pline + "' "
                               + " AND STATION_CODE='" + station + "' and PRODUCT_SERIES ='" + pSeries + "' "
                               + " AND MACHINENAME='" + GV_MachineName + "' and detect_code='" + detectCode + "' ";
                    dataConn dc1 = new dataConn(sql);
                    DataTable dt1 = dc1.GetTable();

                    if (dt1.Rows[0]["insert_flag"].ToString() == "1")
                    {
                        string upSql = "update REL_STATION_DETECT_TEMP set insert_flag='2' where PLINE_CODE='" + pline + "'  "
                                     + "AND STATION_CODE='" + station + "' and PRODUCT_SERIES ='" + pSeries + "' "
                                     + " AND MACHINENAME='" + GV_MachineName + "' and detect_code='" + detectCode + "' ";
                        dc.ExeSql(upSql);

                    }
                    if (dt1.Rows[0]["insert_flag"].ToString() == "3")
                    {
                        string deSqlTemp = "DELETE FROM  REL_STATION_DETECT_TEMP WHERE PLINE_CODE='" + pline + "' and PRODUCT_SERIES ='" + pSeries + "' "
                                         + " AND STATION_CODE='" + station + "' AND MACHINENAME='" + GV_MachineName + "' and detect_code='" + detectCode + "'";
                        dc.ExeSql(deSqlTemp);
                    }
                }
                e.Result = "OK,删除成功！";
                return;
            }
            if (collection[0] == "SURE")
            {
                string tempSql = "select RMES_ID,detect_code from REL_STATION_DETECT_TEMP";
                dataConn tempDc = new dataConn(tempSql);
                DataTable tempDt = tempDc.GetTable();

                for (count = 0; count < tempDt.Rows.Count; count++)
                {
                    string rmesID = tempDt.Rows[count]["RMES_ID"].ToString();
                    detectCode = tempDt.Rows[count]["detect_code"].ToString();

                    string sql = "select insert_flag from REL_STATION_DETECT_TEMP where rmes_id='" + rmesID + "'";
                    dataConn dc1 = new dataConn(sql);
                    DataTable dt1 = dc1.GetTable();

                    if (dt1.Rows[0]["insert_flag"].ToString() == "2")
                    {
                        //插入到日志表
                        try
                        {
                            string Sql2 = "INSERT INTO REL_STATION_DETECT_LOG (rmes_id,company_code,pline_code,station_code,product_series,detect_code,detect_name,detect_type,detect_standard,"
                                        + "detect_max,detect_min,detect_unit,DETECT_SEQ,TEMP01,TEMP02,TEMP03,user_code,flag,rqsj)"
                                        + " SELECT rmes_id,company_code,pline_code,station_code,product_series,detect_code,detect_name,detect_type,detect_standard,"
                                        + "detect_max,detect_min,detect_unit,DETECT_SEQ,TEMP01,TEMP02,TEMP03,'"
                                        + theUserCode + "' , 'DEL', SYSDATE FROM REL_STATION_DETECT WHERE  rmes_id =  '"
                                        + rmesID + "' ";
                            dc.ExeSql(Sql2);
                        }
                        catch
                        {
                            return;
                        }

                        string dSql = "delete from REL_STATION_DETECT where rmes_id='" + rmesID + "'";
                        dc.ExeSql(dSql);

                    }
                    if (dt1.Rows[0]["insert_flag"].ToString() == "3")
                    {
                        string inSql = "insert into REL_STATION_DETECT(RMES_ID,COMPANY_CODE,PLINE_CODE,STATION_CODE,PRODUCT_SERIES,DETECT_CODE,DETECT_NAME,DETECT_TYPE,"
                                     + "DETECT_STANDARD,DETECT_MAX,DETECT_MIN,DETECT_UNIT,DETECT_SEQ,input_person,input_time) "
                                     + "select RMES_ID,'" + theCompanyCode + "',PLINE_CODE,STATION_CODE,PRODUCT_SERIES,DETECT_CODE,DETECT_NAME,DETECT_TYPE,"
                                     + "DETECT_STANDARD,DETECT_MAX,DETECT_MIN,DETECT_UNIT,DETECT_SEQ,'"+theUserId+"',sysdate from REL_STATION_DETECT_TEMP "
                                     + "where RMES_ID='" + rmesID + "'";
                        dc.ExeSql(inSql);
                        //插入到日志表
                        try
                        {
                            string Sql2 = "INSERT INTO REL_STATION_DETECT_LOG (rmes_id,company_code,pline_code,station_code,product_series,detect_code,detect_name,detect_type,detect_standard,"
                                        + "detect_max,detect_min,detect_unit,DETECT_SEQ,TEMP01,TEMP02,TEMP03,user_code,flag,rqsj)"
                                        + " SELECT rmes_id,company_code,pline_code,station_code,product_series,detect_code,detect_name,detect_type,detect_standard,"
                                        + "detect_max,detect_min,detect_unit,DETECT_SEQ,TEMP01,TEMP02,TEMP03,'"
                                        + theUserCode + "' , 'ADD', SYSDATE FROM REL_STATION_DETECT WHERE  rmes_id =  '"
                                        + rmesID + "' ";
                            dc.ExeSql(Sql2);
                        }
                        catch
                        {
                            return;
                        }

                    }
                }
                string sqlD = "DELETE from REL_STATION_DETECT_TEMP";
                dc.ExeSql(sqlD);

                e.Result = "Sure,确认成功！";
                return;

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