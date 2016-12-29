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
using Microsoft.Office.Interop.Excel;
using Rmes.WebApp.Rmes.File;
using System.Diagnostics;
//using System.Data.OracleClient;
using Rmes.DA.Procedures;


 /* 2016-09-04
 * 杨少霞 
 * 出库操作
 * */

namespace Rmes.WebApp.Rmes.Inv.inv1200
{
    public partial class inv1200 : BasePage
    {
        private dataConn dc = new dataConn();
        private PubCs thePubCs = new PubCs();
        private static string theCompanyCode, theUserId, theUserName;
        public string theProgramCode;
        public string theDepotName, GV_MachineName, LX, m_Ch,v_CurrentBatchId, v_DateStr;
        public string GV_Depot = "";
        public Boolean theVerifyFlag = true;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();
            theUserName = theUserManager.getUserName();

            theProgramCode = "inv1100";

            //TxtCh.Attributes.Add("onkeydown", "if(event.keyCode==13){document.all." + BtnHidden.ClientID + ".click();return   false}");

            v_CurrentBatchId = "%";
            CmbCh.Text = "全部车号";

            GV_MachineName = System.Net.Dns.GetHostName();
            if (comboPline.Value != null)
            {
                GV_Depot = comboPline.Value.ToString();
            }

            //根据登录人员去初始化生产线
            //string sqlNew = "select a.pline_code,b.rmes_id,b.pline_name from VW_USER_ROLE_PROGRAM a "
            //       + "left join code_product_line b on a.pline_code=b.pline_code "
            //       + "where a.COMPANY_CODE = '" + theCompanyCode + "' and a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' order by b.PLINE_NAME";
            string sqlNew = "select depotid pline_code,depotname pline_name from dp_dmdepot ";
            comboPline.DataSource = dc.GetTable(sqlNew);
            comboPline.DataBind();

            //初始化入库类型
            string SQlstr = "SELECT lxmc FROM DP_rklx where lxdm like 'O%' ORDER BY lxdm";
            CmbRklx.DataSource = dc.GetTable(SQlstr);
            CmbRklx.TextField = "LXMC";
            CmbRklx.ValueField = "LXMC";
            CmbRklx.DataBind();

            //初始化员工
            string sql = "SELECT ygmc FROM DP_ygb ORDER BY ygmc";
            CmbYgmc.DataSource = dc.GetTable(sql);
            CmbYgmc.TextField = "YGMC";
            CmbYgmc.ValueField = "YGMC";
            CmbYgmc.DataBind();

            //初始化方向
            string sqlFx = "SELECT fxmc FROM DP_dmfxb ORDER BY fxdm";
            CmbFx.DataSource = dc.GetTable(sqlFx);
            CmbFx.TextField = "FXMC";
            CmbFx.ValueField = "FXMC";
            CmbFx.DataBind();
            if (!IsPostBack)
            {
                SJFJQD.Visible = false;
            }
        }
        private void GetTemplsh()
        {
            string sql99 = "select SO,GHTM,GZRQ,YGMC,RC,RKLX,SOURCEPLACE,DESTINATION,BATCHID,CH,RKDATE,MACHINENAME,GZDD,JHDM,rownum "
                         +"from dp_rckwcbtemp "
                         + "where MachineName='" + GV_MachineName + "' and gzdd='" + GV_Depot + "' and batchid like '" + v_CurrentBatchId + "'  order by Ghtm,rkdate";
            System.Data.DataTable dt = dc.GetTable(sql99); 
            fpSpDetail.DataSource = dt;
            fpSpDetail.DataBind();

            string m_SqlStr = "SELECT BatchId,ch,destination,so,COUNT(*) as sl FROM dp_rckwcbtemp  "
                            +"where MachineName='" + GV_MachineName + "' and gzdd='" + GV_Depot + "' and BatchId like '" + v_CurrentBatchId + "'  "
                            +"GROUP BY BatchId,ch,destination,so order by so";
            System.Data.DataTable dtCal = dc.GetTable(m_SqlStr);

            fpSpCal.DataSource = dtCal;
            fpSpCal.DataBind();

            string m_SqlStrCh = "select distinct ch from dp_rckwcbtemp where MachineName='" + GV_MachineName + "' and gzdd='" + GV_Depot + "' order by ch";
            CmbCh.DataSource = dc.GetTable(m_SqlStrCh);
            CmbCh.TextField = "CH";
            CmbCh.ValueField = "CH";
            CmbCh.DataBind();

        }
        protected void CmbCh_SelectedIndexChanged(object sender, EventArgs e)
        {
            int m_BatchIdNum;
            string m_tempStr;
            v_DateStr = DateTime.Now.ToString("yyMMdd");
            if (CmbCh.Value != null)
            {
                m_Ch = CmbCh.Value.ToString();
            }
            else
            {
                m_Ch = "%";
            }
            if (m_Ch == "全部车号")
            {
                m_Ch = "%";
            }

            string m_Sql = "select count(*) as count1 from DP_day_BatchId where WorkDate='" + v_DateStr + "' and gzdd='" + GV_Depot + "'";
            System.Data.DataTable dt = dc.GetTable(m_Sql);
            if (dt.Rows.Count > 0)
            {
                m_BatchIdNum = Convert.ToInt32(dt.Rows[0]["count1"].ToString());

                m_tempStr = (m_BatchIdNum + 1).ToString();
                //m_tempStr = Space(3 - Len(m_tempStr)) + m_tempStr;         没理解vb程序的意思
                //m_tempStr = Replace(m_tempStr, " ", "0");                  没理解vb程序的意思
                v_CurrentBatchId = v_DateStr + GV_Depot + m_tempStr;

                string m_Sql1 = "update dp_rckwcbtemp set batchid='' where MachineName='" + GV_MachineName + "' and gzdd='" + GV_Depot + "'";
                dc.ExeSql(m_Sql1);

                string m_Sql2 = "update dp_rckwcbtemp set batchid='" + v_CurrentBatchId + "' where ch like '" + m_Ch + "' and MachineName='" + GV_MachineName + "' and gzdd='" + GV_Depot + "'";
                dc.ExeSql(m_Sql2);
            }

            GetTemplsh();
           
        }

