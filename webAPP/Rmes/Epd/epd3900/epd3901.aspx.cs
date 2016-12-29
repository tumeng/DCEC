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
using Rmes.DA.Factory;
using Rmes.DA.Entity;
using DevExpress.Web.ASPxGridView;

/*
 * 跨线工位工序维护
 * 杨少霞
 * 2016.9.6修改工序和替换后的对应关系为gridview
 * */

namespace Rmes.WebApp.Rmes.Epd.epd3900
{
    public partial class epd3901 : System.Web.UI.Page
    {
        private dataConn dc = new dataConn();
        private PubCs pc = new PubCs();
        public string theCompanyCode, theProgramCode, theUserId,theUserCode;

        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theProgramCode = "epd3900";
            theUserId = theUserManager.getUserId();
            theUserCode = theUserManager.getUserCode();

            string sqlNew = "select a.pline_code,a.rmes_id,a.pline_name from code_product_line a ";
            comboPlineCodeNew.DataSource = dc.GetTable(sqlNew);
            comboPlineCodeNew.DataBind();

            string sql1 = "select ALINE_CODE,ALINE_NAME from ATPU_ACROSSLINE where COMPANY_CODE = '" + theCompanyCode + "' order by ALINE_CODE";
            comboAline.DataSource = dc.GetTable(sql1);
            comboAline.DataBind();

            if (!IsPostBack)
            {
                
            }

        }
        private void SetCondition()
        {
            if (comboAline.Value != null)
            {
                string sql = "select a.RMES_ID,a.LOCATION_CODE,a.PROCESS_CODE,a.process_id, "
                                + " a.process_name,a.location_name  "
                                + " from ATPU_ACROSS_DATA a "
                                + " where a.ALINE_CODE='" + comboAline.Value.ToString() + "' and a.pline_id_new is null "
                                + " and a.process_id not in (select process_id from ATPU_ACROSS_DATA_TEMP)  order by a.input_time desc nulls last, a.PROCESS_CODE";
                DataTable dt = dc.GetTable(sql);

                ASPxGridView1.DataSource = dt;
                ASPxGridView1.DataBind();
            }
        }
        private void SetCondition2()
        {
            string aline_code = "";
            try
            {
                aline_code = comboAline.Value.ToString();
            }
            catch
            { }
            string sqlQ = "select PLINE_ID_OLD,LOCATION_ID,LOCATION_CODE,LOCATION_NAME,"
                                 + "PROCESS_ID,ALINE_CODE,PROCESS_CODE,PROCESS_NAME from ATPU_ACROSS_DATA_TEMP where aline_code='" + aline_code + "'";
            DataTable dtQ = dc.GetTable(sqlQ);

            ASPxGridView2.DataSource = dtQ;
            ASPxGridView2.DataBind();
            
        }
        protected void ASPxGridView1_CustomCallback1(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string alineCode = e.Parameters;
            //初始化GRIDVIEW
            string sql = "select a.RMES_ID,a.LOCATION_CODE,a.PROCESS_CODE,a.process_id, "
                        + " a.process_name,a.location_name  "
                        + " from ATPU_ACROSS_DATA a "
                        + " where a.ALINE_CODE='" + alineCode + "' and a.pline_id_new is null "
                        + " and a.process_id not in (select process_id from ATPU_ACROSS_DATA_TEMP)  order by a.input_time desc nulls last, a.PROCESS_CODE";
            DataTable dt = dc.GetTable(sql);

            ASPxGridView gridview1 = sender as ASPxGridView;
            gridview1.DataSource = dt;
            gridview1.DataBind();
            gridview1.Selection.UnselectAll();
        }
        protected void ASPxGridView1_PageIndexChanged(object sender, EventArgs e)
        {
            SetCondition();
            ASPxGridView1.DataBind();
        }
        protected void ASPxGridView2_PageIndexChanged(object sender, EventArgs e)
        {
            SetCondition2();
            ASPxGridView2.DataBind();
        }
        protected void ASPxGridView2_CustomCallback1(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            SetCondition2();
            ASPxGridView2.Selection.UnselectAll();
        }
        //原生产线初始化callback
        protected void comboPlineCodeOld_Callback(object sender, CallbackEventArgsBase e)
        {
            string alineCode = e.Parameter;

            string sql = "select distinct a.PLINE_ID_OLD,a.PLINE_CODE_OLD,a.PLINE_NAME_OLD"
                        + " from ATPU_ACROSS_DATA a "
                        + " where a.ALINE_CODE='" + alineCode + "' order by  a.PLINE_CODE_OLD";
            DataTable dt = dc.GetTable(sql);

            ASPxComboBox process = sender as ASPxComboBox;
            process.DataSource = dt;
            process.DataBind();
        }
        //工位初始化callback
        protected void listLocationTH_Callback(object sender, CallbackEventArgsBase e)
        {
            string plineNew = e.Parameter;

            string sql = "select RMES_ID,LOCATION_NAME,LOCATION_CODE from CODE_LOCATION where PLINE_CODE='" + plineNew + "' order by LOCATION_CODE";
            DataTable dt = dc.GetTable(sql);

            ASPxListBox location = sender as ASPxListBox;
            location.DataSource = dt;
            location.DataBind();
        }

