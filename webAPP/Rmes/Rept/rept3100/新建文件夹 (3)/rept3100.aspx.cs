using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

using Rmes.Web.Base;
using Rmes.Pub.Data;

using DevExpress.Web.ASPxEditors;
using System.Collections;

/**
 * 功能概述：在制品查询
 * 作者：任海
 * 创建时间：2016-09-16
 */
namespace Rmes.WebApp.Rmes.Rept.rept3100
{
    public partial class rept3100 : BasePage
    {
        private dataConn dc = new dataConn();
        public string theProgramCode;
        public string theCompanyCode;
        private string theUserId;
        //控制首次进入页面不显示数据的变量
        //private int m_show;

        protected void Page_Load(object sender, EventArgs e)
        {
            theProgramCode = "rept3100";
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();

            if (!IsPostBack)
            {
                //m_show = 0;
                //txtPCode.Value = "E";
                initPlineCode();
                DTPicker1.Date = DateTime.Now;
                DTPicker2.Date = DateTime.Now;
                initZd();
            }
            //else
            //{
            //    m_show = 1;
            //}
            //initQy();
            //initfl();
            //initZd();
            //必须放在这里才能避免被刷新吗
            //放这里有问题
            //initJHDM();
            //生产线不为空且计划号不为空的时候才进行查询，否则数据量太大，显示地特慢  
            //修改：将计划号判断取消，因为会使页面一直有之前的数据，即使点击在制品查询也无法刷新数据
            //if (txtPCode.Text.Trim() != null && txtPCode.Text.Trim() != "" && List1.Items.Count >= 1)
            //if (txtPCode.Text.Trim() != null && txtPCode.Text.Trim() != "")
            //{
            setCondition();
            //}
        }

        //初始化gridview
        //private void setCondition()
        //{
        //    string sql = "";
        //    //查询发动机状态，不随出入库删除
        //    sql = "select A.ROWID,a.ghtm,b.PLAN_CODE,a.so,a.rqsj,a.ggxhmc,a.zdmc,a.bcmc,a.bzmc,a.ygmc "
        //        + " from sjcbb_ndel a "
        //        + " left join data_product b on b.SN=a.ghtm "
        //        + " left join code_station c on c.STATION_NAME=a.zdmc "
        //        + " left join data_plan d on d.PLAN_CODE=b.PLAN_CODE "
        //        + " where 1=1 ";
        //        //+" where 1=" + m_show + " ";
        //    if (txtPCode.Text.Trim() != "")
        //    {
        //        sql += " AND b.PLINE_CODE = '" + txtPCode.Value.ToString() + "' ";
        //    }
        //    //if (cmbJhdm.Text.Trim() != "" && Check1.Value.ToString() == "true")
        //    //{
        //    //    sql += " AND b.PLAN_CODE = '" + cmbJhdm.Text.Trim() + "' ";
        //    //    if (txtSO.Text.Trim() != "")
        //    //    {
        //    //        sql += " AND a.so = '" + txtSO.Text.Trim() + "' ";
        //    //    }
        //    //}
        //    //如果列表中没有条件，则查询的数据为空
        //    if (List1.Items.Count < 1)
        //    {
        //        sql += " AND b.PLAN_CODE = '' ";
        //    }
        //    //？？取不到选中的列，怎么设置只能选一行
        //    if (List1.Items.Count == 1 && Check1.Checked == true)
        //    {
        //        int listindex = List1.Items.IndexOfText("--");
        //        //这里只取一行吗？
        //        string planCode = List1.SelectedItems[0].ToString().Substring(0, listindex);
        //        sql += " AND B.PLAN_CODE = '" + planCode + "' ";
        //        if (txtSO.Text.Trim() != "")
        //        {
        //            sql += " AND A.SO = '" + txtSO.Text.Trim() + "' ";
        //        }
        //    }
        //    if (Check1.Checked == false && List1.Items.Count >= 1)
        //    {
        //        //为什么获取不到，因为IndexOfText取的是文本对应item在items中的位置
        //        //int listindex = List1.Items.IndexOfText("2016P0926-01--SO22064");
        //        //test
        //        string item = List1.Items[0].ToString();
        //        int listindex = item.IndexOf("--");
        //        if (listindex >= 0)
        //        {
        //            //不能用SelectedItems，好像取不到，待测试
        //            sql += " AND ( b.PLAN_CODE = '" + List1.Items[0].ToString().Substring(0, List1.Items[0].ToString().IndexOf("--")) + "' ";
        //            for (int i = 2; i <= List1.Items.Count; i++)
        //            {
        //                listindex = List1.Items[i - 1].ToString().IndexOf("--");
        //                sql += " OR B.PLAN_CODE = '" + List1.Items[i - 1].ToString().Substring(0, listindex) + "' ";
        //            }
        //            sql += ") ";
        //        }
        //    }
        //    //if (cmbJhdm.Text.Trim() != "")
        //    //{
        //    //    sql += " AND b.PLAN_CODE = '" + cmbJhdm.Text.Trim() + "' ";
        //    //}
        //    if (cmbzd.Text.Trim() != "")
        //    {
        //        sql += " AND a.ZDMC = '" + cmbzd.Text.Trim() + "' ";
        //    }
        //    if (cmbqy.Text.Trim() != "")
        //    {
        //        sql += " AND a.ZDQY = '" + cmbqy.Text.Trim() + "' ";
        //    }
        //    if (cmbfl.Text.Trim() != "")
        //    {
        //        sql += " AND a.ZDFL = '" + cmbfl.Text.Trim() + "' ";
        //    }
        //    if (txtGhtm.Text.Trim() != "")
        //    {
        //        sql += " AND a.GHTM = '" + txtGhtm.Text.Trim() + "' ";
        //    }
        //    //数据量太大，先取前二十条数据
        //    //sql += " AND ROWNUM <= 20 ";

