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
            string sql11 = "select nvl(item_flag,'N') from data_plan where plan_code='" + Request["planCode"].ToString() + "' ";
            if (dc.GetValue(sql11) == "Y")
            {
                showAlert(this, "计划已库房确认，不能替换！");
                return;
            }

            //先删除
            BomReplaceFactory.MW_INSERT_SJBOMSOTHMUTI("DELETE", Request["so"].ToString(), "", "", userName, Request.UserHostAddress,
                Request["planCode"].ToString(), "", "", "", Request["plineCode"].ToString(), cmbGroup.Text, "", "");

            //逐条新增
            string sql = "select a.oldpart ,b.pt_desc2 oldpart_name, a.newpart ,c.pt_desc2 newpart_name,a.sl,d.location_code  from sjbomthset a"
             + " left outer join copy_pt_mstr b on a.oldpart=b.pt_part"
             + " left outer join copy_pt_mstr c on a.newpart=c.pt_part"
             + " left join DATA_PLAN_STANDARD_BOM d on a.oldpart = d.item_code and plan_code='" + Request["planCode"].ToString() + "' "
             + " where (so='" + Request["so"].ToString() + "' or so='" + thgxjx
             + "') and settype='1' and usetime<=to_date(to_char(sysdate,'yyyy-mm-dd'),'yyyy-mm-dd') and endtime>=to_date(to_char(sysdate,'yyyy-mm-dd'),'yyyy-mm-dd') and thgroup='" + cmbGroup.Text + "'";
            DataTable dt = dc.GetTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                BomReplaceFactory.MW_INSERT_SJBOMSOTHMUTI("ADD", Request["so"].ToString(), dt.Rows[i]["oldpart"].ToString(),
                    dt.Rows[i]["newPart"].ToString(), userName, Request.UserHostAddress, Request["planCode"].ToString(),
                    dt.Rows[i]["location_code"].ToString(), dt.Rows[i]["location_code"].ToString(), "", Request["plineCode"].ToString(), cmbGroup.Text,
                    dt.Rows[i]["sl"].ToString(), "");
            }
        }
        protected void ASPxGridView2_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            string sql = "select a.ljdm1,b.pt_desc2 oldpart_name ,a.GWMC1, a.ljdm2,c.pt_desc2 newpart_name,sl from SJBOMSOTHMUTI a"
                 + " left outer join copy_pt_mstr b on a.ljdm1=b.pt_part"
                 + " left outer join copy_pt_mstr c on a.ljdm2=c.pt_part"
                 + " where so='" + Request["so"].ToString() + "' and jhdm='" + Request["planCode"].ToString()
                 + "' and gzdd='" + Request["plineCode"].ToString() + "' and thgroup='" + cmbGroup.Text + "'";
            ASPxGridView2.DataSource = dc.GetTable(sql);
            ASPxGridView2.DataBind();
        }

        //前端弹出alert消息
        public static void showAlert(System.Web.UI.Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + msg.ToString() + "');</script>");
        }
    }
}