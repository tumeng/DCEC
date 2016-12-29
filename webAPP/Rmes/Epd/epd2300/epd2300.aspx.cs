using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rmes.Pub.Data;
using Rmes.DA.Procedures;
using Rmes.DA.Base;
using DevExpress.Web.ASPxGridLookup;
using System.Data;
using Rmes.Web.Base;

namespace Rmes.WebApp.Rmes.Epd.epd2300
{
    public partial class epd2300 : BasePage
    {
        private dataConn dc = new dataConn();
        private PubCs pc = new PubCs();
        public string theCompanyCode;

        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();

            //初始化新增模版里程序下拉列表
            initPline();

            //初始化新增模版里角色下拉列表
            initDEPT();

            setCondition();
        }

        private void initDEPT()
        {
            //初始化角色下拉列表
            string sql = "SELECT DEPT_CODE,DEPT_NAME FROM CODE_DEPT WHERE COMPANY_CODE = '" + theCompanyCode + "'";
            SqlRole.SelectCommand = sql;
            SqlRole.DataBind();
        }

        private void initPline()
        {
            //初始化新增模版里程序下拉列表
            string sql = "SELECT PLINE_CODE,PLINE_NAME FROM CODE_PRODUCT_LINE WHERE COMPANY_CODE = '" + theCompanyCode + "'";
            SqlUser.SelectCommand = sql;
            SqlUser.DataBind();
        }
        private void setCondition()
        {
            //初始化GRIDVIEW
            string sql = "SELECT * FROM VW_REL_DEPT_PLIINE WHERE COMPANY_CODE = '" + theCompanyCode + "' ORDER BY DEPT_CODE,PLINE_CODE";
            DataTable dt = dc.GetTable(sql);

            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
        }
        protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string id = e.Values["RMES_ID"].ToString();
            string sql = "delete from REL_DEPT_PLINE WHERE RMES_ID="+id;
            dc.ExeSql(sql);

            setCondition();
            e.Cancel = true;
        }
        protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            //新增
            int indexPline, indexDEPT;
            ASPxGridLookup gridLookupPline = ASPxGridView1.FindEditFormTemplateControl("gridLookupPline") as ASPxGridLookup;
            ASPxGridLookup gridLookupDEPT = ASPxGridView1.FindEditFormTemplateControl("gridLookupDEPT") as ASPxGridLookup;

            List<object> plines = gridLookupPline.GridView.GetSelectedFieldValues("PLINE_CODE");
            List<object> depts = gridLookupDEPT.GridView.GetSelectedFieldValues("DEPT_CODE");

            for (indexDEPT = 0; indexDEPT < depts.Count; indexDEPT++)
            {
                for (indexPline = 0; indexPline < plines.Count; indexPline++)
                {
                    string sql = "insert into REL_DEPT_PLINE(RMES_ID,DEPT_CODE,PLINE_CODE,COMPANY_CODE) VALUES(Seq_Rmes_Id.Nextval,'" + depts[indexDEPT].ToString() + "','" + plines[indexPline] + "','"+theCompanyCode+"')";
                    dc.ExeSql(sql);
                }
            }
            setCondition();
            e.Cancel = true;
            ASPxGridView1.CancelEdit();
        }
        protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
        {
            ASPxGridLookup gridLookupProgram = ASPxGridView1.FindEditFormTemplateControl("gridLookupPline") as ASPxGridLookup;
            gridLookupProgram.GridView.Width = 250;

            ASPxGridLookup gridLookupRole = ASPxGridView1.FindEditFormTemplateControl("gridLookupDEPT") as ASPxGridLookup;
            gridLookupRole.GridView.Width = 250;
        }
    }
}