        //    sql += " ORDER BY A.GHTM,A.RQSJ ";

        //    DataTable dt = dc.GetTable(sql);
        //    ASPxGridView1.DataSource = dt;
        //    ASPxGridView1.DataBind();
        //}

        //初始化gridview
        private void setCondition()
        {
            string sql = "";
            //查询发动机状态，不随出入库删除
            sql = " SELECT A.ROWID,A.SN,A.PLAN_CODE,A.PLAN_SO,A.START_TIME,A.PRODUCT_MODEL,B.STATION_NAME,C.SHIFT_NAME,D.TEAM_NAME,E.USER_NAME "
                + " FROM DATA_STORE_NDEL A "
                + " LEFT JOIN CODE_STATION B ON B.STATION_CODE = A.STATION_CODE "
                + " LEFT JOIN CODE_SHIFT C ON C.SHIFT_CODE = A.SHIFT_CODE "
                + " LEFT JOIN CODE_TEAM D ON D.TEAM_CODE = A.TEAM_CODE "
                + " LEFT JOIN CODE_USER E ON E.USER_NAME = A.USER_ID "
                + " WHERE 1=1 ";
            //若生产线为空，则取生产线为空的值
            if (txtPCode.Text.Trim() == "")
            {
                sql += " AND A.PLINE_CODE = '' ";
            }
            if (txtPCode.Text.Trim() != "")
            {
                sql += " AND A.PLINE_CODE = '" + txtPCode.Value.ToString() + "' ";
            }
            //如果列表中没有条件，则查询的数据为空
            if (List1.Items.Count < 1)
            {
                sql += " AND A.PLAN_CODE = '' ";
            }
            //？？取不到选中的列，怎么设置只能选一行
            if (List1.Items.Count == 1 && Check1.Checked == true)
            {
                int listindex = List1.Items.IndexOfText("--");
                //这里只取一行吗？
                string planCode = List1.SelectedItems[0].ToString().Substring(0, listindex);
                sql += " AND A.PLAN_CODE = '" + planCode + "' ";
                if (txtSO.Text.Trim() != "")
                {
                    sql += " AND A.SO = '" + txtSO.Text.Trim() + "' ";
                }
            }
            if (Check1.Checked == false && List1.Items.Count >= 1)
            {
                //为什么获取不到，因为IndexOfText取的是文本对应item在items中的位置
                //int listindex = List1.Items.IndexOfText("2016P0926-01--SO22064");
                //test
                string item = List1.Items[0].ToString();
                int listindex = item.IndexOf("--");
                if (listindex >= 0)
                {
                    //不能用SelectedItems，好像取不到，待测试
                    sql += " AND ( A.PLAN_CODE = '" + List1.Items[0].ToString().Substring(0, List1.Items[0].ToString().IndexOf("--")) + "' ";
                    for (int i = 2; i <= List1.Items.Count; i++)
                    {
                        listindex = List1.Items[i - 1].ToString().IndexOf("--");
                        sql += " OR A.PLAN_CODE = '" + List1.Items[i - 1].ToString().Substring(0, listindex) + "' ";
                    }
                    sql += ") ";
                }
            }
            if (cmbzd.Text.Trim() != "")
            {
                sql += " AND B.STATION_NAME = '" + cmbzd.Text.Trim() + "' ";
            }
            //if (cmbqy.Text.Trim() != "")
            //{
            //    sql += " AND A.ZDQY = '" + cmbqy.Text.Trim() + "' ";
            //}
            //if (cmbfl.Text.Trim() != "")
            //{
            //    sql += " AND A.ZDFL = '" + cmbfl.Text.Trim() + "' ";
            //}
            //if (txtGhtm.Text.Trim() != "")
            //{
            //    sql += " AND A.GHTM = '" + txtGhtm.Text.Trim() + "' ";
            //}
            sql += " ORDER BY A.SN,A.START_TIME ";

            DataTable dt = dc.GetTable(sql);
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
        }

