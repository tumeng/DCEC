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


public partial class Rmes_epd3401 : BasePage
{
    private dataConn dc = new dataConn();
    private PubCs pc = new PubCs();
    public string theCompanyCode, theProgramCode, theUserId, theUserCode;

    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theProgramCode = "epd3400";
        theUserId = theUserManager.getUserId();
        theUserCode = theUserManager.getUserCode();
        string sql = "select a.pline_code,b.rmes_id,b.pline_name from VW_USER_ROLE_PROGRAM a "
                   +"left join code_product_line b on a.pline_code=b.pline_code "
                   +"where a.COMPANY_CODE = '" + theCompanyCode + "' and a.user_id='"+theUserId+"' and a.program_code='"+theProgramCode+"' order by b.PLINE_NAME";
        comboPlineCode.DataSource = dc.GetTable(sql);
        comboPlineCode.DataBind();

        //string sql1 = "select INTERNAL_CODE,INTERNAL_NAME from CODE_INTERNAL where COMPANY_CODE='" + theCompanyCode + "' AND INTERNAL_TYPE_CODE='001' order by INTERNAL_CODE desc";
        //locationPro.DataSource = dc.GetTable(sql1);
        //locationPro.DataBind();

        if (!IsPostBack)
        {
           
            comboPlineCode.SelectedIndex = 0;
            string sql23 = "select RMES_ID,STATION_NAME from CODE_STATION where PLINE_CODE='" + comboPlineCode.Value.ToString() + "' order by STATION_NAME";
            DataTable dt = dc.GetTable(sql23);

            comboStationCode.DataSource = dt;
            comboStationCode.ValueField = "RMES_ID";
            comboStationCode.TextField = "STATION_NAME";
            comboStationCode.DataBind();
             
            string sql2 = "SELECT A.RMES_ID,A.LOCATION_CODE,A.LOCATION_CODE LOCATION_NAME FROM CODE_LOCATION A WHERE  location_code not in (select location_code from vw_rel_station_location )  ORDER BY A.LOCATION_CODE";

            DataTable dt2 = dc.GetTable(sql2);
            ASPxListBox1.DataSource = dt2;
            ASPxListBox1.ValueField = "RMES_ID";
            ASPxListBox1.TextField = "LOCATION_NAME";
            ASPxListBox1.DataBind();

            //sql = "SELECT A.RMES_ID,A.LOCATION_CODE,A.LOCATION_CODE LOCATION_NAME FROM CODE_LOCATION A WHERE  location_code not in (select location_code from vw_rel_station_location where pline_id='" + comboPlineCode.Value.ToString() + "' and location_flag='A' ) and pline_code='" + comboPlineCode.Value.ToString() + "' ORDER BY A.LOCATION_CODE";
            sql = "select A.RMES_ID, A.LOCATION_CODE, A.LOCATION_CODE LOCATION_NAME from code_location a where pline_code = '" + comboPlineCode.Value.ToString() + "' and not exists(select * from vw_rel_station_location b where b.pline_id = '" + comboPlineCode.Value.ToString() + "' "
                + " and b.location_flag = 'A' and a.location_code=b.location_code and a.pline_code=b.pline_id ) ORDER BY A.LOCATION_CODE";
            dt = dc.GetTable(sql);

            ASPxListBox1.DataSource = dt;
            ASPxListBox1.DataBind();
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
    protected void listBoxLocation_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        //string pline = e.Parameter;
        string[] param = e.Parameter.Split(',');
        string sss1 = param[0];
        string sss2 = param[1];
        string sss3 = param[2];
        ASPxListBox location = sender as ASPxListBox;

        //如果是装配工位排除已经对应过站点的工位
        string sql = "";
        if (sss2 == "A")
        {
            sql = "SELECT A.RMES_ID,A.LOCATION_CODE,A.LOCATION_CODE LOCATION_NAME FROM CODE_LOCATION A WHERE A.PLINE_CODE='"
                + sss1 + "' AND NOT EXISTS(SELECT * FROM REL_STATION_LOCATION C WHERE C.LOCATION_CODE=A.RMES_ID  and c.LOCATION_FLAG='A' and PLINE_CODE='" + sss1 + "' ) "
                + " AND NOT EXISTS(SELECT * FROM REL_STATION_LOCATION D WHERE D.LOCATION_CODE=A.RMES_ID   and D.station_code='" + sss3 + "' ) " //and c.LOCATION_FLAG='B'
                +" ORDER BY A.LOCATION_CODE";//
        }
        if (sss2 == "B")
        {
            sql = "SELECT A.RMES_ID,A.LOCATION_CODE,A.LOCATION_CODE LOCATION_NAME FROM CODE_LOCATION A WHERE A.PLINE_CODE='" + sss1 + "' "
                + " AND NOT EXISTS(SELECT * FROM REL_STATION_LOCATION C WHERE C.LOCATION_CODE=A.RMES_ID   and c.station_code='" + sss3 + "' ) " //and c.LOCATION_FLAG='B'
                + " ORDER BY A.LOCATION_CODE";
        }
        //如果是查看工位显示所有工位
        DataTable dt = dc.GetTable(sql);

        location.DataSource = dt;
        location.DataBind();
        
    }
    protected void itemPro_Callback(object sender, CallbackEventArgsBase e)
    {
        string locationPro1 = e.Parameter;
        if (locationPro1 == "B")
        {
            string sql = "select INTERNAL_CODE,INTERNAL_NAME from CODE_INTERNAL where COMPANY_CODE='" + theCompanyCode + "' AND INTERNAL_TYPE_CODE='002' order by INTERNAL_CODE";
            DataTable dt = dc.GetTable(sql);

            ASPxComboBox itemPro1 = sender as ASPxComboBox;
            itemPro1.DataSource = dt;
            itemPro1.ValueField = "INTERNAL_CODE";
            itemPro1.TextField = "INTERNAL_NAME";
            itemPro1.DataBind();
        }
    }

