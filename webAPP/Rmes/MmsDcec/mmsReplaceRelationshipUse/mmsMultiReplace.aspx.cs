using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rmes.Pub.Data;
using System.Data;
using Oracle.DataAccess.Client;
using Rmes.DA.Factory;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxGridView;

namespace Rmes.WebApp.Rmes.MmsDcec.mmsReplaceRelationshipUse
{
    public partial class mmsMultiReplace : System.Web.UI.Page
    {
        private dataConn dc = new dataConn();
        private static string thgxjx;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string sql = "select config from copy_engine_property where upper(so)=upper('" + Request["so"].ToString() + "')";
                DataTable dt = dc.GetTable(sql);
                if (dt.Rows.Count > 0)
                {
                    thgxjx = dt.Rows[0][0].ToString();
                    if (thgxjx.Substring(thgxjx.Length - 2, 2) == "ZZ")
                        thgxjx = thgxjx.Substring(0, thgxjx.Length - 2);
                }

                //显示计划bom里存在的替换关系组
                sql = "select distinct thgroup from sjbomthset where (so='" + Request["so"].ToString()
                + "' or so='" + thgxjx + "') and settype='1' "
                + " and usetime<=to_date(to_char(sysdate,'yyyy-mm-dd'),'yyyy-mm-dd') and endtime>=to_date(to_char(sysdate,'yyyy-mm-dd'),'yyyy-mm-dd')"
                + " and oldpart in (select item_code from data_plan_standard_bom where plan_code='" + Request["planCode"].ToString() + "')"
                + " and oldpart not in (select xxptmp_comp from qad_xxptmp_mstr where xxptmp_jhdm='" + Request["planCode"].ToString() + "')"
                + " and thgroup not in( select distinct thgroup from sjbomthset where (so='" + Request["so"].ToString()
                + "' or so='" + thgxjx + "') and settype='1' "
                + " and usetime<=to_date(to_char(sysdate,'yyyy-mm-dd'),'yyyy-mm-dd') and endtime>=to_date(to_char(sysdate,'yyyy-mm-dd'),'yyyy-mm-dd')"
                + " and oldpart not in (select item_code from data_plan_standard_bom where plan_code='" + Request["planCode"].ToString() + "') and oldpart is not null )  ";

                //sql = "select distinct thgroup from sjbomsothmuti where so='" + Request["so"].ToString() + "' and jhdm='" + Request["planCode"].ToString() + "' and gzdd='" + ThisSite + "'";
                cmbGroup.DataSource = dc.GetTable(sql);
                cmbGroup.TextField = "thgroup";
                cmbGroup.ValueField = "thgroup";
                cmbGroup.DataBind();
            }