        //初始化生产线
        private void initPlineCode()
        {
            string sql = "select distinct a.pline_code,b.pline_name from vw_user_role_program a "
                + " left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" 
                + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'";
            sqlGzdd.SelectCommand = sql;
            sqlGzdd.DataBind();
            txtPCode.SelectedIndex = txtPCode.Items.Count >= 0 ? 0 : -1;
        }

        //初始化JHDM
        //private void initJHDM()
        //{
        //    string sql = " select distinct b.PLAN_CODE||'--'||b.PLAN_SO AS PLAN_CODE,b.PLAN_SEQ,  b.PLAN_QTY,b.BEGIN_DATE from DATA_PLAN b "
        //        + " where b.BEGIN_DATE>=to_date('" + DTPicker1.Date.ToString("yyyy/MM/dd") + "','yyyy-MM-dd') and b.BEGIN_DATE<=to_date('" + DTPicker2.Date.ToString("yyyy/MM/dd") + "','yyyy-MM-dd') "
        //        + " AND  B.PLINE_CODE = rh_get_data('G','" + txtPCode.Text.Trim() + "','','','') ";
        //    //string sql = " select distinct b.PLAN_CODE, b.PLAN_SO ,b.PLAN_SEQ,  b.PLAN_QTY,b.BEGIN_DATE from DATA_PLAN b WHERE "
        //    //    + " b.BEGIN_DATE>=to_date('" + DTPicker1.Date.ToString("yyyy/MM/dd") + "','yyyy-MM-dd') and b.BEGIN_DATE<=to_date('" + DTPicker2.Date.ToString("yyyy/MM/dd") + "','yyyy-MM-dd') AND "
        //    //    + " B.PLINE_CODE in(select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') ";
        //    if (txtSO.Text.Trim() != "")
        //    {
        //        sql += " AND b.PLAN_SO = '" + txtSO.Text.Trim() + "' ";
        //    }
        //    sql += " ORDER BY PLAN_CODE ";

        //    //sqlJhdm.SelectCommand = sql;
        //    //sqlJhdm.DataBind();

