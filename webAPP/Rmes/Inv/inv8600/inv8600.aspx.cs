using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

using Rmes.Web.Base;
using System.Data;
using Rmes.Pub.Data;
using PetaPoco;
using Rmes.DA.Base;
using System.Configuration;
using System.Data.SqlClient;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxClasses;
using System.Net;

/**
 * 功能概述：期初库存设定
 * 作者：任海
 * 创建时间：2016-09-15
 */
namespace Rmes.WebApp.Rmes.Inv.inv8600
{
    public partial class inv8600 : BasePage
    {
        private dataConn dc = new dataConn();
        private PubCs thePubCs = new PubCs();

        private static string theCompanyCode, theUserId, theUserName;
        public string theProgramCode, MachineName;
        public Database db = DB.GetInstance();

        public const string FilePath = "d:\\qichu.txt"; 

        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();
            theUserName = theUserManager.getUserName();
            //获取MachineName
            string hostIPAddress = Page.Request.UserHostAddress;
            //IPHostEntry hostInfo = System.Net.Dns.GetHostByAddress(hostIPAddress);
            MachineName = hostIPAddress;
            //MachineName = System.Net.Dns.GetHostName();
            theProgramCode = "inv8600";

            //ASPxListBoxPart1.Attributes.Add("ondblclick", "ListBoxDblClick(this);");
            if (!IsPostBack)
            {
                //放在外面每次都初始化
                ToRq.Date = DateTime.Now;
                txtPCode.Value = "E";
                initPlineCode();
            }

            setCondition();

        }

        //初始化gridview
        private void setCondition()
        {
            string sql = "";
            sql = "SELECT P.ROWID,P.* FROM DP_PKTEMP P "
                + " WHERE MACHINENAME = '" + MachineName + "' AND GZDD = '" + txtPCode.Value.ToString() + "' ";
            sql += " order by SO,GHTM,RKDATE ";

            DataTable dt = dc.GetTable(sql);
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
        }

        //初始化生产线
        private void initPlineCode()
        {
            string sql = "select distinct a.pline_code,b.pline_name from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'";
            SqlCode.SelectCommand = sql;
            SqlCode.DataBind();
            //有问题
            txtPCode.SelectedIndex = txtPCode.Items.Count >= 0 ? 0 : -1;
        }

