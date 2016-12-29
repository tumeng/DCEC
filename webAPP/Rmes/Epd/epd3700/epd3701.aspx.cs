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
using System.Net;


public partial class Rmes_epd3701 : BasePage
{
    private dataConn dc = new dataConn();
    private PubCs pc = new PubCs();
    public string theCompanyCode, theProgramCode, theUserId, GV_MachineName,theUserCode;

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();

        string hostIPAddress = Page.Request.UserHostAddress;
        //IPHostEntry hostInfo = System.Net.Dns.GetHostByAddress(hostIPAddress);
        GV_MachineName = hostIPAddress;
        //GV_MachineName = System.Net.Dns.GetHostName();

        theProgramCode = "epd3700";
        theUserId = theUserManager.getUserId();
        theUserCode = theUserManager.getUserCode();

        string sql = "select a.pline_code,b.rmes_id,b.pline_name from VW_USER_ROLE_PROGRAM a "
                   + "left join code_product_line b on a.pline_code=b.pline_code "
                   + "where a.COMPANY_CODE = '" + theCompanyCode + "' and a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' order by b.PLINE_NAME";
        comboPlineCode.DataSource = dc.GetTable(sql);
        comboPlineCode.DataBind();

        setCondition();

        if (!IsPostBack)
        {
            string sqlD = "DELETE from REL_STATION_PRESTATION_TEMP";
            dc.ExeSql(sqlD);
        }

    }
    private void setCondition()
    {
        if (comboPlineCode.Value != null && comboStationCode.Value != null)
        {
            string sql = "SELECT A.RMES_ID,A.STATION_CODE,A.STATION_NAME STATION_NAME FROM CODE_STATION A WHERE A.PLINE_CODE='" + comboPlineCode.Value.ToString() + "'"
                           + " and not EXISTS(select * from REL_STATION_PRESTATION c where c.prestation_code=a.rmes_id and c.pline_code='" + comboPlineCode.Value.ToString() + "' and c.station_code='" + comboStationCode.Value.ToString() + "') "
                           + " and not EXISTS(select * from REL_STATION_PRESTATION_temp d where d.prestation_code=a.rmes_id and d.pline_code='" + comboPlineCode.Value.ToString() + "'and d.station_code='" + comboStationCode.Value.ToString() + "') "
                           + " ORDER BY A.STATION_NAME";

            DataTable dt = dc.GetTable(sql);

            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
        }
    }

    protected void comboStationCode_Callback(object sender, CallbackEventArgsBase e)
    {
        string pline = e.Parameter;

        string sql = "select '' RMES_ID,'' STATION_NAME FROM DUAL UNION select RMES_ID,STATION_NAME from CODE_STATION where PLINE_CODE='" + pline + "' order by STATION_NAME DESC";
        DataTable dt = dc.GetTable(sql);

        ASPxComboBox station = sender as ASPxComboBox;
        station.DataSource = dt;
        station.ValueField = "RMES_ID";
        station.TextField = "STATION_NAME";
        station.DataBind();
    }
    protected void ASPxGridView1_CustomCallback1(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        string[] param = e.Parameters.Split(',');
        string sss1 = param[0];//生产线
        string sss2 = param[1];//站点
        
        //
        string sql = "SELECT A.RMES_ID,A.STATION_CODE,A.STATION_NAME STATION_NAME FROM CODE_STATION A WHERE A.PLINE_CODE='" + sss1 + "'"
                   + " and not EXISTS(select * from REL_STATION_PRESTATION c where c.prestation_code=a.rmes_id and c.pline_code='" + sss1 + "' and c.station_code='" + sss2 + "') "
                   + " and not EXISTS(select * from REL_STATION_PRESTATION_temp d where d.prestation_code=a.rmes_id and d.pline_code='" + sss1 + "'and d.station_code='" + sss2 + "') "
                   + " ORDER BY A.STATION_NAME";
        
        DataTable dt = dc.GetTable(sql);

        ASPxGridView gridview1 = sender as ASPxGridView;
        gridview1.DataSource = dt;
        gridview1.DataBind();
        gridview1.Selection.UnselectAll();
    }
    protected void ASPxGridView2_CustomCallback1(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        string[] param = e.Parameters.Split(',');
        string sss1 = param[0];//生产线
        string sss2 = param[1];//站点
        string sss3 = param[2];//标识

        string sql = "";
        if (sss3 == "CHUFA" )
        {
            string inSqlTemp = "insert into REL_STATION_PRESTATION_TEMP(RMES_ID,PLINE_CODE,STATION_CODE,PRESTATION_CODE,MACHINENAME,INSERT_FLAG,station_name,preStation_name) "
                         + "select RMES_ID,PLINE_CODE,STATION_CODE,PRESTATION_CODE,'" + GV_MachineName + "','1',station_name,prestation_name from REL_STATION_PRESTATION "
                         + "where PLINE_CODE='" + sss1 + "' and station_code='" + sss2 + "'";
            dc.ExeSql(inSqlTemp);

            sql = "SELECT A.RMES_ID,A.STATION_CODE,A.STATION_NAME STATION_NAME FROM CODE_STATION A WHERE A.PLINE_CODE='"
                + sss1 + "' AND EXISTS(SELECT * FROM REL_STATION_PRESTATION_TEMP C WHERE C.PRESTATION_CODE=A.RMES_ID  "
                + " and PLINE_CODE='" + sss1 + "' and station_code='" + sss2 + "' and insert_flag<>'2')  ORDER BY A.STATION_NAME";

        }
        
        if (sss3 == "INIT")
        {
            sql = "SELECT A.RMES_ID,A.STATION_CODE,A.STATION_NAME STATION_NAME FROM CODE_STATION A  WHERE A.PLINE_CODE='" + sss1 + "' "
                    + " AND EXISTS(SELECT * FROM REL_STATION_PRESTATION_TEMP C WHERE C.PRESTATION_CODE=A.RMES_ID  and c.station_code='" + sss2 + "' and c.insert_flag<>'2') "
                    + " ORDER BY A.STATION_NAME";

        }
       
        DataTable dt = dc.GetTable(sql);

        ASPxGridView gridview2 = sender as ASPxGridView;
        gridview2.DataSource = dt;
        gridview2.DataBind();

        gridview2.Selection.UnselectAll();
    }
    protected void ASPxCbSubmit_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        int count = 0;

        string oFlag, pline, station, preStation;

        char[] charSeparators = new char[] { ',' };
        string[] collection = e.Parameter.Split(charSeparators);
        int cnt = collection.Length;

        oFlag = collection[0].ToString();
        pline = collection[1].ToString();
        station = collection[2].ToString();

        string stationName = "";
        string sql5 = "select station_name from code_station where rmes_id='" + station + "'";
        DataTable dt5 = dc.GetTable(sql5);
        if (dt5.Rows.Count > 0)
        {
            stationName = dt5.Rows[0]["station_name"].ToString();
        }
       

        if (oFlag == "ADD")
        {
            //获取前道站点
            List<string> s = new List<string>();
            for (int i = 3; i < cnt; i++)
            {
                s.Add(collection[i].ToString());
            }
            string[] s1 = s.ToArray();

            if (ASPxGridView1.Selection.Count == 0)
            {
                e.Result = "Fail,请选择要增加的前道站点！";
                return;
            }
            for (int i = 0; i < s1.Length; i++)
            {
                preStation = s1[i].ToString();

                string preStationName = "";
                string sql2 = "select station_name from code_station where rmes_id='" + preStation + "'";
                DataTable dt2 = dc.GetTable(sql2);
                if (dt2.Rows.Count > 0)
                {
                    preStationName = dt2.Rows[0]["station_name"].ToString();
                }

                string inSqlTemp = "insert into REL_STATION_PRESTATION_TEMP(RMES_ID,PLINE_CODE,STATION_CODE,PRESTATION_CODE,MACHINENAME,INSERT_FLAG,station_name,prestation_name) "
                            + "values(SEQ_RMES_ID.NEXTVAL,'" + pline + "','" + station + "','" + preStation + "','" + GV_MachineName + "','3','"+stationName+"','"+preStationName+"')";
                dc.ExeSql(inSqlTemp);
            }
            e.Result = "OK,增加成功！";
            return;
        }
        if (oFlag == "DELETE")
        {
            //获取工位
            List<string> s = new List<string>();
            for (int i = 3; i < cnt; i++)
            {
                s.Add(collection[i].ToString());
            }
            string[] s1 = s.ToArray();

            if (ASPxGridView2.Selection.Count == 0)
            {
                e.Result = "Fail,请选择要删除的前道站点！";
                return;
            }
            for (int i = 0; i < s1.Length; i++)
            {
                preStation = s1[i].ToString();
                string sql = "select insert_flag from REL_STATION_PRESTATION_TEMP where PLINE_CODE='" + pline + "' "
                           + " AND STATION_CODE='" + station + "' "
                           + " AND MACHINENAME='" + GV_MachineName + "' ";
                dataConn dc1 = new dataConn(sql);
                DataTable dt1 = dc1.GetTable();

                if (dt1.Rows[0]["insert_flag"].ToString() == "1")
                {
                    string upSql = "update REL_STATION_PRESTATION_TEMP set insert_flag='2' where PLINE_CODE='" + pline + "'  "
                                 + "AND STATION_CODE='" + station + "' "
                                 + " AND MACHINENAME='" + GV_MachineName + "' and prestation_code='"+preStation+"' ";
                    dc.ExeSql(upSql);

                }
                if (dt1.Rows[0]["insert_flag"].ToString() == "3")
                {
                    string deSqlTemp = "DELETE FROM  REL_STATION_PRESTATION_TEMP WHERE PLINE_CODE='" + pline + "' "
                                     + " AND STATION_CODE='" + station + "' AND MACHINENAME='" + GV_MachineName + "' and prestation_code='" + preStation + "'";
                    dc.ExeSql(deSqlTemp);
                }
            }
            e.Result = "OK,删除成功！";
            return;
        }
        if (collection[0] == "SURE")
        {
            string tempSql = "select RMES_ID,PRESTATION_CODE from REL_STATION_PRESTATION_TEMP";
            dataConn tempDc = new dataConn(tempSql);
            DataTable tempDt = tempDc.GetTable();

            for (count = 0; count < tempDt.Rows.Count; count++)
            {
                string rmesID = tempDt.Rows[count]["RMES_ID"].ToString();
                preStation = tempDt.Rows[count]["PRESTATION_CODE"].ToString();

                string sql = "select insert_flag from REL_STATION_PRESTATION_TEMP where rmes_id='" + rmesID + "'";
                dataConn dc1 = new dataConn(sql);
                DataTable dt1 = dc1.GetTable();

                if (dt1.Rows[0]["insert_flag"].ToString() == "2")
                {
                    //插入日志表
                    try
                    {
                        string sql2 = "insert into REL_STATION_PRESTATION_LOG(RMES_ID,COMPANY_CODE,PLINE_CODE,STATION_CODE,PRESTATION_CODE,station_name,prestation_name,INPUT_PERSON,INPUT_TIME,USER_CODE,FLAG,RQSJ)"
                                    + " select RMES_ID,COMPANY_CODE,PLINE_CODE,STATION_CODE,PRESTATION_CODE,station_name,prestation_name,INPUT_PERSON,INPUT_TIME,'" + theUserCode + "','DEL',sysdate from REL_STATION_PRESTATION where RMES_ID='" + rmesID + "'";
                        dc.ExeSql(sql2);
                    }
                    catch
                    {
                        return;
                    }
                    
                    string dSql = "delete from REL_STATION_PRESTATION where rmes_id='" + rmesID + "'";
                    dc.ExeSql(dSql);
                    

                }
                if (dt1.Rows[0]["insert_flag"].ToString() == "3")
                {
                    string inSql = "insert into REL_STATION_PRESTATION(RMES_ID,COMPANY_CODE,PLINE_CODE,STATION_CODE,PRESTATION_CODE,station_name,prestation_name,INPUT_PERSON,INPUT_TIME) "
                         + "select RMES_ID,'" + theCompanyCode + "',PLINE_CODE,STATION_CODE,PRESTATION_CODE,station_name,prestation_name,'"+theUserId+"',SYSDATE from REL_STATION_PRESTATION_TEMP "
                         + "where RMES_ID='" + rmesID + "'";
                    dc.ExeSql(inSql);

                    //插入日志表
                    try
                    {
                        string sql2 = "insert into REL_STATION_PRESTATION_LOG(RMES_ID,COMPANY_CODE,PLINE_CODE,STATION_CODE,PRESTATION_CODE,station_name,prestation_name,INPUT_PERSON,INPUT_TIME,USER_CODE,FLAG,RQSJ)"
                                    + " select RMES_ID,COMPANY_CODE,PLINE_CODE,STATION_CODE,PRESTATION_CODE,station_name,prestation_name,INPUT_PERSON,INPUT_TIME,'" + theUserCode + "','ADD',sysdate from REL_STATION_PRESTATION where RMES_ID='" + rmesID + "'";
                        dc.ExeSql(sql2);
                    }
                    catch
                    {
                        return;
                    }
                    
                }
            }
            string sqlD = "DELETE from REL_STATION_PRESTATION_TEMP";
            dc.ExeSql(sqlD);

            e.Result = "Sure,确认成功！";
            return;
            
        }
    }

    protected void BtnCloseWindow_Click(object sender, EventArgs e)
    {
        Response.Write("<script type='text/javascript'>window.opener.location.reload();window.close();</script>");
    }
    
}