            initGrid2();
        }

        protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            string sql = "select a.oldpart ,b.pt_desc2 oldpart_name, a.newpart ,c.pt_desc2 newpart_name,a.sl,d.location_code  from sjbomthset a"
             + " left join copy_pt_mstr b on a.oldpart=b.pt_part"
             + " left join copy_pt_mstr c on a.newpart=c.pt_part"
             + " left join DATA_PLAN_STANDARD_BOM d on a.oldpart = d.item_code and plan_code='" + Request["planCode"].ToString() + "' "
             + " where (so='" + Request["so"].ToString() + "' or so='" + thgxjx
             + "') and settype='1' and usetime<=to_date(to_char(sysdate,'yyyy-mm-dd'),'yyyy-mm-dd') and endtime>=to_date(to_char(sysdate,'yyyy-mm-dd'),'yyyy-mm-dd') and thgroup='" + cmbGroup.Text + "'";
            //string sql = "select a.oldpart ,b.pt_desc2 oldpart_name, a.newpart ,c.pt_desc2 newpart_name,a.sl,d.location_code from vw_DATA_PLAN_STANDARD_BOM d where d.plan_so='SO42846' and d.plan_code='E20160918-01' and d.item_code in (select oldpart from sjbomthset where (so='" + Request["so"].ToString() + "' or so='" + thgxjx + "') and settype='1' and usetime<=to_date(to_char(sysdate,'yyyy-mm-dd'),'yyyy-mm-dd') and endtime>=to_date(to_char(sysdate,'yyyy-mm-dd'),'yyyy-mm-dd') )";
            gridMaterial.DataSource = dc.GetTable(sql);
            gridMaterial.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            string userName = theUserManager.getUserName();
            //string sql11 = "select nvl(item_flag,'N') from data_plan where plan_code='" + Request["planCode"].ToString() + "' ";
            //if (dc.GetValue(sql11) == "Y")
            //{
            //    showAlert(this, "计划已库房确认，不能替换！");
            //    return;
            //}

            //先删除
            BomReplaceFactory.MW_INSERT_SJBOMSOTHMUTI("DELETE", Request["so"].ToString(), "", "", userName, Request.UserHostAddress,
                Request["planCode"].ToString(), "", "", "", Request["plineCode"].ToString(), cmbGroup.Text, "", "");

            //逐条新增
            string sql = "select distinct a.oldpart ,b.pt_desc2 oldpart_name, a.newpart ,c.pt_desc2 newpart_name,a.sl  from sjbomthset a"
             + " left outer join copy_pt_mstr b on a.oldpart=b.pt_part"
             + " left outer join copy_pt_mstr c on a.newpart=c.pt_part"
             + " left join DATA_PLAN_STANDARD_BOM d on a.oldpart = d.item_code and plan_code='" + Request["planCode"].ToString() + "' "
             + " where (so='" + Request["so"].ToString() + "' or so='" + thgxjx
             + "') and settype='1' and usetime<=to_date(to_char(sysdate,'yyyy-mm-dd'),'yyyy-mm-dd') and endtime>=to_date(to_char(sysdate,'yyyy-mm-dd'),'yyyy-mm-dd') and thgroup='" + cmbGroup.Text + "'";
            DataTable dt = dc.GetTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string locationcode1 = dc.GetValue("select location_code from (select location_code from DATA_PLAN_STANDARD_BOM where plan_code='" + Request["planCode"].ToString() + "' and item_code='" + dt.Rows[i]["oldpart"].ToString() + "'  order by location_code) where rownum=1  ");
                BomReplaceFactory.MW_INSERT_SJBOMSOTHMUTI("ADD", Request["so"].ToString(), dt.Rows[i]["oldpart"].ToString(),
                    dt.Rows[i]["newPart"].ToString(), userName, Request.UserHostAddress, Request["planCode"].ToString(),
                    locationcode1, locationcode1, "", Request["plineCode"].ToString(), cmbGroup.Text,
                    dt.Rows[i]["sl"].ToString(), "");
            }
            initGrid2();
            ////逐条新增
            //string sql = "select a.oldpart ,b.pt_desc2 oldpart_name, a.newpart ,c.pt_desc2 newpart_name,a.sl,d.location_code  from sjbomthset a"
            // + " left outer join copy_pt_mstr b on a.oldpart=b.pt_part"
            // + " left outer join copy_pt_mstr c on a.newpart=c.pt_part"
            // + " left join DATA_PLAN_STANDARD_BOM d on a.oldpart = d.item_code and plan_code='" + Request["planCode"].ToString() + "' "
            // + " where (so='" + Request["so"].ToString() + "' or so='" + thgxjx
            // + "') and settype='1' and usetime<=to_date(to_char(sysdate,'yyyy-mm-dd'),'yyyy-mm-dd') and endtime>=to_date(to_char(sysdate,'yyyy-mm-dd'),'yyyy-mm-dd') and thgroup='" + cmbGroup.Text + "'";
            //DataTable dt = dc.GetTable(sql);

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    BomReplaceFactory.MW_INSERT_SJBOMSOTHMUTI("ADD", Request["so"].ToString(), dt.Rows[i]["oldpart"].ToString(),
            //        dt.Rows[i]["newPart"].ToString(), userName, Request.UserHostAddress, Request["planCode"].ToString(),
            //        dt.Rows[i]["location_code"].ToString(), dt.Rows[i]["location_code"].ToString(), "", Request["plineCode"].ToString(), cmbGroup.Text,
            //        dt.Rows[i]["sl"].ToString(), "");
            //}
        }

        //前端弹出alert消息
        public static void showAlert(System.Web.UI.Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + msg.ToString() + "');</script>");
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {

        }
        private void initGrid2()
        {
            string sql = "select a.ljdm1,b.pt_desc2 oldpart_name ,a.gwmc, a.ljdm2,c.pt_desc2 newpart_name,sl,a.gwmc1,a.gxmc1,a.rowid from SJBOMSOTHMUTI a"
                 + " left outer join copy_pt_mstr b on a.ljdm1=b.pt_part"
                 + " left outer join copy_pt_mstr c on a.ljdm2=c.pt_part"
                 + " where so='" + Request["so"].ToString() + "' and jhdm='" + Request["planCode"].ToString()
                 + "' and gzdd='" + Request["plineCode"].ToString() + "' and thgroup='" + cmbGroup.Text + "'";
            ASPxGridView2.DataSource = dc.GetTable(sql);
            ASPxGridView2.DataBind();        
        }
        protected void ASPxGridView2_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            string sql = "select a.ljdm1,b.pt_desc2 oldpart_name ,a.gwmc, a.ljdm2,c.pt_desc2 newpart_name,sl,a.gwmc1,a.gxmc1,a.rowid from SJBOMSOTHMUTI a"
                 + " left outer join copy_pt_mstr b on a.ljdm1=b.pt_part"
                 + " left outer join copy_pt_mstr c on a.ljdm2=c.pt_part"
                 + " where so='" + Request["so"].ToString() + "' and jhdm='" + Request["planCode"].ToString()
                 + "' and gzdd='" + Request["plineCode"].ToString() + "' and thgroup='" + cmbGroup.Text + "'";
            ASPxGridView2.DataSource = dc.GetTable(sql);
            ASPxGridView2.DataBind();   
        }
        protected void ASPxGridView2_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string gwmc = "", gwmc1 = "", gxmc = "", gxmc1 = "", ljdm1 = "", ljdm2 = "";
            string sql2 = "",rowid="";
            try
            {
                rowid = e.NewValues["ROWID"].ToString();
            }
            catch { }
            try
            {
                ljdm1 = e.NewValues["LJDM1"].ToString();
            }
            catch { }
            try
            {
                gwmc = e.NewValues["GWMC"].ToString();
            }
            catch { }
            try
            {
                ljdm2 = e.NewValues["LJDM2"].ToString();
            }
            catch { }
            try
            {
                gwmc1 = e.NewValues["GWMC1"].ToString();
            }
            catch { }
            try
            {
                gxmc1 = e.NewValues["GXMC1"].ToString();
            }
            catch { }
            if (ljdm1 == "")
            {
                sql2 = "update SJBOMSOTHMUTI set gwmc='" + gwmc + "',GWMC1='" + gwmc1
                   + "',GXMC1='" + gxmc1 + "' where LJDM2 = '" + ljdm2 + "' and jhdm='" + Request["planCode"].ToString() + "'";
            }
            else
            {
                sql2 = "update SJBOMSOTHMUTI set gwmc='" + gwmc + "',GWMC1='" + gwmc1
                   + "',GXMC1='" + gxmc1 + "' where LJDM1 = '" + ljdm1 + "' and jhdm='" + Request["planCode"].ToString() + "'";
            }

            //sql2 = "update SJBOMSOTHMUTI set gwmc='" + e.NewValues["GWMC"].ToString() + "',GWMC1='" + e.NewValues["GWMC1"].ToString()
            //    + "',GXMC1='" + e.NewValues["GXMC1"].ToString() + "' where LJDM1 = '" + e.Keys["LJDM1"].ToString() + "' and jhdm='" + Request["planCode"].ToString() + "'";
            //sql2 = "update SJBOMSOTHMUTI set gwmc='" + gwmc + "',GWMC1='" + gwmc1
            //   + "',GXMC1='" + gxmc1 + "' where LJDM1 = '" + ljdm1 + "' and jhdm='" + Request["planCode"].ToString() + "'";
            //sql2 = "update SJBOMSOTHMUTI set gwmc='" + gwmc + "',GWMC1='" + gwmc1
            //   + "',GXMC1='" + gxmc1 + "' where rowid = '" + rowid + "' and jhdm='" + Request["planCode"].ToString() + "'";
            dc.ExeSql(sql2);

            e.Cancel = true;
            ASPxGridView2.CancelEdit();
            initGrid2();
            return;
        }

        protected void ASPxGridView2_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditorEventArgs e)
        {
            ASPxGridView aspx1 = sender as ASPxGridView;
            //aspx1.KeyFieldName = "LJDM1";
            if (ASPxGridView2.IsEditing)
            {
                string rowid1 = "";
                try
                {
                    rowid1 = (string)ASPxGridView2.GetRowValuesByKeyValue(e.KeyValue, "ROWID");
                }
                catch { }
                string LJDM1 = "";
                try
                {
                    LJDM1 = dc.GetValue("select ljdm1  from SJBOMSOTHMUTI where so='" + Request["so"].ToString() + "' and jhdm='" + Request["planCode"].ToString()
                             + "' and rowid='"+rowid1+"'   ");
                    //LJDM1 = (string)ASPxGridView2.GetRowValuesByKeyValue(e.KeyValue, "LJDM1");
                }
                catch { }
                if (e.Column.FieldName == "GWMC")
                {
                    ASPxComboBox combo = e.Editor as ASPxComboBox;
                    string sql = "select location_code from data_plan_standard_Bom where plan_code='" + Request["planCode"].ToString() + "' and item_code='" + LJDM1 + "' order by location_code";
                    DataTable dt = dc.GetTable(sql);
                    combo.Items.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                        combo.Items.Add(dt.Rows[i]["location_code"].ToString(), dt.Rows[i]["location_code"].ToString());
                }
                if (e.Column.FieldName == "GWMC1")
                {
                    ASPxComboBox combo = e.Editor as ASPxComboBox;
                    string sql = "select distinct location_code from data_plan_standard_Bom where plan_code='" + Request["planCode"].ToString() + "' order by location_code ";
                    DataTable dt = dc.GetTable(sql);
                    combo.Items.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                        combo.Items.Add(dt.Rows[i]["location_code"].ToString(), dt.Rows[i]["location_code"].ToString());
                }
                if (e.Column.FieldName == "GXMC1")
                {
                    ASPxComboBox combo = e.Editor as ASPxComboBox;
                    string GWMC1 = "";
                    GWMC1 = dc.GetValue("select gwmc1  from SJBOMSOTHMUTI where so='" + Request["so"].ToString() + "' and jhdm='" + Request["planCode"].ToString()
                             + "' and rowid='" + rowid1 + "'   ");
                    string sql = "select distinct process_code from data_plan_standard_Bom where plan_code='" + Request["planCode"].ToString() + "' and location_code='" + GWMC1 + "' order by process_code";
                    dc.setTheSql(sql);
                    DataTable dt = dc.GetTable();

                    combo.Items.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                        combo.Items.Add(dt.Rows[i]["process_code"].ToString(), dt.Rows[i]["process_code"].ToString());
                    combo.Callback += new CallbackEventHandlerBase(cmbGXMC1_OnCallback);
                }
            }
        }
        protected void cmbGXMC1_OnCallback(object source, CallbackEventArgsBase e)
        {
            ASPxComboBox cmbRunner = source as ASPxComboBox;
            string GWMC1 = e.Parameter;

            string sql = "select distinct process_code from data_plan_standard_Bom where plan_code='" + Request["planCode"].ToString() + "' and location_code='" + GWMC1 + "' order by process_code";
            dc.setTheSql(sql);
            DataTable dt = dc.GetTable();

            cmbRunner.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
                cmbRunner.Items.Add(dt.Rows[i]["process_code"].ToString(), dt.Rows[i]["process_code"].ToString());

            cmbRunner.SelectedIndex = 0;
        }

        protected void ASPxGridView2_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.VisibleIndex < 0) return;
            string ljdm1 = e.GetValue("LJDM1").ToString(); //已确认绿色
            int count1 = Convert.ToInt32(dc.GetValue("select count(location_code) from data_plan_standard_Bom where plan_code='" + Request["planCode"].ToString() + "' and item_code='" + ljdm1 + "'"));
            if (count1 > 1)
            {
                e.Row.BackColor = System.Drawing.Color.Yellow;
            }
        }
    }
}