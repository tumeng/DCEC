using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PetaPoco;
using Rmes.DA.Base;
using Rmes.DA.Entity;
using Rmes.DA.Dal;
using Rmes.DA.Factory;
using Rmes.Pub.Data;
using Rmes.Web.Base;
using System.Data;
using DevExpress.Web.ASPxGridView;

/**
 * 功能概述：QAD计划查询
 * 作者：游晓航
 * 创建时间：2016-06-21
 */
namespace Rmes.WebApp.Rmes.Atpu.atpu1300
{
    public partial class atpu1300 : BasePage
    {
        private dataConn dc = new dataConn();
        PubCs thePubCs = new PubCs();
        public DateTime  theBeginDate, theEndDate;
        protected void Page_Load(object sender, EventArgs e)
        {
            setCondition();
        }
        private void setCondition()
        {
            string sql = "";
            sql = "select * from COPY_RPS_MSTR WHERE ";

            if (ASPxDateEdit1.Text.Trim() != "" && ASPxDateEdit3.Text.Trim() != "")
            {
                sql = sql + "  RPS_DUE_DATE>=to_date('" + ASPxDateEdit1.Text.Trim() + "','YYYY-MM-DD') AND RPS_DUE_DATE<=to_date('" + ASPxDateEdit3.Text.Trim() + "','YYYY-MM-DD') ";
            }
            else if (ASPxDateEdit1.Text.Trim() != "" && ASPxDateEdit3.Text.Trim() == "")
            {
                sql = sql + "  RPS_DUE_DATE>=to_date('" + ASPxDateEdit1.Text.Trim() + "','YYYY-MM-DD') ";
            }
            else if (ASPxDateEdit1.Text.Trim() == "" && ASPxDateEdit3.Text.Trim() != "")
            {
                sql = sql + "  RPS_DUE_DATE<=to_date('" + ASPxDateEdit3.Text.Trim() + "','YYYY-MM-DD') ";
            }
            else
            {
                sql = sql + "  1=1 ";
            }
            DataTable dt = dc.GetTable(sql);
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
        }
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            setCondition();
        }
    }
}