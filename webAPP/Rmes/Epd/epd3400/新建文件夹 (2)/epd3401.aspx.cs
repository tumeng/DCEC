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


public partial class Rmes_epd3401 : BasePage
{
    private dataConn dc = new dataConn();
    private PubCs pc = new PubCs();
    public string theCompanyCode, theProgramCode, theUserId, theUserCode, GV_MachineName;

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();

        string hostIPAddress = Page.Request.UserHostAddress;
        //IPHostEntry hostInfo = System.Net.Dns.GetHostByAddress(hostIPAddress);
        GV_MachineName = hostIPAddress;
        //GV_MachineName = System.Net.Dns.GetHostName();

        theProgramCode = "epd3400";
        theUserId = theUserManager.getUserId();
        theUserCode = theUserManager.getUserCode();
        string sql = "select a.pline_code,b.rmes_id,b.pline_name from VW_USER_ROLE_PROGRAM a "
                   +"left join code_product_line b on a.pline_code=b.pline_code "
                   +"where a.COMPANY_CODE = '" + theCompanyCode + "' and a.user_id='"+theUserId+"' and a.program_code='"+theProgramCode+"' order by b.PLINE_NAME";
        comboPlineCode.DataSource = dc.GetTable(sql);
        comboPlineCode.DataBind();

        SetCondition();