        protected void butCloseWindow_Click(object sender, EventArgs e)
        {
            Response.Write("<script type='text/javascript'>window.opener.location.reload();window.close();</script>");
        }

        //替换到临时表，删除，保存替换的处理
        protected void ASPxCbSubmit_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
        {
            int count = 0;
            string processId,locationId,plineIdNew,alineCode,plineIdOld;
            string plineCodeNew="";
            string plineNameNew="";

            plineIdOld = comboPlineCodeOld.Value.ToString();
            plineIdNew = comboPlineCodeNew.Value.ToString();
            alineCode = comboAline.Value.ToString();

            string chSql = "select pline_code,pline_name from code_product_line where rmes_id='" + plineIdNew + "'";
            dataConn chDc = new dataConn(chSql);
            DataTable chDt = chDc.GetTable();
            if (chDt.Rows.Count > 0)
            {
                plineCodeNew = chDt.Rows[0]["pline_code"].ToString();
                plineNameNew = chDt.Rows[0]["pline_name"].ToString();
            }
            
            char[] charSeparators = new char[] { ',' };
            string[] collection = e.Parameter.Split(charSeparators);
            int cnt = collection.Length;
            
            if (collection[0] == "TIHUAN")
            {
                locationId = collection[1];
                //替换
                //获取工位，工序
                List<string> s = new List<string>();
                for (int i = 2; i < cnt; i++)
                {
                    s.Add(collection[i].ToString());
                }
                string[] s1 = s.ToArray();

                if (ASPxGridView1.Selection.Count == 0)
                {
                    e.Result = "Fail,请选择要替换的工序！";
                    return;
                }
                
                for (int i = 0; i < s1.Length; i++)
                {
                    processId = s1[i].ToString();
                    
                    string lCode = "";
                    string lName = "";

                    string chSql1 = "select location_code,location_name from code_location where rmes_id='" + locationId + "'";
                    dataConn chDc1 = new dataConn(chSql1);
                    DataTable chDt1 = chDc1.GetTable();
                    if (chDt1.Rows.Count > 0)
                    {
                        lCode = chDt1.Rows[0]["location_code"].ToString();
                        lName = chDt1.Rows[0]["location_name"].ToString();
                    }

                    string pCode = "";
                    string pName = "";

                    string chSql2 = "select process_code,process_name from code_process where rmes_id='" + processId + "'";
                    dataConn chDc2 = new dataConn(chSql2);
                    DataTable chDt2 = chDc2.GetTable();
                    if (chDt2.Rows.Count > 0)
                    {
                        pCode = chDt2.Rows[0]["process_code"].ToString();
                        pName = chDt2.Rows[0]["process_name"].ToString();
                    }
                    string inSql = "insert into ATPU_ACROSS_DATA_TEMP (PLINE_ID_OLD,LOCATION_ID,LOCATION_CODE,LOCATION_NAME,"
                                 + "PROCESS_ID,ALINE_CODE,PROCESS_CODE,PROCESS_NAME) values ('" + plineIdOld + "','" + locationId + "','" + lCode + "','" + lName + "',"
                                 + "'" + processId + "','" + alineCode + "','" + pCode + "','" + pName + "')";
                    dc.ExeSql(inSql);

                }
                e.Result = "OK1,替换成功！";
                return;
            }
            if (collection[0] == "SHANCHU")
            {
                //替换
                //获取工位，工序
                List<string> s = new List<string>();
                for (int i = 2; i < cnt; i++)
                {
                    s.Add(collection[i].ToString());
                }
                string[] s1 = s.ToArray();

                if (ASPxGridView2.Selection.Count == 0)
                {
                    e.Result = "Fail,请选择要删除的工序！";
                    return;
                }
                
                for (int i = 0; i < s1.Length; i++)
                {
                    processId = s1[i].ToString();
                    
                    string pCode = "";
                    string pName = "";

                    string chSql2 = "select process_code,process_name from code_process where rmes_id='" + processId + "'";
                    dataConn chDc2 = new dataConn(chSql2);
                    DataTable chDt2 = chDc2.GetTable();
                    if (chDt2.Rows.Count > 0)
                    {
                        pCode = chDt2.Rows[0]["process_code"].ToString();
                        pName = chDt2.Rows[0]["process_name"].ToString();
                    }
                    string dSql = "delete from ATPU_ACROSS_DATA_TEMP where PROCESS_ID='" + processId + "'";
                    dc.ExeSql(dSql);

                }
                e.Result = "OK1,删除成功！";
                return;
            }
            if (collection[0] == "SURE")
            {
                string tempSql = "select process_id,location_id from atpu_across_data_temp";
                dataConn tempDc = new dataConn(tempSql);
                DataTable tempDt = tempDc.GetTable();


                for (count = 0; count < tempDt.Rows.Count; count++)
                {
                    processId = tempDt.Rows[count]["process_id"].ToString();
                    locationId = tempDt.Rows[count]["location_id"].ToString();

                    string lCode = "";
                    string lName = "";

                    string chSql1 = "select location_code,location_name from code_location where rmes_id='" + locationId + "'";
                    dataConn chDc1 = new dataConn(chSql1);
                    DataTable chDt1 = chDc1.GetTable();
                    if (chDt1.Rows.Count > 0)
                    {
                        lCode = chDt1.Rows[0]["location_code"].ToString();
                        lName = chDt1.Rows[0]["location_name"].ToString();
                    }

                    string pCode = "";
                    string pName = "";

                    string chSql2 = "select process_code,process_name from code_process where rmes_id='" + processId + "'";
                    dataConn chDc2 = new dataConn(chSql2);
                    DataTable chDt2 = chDc2.GetTable();
                    if (chDt2.Rows.Count > 0)
                    {
                        pCode = chDt2.Rows[0]["process_code"].ToString();
                        pName = chDt2.Rows[0]["process_name"].ToString();
                    }
                    //插入到日志表
                    try
                    {
                        string Sql2 = "INSERT INTO ATPU_ACROSS_DATA_LOG (RMES_ID,COMPANY_CODE,PLINE_ID_OLD,PLINE_CODE_OLD,PLINE_NAME_OLD,PLINE_ID_NEW,PLINE_CODE_NEW,PLINE_NAME_NEW,LOCATION_ID,"
                            + "LOCATION_CODE,LOCATION_NAME,PROCESS_ID,PROCESS_CODE,PROCESS_NAME,LOCATION_SEQUENCE,ALINE_CODE,ALINE_NAME,user_code,flag,rqsj)"
                            + " SELECT RMES_ID,COMPANY_CODE,PLINE_ID_OLD,PLINE_CODE_OLD,PLINE_NAME_OLD,PLINE_ID_NEW,PLINE_CODE_NEW,PLINE_NAME_NEW,LOCATION_ID,"
                            + "LOCATION_CODE,LOCATION_NAME,PROCESS_ID,PROCESS_CODE,PROCESS_NAME,LOCATION_SEQUENCE,ALINE_CODE,ALINE_NAME,'"
                            + theUserCode + "' , 'BEFOREEDIT', SYSDATE FROM ATPU_ACROSS_DATA where aline_code='" + alineCode + "' "
                               + " and PLINE_ID_OLD='" + plineIdOld + "' and   process_id='" + processId + "'";
                        dc.ExeSql(Sql2);
                    }
                    catch
                    {
                        return;
                    }
                    string sql = "update ATPU_ACROSS_DATA set location_id='" + locationId + "',PLINE_ID_NEW='" + plineIdNew + "',"
                               + "location_code='" + lCode + "',location_name='" + lName + "',input_person='"+theUserId+"',input_time=sysdate, "
                               + "pline_code_new='" + plineCodeNew + "',pline_name_new='" + plineNameNew + "' where aline_code='" + alineCode + "' "
                               + " and PLINE_ID_OLD='" + plineIdOld + "' and   process_id='" + processId + "'";
                    dc.ExeSql(sql);
                    //插入到日志表
                    try
                    {
                        string Sql2 = "INSERT INTO ATPU_ACROSS_DATA_LOG (RMES_ID,COMPANY_CODE,PLINE_ID_OLD,PLINE_CODE_OLD,PLINE_NAME_OLD,PLINE_ID_NEW,PLINE_CODE_NEW,PLINE_NAME_NEW,LOCATION_ID,"
                            + "LOCATION_CODE,LOCATION_NAME,PROCESS_ID,PROCESS_CODE,PROCESS_NAME,LOCATION_SEQUENCE,ALINE_CODE,ALINE_NAME,user_code,flag,rqsj)"
                            + " SELECT RMES_ID,COMPANY_CODE,PLINE_ID_OLD,PLINE_CODE_OLD,PLINE_NAME_OLD,PLINE_ID_NEW,PLINE_CODE_NEW,PLINE_NAME_NEW,LOCATION_ID,"
                            + "LOCATION_CODE,LOCATION_NAME,PROCESS_ID,PROCESS_CODE,PROCESS_NAME,LOCATION_SEQUENCE,ALINE_CODE,ALINE_NAME,'"
                            + theUserCode + "' , 'AFTEREDIT', SYSDATE FROM ATPU_ACROSS_DATA where aline_code='" + alineCode + "' "
                               + " and PLINE_ID_OLD='" + plineIdOld + "' and   process_id='" + processId + "'";
                        dc.ExeSql(Sql2);
                    }
                    catch
                    {
                        return;
                    }

                }
                string sqlD = "DELETE from ATPU_ACROSS_DATA_TEMP  where aline_code='" + alineCode + "' ";
                dc.ExeSql(sqlD);

                e.Result = "Sure,保存替换成功！";
                return;
            }
         }

        protected void BtnCloseWindow_Click(object sender, EventArgs e)
        {
            Response.Write("<script type='text/javascript'>window.opener.location.reload();window.close();</script>");
        }
        
  }
}