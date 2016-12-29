using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rmes.Pub.Data;

namespace Rmes.WebApp.Rmes.MmsDcec.mmsMaterialRequisition
{
    public partial class mmsMaterialRequisitionDetail : System.Web.UI.Page
    {
        private dataConn dc = new dataConn();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RequisitionDetail"] != null)
            {
                string sql = Session["RequisitionDetail"].ToString();
                ASPxGridView1.DataSource = dc.GetTable(sql);
                ASPxGridView1.DataBind();
            }
        }
    }
}