using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rmes.DA.Procedures;
using System.Data;

using Rmes.Web.Base;
using Rmes.Pub.Data;

using DevExpress.Utils;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxClasses;
using Rmes.DA.Base;

/**
 * 功能概述：在制品物料清单
 * 作者：任海
 * 创建时间：2016-09-18
 */
namespace Rmes.WebApp.Rmes.Rept.rept3200
{
    public partial class rept3200 : BasePage
    {
        private dataConn dc = new dataConn();
        public string theProgramCode;
        public string theCompanyCode;
        private string theUserId;

        protected void Page_Load(object sender, EventArgs e)
        {
            theProgramCode = "rept3200";
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();
            theUserId = theUserManager.getUserId();

            if (!IsPostBack)
            {
                //放在外面每次都初始化
                //对调序有影响，先不设默认值??
                //txtPCode.Value = "E";
            }
            initPlineCode();
            //init_CmbZddm();
            setCondition1();
            setCondition();
        }

        //初始化生产线
        private void initPlineCode()
        {
            string sql = "select distinct a.pline_code,b.pline_name from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code where a.user_id='" + theUserId + "' and a.program_code='" + theProgramCode + "' and a.company_code='" + theCompanyCode + "'";
            SqlCode.SelectCommand = sql;
            SqlCode.DataBind();
            SqlCode1.SelectCommand = sql;
            SqlCode1.DataBind();
            //对调序有影响，先不设默认值
            //txtPCode.SelectedIndex = txtPCode.Items.Count >= 0 ? 0 : -1;
        }

        //对ASPxGridView1进行数据绑定
        private void setCondition1()
        {
            string sql = "select ROWID,sx,zddm from zdxlb where gzdd = rh_get_data('G','" + txtPCode.Text.Trim() + "','','','') ";
            //??页面第一次进去并没有初始化生产线，此时调序会与显示不一样
            //生产线下拉框查询
            if (txtPCode.Text.Trim() != "")
            {
                sql += " AND GZDD = RH_GET_DATA('G','" + txtPCode.Text.Trim() + "','','','') ";
            }
            sql += " order by sx,ZDDM  ";

            DataTable dt = dc.GetTable(sql);
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
        }

        //初始化物料清单
        private void setCondition()
        {
            string sql = "";
            sql = "select a.abom_comp,sum(a.abom_qty) AS SUM from rst_bom_zzpwlqd a "
                //REL_STATION_LOCATION里没有STATION_NAME，VW_REL_STATION_LOCATION里有
                + "join (select b.sx,b.zddm,c.LOCATION_CODE AS GWDM from zdxlb b join VW_REL_STATION_LOCATION c on c.STATION_NAME=b.zddm ) d on a.abom_wkctr=d.gwdm "  
                + "Join (select e.SN,f.sx from DATA_STORE e join zdxlb f on e.STATION_CODE=f.zddm ) g on a.ghtm=g.SN "   
                + "Where d.sx <= g.sx ";
            sql += "group by a.abom_comp,a.abom_qty order by a.abom_comp ";

            DataTable dt = dc.GetTable(sql);
            ASPxGridView2.DataSource = dt;
            ASPxGridView2.DataBind();
        }

        //在制品物料清单查询
        //protected void btnQuery_Click(object sender, EventArgs e)
        //{
        //    string sql = "delete from rst_bom_zzpwlqd ";
        //    dc.ExeSql(sql);
        //    for (int i = 0; i < ASPxListBoxUsed.Items.Count; i++)
        //    {
        //        string ghtm = ASPxListBoxUsed.Items[i].ToString();

        //        RST_CREATE_BOM_ZZPWLQD sp = new RST_CREATE_BOM_ZZPWLQD()
        //        {
        //            GHTM1 = ghtm
        //        };
        //        Procedure.run(sp);
        //    }

        //    setCondition();
        //}

        //在制品物料清单查询
        protected void ASPxGridView2_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            string sql = "delete from rst_bom_zzpwlqd ";
            dc.ExeSql(sql);
            for (int i = 0; i < ASPxListBoxUsed.Items.Count; i++)
            {
                string ghtm = ASPxListBoxUsed.Items[i].ToString();

                RST_CREATE_BOM_ZZPWLQD sp = new RST_CREATE_BOM_ZZPWLQD()
                {
                    GHTM1 = ghtm
                };
                Procedure.run(sp);
            }

            setCondition();
        }