        //    DataTable dt = dc.GetTable(sql);
        //    listJhdm.DataSource = dt;
        //    listJhdm.DataBind();
        //}
        //使用callback初始化JHDM
        protected void listJhdm_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string flag = e.Parameter.ToString();
            if (flag == "查询计划")
            {
                //重新查询计划时要把已选的计划号列表清空一下
                List1.Items.Clear();

                ASPxListBox jhdmList = sender as ASPxListBox;

                string sql = " select distinct b.PLAN_CODE||'--'||b.PLAN_SO AS PLAN_CODE,b.PLAN_SEQ,  b.PLAN_QTY,b.BEGIN_DATE from DATA_PLAN b "
                    + " where b.BEGIN_DATE>=to_date('" + DTPicker1.Date.ToString("yyyy/MM/dd") + "','yyyy-MM-dd') and b.BEGIN_DATE<=to_date('" + DTPicker2.Date.ToString("yyyy/MM/dd") + "','yyyy-MM-dd') ";
                if (txtPCode.Text.Trim() != "")
                {
                    sql += " AND b.PLINE_CODE = '" + txtPCode.Value.ToString() + "' ";
                }
                //string sql = " select distinct b.PLAN_CODE, b.PLAN_SO ,b.PLAN_SEQ,  b.PLAN_QTY,b.BEGIN_DATE from DATA_PLAN b WHERE "
                //    + " b.BEGIN_DATE>=to_date('" + DTPicker1.Date.ToString("yyyy/MM/dd") + "','yyyy-MM-dd') and b.BEGIN_DATE<=to_date('" + DTPicker2.Date.ToString("yyyy/MM/dd") + "','yyyy-MM-dd') AND "
                //    + " B.PLINE_CODE in(select pline_code from vw_user_role_program where user_id='" + theUserId + "' and program_code='" + theProgramCode + "' and company_code='" + theCompanyCode + "') ";
                if (txtSO.Text.Trim() != "")
                {
                    sql += " AND b.PLAN_SO = UPPER('" + txtSO.Text.Trim() + "') ";
                }
                sql += " ORDER BY PLAN_CODE ";

                //sqlJhdm.SelectCommand = sql;
                //sqlJhdm.DataBind();

                DataTable dt = dc.GetTable(sql);
                jhdmList.DataSource = dt;
                jhdmList.DataBind();
            }
            if (flag == "增加")
            {
                //DataTable dt = new DataTable();
                //dt.Columns.Add("PLAN_CODE");
                //if (listJhdm.Items.Count != 0 && listJhdm.Items != null)
                //{
                //    for (int i = 0; i < listJhdm.Items.Count; i++)
                //    {
                //        dt.Rows.Add(listJhdm.Items[i].ToString());
                //    }
                //}
                //listJhdm.DataSource = dt;
                //listJhdm.DataBind();
            }
        }

        //初始化站点
        private void initZd()
        {
            string sql = " SELECT DISTINCT a.STATION_CODE FROM DATA_STORE a left join CODE_STATION b on b.STATION_NAME=a.STATION_CODE "
                + " where 1=1 "
                //+ " AND A.PLINE_CODE =  '" + txtPCode.Items[0] + "' "
                + " AND a.PLINE_CODE IN (select distinct c.pline_code from vw_user_role_program c where c.user_id='" + theUserId + "' and c.program_code='" + theProgramCode + "' and c.company_code='" + theCompanyCode + "') ";
            if (txtPCode.Text.Trim() != "")
            {
                sql += " AND A.PLINE_CODE = '" + txtPCode.Value.ToString() + "' ";
            }
            //第一次进入页面生产线值取不到的解决方法
            //若生产线为空则取默认值
            if (txtPCode.Text.Trim() == "")
            {
                string plineSql = "select distinct a.pline_code,b.pline_name from vw_user_role_program a "
                + " left join code_product_line b on a.pline_code=b.pline_code where a.user_id='"
                + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'";
                DataTable dt = dc.GetTable(plineSql);
                //注意此处用rows才能取到，用columns取不到
                sql += " AND A.PLINE_CODE = '" + dt.Rows[0]["PLINE_CODE"].ToString() + "' ";
            }
            if (cmbqy.Text.Trim() != "")
            {
                sql += " AND B.STATION_AREA = '" + cmbqy.Text.Trim() + "' ";
            }
            if (cmbfl.Text.Trim() != "")
            {
                //??站点分类应该对应STATION_TYPE_CODE
                sql += " AND B.STATION_TYPE = '" + cmbfl.Text.Trim() + "' ";
            }
            sql += " ORDER BY A.STATION_CODE ";

            //sqlzd.SelectCommand = sql;
            //sqlzd.DataBind();
            cmbzd.DataSource = dc.GetTable(sql);
            cmbzd.DataBind();
        }