        protected void fpSpDetail_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            
            if (e.VisibleIndex < 0) return;
            string m_Ghtm = e.GetValue("GHTM").ToString();
            string m_Lxmc = e.GetValue("RKLX").ToString();

            //库存中没有则背景色绿色显示
            string m_SqlStr = "SELECT * FROM dp_kcb where ghtm='" + m_Ghtm + "'";
            if (dc.GetState(m_SqlStr) == false)
            {
                e.Row.BackColor = System.Drawing.Color.LightGreen;
            }
            //重复出库要提示
            string m_SqlStr1 = "SELECT * FROM dp_rckwcb where ghtm='" + m_Ghtm + "' and gzdd='" + GV_Depot + "' order by rkdate desc";
            System.Data.DataTable dt = dc.GetTable(m_SqlStr1);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["rc"].ToString() == "出库")
                {
                    e.Row.BackColor = System.Drawing.Color.Yellow;
                }
                else
                {
                    string m_SqlStr2 = "SELECT * FROM dp_rckwcb where ghtm='" + m_Ghtm + "' and gzdd='" + GV_Depot + "' and rc='出库'";
                    if (dc.GetState(m_SqlStr2) == true && m_Lxmc == "正常出库")
                    {
                        e.Row.BackColor = System.Drawing.Color.Yellow;
                    }
                }
            }
            //如果是重复的要变颜色
            //判断是否重复
            string chSql = "select count(*) as count2 from dp_rckwcbtemp where MachineName='" + GV_MachineName + "' "
                         + "and gzdd='" + GV_Depot + "' and batchid like '" + v_CurrentBatchId + "' and ghtm='" + m_Ghtm + "'";
            System.Data.DataTable dtCh = dc.GetTable(chSql);
            if (dtCh.Rows.Count>1)
            {
                e.Row.BackColor = System.Drawing.Color.Red;
            }

        }

        protected void CmbFx_SelectedIndexChanged(object sender, EventArgs e)
        {
            string m_Fx = "";
            if (CmbFx.Value != null)
            {
                m_Fx = CmbFx.Value.ToString().Trim();

                string m_Sql = "update dp_rckwcbtemp set destination='" + m_Fx + "' where BatchId like '" + v_CurrentBatchId + "' "
                             +"and MachineName='" + GV_MachineName + "' and gzdd='" + GV_Depot + "'";
                dc.ExeSql(m_Sql);

                
                if (GV_Depot == "W" && m_Fx == "十堰库")
                {
                    string m_SO = "";
                    string m_SqlStr = "SELECT BatchId,ch,destination,so,COUNT(*) as sl FROM dp_rckwcbtemp  "
                            + "where MachineName='" + GV_MachineName + "' and gzdd='" + GV_Depot + "' and BatchId like '" + v_CurrentBatchId + "'  "
                            + "GROUP BY BatchId,ch,destination,so";
                    System.Data.DataTable dtCal = dc.GetTable(m_SqlStr);
                    if (dtCal.Rows.Count > 0)
                    {
                        m_SO = dtCal.Rows[0]["SO"].ToString();

                        string m_Sql1 = "select * from atpuenameplate where bzso='" + m_SO + "'";
                        System.Data.DataTable dt1 = dc.GetTable(m_Sql1);
                        if (dt1.Rows.Count > 0)
                        {
                            string upSql = "update dp_rckwcbtemp set so='" + m_SO + dt1.Rows[0]["Khh"].ToString() + "'";
                            dc.ExeSql(upSql);
                        }

                    }
                }
                GetTemplsh();
            }
        }

        protected void CmbRklx_SelectedIndexChanged(object sender, EventArgs e)
        {
            string m_rklxmc = "";
            if (CmbRklx.Value != null)
            {
                m_rklxmc = CmbRklx.Value.ToString().Trim();

                string m_Sql = "update dp_rckwcbtemp set rklx='" + m_rklxmc + "' where BatchId like '" + v_CurrentBatchId + "' and MachineName='" + GV_MachineName + "' and gzdd='" + GV_Depot + "'";
                dc.ExeSql(m_Sql);
            }
            GetTemplsh();
        }

        protected void CmbYgmc_SelectedIndexChanged(object sender, EventArgs e)
        {
            string m_Ygmc = "";
            if (CmbYgmc.Value != null)
            {
                m_Ygmc = CmbYgmc.Value.ToString();
                string m_Sql = "update dp_rckwcbtemp set ygmc='" + m_Ygmc + "' where BatchId like '" + v_CurrentBatchId + "' and MachineName='" + GV_MachineName + "' and gzdd='" + GV_Depot + "'";
                dc.ExeSql(m_Sql);
            }

            GetTemplsh();
        }
        protected void TxtCh_TextChanged(object sender, EventArgs e)
        {
            string m_Ch = "";
            if (TxtCh.Text.Trim() != "")
            {
                m_Ch = TxtCh.Text.Trim();

                string m_Sql = "update dp_rckwcbtemp set ch='" + m_Ch + "' where BatchId like '" + v_CurrentBatchId + "' and MachineName='" + GV_MachineName + "' and gzdd='" + GV_Depot + "'";
                dc.ExeSql(m_Sql);
            }
            GetTemplsh();
        }
        
        protected void ASPxCbSubmit_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
        {
            string collection = e.Parameter;
            if (collection == "DeleteScan")
            {
                string m_SqlStr = "select * from dp_rckwcbtemp where MachineName='" + GV_MachineName + "' and batchid like '" + v_CurrentBatchId + "' and gzdd='" + GV_Depot + "' order by Ghtm,rkdate";// 
                System.Data.DataTable chDt = dc.GetTable(m_SqlStr);
                for (int i = 0; i < chDt.Rows.Count; i++)
                {
                    string m_Ghtm = chDt.Rows[i]["Ghtm"].ToString();
                    string m_RkDate = chDt.Rows[i]["RKDATE"].ToString();

                    string sql10 = "select count(*) as count1 from dp_rckwcbtemp where MachineName='" + GV_MachineName + "' and batchid like '" + v_CurrentBatchId + "' and gzdd='" + GV_Depot + "' and Ghtm='" + m_Ghtm + "'  ";
                    System.Data.DataTable dt21 = dc.GetTable(sql10);
                    if (Convert.ToInt32(dt21.Rows[0]["count1"].ToString()) > 1)
                    {
                        string m_Sql = "delete from dp_rckwcbtemp where ghtm='" + m_Ghtm + "' and rkdate='" + m_RkDate + "' and MachineName='" + GV_MachineName + "' and gzdd='" + GV_Depot + "'";
                               
                        dc.ExeSql(m_Sql);
                    }

                }
                e.Result = "OK,重复扫描数据删除成功！";
                return;
                
            }
            if (collection == "DeleteCFRK")
            {
                string m_Ghtm = "";
                string m_RkDate = "";
                string m_Lxmc = "";
                
                string m_SqlStr = "select * from dp_rckwcbtemp where MachineName='" + GV_MachineName + "' and batchid like '" + v_CurrentBatchId + "' and gzdd='" + GV_Depot + "' order by Ghtm,rkdate";// 
                System.Data.DataTable chDt = dc.GetTable(m_SqlStr);
                for (int i = 0; i < chDt.Rows.Count; i++)
                {
                    m_Ghtm = chDt.Rows[i]["Ghtm"].ToString();
                    m_RkDate = chDt.Rows[i]["rkdate"].ToString();
                    m_Lxmc = chDt.Rows[i]["rklx"].ToString();

                    string m_SqlStr1 = "SELECT * FROM dp_rckwcb where ghtm='" + m_Ghtm + "' and gzdd='" + GV_Depot + "' order by rkdate desc";
                    System.Data.DataTable dt = dc.GetTable(m_SqlStr1);
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["rc"].ToString() == "出库")
                        {
                            string m_Sql = "delete from dp_rckwcbtemp where ghtm='" + m_Ghtm + "' and rkdate='" + m_RkDate + "' and MachineName='" + GV_MachineName + "' and gzdd='" + GV_Depot + "'";
                            dc.ExeSql(m_Sql);
                        }
                        else
                        {
                            string m_SqlStr2 = "SELECT * FROM dp_rckwcb where ghtm='" + m_Ghtm + "' and gzdd='" + GV_Depot + "' and rc='出库'";
                            if (dc.GetState(m_SqlStr2) == true && m_Lxmc == "正常出库")
                            {
                                string m_Sql = "delete from dp_rckwcbtemp where ghtm='" + m_Ghtm + "' and rkdate='" + m_RkDate + "' and MachineName='" + GV_MachineName + "' and gzdd='" + GV_Depot + "'";
                                dc.ExeSql(m_Sql);
                            }
                        }
                    }
                 }

                e.Result = "OK,重复出库数据删除成功！";
                return;
            }
        }

        protected void cmdCfm_Click(object sender, EventArgs e)
        {
            string m_Ghtm = "";
            string m_Lxmc = "";

            //先判断是否有重复扫描和重复出库的记录
            for (int i = 0; i < this.fpSpDetail.VisibleRowCount; i++)
            {
                m_Ghtm = this.fpSpDetail.GetRowValues(i, "GHTM").ToString();
                m_Lxmc = this.fpSpDetail.GetRowValues(i, "RKLX").ToString();
                //重复出库要提示
                string m_SqlStr1 = "SELECT * FROM dp_rckwcb where ghtm='" + m_Ghtm + "' and gzdd='" + GV_Depot + "' order by rkdate desc";
                System.Data.DataTable dt = dc.GetTable(m_SqlStr1);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["rc"].ToString() == "出库")
                    {
                        Show(this, "存在重复出库记录，操作失败!");
                        return;
                    }
                    else
                    {
                        string m_SqlStr2 = "SELECT * FROM dp_rckwcb where ghtm='" + m_Ghtm + "' and gzdd='" + GV_Depot + "' and rc='出库'";
                        if (dc.GetState(m_SqlStr2) == true && m_Lxmc == "正常出库")
                        {
                            Show(this, "存在重复扫描记录，操作失败!");
                            return;
                        }
                    }
                }
                //如果是重复的要变颜色
                //判断是否重复
                string chSql = "select count(*) as count2 from dp_rckwcbtemp where MachineName='" + GV_MachineName + "' "
                             + "and gzdd='" + GV_Depot + "' and batchid like '" + v_CurrentBatchId + "' and ghtm='" + m_Ghtm + "'";
                System.Data.DataTable dtCh = dc.GetTable(chSql);
                if (dtCh.Rows.Count > 1)
                {
                    Show(this, "存在重复出库记录，操作失败!");
                    return;
                }
                //库存中没有则背景色绿色显示
                string m_SqlStr = "SELECT * FROM dp_kcb where ghtm='" + m_Ghtm + "'";
                if (dc.GetState(m_SqlStr) == false)
                {
                    Show(this, "存在未入库记录，不能进行出库操作！请检查！");
                    return;
                }
            }

            string m_Sql = "insert into dp_rckwcb(rmes_id,ghtm,so,jhdm,ygmc,rklx,rkdate,gzrq,gzdd,rc,sourceplace,destination,batchId,ch) select SEQ_RMES_ID.NEXTVAL,ghtm,so,jhdm,ygmc,rklx,rkdate,to_char(to_date(gzrq,'yyyy-mm-dd hh24:mi:ss'),'yyyy-mm-dd hh24:mi:ss'),gzdd,rc,sourceplace,destination,batchId,ch from dp_rckwcbtemp where BatchId like'" + v_CurrentBatchId + "' and MachineName='" + GV_MachineName + "' and gzdd='" + GV_Depot + "'";
            dc.ExeSql(m_Sql);

            //尚未入库的纪录
            string m_Sql1 = "insert into dp_willrckwcb(ghtm,so,jhdm,ygmc,rklx,rkdate,gzrq,gzdd,rc,sourceplace,destination,batchId,ch) select ghtm,so,jhdm,ygmc,rklx,rkdate,gzrq,gzdd,rc,sourceplace,destination,batchId,ch from dp_rckwcbtemp where BatchId like'" + v_CurrentBatchId + "' and MachineName='" + GV_MachineName + "' and gzdd='" + GV_Depot + "' and ghtm not in (select ghtm from dp_kcb where gzdd='" + GV_Depot + "')";
            dc.ExeSql(m_Sql1);

            string m_Sql2 = "delete from dp_kcb where ghtm in (select ghtm from dp_rckwcbtemp where BatchId like '" + v_CurrentBatchId + "' and MachineName='" + GV_MachineName + "' and gzdd='" + GV_Depot + "') and ghtm in (select ghtm from dp_kcb where gzdd='" + GV_Depot + "')";
            dc.ExeSql(m_Sql2);

            //随机附件生成
            MW_CREATE_GHTMFJB sp = new MW_CREATE_GHTMFJB()
            {
                GZDD1 = GV_Depot,
                MACHINENAME1 = GV_MachineName,
                RQSJ1 = DateTime.Now.ToString(),
                
            };
            Procedure.run(sp);
            
            string m_Sql3 = "delete from dp_rckwcbtemp where BatchId like '" + v_CurrentBatchId + "' and MachineName='" + GV_MachineName + "' and gzdd='" + GV_Depot + "'";
            dc.ExeSql(m_Sql3);

            string m_Sql4 = "insert into DP_day_BatchId(WorkDate,gzdd,BatchId) values('" + v_DateStr + "','" + GV_Depot + "','" + v_CurrentBatchId + "')";
            dc.ExeSql(m_Sql4);

            //删除txt文件
            System.IO.File.Delete(@"D:\DT900\UP\chuku.txt");
            
            GetTemplsh();

        }

        protected void CmdPrint_Click(object sender, EventArgs e)
        {
            int IdxCount;
            int I;
            int SubTotal;
            int Total;
            int PageSerN;
            int RowSerN;
            int ColSerN;
            string Fxmc="";
            string Ch;
            string SO;
            int Count;
            string LastFxmc;
            string LastCh;
            string BatchId;

            int PageRows = 15;
            int RowBegin  = 7;
            int RowEnd = 15;
            int RowCh  = 3;
            int RowDate  = 3;
    
            int RecordRowCount  = 5;
    
             IdxCount = RowBegin;
             I = 1;
             PageSerN = 1;
             SubTotal = 0;
             Total = 0;
            
            if (fpSpCal.VisibleRowCount == 0)
            {
                Show(this, "请先读取文本!");
                return;
            }
            else
            {
                //创建Application对象
                Microsoft.Office.Interop.Excel.Application apps = new Microsoft.Office.Interop.Excel.Application();
                string filename = @"D:\DT900\UP\sfqd.xls";

                
                if (System.IO.File.Exists(filename))
                {
                    try
                    {
                        System.IO.File.Delete(filename);
                    }
                    catch (Exception)
                    {
                        Show(this, "请先关闭打开的xls文件！");
                        return;
                    }
                }
                
                string sourcePath =Server.MapPath(Request.ApplicationPath)+ "\\Rmes\\File\\入出库\\sfqd.xls";
                String targetPath = "D:\\DT900\\UP\\sfqd.xls";
                bool isrewrite = true; // true=覆盖已存在的同名文件,false则反之
                System.IO.File.Copy(sourcePath, targetPath, isrewrite); 

                object oMissing = System.Reflection.Missing.Value;

                apps.Visible = true;

                //得到WorkBook对象,可以用两种方式之一:下面的是打开已有的文件 
                Microsoft.Office.Interop.Excel.Workbook xBook = apps.Workbooks._Open(filename, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing,
                    oMissing, oMissing, oMissing, oMissing, oMissing);
                
                //指定要操作的Sheet，两种方式：
                Microsoft.Office.Interop.Excel.Worksheet xSheet = (Microsoft.Office.Interop.Excel.Worksheet)xBook.Sheets[1];

                //读取数据，通过Range对象 
                for (int i = 0; i < this.fpSpCal.VisibleRowCount; i++)
                {
                    LastFxmc = this.fpSpCal.GetRowValues(0, "DESTINATION").ToString();
                    LastCh = this.fpSpCal.GetRowValues(0, "CH").ToString();

                    Fxmc = this.fpSpCal.GetRowValues(i, "DESTINATION").ToString();
                    Ch = this.fpSpCal.GetRowValues(i, "CH").ToString();
                    SO = this.fpSpCal.GetRowValues(i, "SO").ToString();

                    if (GV_Depot == "W" && CmbFx.Value.ToString() == "十堰库")
                    {
                        string m_Sql = "select * from atpuenameplate where bzso='" + SO + "'";
                        System.Data.DataTable dt = dc.GetTable(m_Sql);
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["KHH"].ToString() != "")
                            {
                                SO = SO + dt.Rows[0]["KHH"].ToString();
                            }
                        }
                    }
                    BatchId = this.fpSpCal.GetRowValues(i, "BATCHID").ToString();
                    Count = Convert.ToInt32(this.fpSpCal.GetRowValues(i, "SL").ToString());

                    if (I <= RecordRowCount && Fxmc == LastFxmc && Ch == LastCh)
                    {
                        int P = IdxCount + 1;
                        xSheet.Cells[P, 1] = SO;
                        xSheet.Cells[P, 4] = Count;
                        xSheet.Cells[P, 8] = "是";
                        xSheet.Cells[P, 6] = Count;
                        xSheet.Cells[P, 10] = "是";
                        
                        SubTotal = SubTotal + Count;
                        I = I + 1;
                        IdxCount = IdxCount + 1;

                    }
                    else
                    {
                        //换页
                        //一页结束，这时填写页附属内容
                        int M = PageRows * (PageSerN - 1) + RowBegin + 6;
                        xSheet.Cells[M, 4] = SubTotal;
                        xSheet.Cells[M, 6] = SubTotal;
                        xSheet.Cells[M, 8] = "是";
                        xSheet.Cells[M, 10] = "是";
                        
                        xSheet.Cells[PageRows * PageSerN, 6] = Fxmc;
                        xSheet.Cells[PageRows * PageSerN, 12] = DateTime.Now.ToLongTimeString();

                        string GV_DepotName = "";
                        if (GV_Depot == "E")
                        {
                            GV_DepotName = "材料部东区总成库";
                        }
                        if (GV_Depot == "W")
                        {
                            GV_DepotName = "材料部西区总成库";
                        }
                        if (GV_Depot == "A")
                        {
                            GV_DepotName = "外销库";
                        }

                        xSheet.Cells[PageRows * PageSerN, 1] = GV_DepotName;
                        
                        //车号批次号
                        int ppp=PageRows * (PageSerN - 1) + RowCh;
                        xSheet.Cells[ppp, 9] = Ch;
                        xSheet.Cells[ppp, 9] = BatchId;

                        //日期
                        int dd = PageRows * (PageSerN - 1) + RowDate + 1;
                        xSheet.Cells[dd, 6] = DateTime.Now.Year.ToString();
                        xSheet.Cells[dd, 7] = DateTime.Now.Month.ToString();
                        xSheet.Cells[dd, 8] = DateTime.Now.Day.ToString();

                        IdxCount = PageRows * PageSerN + RowBegin;
                        PageSerN = PageSerN + 1;

                        //xSheet.Cells[IdxCount + 1, 1] = SO;
                        //xSheet.Cells[IdxCount + 1, 3] = Count;

                        SubTotal = 0;
                        SubTotal = SubTotal + Count;
                        i = i - 1;
                        I = 1;
                    }

                    Total = Total + Count;
                }
                //一页未结束时但记录集结束，这时填写页附属内容
                //合计
                int ww=PageRows * (PageSerN - 1) + RowBegin + 6;
                Microsoft.Office.Interop.Excel.Range rng4 = xSheet.get_Range("D" + PageRows * (PageSerN - 1) + RowBegin + 6, Type.Missing);
                rng4 = xSheet.get_Range("D" + ww, Type.Missing);
                rng4.Value2 = SubTotal;
                rng4 = xSheet.get_Range("F" + ww, Type.Missing);
                rng4.Value2 = SubTotal;
                rng4 = xSheet.get_Range("H" + ww, Type.Missing);
                rng4.Value2 = "是";
                rng4 = xSheet.get_Range("J" + ww, Type.Missing);
                rng4.Value2 = "是";
                //收货单位
                rng4 = xSheet.get_Range("F" + PageRows * PageSerN, Type.Missing);
                rng4.Value2 = Fxmc;
                //时间
                rng4 = xSheet.get_Range("L" + PageRows * PageSerN, Type.Missing);
                rng4.Value2 = DateTime.Now.ToLongTimeString();
                //库房名称
                string GV_DepotName1 = "";
                if (GV_Depot == "E")
                {
                    GV_DepotName1 = "材料部东区总成库";
                }
                if (GV_Depot == "W")
                {
                    GV_DepotName1 = "材料部西区总成库";
                }
                if (GV_Depot == "A")
                {
                    GV_DepotName1 = "外销库";
                }

                rng4 = xSheet.get_Range("A" + PageRows * PageSerN, Type.Missing);
                rng4.Value2 = GV_DepotName1;
                //日期
                int sj = PageRows * (PageSerN - 1) + RowDate + 1;
                rng4 = xSheet.get_Range("D" + sj, Type.Missing);
                rng4.Value2 = DateTime.Now.Year.ToString();
                rng4 = xSheet.get_Range("F" + sj, Type.Missing);
                rng4.Value2 = DateTime.Now.Month.ToString();
                rng4 = xSheet.get_Range("G" + sj, Type.Missing);
                rng4.Value2 = DateTime.Now.Day.ToString();

                xBook.Save();
                //xBook.Close(oMissing, oMissing, oMissing);
                //apps.Quit();

                //xSheet = null;
                //xBook = null;
                //apps = null;
            }
        }
        protected void CmdPrint2_Click(object sender, EventArgs e)
        {
            int RowSerN;
            int ColSerN;
            int MaxRows;
            int MaxCols;
            int SheetRow;
            int ColSerNTemp;
            
            //创建Application对象
            Microsoft.Office.Interop.Excel.Application apps = new Microsoft.Office.Interop.Excel.Application();
            string filename = @"D:\DT900\UP\lshqd.xls";

            if (System.IO.File.Exists(filename))
            {
                try
                {
                    System.IO.File.Delete(filename);
                }
                catch (Exception)
                {
                    Show(this, "请先关闭打开的lshqd.xls文件！");
                    return;
                }
            }

            String sourcePath = Server.MapPath(Request.ApplicationPath) + "\\Rmes\\File\\入出库\\lshqd.xls";
            String targetPath = "D:\\DT900\\UP\\lshqd.xls";
            bool isrewrite = true; // true=覆盖已存在的同名文件,false则反之
            System.IO.File.Copy(sourcePath, targetPath, isrewrite); 

            object oMissing = System.Reflection.Missing.Value;
            apps.Visible = true;

            //得到WorkBook对象,可以用两种方式之一:下面的是打开已有的文件 
            Microsoft.Office.Interop.Excel.Workbook xBook = apps.Workbooks._Open(filename, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing,
                oMissing, oMissing, oMissing, oMissing, oMissing);

            //指定要操作的Sheet，两种方式：
            Microsoft.Office.Interop.Excel.Worksheet xSheet = (Microsoft.Office.Interop.Excel.Worksheet)xBook.Sheets[1];

            MaxRows = fpSpDetail.VisibleRowCount;
            MaxCols = fpSpDetail.VisibleColumns.Count - 3;

            SheetRow = 0;
            //没从gridview中取数据，从表里取的数据
            string sql99 = "select 'SO' as SO,'流水号' AS GHTM,'计划代码' AS JHDM,'入出库时间' AS GZRQ,'工作地点' AS GZDD,'操作员工' AS YGMC,'入/出' AS RC,"
                         + "'入出库类型'AS RKLX,'从' AS SOURCEPLACE,'到' AS DESTINATION,'批次' AS BATCHID,'车号' AS CH,'扫描时间' AS RKDATE "
                         +" FROM DUAL UNION "
                         + "select SO,GHTM,JHDM,GZRQ,GZDD,YGMC,RC,RKLX,SOURCEPLACE,DESTINATION,BATCHID,CH,RKDATE "
                         + "from dp_rckwcbtemp "
                         + "where MachineName='" + GV_MachineName + "' and gzdd='" + GV_Depot + "' and batchid like '" + v_CurrentBatchId + "'  order by Ghtm desc";
            System.Data.DataTable dt = dc.GetTable(sql99); //没从gridview中取数据，从表里取的数据
            string sql88 =  "select SO,GHTM,JHDM,GZRQ,GZDD,YGMC,RC,RKLX,SOURCEPLACE,DESTINATION,BATCHID,CH,RKDATE "
                         + "from dp_rckwcbtemp "
                         + "where MachineName='" + GV_MachineName + "' and gzdd='" + GV_Depot + "' and batchid like '" + v_CurrentBatchId + "'  order by Ghtm ";
            System.Data.DataTable dt88 = dc.GetTable(sql88);
            for (RowSerN = 0; RowSerN < MaxRows; RowSerN++)
            {
                SheetRow = SheetRow + 1;
                if (SheetRow % 25 == 1)
                {
                    for (ColSerN = 0; ColSerN < MaxCols; ColSerN++)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[0][ColSerN].ToString()))
                        {
                            xSheet.Cells[SheetRow, ColSerN + 1] = dt.Rows[0][ColSerN].ToString();
                        }
                    }
                    SheetRow = SheetRow + 1;
                }
                for (ColSerN = 0; ColSerN < MaxCols; ColSerN++)
                {
                    xSheet.Cells[SheetRow, ColSerN + 1] = dt88.Rows[RowSerN][ColSerN].ToString();
                }
            }

            MaxRows = fpSpCal.VisibleRowCount+1;
            MaxCols = fpSpCal.VisibleColumns.Count;
            string m_SqlStr = "select '批次' as BatchId,'车号' as ch,'方向' as destination,'SO' as so,'数量' as sl from dual union "
                            +"SELECT BatchId,ch,destination,so,to_char(COUNT(*)) as sl FROM dp_rckwcbtemp  "
                            + "where MachineName='" + GV_MachineName + "' and gzdd='" + GV_Depot + "' and BatchId like '" + v_CurrentBatchId + "'  "
                            + "GROUP BY BatchId,ch,destination,so order by BatchId desc";
            System.Data.DataTable dtCal = dc.GetTable(m_SqlStr);
            for (RowSerN = 0; RowSerN < MaxRows; RowSerN++)
            {
                SheetRow = SheetRow + 1;
                ColSerNTemp = 1;
                for (ColSerN = 0; ColSerN < MaxCols; ColSerN++)
                {
                    xSheet.Cells[SheetRow, ColSerN + 1] = dtCal.Rows[RowSerN][ColSerN].ToString();
                }

            }

            SheetRow = SheetRow + 1;
            xSheet.Cells[SheetRow, 1] = "合计";
            xSheet.Cells[SheetRow, 5] = fpSpDetail.VisibleRowCount;
            SheetRow = SheetRow + 1;
            xSheet.Cells[SheetRow, 1] = "                          " + "共" + fpSpDetail.VisibleRowCount + "台" + "    " + "客户确认：";

            xBook.Save();
            //xBook.Close(oMissing, oMissing, oMissing);
            //apps.Quit();

        }
        public string GetColName(int number, string ColName)
        {
            int numberadd;
            int I;
            int J;
            numberadd = 64;
            if (number > 26 * 26)
            {
                Show(this, "不会吧，老兄，这么大的数？");
                //这里不允许退出！
            }
            I = (number - 1) / 26;//保证当number 等于26时i=0
            J = number - 26 * I;

            if (I == 0)
            {
                ColName = Convert.ToString(J + numberadd).Trim();
            }
            else
            {
                ColName = Convert.ToString(I + numberadd).Trim() + Convert.ToString(J + numberadd).Trim();
            }
            return ColName;
            //本函数按正常来说没必要再保证number的大小了，不会太大的
            //注意在这里符号“/”的取值是四舍五入,而符号“\”才表示取模
        }
        
        protected void cmdRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                string m_SO = "";
                string m_Ch = "";
                string m_Jhdm = "";
                string m_Lxmc = "";
                string GV_DepotName = "";
                string m_SourcePlace = "";
                string m_Rc = "";
                string m_BatchId = "";
                string m_Destination = "";
                string m_Ygmc = "";

                //根据选择生产线（仓库代码）获得仓库名称
                if (comboPline.Value != null)
                {
                    GV_Depot = comboPline.Value.ToString();
                    string chSql = "SELECT depotname FROM DP_dmdepot where depotid='" + GV_Depot + "'";
                    dataConn chDc = new dataConn(chSql);
                    System.Data.DataTable chDt = chDc.GetTable();
                    if (chDt.Rows.Count > 0)
                    {
                        GV_DepotName = chDt.Rows[0]["depotname"].ToString();
                    }
                }
                //删除临时表
                string m_Sql = "delete from dp_rckwcbtemp where MachineName='" + GV_MachineName + "' and gzdd='" + GV_Depot + "'";
                dc.ExeSql(m_Sql);

                v_CurrentBatchId = "%";

                string path = File2.Value;//上传文件路径
                if (path == "" && !path.ToUpper().Contains(".TXT"))
                {
                    ButRead.Enabled = true;
                    return;
                }
                string uploadPath = Server.MapPath(Request.ApplicationPath); //上传目录及文件名
                string fileName = "RK" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";

                //if (!Directory.Exists("C:\\inetpub\\wwwroot\\webapp\\Rmes\\File\\excel"))
                //{
                //    Directory.CreateDirectory("C:\\inetpub\\wwwroot\\webapp\\Rmes\\File\\excel");
                //}
                uploadPath = uploadPath + "\\Rmes\\File\\excel\\" + fileName; //得到上传目录及文件名称  
                File2.PostedFile.SaveAs(uploadPath);       //上传文件到服务器  

                ////直接读取出字符串
                //string text = System.IO.File.ReadAllText(uploadPath);


                ////员工&类型
                //if (text.Length < 10)
                //{
                //    //员工
                //    if (text.Substring(0, 2).ToString() == "YG")
                //    {
                //        string m_Ygdm = text.Substring(0, 5).ToString();
                //        //string m_Ygmc1 = "";
                //        string m_Sql1 = "select ygmc from dp_ygb where ygdm='" + m_Ygdm + "'";
                //        System.Data.DataTable dt1 = dc.GetTable(m_Sql1);
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
                //        System.Data.DataTable dt2 = dc.GetTable(m_Sql2);
                //        if (dt2.Rows.Count > 0)
                //        {
                //            m_lxmc = dt2.Rows[0]["lxmc"].ToString();
                //        }
                //    }
                //    if (text.Substring(0, 2) != "YG" && text.Substring(0, 1) != "I" && text.Substring(0, 1) != "O")//?????????????????????????
                //    {
                //        m_Ch = text;
                //    }
                //}
                //流水号
                //if (text.Length > 10)
                //{
                    //按行读取为字符串数组
                    string[] lines = System.IO.File.ReadAllLines(uploadPath);
                    //string ch = lines[0].Trim();//?????????????????????????????????????????????????????

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
                                //string m_Ygmc1 = "";
                                string m_Sql1 = "select ygmc from dp_ygb where ygdm='" + m_Ygdm + "'";
                                System.Data.DataTable dt1 = dc.GetTable(m_Sql1);
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
                                System.Data.DataTable dt2 = dc.GetTable(m_Sql2);
                                if (dt2.Rows.Count > 0)
                                {
                                    m_lxmc = dt2.Rows[0]["lxmc"].ToString();
                                }
                            }
                            if (lines[i].Substring(0, 2) != "YG" && lines[i].Substring(0, 1) != "I" && lines[i].Substring(0, 1) != "O")
                            {
                                m_Ch = lines[i];
                            }
                        }
                        //流水号
                        if (lines[i].Length > 10)
                        {
                            string m_Ghtm = lines[i].Substring(0, 8).ToString();
                            string m_RkDate = lines[i].Substring(lines[i].Length - 14, 14);
                            DateTime dt = DateTime.ParseExact(m_RkDate, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);

                            string m_Sql1 = "Select * from dp_rckwcb where ghtm='" + m_Ghtm + "' and rc='入库' and destination='" + GV_DepotName + "' order by gzrq desc";
                            System.Data.DataTable dt1 = dc.GetTable(m_Sql1);
                            if (dt1.Rows.Count > 0)
                            {
                                //由入库至出库
                                m_SO = dt1.Rows[0]["SO"].ToString();
                                m_Jhdm = dt1.Rows[0]["Jhdm"].ToString();
                                m_Lxmc = dt1.Rows[0]["rklx"].ToString();

                                //由上一次入库库类型得出此次出库类型
                                string m_Sql2 = "select * from dp_rckmap where rklx='" + m_Lxmc + "'";
                                System.Data.DataTable dt2 = dc.GetTable(m_Sql2);
                                if (dt2.Rows.Count > 0)
                                {
                                    m_Lxmc = dt2.Rows[0]["cklx"].ToString();
                                }
                            }
                            else
                            {
                                //没有入库，直接出库
                                if (GV_Depot != "A")
                                {
                                    //默认正常出库
                                    m_Lxmc = "正常出库";
                                    //从atpu中查询入出库类型
                                    string m_Sql4 = "select c.* from data_product a,data_plan b,dp_atpukcmap c where a.PLAN_CODE=b.PLAN_CODE and a.SN='" + m_Ghtm + "' and b.PLAN_TYPE=c.atpujhlx and rc='出库'";
                                    System.Data.DataTable dt4 = dc.GetTable(m_Sql4);
                                    if (dt4.Rows.Count > 0)
                                    {
                                        m_Lxmc = dt1.Rows[0]["rklx"].ToString();
                                    }
                                }
                                string m_Sql5 = "Select PLAN_SO,PLAN_CODE from DATA_PRODUCT where sn='" + m_Ghtm + "'";
                                System.Data.DataTable dt5 = dc.GetTable(m_Sql5);
                                if (dt5.Rows.Count > 0)
                                {
                                    m_SO = dt5.Rows[0]["PLAN_SO"].ToString();
                                    m_Jhdm = dt5.Rows[0]["PLAN_CODE"].ToString();
                                }
                            }


                            //确保m_SO正确，计划代码不为空
                            string m_Sql6 = "Select PLAN_SO,PLAN_CODE from DATA_PRODUCT where sn='" + m_Ghtm + "'";
                            System.Data.DataTable dt6 = dc.GetTable(m_Sql6);
                            if (dt6.Rows.Count > 0)
                            {
                                m_SO = dt6.Rows[0]["PLAN_SO"].ToString();
                                m_Jhdm = dt6.Rows[0]["PLAN_CODE"].ToString();
                            }

                            //记入临时表
                            m_SourcePlace = GV_DepotName;
                            m_Rc = "出库";
                            if (string.IsNullOrEmpty(m_Ch))
                            {
                                m_Ch = " ";
                            }
                            if (string.IsNullOrEmpty(m_BatchId))
                            {
                                m_BatchId = " ";
                            }
                            if (string.IsNullOrEmpty(m_Destination))
                            {
                                m_Destination = " ";
                            }
                            m_Ygmc = theUserName;

                            string m_SqlIn = "insert into dp_rckwcbtemp(ghtm,so,jhdm,ygmc,rklx,rkdate,gzrq,gzdd,rc,SourcePlace,destination,BatchId,Ch,MachineName) values('" + m_Ghtm + "','" + m_SO + "','" + m_Jhdm + "','" + m_Ygmc + "','" + m_Lxmc + "','" + m_RkDate + "',to_char(sysdate,'yyyy-mm-dd hh24:mi:ss'),'" + GV_Depot + "','" + m_Rc + "','" + m_SourcePlace + "','" + m_Destination + "','" + m_BatchId + "','" + m_Ch + "','" + GV_MachineName + "')";
                            dc.ExeSql(m_SqlIn);
                        }
                    }
                //}
                GetTemplsh();
            }
            catch
            { }
        }

        protected void cmdQryFj_Click(object sender, EventArgs e)
        {
            QITA.Visible = false;
            SJFJQD.Visible = true;

            //初始化日期时间下拉列表
            string chSql = "select distinct rqsj from sjghtmfjb where gzdd='" + GV_Depot + "' and machinename='" + GV_MachineName + "'";
            cmbSjPc.DataSource = dc.GetTable(chSql);
            cmbSjPc.TextField = "RQSJ";
            cmbSjPc.ValueField = "RQSJ";
            cmbSjPc.DataBind();

        }
        private void InitSpreadSo(string ThisSj, string ThisGZDd, string thisMachineName)
        {
            string theSo = "";
            string theLshSl = "";
            string theLshStr = "";

            string theSql = "select distinct so from sjghtmfjb where gzdd='" + ThisGZDd + "' and machinename='" + thisMachineName + "' and rqsj='" + ThisSj + "'";
            System.Data.DataTable dt = dc.GetTable(theSql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                theSo = dt.Rows[i]["so"].ToString();

                //获得总行数
                string sql2 = "select count(*) as count1 from sjghtmfjb where gzdd='" + ThisGZDd + "' and machinename='" + thisMachineName + "' and rqsj='" + ThisSj + "' and so='" + theSo + "'";
                System.Data.DataTable dt2 = dc.GetTable(sql2);
                if (dt2.Rows.Count > 0)
                {
                    theLshSl = dt2.Rows[0]["count1"].ToString();
                }

                //获得条码号
                string theSql1 = "select distinct ghtm from sjghtmfjb where gzdd='" + ThisGZDd + "' and machinename='" + thisMachineName + "' and rqsj='" + ThisSj + "' and so='" + theSo + "'";
                System.Data.DataTable dt1 = dc.GetTable(theSql1);
                for (int j = 0; j < dt1.Rows.Count; j++)
                {
                    theLshStr = dt1.Rows[j]["ghtm"].ToString();
                }


            }
        }

        protected void fpSpDetail_PageIndexChanged(object sender, EventArgs e)
        {
            GetTemplsh(); ;
            fpSpDetail.DataBind();
        }
        public static void Show(System.Web.UI.Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + msg.ToString() + "');</script>");
        }

        protected void cmdShut_Click(object sender, EventArgs e)
        {
            QITA.Visible = true;
            SJFJQD.Visible = false;
        }
        private void setSJFJ()
        {

            string theLshStr = "";
            int theLshSl;
            int theBzSl;

            txtThisSj.Text = cmbSjPc.Value.ToString();
            //SO
            string theSql = "select distinct so from sjghtmfjb where gzdd='" + GV_Depot + "' and machinename='" + GV_MachineName + "' and rqsj='" + cmbSjPc.Value.ToString() + "'";
            System.Data.DataTable dt = dc.GetTable(theSql);
            if (dt.Rows.Count > 0)
            {
                txtTheSo.Text = dt.Rows[0]["SO"].ToString();

                //流水号数量,流水号明细
                string theSql1 = "select distinct ghtm from sjghtmfjb where gzdd='" + GV_Depot + "' and machinename='" + GV_MachineName + "' and rqsj='" + cmbSjPc.Value.ToString() + "' and so='" + dt.Rows[0]["SO"].ToString() + "'";
                System.Data.DataTable dt1 = dc.GetTable(theSql1);

                theLshSl = dt1.Rows.Count;
                txttheLshSl.Text = theLshSl.ToString();
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    theLshStr = theLshStr + dt1.Rows[i]["ghtm"].ToString() + "、";
                }
                txttheLshStr.Text = theLshStr;

                //包装单元数量,包装单元明细
                string theSql2 = "select distinct dyxh,bzdy,fjb from sjghtmfjb where gzdd='" + GV_Depot + "' and machinename='" + GV_MachineName + "' "
                               + "and rqsj='" + cmbSjPc.Value.ToString() + "' and so='" + dt.Rows[0]["SO"].ToString() + "' order by dyxh";
                System.Data.DataTable dt2 = dc.GetTable(theSql2);

                theBzSl = dt2.Rows.Count;
                txttheBzSl.Text = theBzSl.ToString();

                ASPxGridView1.DataSource = dt2;
                ASPxGridView1.DataBind();

            }
        }
        protected void cmdSelSj_Click(object sender, EventArgs e)
        {
            setSJFJ();
        }

        protected void ASPxGridView1_PageIndexChanged(object sender, EventArgs e)
        {
            setSJFJ();
        }
        
        protected void fpSpDetail_detail_BeforePerformDataSelect(object sender, EventArgs e)
        {
            ASPxGridView gridDetail = (ASPxGridView)sender;
            //string GV_CurrentGhtm = gridDetail.GetMasterRowFieldValues().ToString();
            string GV_CurrentGhtm = gridDetail.GetMasterRowFieldValues("GHTM").ToString();
            string detailSql = "select distinct ghtm,so,ygmc,rklx,rkdate,gzrq,gzdd,rc,SourcePlace,Destination,BatchId,Ch from dp_rckwcb"
                             + " where GHTM='" + GV_CurrentGhtm + "' order by gzrq,rkdate";
            //System.Data.DataTable dtDetail = dc.GetTable(detailSql);
            detailDS.SelectCommand = detailSql;
            gridDetail.DataSource = detailDS;
            //gridDetail.DataBind();
        }

        protected void btnXlsExport_Click(object sender, EventArgs e)
        {
            setSJFJ();
            gridExport.WriteXlsToResponse("随机附件");
        }

        protected void fpSpDetail_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
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

        protected void fpSpDetail_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            GetTemplsh();
            fpSpDetail.Selection.UnselectAll();
        }

        protected void fpSpCal_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            GetTemplsh();
            
           
        }
        //单行修改数据
        protected void fpSpDetail_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string sql = "";
            if (e.NewValues["GHTM"].ToString() == e.OldValues["GHTM"].ToString())
            {
                sql = "update dp_Rckwcbtemp set destination='" + e.NewValues["DESTINATION"].ToString() + "', "
                    + " rklx='" + e.NewValues["RKLX"].ToString() + "',so='" + e.NewValues["SO"].ToString() + "' "
                    + "where ghtm='" + e.NewValues["GHTM"].ToString() + "' and rkdate='" + e.NewValues["RKDATE"].ToString() + "'";
            }
            else
            {
                sql = "update dp_Rckwcbtemp set ghtm='" + e.NewValues["GHTM"].ToString() + "',destination='" + e.NewValues["DESTINATION"].ToString() + "', "
                    + "rklx='" + e.NewValues["RKLX"].ToString() + "',so='" + e.NewValues["SO"].ToString() + "' "
                    + "where rkdate='" + e.NewValues["RKDATE"].ToString() + "' and gzdd='" + e.NewValues["GZDD"].ToString() + "' "
                    + " and machinename='" + e.NewValues["MACHINENAME"].ToString() + "'";
            }
                
            dc.ExeSql(sql);

            e.Cancel = true;
            fpSpDetail.CancelEdit();
            GetTemplsh();
        }

        //protected void BtnHidden_Click(object sender, EventArgs e)
        //{
        //    GetTemplsh();
        //}

        
        
    }
}