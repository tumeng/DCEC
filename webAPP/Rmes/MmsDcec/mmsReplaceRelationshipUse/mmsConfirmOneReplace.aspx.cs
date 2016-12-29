using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rmes.Pub.Data;
using Rmes.DA.Factory;
using DevExpress.Web.ASPxGridView;

namespace Rmes.WebApp.Rmes.MmsDcec.mmsReplaceRelationshipUse
{
    public partial class mmsConfirmOneReplace : System.Web.UI.Page
    {
        private dataConn dc = new dataConn();
        private string userCode, userName,plineCode;
        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            userName = theUserManager.getUserName();
            plineCode = Request["plineCode"].ToString();

            if (!IsPostBack)
            {
                DateFrom.Date = System.DateTime.Today;
                DateTo.Date = System.DateTime.Today;

                //setCondition("");
            }
            setCondition("");
        }

        protected void grid_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            setCondition("");
        }
        private void setCondition(string flag)
        {
            string sql = "SELECT t.*,t.rowid FROM SJBOMSOTH t WHERE to_date(to_char(LRSJ,'yyyy-mm-dd'),'yyyy-mm-dd')>=to_date('" + DateFrom.Text 
                + "','yyyy-mm-dd') AND to_date(to_char(LRSJ,'yyyy-mm-dd'),'yyyy-mm-dd')<=to_date('" + DateTo.Text
                + "','yyyy-mm-dd') AND GZDD='" + plineCode + "' and ygmc='" + userName + "' ";
            if (txtPlanCode.Text != "")
                sql += " and jhdm='" + txtPlanCode.Text.ToUpper() + "'";

            if (txtSO.Text != "")
                sql += " and so='" + txtSO.Text.ToUpper() + "'";

            if (txtOldPart.Text != "")
                sql += " and ljdm1='" + txtOldPart.Text.ToUpper() + "'";

            if (txtNewPart.Text != "")
                sql += " and LJDM2=UPPER('" + txtNewPart.Text.ToUpper() + "') ";

            if (txtLocationCode.Text != "")
                sql += " and gwmc='" + txtLocationCode.Text.ToUpper() + "'";

            sql += " ORDER BY JHDM,GWMC";

            grid.DataSource = dc.GetTable(sql);
            grid.DataBind();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            ASPxGridViewExporter1.WriteXlsToResponse();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //ASPxGridView atl1 = (ASPxGridView)sender;
            List<object> rowid = grid.GetSelectedFieldValues("ROWID");
            //List<object> planCode = grid.GetSelectedFieldValues("JHDM");
            //List<object> oldPart = grid.GetSelectedFieldValues("LJDM1");
            //List<object> newPart = grid.GetSelectedFieldValues("LJDM2");
            //List<object> SO = grid.GetSelectedFieldValues("SO");
            //List<object> locationCode = grid.GetSelectedFieldValues("GWMC");
            //List<object> isTrue = grid.GetSelectedFieldValues("ISTRUE");
            

            userManager theUserManager = (userManager)Session["theUserManager"];
            string userName = theUserManager.getUserName();
            string companyCode = theUserManager.getCompanyCode();

            for (int i = 0; i < rowid.Count; i++)
            {
                string planCode = dc.GetValue("select jhdm from sjbomsoth where rowid='" + rowid[i].ToString() + "'");
                string SO = dc.GetValue("select SO from sjbomsoth where rowid='" + rowid[i].ToString() + "'"); ;
                string oldPart = dc.GetValue("select LJDM1 from sjbomsoth where rowid='" + rowid[i].ToString() + "'"); ;
                string newPart = dc.GetValue("select LJDM2 from sjbomsoth where rowid='" + rowid[i].ToString() + "'"); ;
                string locationCode = dc.GetValue("select GWMC from sjbomsoth where rowid='" + rowid[i].ToString() + "'");

                string sql = "select IS_JHOFFLINE('" + planCode.ToString() + "','" + SO.ToString()
                    + "','" + plineCode.ToString() + "') from dual";
                if (dc.GetValue(sql) == "1")
                {
                    Response.Write("<script>alert('计划已经全部下线,不能删除');</script>");
                    continue;
                }
               string is_confirm=dc.GetValue("select ISTRUE from sjbomsoth where rowid='" + rowid[i].ToString() + "'");
               if (is_confirm == "0")
               //yxh 修改 增加未确认的直接删除逻辑
               {

                   sql = "delete FROM SJBOMSOTH WHERE JHDM='" + planCode.ToString() + "' and ljdm1='" + oldPart.ToString()
                       + "' and ljdm2='" + newPart.ToString() + "' and so='" + SO.ToString() + "' AND GWMC='" + locationCode.ToString()
                       + "' AND GZDD='" + plineCode + "' and ygmc='" + userName + "'";
                   dc.ExeSql(sql);


               }
               else
               {
                   //sql = "select IS_JHONLINE('" + planCode.ToString() + "','" + SO.ToString()+"','" + plineCode + "') from dual";
                   sql = "select IS_JHPASSGW('" + planCode.ToString() + "','" + SO.ToString() + "','" + locationCode + "') from dual";
                   //如果已经确认，删除将提交确认流程 xuying 2016.9.9
                   //if (isTrue[i].ToString() == "1") {
                   if (dc.GetValue(sql) == "1")
                   {
                       Response.Write("<script>alert('计划已上线且经过该工位，进入审批流程');</script>");
                       BomReplaceFactory.MW_MODIFY_SJBOMTHCFM_DEL("ADD", planCode.ToString(), SO.ToString(), oldPart.ToString(),
                           newPart.ToString(), userName, locationCode.ToString(), plineCode);
                   }
                   else
                   {
                        
                       sql = "delete FROM SJBOMSOTH WHERE JHDM='" + planCode.ToString() + "' and ljdm1='" + oldPart.ToString()
                           + "' and ljdm2='" + newPart.ToString() + "' and so='" + SO.ToString() + "' AND GWMC='" + locationCode.ToString()
                           + "' AND GZDD='" + plineCode + "' and ygmc='" + userName + "'";
                       dc.ExeSql(sql);
                   }
               }

            }

            setCondition("");
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            //确认
            //ASPxGridView atl1 = (ASPxGridView)sender;
            List<object> rowid = grid.GetSelectedFieldValues("ROWID");
            //List<object> planCode = grid.GetSelectedFieldValues("JHDM");
            //List<object> oldPart = grid.GetSelectedFieldValues("LJDM1");
            //List<object> newPart = grid.GetSelectedFieldValues("LJDM2");
            //List<object> SO = grid.GetSelectedFieldValues("SO");
            //List<object> locationCode = grid.GetSelectedFieldValues("GWMC");

            userManager theUserManager = (userManager)Session["theUserManager"];
            string userName = theUserManager.getUserName();
            string companyCode = theUserManager.getCompanyCode();

            for (int i = 0; i < rowid.Count; i++)
            {
                string planCode = dc.GetValue("select jhdm from sjbomsoth where rowid='" + rowid[i].ToString() + "'");
                string SO = dc.GetValue("select SO from sjbomsoth where rowid='" + rowid[i].ToString() + "'"); ;
                string oldPart = dc.GetValue("select LJDM1 from sjbomsoth where rowid='" + rowid[i].ToString() + "'"); ;
                string newPart = dc.GetValue("select LJDM2 from sjbomsoth where rowid='" + rowid[i].ToString() + "'"); ;
                string locationCode = dc.GetValue("select GWMC from sjbomsoth where rowid='" + rowid[i].ToString() + "'");

                //string sql = "select IS_JHONLINE('" + planCode.ToString() + "','" + SO.ToString()
                //    + "','" + plineCode + "') from dual";
                //string retVal = dc.GetValue(sql);
                 string sql = "select IS_JHPASSGW('" + planCode.ToString() + "','" + SO.ToString() + "','" + locationCode + "') from dual";
                if (dc.GetValue(sql) == "0")//未经过该工位的可以进行替换确认
                {
                     

                    sql = "update SJBOMSOTH set istrue=1,qrygmc='" + userName + "' WHERE to_date(to_char(LRSJ,'yyyy-mm-dd'),'yyyy-mm-dd')>=to_date('" + DateFrom.Text
                    + "','yyyy-mm-dd') AND to_date(to_char(LRSJ,'yyyy-mm-dd'),'yyyy-mm-dd')<=to_date('" + DateTo.Text
                    + "','yyyy-mm-dd') AND GZDD='" + plineCode + "' and istrue<>1 and ygmc='" + userName + "' and jhdm='" + planCode.ToString()
                    + "' and ljdm1='" + oldPart.ToString() + "' and gwmc='" + locationCode.ToString() + "'";

                    dc.ExeSql(sql);
                }
                else
                {
                    //修改 yxh
                    
                    Response.Write("<script>alert('计划已上线且经过该工位，将提交给调度确认');</script>");
                    BomReplaceFactory.MW_MODIFY_SJBOMTHCFM("ADD", planCode, SO, oldPart, newPart,
                        userName, locationCode, plineCode);
                }

            }

            setCondition("");
        }
    }
}