        if (!IsPostBack)
        {
            comboPlineCode.SelectedIndex = 0;
            string sql23 = "select RMES_ID,STATION_NAME from CODE_STATION where PLINE_CODE='" + comboPlineCode.Value.ToString() + "' order by STATION_NAME";
            DataTable dt = dc.GetTable(sql23);

            comboStationCode.DataSource = dt;
            comboStationCode.ValueField = "RMES_ID";
            comboStationCode.TextField = "STATION_NAME";
            comboStationCode.DataBind();

            sql = "select A.RMES_ID, A.LOCATION_CODE, A.LOCATION_CODE LOCATION_NAME from code_location a where pline_code = '" + comboPlineCode.Value.ToString() + "' and not exists(select * from vw_rel_station_location b where b.pline_id = '" + comboPlineCode.Value.ToString() + "' "
                + " and b.location_flag = 'A' and a.location_code=b.location_code and a.pline_code=b.pline_id ) ORDER BY A.LOCATION_CODE ";

            DataTable dt1 = dc.GetTable(sql);

            ASPxListBox1.DataSource = dt1;
            ASPxListBox1.DataBind();

            string sqlD = "DELETE from REL_STATION_LOCATION_TEMP";
            dc.ExeSql(sqlD);
            
        }
        
    }
    private void SetCondition()
    {
        if (comboPlineCode.Value != null && comboStationCode.Value != null && locationPro.Value!=null)
        {
            string sql = "";
            if (locationPro.Value.ToString() == "A")
            {
                sql = "SELECT A.RMES_ID,A.LOCATION_CODE,A.LOCATION_CODE LOCATION_NAME FROM CODE_LOCATION A WHERE A.PLINE_CODE='"
                    + comboPlineCode.Value.ToString() + "' AND NOT EXISTS(SELECT * FROM REL_STATION_LOCATION C WHERE C.LOCATION_CODE=A.RMES_ID  and c.LOCATION_FLAG='A' and PLINE_CODE='" + comboPlineCode.Value.ToString() + "' )  ORDER BY A.LOCATION_CODE";

            }
            if (locationPro.Value.ToString() == "B")
            {
                sql = "SELECT A.RMES_ID,A.LOCATION_CODE,A.LOCATION_CODE LOCATION_NAME FROM CODE_LOCATION A WHERE A.PLINE_CODE='" + comboPlineCode.Value.ToString() + "' "
                    + " AND NOT EXISTS(SELECT * FROM REL_STATION_LOCATION C WHERE C.LOCATION_CODE=A.RMES_ID  and c.LOCATION_FLAG='B' and c.station_code='" + comboStationCode.Value.ToString() + "' ) "
                    + " ORDER BY A.LOCATION_CODE";

            }
            //如果是查看工位显示所有工位
            DataTable dt = dc.GetTable(sql);

            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
        }
    }
    
    protected void comboStationCode_Callback(object sender, CallbackEventArgsBase e)
    {
        string pline = e.Parameter;

        string sql = "select RMES_ID,STATION_NAME from CODE_STATION where PLINE_CODE='" + pline + "' order by STATION_NAME";
        DataTable dt = dc.GetTable(sql);

        ASPxComboBox station = sender as ASPxComboBox;
        station.DataSource = dt;
        station.ValueField = "RMES_ID";
        station.TextField = "STATION_NAME";
        station.DataBind();
    }
    protected void locationPro_Callback(object sender, CallbackEventArgsBase e)
    {
        string sql1 = "select '' INTERNAL_CODE,'' INTERNAL_NAME from dual union select INTERNAL_CODE,INTERNAL_NAME from CODE_INTERNAL where COMPANY_CODE='" + theCompanyCode + "' AND INTERNAL_TYPE_CODE='001' order by INTERNAL_CODE desc";
        locationPro.DataSource = dc.GetTable(sql1);
        locationPro.DataBind();
    }
    
    protected void itemPro_Callback(object sender, CallbackEventArgsBase e)
    {
        string locationPro1 = e.Parameter;
        if (locationPro1 == "B")
        {
            string sql = "select '' INTERNAL_CODE,'' INTERNAL_NAME from dual union select INTERNAL_CODE,INTERNAL_NAME from CODE_INTERNAL where COMPANY_CODE='" + theCompanyCode + "' AND INTERNAL_TYPE_CODE='002' order by INTERNAL_CODE desc";
            comboItemPro.DataSource = dc.GetTable(sql);
            comboItemPro.DataBind();
            
        }
    }
    protected void ASPxGridView1_CustomCallback1(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        string[] param = e.Parameters.Split(',');
        string sss1 = param[0];//生产线
        string sss2 = param[1];//站点
        string sss3 = param[2];//工位属性

        //如果是装配工位排除已经对应过站点的工位
        string sql = "";
        if (sss2 == "B")//查看工位
        {
            sql = "SELECT A.RMES_ID,A.LOCATION_CODE,A.LOCATION_CODE LOCATION_NAME FROM CODE_LOCATION A WHERE A.PLINE_CODE='"
                + sss1 + "' AND NOT EXISTS(SELECT * FROM REL_STATION_LOCATION C WHERE C.LOCATION_CODE=A.RMES_ID  and c.LOCATION_FLAG='B' and PLINE_CODE='" + sss1 + "' ) "
                + " and not exists(select * from REL_STATION_LOCATION_temp d WHERE d.LOCATION_CODE=A.RMES_ID  and d.LOCATION_FLAG='B' and PLINE_CODE='" + sss1 + "' ) "
                + " ORDER BY A.LOCATION_CODE";

        }
        if (sss2 == "A")//装配工位
        {
            sql = "SELECT A.RMES_ID,A.LOCATION_CODE,A.LOCATION_CODE LOCATION_NAME FROM CODE_LOCATION A WHERE A.PLINE_CODE='" + sss1 + "' "
                + " AND NOT EXISTS(SELECT * FROM REL_STATION_LOCATION C WHERE C.LOCATION_CODE=A.RMES_ID  and c.LOCATION_FLAG='A'  and PLINE_CODE='" + sss1 + "' ) " //and c.station_code='" + sss3 + "'
                + " and not exists(select * from REL_STATION_LOCATION_temp d WHERE d.LOCATION_CODE=A.RMES_ID  and d.LOCATION_FLAG='A' and d.station_code='" + sss3 + "' and PLINE_CODE='" + sss1 + "' ) "
                + " ORDER BY A.LOCATION_CODE";

        }
        //如果是查看工位显示所有工位
        DataTable dt = dc.GetTable(sql);

        ASPxGridView gridview1 = sender as ASPxGridView;
        gridview1.DataSource = dt;
        gridview1.DataBind();
        gridview1.Selection.UnselectAll();
    }
    protected void ASPxGridView2_CustomCallback1(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        //string pline = e.Parameter;
        string[] param = e.Parameters.Split(',');
        string sss1 = param[0];//生产线
        string sss2 = param[1];//工位属性
        string sss3 = param[2];//站点
        string sss4 = param[3];//标识
        string sss5 = param[4];//是否显示全部工位

        //如果是装配工位排除已经对应过站点的工位
        string sql = "";
        if (sss4 == "CHUFA" && sss5 == "AAA")
        {
            string inSqlTemp = "insert into REL_STATION_LOCATION_TEMP(RMES_ID,PLINE_CODE,LOCATION_CODE,STATION_CODE,LOCATION_FLAG,MACHINENAME,LOCATION_FLAG1,INSERT_FLAG) "
                         + "select RMES_ID,PLINE_CODE,LOCATION_CODE,STATION_CODE,LOCATION_FLAG,'" + GV_MachineName + "',LOCATION_FLAG1,'1' from REL_STATION_LOCATION "
                         + "where LOCATION_FLAG='" + sss2 + "' and PLINE_CODE='" + sss1 + "' and station_code='" + sss3 + "'";
            dc.ExeSql(inSqlTemp);

            sql = "SELECT A.RMES_ID,A.LOCATION_CODE,A.LOCATION_CODE LOCATION_NAME FROM CODE_LOCATION A WHERE A.PLINE_CODE='"
                + sss1 + "' AND EXISTS(SELECT * FROM REL_STATION_LOCATION_TEMP C WHERE C.LOCATION_CODE=A.RMES_ID  "
                + "and c.LOCATION_FLAG='" + sss2 + "' and PLINE_CODE='" + sss1 + "' )  ORDER BY A.LOCATION_CODE";

        }
        if (sss4 == "CHUFA" && sss5 != "AAA")
        {
            string inSqlTemp = "insert into REL_STATION_LOCATION_TEMP(RMES_ID,PLINE_CODE,LOCATION_CODE,STATION_CODE,LOCATION_FLAG,MACHINENAME,LOCATION_FLAG1,INSERT_FLAG) "
                         + "select RMES_ID,PLINE_CODE,LOCATION_CODE,STATION_CODE,LOCATION_FLAG,'" + GV_MachineName + "',LOCATION_FLAG1,'1' from REL_STATION_LOCATION "
                         + "where LOCATION_FLAG='" + sss2 + "' and PLINE_CODE='" + sss1 + "' and station_code='" + sss3 + "' and LOCATION_FLAG1='" + sss5 + "'";
            dc.ExeSql(inSqlTemp);

            sql = "SELECT A.RMES_ID,A.LOCATION_CODE,A.LOCATION_CODE LOCATION_NAME FROM CODE_LOCATION A WHERE A.PLINE_CODE='"
                    + sss1 + "' AND EXISTS(SELECT * FROM REL_STATION_LOCATION_TEMP C WHERE C.LOCATION_CODE=A.RMES_ID  "
                    + "and c.LOCATION_FLAG='" + sss2 + "' and PLINE_CODE='" + sss1 + "' AND C.LOCATION_FLAG1='" + sss5 + "' )  ORDER BY A.LOCATION_CODE";

        }
        if (sss4 == "INIT" && sss5 == "BBB")
        {
            sql = "SELECT A.RMES_ID,A.LOCATION_CODE,A.LOCATION_CODE LOCATION_NAME FROM CODE_LOCATION A WHERE A.PLINE_CODE='" + sss1 + "' "
                    + " AND EXISTS(SELECT * FROM REL_STATION_LOCATION_TEMP C WHERE C.LOCATION_CODE=A.RMES_ID  and c.LOCATION_FLAG='" + sss2 + "' and c.station_code='" + sss3 + "' and c.insert_flag<>'2') "
                    + " ORDER BY A.LOCATION_CODE";

        }
        if (sss4 == "INIT" && sss5 != "BBB")
        {
            sql = "SELECT A.RMES_ID,A.LOCATION_CODE,A.LOCATION_CODE LOCATION_NAME FROM CODE_LOCATION A WHERE A.PLINE_CODE='" + sss1 + "' "
                        + " AND EXISTS(SELECT * FROM REL_STATION_LOCATION_TEMP C WHERE C.LOCATION_CODE=A.RMES_ID  and c.LOCATION_FLAG='" + sss2 + "' and c.station_code='" + sss3 + "' AND C.LOCATION_FLAG1='" + sss5 + "' and c.insert_flag<>'2') "
                        + " ORDER BY A.LOCATION_CODE";
        }
        //如果是查看工位显示所有工位
        DataTable dt = dc.GetTable(sql);

        ASPxGridView gridview2 = sender as ASPxGridView;
        gridview2.DataSource = dt;
        gridview2.DataBind();

        gridview2.Selection.UnselectAll();
    }

    protected void BtnCloseWindow_Click(object sender, EventArgs e)
    {
        Response.Write("<script type='text/javascript'>window.opener.location.reload();window.close();</script>");
    }

    protected void ASPxListBox1_Callback(object sender, CallbackEventArgsBase e)
    {
        ASPxListBox location = sender as ASPxListBox;
        string pline = e.Parameter;
        string sql = "";
        sql = "select A.RMES_ID, A.LOCATION_CODE, A.LOCATION_CODE LOCATION_NAME from code_location a where pline_code = '" + pline + "' and not exists(select * from vw_rel_station_location b where b.pline_id = '" + pline + "' "
                + " and b.location_flag = 'A' and a.location_code=b.location_code and a.pline_code=b.pline_id ) ORDER BY A.LOCATION_CODE ";

        DataTable dt = dc.GetTable(sql);

        location.DataSource = dt;
        location.DataBind();
    }
    protected void ASPxCbSubmit_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        int count = 0;

        string oFlag, pline, station, locationPro, locationId,itemPro;

        char[] charSeparators = new char[] { ',' };
        string[] collection = e.Parameter.Split(charSeparators);
        int cnt = collection.Length;

        oFlag = collection[0].ToString();
        pline = collection[1].ToString();
        station = collection[2].ToString();
        locationPro = collection[3].ToString();
        itemPro=collection[4].ToString();

        if (oFlag == "ADD")
        {
            //获取工位
            List<string> s = new List<string>();
            for (int i = 5; i < cnt; i++)
            {
                s.Add(collection[i].ToString());
            }
            string[] s1 = s.ToArray();

            if (ASPxGridView1.Selection.Count == 0)
            {
                e.Result = "Fail,请选择要增加的工位！";
                return;
            }
            for (int i = 0; i < s1.Length; i++)
            {
                locationId = s1[i].ToString();
                string inSqlTemp = "insert into REL_STATION_LOCATION_TEMP(RMES_ID,PLINE_CODE,LOCATION_CODE,STATION_CODE,LOCATION_FLAG,MACHINENAME,LOCATION_FLAG1,INSERT_FLAG) "
                            + "values(SEQ_RMES_ID.NEXTVAL,'" + pline + "','" + locationId + "','" + station + "','" + locationPro + "','" + GV_MachineName + "','" + itemPro + "','3')";
                dc.ExeSql(inSqlTemp);
            }
            e.Result = "OK,增加成功！";
            return;
        }
        if (oFlag == "DELETE")
        {
            //获取工位
            List<string> s = new List<string>();
            for (int i = 5; i < cnt; i++)
            {
                s.Add(collection[i].ToString());
            }
            string[] s1 = s.ToArray();

            if (ASPxGridView2.Selection.Count == 0)
            {
                e.Result = "Fail,请选择要删除的工位！";
                return;
            }
            for (int i = 0; i < s1.Length; i++)
            {
                locationId = s1[i].ToString();
                string sql = "select insert_flag from REL_STATION_LOCATION_TEMP where PLINE_CODE='" + pline + "' AND LOCATION_CODE='" + locationId + "'"
                           +" AND STATION_CODE='" + station + "' "
                           + " AND LOCATION_FLAG='" + locationPro + "' AND MACHINENAME='" + GV_MachineName + "' ";
                if(itemPro!="")
                {
                    sql = sql + "and LOCATION_FLAG1='" + itemPro + "' ";
                }
                
                dataConn dc1 = new dataConn(sql);
                DataTable dt1 = dc1.GetTable();

                if (dt1.Rows[0]["insert_flag"].ToString() == "1")
                {
                    string upSql = "update REL_STATION_LOCATION_TEMP set insert_flag='2' where PLINE_CODE='" + pline + "' AND LOCATION_CODE='" + locationId + "' "
                                 + "AND STATION_CODE='" + station + "' "
                                 + " AND LOCATION_FLAG='" + locationPro + "' AND MACHINENAME='" + GV_MachineName + "'";// and LOCATION_FLAG1='" + itemPro + "' ";
                    if (itemPro != "")
                    {
                        upSql = upSql + "and LOCATION_FLAG1='" + itemPro + "' ";
                    }
                    dc.ExeSql(upSql);

                }
                if (dt1.Rows[0]["insert_flag"].ToString() == "3")
                {
                    string deSqlTemp = "DELETE FROM  REL_STATION_LOCATION_TEMP WHERE PLINE_CODE='" + pline + "' AND LOCATION_CODE='" + locationId + "'"
                                     + " AND STATION_CODE='" + station + "' AND LOCATION_FLAG='" + locationPro + "' AND MACHINENAME='" + GV_MachineName + "'";// and LOCATION_FLAG1='" + itemPro + "' ";
                    if (itemPro != "")
                    {
                        deSqlTemp = deSqlTemp + "and LOCATION_FLAG1='" + itemPro + "' ";
                    }
                    dc.ExeSql(deSqlTemp);
                }
            }
            e.Result = "OK,删除成功！";
            return;
        }
        if (collection[0] == "SURE")
        {
            string tempSql = "select RMES_ID,location_code from REL_STATION_LOCATION_TEMP";
            dataConn tempDc = new dataConn(tempSql);
            DataTable tempDt = tempDc.GetTable();

            for (count = 0; count < tempDt.Rows.Count; count++)
            {
                string rmesID = tempDt.Rows[count]["RMES_ID"].ToString();
                locationId = tempDt.Rows[count]["location_code"].ToString();
                
                string sql = "select insert_flag from REL_STATION_LOCATION_TEMP where rmes_id='" + rmesID + "'";
                dataConn dc1 = new dataConn(sql);
                DataTable dt1 = dc1.GetTable();

                if (dt1.Rows[0]["insert_flag"].ToString() == "2")
                {
                    //插入到日志表
                    try
                    {
                        string Sql2 = " INSERT INTO REL_STATION_LOCATION_LOG(rmes_id,company_code,pline_code,station_code,location_code,location_flag,location_flag1,user_code,flag,rqsj)"
                                    + " VALUES('" + rmesID + "','" + theCompanyCode + "','" + pline + "','" + station + "','" + locationId + "','" + locationPro + "','" + itemPro + "','" + theUserCode + "','DELETE',SYSDATE)";
                        dc.ExeSql(Sql2);
                    }
                    catch
                    {
                        return;
                    }

                    string dSql = "delete from REL_STATION_LOCATION where rmes_id='" + rmesID + "'";
                    dc.ExeSql(dSql);

                    
                }
                if (dt1.Rows[0]["insert_flag"].ToString() == "3")
                {
                    string inSql = "insert into REL_STATION_LOCATION(RMES_ID,COMPANY_CODE,PLINE_CODE,LOCATION_CODE,STATION_CODE,LOCATION_FLAG,LOCATION_FLAG1,INPUT_PERSON,INPUT_TIME) "
                         + "select RMES_ID,'" + theCompanyCode + "',PLINE_CODE,LOCATION_CODE,STATION_CODE,LOCATION_FLAG,LOCATION_FLAG1,'" + theUserId + "',sysdate from REL_STATION_LOCATION_TEMP "
                         + "where RMES_ID='" + rmesID + "'";//20161031增加时间列
                    dc.ExeSql(inSql);
                    //插入到日志表
                    try
                    {
                        string Sql2 = " INSERT INTO REL_STATION_LOCATION_LOG(rmes_id,company_code,pline_code,station_code,location_code,location_flag,location_flag1,user_code,flag,rqsj)"
                                    + " VALUES('" + rmesID + "','" + theCompanyCode + "','" + pline + "','" + station + "','" + locationId + "','" + locationPro + "','" + itemPro + "','" + theUserCode + "','ADD',SYSDATE)";
                        dc.ExeSql(Sql2);
                    }
                    catch
                    {
                        return;
                    }
                }
            }
            string sqlD = "DELETE from rel_station_location_temp";
            dc.ExeSql(sqlD);

            e.Result = "Sure,确认成功！";
            return;
            //comboPlineCode.Text = "";
            //comboStationCode.Text = "";
            //locationPro.Text = "";
            //ASPxListBoxLocation.Items.Clear();
            //comboItemPro.Text = "";
        }
    }
    
}