        //初始化站点类别
        private void initQy()
        {
            //CODE_STATION里没有PLINE_CODE,VW_CODE_STATION里有
            string sql = "SELECT DISTINCT a.STATION_AREA FROM  VW_CODE_STATION a "
                + " where 1=1 ";
            if (txtPCode.Text.Trim() != "")
            {
                sql += " AND A.PLINE_CODE = '" + txtPCode.Value.ToString() + "' ";
            }
            sql += " ORDER BY A.STATION_AREA ";

            sqlqy.SelectCommand = sql;
            sqlqy.DataBind();
        }

        //初始化站点分类
        private void initfl()
        {
            string sql = "SELECT DISTINCT a.STATION_TYPE FROM  VW_CODE_STATION a "
                + " where 1=1 ";
            if (txtPCode.Text.Trim() != "")
            {
                sql += " AND A.PLINE_CODE = '" + txtPCode.Value.ToString() + "' ";
            }
            sql += " ORDER BY A.STATION_TYPE ";

            sqlfl.SelectCommand = sql;
            sqlfl.DataBind();
        }

        //初始化list
        //private void init_list()
        //{
        //    string sql = "SELECT PLAN_SO,PLAN_QTY,PLAN_CODE FROM DATA_PLAN WHERE PLAN_CODE='"
        //        + cmbJhdm.Text.Trim() + "' and PLAN_SO='" + txtSO.Text.Trim() + "'";

        //    DataTable dt = dc.GetTable(sql);
        //    List1.DataSource = dt;
        //    List1.DataBind();
        //}

        //查询计划
        //protected void ASPxButton1_Click(object sender, EventArgs e)
        //{
        //    initJHDM();
        //}

        //在制品查询
        //protected void query_Click(object sender, EventArgs e)
        //{
        //    //setCondition();
        //}

        protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            setCondition();
        }

        protected void cmbzd_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            //initZd();
            ASPxComboBox station = (ASPxComboBox)sender;
            string sql = " SELECT DISTINCT a.STATION_CODE FROM DATA_STORE a left join CODE_STATION b on b.STATION_NAME=a.STATION_CODE "
                + " where 1=1 "
                + " AND a.PLINE_CODE IN (select distinct c.pline_code from vw_user_role_program c where c.user_id='" + theUserId + "' and c.program_code='" + theProgramCode + "' and c.company_code='" + theCompanyCode + "') ";
            if (txtPCode.Text.Trim() != "")
            {
                sql += " AND A.PLINE_CODE = '" + txtPCode.Value.ToString() + "' ";
            }
            if (cmbqy.Text.Trim() != "")
            {
                sql += " AND B.STATION_AREA = '" + cmbqy.Text.Trim() + "' ";
            }
            if (cmbfl.Text.Trim() != "")
            {
                //??站点分类应该对应STATION_TYPE_CODE
                sql += " AND B.STATION_TYPE = '" + cmbfl.Text.Trim() + "' ";
            }
            sql += " ORDER BY A.STATION_CODE ";
            DataTable dt = dc.GetTable(sql);
            //sqlzd.SelectCommand = sql;
            //sqlzd.DataBind();
            station.DataSource = dt;
            station.DataBind();
        }

        protected void List1_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            //ASPxListBox jhdmList = sender as ASPxListBox;

            string sql = " select distinct b.PLAN_CODE||'--'||b.PLAN_SO AS PLAN_CODE,b.PLAN_SEQ,  b.PLAN_QTY,b.BEGIN_DATE from DATA_PLAN b "
                + " where b.BEGIN_DATE>=to_date('" + DTPicker1.Date.ToString("yyyy/MM/dd") + "','yyyy-MM-dd') and b.BEGIN_DATE<=to_date('" + DTPicker2.Date.ToString("yyyy/MM/dd") + "','yyyy-MM-dd') ";
            if (txtPCode.Text.Trim() != "")
            {
                sql += " AND b.PLINE_CODE = '" + txtPCode.Value.ToString() + "' ";
            }
            if (txtSO.Text.Trim() != "")
            {
                sql += " AND b.PLAN_SO = UPPER('" + txtSO.Text.Trim() + "') ";
            }
            sql += " ORDER BY PLAN_CODE ";

            DataTable dt = dc.GetTable(sql);
            //jhdmList.DataSource = dt;
            //jhdmList.DataBind();
            List1.DataSource = dt;
            List1.DataBind();

            //DataTable dt1 = new DataTable();
            //dt1.Columns.Add("PLAN_CODE");
            ////items取不到值，为什么
            //if (listJhdm.Items.Count != 0 && listJhdm.Items != null)
            //{
            //    for (int i = 0; i < listJhdm.Items.Count; i++)
            //    {
            //        dt1.Rows.Add(listJhdm.Items[i].ToString());
            //    }
            //}
            //if (List1.SelectedItems.Count != 0 && List1.SelectedItems != null)
            //{
            //    for (int i = 0; i < List1.SelectedItems.Count; i++)
            //    {
            //        dt1.Rows.Add(List1.SelectedItems[i].ToString());
            //    }
            //}
            //listJhdm.DataSource = dt1;
            //listJhdm.DataBind();
            //DataTable dt2 = new DataTable();
            //dt2.Columns.Add("PLAN_CODE");
            //string selectedItems = "";
            //for (int i = 0; i < List1.SelectedItems.Count; i++)
            //{
            //    //contains判断的是字符串是否在字符串中，需先把items拼接起来
            //    selectedItems += List1.SelectedItems[i].ToString();
            //}
            //for (int i = 0; i < List1.Items.Count; i++)
            //{
            //    Boolean flag = selectedItems.Contains(List1.Items[i].ToString());
            //    if (!selectedItems.Contains(List1.Items[i].ToString()))
            //    {
            //        dt2.Rows.Add(List1.Items[i].ToString());
            //    }
            //}
            //List1.DataSource = dt2;
            //List1.DataBind();
            //因为取不到前台的items，所以换方法
            //string[] items = e.Parameter.ToString().Split("@".ToCharArray());
            //if (items.Length == 0 || items == null)
            //{
            //    return;
            //}
            //string items1 = items[0];
            //string[] leftItems = items1.Split("&".ToCharArray());
            //string items2 = items[1];
            //string[] rightItems = items2.Split("&".ToCharArray());
            //string items3 = items[2];
            //string[] rightSelectedItems = items3.Split("&".ToCharArray());
            //DataTable dt1 = new DataTable();
            //dt1.Columns.Add("PLAN_CODE");
            //for (int i = 0; i < leftItems.Length; i++)
            //{
            //    dt1.Rows.Add(leftItems[i]);
            //}
            //for (int i = 0; i < rightSelectedItems.Length; i++)
            //{
            //    dt1.Rows.Add(rightSelectedItems[i]);
            //}
            //listJhdm.DataSource = dt1;
            //listJhdm.DataBind();
            ////动态数组声明方法，必须用new
            //ArrayList rightUnselectedItems = new ArrayList();
            ////怎么取出两个数组中不同的数据，值得研究
            //for (int i = 0; i < rightItems.Length; i++)
            //{
            //    if (!rightSelectedItems.Contains(rightItems[i]))
            //    {
            //        rightUnselectedItems.Add(rightItems[i]);
            //    }
            //}
            //DataTable dt2 = new DataTable();
            //dt2.Columns.Add("PLAN_CODE");
            //if (rightUnselectedItems.Count != 0 || rightUnselectedItems != null)
            //{
            //    for (int i = 0; i < rightUnselectedItems.Count; i++)
            //    {
            //        dt2.Rows.Add(rightUnselectedItems[i]);
            //    }
            //}
            //List1.DataSource = dt2;
            //List1.DataBind();
        }


    }
}