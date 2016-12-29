using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rmes.Pub.Data;
using Rmes.DA.Factory;

namespace Rmes.WebApp.Rmes.MmsDCEC.mmsReplaceRelationshipSet
{
    public partial class mmsReplaceRelationshipSet : System.Web.UI.Page
    {
        private dataConn dc = new dataConn();
        private string sql = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["qryFlag"] != null)
            {
                if (Session["qryFlag"].ToString() == "single")
                    setConditionSingle();
                if (Session["qryFlag"].ToString() == "multi")
                    setConditionMulti();
            }
        }
        protected void btnQrySingle_Click(object sender, EventArgs e)
        { setConditionSingle(); Session["qryFlag"] = "single"; }
        private void setConditionSingle(){
            //一对一替换查询
            string condition = "";
            if (txtQryPart.Text != "")
                condition += " and oldpart='" + txtQryPart.Text + "'";

            if (txtQryPartRep.Text != "")
                condition += " and newpart='" + txtQryPartRep.Text + "'";

            if (txtQryPe.Text != "")
                condition += " and pefile='" + txtQryPe.Text + "'";

            //如果选中到期，那么查询还差15天到期的数据 20081211
            if (CheckExpire.Checked)
                condition += " and endtime>sysdate-1 and endtime<sysdate+30 ";
            else
            {
                //增加维护日期的过滤 20080225
                if (DTQueryFrom.Text != "")
                    condition += " and createtime >=to_date('" + DTQueryFrom.Date.ToString("yyyy-MM-dd HH:mm:ss") + "','YYYY-MM-DD HH24:MI:SS') ";

                if (DTQueryTo.Text != "")
                    condition += " and createtime <=to_date('" + DTQueryTo.Date.ToString("yyyy-MM-dd HH:mm:ss") + "','YYYY-MM-DD HH24:MI:SS')+1 ";
            }

            sql = "select SO,OLDPART,NEWPART,SL ,PEFILE,CREATEUSER,USETIME,ENDTIME,THGROUP,CREATETIME,XL,SITE "
                + "from sjbomthset where so like '%" + txtQrySo.Text + "%' and settype='0' " + condition + " order by so,oldpart";
            ASPxGridView1.DataSource = dc.GetTable(sql);
            ASPxGridView1.DataBind();

            
        }
        protected void cmdQuery2_Click(object sender, EventArgs e)
        { setConditionMulti(); Session["qryFlag"] = "multi"; }
        private void setConditionMulti(){
            //多对多替换查询
            string condition = "";
            if (txtQryPart.Text != "")
                condition += " and oldpart='" + txtQryPart.Text + "'";

            if (txtQryPartRep.Text != "")
                condition += " and newpart='" + txtQryPartRep.Text + "'";

            if (txtQryPe.Text != "")
                condition += " and pefile='" + txtQryPe.Text + "'";

            //如果选中到期，那么查询还差15天到期的数据 20081211
            if (CheckExpire.Checked)
                condition += " and endtime>sysdate-1 and endtime<sysdate+30 ";
            else
            {
                //增加维护日期的过滤 20080225
                if (DTQueryFrom.Text != "")
                    condition += " and createtime >=to_date('" + DTQueryFrom.Date.ToString("yyyy-MM-dd HH:mm:ss") + "','YYYY-MM-DD HH24:MI:SS') ";

                if (DTQueryTo.Text != "")
                    condition += " and createtime <=to_date('" + DTQueryTo.Date.ToString("yyyy-MM-dd HH:mm:ss") + "','YYYY-MM-DD HH24:MI:SS')+1 ";
            }

            sql = "select SO,OLDPART,NEWPART,SL ,PEFILE,CREATEUSER,USETIME,ENDTIME,THGROUP,CREATETIME,XL,SITE "
                + "from sjbomthset where thgroup in (select distinct thgroup from sjbomthset where 1=1 " + condition
                + ") order by so,thgroup,oldpart";
            ASPxGridView1.DataSource = dc.GetTable(sql);
            ASPxGridView1.DataBind();
        }

        protected void CmdExp_Click(object sender, EventArgs e)
        {
            ASPxGridViewExporter1.WriteXlsToResponse("物料替换关系.xls");
        }

        protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string thisPart = e.Values["OLDPART"].ToString();
            string thisPartNew = e.Values["NEWPART"].ToString();
            string thisSO = e.Values["SO"].ToString();
            string thisPe = e.Values["PEFILE"].ToString();
            string thisSite = e.Values["SITE"].ToString();
            string userNmae = e.Values["CREATEUSER"].ToString();
            string thisFromDate = Convert.ToDateTime(e.Values["USETIME"]).ToString("yyyy-MM-dd");
            string thisToDate = Convert.ToDateTime(e.Values["ENDTIME"]).ToString("yyyy-MM-dd");
            string thisGroup = e.Values["THGROUP"].ToString();

            if (Session["qryFlag"].ToString() == "single")
            {
                BomReplaceFactory.MW_MODIFY_SJBOMTHSET("DELETE", thisPart, thisPartNew, thisSO, thisPe, thisSite, userNmae, thisFromDate, thisToDate, "", "", "");
                setConditionSingle();
            
            }
            if (Session["qryFlag"].ToString() == "multi")
            {
                BomReplaceFactory.MW_MODIFY_SJBOMTHSET("DELETE2", thisPart, thisPartNew, thisSO, thisPe, thisSite, userNmae, thisFromDate, thisToDate, thisGroup, "", "");
                setConditionMulti();            
            }
            e.Cancel = true;
        }
    }
}