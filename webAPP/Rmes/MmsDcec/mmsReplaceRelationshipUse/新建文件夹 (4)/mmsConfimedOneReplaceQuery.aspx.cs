﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rmes.Pub.Data;

namespace Rmes.WebApp.Rmes.MmsDcec.mmsReplaceRelationshipUse
{
    public partial class mmsConfimedOneReplaceQuery : System.Web.UI.Page
    {
        private dataConn dc = new dataConn();
        protected void Page_Load(object sender, EventArgs e)
        {
            string so=Request["so"].ToString();
            string planCode=Request["planCode"].ToString();
            string plineCode=Request["plineCode"].ToString();
            string sql = "select ljdm1 ,ljdm2 ,gwmc ,zjsl ,ygmc ,lrsj  from sjbomsoth where gzdd='" + plineCode + "' and jhdm='" + planCode + "' and so='" + so + "' and istrue='1'";
            ASPxGridView1.DataSource = dc.GetTable(sql);
            ASPxGridView1.DataBind();
        }
    }
}