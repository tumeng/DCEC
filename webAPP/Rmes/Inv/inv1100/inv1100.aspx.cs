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
using System.Text;
using System.Net;
using System.Windows.Forms;
/**
 * 2016-08-25
 * 杨少霞 
 * 入库操作
 * */

namespace Rmes.WebApp.Rmes.Inv.inv1100
{
    public partial class inv1100 : BasePage
    {
        private dataConn dc = new dataConn();
        private PubCs thePubCs = new PubCs();
        private static string theCompanyCode, theUserId, theUserName, theUserCode;
        public string theProgramCode;
        public string theDepotName, GV_MachineName, LX, v_CurrentBatchId;
        public string GV_Depot = "";
        public Boolean theVerifyFlag = true;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();
            theUserName = theUserManager.getUserName();
            theUserCode = theUserManager.getUserCode();
            theProgramCode = "inv1100";

            v_CurrentBatchId = "%";

            string hostIPAddress = Page.Request.UserHostAddress;
            //IPHostEntry hostInfo = System.Net.Dns.GetHostByAddress(hostIPAddress);
            GV_MachineName = hostIPAddress;
            //GV_MachineName = System.Net.Dns.GetHostName();
            if (comboPline.Value != null)
            {
                GV_Depot = comboPline.Value.ToString();
            }
            if (GV_Depot == "L")//161108add begin
            {
                Label13.Visible = true;
                comboJCJ.Visible = true;
            }
            else
            {
                Label13.Visible = false; ;
                comboJCJ.Visible = false;
            }//161108add end

            //根据登录人员去初始化生产线
            //string sqlNew = "select a.pline_code,b.rmes_id,b.pline_name from VW_USER_ROLE_PROGRAM a "
            //       + "left join code_product_line b on a.pline_code=b.pline_code "
            //       + "where a.COMPANY_CODE = '" + theCompanyCode + "' and a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' order by b.PLINE_NAME";
            //string sqlNew = "select depotid pline_code,depotname pline_name from dp_dmdepot ";
            string sqlNew = "select a.depotid pline_code,a.depotname pline_name from dp_dmdepot a left join REL_USER_DMDEPOT b on a.depotid=b.depotid where b.user_code='" + theUserCode + "' and b.PROGRAM_CODE='INV1100'  ";
                   //+ "left join code_product_line b on a.pline_code=b.pline_code "
                   //+ "where a.COMPANY_CODE = '" + theCompanyCode + "' and a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' order by b.PLINE_NAME";
            
            comboPline.DataSource = dc.GetTable(sqlNew);
            comboPline.DataBind();

            //初始化入库类型
            string SQlstr = "SELECT lxmc FROM DP_rklx where lxdm like 'I%' ORDER BY lxdm";
            comboRKLX.DataSource = dc.GetTable(SQlstr); 
            comboRKLX.TextField = "LXMC";
            comboRKLX.ValueField = "LXMC";
            comboRKLX.DataBind();

            SqlRklx.SelectCommand = "SELECT lxmc FROM DP_rklx where lxdm like 'I%' ORDER BY lxdm";

            //初始化员工
            string sql = "SELECT ygmc FROM DP_ygb ORDER BY ygmc";
            comboUser.DataSource = dc.GetTable(sql);
            comboUser.TextField = "YGMC";
            comboUser.ValueField = "YGMC";
            comboUser.DataBind();

            //初始化柳汽标识
            string sqlL = "SELECT internal_code,internal_name FROM code_internal where internal_type_code='010' ORDER BY internal_code";
            comboJCJ.DataSource = dc.GetTable(sqlL);
            comboJCJ.ValueField = "internal_name";
            comboJCJ.TextField = "internal_name";
            comboJCJ.DataBind();

            initBatch();
            setCondition();
            if (!IsPostBack)
            {
                comboPline.SelectedIndex = 0;
                if (comboPline.Value != null)
                {
                    GV_Depot = comboPline.Value.ToString();
                }
                if (comboPline.Value.ToString() == "L")//161108add begin
                {
                    Label13.Visible = true;
                    comboJCJ.Visible = true;
                }
                else
                {
                    Label13.Visible = false; ;
                    comboJCJ.Visible = false;
                }//161108add end
                //删除临时表数据161108add
                string m_Sql = "delete from dp_rckwcbtemp where MachineName='" + GV_MachineName + "' ";//and gzdd='" + GV_Depot + "'
                dc.ExeSql(m_Sql);
            }

        }
        
