using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxClasses;
using Rmes.Pub.Data;
using Rmes.DA.Base;
using Rmes.DA.Procedures;
using System.Net;
/**
 * 功能概述：单机装配信息查询
 * 作者：游晓航
 * 创建时间：2016-09-13
 */

public partial class Rmes_rept1400 : Rmes.Web.Base.BasePage
{
    private dataConn dc = new dataConn();
    private PubCs pc = new PubCs();
    public string theCompanyCode, theUserId, MachineName;
    public string theProgramCode;
    protected void Page_Load(object sender, EventArgs e)
    {
        userManager theUserManager = (userManager)Session["theUserManager"];
        theCompanyCode = theUserManager.getCompanyCode();
        theUserId = theUserManager.getUserId();
        //MachineName = System.Net.Dns.GetHostName();
        string hostIPAddress = Page.Request.UserHostAddress;
        //IPHostEntry hostInfo = System.Net.Dns.GetHostByAddress(hostIPAddress);
        MachineName = hostIPAddress;
        theProgramCode = "rept1400";
        setCondition();
        initCode();
        Session["rept1400_SN"] = txtSN.Text.Trim();
        if (!IsPostBack)
        {

            

        }
        if (Request["opFlag"] == "getEditSeries")
        {
            string str1 = "", strSo = "", strPseries = "", strPmodel = "", strPlancode = "", strFR = "", strtime1 = "", strtime2 = "";
            //DateTime Time1 = DateTime.Now, Time2 = DateTime.Now.AddDays(1);
            string sn = Request["SN"].ToString().Trim().ToUpper();
            string Sql = "select a.*,b.fr from data_product a left join atpuplannameplate b  on a.plan_Code=b.plan_code where a.sn='" + sn + "'";
            DataTable DT = dc.GetTable(Sql);
            
            if (DT.Rows.Count > 0)
            {
                strSo = DT.Rows[0]["PLAN_SO"].ToString();
                strPseries = DT.Rows[0]["PRODUCT_SERIES"].ToString();
                strPmodel = DT.Rows[0]["PRODUCT_MODEL"].ToString();
                strPlancode = DT.Rows[0]["PLAN_CODE"].ToString();
                string plinecode=dc.GetValue("select pline_code from data_plan where plan_code='"+txtPlanCode+"'");

                strFR = dc.GetValue("select GET_FR('" + strSo + "','" + plinecode + "') from dual ");
                str1 = strSo + "@" + strPseries + "@" + strPmodel + "@" + strPlancode + "@" + strFR;

                string Sql2 = "SELECT min(WORK_DATE),max(WORK_DATE) FROM VW_DATA_COMPLETE where SN='" + sn + "'";

                DataTable DT2 = dc.GetTable(Sql2);
                if (DT2.Rows.Count > 0)
                {
                    //Time1 = Convert.ToDateTime(DT2.Rows[0][0].ToString());
                    //Time2 = Convert.ToDateTime(DT2.Rows[0][1].ToString());
                    strtime1 = DT2.Rows[0][0].ToString();
                    strtime2 = DT2.Rows[0][1].ToString();
                    str1 = str1 + "@" + strtime1 + "@" + strtime2;
                }
            }
             
            this.Response.Write(str1);
            this.Response.End();
        }
        if (Request["opFlag"] == "getEditSeries2")
        {
            string str1 = "";
            string ghtm = "", fdjxh = "", cgqxh = "", cgqbh = "", rygg = "", jygg = "", syry = "", sydz = "";
            string sn = Request["SN"].ToString().Trim();

            string Sql3 = "SELECT * FROM NJJ_TEST_INFO WHERE  ghtm='" + sn + "'";
            DataTable DT3 = dc.GetTable(Sql3);
            if (DT3.Rows.Count > 0)
            {
                ghtm = DT3.Rows[0]["GHTM"].ToString();
                fdjxh = DT3.Rows[0]["FDJXH"].ToString();
                cgqxh = DT3.Rows[0]["CGQXH"].ToString();
                cgqbh = DT3.Rows[0]["CGQBH"].ToString();
                rygg = DT3.Rows[0]["RYGG"].ToString();
                jygg = DT3.Rows[0]["JYGG"].ToString();
                syry = DT3.Rows[0]["SYRY"].ToString();
                sydz = DT3.Rows[0]["SYDZ"].ToString();
            }
            str1 = ghtm + "@" + fdjxh + "@" + cgqxh + "@" + cgqbh + "@" + rygg + "@" + jygg + "@" + syry + "@" + sydz;
            this.Response.Write(str1);
            this.Response.End();
        }
        if (Request["opFlag"] == "getEditSeries3")
        {
            string str1 = "" ;
            //DateTime Time1 = DateTime.Now, Time2 = DateTime.Now.AddDays(1);
            string sn = Request["SN3"].ToString().Trim();
            string Sql = "select sn from data_product where sn='" + sn + "'";
            DataTable DT = dc.GetTable(Sql);
            if (DT.Rows.Count > 0)
            {
                str1 = DT.Rows[0][0].ToString();
                 
            }
             

             
            this.Response.Write(str1);
            this.Response.End();
        }
    }
    private void initCode()
    {
        //初始化下拉列表
        string sql = "select distinct a.pline_code,b.pline_name from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'";
        SqlCode.SelectCommand = sql;
        SqlCode.DataBind();

    }


