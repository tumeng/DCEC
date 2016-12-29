using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridLookup;
using Rmes.DA.Procedures;
using System.Data;
using Rmes.DA.Base;
using Rmes.Pub.Data;
using Rmes.Web.Base;
using Rmes.DA.Factory;

namespace Rmes.WebApp.Rmes.Sam.sam2700
{
    public partial class sam2700 : BasePage
    {
        private dataConn dc = new dataConn();
        private PubCs pc = new PubCs();
        public string theCompanyCode;

        protected void Page_Load(object sender, EventArgs e)
        {
            userManager theUserManager = (userManager)Session["theUserManager"];
            theCompanyCode = theUserManager.getCompanyCode();

            //初始化新增模版里员工下拉列表
            initUser();

            //初始化新增模版里生产线下拉列表
            initWorkUnit();

            setCondition();
        }

        private void initWorkUnit()
        {
            //初始化生产线下拉列表
            string sql = "select WORKUNIT_CODE,STATION_NAME,RMES_ID from CODE_STATION WHERE COMPANY_CODE = '" + theCompanyCode + "'";
            SqlRole.SelectCommand = sql;
            SqlRole.DataBind();
        }

        private void initUser()
        {
            //初始化用户下拉列表
            string sql = "SELECT USER_ID,USER_CODE,USER_NAME FROM VW_CODE_USER WHERE COMPANY_CODE = '" + theCompanyCode + "' ORDER BY USER_CODE";
            SqlUser.SelectCommand = sql;
            SqlUser.DataBind();
        }
        private void setCondition()
        {
            //初始化GRIDVIEW
            string sql = "SELECT * FROM VW_REL_USER_WORKUNIT WHERE COMPANY_CODE = '" + theCompanyCode + "'";
            DataTable dt = dc.GetTable(sql);

            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
        }
        protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            User_WorkUnitFactory.Delete(theCompanyCode, e.Values["USER_ID"].ToString(), e.Values["WORKUNIT_CODE"].ToString());

            setCondition();
            e.Cancel = true;
        }

        protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            //新增
            int indexUser, indexUnit;
            ASPxGridLookup gridLookupUser = ASPxGridView1.FindEditFormTemplateControl("GridLookupUser") as ASPxGridLookup;
            ASPxGridLookup gridLookupRole = ASPxGridView1.FindEditFormTemplateControl("GridLookupRole") as ASPxGridLookup;

            List<object> Users = gridLookupUser.GridView.GetSelectedFieldValues("USER_ID");
            List<object> WorkUnits = gridLookupRole.GridView.GetSelectedFieldValues("RMES_ID");
            for (indexUser = 0; indexUser < Users.Count; indexUser++)
            {
                for (indexUnit = 0; indexUnit < WorkUnits.Count; indexUnit++)
                {

                    User_WorkUnitFactory.Insert(theCompanyCode, Users[indexUser] as string, WorkUnits[indexUnit] as string);
                    
                }
            }
            setCondition();
            e.Cancel = true;
            ASPxGridView1.CancelEdit();
        }
        protected void ASPxGridView1_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
        {
            ASPxGridLookup gridLookupUser = ASPxGridView1.FindEditFormTemplateControl("GridLookupUser") as ASPxGridLookup;
            gridLookupUser.GridView.Width = 250;

            ASPxGridLookup gridLookupRole = ASPxGridView1.FindEditFormTemplateControl("GridLookupRole") as ASPxGridLookup;
            gridLookupRole.GridView.Width = 250;
        }
    }
}