        //导入txt
        protected void ASPxButton_Import_Click(object sender, EventArgs e)
        {
            string m_Ghtm = "";
            string m_SO = "";
            string m_Jhdm = "";
            string m_Ygdm = "";
            string m_Ygmc = "";
            string m_Lxdm = "";
            string m_Lxmc = "";
            string m_Rklx = "";
            string m_RkDate = "";
            string m_Gzrq = "";
            string m_Gzdd = "";
            string m_Rc = "";
            string m_SourcePlace = "";
            string m_Destination = "";
            string m_BatchId = "";
            string m_Ch = "";

            m_Gzrq = DateTime.Now.ToString();
            m_Ygmc = theUserName;
            m_Rc = "入库";
            //用combobox获取到生产线后再给m_Destination赋值
            string sql = "SELECT depotname FROM DP_dmdepot where depotid='" + txtPCode.Value + "'";
            dc.setTheSql(sql);
            try
            {
                m_Destination = dc.GetTable().Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                m_Destination = "";
            }

            if (!System.IO.File.Exists(FilePath))
            {
                showAlert(this, "文件不存在！");
                return;
            }

            //读取文件的try/catch应该怎么写？是这样写吗
            string line;
            System.IO.StreamReader my;
            try
            {
                my = new System.IO.StreamReader(FilePath, System.Text.Encoding.Default);
                if ((line = my.ReadLine()) == null)
                {
                    showAlert(this, "文件为空！");
                    return;
                }
                //若不重新赋值，会被视为 已经读过一行了（值得再研究）
                my = new System.IO.StreamReader(FilePath, System.Text.Encoding.Default);

                //删除临时表
                sql = "delete from dp_pktemp where MachineName='" + MachineName + "' and GZDD = '" + txtPCode.Value.ToString() + "' ";
                dc.ExeSql(sql);

                while ((line = my.ReadLine()) != null)
                {
                    m_SO = "";
                    //如果是员工和类型
                    if (line.Length < 10)
                    {
                        if (line.Substring(0, 2) == "YG")
                        {
                            m_Ygdm = line.Substring(0, 5);
                            //取员工名称
                            sql = "select ygmc from dp_ygb where ygdm='" + m_Ygdm + "'";
                            dc.setTheSql(sql);
                            if (dc.GetTable().Rows.Count != 0)
                            {
                                m_Ygmc = dc.GetTable().Rows[0][0].ToString();
                            }
                            else
                            {
                                m_Ygmc = "";
                            }
                        }
                        if (line.Substring(0, 1) == "I")
                        {
                            m_Lxdm = line.Substring(0, 4);
                            //取类型名称
                            sql = "SELECT LXMC FROM DP_RKLX WHERE LXDM = '" + m_Lxdm + "'";
                            dc.setTheSql(sql);
                            if (dc.GetTable().Rows.Count != 0)
                            {
                                m_Lxmc = dc.GetTable().Rows[0][0].ToString();
                            }
                            else
                            {
                                m_Lxmc = "";
                            }
                        }
                    }
                    //如果是流水号
                    if (line.Length > 10)
                    {
                        m_Ghtm = line.Substring(0, 8);
                        m_RkDate = line.Substring(8, line.Length - 8);
                        m_Gzrq = "1900-01-01 00:00:00";
                        //so,batchid
                        sql = "SELECT * FROM DP_KCB WHERE GHTM = '" + m_Ghtm + "'";
                        dc.setTheSql(sql);
                        DataTable dc1 = dc.GetTable();
                        if (dc1.Rows.Count >= 1)
                        {
                            m_SO = dc1.Rows[0]["SO"].ToString();
                            m_Jhdm = dc1.Rows[0]["JHDM"].ToString();
                            m_Ygmc = dc1.Rows[0]["YGMC"].ToString();
                            m_Rklx = dc1.Rows[0]["RKLX"].ToString();
                            m_Gzrq = dc1.Rows[0]["GZRQ"].ToString();
                            m_Gzdd = dc1.Rows[0]["GZDD"].ToString();
                            m_Rc = dc1.Rows[0]["RC"].ToString();
                            m_SourcePlace = dc1.Rows[0]["SOURCEPLACE"].ToString();
                            m_Destination = dc1.Rows[0]["DESTINATION"].ToString();
                            m_BatchId = dc1.Rows[0]["BATCHID"].ToString();
                            m_Ch = dc1.Rows[0]["CH"].ToString();
                            m_RkDate = dc1.Rows[0]["RKDATE"].ToString();
                        }
                        else
                        {
                            sql = "Select * from dp_rckwcb where ghtm='" + m_Ghtm + "' and rc='入库'order by gzrq desc";
                            dc.setTheSql(sql);
                            dc1 = dc.GetTable();
                            //若多于一行会怎么样
                            if (dc1.Rows.Count >= 1)
                            {
                                m_SO = dc1.Rows[0]["SO"].ToString();
                                m_Jhdm = dc1.Rows[0]["JHDM"].ToString();
                                m_Ygmc = dc1.Rows[0]["YGMC"].ToString();
                                m_Rklx = dc1.Rows[0]["RKLX"].ToString();
                                m_Gzrq = dc1.Rows[0]["GZRQ"].ToString();
                                m_Gzdd = dc1.Rows[0]["GZDD"].ToString();
                                m_Rc = dc1.Rows[0]["RC"].ToString();
                                m_SourcePlace = dc1.Rows[0]["SOURCEPLACE"].ToString();
                                m_Destination = dc1.Rows[0]["DESTINATION"].ToString();
                                m_BatchId = dc1.Rows[0]["BATCHID"].ToString();
                                m_Ch = dc1.Rows[0]["CH"].ToString();
                                m_RkDate = dc1.Rows[0]["RKDATE"].ToString();
                            }
                        }
                        //原来是在这里插入，是否有问题，  因为是全局变量，所以可以插入之前的m_Lxmc，但m_Ygmc已被刷新，读取到没有意义？？
                        sql = "insert into dp_pktemp(ghtm,so,jhdm,ygmc,rklx,rkdate,gzrq,gzdd,rc,SourcePlace,destination,BatchId,Ch,MachineName) values('"
                            + m_Ghtm + "','" + m_SO + "','" + m_Jhdm + "','" + m_Ygmc + "','" + m_Lxmc + "','" + m_RkDate + "','" + m_Gzrq + "','" + txtPCode.Value.ToString()
                            + "','" + m_Rc + "','" + m_SourcePlace + "','" + m_Destination + "','" + m_BatchId + "','" + m_Ch + "','" + MachineName + "') ";
                        dc.ExeSql(sql);
                    }
                }
                my.Close();
                showAlert(this, "导入成功");
            }
            catch
            {
                showAlert(this, "读取文件异常！");
            }
            setCondition();
        }