    protected void setCondition()
    {
        
            string sql = "SELECT STATION_NAME,DETECT_NAME,case WHEN STATION_NAME='ZF160' and DETECT_VALUE='1' THEN '合格' when STATION_NAME='ZF160' and DETECT_VALUE='0' THEN '不合格' when DETECT_NAME='连杆大头侧隙' and DETECT_VALUE='1' then '合格' "
                       + " when DETECT_NAME='连杆大头侧隙' and DETECT_VALUE='0' then '不合格' else DETECT_VALUE end DETECT_VALUE from VW_DATA_SN_DETECT_DATA where SN='" + txtSN.Text.Trim() + "' ORDER BY STATION_NAME,DETECT_CODE";
            DataTable dt = dc.GetTable(sql);
            ASPxGridView6.DataSource = dt;
            ASPxGridView6.DataBind();
            string sql1 = " SELECT DISTINCT * FROM DATA_SN_FAULT where SN='" + txtSN.Text.Trim() + "'";
            DataTable dt1 = dc.GetTable(sql1);

            string sql2 = " SELECT a.* FROM vw_data_complete a where a.sn='" + txtSN.Text.Trim() + "' order by a.start_time,a.station_name";
            DataTable dt2 = dc.GetTable(sql2);
            ASPxGridView1.DataSource = dt2;
            ASPxGridView1.DataBind();

            string sql3 = " SELECT ghtm 流水号,rqsj 打印时间,jx 机型,bzso 标准so,gl 功率,qfjq 进阀进气,qfpq 进阀排气,pl 排量,xl 系列,ds 怠速,fhcx 发火次序,xnbh 性能表号,dyjx 打印机型,zs 转速 FROM sjenameplate where ghtm='" + txtSN.Text.Trim() + "'"
             + " union SELECT ghtm 流水号,rqsj1  打印时间,jx 机型,bzso 标准so,gl 功率,qfjq 进阀进气,qfpq 进阀排气,pl 排量,xl 系列,ds 怠速,fhcx 发火次序,xnbh 性能表号,dyjx 打印机型,zs 转速 from sjchgnmplt where ghtm='" + txtSN.Text.Trim() + "'";
            DataTable dt3 = dc.GetTable(sql3);
            ASPxGridView7.DataSource = dt3;
            ASPxGridView7.DataBind();
        
            //string sql4 = "SELECT * FROM NJJ_TEST_DETAIL where ghtm='" + txtSN2.Text.Trim() + "'";
            //DataTable dt4 = dc.GetTable(sql4);
            //ASPxGridView2.DataSource = dt4;
            //ASPxGridView2.DataBind();
        
       
            //string sql5 = "SELECT * from FZSJCLB where ghtm='" + txtSN3.Text.Trim() + "' ORDER BY zdmc,xmdm";
            //DataTable dt5 = dc.GetTable(sql5);
            //ASPxGridView3.DataSource = dt5;
            //ASPxGridView3.DataBind();
        
              
            //string sql6 = "SELECT * FROM JWTCM WHERE GHTM='" + txtSN4.Text.Trim() + "'";
            //DataTable dt6 = dc.GetTable(sql6);
            //ASPxGridView4.DataSource = dt6;
            //ASPxGridView4.DataBind();

            if (Session["rept1400_SN"] as string != null)
            {
                string sn = Session["rept1400_SN"].ToString();

                string sql7 = "SELECT a.SN 流水号,a.station_name 站点,to_char(a.create_time,'YYYY-MM-DD HH24:MI:SS') 扫描时间, b.user_name 员工,a.pline_code 地点,a.item_code 零件号,a.item_sn 零件流水号,a.item_name  零件名称,a.item_vendor 供应商, "
                            + " barcode 条码  FROM data_scan_item a left join code_user b on a.CREATE_USERID=b.USER_CODE   WHERE a.SN='" + sn + "'  and machinename='" + MachineName + "' and  item_type!='C'";
                DataTable dt7 = dc.GetTable(sql7);
                ASPxGridView5.DataSource = dt7;
                ASPxGridView5.DataBind();
            }

        

    }

    protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (txtSN.Text.Trim() == "")
        {
            ASPxGridView1.JSProperties.Add("cpCallbackName", "Fail");
            ASPxGridView1.JSProperties.Add("cpCallbackRet", "流水号不能为空！");
            return;
        }
        //下边调用存储过程生成一个中间表，保存发动机的实际扫描零件清单
        dc.ExeSql("delete from DATA_SCAN_ITEM where machinename='" + MachineName + "'"); 
        PL_INSERT_SJZJQD sp = new PL_INSERT_SJZJQD()
        {
            SN1 = txtSN.Text.Trim(),
            MACHINENAME1=MachineName

        };
        Procedure.run(sp);

        setCondition();


    }

    protected void ASPxGridView2_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        //if (txtSN2.Text.Trim() == "")
        //{
        //    ASPxGridView2.JSProperties.Add("cpCallbackName", "Fail");
        //    ASPxGridView2.JSProperties.Add("cpCallbackRet", "请先输入流水号再进行查询！");
        //    return;
        //}

        //setCondition();


    }
    protected void ASPxGridView7_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {

        setCondition();


    }
    protected void ASPxGridView5_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {

        setCondition();


    }
    protected void ASPxGridView6_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {

        setCondition();


    }
    protected void ASPxGridView3_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        //if (txtSN3.Text.Trim() == "")
        //{
        //    ASPxGridView3.JSProperties.Add("cpCallbackName", "Fail");
        //    ASPxGridView3.JSProperties.Add("cpCallbackRet", "请先输入流水号再进行查询！");
        //    return;
        //}
        //setCondition();


    }
    protected void ASPxGridView4_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        //if (txtSN4.Text.Trim() == "")
        //{
        //    ASPxGridView4.JSProperties.Add("cpCallbackName", "Fail");
        //    ASPxGridView4.JSProperties.Add("cpCallbackRet", "请先输入流水号再进行查询！");
        //    return;
        //}
        //setCondition();


    }
  
    protected void btnXlsExport_Click(object sender, EventArgs e)
    {
        ASPxGridViewExporter1.WriteXlsToResponse("单机装配信息导出");
    }

    

    

    protected void pageControl_TabClick(object source, DevExpress.Web.ASPxTabControl.TabControlCancelEventArgs e)
    {
        setCondition();

    }

    protected void data_scan_item()
    {
        //下边程序动态生成一个中间表，保存发动机的实际扫描零件清单,现在用存储过程PL_INSERT_SJZJQD代替
      
        string flag = dc.GetValue("select is_ENGoffline('" + txtSN.Text.Trim() + "') from dual");
        string chsql2 = "select plan_code from data_product where sn='" + txtSN.Text.Trim() + "'";//根据流水号找到对应的最新计划号
        string plancode = dc.GetValue(chsql2);
        string chsql3 = "", chsql4 = "", type = "", id = "", sname = "", icode = "", iname = "", userid = "", ctime = "", pline_code = "", i_sn = "", ivendor = "", barcode = "", insql = "";
        string delsql = "delete from data_scan_item ";//删掉中间表中的数据
        dc.ExeSql(delsql);
        if (flag == "0")//未下线的发动机信息在表：data_sn_bom_temp
        {
            //A类件的流水号，B类件的批次号在不同的表中获取
            chsql3 = "select item_type,rmes_id,item_code,item_name,station_name,create_userid,create_time,pline_code from data_sn_bom_temp where sn='" + txtSN.Text.Trim() + "' and plan_code='" + plancode + "'";
            DataTable chdt = dc.GetTable(chsql3);
            for (int i = 0; i < chdt.Rows.Count; i++)
            {
                type = chdt.Rows[i][0].ToString();
                id = chdt.Rows[i][1].ToString();
                icode = chdt.Rows[i][2].ToString();
                iname = chdt.Rows[i][3].ToString();
                sname = chdt.Rows[i][4].ToString();
                userid = chdt.Rows[i][5].ToString();
                ctime = chdt.Rows[i][6].ToString();
                pline_code = chdt.Rows[i][7].ToString();
                if (type == "A")
                {
                    chsql4 = "select item_sn,barcode,item_vendor from data_sn_bom_itemsn where bom_rmes_id='" + id + "'";
                    DataTable chdt4 = dc.GetTable(chsql4);
                    for (int j = 0; j < chdt4.Rows.Count; j++)
                    {
                        i_sn = chdt4.Rows[j][0].ToString();
                        barcode = chdt4.Rows[j][1].ToString();
                        ivendor = chdt4.Rows[j][2].ToString();
                        insql = "insert into data_scan_item(rmes_id,sn,plan_code,item_code,item_name,item_sn,item_vendor,item_type,station_name,create_userid,create_time,pline_code) "
                        + "values('" + id + "','" + txtSN.Text.Trim() + "','" + plancode + "','" + icode + "','" + iname + "','" + i_sn + "','" + ivendor + "','" + type + "','" + sname + "','" + userid + "',TO_DATE('" + ctime + "','YYYY-MM-DD HH24:MI:SS'),'" + pline_code + "')";
                        dc.ExeSql(insql);
                    }
                }
                else if (type == "B")
                {
                    chsql4 = "select item_sn,barcode,item_vendor from data_sn_bom_itembatch where bom_rmes_id='" + id + "'";
                    DataTable chdt4 = dc.GetTable(chsql4);
                    for (int j = 0; j < chdt4.Rows.Count; j++)
                    {
                        i_sn = chdt4.Rows[j][0].ToString();
                        barcode = chdt4.Rows[j][1].ToString();
                        ivendor = chdt4.Rows[j][2].ToString();
                        insql = "insert into data_scan_item(rmes_id,sn,plan_code,item_code,item_name,item_sn,item_vendor,item_type,station_name,create_userid,create_time,pline_code) "
                        + "values('" + id + "','" + txtSN.Text.Trim() + "','" + plancode + "','" + icode + "','" + iname + "','" + i_sn + "','" + ivendor + "','" + type + "','" + sname + "','" + userid + "',TO_DATE('" + ctime + "','YYYY-MM-DD HH24:MI:SS'),'" + pline_code + "')";
                        dc.ExeSql(insql);
                    }

                }
                else
                {
                    insql = " insert into data_scan_item(rmes_id,sn,plan_code,item_code,item_name,item_sn,item_vendor,item_type,station_name,create_userid,create_time,pline_code) "
                        + "select  rmes_id,sn,plan_code,item_code,item_name,item_batch,vendor_code,item_type,station_name,create_userid,create_time,pline_code  from data_sn_bom where  rmes_id='" + id + "'";
                    dc.ExeSql(insql);


                }

            }

        }
        else
        {
            //A类件的流水号，B类件的批次号在不同的表中获取
            chsql3 = "select item_type,rmes_id,item_code,item_name,station_name,create_userid,create_time,pline_code from data_sn_bom where sn='" + txtSN.Text.Trim() + "' and plan_code='" + plancode + "'";
            DataTable chdt = dc.GetTable(chsql3);
            for (int i = 0; i < chdt.Rows.Count; i++)
            {
                type = chdt.Rows[i][0].ToString();
                id = chdt.Rows[i][1].ToString();
                icode = chdt.Rows[i][2].ToString();
                iname = chdt.Rows[i][3].ToString();
                sname = chdt.Rows[i][4].ToString();
                userid = chdt.Rows[i][5].ToString();
                ctime = chdt.Rows[i][6].ToString();
                pline_code = chdt.Rows[i][7].ToString();
                if (type == "A")
                {
                    chsql4 = "select item_sn,barcode,item_vendor from data_sn_bom_itemsn where bom_rmes_id='" + id + "'";
                    DataTable chdt4 = dc.GetTable(chsql4);
                    for (int j = 0; j < chdt4.Rows.Count; j++)
                    {
                        i_sn = chdt4.Rows[j][0].ToString();
                        barcode = chdt4.Rows[j][1].ToString();
                        ivendor = chdt4.Rows[j][2].ToString();
                        insql = "insert into data_scan_item(rmes_id,sn,plan_code,item_code,item_name,item_sn,item_vendor,item_type,station_name,create_userid,create_time,pline_code) "
                        + "values('" + id + "','" + txtSN.Text.Trim() + "','" + plancode + "','" + icode + "','" + iname + "','" + i_sn + "','" + ivendor + "','" + type + "','" + sname + "','" + userid + "',TO_DATE('" + ctime + "','YYYY-MM-DD HH24:MI:SS'),'" + pline_code + "')";
                        dc.ExeSql(insql);
                    }
                }
                else if (type == "B")
                {
                    chsql4 = "select item_sn,barcode,item_vendor from data_sn_bom_itembatch where bom_rmes_id='" + id + "'";
                    DataTable chdt4 = dc.GetTable(chsql4);
                    for (int j = 0; j < chdt4.Rows.Count; j++)
                    {
                        i_sn = chdt4.Rows[j][0].ToString();
                        barcode = chdt4.Rows[j][1].ToString();
                        ivendor = chdt4.Rows[j][2].ToString();
                        insql = "insert into data_scan_item(rmes_id,sn,plan_code,item_code,item_name,item_sn,item_vendor,item_type,station_name,create_userid,create_time,pline_code) "
                        + "values('" + id + "','" + txtSN.Text.Trim() + "','" + plancode + "','" + icode + "','" + iname + "','" + i_sn + "','" + ivendor + "','" + type + "','" + sname + "','" + userid + "',TO_DATE('" + ctime + "','YYYY-MM-DD HH24:MI:SS'),'" + pline_code + "')";
                        dc.ExeSql(insql);
                    }

                }
                else
                {
                    insql = " insert into data_scan_item(rmes_id,sn,plan_code,item_code,item_name,item_sn,item_vendor,item_type,station_name,create_userid,create_time,pline_code) "
                        + "select  rmes_id,sn,plan_code,item_code,item_name,item_batch,vendor_code,item_type,station_name,create_userid,create_time,pline_code  from data_sn_bom where rmes_id='" + id + "'";
                    dc.ExeSql(insql);


                }

            }

        }
    }


}