        private void GetTemplsh2()
        {
            string m_SqlStr = "select * from dp_rckwcb where rc='出库' and BatchId='" + v_CurrentBatchId + "' order by ghtm,rkdate";
            
            DataTable dt = dc.GetTable(m_SqlStr);
            fpSpDetail2.DataSource = dt;
            fpSpDetail2.DataBind();

            string m_SqlStr2 = "SELECT so,COUNT(*) as sl FROM dp_rckwcb  where rc='出库' and BatchId='" + v_CurrentBatchId + "'  GROUP BY so order by So";
            DataTable dt1 = dc.GetTable(m_SqlStr);
            fpSpCal2.DataSource = dt1;
            fpSpCal2.DataBind();
      
        }
        //初始化临时表清单,统计表
        private void setCondition()
        {
            string sql99 = "select SO,GHTM,GZRQ,YGMC,RC,RKLX,SOURCEPLACE,DESTINATION,BATCHID,CH,RKDATE,MACHINENAME,GZDD,JHDM from dp_rckwcbtemp where MachineName='" + GV_MachineName + "' and gzdd='" + GV_Depot + "'  order by Ghtm,rkdate";//and batchid like '" + v_CurrentBatchId + "'
            DataTable dt = dc.GetTable(sql99);
            fpSpDetail.DataSource = dt;
            fpSpDetail.DataBind();

            string m_SqlStr = "SELECT so,COUNT(*) as sl FROM dp_rckwcbtemp  where MachineName='" + GV_MachineName + "' and gzdd='" + GV_Depot + "' and BatchId like '" + v_CurrentBatchId + "'  GROUP BY so order by So";
            DataTable dt2 = dc.GetTable(m_SqlStr);
            fpSpCal.DataSource = dt2;
            fpSpCal.DataBind();

        }
        //初始化批次号选择
        private void initBatch()
        {
            string m_SqlStr = "select '全部批次' as BatchId from dual union select distinct BatchId from dp_rckwcbtemp where MachineName='" + GV_MachineName + "' and gzdd='" + GV_Depot + "' order by BatchId";
            comboBatch.DataSource = dc.GetTable(m_SqlStr);
            comboBatch.TextField = "BatchId";
            comboBatch.ValueField = "BatchId";
            comboBatch.DataBind();

        }
        protected void comboBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBatch.Value != null)
            {
                v_CurrentBatchId = comboBatch.Value.ToString();
            }
            else
            {
                v_CurrentBatchId = "%";
            }
            if (v_CurrentBatchId == "全部批次")
            {
                v_CurrentBatchId = "%";
            }
            setCondition();
            GetTemplsh2();
        }
        protected void ButRead_Click1(object sender, EventArgs e)
        {
            //直接读取出字符串
            try
            {
                string GV_Depot = "";
                string GV_DepotName = "";

                if (comboPline.SelectedIndex < 0)//判断生产线是否为空
                {
                    return;
                }
                //根据选择生产线（仓库代码）获得仓库名称
                if (comboPline.Value != null)
                {
                    GV_Depot = comboPline.Value.ToString();
                    string chSql = "SELECT depotname FROM DP_dmdepot where depotid='" + GV_Depot + "'";
                    dataConn chDc = new dataConn(chSql);
                    DataTable chDt = chDc.GetTable();
                    if (chDt.Rows.Count > 0)
                    {
                        GV_DepotName = chDt.Rows[0]["depotname"].ToString();
                    }
                }
                //删除临时表数据
                string m_Sql = "delete from dp_rckwcbtemp where MachineName='" + GV_MachineName + "' and gzdd='" + GV_Depot + "'";
                dc.ExeSql(m_Sql);

                string path = File2.Value;//上传文件路径
                if (path == "" && !path.ToUpper().Contains(".TXT"))
                {
                    ButRead.Enabled = true;
                    return;
                }
                string uploadPath = Server.MapPath(Request.ApplicationPath); //上传目录及文件名
                string fileName = "RK"+ DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";

                //if (!Directory.Exists("C:\\inetpub\\wwwroot\\webapp\\Rmes\\File\\excel"))
                //{
                //    Directory.CreateDirectory("C:\\inetpub\\wwwroot\\webapp\\Rmes\\File\\excel");
                //}
                uploadPath = uploadPath + "\\Rmes\\File\\excel\\" + fileName; //得到上传目录及文件名称  
                File2.PostedFile.SaveAs(uploadPath);       //上传文件到服务器  


                //string text = System.IO.File.ReadAllText(uploadPath);//@"D:\DT900\UP\733.txt"
                //Console.WriteLine(text);

                

                string m_SO = "";
                string m_BatchId = "";
                string m_Ch = "";
                string m_Jhdm = "";
                string m_Lxmc = "";
                string m_SourcePlace = "";
                string m_Destination = "";

                
                ////员工&类型
                //if (text.Length < 10)
                //{
                //    //员工
                //    if (text.Substring(0, 2).ToString() == "YG")
                //    {
                //        string m_Ygdm = text.Substring(0, 5).ToString();
                //        string m_Ygmc = "";
                //        string m_Sql1 = "select ygmc from dp_ygb where ygdm='" + m_Ygdm + "'";
                //        DataTable dt1 = dc.GetTable(m_Sql1);
                //        if (dt1.Rows.Count > 0)
                //        {
                //            m_Ygmc = dt1.Rows[0]["ygmc"].ToString();
                //        }
                //    }
                //    //类型
                //    if (text.Substring(0, 1).ToString() == "I")
                //    {
                //        string m_lxdm = text.Substring(0, 4).ToString();
                //        string m_lxmc = "";
                //        string m_Sql2 = "select lxmc from dp_rklx where lxdm='" + m_lxdm + "'";
                //        DataTable dt2 = dc.GetTable(m_Sql2);
                //        if (dt2.Rows.Count > 0)
                //        {
                //            m_lxmc = dt2.Rows[0]["lxmc"].ToString();
                //        }
                //    }
                //}
                //流水号
                //if (text.Length > 10)
                //{
                    //按行读取为字符串数组
                    string[] lines = System.IO.File.ReadAllLines(uploadPath);//@"D:\DT900\UP\733.txt"
                    string ch = lines[0].Trim();

                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i].ToString().Trim() == "")
                            continue;
                        if (lines[i].Length < 10)
                        {
                            //员工
                            if (lines[i].Substring(0, 2).ToString() == "YG")
                            {
                                string m_Ygdm = lines[i].Substring(0, 5).ToString();
                                string m_Ygmc = "";
                                string m_Sql1 = "select ygmc from dp_ygb where ygdm='" + m_Ygdm + "'";
                                DataTable dt1 = dc.GetTable(m_Sql1);
                                if (dt1.Rows.Count > 0)
                                {
                                    m_Ygmc = dt1.Rows[0]["ygmc"].ToString();
                                }
                            }
                            //类型
                            if (lines[i].Substring(0, 1).ToString() == "I")
                            {
                                string m_lxdm = lines[i].Substring(0, 4).ToString();
                                string m_lxmc = "";
                                string m_Sql2 = "select lxmc from dp_rklx where lxdm='" + m_lxdm + "'";
                                DataTable dt2 = dc.GetTable(m_Sql2);
                                if (dt2.Rows.Count > 0)
                                {
                                    m_lxmc = dt2.Rows[0]["lxmc"].ToString();
                                }
                            }
                        }
                        //流水号
                        if (lines[i].Length > 10)
                        {
                            string sn = lines[i].Substring(0, 8).ToString();
                            string onlinesite = "";
                            try
                            {
                                onlinesite = dc.GetValue("select pline_code from data_product where sn='" + sn + "' and rownum=1");
                            }
                            catch
                            { }
                            if ((GV_Depot=="E" ||GV_Depot=="W"||GV_Depot=="Z" ) && onlinesite != GV_Depot)
                            {
                                continue;
                            }
                            string m_RkDate = lines[i].Substring(lines[i].Length - 14, 14);
                            DateTime dt = DateTime.ParseExact(m_RkDate, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);

                            //---------------------由出库至入库情况---------------------------------------
                            string sql21 = "Select * from dp_rckwcb where Rc='出库' and ghtm='" + sn + "' and destination='" + GV_DepotName + "' order by gzrq desc";
                            DataTable dt21 = dc.GetTable(sql21);
                            if (dt21.Rows.Count > 0)
                            {
                                m_SO = dt21.Rows[0]["SO"].ToString();
                                m_BatchId = dt21.Rows[0]["BATCHID"].ToString();
                                m_Ch = dt21.Rows[0]["CH"].ToString();
                                m_Jhdm = dt21.Rows[0]["JHDM"].ToString();
                                m_Lxmc = dt21.Rows[0]["RKLX"].ToString();
                                m_SourcePlace = dt21.Rows[0]["SOURCEPLACE"].ToString();


                                //由上一站点出库类型得出此站点入库类型
                                string sql22 = "select * from dp_rckmap where cklx='" + m_Lxmc + "'";
                                DataTable dt22 = dc.GetTable(sql22);
                                if (dt22.Rows.Count > 0)
                                {
                                    m_Lxmc = dt22.Rows[0]["RKLX"].ToString();
                                }

                                if (GV_Depot == "E" || GV_Depot == "W")
                                //从atpu中查询入出库类型
                                {
                                    string sql3 = "select c.* from data_product a,data_plan b,dp_atpukcmap c where a.PLAN_CODE=b.PLAN_CODE and a.SN='" + sn + "' and b.PLAN_TYPE=c.atpujhlx and rc='入库'";
                                    DataTable dt3 = dc.GetTable(sql3);
                                    if (dt3.Rows.Count > 0)
                                    {
                                        m_Lxmc = dt3.Rows[0]["RKLX"].ToString();
                                    }
                                }
                            }
                            else
                            {
                                if (GV_Depot == "A")
                                {
                                    m_Lxmc = " ";
                                    m_SourcePlace = "客户";
                                }
                                else
                                {
                                    m_Lxmc = "正常入库";
                                    m_SourcePlace = "ATPU";
                                    if (GV_Depot == "L")
                                    {
                                        if (comboJCJ.Text == "是")
                                        {
                                            m_SourcePlace = "DCEC";
                                        }
                                        else
                                        {
                                            m_SourcePlace = "柳汽U";
                                        }
                                    }
                                    //从atpu中查询入出库类型
                                    string sql4 = "select c.* from data_product a,data_plan b,dp_atpukcmap c where a.PLAN_CODE=b.PLAN_CODE and a.SN='" + sn + "' and b.PLAN_TYPE=c.atpujhlx and rc='入库'";
                                    DataTable dt4 = dc.GetTable(sql4);
                                    if (dt4.Rows.Count > 0)
                                    {
                                        m_Lxmc = dt4.Rows[0]["RKLX"].ToString();
                                    }
                                }

                                string sql5 = "Select PLAN_SO,PLAN_CODE from DATA_PRODUCT where sn='" + sn + "'";
                                DataTable dt5 = dc.GetTable(sql5);
                                if (dt5.Rows.Count > 0)
                                {
                                    m_SO = dt5.Rows[0]["PLAN_SO"].ToString();
                                    m_Jhdm = dt5.Rows[0]["PLAN_CODE"].ToString();
                                }
                                ////最后查询一遍计划代码，保证计划代码不为空
                                //string sql6 = "Select PLAN_SO,PLAN_CODE from DATA_PRODUCT where sn='" + sn + "'";
                                //dataConn dc6 = new dataConn(sql6);
                                //DataTable dt6 = dc6.GetTable();
                                //if (dt6.Rows.Count > 0)
                                //{
                                //    m_SO = dt6.Rows[0]["PLAN_SO"].ToString();
                                //    m_Jhdm = dt6.Rows[0]["PLAN_CODE"].ToString();
                                //}
                            }
                            //写入临时表
                            if (string.IsNullOrEmpty(m_BatchId))
                            {
                                m_BatchId = " ";
                            }
                            if (string.IsNullOrEmpty(m_Ch))
                            {
                                m_Ch = " ";
                            }

                            m_Destination = GV_DepotName;
                            if (GV_Depot == "L")
                                m_Destination = "柳器总成库";

                            //liuqi
                            if (comboJCJ.Text == "是")
                                m_Lxmc = "柳汽收货入库";

                            string inSql = "insert into dp_rckwcbtemp(ghtm,so,jhdm,ygmc,rklx,rkdate,gzrq,gzdd,rc,SourcePlace,destination,BatchId,Ch,MachineName) "
                                + "values('" + sn + "','" + m_SO + "','" + m_Jhdm + "','" + theUserName + "','" + m_Lxmc + "','" + m_RkDate + "',to_char(sysdate,'yyyy-mm-dd hh24:mi:ss'),"
                                + "'" + GV_Depot + "','入库','" + m_SourcePlace + "','" + m_Destination + "','" + m_BatchId + "','" + m_Ch + "','" + GV_MachineName + "')";
                            dc.ExeSql(inSql);
                        }
                    }
                //}
                //删除txt文件20160920
                //System.IO.File.Delete(uploadPath);

                setCondition();
            }
            catch
            {
                setCondition();
            }
        }

        protected void fpSpDetail_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.VisibleIndex < 0) return;
            string m_Ghtm = e.GetValue("GHTM").ToString();
            string m_Lxmc = e.GetValue("RKLX").ToString();
            string m_RcTemp, m_GzddTemp, m_RklxTemp;

            //-------------------------------重复入库提示---------------------------
            string m_SqlStr1 = "SELECT * FROM dp_rckwcb where ghtm='" + m_Ghtm + "' order by gzrq desc ";
            DataTable chDt1 = dc.GetTable(m_SqlStr1);
            if (chDt1.Rows.Count > 0)
            {
                m_RcTemp = chDt1.Rows[0]["rc"].ToString();
                m_GzddTemp = chDt1.Rows[0]["GZDD"].ToString();

                //---------------如果最后一次操作是在当前站点并且是入库,提示重复入库---------------
                if (m_RcTemp == "入库" && m_GzddTemp == GV_Depot)
                {
                    e.Row.BackColor = System.Drawing.Color.Yellow;
                }
                else
                {
                    //-----------------------------如果连续两次在同一地点正常入库，提示重复入库----------------
                    string m_SqlStr2 = "SELECT * FROM dp_rckwcb where ghtm='" + m_Ghtm + "' and rc='入库' order by gzrq ";
                    DataTable chDt2 = dc.GetTable(m_SqlStr2);
                    if (chDt2.Rows.Count > 0)
                    {
                        m_RklxTemp = chDt2.Rows[0]["rklx"].ToString();
                        m_GzddTemp = chDt2.Rows[0]["GZDD"].ToString();
                        if (m_GzddTemp == GV_Depot && m_RklxTemp == "正常入库" && m_Lxmc == "正常入库" && m_GzddTemp == GV_Depot)
                        {
                            e.Row.BackColor = System.Drawing.Color.Yellow;
                        }
                    }
                }
            }
            //重复扫描的记录背景色为红色
            string sql10 = "select count(*) as count1 from dp_rckwcbtemp where MachineName='" + GV_MachineName + "' and batchid like '" + v_CurrentBatchId + "' and gzdd='" + GV_Depot + "' and Ghtm='" + m_Ghtm + "'  ";
            DataTable dt21 = dc.GetTable(sql10);
            if (Convert.ToInt32(dt21.Rows[0]["count1"].ToString()) > 1)
            {
                e.Row.BackColor = System.Drawing.Color.Red;
            }


            ////不一致数据背景色为蓝色
            //string m_SqlStrC = "select count(*) as count1 from dp_rckwcb where rc='出库'  and batchid like '" + v_CurrentBatchId + "' and gzdd='" + GV_Depot + "' and Ghtm='" + m_Ghtm + "'";
            //DataTable chDtC = dc.GetTable(m_SqlStrC);

            //if (Convert.ToInt32(chDtC.Rows[0]["count1"].ToString()) > 0)
            //{
            //    theVerifyFlag = false;
            //    e.Row.BackColor = System.Drawing.Color.Blue;
            //}
           
            //初始化批次号选择
            initBatch();
            GetTemplsh2();

        }

        protected void comboRKLX_SelectedIndexChanged(object sender, EventArgs e)
        {
            string m_rklxmc = "";
            if (comboRKLX.Value != null)
            {
                m_rklxmc = comboRKLX.Value.ToString();
            }
            string m_Sql = "update dp_rckwcbtemp set rklx='" + m_rklxmc + "' where BatchId like '" + v_CurrentBatchId + "' and MachineName='" + GV_MachineName + "' and gzdd='" + GV_Depot + "'";
            dc.ExeSql(m_Sql);

            setCondition();
        }

        protected void comboUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            string m_Ygmc = comboUser.Value.ToString();
            string m_Sql = "update dp_rckwcbtemp set ygmc='" + m_Ygmc + "' where BatchId like '" + v_CurrentBatchId + "' and MachineName='" + GV_MachineName + "' and gzdd='" + GV_Depot + "'";
            dc.ExeSql(m_Sql);

            setCondition();
        }
        //删除重复扫描数据
        protected void Cmd_Del1_Click(object sender, EventArgs e)
        {
            //string m_SqlStr = "select * from dp_rckwcbtemp where MachineName='" + GV_MachineName + "' and batchid like '" + v_CurrentBatchId + "' and gzdd='" + GV_Depot + "' order by Ghtm,rkdate";// 
            //DataTable chDt = dc.GetTable(m_SqlStr);
            //for (int i = 0; i < chDt.Rows.Count; i++)
            //{
            //    string m_Ghtm = chDt.Rows[i]["Ghtm"].ToString();
            //    string m_RkDate = chDt.Rows[i]["RKDATE"].ToString();
                
            //    string sql10 = "select count(*) as count1 from dp_rckwcbtemp where MachineName='" + GV_MachineName + "' and batchid like '" + v_CurrentBatchId + "' and gzdd='" + GV_Depot + "' and Ghtm='" + m_Ghtm + "'  ";
            //    DataTable dt21 = dc.GetTable(sql10);
            //    if (Convert.ToInt32(dt21.Rows[0]["count1"].ToString()) > 1)
            //    {
            //        string m_Sql = "delete from dp_rckwcbtemp where ghtm='" + m_Ghtm + "' and rkdate='" + m_RkDate + "' and MachineName='" + GV_MachineName + "' and gzdd='" + GV_Depot + "'";
            //        dc.ExeSql(m_Sql);
            //    }
                    
                
            //}

            //setCondition();
        }
        //删除重复入库数据
        protected void Cmd_Del2_Click(object sender, EventArgs e)
        {
            //string m_Ghtm = "";
            //string m_Lxmc = "";
            //string m_RkDate = "";
            //string m_RcTemp, m_GzddTemp, m_RklxTemp, delSql;
            //string m_SqlStr = "select * from dp_rckwcbtemp where MachineName='" + GV_MachineName + "' and batchid like '" + v_CurrentBatchId + "' and gzdd='" + GV_Depot + "' order by Ghtm,rkdate";// 
            //DataTable chDt = dc.GetTable(m_SqlStr);
            //for (int i = 0; i < chDt.Rows.Count; i++)
            //{
            //    m_Ghtm = chDt.Rows[i]["Ghtm"].ToString();
            //    m_Lxmc = chDt.Rows[i]["rklx"].ToString();
            //    m_RkDate = chDt.Rows[i]["rkdate"].ToString();

            //    string m_SqlStr1 = "SELECT * FROM dp_rckwcb where ghtm='" + m_Ghtm + "' order by gzrq desc ";
            //    DataTable chDt1 = dc.GetTable(m_SqlStr1);
            //    if (chDt1.Rows.Count > 0)
            //    {
            //        m_RcTemp = chDt1.Rows[0]["rc"].ToString();
            //        m_GzddTemp = chDt1.Rows[0]["GZDD"].ToString();

            //        //---------------如果最后一次操作是在当前站点并且是入库,删除重复入库---------------
            //        if (m_RcTemp == "入库" && m_GzddTemp == GV_Depot)
            //        {
            //            delSql = "delete from dp_rckwcbtemp where ghtm='" + m_Ghtm + "' and rkdate='" + m_RkDate + "' and MachineName='" + GV_MachineName + "' and gzdd='" + GV_Depot + "'";
            //            dc.ExeSql(delSql);
            //        }
            //        else
            //        {
            //            //-----------------------------如果连续两次在同一地点正常入库，删除重复入库----------------
            //            string m_SqlStr2 = "SELECT * FROM dp_rckwcb where ghtm='" + m_Ghtm + "' and rc='入库' order by gzrq ";
            //            DataTable chDt2 = dc.GetTable(m_SqlStr2);
            //            if (chDt2.Rows.Count > 0)
            //            {
            //                m_RklxTemp = chDt2.Rows[0]["rklx"].ToString();
            //                m_GzddTemp = chDt2.Rows[0]["GZDD"].ToString();
            //                if (m_GzddTemp == GV_Depot && m_RklxTemp == "正常入库" && m_Lxmc == "正常入库" && m_GzddTemp == GV_Depot)
            //                {
            //                    delSql = "delete from dp_rckwcbtemp where ghtm='" + m_Ghtm + "' and rkdate='" + m_RkDate + "' and MachineName='" + GV_MachineName + "' and gzdd='" + GV_Depot + "'";
            //                    dc.ExeSql(delSql);
            //                }
            //            }
            //        }
            //    }
            //}
            
            //setCondition();
        }

        protected void ButInputSure_Click(object sender, EventArgs e)
        {
            string m_Ghtm = "";
            
            //先判断是否有重复扫描和重复入库的记录


            string m_Sql = "insert into dp_rckwcb(rmes_id,ghtm,so,jhdm,ygmc,rklx,rkdate,gzrq,gzdd,rc,sourceplace,destination,batchId,ch) select SEQ_RMES_ID.NEXTVAL,ghtm,so,jhdm,ygmc,rklx,rkdate,to_char(to_date(gzrq,'yyyy-mm-dd hh24:mi:ss'),'yyyy-mm-dd hh24:mi:ss'),gzdd,rc,sourceplace,destination,batchId,ch from dp_rckwcbtemp where BatchId like'" + v_CurrentBatchId + "' and MachineName='" + GV_MachineName + "' and gzdd='" + GV_Depot + "'";
            dc.ExeSql(m_Sql);

            string m_sql1 = "insert into dp_rkwcb(ghtm,so,jhdm,ygmc,gzrq,rklx,rkdate,gzdd) select ghtm,so,jhdm,ygmc,to_date(gzrq,'yyyy-mm-dd hh24:mi:ss'),rklx,rkdate,gzdd from dp_rckwcbtemp where BatchId like'" + v_CurrentBatchId + "' and MachineName='" + GV_MachineName + "' and gzdd='" + GV_Depot + "'";
            dc.ExeSql(m_sql1);

            //'liuqi
            string inSql = "";
            string lq = "";
            if (comboJCJ.Value != null)
            {
                lq = comboJCJ.Value.ToString();
            }
            if (lq == "是")
            {
                inSql = "insert into dp_kcb(rmes_id,ghtm,so,ygmc,rklx,rkdate,gzrq,gzdd,sourceplace,batchId,ch,lq_flag) select SEQ_RMES_ID.NEXTVAL,ghtm,so,ygmc,rklx,rkdate,gzrq,gzdd,sourceplace,batchId,ch,'Y' from dp_rckwcbtemp where BatchId like'" + v_CurrentBatchId + "' and MachineName='" + GV_MachineName + "' and gzdd='" + GV_Depot + "'";
            }
            else
            {
                inSql = "insert into dp_kcb(rmes_id,ghtm,so,ygmc,rklx,rkdate,gzrq,gzdd,sourceplace,batchId,ch) select SEQ_RMES_ID.NEXTVAL,ghtm,so,ygmc,rklx,rkdate,gzrq,gzdd,sourceplace,batchId,ch from dp_rckwcbtemp where BatchId like'" + v_CurrentBatchId + "' and MachineName='" + GV_MachineName + "' and gzdd='" + GV_Depot + "'";
            }
            dc.ExeSql(inSql);

            //so分布 入库时删除储备表信息
            string delSql = "delete from data_store where SN in (select ghtm from dp_rckwcbtemp where BatchId like'" + v_CurrentBatchId + "' and MachineName='" + GV_MachineName + "' and gzdd='" + GV_Depot + "')";
            dc.ExeSql(delSql);
            //删除临时表
            string delSql1 = "delete from dp_rckwcbtemp where BatchId like '" + v_CurrentBatchId + "' and MachineName='" + GV_MachineName + "' and gzdd='" + GV_Depot + "'";
            dc.ExeSql(delSql1);

            //删除dp_kcb和dp_willrckwcb
            string sql = "select * from dp_willrckwcb where rc='出库' and gzdd='" + GV_Depot + "' order by ghtm";
            dataConn chDc2 = new dataConn(sql);
            DataTable chDt2 = chDc2.GetTable();
            if (chDt2.Rows.Count > 0)
            {
                m_Ghtm = chDt2.Rows[0]["Ghtm"].ToString();
            }
            string sql3 = "select * from dp_kcb where ghtm='" + m_Ghtm + "' and gzdd='" + GV_Depot + "'";
            dataConn chDc3 = new dataConn(sql3);
            DataTable chDt3 = chDc3.GetTable();
            if (chDt3.Rows.Count > 0)
            {
                string dSql = "delete from dp_kcb where ghtm='" + m_Ghtm + "' and gzdd='" + GV_Depot + "'";
                dc.ExeSql(dSql);
                string dSql1 = "delete from dp_willrckwcb where ghtm='" + m_Ghtm + "' and rc='出库' and gzdd='" + GV_Depot + "'";
                dc.ExeSql(dSql1);
            }

            setCondition();

        }
        
        public void Del_TextFile(String FilePath )
        {
            if (theVerifyFlag == false)
            {
                Show(this, "扫描纪录与出库单不一致!");
                return;
            }
            else
            {
                Show(this, "成功通过验证!");
            }
        }

        protected void fpSpDetail2_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.VisibleIndex < 0) return;
            string m_Ghtm = e.GetValue("GHTM").ToString();

            string m_SqlStr = "select * from dp_rckwcbtemp where MachineName='" + GV_MachineName + "' and batchid like '" + v_CurrentBatchId + "' and gzdd='" + GV_Depot + "' and ghtm='" + m_Ghtm + "'";// 
            DataTable chDtC = dc.GetTable(m_SqlStr);

            if (Convert.ToInt32(chDtC.Rows[0]["count1"].ToString()) > 0)
            {
                theVerifyFlag = false;
                e.Row.BackColor = System.Drawing.Color.Blue;
            }
        }

        protected void fpSpDetail_BeforeColumnSortingGrouping(object sender, ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            setCondition();
        }
        //批次校验
        protected void ButBatchCheck_Click(object sender, EventArgs e)
        {

        }
        protected void ASPxCallbackPanel6_Callback(object sender, CallbackEventArgsBase e)
        { 
        
        }

        protected void ASPxButton_Import_Click(object sender, EventArgs e)
        {
            try
            {
                ASPxButton_Import.Enabled = false;
                string path = File1.Value;//上传文件路径
                if (path == "")
                {
                    ASPxButton_Import.Enabled = true;
                    return;
                }
                string uploadPath = Server.MapPath(Request.ApplicationPath); //上传目录及文件名
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

                if (!Directory.Exists("C:\\inetpub\\wwwroot\\webapp\\Rmes\\File\\excel"))
                {
                    Directory.CreateDirectory("C:\\inetpub\\wwwroot\\webapp\\Rmes\\File\\excel");
                }
                uploadPath = uploadPath + "\\Rmes\\File\\excel\\" + fileName; //得到上传目录及文件名称  
                File1.PostedFile.SaveAs(uploadPath);       //上传文件到服务器  

                DataTable dt = GetExcelDataTable(uploadPath);
                 int count = 0;
                 foreach (DataRow t in dt.Rows)
                 {
                     if (t.IsNull(0)) continue;
                     string m_SO = t[0].ToString().Trim();
                     string m_Ghtm = t[1].ToString().Trim();
                     string m_Jhdm = t[2].ToString().Trim();
                     string m_Gzrq = t[3].ToString().Trim();
                     string m_Gzdd = t[4].ToString().Trim();
                     string m_Ygmc = t[5].ToString().Trim();
                     string m_Rc = t[6].ToString().Trim();
                     string m_Lxmc = t[7].ToString().Trim();
                     string m_SourcePlace = t[8].ToString().Trim();
                     string m_Destination = t[9].ToString().Trim();
                     string m_BatchId = t[10].ToString().Trim();
                     string m_Ch = t[11].ToString().Trim();
                     string m_RkDate = t[12].ToString().Trim();

                     if (string.IsNullOrEmpty(m_Rc))
                     {
                         m_Rc = "出库";
                     }
                     if (string.IsNullOrEmpty(m_Gzdd))
                     {
                         string SQlstr = "select * from dp_dmdepot where depotname='" + m_SourcePlace + ",";
                         DataTable dtP = dc.GetTable(SQlstr);
                         if (dtP.Rows.Count > 0)
                         {
                             m_Gzdd = dtP.Rows[0]["depotid"].ToString();
                         }
                     }
                     string SQlch = "select * from dp_rckwcb where ghtm='" + m_Ghtm + "' and gzrq='" + m_Gzrq + "' and gzdd='" + m_Gzdd + "'";
                     DataTable dtCh = dc.GetTable(SQlch);
                     if (dtCh.Rows.Count <= 0)
                     {
                         string inSql = "insert into dp_rckwcb(ghtm,so,ygmc,rklx,rkdate,gzrq,gzdd,rc,SourcePlace,destination,BatchId,Ch,jhdm,rmes_id) "
                                      + "values('" + m_Ghtm + "','" + m_SO + "','" + m_Ygmc + "','" + m_Lxmc + "','" + m_RkDate + "','" + m_Gzrq + "',"
                                      + "'" + m_Gzdd + "','" + m_Rc + "','" + m_SourcePlace + "','" + m_Destination + "','" + m_BatchId + "',"
                                      + "'" + m_Ch + "','" + m_Jhdm + "',SEQ_RMES_ID.NEXTVAL)";
                         dc.ExeSql(inSql);
                     }
                     else
                     {
                         //ASPxCallbackPanel6.JSProperties.Add("cpCallbackName", "Fail");
                         //ASPxCallbackPanel6.JSProperties.Add("cpCallbackRet", "有重复数据");
                         Show(this, m_Ghtm+":" + "重复数据");
                         ASPxButton_Import.Enabled = true;
                         return;
                     }

                 }
                 ASPxButton_Import.Enabled = true;
                 setCondition();
                 return;
            }
            catch (Exception ex)
            {
                ASPxCallbackPanel6.JSProperties.Add("cpCallbackName", "Fail");
                ASPxCallbackPanel6.JSProperties.Add("cpCallbackRet", ex.Message);
                ASPxButton_Import.Enabled = true;
                return;
            }
        }
        ///<summary>
        ///根据excel路径和sheet名称，返回excel的DataTable
        ///</summary>
        public static DataTable GetExcelDataTable(string path)
        {
            /*Office 2007*/
            string ace = "Microsoft.ACE.OLEDB.12.0";
            /*Office 97 - 2003*/
            string jet = "Microsoft.Jet.OLEDB.4.0";
            string xl2007 = "Excel 12.0 Xml";
            string xl2003 = "Excel 8.0";
            string imex = "IMEX=1";
            /* csv */
            string text = "text";
            string fmt = "FMT=Delimited";
            string hdr = "Yes";
            string conn = "Provider={0};Data Source={1};Extended Properties=\"{2};HDR={3};{4}\";";

            //string select = sql;
            string ext = Path.GetExtension(path);
            OleDbDataAdapter oda;
            DataTable dt = new DataTable("data");
            switch (ext.ToLower())
            {
                case ".xlsx":
                    conn = String.Format(conn, ace, Path.GetFullPath(path), xl2007, hdr, imex);
                    break;
                case ".xls":
                    conn = String.Format(conn, jet, Path.GetFullPath(path), xl2003, hdr, imex);
                    break;
                case ".csv":
                    conn = String.Format(conn, jet, Path.GetDirectoryName(path), text, hdr, fmt);
                    //sheet = Path.GetFileName(path);
                    break;
                default:
                    throw new Exception("File Not Supported!");
            }
            OleDbConnection con = new OleDbConnection(conn);
            con.Open();
            //select = string.Format(select, sql);

            DataTable dtbl1 = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            //dataGridView2.DataSource = dtbl1;   
            string strSheetName = dtbl1.Rows[0][2].ToString().Trim();

            string select = string.Format("SELECT * FROM [{0}]", strSheetName);

            oda = new OleDbDataAdapter(select, con);
            oda.Fill(dt);
            con.Close();
            return dt;
        }
        public static void Show(System.Web.UI.Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + msg.ToString() + "');</script>");
        }

        protected void fpSpDetail_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            setCondition();
            fpSpDetail.Selection.UnselectAll();
        }

        protected void fpSpDetail_CustomDataCallback(object sender, ASPxGridViewCustomDataCallbackEventArgs e)
        {
            string s = e.Parameters;
            string[] s1 = s.Split('|');
            string type1 = s1[0];
            int rowIndex = int.Parse(s1[1]);
            string sql = "";
            
            ASPxGridView atl1 = (ASPxGridView)sender;
            int count1 = atl1.Selection.Count;
            List<object> m_Ghtm = atl1.GetSelectedFieldValues("GHTM");
            List<object> m_RkDate = atl1.GetSelectedFieldValues("RKDATE");
            switch (type1)
            {
                case "Delete":
                    
                    for (int i = 0; i < count1; i++)
                    {
                        sql = "delete from dp_rckwcbtemp where ghtm='" + m_Ghtm[i] + "' and rkdate='" + m_RkDate[i] + "' and MachineName='" + GV_MachineName + "' and gzdd='" + GV_Depot + "'";
                        dc.ExeSql(sql);
                    }
                    e.Result = "OK,已删除！";
                    break;
                default:

                    break;
            }
        }
        protected void ASPxCbSubmit_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
        {
            string collection = e.Parameter;
            if (collection == "DeleteScan")
            {
                string m_SqlStr = "select * from dp_rckwcbtemp where MachineName='" + GV_MachineName + "' and batchid like '" + v_CurrentBatchId + "' and gzdd='" + GV_Depot + "' order by Ghtm,rkdate";// 
                DataTable chDt = dc.GetTable(m_SqlStr);
                for (int i = 0; i < chDt.Rows.Count; i++)
                {
                    string m_Ghtm = chDt.Rows[i]["Ghtm"].ToString();
                    string m_RkDate = chDt.Rows[i]["RKDATE"].ToString();

                    string sql10 = "select count(*) as count1 from dp_rckwcbtemp where MachineName='" + GV_MachineName + "' and batchid like '" + v_CurrentBatchId + "' and gzdd='" + GV_Depot + "' and Ghtm='" + m_Ghtm + "'  ";
                    DataTable dt21 = dc.GetTable(sql10);
                    if (Convert.ToInt32(dt21.Rows[0]["count1"].ToString()) > 1)
                    {
                        string m_Sql = "delete from dp_rckwcbtemp where ghtm='" + m_Ghtm + "' and rkdate='" + m_RkDate + "' and MachineName='" + GV_MachineName + "' and gzdd='" + GV_Depot + "'";
                        dc.ExeSql(m_Sql);
                    }
                    
                }
                e.Result = "OK,重复扫描数据删除成功！";
                return;
                //setCondition();
            }
            if (collection == "DeleteCFRK")
            {
                string m_Ghtm = "";
                string m_Lxmc = "";
                string m_RkDate = "";
                string m_RcTemp, m_GzddTemp, m_RklxTemp, delSql;
                string m_SqlStr = "select * from dp_rckwcbtemp where MachineName='" + GV_MachineName + "' and batchid like '" + v_CurrentBatchId + "' and gzdd='" + GV_Depot + "' order by Ghtm,rkdate";// 
                DataTable chDt = dc.GetTable(m_SqlStr);
                for (int i = 0; i < chDt.Rows.Count; i++)
                {
                    m_Ghtm = chDt.Rows[i]["Ghtm"].ToString();
                    m_Lxmc = chDt.Rows[i]["rklx"].ToString();
                    m_RkDate = chDt.Rows[i]["rkdate"].ToString();

                    string m_SqlStr1 = "SELECT * FROM dp_rckwcb where ghtm='" + m_Ghtm + "' order by gzrq desc ";
                    DataTable chDt1 = dc.GetTable(m_SqlStr1);
                    if (chDt1.Rows.Count > 0)
                    {
                        m_RcTemp = chDt1.Rows[0]["rc"].ToString();
                        m_GzddTemp = chDt1.Rows[0]["GZDD"].ToString();

                        //---------------如果最后一次操作是在当前站点并且是入库,删除重复入库---------------
                        if (m_RcTemp == "入库" && m_GzddTemp == GV_Depot)
                        {
                            delSql = "delete from dp_rckwcbtemp where ghtm='" + m_Ghtm + "' and rkdate='" + m_RkDate + "' and MachineName='" + GV_MachineName + "' and gzdd='" + GV_Depot + "'";
                            dc.ExeSql(delSql);
                        }
                        else
                        {
                            //-----------------------------如果连续两次在同一地点正常入库，删除重复入库----------------
                            string m_SqlStr2 = "SELECT * FROM dp_rckwcb where ghtm='" + m_Ghtm + "' and rc='入库' order by gzrq ";
                            DataTable chDt2 = dc.GetTable(m_SqlStr2);
                            if (chDt2.Rows.Count > 0)
                            {
                                m_RklxTemp = chDt2.Rows[0]["rklx"].ToString();
                                m_GzddTemp = chDt2.Rows[0]["GZDD"].ToString();
                                if (m_GzddTemp == GV_Depot && m_RklxTemp == "正常入库" && m_Lxmc == "正常入库" && m_GzddTemp == GV_Depot)
                                {
                                    delSql = "delete from dp_rckwcbtemp where ghtm='" + m_Ghtm + "' and rkdate='" + m_RkDate + "' and MachineName='" + GV_MachineName + "' and gzdd='" + GV_Depot + "'";
                                    dc.ExeSql(delSql);
                                }
                            }
                        }
                    }
                }

                e.Result = "OK,重复入库数据删除成功！";
                return;
            }
        }

        protected void fpSpDetail_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string sql = "update dp_Rckwcbtemp set rklx='" + e.NewValues["RKLX"].ToString() + "',SO='" + e.NewValues["SO"].ToString() + "' "
                       +"where ghtm='" + e.NewValues["GHTM"].ToString() + "' and rkdate='" + e.NewValues["RKDATE"].ToString() + "'";
            dc.ExeSql(sql);

            e.Cancel = true;
            fpSpDetail.CancelEdit();
            setCondition();
        }
    
    }
}