        //设为期初库存
        protected void ASPxCallbackPanel6_Callback(object sender, CallbackEventArgsBase e)
        {
            DateTime toRq = ToRq.Date;
            string m_YearStr = toRq.Year.ToString();
            string m_MonthStr = toRq.Month.ToString();
            string m_TorqStr = toRq.ToString();
            string sql = "delete from dp_monthkcb where year='" + m_YearStr + "' and month='" + m_MonthStr + "' and PeriodLine='Begin' and GZDD = '" + txtPCode.Value.ToString() + "' ";
            dc.ExeSql(sql);
            sql = "insert into dp_Monthkcb(So,ghtm,jhdm,gzrq,gzdd,ygmc,rc,rklx,SourcePlace,Destination,BatchId,Ch,RkDate,MonthDate,Year,Month,PeriodLine) select So,ghtm,jhdm,gzrq,gzdd,ygmc,rc,rklx,SourcePlace,Destination,BatchId,Ch,RkDate,'"
                + m_TorqStr + "','" + m_YearStr + "','" + m_MonthStr + "','Begin' from dp_pktemp where MachineName='" + MachineName + "' and GZDD = '" + txtPCode.Value.ToString() + "' ";
            dc.ExeSql(sql);
            sql = "delete from dp_pktemp where MachineName='" + MachineName + "' and GZDD = '" + txtPCode.Value.ToString() + "' ";
            dc.ExeSql(sql);
            
            //删除txt文件
            deleteFile(FilePath);

            //为什么不弹出
            showAlert(this, "设为期初库存成功！");

            //清单显示
            sql = "select ROWID,so,ghtm,jhdm,gzrq,gzdd,ygmc,rc,rklx,SourcePlace,Destination,BatchId,ch,RkDate from dp_monthkcb where year='"
                + m_YearStr + "' and month='" + m_MonthStr + "' and periodline='Begin' and GZDD = '" + txtPCode.Value.ToString() + "' ";
            DataTable dt = dc.GetTable(sql);
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();

            //统计显示，前端ASPxSummaryItem实现
        }

        //创建EDITFORM前
        protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
        {
            string sql = "SELECT LXMC FROM DP_RKLX ORDER BY LXDM ";
            DataTable dt = dc.GetTable(sql);
            ASPxComboBox cb_Rklx = ASPxGridView1.FindEditFormTemplateControl("txtRklx") as ASPxComboBox;
            cb_Rklx.DataSource = dt;
            cb_Rklx.TextField = dt.Columns[0].ToString();
            cb_Rklx.ValueField = dt.Columns[0].ToString();
        }