        //初始化JHDM
        private void initJhdm()
        {
            string sql = "SELECT DISTINCT PLINE_CODE FROM VW_USER_ROLE_PROGRAM WHERE USER_ID = '" + theUserId
                + "' AND PROGRAM_CODE = '" + theProgramCode + "' AND COMPANY_CODE = '" + theCompanyCode + "' "
                + " ORDER BY PLINE_CODE";

            //sqlJhdm.SelectCommand = sql;
            //sqlJhdm.DataBind();
        }

        //初始化站点
        //private void init_CmbZddm()
        //{
        //    string sql = "select STATION_CODE,STATION_NAME from VW_CODE_STATION where PLINE_CODE = rh_get_data('G','" + txtPCode.Text.Trim() + "','','','') "
        //        + " order by STATION_NAME";

        //    DataTable dt = dc.GetTable(sql);
        //    listZD.DataSource = dt;
        //    listZD.DataBind();
        //}
        //用callback初始化站点
        protected void listZD_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string sql = "select STATION_CODE,STATION_NAME from VW_CODE_STATION where PLINE_CODE = rh_get_data('G','" + txtPCode.Text.Trim() + "','','','') "
                + " order by STATION_NAME";

            DataTable dt = dc.GetTable(sql);
            listZD.DataSource = dt;
            listZD.DataBind();
        }

        //查询
        //protected void query_Click(object sender, EventArgs e)
        //{
        //    init_listLSH();
        //}
        //初始化流水号列表
        protected void ASPxListBoxUnused_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string sql = "select ghtm from zzplshb where 1 = 1 ";
            if (txtPCode1.Text.Trim() != "")
            {
                sql += " AND gzdd = rh_get_data('G','" + txtPCode1.Text.Trim() + "','','','') ";
            }
            else
            {
                sql += " AND GZDD = '' ";
            }

            DataTable dt = dc.GetTable(sql);
            ASPxListBoxUnused.DataSource = dt;
            ASPxListBoxUnused.DataBind();
        }

        //初始化流水号列表
        //private void init_listLSH()
        //{
        //    string sql = "select ghtm from zzplshb where 1 = 1 ";
        //    if (txtPCode1.Text.Trim() != "")
        //    {
        //        sql += " AND gzdd = rh_get_data('G','" + txtPCode1.Text.Trim() + "','','','') ";
        //    }
        //    else
        //    {
        //        sql += " AND GZDD = '' ";
        //    }

        //    DataTable dt = dc.GetTable(sql);
        //    ASPxListBoxUnused.DataSource = dt;
        //    ASPxListBoxUnused.DataBind();
        //}

        //调序
        public void ASPxGridView1_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
        {
            string s = e.Parameters;
            string[] s1 = s.Split('|');
            string type1 = s1[0];
            int rowIndex = int.Parse(s1[1]);
            string sql = "";
            string rowid, sx, zddm;

            ASPxGridView atl1 = (ASPxGridView)sender;
            int count1 = atl1.Selection.Count;
            List<object> aa = atl1.GetSelectedFieldValues("ROWID");

            try
            {
                rowid = ASPxGridView1.GetRowValues(rowIndex, "ROWID").ToString();
                sx = ASPxGridView1.GetRowValues(rowIndex, "SX").ToString();
                zddm = ASPxGridView1.GetRowValues(rowIndex, "ZDDM").ToString();
            }
            catch
            {
                e.Result = "Fail,缺少关键值！";
                return;
            }
            switch (type1)
            {
                case "Up":
                    sql = "SELECT MAX(SX) FROM ZDXLB WHERE SX<'" + sx + "' AND gzdd = rh_get_data('G','" + txtPCode.Text.Trim() + "','','','') ";
                    dc.setTheSql(sql);
                    string newsx = dc.GetTable().Rows[0][0].ToString();
                    if (dc.GetTable().Rows.Count == 0 || newsx == "")
                    {
                        e.Result = "Fail,当前已最小序！";
                        break;
                    }
                    //??添加ROWNUM是防止多个序号一样的全部改变序号
                    sql = "update ZDXLB set SX='" + sx + "' where SX='" + newsx + "' AND ROWNUM = '1' AND gzdd = rh_get_data('G','" + txtPCode.Text.Trim() + "','','','') ";
                    dc.ExeSql(sql);
                    sql = "update ZDXLB set SX='" + newsx + "' where ROWID='" + rowid + "' ";
                    dc.ExeSql(sql);
                    //e.Result = "OK,上调成功！";
                    break;
                case "Down":
                    sql = "SELECT MIN(SX) FROM ZDXLB WHERE SX>'" + sx + "' AND gzdd = rh_get_data('G','" + txtPCode.Text.Trim() + "','','','') ";
                    dc.setTheSql(sql);
                    newsx = dc.GetTable().Rows[0][0].ToString();
                    if (dc.GetTable().Rows.Count == 0 || newsx == "")
                    {
                        e.Result = "Fail,当前已最大序！";
                        break;
                    }
                    //??添加ROWID是防止多个序号一样的全部改变序号
                    sql = "update ZDXLB set SX='" + sx + "' where SX='" + newsx + "' AND ROWNUM = '1' AND gzdd = rh_get_data('G','" + txtPCode.Text.Trim() + "','','','') ";
                    dc.ExeSql(sql);
                    sql = "update ZDXLB set SX='" + newsx + "' where ROWID='" + rowid + "' ";
                    dc.ExeSql(sql);
                    //e.Result = "OK,下调成功！";
                    break;
                default:

                    break;
            }
        }

        //删除
        protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ThisZddm = e.Values["ZDDM"].ToString();
            string sx = e.Values["SX"].ToString();

            string sql = "delete from zdxlb where zddm='" + ThisZddm + "' ";
            dc.ExeSql(sql);
            string sql1 = " SELECT * FROM ZDXLB WHERE SX > '" + sx + "' ";
            dc.setTheSql(sql1);
            if (dc.GetTable().Rows.Count >= 1)
            {
                int a = dc.GetTable().Rows.Count;
                //删掉后将所有比删掉序号大的序号都减1
                for (int i = 0; i < a; i++)
                {
                    string sql2 = " UPDATE ZDXLB SET SX = " + dc.GetTable().Rows[i][1] + " - 1 WHERE SX = " + dc.GetTable().Rows[i][1] + " ";
                    dc.ExeSql(sql2);
                }
            }
            else
            {
                return;
            }

            e.Cancel = true;
            setCondition1();
        }

        //将list里的数据插入
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            for (int a = 0; a < ListInsert.Items.Count; a++)
            {
                string zddm = ListInsert.Items[a].ToString();

                string sql = "select * from zdxlb where zddm='" + zddm + "'";
                dc.setTheSql(sql);
                if (dc.GetTable().Rows.Count != 0)
                {
                    showAlert(this, "该站点代码已存在！");
                    //return;
                }
                else
                {
                    sql = "select sx from zdxlb where gzdd = rh_get_data('G','" + txtPCode.Text.Trim() + "','','','') order by sx desc";
                    dc.setTheSql(sql);
                    string sx = dc.GetTable().Rows[0][0].ToString();
                    sql = "insert into zdxlb (zddm,sx,gzdd)values('" + zddm + "'," + sx + "+1 ,rh_get_data('G','" + txtPCode.Text.Trim() + "','','','') ) ";
                    dc.ExeSql(sql);
                }
            }
            //清空ListInsert
            ListInsert.Items.Clear();
            setCondition1();
        }

        //protected void ASPxGridView1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        //{
        //    string flag = e.Parameters.ToString();
        //    if (flag == "插入")
        //    {
        //        for (int a = 0; a < ListInsert.Items.Count; a++)
        //        {
        //            string zddm = ListInsert.Items[a].ToString();

        //            string sql = "select * from zdxlb where zddm='" + zddm + "'";
        //            dc.setTheSql(sql);
        //            if (dc.GetTable().Rows.Count != 0)
        //            {
        //                showAlert(this, "该站点代码已存在！");
        //                return;
        //            }
        //            else
        //            {
        //                sql = "select sx from zdxlb where gzdd = rh_get_data('G','" + txtPCode.Text.Trim() + "','','','') order by sx desc";
        //                dc.setTheSql(sql);
        //                string sx = dc.GetTable().Rows[0][0].ToString();
        //                sql = "insert into zdxlb (zddm,sx,gzdd)values('" + zddm + "'," + sx + "+1 ,rh_get_data('G','" + txtPCode.Text.Trim() + "','','','') ) ";
        //                dc.ExeSql(sql);
        //            }

        //        }
        //        setCondition1();
        //    }
        //}

        //导出物料清单 EXCEL
        protected void btnXlsExport_Bom_List(object sender, EventArgs e)
        {
            ASPxGridViewExporter1.WriteXlsToResponse("Plan_BOM_List");
        }

        //前端弹出alert消息
        public static void showAlert(System.Web.UI.Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + msg.ToString() + "');</script>");
        }


    }
}