    protected void butConfirm_Click(object sender, EventArgs e)
    {
        int count = 0;

        string location, pline, station,loactionProC,itemProC;

        pline = comboPlineCode.Value.ToString();
        station = comboStationCode.Value.ToString();
        loactionProC = locationPro.Value.ToString();
        
        if (comboItemPro.Value != null)
        {
            itemProC = comboItemPro.Value.ToString();
        }
        else
        {
            itemProC = "";
        }


        for (count = 0; count < listChosedLocation.Items.Count; count++)
        {

            location = listChosedLocation.Items[count].ToString();
            string[] location1 = location.Split(';');
            location=location1[0];
            location = dc.GetValue("select rmes_id from code_location where location_code='"+location+"' ");
            //取RMES_ID的值
            string sql_rmes_id = "SELECT SEQ_RMES_ID.NEXTVAL FROM DUAL ";
            dc.setTheSql(sql_rmes_id);
            string rmes_id = dc.GetTable().Rows[0][0].ToString();

            //插入到日志表
            try
            {
                string Sql2 = " INSERT INTO REL_STATION_LOCATION_LOG(rmes_id,company_code,pline_code,station_code,location_code,location_flag,location_flag1,user_code,flag,rqsj)"
                            + " VALUES('" + rmes_id + "','" + theCompanyCode + "','" + pline + "','" + station + "','" + location + "','" + loactionProC + "','" + itemProC + "','" + theUserCode + "','ADD',SYSDATE)";
                dc.ExeSql(Sql2);
            }
            catch
            {
                return;
            }
            string sql = "insert into rel_station_location(rmes_id,company_code,pline_code,station_code,location_code,LOCATION_FLAG,LOCATION_FLAG1)"
                       + "values(SEQ_RMES_ID.NEXTVAL,'" + theCompanyCode + "','" + pline + "','" + station + "','" + location + "','" + loactionProC + "','" + itemProC + "')";
            dc.ExeSql(sql);
            
            
        }

        Response.Write("<script type='text/javascript'>alert('新增站点工位关系成功！');window.opener.location.reload();location.href='epd3401.aspx';</script>");//window.opener.location.reload();window.close();
        comboPlineCode.Text = "";
        comboStationCode.Text = "";
        locationPro.Text = "";
        ASPxListBoxLocation.Items.Clear();
        comboItemPro.Text = "";
    }

    protected void butCloseWindow_Click(object sender, EventArgs e)
    {
        Response.Write("<script type='text/javascript'>window.opener.location.reload();window.close();</script>");
    }

    protected void ASPxListBox1_Callback(object sender, CallbackEventArgsBase e)
    {
        ASPxListBox location = sender as ASPxListBox;
        string pline = e.Parameter;
        string sql = "";
        //sql = "SELECT A.RMES_ID,A.LOCATION_CODE,A.LOCATION_CODE LOCATION_NAME FROM CODE_LOCATION A WHERE  location_code not in (select location_code from vw_rel_station_location where pline_id='" + pline + "' and location_flag='A' ) and pline_code='" + pline + "' ORDER BY A.LOCATION_CODE";
        sql = "select A.RMES_ID, A.LOCATION_CODE, A.LOCATION_CODE LOCATION_NAME from code_location a where pline_code = '" + pline + "' and not exists(select * from vw_rel_station_location b where b.pline_id = '" + pline + "' "
                + " and b.location_flag = 'A' and a.location_code=b.location_code and a.pline_code=b.pline_id ) ORDER BY A.LOCATION_CODE ";
        DataTable dt = dc.GetTable(sql);

        location.DataSource = dt;
        location.DataBind();
    }

    
    

}