        //dp_kcb中已有的变为黄色，重复行变为红色
        protected void ASPxGridView1_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.VisibleIndex < 0) return;

            string ghtm = e.GetValue("GHTM").ToString();
            string sql = "SELECT * FROM dp_kcb where ghtm='" + ghtm + "'";
            dc.setTheSql(sql);
            if (dc.GetTable().Rows.Count >= 1)
            {
                e.Row.BackColor = System.Drawing.Color.Yellow;
            }
            sql = "select * from dp_pktemp where MachineName='" + MachineName + "' and gzdd= '" + txtPCode.Value.ToString() + "' AND GHTM = '" + ghtm + "' order by So,Ghtm,rkdate ";
            dc.setTheSql(sql);
            if (dc.GetTable().Rows.Count > 1)
            {
                e.Row.BackColor = System.Drawing.Color.Red;
            }
            else
            {
                return;
            }

            //string ghtm = e.GetValue("GHTM").ToString();
            //string sql = " SELECT GHTM FROM DP_PKTEMP WHERE GHTM IN (select GHTM from dp_pktemp group by GHTM having count(1) >1) and ghtm = '" + ghtm +"' ";
            //if (dc.ExeSql(sql) > 0)
            //{
            //    dc.setTheSql(sql);
            //    string status = dc.GetTable().Rows[0][0].ToString();
            //    e.Row.BackColor = System.Drawing.Color.Red;
            //}
            //else return;
        }

        //修改
        protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxTextBox m_rowID = ASPxGridView1.FindEditFormTemplateControl("ROWID") as ASPxTextBox;
            ASPxTextBox m_Ghtm = ASPxGridView1.FindEditFormTemplateControl("GHTM") as ASPxTextBox;
            ASPxTextBox m_RkDate = ASPxGridView1.FindEditFormTemplateControl("RKDATE") as ASPxTextBox;
            ASPxTextBox m_SO = ASPxGridView1.FindEditFormTemplateControl("txtSO") as ASPxTextBox;
            ASPxComboBox m_Rklx = ASPxGridView1.FindEditFormTemplateControl("txtRklx") as ASPxComboBox;

            string sql = "update dp_pktemp set rklx='" + m_Rklx.Text.Trim() + "'，SO = '" + m_SO.Text.Trim()
                //判断哪一行直接用ROWID最方便
                + "' where ROWID = '" + m_rowID.Text.ToString() + "' ";
            dc.ExeSql(sql);

            e.Cancel = true;
            ASPxGridView1.CancelEdit();
            setCondition();
        }

        //删除
        protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string strGhtm = e.Values["GHTM"].ToString();
            string strRKDate = e.Values["RKDATE"].ToString();

            string sql = " delete from dp_pktemp where ghtm='" + strGhtm + "' and rkdate='" + strRKDate
                + "' and MachineName='" + MachineName + "' and GZDD = '" + txtPCode.Value.ToString() + "' ";
            dc.ExeSql(sql);

            setCondition();
            e.Cancel = true;
        }

        //显示detailrow
        //根据GHTM从dp_rckwcb取数据
        protected void fpSpDetail_detail_BeforePerformDataSelect(object sender, EventArgs e)
        {
            ASPxGridView gridDetail = (ASPxGridView)sender;
            string GV_CurrentGhtm = gridDetail.GetMasterRowFieldValues("GHTM").ToString();
            string detailSql = "select ghtm,so,ygmc,rklx,rkdate,gzrq,gzdd,rc,SourcePlace,Destination,BatchId,Ch from dp_rckwcb"
                             + " where GHTM='" + GV_CurrentGhtm + "' order by gzrq,rkdate";

            DataTable detailDataTable = dc.GetTable(detailSql);
            //不是直接给detailrow绑定数据，通过主表的点击来动态绑定数据
            gridDetail.DataSource = detailDataTable;
        }

        //删除文件
        private void deleteFile(string path)
        {
            //如果直接写File找不到方法，就写全路径
            if (System.IO.File.Exists(path))
            {
                try
                {
                    System.IO.File.Delete(path);
                }
                catch
                {
                    return;
                }
            }
        }

        //前端弹出alert消息
        public static void showAlert(System.Web.UI.Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + msg.ToString() + "');</script>");
        }

        //前端弹出confirm消息
        public static void showConfirm(System.Web.UI.Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>confirm('" + msg.ToString() + "');</script>");
        }


    }
}