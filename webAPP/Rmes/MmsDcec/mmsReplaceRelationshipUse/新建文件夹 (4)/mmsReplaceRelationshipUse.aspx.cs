using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rmes.Pub.Data;
using DevExpress.Web.ASPxEditors;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Utils;
using Oracle.DataAccess.Client;
using Rmes.DA.Factory;

namespace Rmes.WebApp.Rmes.MmsDcec.mmsReplaceRelationshipUse
{
    public partial class mmsReplaceRelationshipUse : System.Web.UI.Page
    {
        private dataConn dc = new dataConn();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["gridOneReplace"] = "";
                Session["gridMaterial"] = "";
                Session["gridMulitiReplace"] = "";
                Session["RRFZSO"] = "";
                Session["RRFZTYPE"] = "";
                dtRq.Date = DateTime.Today;
            }
        }
        protected void cmbPline_Init(object sender, EventArgs e)
        {
            //初始化生产线下拉列表
            userManager theUserManager = (userManager)Session["theUserManager"];
            string userId = theUserManager.getUserId();

            string sql = "select a.pline_code,b.pline_name "
                + "from vw_user_role_program a left join code_product_line b on a.pline_code=b.pline_code "
                + "where a.user_id='" + userId + "' and a.program_code='mmsReplaceRelationshipUse' order by pline_name";
            (sender as ASPxComboBox).DataSource = dc.GetTable(sql);
            (sender as ASPxComboBox).ValueField = "pline_code";
            (sender as ASPxComboBox).TextField = "pline_code";
            (sender as ASPxComboBox).DataBind();
        }
        protected void gridOneReplace_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            string par = e.Parameters as string;
            string[] parstr = par.Split(';');
            string so = parstr[0];
            string planCode = parstr[1];

            Session["plineCode"] = cmbPline.Text;
            Session["planCode"] = planCode;
            Session["so"] = so;
            try
            {
                if (Session["RRFZTYPE"].ToString() == "B")
                    so = Session["RRFZSO"].ToString();
            }
            catch
            { }
            initOneMaterialReplace(so, planCode, "0");
        }

        private void initOneMaterialReplace(string so, string planCode, string qryType)
        {
            //初始化bom替换关系列表
            //0-一对一，1-多对多
            string thgxjx = "";
            string sql = "select config from copy_engine_property where upper(so)=upper('" + so + "')";
            DataTable dt = dc.GetTable(sql);
            if (dt.Rows.Count > 0)
            {
                thgxjx = dt.Rows[0][0].ToString();
                if (thgxjx.Substring(thgxjx.Length - 2, 2) == "ZZ")
                    thgxjx = thgxjx.Substring(0, thgxjx.Length - 2);
            }

            sql = " select AA.*,NVL(BB.ITEM_CODE,'无效') STATUS from (select a.OLDPART,b.PT_DESC2 OLDPART_NAME, a.NEWPART ,c.PT_DESC2 NEWPART_NAME,'',a.USETIME,a.ENDTIME  "
                + " from sjbomthset a"
                + " left outer join copy_pt_mstr b on a.oldpart=b.pt_part"
                + " left outer join copy_pt_mstr c on a.newpart=c.pt_part";
            if (thgxjx == "")
                sql = sql + " where so='" + so + "' and settype='0') AA ";
            else
                sql = sql + " where (so='" + so + "' or so='" + thgxjx + "') and settype='0') AA ";
                                                                            //plan_so='" + so + "' and
            sql += "left join (select * from VW_DATA_PLAN_STANDARD_BOM where  plan_code='" + planCode + "' AND ITEM_CODE not in (select xxptmp_comp from qad_xxptmp_mstr where xxptmp_jhdm='" + planCode + "'))  BB on AA.OLDPART=BB.ITEM_CODE ";
            sql += "order by aa.OLDPART";

            Session["gridOneReplace"] = sql;
            gridOneReplace.DataSource = dc.GetTable(sql);
            gridOneReplace.DataBind();
        }

        protected void gridMaterial_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            gridMaterial.Columns[0].Visible = false;
            string[] e11 = e.Parameters.Split('|');
            string btnFlag = e11[0];
            string so = "";
            string planCode = "";
            if (e11.Length > 1)
            {
                string[] str1 = e11[1].Split(';');
                //由计划显示bom
                so = str1[0];//=Session["so"].ToString();
                planCode = str1[1];// Session["planCode"].ToString();
            }
            else
            {
                so =Session["so"].ToString();
                planCode = Session["planCode"].ToString();
            }
            string sql = "";

            if (btnFlag == "init")
            {
                sql = "select ITEM_CODE ,ITEM_NAME ,ITEM_QTY ,PROCESS_CODE ,LOCATION_CODE ,ITEM_TYPE ,'' COMMENT1,VENDOR_CODE NEWPART  from VW_DATA_PLAN_STANDARD_BOM where PLAN_SO='" + so + "' and PLAN_CODE='" + planCode + "' order by LOCATION_CODE,PROCESS_CODE";
                Session["gridMaterial"] = sql;
                gridMaterial.DataSource = dc.GetTable(sql);
                gridMaterial.DataBind();

            }
            if (btnFlag == "supplier")
            {
                sql = " select ITEM_CODE ,ITEM_NAME ,ITEM_QTY ,PROCESS_CODE ,LOCATION_CODE ,ITEM_TYPE,'指定供应商' COMMENT1,NVL(b.xxptmp_rmks,' ') NEWPART  from VW_DATA_PLAN_STANDARD_BOM a "
                    + " join qad_xxptmp_mstr b on a.ITEM_CODE=b.xxptmp_comp and a.plan_code=b.xxptmp_jhdm "
                    + " where PLAN_SO='" + so + "' and PLAN_CODE='" + planCode + "' "
                    + " order by LOCATION_CODE,PROCESS_CODE";

                Session["gridMaterial"] = sql;
                gridMaterial.DataSource = dc.GetTable(sql);
                gridMaterial.DataBind();
            }
            if (btnFlag == "one")
            {
                Session["replaceType"] = "oneReplace";

                sql = "select config from copy_engine_property where upper(so)=upper('" + so + "')";
                DataTable dt = dc.GetTable(sql);
                string thgxjx = "";
                if (dt.Rows.Count > 0)
                {
                    thgxjx = dt.Rows[0][0].ToString();
                    if (thgxjx.Substring(thgxjx.Length - 2, 2) == "ZZ")
                        thgxjx = thgxjx.Substring(0, thgxjx.Length - 2);
                }

                sql = "select distinct ITEM_CODE ,ITEM_NAME ,ITEM_QTY ,PROCESS_CODE ,LOCATION_CODE ,ITEM_TYPE ,'替换' COMMENT1,b.NEWPART  "
                    + "from VW_DATA_PLAN_STANDARD_BOM a "
                    + "left join sjbomthset b on a.item_code=b.oldpart and (b.so='" + so + "' or b.so='" + thgxjx + "') and b.usetime<=to_date(to_char(sysdate,'YYYY-MM-DD'),'yyyy-mm-dd') and b.endtime>=to_date(to_char(sysdate,'YYYY-MM-DD'),'yyyy-mm-dd')  and b.settype='0' and b.oldpart not in (select xxptmp_comp from qad_xxptmp_mstr where xxptmp_jhdm='" + planCode + "')"
                    + "where PLAN_SO='" + so + "' and PLAN_CODE='" + planCode + "' ";
                //Session["gridMaterial"] = sql;
                //gridMaterial.DataSource = dc.GetTable(sql);
                //gridMaterial.DataBind();

                if (thgxjx == "")
                    sql = sql + " and ITEM_CODE in (select oldpart from sjbomthset where so='" + so
                        + "' and settype='0' and usetime<=to_date('" + DateTime.Today.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') and endtime>=to_date('" + DateTime.Today.ToString("yyyy-MM-dd") + "','yyyy-mm-dd'))";
                else
                    sql = sql + " and ITEM_CODE in (select oldpart from sjbomthset where (so='" + so
                        + "' or so='" + thgxjx + "') and settype='0' and usetime<=to_date('" + DateTime.Today.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') and endtime>=to_date('" + DateTime.Today.ToString("yyyy-MM-dd") + "','yyyy-mm-dd'))";

                sql = sql + " order by LOCATION_CODE,PROCESS_CODE";
                Session["gridMaterial"] = sql;
                gridMaterial.DataSource = dc.GetTable(sql);
                gridMaterial.DataBind();

                //显示替换按钮
                gridMaterial.Columns[0].Visible = true;
            }
            if (btnFlag == "multi")
            {
                Session["replaceType"] = "multiReplace";

                sql = "select config from copy_engine_property where upper(so)=upper('" + so + "')";
                DataTable dt = dc.GetTable(sql);
                string thgxjx = "";
                if (dt.Rows.Count > 0)
                {
                    thgxjx = dt.Rows[0][0].ToString();
                    if (thgxjx.Substring(thgxjx.Length - 2, 2) == "ZZ")
                        thgxjx = thgxjx.Substring(0, thgxjx.Length - 2);
                }

                sql = "select ITEM_CODE ,ITEM_NAME ,ITEM_QTY ,PROCESS_CODE ,LOCATION_CODE ,ITEM_TYPE,CASE WHEN xxptmp_comp IS NULL THEN '多重替换' END COMMENT1,'' NEWPART  "
                    + "from VW_DATA_PLAN_STANDARD_BOM a "
                    + "left join qad_xxptmp_mstr b on a.item_code=b.xxptmp_comp AND xxptmp_jhdm='" + planCode + "' "
                    + "where a.PLAN_SO='" + so + "' and a.PLAN_CODE='" + planCode + "' ";

                if (thgxjx == "")
                    sql = sql + " and ITEM_CODE in (select oldpart from sjbomthset where so='" + so + "' and settype='1' and usetime<=to_date('" + DateTime.Today.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') and endtime>=to_date('" + DateTime.Today.ToString("yyyy-MM-dd") + "','yyyy-mm-dd'))";
                else
                    sql = sql + " and ITEM_CODE in (select oldpart from sjbomthset where (so='" + so + "' or so='" + thgxjx + "') and settype='1' and usetime<=to_date('" + DateTime.Today.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') and endtime>=to_date('" + DateTime.Today.ToString("yyyy-MM-dd") + "','yyyy-mm-dd'))";
                sql += "";
                sql += " order by LOCATION_CODE,PROCESS_CODE";
                Session["gridMaterial"] = sql;
                gridMaterial.DataSource = dc.GetTable(sql);
                gridMaterial.DataBind();


                //显示替换按钮
                gridMaterial.Columns[0].Visible = true;
            }
        }
        public void ASPxGridView1_CustomDataCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomDataCallbackEventArgs e)
        {
            string s = e.Parameters;
            string[] s1 = s.Split('|');
            string type1 = s1[0];
            int rowIndex = int.Parse(s1[1]);
            string sql = "";
            string rmesid, planseq, begindate, plinecode1;

            try
            {
                rmesid = gridMaterial.GetRowValues(rowIndex, "ITEM_CODE").ToString();
                planseq = gridMaterial.GetRowValues(rowIndex, "NEWPART").ToString();
                begindate = gridMaterial.GetRowValues(rowIndex, "ITEM_QTY").ToString();
                plinecode1 = gridMaterial.GetRowValues(rowIndex, "LOCATION_CODE") as string;
            }
            catch
            {
                e.Result = "Fail,缺少关键值！";
                return;
            }
            return;
            //switch (type1)
            //{
            //    case "Up":
            //        sql = "select max(plan_seq) from data_plan where begin_date=to_date('" + begindate + "','yyyy-mm-dd') and plan_seq<'" + planseq + "' and pline_code='" + plinecode1 + "'  and plan_type='A'";
            //        dc.setTheSql(sql);
            //        string planseq1 = dc.GetTable().Rows[0][0].ToString();
            //        if (dc.GetTable().Rows.Count == 0 || planseq1 == "")
            //        {
            //            e.Result = "Fail,当前已最小序！";
            //            break;
            //        }

            //        sql = "update data_plan set plan_seq='" + planseq + "' where plan_seq='" + planseq1 + "' and  begin_date=to_date('" + begindate + "','yyyy-mm-dd') and pline_code='" + plinecode1 + "' and plan_type='A' ";
            //        dc.ExeSql(sql);
            //        sql = "update data_plan set plan_seq='" + planseq1 + "' where rmes_id='" + rmesid + "' ";
            //        dc.ExeSql(sql);
            //        //e.Result = "OK,上调成功！";
            //        break;
            //    default:

            //        break;
            //}
        }
        protected void gridMulitiReplace_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            //显示多对多替换
            string par = e.Parameters as string;
            string[] parstr = par.Split(';');
            string so = parstr[0];
            string planCode = parstr[1];
            try
            {
                if (Session["RRFZTYPE"].ToString() == "B")
                    so = Session["RRFZSO"].ToString();
            }
            catch
            { }
     
             initMultiMaterialReplace(so, planCode, "0");

        }
        private void initMultiMaterialReplace(string so, string planCode, string qryType)
        {
            //初始化bom替换关系列表
            //0-一对一，1-多对多
            string thgxjx = "";
            string sql = "select config from copy_engine_property where upper(so)=upper('" + so + "')";
            DataTable dt = dc.GetTable(sql);
            if (dt.Rows.Count > 0)
            {
                thgxjx = dt.Rows[0][0].ToString();
                if (thgxjx.Substring(thgxjx.Length - 2, 2) == "ZZ")
                    thgxjx = thgxjx.Substring(0, thgxjx.Length - 2);
            }

            sql = " select AA.*,case when usetime>to_date(to_char(sysdate,'yyyy-mm-dd'),'yyyy-mm-dd') then '无效' when  endtime<to_date(to_char(sysdate,'yyyy-mm-dd'),'yyyy-mm-dd') then '无效' when (aa.oldpart is not null and BB.ITEM_CODE is null ) then '无效' when aa.oldpart is null then '有效' else '有效' end  STATUS,to_char(usetime,'yyyy-mm-dd') usetime1,to_char(endtime,'yyyy-mm-dd') endtime1 from (select a.OLDPART,b.PT_DESC2 OLDPART_NAME, a.NEWPART ,c.PT_DESC2 NEWPART_NAME,'',a.THGROUP,a.USETIME,a.ENDTIME  "
                + " from sjbomthset a"
                + " left outer join copy_pt_mstr b on a.oldpart=b.pt_part"
                + " left outer join copy_pt_mstr c on a.newpart=c.pt_part";
            if (thgxjx == "")
                sql = sql + " where so='" + so + "' and settype='1') AA ";
            else
                sql = sql + " where (so='" + so + "' or so='" + thgxjx + "') and settype='1') AA ";

            sql += "left join (select * from VW_DATA_PLAN_STANDARD_BOM where plan_so='" + so + "' and plan_code='" + planCode + "' AND ITEM_CODE not in (select xxptmp_comp from qad_xxptmp_mstr where xxptmp_jhdm='" + planCode + "'))  BB on AA.OLDPART=BB.ITEM_CODE ";
            sql += "order by THGROUP,STATUS,aa.OLDPART ";

            Session["gridMulitiReplace"] = sql;
            DataTable dt1 = dc.GetTable(sql);
            dt1=Repair(dt1);
            gridMulitiReplace.DataSource = dt1;
            gridMulitiReplace.DataBind();
        }
        private DataTable Repair(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string str1 = dt.Rows[i]["THGROUP"].ToString();
                string str2 = dt.Rows[i]["STATUS"].ToString();
                if (str2 == "无效")
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (dt.Rows[j]["THGROUP"].ToString() == str1)
                        {
                            dt.Rows[j]["STATUS"] = "无效";
                        }
                    }
                }
            }
            return dt;
        }
        protected void gridMaterial_PageIndexChanged(object sender, EventArgs e)
        {
            string sql = Session["gridMaterial"] as string;
            (sender as ASPxGridView).DataSource = dc.GetTable(sql);
            (sender as ASPxGridView).DataBind();
        }

        protected void gridMulitiReplace_PageIndexChanged(object sender, EventArgs e)
        {
            string sql = Session["gridMulitiReplace"] as string;
            DataTable dt1 = dc.GetTable(sql);
            dt1 = Repair(dt1);
            (sender as ASPxGridView).DataSource = dt1;
            (sender as ASPxGridView).DataBind();
        }

        protected void gridOneReplace_PageIndexChanged(object sender, EventArgs e)
        {
            string sql = Session["gridOneReplace"] as string;
            (sender as ASPxGridView).DataSource = dc.GetTable(sql);
            (sender as ASPxGridView).DataBind();
        }

        protected void gridMaterial_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            if (e.VisibleIndex < 0) return;
            if (Session["replaceType"].ToString() == "oneReplace")
            {
                //如果bom里有匹配物料替换设定一对一替换的，显示替换按钮
                ASPxGridView grid = (ASPxGridView)sender;
                string itemCode = grid.GetRowValues(e.VisibleIndex, "ITEM_CODE") as string;
                string newPart = grid.GetRowValues(e.VisibleIndex, "NEWPART") as string;
                string locationCode = grid.GetRowValues(e.VisibleIndex, "LOCATION_CODE") as string;

                //已经替换的不显示按钮
                string sql = "select * from sjbomsoth where ljdm1='" + itemCode + "' and ljdm2='" + newPart + "' and jhdm='" + Session["planCode"].ToString()
                    + "' and so='" + Session["so"].ToString() + "'  and gwmc='" + locationCode + "' and gzdd='" + Session["plineCode"].ToString() + "'";
                bool useFlag = dc.GetState(sql);

                if (e.ButtonID == "OneReplace")
                {
                    if (newPart == "" || useFlag==true)
                        e.Visible = DefaultBoolean.False;
                }
                if (e.ButtonID == "MultiReplace")
                {
                    e.Visible = DefaultBoolean.False;
                }
            }

            //显示多对多替换按钮
            if (Session["replaceType"].ToString() == "multiReplace")
            {

                //如果bom里有匹配物料替换设定一对一替换的，显示替换按钮
                ASPxGridView grid = (ASPxGridView)sender;
                string comment1 = grid.GetRowValues(e.VisibleIndex, "COMMENT1") as string;
                if (e.ButtonID == "MultiReplace")
                {
                    if (comment1 == "多重替换")
                        e.Visible = DefaultBoolean.True;
                    else
                        e.Visible = DefaultBoolean.False;
                }
                if (e.ButtonID == "OneReplace")
                {
                    e.Visible = DefaultBoolean.False;
                }
            }
        }

        protected void cmdReplaceByPlan_Click(object sender, EventArgs e)
        {
            //按计划替换
            userManager theUserManager = (userManager)Session["theUserManager"];
            string userName = theUserManager.getUserName();

            dataConn theDataConn = new dataConn();
            theDataConn.theComd.CommandType = CommandType.StoredProcedure;
            theDataConn.theComd.CommandText = "MW_HANDLE_LJTH";

            theDataConn.theComd.Parameters.Add("JHRQ1", OracleDbType.Varchar2).Value = dtRq.Text;
            theDataConn.theComd.Parameters.Add("JHDM1", OracleDbType.Varchar2).Value = Session["planCode"].ToString();
            theDataConn.theComd.Parameters.Add("JHSO1", OracleDbType.Varchar2).Value = Session["so"].ToString();
            theDataConn.theComd.Parameters.Add("GZDD1", OracleDbType.Varchar2).Value = cmbPline.Value.ToString();
            theDataConn.theComd.Parameters.Add("USER1", OracleDbType.Varchar2).Value = userName;
            theDataConn.theComd.Parameters.Add("THRQBS1", OracleDbType.Varchar2).Value = "0";
            theDataConn.theComd.Parameters.Add("THLXBS1", OracleDbType.Varchar2).Value = "0";
            theDataConn.theComd.Parameters.Add("OUTSTR1", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
            theDataConn.OpenConn();
            theDataConn.theComd.ExecuteNonQuery();

            string retVal = theDataConn.theComd.Parameters["OUTSTR1"].Value.ToString();
            theDataConn.CloseConn();

            if (retVal == "0")
                showAlert(this, "处理完毕，请到手工处理界面检查需手工处理部分！");
            else
                showAlert(this, retVal);

        }

        protected void cmdReplaceByDate_Click(object sender, EventArgs e)
        {
            //按日期替换
            userManager theUserManager = (userManager)Session["theUserManager"];
            string userName = theUserManager.getUserName();


            dataConn theDataConn = new dataConn();
            theDataConn.theComd.CommandType = CommandType.StoredProcedure;
            theDataConn.theComd.CommandText = "MW_HANDLE_LJTH";

            theDataConn.theComd.Parameters.Add("JHRQ1", OracleDbType.Varchar2).Value = dtRq.Text;
            theDataConn.theComd.Parameters.Add("JHDM1", OracleDbType.Varchar2).Value = "";
            theDataConn.theComd.Parameters.Add("JHSO1", OracleDbType.Varchar2).Value = "";
            theDataConn.theComd.Parameters.Add("GZDD1", OracleDbType.Varchar2).Value = cmbPline.Value.ToString();
            theDataConn.theComd.Parameters.Add("USER1", OracleDbType.Varchar2).Value = userName;
            theDataConn.theComd.Parameters.Add("THRQBS1", OracleDbType.Varchar2).Value = "1";
            theDataConn.theComd.Parameters.Add("THLXBS1", OracleDbType.Varchar2).Value = "0";
            theDataConn.theComd.Parameters.Add("OUTSTR1", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
            theDataConn.OpenConn();
            theDataConn.theComd.ExecuteNonQuery();

            string retVal = theDataConn.theComd.Parameters["OUTSTR1"].Value.ToString();
            theDataConn.CloseConn();

            if (retVal == "0")
                 showAlert(this, "处理完毕，请到手工处理界面检查需手工处理部分！");
            else
                showAlert(this, retVal);

        }

        protected void gridOneReplace_Init(object sender, EventArgs e)
        {
            try
            {
                string sql = Session["gridOneReplace"] as string;
                (sender as ASPxGridView).DataSource = dc.GetTable(sql);
                (sender as ASPxGridView).DataBind();
            }
            catch
            { }
        }

        protected void gridMulitiReplace_Init(object sender, EventArgs e)
        {
            try
            {
            string sql = Session["gridMulitiReplace"] as string;
            DataTable dt1 = dc.GetTable(sql);
            dt1 = Repair(dt1);
            (sender as ASPxGridView).DataSource = dt1;
            (sender as ASPxGridView).DataBind();
            }
            catch
            { }
        }

        protected void gridOneReplace_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.VisibleIndex < 0) return;
            string status = e.GetValue("STATUS").ToString(); //失效黄色
            if (status == "无效")
            {
                e.Row.BackColor = System.Drawing.Color.Yellow;
            }

        }

        protected void gridMulitiReplace_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.VisibleIndex < 0) return;
            string status = e.GetValue("STATUS").ToString(); //失效黄色
            if (status == "无效")
            {
                e.Row.BackColor = System.Drawing.Color.Yellow;
            }
        }

        protected void gridMaterial_Init(object sender, EventArgs e)
        {
            try
            {
                string sql = Session["gridMaterial"] as string;
                (sender as ASPxGridView).DataSource = dc.GetTable(sql);
                (sender as ASPxGridView).DataBind();
            }
            catch
            { }
        }

        protected void gridMaterial_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            string oldPart = gridMaterial.GetRowValues(e.VisibleIndex, "ITEM_CODE").ToString();
            string newPart = gridMaterial.GetRowValues(e.VisibleIndex, "NEWPART").ToString();
            string locationCode = gridMaterial.GetRowValues(e.VisibleIndex, "LOCATION_CODE").ToString();
            string itemQty = gridMaterial.GetRowValues(e.VisibleIndex, "ITEM_QTY").ToString();

            string planCode = Session["planCode"].ToString();
            string so = Session["so"].ToString();
            string plineCode =cmbPline.Value.ToString();

            gridMaterial.JSProperties.Add("cpCallbackName", e.ButtonID.ToString());

            //一对一替换
            if (e.ButtonID == "OneReplace")
            {
                userManager theUserManager = (userManager)Session["theUserManager"];
                string userName = theUserManager.getUserName();

                //已经上线的计划需要走流程确认
                if (PubLogic.isEngOnline(planCode, so, oldPart, newPart, locationCode, plineCode))
                {

                    gridMaterial.JSProperties.Add("cpAlertContent", "计划已上线，将提交给调度确认.");

                    BomReplaceFactory.MW_MODIFY_SJBOMTHCFM("ADD", planCode, so, oldPart, newPart,
                        userName, locationCode, plineCode);
                }
                else
                {
                    string sql = "select nvl(item_flag,'N') from data_plan where plan_code='" + planCode + "' ";
                    if (dc.GetValue(sql) == "Y")
                    {
                        gridMaterial.JSProperties.Add("cpAlertContent", "计划已库房确认，不能替换！");
                        return;
                    }
                    //判断是否指定供应商
                    if (PubLogic.isZdgys(planCode, so, oldPart, cmbPline.Value.ToString()))
                    {
                        gridMaterial.JSProperties.Add("cpAlertContent", "已经指定供应商，不能替换！");
                        return;
                    }
                    BomReplaceFactory.PL_INSERT_SJBOMSOTH("ADD", so, oldPart, newPart, "", "", "",
                        userName, Request.UserHostAddress, planCode, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), locationCode, itemQty,
                        "", "0", plineCode);

                    //gridMaterial.JSProperties.Add("cpAlertContent", "替换成功");

                }

                gridMaterial.DataSource = dc.GetTable(Session["gridMaterial"].ToString());
                gridMaterial.DataBind();
            }
            //多对多替换
            if (e.ButtonID == "MultiReplace")
            {
                gridMaterial.JSProperties.Add("cpplineCode", plineCode);
                gridMaterial.JSProperties.Add("cpplanCode", planCode);
                gridMaterial.JSProperties.Add("cpso", so);
                gridMaterial.JSProperties.Add("cpitemCode", oldPart);
                gridMaterial.JSProperties.Add("cpnewPart", newPart);
                gridMaterial.JSProperties.Add("cplocationCode", locationCode);
                gridMaterial.JSProperties.Add("cpitemQty", itemQty);
            }


        }

        //前端弹出alert消息
        public static void showAlert(System.Web.UI.Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + msg.ToString() + "');</script>");
        }

        protected void ASPxGridView1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            //查询计划
            //string sql = "select PLAN_CODE ,PLAN_SO ,PLAN_QTY ,CUSTOMER_NAME ,ONLINE_QTY  FROM DATA_PLAN WHERE BEGIN_DATE>=to_date('" + dtRq.Date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') AND BEGIN_DATE<=to_date('" + dtRq.Date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') AND PLINE_CODE='" + cmbPline.Value.ToString() + "' and item_flag<>'N' and bom_flag='Y' and confirm_flag='Y' ORDER BY BEGIN_DATE,PLAN_CODE";
            //2016.11.1 如果是分装线，从remark字段取SO
            string sql = "select pline_type_CODE from code_product_line where pline_code='" + cmbPline.Text + "'";
            string plineType = dc.GetValue(sql);
            Session["RRFZTYPE"] = plineType;
            if (plineType == "B")
            {
                string sql11 = "select case when instr(remark,'-')<=0 then remark else substr(REMARK,0,instr(remark,'-')-1) end AS PLAN_SO FROM DATA_PLAN WHERE BEGIN_DATE>=to_date('" + dtRq.Date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') AND BEGIN_DATE<=to_date('" + dtRq.Date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') AND PLINE_CODE='" + cmbPline.Value.ToString() + "' and item_flag<>'N' and bom_flag='Y' and confirm_flag='Y' ORDER BY BEGIN_DATE,PLAN_CODE";
                Session["RRFZSO"] = dc.GetValue(sql11); 
            }
            
            sql = "select PLAN_CODE ,PLAN_SO ,PLAN_QTY ,CUSTOMER_NAME ,ONLINE_QTY  FROM DATA_PLAN WHERE BEGIN_DATE>=to_date('" + dtRq.Date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') AND BEGIN_DATE<=to_date('" + dtRq.Date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') AND PLINE_CODE='" + cmbPline.Value.ToString() + "' and item_flag<>'N' and bom_flag='Y' and confirm_flag='Y' ORDER BY BEGIN_DATE,PLAN_CODE";
            
            //if (plineType == "B")
            //    sql = "select PLAN_CODE ,case when instr(remark,'-')<=0 then remark else substr(REMARK,0,instr(remark,'-')-1) end AS PLAN_SO ,PLAN_QTY ,CUSTOMER_NAME ,ONLINE_QTY  FROM DATA_PLAN WHERE BEGIN_DATE>=to_date('" + dtRq.Date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') AND BEGIN_DATE<=to_date('" + dtRq.Date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') AND PLINE_CODE='" + cmbPline.Value.ToString() + "' and item_flag<>'N' and bom_flag='Y' and confirm_flag='Y' ORDER BY BEGIN_DATE,PLAN_CODE";
            //else
            //    sql = "select PLAN_CODE ,PLAN_SO ,PLAN_QTY ,CUSTOMER_NAME ,ONLINE_QTY  FROM DATA_PLAN WHERE BEGIN_DATE>=to_date('" + dtRq.Date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') AND BEGIN_DATE<=to_date('" + dtRq.Date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') AND PLINE_CODE='" + cmbPline.Value.ToString() + "' and item_flag<>'N' and bom_flag='Y' and confirm_flag='Y' ORDER BY BEGIN_DATE,PLAN_CODE";
            
            ASPxGridView1.DataSource = dc.GetTable(sql);
            ASPxGridView1.DataBind();
        }

        protected void ASPxGridView1_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.VisibleIndex < 0) return;

            //如果计划已替换，显示黄色
            string sql = "select * from sjbomsoth where jhdm='" + e.KeyValue.ToString() + "' and istrue=1";
            if (dc.GetState(sql))
            {
                e.Row.BackColor = System.Drawing.Color.Yellow;
            }
            sql = "select * from sjbomsothmuti where jhdm='" + e.KeyValue.ToString() + "' and istrue=1 ";
            if (dc.GetState(sql))
            {
                e.Row.BackColor = System.Drawing.Color.Yellow;
            }
        }

        protected void ASPxGridView1_PageIndexChanged(object sender, EventArgs e)
        {
            //2016.11.1 如果是分装线，从remark字段取SO
            string sql = "select pline_type_CODE from code_product_line where pline_code='" + cmbPline.Text + "'";
            string plineType = dc.GetValue(sql);
            Session["RRFZTYPE"] = plineType;
            if (plineType == "B")
            {
                string sql11 = "select case when instr(remark,'-')<=0 then remark else substr(REMARK,0,instr(remark,'-')-1) end AS PLAN_SO FROM DATA_PLAN WHERE BEGIN_DATE>=to_date('" + dtRq.Date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') AND BEGIN_DATE<=to_date('" + dtRq.Date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') AND PLINE_CODE='" + cmbPline.Value.ToString() + "' and item_flag<>'N' and bom_flag='Y' and confirm_flag='Y' ORDER BY BEGIN_DATE,PLAN_CODE";
                Session["RRFZSO"] = dc.GetValue(sql11);
            }

            sql = "select PLAN_CODE ,PLAN_SO ,PLAN_QTY ,CUSTOMER_NAME ,ONLINE_QTY  FROM DATA_PLAN WHERE BEGIN_DATE>=to_date('" + dtRq.Date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') AND BEGIN_DATE<=to_date('" + dtRq.Date.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') AND PLINE_CODE='" + cmbPline.Value.ToString() + "' and item_flag<>'N' and bom_flag='Y' and confirm_flag='Y' ORDER BY BEGIN_DATE,PLAN_CODE";
            ASPxGridView1.DataSource = dc.GetTable(sql);
            ASPxGridView1.DataBind();
        }
    }
}